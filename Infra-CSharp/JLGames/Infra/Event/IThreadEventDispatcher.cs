using System.Threading;

namespace JLGames.Infra.Event
{
    public interface IThreadEventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// 设置线程上下文
        /// </summary>
        /// <param name="context"></param>
        void SetSyncContext(SynchronizationContext context);

        /// <summary>
        /// 清除线程上下文
        /// </summary>
        void ClearSyncContext();
    }
}