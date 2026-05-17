using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Persist service data.
    /// 服务的数据保存接口。
    /// Invoked sequentially by <see cref="ServiceManager.SaveServicesData"/> for each registered implementation.
    /// 由 <see cref="ServiceManager.SaveServicesData"/> 按配置顺序依次调用。
    /// </summary>
    public interface ISaveDataService : IEventDispatcher
    {
        /// <summary>
        /// Save data; dispatch <see cref="ServiceEvents.OnServiceDataSaved"/> with the service name when done.
        /// 保存数据；完成后应派发 <see cref="ServiceEvents.OnServiceDataSaved"/>，数据为服务名称。
        /// </summary>
        void SaveData();
    }
}