using JLGames.Infra.Event;

namespace JLGames.Infra.Serial
{
    public interface ISerialModule : IEventDispatcher
    {
        /// <summary>
        /// Start
        /// 启动
        /// </summary>
        void Startup();

        /// <summary>
        /// Stop
        /// 停止
        /// </summary>
        void Shutdown();
    }
}