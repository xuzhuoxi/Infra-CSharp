using System.Collections.Generic;

namespace JLGames.Infra.Event
{
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
        /// <param name="data"></param>
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
        /// Add event hande function, use custom order, custom reamin
        /// 添加事件处理函数, 使用自定义执行顺序系数, 自定义响应次数
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="weight"></param>
        /// <param name="handleTimes"></param>
        /// <param name="tag"></param>
        public void AddEventHandler(EventDelegates.EventHandler handler, int weight, uint handleTimes, string tag)
        {
            if (null == handler) return;
            m_Handlers.Add(new EventItem {Tag = tag, Handler = handler, Weight = weight, Remain = handleTimes});
        }

        /// <summary>
        /// Delete listener function
        /// 删除监听函数
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="tag"></param>
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
        /// <param name="tag"></param>
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