using System.Collections.Generic;

namespace JLGames.Infra.Event
{
    /// <summary>
    /// Named <see cref="IEventDispatcher"/> instance pool.
    /// 按名称管理的事件调度器实例池。
    /// </summary>
    public sealed class EventDispatcherPool
    {
        internal readonly Dictionary<string, IEventDispatcher> m_Pool = new Dictionary<string, IEventDispatcher>();

        /// <summary>
        /// Get event dispatcher instance.
        /// 取事件调度实例
        /// </summary>
        /// <param name="instanceName">Instance name.<br/>实例名称。</param>
        /// <param name="createIfNotExist">Create a new dispatcher when missing.<br/>不存在时是否创建新实例。</param>
        /// <returns>Dispatcher instance, or null when missing and not created.<br/>调度器实例；未创建且不存在时返回 null。</returns>
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
        /// Remove a named dispatcher from the pool, optionally clearing its listeners first.
        /// 从池中移除指定名称的调度器，可选先清除其全部监听。
        /// </summary>
        /// <param name="instanceName">Instance name.<br/>实例名称。</param>
        /// <param name="removeListener">Clear listeners before removal.<br/>移除前是否清除监听。</param>
        /// <returns>Removed dispatcher, or null if not found.<br/>被移除的调度器；不存在时返回 null。</returns>
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