using System.Collections.Generic;

namespace JLGames.Infra.Event
{
    /// <summary>
    /// Event manager.
    /// 事件管理器
    /// </summary>
    public static class EventManager
    {
        private const string m_DefaultName = "Default";
        private static readonly EventDispatcherPool s_DispatcherPool = new EventDispatcherPool();

        public static IEventDispatcher DefaultDispatcher => s_DispatcherPool.GetInstance(m_DefaultName, true);

        public static IEventDispatcher GetInstance(string instanceName)
        {
            return s_DispatcherPool.GetInstance(instanceName, true);
        }

        /// <summary>
        /// Remove instance listeners and instance.
        /// </summary>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        public static IEventDispatcher RemoveInstance(string instanceName)
        {
            return s_DispatcherPool.Clear(instanceName, true);
        }

        /// <summary>
        /// Remove event listeners.
        /// 移除事件监听
        /// </summary>
        /// <param name="instanceName"></param>
        public static void RemoveListeners(string instanceName)
        {
            var instance = s_DispatcherPool.GetInstance(instanceName, false);
            if (instance == null) return;
            instance.RemoveEventListener();
        }

        /// <summary>
        /// Remove all instances event listeners .
        /// 移除事件监听
        /// </summary>
        public static void RemoveListeners()
        {
            foreach (var pair in s_DispatcherPool.m_Pool)
            {
                pair.Value.RemoveEventListener();
            }
        }
    }
}