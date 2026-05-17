namespace JLGames.Infra.Event
{
    /// <summary>
    /// Event data.
    /// 事件数据
    /// </summary>
    public class EventData
    {
        private readonly string m_Type;
        private readonly object m_Data;

        private readonly IEventDispatcher m_CurrentDispatcher;

        /// <summary>
        /// Create event data with type and payload.
        /// 使用事件类型与载荷创建事件数据。
        /// </summary>
        /// <param name="type">Event type.<br/>事件类型。</param>
        /// <param name="data">Event payload (may be null).<br/>事件载荷（可为 null）。</param>
        public EventData(string type, object data)
        {
            m_Type = type;
            m_Data = data;
        }

        /// <summary>
        /// Create event data with type, payload, and originating dispatcher.
        /// 使用事件类型、载荷及派发来源调度器创建事件数据。
        /// </summary>
        /// <param name="type">Event type.<br/>事件类型。</param>
        /// <param name="data">Event payload (may be null).<br/>事件载荷（可为 null）。</param>
        /// <param name="currentDispatcher">Dispatcher that dispatched this event.<br/>派发该事件的调度器。</param>
        public EventData(string type, object data, IEventDispatcher currentDispatcher)
        {
            m_Type = type;
            m_Data = data;
            m_CurrentDispatcher = currentDispatcher;
        }

        /// <summary>
        /// Event type
        /// 事件类型
        /// </summary>
        public string Type => m_Type;

        /// <summary>
        /// Event data
        /// 事件传递的数据
        /// </summary>
        public object Data => m_Data;

        /// <summary>
        /// Dispatcher that dispatched this event; null if not set at construction.
        /// 派发该事件的调度器；构造时未传入则为 null。
        /// </summary>
        public IEventDispatcher CurrentDispatcher => m_CurrentDispatcher;
    }
}