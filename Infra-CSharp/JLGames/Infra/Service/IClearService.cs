namespace JLGames.Infra.Service
{
    /// <summary>
    /// Reset service interface, providing reset service to initialized state
    /// 重置服务接口，提供把服务重置到初始化状态
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