using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Load data processing interface
    /// 加载数据处理接口
    /// </summary>
    public interface ILoadDataService : IEventDispatcher
    {
        /// <summary>
        /// Load Data
        /// 加载数据 
        /// </summary>
        void LoadData();
    }
}