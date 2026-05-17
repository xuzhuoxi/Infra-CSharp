using System.Threading;

namespace JLGames.Infra.Threadx
{
    /// <summary>
    /// Immutable work item for <see cref="FixedThreadContext"/>; wraps a callback, state, and optional wait handle.
    /// <see cref="FixedThreadContext"/> 的不可变工作项，封装回调、状态及可选的等待句柄。
    /// </summary>
    public readonly struct ContextWorkRequest
    {
        private readonly SendOrPostCallback m_DelegateCallback;
        private readonly object m_DelegateState;
        private readonly ManualResetEvent m_WaitHandle;

        /// <summary>
        /// Creates a work request to be queued and executed on the main thread.
        /// 创建工作请求，供主线程队列调度执行。
        /// </summary>
        /// <param name="callback">Delegate to invoke on the main thread. 在主线程上调用的委托。</param>
        /// <param name="state">State passed to <paramref name="callback"/>. 传给 <paramref name="callback"/> 的状态对象。</param>
        /// <param name="waitHandle">
        /// Optional signal set in <see cref="Invoke"/> after the callback completes; used by synchronous <see cref="FixedThreadContext.Send"/>.
        /// 可选信号量，在 <see cref="Invoke"/> 回调结束后置位；供 <see cref="FixedThreadContext.Send"/> 同步等待使用。
        /// </param>
        public ContextWorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
        {
            m_DelegateCallback = callback;
            m_DelegateState = state;
            m_WaitHandle = waitHandle;
        }

        /// <summary>
        /// Invokes the callback and signals <c>waitHandle</c> when present.
        /// 执行回调；若存在等待句柄则在完成后置位。
        /// </summary>
        public void Invoke()
        {
            try
            {
                m_DelegateCallback(m_DelegateState);
            }
            finally
            {
                m_WaitHandle?.Set();
            }
        }
    }
}
