using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Save data processing interface
    /// 保存数据处理接口
    /// </summary>
    public interface ISaveDataService : IEventDispatcher
    {
        /// <summary>
        /// Save data
        /// 保存数据 
        /// </summary>
        void SaveData();
    }
}