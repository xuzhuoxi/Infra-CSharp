namespace JLGames.Infra.Event
{
    /// <summary>
    /// Event-related delegate types.
    /// 事件相关委托类型。
    /// </summary>
    public static class EventDelegates
    {
        /// <summary>
        /// Event listener callback.
        /// 事件监听回调。
        /// </summary>
        /// <param name="evd">Event data passed to the listener.<br/>传递给监听器的事件数据。</param>
        public delegate void EventHandler(EventData evd);
    }
}