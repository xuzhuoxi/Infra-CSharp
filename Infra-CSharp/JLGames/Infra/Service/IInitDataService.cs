using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Data initialization phase for a service.
    /// 服务的数据初始化阶段接口。
    /// Only when implemented and registered in <see cref="ServiceConfig"/>,
    /// <see cref="InitData"/> runs after all <see cref="IInitService"/> instances complete during startup.
    /// 只有实现并注册到 <see cref="ServiceConfig"/> 后，启动时才会在所有 <see cref="IInitService"/> 完成之后执行 <see cref="InitData"/>。
    /// </summary>
    public interface IInitDataService : IService, IEventDispatcher
    {
        /// <summary>
        /// Whether data initialization has been completed
        /// 是否已经完成数据初始化
        /// </summary>
        bool IsDataInited { get; }

        /// <summary>
        /// Initialize runtime data; dispatch <see cref="ServiceEvents.OnServiceDataInited"/> with <see cref="IService.ServiceName"/> when done.
        /// 初始化运行时数据；完成后应派发 <see cref="ServiceEvents.OnServiceDataInited"/>，数据为 <see cref="IService.ServiceName"/>。
        /// </summary>
        void InitData();
    }
}