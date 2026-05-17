namespace JLGames.Infra.Serial
{
    /// <summary>
    /// Serial module manager contract; registers modules and starts/stops them in registration order (reverse on stop).
    /// 串行模块管理器契约；注册模块并按注册顺序依次启动（停止时逆序）。
    /// </summary>
    public interface ISerialManager
    {
        /// <summary>
        /// Append a module to the end of the serial chain.
        /// 将模块追加到串行链末尾。
        /// </summary>
        /// <param name="module">Module to register; ignored if null.<br/>待注册的模块；为 null 时忽略。</param>
        void AppendModule(ISerialModule module);

        /// <summary>
        /// Start all registered modules in order; invokes <paramref name="endCall"/> when all have started.
        /// 按顺序启动所有已注册模块；全部启动完成后调用 <paramref name="endCall"/>。
        /// </summary>
        /// <param name="endCall">Optional callback after the manager and all modules have started.<br/>管理器及全部模块启动完成后的可选回调。</param>
        /// <returns><c>true</c> if started from <see cref="SerialStatus.Stopped"/>; otherwise <c>false</c>.<br/>从 <see cref="SerialStatus.Stopped"/> 状态成功发起启动时返回 <c>true</c>，否则返回 <c>false</c>。</returns>
        bool StartManager(Callback endCall = null);

        /// <summary>
        /// Stop all registered modules in reverse order; invokes <paramref name="endCall"/> when all have stopped.
        /// 按逆序停止所有已注册模块；全部停止完成后调用 <paramref name="endCall"/>。
        /// </summary>
        /// <param name="endCall">Optional callback after the manager and all modules have stopped.<br/>管理器及全部模块停止完成后的可选回调。</param>
        /// <returns><c>true</c> if stopped from <see cref="SerialStatus.Started"/>; otherwise <c>false</c>.<br/>从 <see cref="SerialStatus.Started"/> 状态成功发起停止时返回 <c>true</c>，否则返回 <c>false</c>。</returns>
        bool StopManager(Callback endCall = null);
    }
}
