using System.Collections.Generic;
using System.Threading;

namespace JLGames.Infra.Event
{
    /// <summary>
    /// Event dispatcher
    /// 事件调度器
    /// </summary>
    public class EventDispatcher : IThreadEventDispatcher
    {
        /// <summary>
        /// Event group mapping
        /// 事件组映射
        /// </summary>
        protected readonly Dictionary<string, EventGroup> m_Event2Group = new Dictionary<string, EventGroup>();

        /// <summary>
        /// 注意：
        /// C#默认的 SynchronizationContext 实现只是一个空壳，提供一个基础的同步上下文接口
        /// 不维护一个队列，也不具备线程切换或调度能力。
        /// 这里传入的应该是具备队列调度能力的 SynchronizationContext 派生类
        /// </summary>
        protected SynchronizationContext m_ThreadEventContext;

        // IThreadEventDispatcher

        public bool IsNullContext => m_ThreadEventContext == null;

        public void SetThreadEventContext(SynchronizationContext context)
        {
            m_ThreadEventContext = context;
        }

        public void ClearThreadEventContext()
        {
            m_ThreadEventContext = null;
        }

        // IEventListener

        public void OnceEventListener(string type, EventDelegates.EventHandler handler, int weight = EventConst.DefaultWeight, string tag = null)
        {
            AddEventListener(type, handler, weight, 1, tag);
        }

        public void AddEventListener(string type, EventDelegates.EventHandler handler, string tag = null)
        {
            AddEventListener(type, handler, EventConst.DefaultWeight, 0, tag);
        }

        public void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, string tag = null)
        {
            AddEventListener(type, handler, weight, 0, tag);
        }

        public void AddEventListener(string type, EventDelegates.EventHandler handler, uint listeningTimes, string tag = null)
        {
            AddEventListener(type, handler, EventConst.DefaultWeight, listeningTimes, tag);
        }

        public void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, uint listeningTimes, string tag = null)
        {
            EventGroup eventGroup;
            if (m_Event2Group.ContainsKey(type))
            {
                eventGroup = m_Event2Group[type];
            }
            else
            {
                eventGroup = new EventGroup();
                m_Event2Group[type] = eventGroup;
            }

            eventGroup.AddEventHandler(handler, weight, listeningTimes, tag);
        }

        public void RemoveEventListener(string type, EventDelegates.EventHandler handler, string tag = null)
        {
            if (null == handler || string.IsNullOrEmpty(type) || !m_Event2Group.ContainsKey(type)) return;
            m_Event2Group[type].RemoveEventHandler(handler, tag);
        }

        public void RemoveEventListener(string type, string tag)
        {
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(tag) || !m_Event2Group.ContainsKey(type)) return;
            m_Event2Group[type].RemoveEventHandler(tag);
        }

        public void RemoveEventListener(string type)
        {
            if (string.IsNullOrEmpty(type) || !m_Event2Group.ContainsKey(type)) return;
            m_Event2Group.Remove(type);
        }

        public void RemoveEventListener()
        {
            m_Event2Group.Clear();
        }

        public virtual void Dispose()
        {
            m_Event2Group.Clear();
        }

        // IEventDispatcher

        public virtual void DispatchEvent(string type, object data)
        {
            if (!m_Event2Group.ContainsKey(type))
                return;

            var eventData = new EventData(type, data, this);
            DispatchData(eventData);
        }

        public virtual void DispatchEvent(EventData evd)
        {
            if (!m_Event2Group.ContainsKey(evd.Type))
                return;

            DispatchData(evd);
        }

        //--------------------

        protected virtual void DispatchData(EventData evd)
        {
            var group = m_Event2Group[evd.Type];
            if (null != m_ThreadEventContext)
                m_ThreadEventContext.Send(state => { group.Handle(evd); }, null);
            else
                group.Handle(evd);
        }
    }
}