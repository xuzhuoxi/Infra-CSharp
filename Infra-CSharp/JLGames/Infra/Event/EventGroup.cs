using System.Collections.Generic;

namespace JLGames.Infra.Event
{
    /// <summary>
    /// Per-event-type handler list: registration, ordered dispatch, and limited invoke counts.
    /// 单一事件类型的处理器列表：注册、按权重派发及限定调用次数。
    /// </summary>
    public sealed class EventGroup
    {
        private struct EventItem
        {
            public string Tag;
            public EventDelegates.EventHandler Handler;
            public int Weight;
            public uint Remain;
        }

        /// <summary>
        /// Delegate function based event item list
        /// 基于委托函数的事件列表
        /// </summary>
        private readonly List<EventItem> m_Handlers = new List<EventItem>();

        private readonly List<EventItem> m_Temp = new List<EventItem>();

        /// <summary>
        /// Trigger the listener event
        /// 触发监听事件
        /// </summary>
        /// <param name="data">Event data to pass to handlers.<br/>传递给处理器的事件数据。</param>
        public void Handle(EventData data)
        {
            if (m_Handlers.Count == 0) return;
            CloneToTemp();
            DecreaseRemains();
            m_Temp.Sort((item1, item2) => item2.Weight.CompareTo(item1.Weight));
            for (var index = 0; index < m_Temp.Count; index++)
            {
                m_Temp[index].Handler.Invoke(data);
            }
        }

        /// <summary>
        /// Add event handler with custom weight and invoke limit.
        /// 添加事件处理函数，使用自定义权重与响应次数。
        /// </summary>
        /// <param name="handler">Handler callback.<br/>处理回调。</param>
        /// <param name="weight">Dispatch order weight (higher runs first).<br/>派发权重（越大越先执行）。</param>
        /// <param name="handleTimes">Max invocations; 0 means unlimited.<br/>最大调用次数；0 表示不限次数。</param>
        /// <param name="tag">Optional tag for later removal.<br/>可选标签，便于后续按标签移除。</param>
        public void AddEventHandler(EventDelegates.EventHandler handler, int weight, uint handleTimes, string tag)
        {
            if (null == handler) return;
            m_Handlers.Add(new EventItem {Tag = tag, Handler = handler, Weight = weight, Remain = handleTimes});
        }

        /// <summary>
        /// Delete listener function
        /// 删除监听函数
        /// </summary>
        /// <param name="handler">Handler to remove.<br/>要移除的处理器。</param>
        /// <param name="tag">Optional tag filter.<br/>可选标签过滤。</param>
        public void RemoveEventHandler(EventDelegates.EventHandler handler, string tag)
        {
            if (null == handler || m_Handlers.Count == 0) return;
            var lastIndex = string.IsNullOrEmpty(tag)
                ? m_Handlers.FindLastIndex(item => item.Handler == handler)
                : m_Handlers.FindLastIndex(item => tag == item.Tag && item.Handler == handler);
            if (-1 == lastIndex) return;
            m_Handlers.RemoveAt(lastIndex);
        }

        /// <summary>
        /// Delete listener function
        /// 删除监听函数
        /// </summary>
        /// <param name="tag">Tag of handlers to remove.<br/>要移除的处理器标签。</param>
        public void RemoveEventHandler(string tag)
        {
            if (string.IsNullOrEmpty(tag)) return;
            for (var index = m_Handlers.Count - 1; index >= 0; index--)
            {
                if (m_Handlers[index].Tag == tag) m_Handlers.RemoveAt(index);
            }
        }

        /// <summary>
        /// Decrease remain
        /// 减少次数
        /// </summary>
        private void DecreaseRemains()
        {
            for (var index = m_Handlers.Count - 1; index >= 0; index--)
            {
                var remain = m_Handlers[index].Remain;
                if (0 == remain) continue; // forever 永久
                if (1 == remain) // last 最后一次
                {
                    m_Handlers.RemoveAt(index);
                    continue;
                }

                // decrease remain 减少次数
                var item = m_Handlers[index];
                item.Remain = remain - 1;
                m_Handlers[index] = item;
            }
        }

        private void CloneToTemp()
        {
            m_Temp.Clear();
            if (m_Handlers.Count == 0) return;
            m_Temp.AddRange(m_Handlers);
        }
    }
}