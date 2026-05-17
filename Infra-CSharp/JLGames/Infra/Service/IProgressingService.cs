using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Reports granular init progress; replaces the default one-step count per <see cref="IInitService"/> / <see cref="IInitDataService"/>.
    /// 上报细粒度初始化进度；可替代每个 <see cref="IInitService"/> / <see cref="IInitDataService"/> 默认的单步计数。
    /// Increment progress and dispatch <see cref="ServiceEvents.OnServiceProcessing"/> from the service implementation.
    /// 由服务实现递增进度并派发 <see cref="ServiceEvents.OnServiceProcessing"/>。
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