namespace JLGames.Infra.Event
{
    /// <summary>
    /// Event dispatcher: register listeners and dispatch events by type.
    /// 事件调度器：按类型注册监听并派发事件。
    /// </summary>
    public interface IEventDispatcher : IEventListener
    {
        /// <summary>
        /// Trigger an event of a certain type and pass data
        /// 触发某一类型的事件,并传递数据
        /// </summary>
        /// <param name="evd">Event data.<br/>事件数据。</param>
        void DispatchEvent(EventData evd);

        /// <summary>
        /// Trigger an event of a certain type and pass data
        /// 触发某一类型的事件,并传递数据
        /// </summary>
        /// <param name="type">Event type.<br/>事件类型。</param>
        /// <param name="data">Event payload (may be null).<br/>事件数据（可为 null）。</param>
        void DispatchEvent(string type, object data);
    }
}