// using System.Threading.Tasks;
//
// namespace JLGames.Infra.Event
// {
//     public interface IAsyncEventDispatcher : IAsyncEventListener
//     {
//         /// <summary>
//         /// Trigger an event of a certain type and pass data
//         /// 触发某一类型的事件,并传递数据
//         /// </summary>
//         /// <param name="evd">Evd.</param>
//         Task DispatchEvent(EventData evd);
//
//         /// <summary>
//         /// Trigger an event of a certain type and pass data
//         /// 触发某一类型的事件,并传递数据
//         /// </summary>
//         /// <param name="type">事件类型</param>
//         /// <param name="data">事件的数据(可为null)</param>
//         Task DispatchEvent(string type, object data);
//     }
// }