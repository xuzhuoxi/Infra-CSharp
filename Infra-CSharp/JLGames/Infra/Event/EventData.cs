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

        public EventData(string type, object data)
        {
            m_Type = type;
            m_Data = data;
        }

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

        public IEventDispatcher CurrentDispatcher => m_CurrentDispatcher;
    }
}