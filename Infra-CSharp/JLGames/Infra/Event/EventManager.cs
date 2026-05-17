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

        /// <summary>
        /// Default named event dispatcher instance.
        /// 默认名称的事件调度器实例。
        /// </summary>
        public static IEventDispatcher DefaultDispatcher => s_DispatcherPool.GetInstance(m_DefaultName, true);

        /// <summary>
        /// Get or create a named event dispatcher instance.
        /// 获取或创建指定名称的事件调度器实例。
        /// </summary>
        /// <param name="instanceName">Instance name.<br/>实例名称。</param>
        /// <returns>Event dispatcher.<br/>事件调度器。</returns>
        public static IEventDispatcher GetInstance(string instanceName)
        {
            return s_DispatcherPool.GetInstance(instanceName, true);
        }

        /// <summary>
        /// Remove all listeners for the instance, then remove it from the pool.
        /// 移除该实例的全部监听后，从池中删除该实例。
        /// </summary>
        /// <param name="instanceName">Instance name.<br/>实例名称。</param>
        /// <returns>Removed dispatcher, or null if not found.<br/>被移除的调度器；不存在时返回 null。</returns>
        public static IEventDispatcher RemoveInstance(string instanceName)
        {
            return s_DispatcherPool.Clear(instanceName, true);
        }

        /// <summary>
        /// Clear all event listeners on the named instance without removing the instance.
        /// 清除指定实例上的全部事件监听，但不从池中移除该实例。
        /// </summary>
        /// <param name="instanceName">Instance name.<br/>实例名称。</param>
        public static void RemoveListeners(string instanceName)
        {
            var instance = s_DispatcherPool.GetInstance(instanceName, false);
            if (instance == null) return;
            instance.RemoveEventListener();
        }

        /// <summary>
        /// Clear all event listeners on every pooled dispatcher instance.
        /// 清除池中每个事件调度器实例上的全部监听。
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