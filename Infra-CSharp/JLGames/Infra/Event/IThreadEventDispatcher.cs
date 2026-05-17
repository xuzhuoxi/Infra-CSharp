using System.Threading;

namespace JLGames.Infra.Event
{
    /// <summary>
    /// Event dispatcher that can marshal dispatch onto a synchronization context.
    /// 可将事件派发到指定同步上下文的事件调度器。
    /// </summary>
    public interface IThreadEventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// Whether no thread synchronization context is set.
        /// 是否未设置线程同步上下文。
        /// </summary>
        bool IsNullContext { get; }
        
        /// <summary>
        /// Set the synchronization context used when dispatching events.
        /// 设置派发事件时使用的同步上下文。
        /// </summary>
        /// <param name="context">Synchronization context with queue/scheduling support.<br/>具备队列或调度能力的同步上下文。</param>
        void SetThreadEventContext(SynchronizationContext context);

        /// <summary>
        /// Clear the thread synchronization context (dispatch runs on caller thread).
        /// 清除线程同步上下文（随后在调用线程上直接派发）。
        /// </summary>
        void ClearThreadEventContext();
    }
}