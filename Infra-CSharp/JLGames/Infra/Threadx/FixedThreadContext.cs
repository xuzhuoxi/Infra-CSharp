using System;
using System.Collections.Concurrent;
using System.Threading;

namespace JLGames.Infra.Threadx
{
    public class FixedThreadContext : SynchronizationContext, IDisposable, ICloneable<SynchronizationContext>
    {
        private const int KAwqInitialCapacity = 20;
        private readonly BlockingCollection<ContextWorkRequest> m_AsyncWorkQueue;
        private readonly int m_MainThreadId;
        private int m_ExecFlag = 0;

        public int MainThreadId => m_MainThreadId;

        public int PendingCount => m_AsyncWorkQueue.Count;

        public FixedThreadContext(int mainThreadId)
        {
            m_AsyncWorkQueue = new BlockingCollection<ContextWorkRequest>(KAwqInitialCapacity);
            m_MainThreadId = mainThreadId;
        }

        public FixedThreadContext(BlockingCollection<ContextWorkRequest> queue, int mainThreadId)
        {
            m_AsyncWorkQueue = queue;
            m_MainThreadId = mainThreadId;
        }

        // Post will add the call to a task list to be executed later on the main thread then work will continue asynchronously
        public override void Post(SendOrPostCallback callback, object state)
        {
            m_AsyncWorkQueue.Add(new ContextWorkRequest(callback, state));
        }

        // Send will process the call synchronously. If the call is processed on the main thread, we'll invoke it
        // directly here. If the call is processed on another thread it will be queued up like POST to be executed
        // on the main thread and it will wait. Once the main thread processes the work we can continue
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

        public void Dispose()
        {
            if (Interlocked.CompareExchange(ref m_ExecFlag, 0, 1) == 1)
            {
                m_AsyncWorkQueue.CompleteAdding();
            }
        }


        public SynchronizationContext Clone()
        {
            return new FixedThreadContext(m_AsyncWorkQueue, m_MainThreadId);
        }

        public void StartExec()
        {
            if (Interlocked.CompareExchange(ref m_ExecFlag, 1, 0) == 0)
            {
                var thread = new Thread(Exec) { IsBackground = true };
                thread.Start();
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
            }
        }
    }
}