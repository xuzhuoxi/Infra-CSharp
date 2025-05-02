using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Initialize data processing services
    /// 初始化数据处理服务
    /// </summary>
    public interface IInitDataService : IService, IEventDispatcher
    {
        /// <summary>
        /// Whether data initialization has been completed
        /// 是否已经完成数据初始化
        /// </summary>
        bool IsDataInited { get; }

        /// <summary>
        /// Initialization data
        /// 初始化数据
        /// </summary>
        void InitData();
    }
}