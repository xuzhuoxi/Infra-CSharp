using System;
using System.Collections.Concurrent;
using System.Threading;

namespace JLGames.Infra.Threadx
{
    /// <summary>
    /// <see cref="SynchronizationContext"/> that marshals work to a fixed main thread via a blocking queue.
    /// 将工作通过阻塞队列派发到固定主线程的 <see cref="SynchronizationContext"/>。
    /// </summary>
    public class FixedThreadContext : SynchronizationContext, IDisposable, ICloneable<SynchronizationContext>
    {
        private const int KAwqInitialCapacity = 20;
        private readonly BlockingCollection<ContextWorkRequest> m_AsyncWorkQueue;
        private readonly int m_MainThreadId;
        private bool m_Disposed;
        private int m_ExecFlag = 0;

        /// <summary>
        /// Managed thread ID of the thread that owns this context (target for marshaled work).
        /// 拥有本上下文的托管线程 ID（工作派发的目标线程）。
        /// </summary>
        public int MainThreadId => m_MainThreadId;

        /// <summary>
        /// Number of work items currently queued and not yet taken.
        /// 当前已入队、尚未取出的工作项数量。
        /// </summary>
        public int PendingCount => m_AsyncWorkQueue.Count;

        /// <summary>
        /// Creates a context with a new internal queue bound to <paramref name="mainThreadId"/>.
        /// 创建绑定到 <paramref name="mainThreadId"/> 的上下文，并新建内部队列。
        /// </summary>
        /// <param name="mainThreadId">Managed thread ID of the main thread. 主线程的托管线程 ID。</param>
        public FixedThreadContext(int mainThreadId)
        {
            m_AsyncWorkQueue = new BlockingCollection<ContextWorkRequest>(KAwqInitialCapacity);
            m_MainThreadId = mainThreadId;
        }

        /// <summary>
        /// Creates a context that shares an existing queue (used by <see cref="Clone"/>).
        /// 使用已有队列创建上下文（供 <see cref="Clone"/> 使用）。
        /// </summary>
        /// <param name="queue">Shared async work queue. 共享的异步工作队列。</param>
        /// <param name="mainThreadId">Managed thread ID of the main thread. 主线程的托管线程 ID。</param>
        public FixedThreadContext(BlockingCollection<ContextWorkRequest> queue, int mainThreadId)
        {
            m_AsyncWorkQueue = queue;
            m_MainThreadId = mainThreadId;
        }

        /// <summary>
        /// Queues the callback for asynchronous execution on the main thread; the caller returns immediately.
        /// 将回调加入队列，在主线程上异步执行；调用方立即返回。
        /// </summary>
        /// <param name="callback">Delegate to run on the main thread. 在主线程上执行的委托。</param>
        /// <param name="state">State passed to <paramref name="callback"/>. 传给 <paramref name="callback"/> 的状态对象。</param>
        public override void Post(SendOrPostCallback callback, object state)
        {
            lock (m_AsyncWorkQueue)
            {
                m_AsyncWorkQueue.Add(new ContextWorkRequest(callback, state));
            }
        }

        /// <summary>
        /// Runs the callback on the main thread and blocks until it completes.
        /// 在主线程上执行回调并阻塞调用方直至完成。
        /// </summary>
        /// <remarks>
        /// If already on the main thread, invokes <paramref name="callback"/> directly; otherwise enqueues with a wait handle and blocks.
        /// 若已在主线程则直接调用 <paramref name="callback"/>；否则入队并借助等待句柄阻塞直至执行完毕。
        /// </remarks>
        /// <param name="callback">Delegate to run on the main thread. 在主线程上执行的委托。</param>
        /// <param name="state">State passed to <paramref name="callback"/>. 传给 <paramref name="callback"/> 的状态对象。</param>
        public override void Send(SendOrPostCallback callback, object state)
        {
            if (m_MainThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId)
            {
                callback(state);
            }
            else
            {
                using (var waitHandle = new ManualResetEvent(false))
                {
                    lock (m_AsyncWorkQueue)
                    {
                        m_AsyncWorkQueue.Add(new ContextWorkRequest(callback, state, waitHandle));
                    }

                    waitHandle.WaitOne();
                }
            }
        }

        /// <summary>
        /// Stops background execution and marks the queue as complete for adding.
        /// 停止后台执行，并将队列标记为不再接受新项。
        /// </summary>
        public void Dispose()
        {
            StopExec();
        }

        /// <summary>
        /// Returns a new context instance that shares the same queue and main thread ID.
        /// 返回共享同一队列与主线程 ID 的新上下文实例。
        /// </summary>
        /// <returns>A cloned <see cref="SynchronizationContext"/>. 克隆后的 <see cref="SynchronizationContext"/>。</returns>
        public SynchronizationContext Clone()
        {
            return new FixedThreadContext(m_AsyncWorkQueue, m_MainThreadId);
        }

        /// <summary>
        /// Drains all currently queued work items; intended to be called from the main thread (or an external pump).
        /// 处理当前队列中的全部工作项；由主线程或外部泵循环调用。
        /// </summary>
        public void ProcessTasks()
        {
            ProcessTasks(m_AsyncWorkQueue.Count);
        }

        /// <summary>
        /// Dequeues and runs at most <paramref name="maxTaskSize"/> work items without blocking on an empty queue.
        /// 最多取出并执行 <paramref name="maxTaskSize"/> 个工作项；队列为空时不阻塞。
        /// </summary>
        /// <param name="maxTaskSize">Maximum number of items to process this call. 本次调用最多处理的工作项数。</param>
        public void ProcessTasks(int maxTaskSize)
        {
            for (var i = 0; i < maxTaskSize; i++)
            {
                try
                {
                    if (m_AsyncWorkQueue.TryTake(out ContextWorkRequest workRequest)) // 这里不会阻塞
                        workRequest.Invoke();
                    else
                        break;
                }
                catch (InvalidOperationException)
                {
                    // 当 BlockingCollection 被标记为完成时，会抛出异常
                    break;
                }
            }
        }

        /// <summary>
        /// Starts a background thread that blocks on the queue and invokes work items until <see cref="StopExec"/>.
        /// 启动后台线程，阻塞等待队列中的工作项并执行，直至调用 <see cref="StopExec"/>。
        /// </summary>
        public void StartExec()
        {
            if (m_Disposed) return;
            if (Interlocked.CompareExchange(ref m_ExecFlag, 1, 0) == 0)
            {
                var thread = new Thread(Exec) { IsBackground = true };
                thread.Start();
            }
        }

        /// <summary>
        /// Stops accepting new work and completes the queue; the background thread exits after pending items are processed.
        /// 停止接受新工作并完成队列；待处理项执行完毕后后台线程退出。
        /// </summary>
        public void StopExec()
        {
            if (m_Disposed) return;
            if (Interlocked.CompareExchange(ref m_ExecFlag, 0, 1) == 1)
            {
                m_AsyncWorkQueue.CompleteAdding();
                m_Disposed = true;
            }
        }

        // Exec will execute tasks off the task list
        private void Exec()
        {
            while (1 == m_ExecFlag)
            {
                try
                {
                    var work = m_AsyncWorkQueue.Take(); // 这里会阻塞，直到有任务返回
                    work.Invoke();
                }
                catch (InvalidOperationException)
                {
                    // 当 BlockingCollection 被标记为完成时，会抛出异常
                    break;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}
