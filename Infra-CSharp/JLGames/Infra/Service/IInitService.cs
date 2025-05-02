using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Service initialization interface
    /// 服务初始化接口
    /// Only when the current interface is implemented and configured into ServiceConfig,
    /// the init method will be executed during the initialization process
    /// 只有实现了当前接口，并配置到ServiceConfig中时，在初始化过程中才会执行init方法
    /// </summary>
    public interface IInitService : IService, IEventDispatcher
    {
        /// <summary>
        /// Whether the initialization has been completed
        /// 是否已经完成初始化
        /// </summary>
        bool IsInited { get; }

        /// <summary>
        /// Initialize base data
        /// 初始化基础数据
        /// </summary>
        void Init();
    }
}