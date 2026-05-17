using System.Threading;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Socket client: connect/disconnect, send/receive, and dispatch connection events.
    /// Socket 客户端：连接/断开、收发数据，并派发连接相关事件。
    /// </summary>
    public interface ISocketClient : ISocketConn, IEventDispatcher
    {
        /// <summary>
        /// Set synchronization context for marshaling callbacks to a specific thread.
        /// 设置同步上下文，将回调封送到指定线程。
        /// </summary>
        /// <param name="context">Target synchronization context.<br/>目标同步上下文。</param>
        void SetContext(SynchronizationContext context);

        /// <summary>
        /// Connect to server.
        /// 连接到服务器。
        /// </summary>
        /// <param name="params">Connection parameters.<br/>连接参数。</param>
        void ConnectServer(SocketParams @params);

        /// <summary>
        /// Disconnect from server.
        /// 关闭与服务器的连接。
        /// </summary>
        void DisconnectServer();
    }
}