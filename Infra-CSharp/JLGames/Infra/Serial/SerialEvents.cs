namespace JLGames.Infra.Serial
{
    /// <summary>
    /// Event type constants for serial module and manager lifecycle.
    /// 串行模块与管理器生命周期相关的事件类型常量。
    /// </summary>
    public static class SerialEvents
    {
        /// <summary>
        /// Dispatched by a module when <see cref="ISerialModule.Startup"/> completes.
        /// 模块在 <see cref="ISerialModule.Startup"/> 完成后派发。
        /// </summary>
        public const string EventOnModuleStarted = "SerialModule:EventOnObserverStarted";

        /// <summary>
        /// Dispatched by a module when <see cref="ISerialModule.Shutdown"/> completes.
        /// 模块在 <see cref="ISerialModule.Shutdown"/> 完成后派发。
        /// </summary>
        public const string EventOnModuleStopped = "SerialModule:EventOnObserverStopped";

        /// <summary>
        /// Dispatched by <see cref="SerialManager"/> when all modules have started.
        /// <see cref="SerialManager"/> 在所有模块启动完成后派发。
        /// </summary>
        public const string EventOnManagerStarted = "SerialManager:EventOnManagerStarted";

        /// <summary>
        /// Dispatched by <see cref="SerialManager"/> when all modules have stopped.
        /// <see cref="SerialManager"/> 在所有模块停止完成后派发。
        /// </summary>
        public const string EventOnManagerStopped = "SerialManager:EventOnManagerStopped";
    }
}
