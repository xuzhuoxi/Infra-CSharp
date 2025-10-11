using System.Threading;

namespace JLGames.Infra.Event
{
    public interface IThreadEventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// 是否为空的线程上下文
        /// </summary>
        bool IsNullContext { get; }
        
        /// <summary>
        /// 设置线程上下文
        /// </summary>
        /// <param name="context"></param>
        void SetThreadEventContext(SynchronizationContext context);

        /// <summary>
        /// 清除线程上下文
        /// </summary>
        void ClearThreadEventContext();
    }
}