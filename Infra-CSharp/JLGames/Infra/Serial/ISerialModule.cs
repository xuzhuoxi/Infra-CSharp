using JLGames.Infra.Event;

namespace JLGames.Infra.Serial
{
    /// <summary>
    /// Serial lifecycle module contract; supports startup/shutdown and dispatches completion events via <see cref="IEventDispatcher"/>.
    /// 串行生命周期模块契约；支持启动/停止，并通过 <see cref="IEventDispatcher"/> 派发完成事件。
    /// </summary>
    /// <remarks>
    /// After <see cref="Startup"/> or <see cref="Shutdown"/> completes, dispatch <see cref="SerialEvents.EventOnModuleStarted"/>
    /// or <see cref="SerialEvents.EventOnModuleStopped"/> respectively so <see cref="SerialManager"/> can proceed to the next module.
    /// <see cref="Startup"/> 或 <see cref="Shutdown"/> 完成后，应分别派发 <see cref="SerialEvents.EventOnModuleStarted"/>
    /// 或 <see cref="SerialEvents.EventOnModuleStopped"/>，以便 <see cref="SerialManager"/> 继续处理下一个模块。
    /// </remarks>
    public interface ISerialModule : IEventDispatcher
    {
        /// <summary>
        /// Start the module asynchronously; dispatch <see cref="SerialEvents.EventOnModuleStarted"/> when finished.
        /// 启动模块（可异步）；完成后派发 <see cref="SerialEvents.EventOnModuleStarted"/>。
        /// </summary>
        void Startup();

        /// <summary>
        /// Stop the module asynchronously; dispatch <see cref="SerialEvents.EventOnModuleStopped"/> when finished.
        /// 停止模块（可异步）；完成后派发 <see cref="SerialEvents.EventOnModuleStopped"/>。
        /// </summary>
        void Shutdown();
    }
}
