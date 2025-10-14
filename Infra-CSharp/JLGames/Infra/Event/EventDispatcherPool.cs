using System.Collections.Generic;

namespace JLGames.Infra.Event
{
    public sealed class EventDispatcherPool
    {
        internal readonly Dictionary<string, IEventDispatcher> m_Pool = new Dictionary<string, IEventDispatcher>();

        /// <summary>
        /// Get event dispatcher instance.
        /// 取事件调度实例
        /// </summary>
        /// <param name="instanceName"></param>
        /// <param name="createIfNotExist"></param>
        /// <returns></returns>
        public IEventDispatcher GetInstance(string instanceName, bool createIfNotExist)
        {
            if (m_Pool.TryGetValue(instanceName, out var instance))
            {
                return instance;
            }

            if (createIfNotExist)
            {
                IEventDispatcher rs = new EventDispatcher(instanceName);
                m_Pool[instanceName] = rs;
                return rs;
            }

            return null;
        }

        /// <summary>
        /// Remove event listeners.
        /// 移除事件调度实例
        /// </summary>
        /// <param name="instanceName"></param>
        /// <param name="removeListener"></param>
        public IEventDispatcher Clear(string instanceName, bool removeListener = true)
        {
            if (m_Pool.TryGetValue(instanceName, out var value))
            {
                if (removeListener)
                    value.RemoveEventListener();

                m_Pool.Remove(instanceName);
                return value;
            }

            return null;
        }

        /// <summary>
        /// Remove event listeners.
        /// 移除事件监听并清理全部事件调度实例
        /// </summary>
        public void ClearAll()
        {
            if (m_Pool.Count == 0) return;
            foreach (var pair in m_Pool)
            {
                pair.Value.RemoveEventListener();
            }

            m_Pool.Clear();
        }
    }
}