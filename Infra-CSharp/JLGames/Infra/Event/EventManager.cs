using System.Collections.Generic;

namespace JLGames.Infra.Event
{
    /// <summary>
    /// Event manager.
    /// 事件管理器
    /// </summary>
    public static class EventManager
    {
        /// <summary>
        /// Default
        /// 默认
        /// </summary>
        public const string DefaultName = "Default";

        private static readonly Dictionary<string, IEventDispatcher> m_MgrPool = new Dictionary<string, IEventDispatcher>();

        public static IEventDispatcher Default => GetInstance(DefaultName);

        /// <summary>
        /// Get event dispatcher instance.
        /// 取事件调度实例
        /// </summary>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        public static IEventDispatcher GetInstance(string instanceName)
        {
            if (m_MgrPool.ContainsKey(instanceName))
            {
                return m_MgrPool[instanceName];
            }
            else
            {
                IEventDispatcher rs = new EventDispatcher();
                m_MgrPool[instanceName] = rs;
                return rs;
            }
        }

        /// <summary>
        /// Remove event dispatcher instance.
        /// 移除事件调度实例
        /// </summary>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        public static IEventDispatcher RemoveInstance(string instanceName)
        {
            if (!m_MgrPool.ContainsKey(instanceName))
            {
                return null;
            }

            var rs = m_MgrPool[instanceName];
            m_MgrPool.Remove(instanceName);
            return rs;
        }

        /// <summary>
        /// Remove event listeners.
        /// 移除事件监听
        /// </summary>
        /// <param name="instanceName"></param>
        public static void RemoveListeners(string instanceName)
        {
            if (!m_MgrPool.ContainsKey(instanceName)) return;
            m_MgrPool[instanceName].RemoveEventListener();
        }

        /// <summary>
        /// Remove event listeners.
        /// 移除事件监听
        /// </summary>
        public static void RemoveListeners()
        {
            if (m_MgrPool.Count == 0) return;
            foreach (var pair in m_MgrPool)
            {
                pair.Value.RemoveEventListener();
            }
        }
    }
}