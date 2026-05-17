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
        /// Initialize the service; dispatch <see cref="ServiceEvents.OnServiceInited"/> with <see cref="IService.ServiceName"/> when done.
        /// 初始化服务；完成后应派发 <see cref="ServiceEvents.OnServiceInited"/>，数据为 <see cref="IService.ServiceName"/>。
        /// </summary>
        void Init();
    }
}