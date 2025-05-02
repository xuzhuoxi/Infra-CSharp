namespace JLGames.Infra.Event
{
    public interface IEventListener
    {
        /// <summary>
        /// Add single event listener
        /// 添加单次事件监听
        /// </summary>
        /// <param name="type">Event Type<br/>事件类型</param>
        /// <param name="handler">Listener Function<br/>监听函数</param>
        /// <param name="weight">Response Weight<br/>响应权重</param>
        /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
        void OnceEventListener(string type, EventDelegates.EventHandler handler, int weight = EventConst.DefaultWeight, string tag = null);

        /// <summary>
        /// Add event listener
        /// 添加事件监听
        /// </summary>
        /// <param name="type">Event Type<br/>事件类型</param>
        /// <param name="handler">Listener Function<br/>监听函数</param>
        /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
        void AddEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

        /// <summary>
        /// Add event listener
        /// 添加事件监听
        /// </summary>
        /// <param name="type">Event Type<br/>事件类型</param>
        /// <param name="handler">Listener Function<br/>监听函数</param>
        /// <param name="weight">Response Weight<br/>响应权重</param>
        /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
        void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, string tag = null);

        /// <summary>
        /// Add event listener
        /// 添加事件监听
        /// </summary>
        /// <param name="type">Event Type<br/>事件类型</param>
        /// <param name="handler">Listener Function<br/>监听函数</param>
        /// <param name="listeningTimes">Times of responses<br/>响应次数</param>
        /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
        void AddEventListener(string type, EventDelegates.EventHandler handler, uint listeningTimes, string tag = null);

        /// <summary>
        /// Add event listener
        /// 添加事件监听
        /// </summary>
        /// <param name="type">Event Type<br/>事件类型</param>
        /// <param name="handler">Listener Function<br/>监听函数</param>
        /// <param name="weight">Response Weight<br/>响应权重</param>
        /// <param name="listeningTimes">Times of responses<br/>响应次数</param>
        /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
        void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, uint listeningTimes, string tag = null);

        /// <summary>
        /// Delete event listener
        /// 删除事件监听
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="handler">监听函数</param>
        /// <param name="tag"></param>
        void RemoveEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

        /// <summary>
        /// Delete event listener
        /// 删除事件监听
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="tag"></param>
        void RemoveEventListener(string type, string tag);

        /// <summary>
        /// Delete a type of event listeners
        /// 删除一类事件监听
        /// </summary>
        /// <param name="type">事件类型</param>
        void RemoveEventListener(string type);

        /// <summary>
        /// Clear all event listeners
        /// 清除全部事件监听
        /// </summary>
        void RemoveEventListener();

        /// <summary>
        /// Dispose
        /// 释放
        /// </summary>
        void Dispose();
    }
}