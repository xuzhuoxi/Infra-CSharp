using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Progress metering interface
    /// 进度计量接口
    /// </summary>
    public interface IProgressingService : IEventDispatcher
    {
        /// <summary>
        /// Total progress
        /// 进度总量
        /// [0,int.Max)
        /// </summary>
        uint ProgressingLen { get; }

        /// <summary>
        /// Current amount of progress
        /// 进度当前量
        /// [0,Total]
        /// </summary>
        uint ProgressingCurrent { get; }
    }
}