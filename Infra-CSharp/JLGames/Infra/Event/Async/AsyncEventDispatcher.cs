// using System.Collections.Generic;
// using System.Threading.Tasks;
//
// namespace JLGames.Infra.Event
// {
//     /// <summary>
//     /// Event dispatcher
//     /// 事件调度器
//     /// </summary>
//     public class AsyncEventDispatcher : IAsyncEventDispatcher
//     {
//         /// <summary>
//         /// Event group mapping
//         /// 事件组映射
//         /// </summary>
//         protected readonly Dictionary<string, AsyncEventGroup> m_Event2Group = new Dictionary<string, AsyncEventGroup>();
//
//         // IEventListener
//
//         public void OnceEventListener(string type, EventDelegates.AsyncEventHandler handler, int weight = EventConst.DefaultWeight, string tag = null)
//         {
//             AddEventListener(type, handler, weight, 1, tag);
//         }
//
//         public void AddEventListener(string type, EventDelegates.AsyncEventHandler handler, string tag = null)
//         {
//             AddEventListener(type, handler, EventConst.DefaultWeight, 0, tag);
//         }
//
//         public void AddEventListener(string type, EventDelegates.AsyncEventHandler handler, int weight, string tag = null)
//         {
//             AddEventListener(type, handler, weight, 0, tag);
//         }
//
//         public void AddEventListener(string type, EventDelegates.AsyncEventHandler handler, uint listeningTimes, string tag = null)
//         {
//             AddEventListener(type, handler, EventConst.DefaultWeight, listeningTimes, tag);
//         }
//
//         public void AddEventListener(string type, EventDelegates.AsyncEventHandler handler, int weight, uint listeningTimes, string tag = null)
//         {
//             AsyncEventGroup eventGroup;
//             if (m_Event2Group.ContainsKey(type))
//             {
//                 eventGroup = m_Event2Group[type];
//             }
//             else
//             {
//                 eventGroup = new AsyncEventGroup();
//                 m_Event2Group[type] = eventGroup;
//             }
//
//             eventGroup.AddEventHandler(handler, weight, listeningTimes, tag);
//         }
//
//         public void RemoveEventListener(string type, EventDelegates.AsyncEventHandler handler, string tag = null)
//         {
//             if (null == handler || string.IsNullOrEmpty(type) || !m_Event2Group.ContainsKey(type)) return;
//             m_Event2Group[type].RemoveEventHandler(handler, tag);
//         }
//
//         public void RemoveEventListener(string type, string tag)
//         {
//             if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(tag) || !m_Event2Group.ContainsKey(type)) return;
//             m_Event2Group[type].RemoveEventHandler(tag);
//         }
//
//         public void RemoveEventListener(string type)
//         {
//             if (string.IsNullOrEmpty(type) || !m_Event2Group.ContainsKey(type)) return;
//             m_Event2Group.Remove(type);
//         }
//
//         public void RemoveEventListener()
//         {
//             m_Event2Group.Clear();
//         }
//
//         public virtual void Dispose()
//         {
//             m_Event2Group.Clear();
//         }
//
//         // IEventDispatcher
//
//         public virtual Task DispatchEvent(string type, object data)
//         {
//             if (!m_Event2Group.ContainsKey(type))
//                 return Task.CompletedTask;
//
//             var eventData = new EventData(type, data, this);
//             DispatchData(eventData);
//             return Task.CompletedTask;
//         }
//
//         public virtual Task DispatchEvent(EventData evd)
//         {
//             if (!m_Event2Group.ContainsKey(evd.Type))
//                 return;
//
//             DispatchData(evd);
//         }
//
//         //--------------------
//
//         protected virtual void DispatchData(EventData evd)
//         {
//             var group = m_Event2Group[evd.Type];
//             group.Handle(evd);
//         }
//     }
// }