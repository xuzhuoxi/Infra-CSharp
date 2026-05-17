namespace JLGames.Infra.Serial
{
    /// <summary>
    /// Lifecycle state of <see cref="SerialManager"/>.
    /// <see cref="SerialManager"/> 的生命周期状态。
    /// </summary>
    public enum SerialStatus
    {
        /// <summary>
        /// All modules stopped; ready to start.
        /// 全部模块已停止；可发起启动。
        /// </summary>
        Stopped,

        /// <summary>
        /// Startup in progress (modules starting sequentially).
        /// 启动进行中（模块按序启动）。
        /// </summary>
        Starting,

        /// <summary>
        /// All modules started; ready to stop.
        /// 全部模块已启动；可发起停止。
        /// </summary>
        Started,

        /// <summary>
        /// Shutdown in progress (modules stopping in reverse order).
        /// 停止进行中（模块按逆序停止）。
        /// </summary>
        Stopping
    }
}
