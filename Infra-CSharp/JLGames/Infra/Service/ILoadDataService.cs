using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Load persisted or external data for a service.
    /// 服务的数据加载接口。
    /// Invoked sequentially by <see cref="ServiceManager.LoadServicesData"/> for each registered implementation.
    /// 由 <see cref="ServiceManager.LoadServicesData"/> 按配置顺序依次调用。
    /// </summary>
    public interface ILoadDataService : IEventDispatcher
    {
        /// <summary>
        /// Load data; dispatch <see cref="ServiceEvents.OnServiceDataLoaded"/> with the service name when done.
        /// 加载数据；完成后应派发 <see cref="ServiceEvents.OnServiceDataLoaded"/>，数据为服务名称。
        /// </summary>
        void LoadData();
    }
}