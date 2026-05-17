namespace JLGames.Infra.Service
{
    /// <summary>
    /// Resets a service to its pre-init state (listeners, flags, timers, etc.).
    /// 将服务重置到未初始化状态（监听、标志、计时器等）。
    /// Called on each service by <see cref="ServiceManager.ClearServices"/>.
    /// 由 <see cref="ServiceManager.ClearServices"/> 对每个服务调用。
    /// </summary>
    public interface IClearService
    {
        /// <summary>
        /// reset
        /// 重置
        /// Clear events, clear timers, etc.
        /// 清除事件、清除计时器等
        /// </summary>
        void Clear();
    }
}