using System.Threading;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    public interface ISocketClient : ISocketConn, IEventDispatcher
    {
        /// <summary>
        /// 设置线程关联的上下文件
        /// </summary>
        /// <param name="context"></param>
        void SetContext(SynchronizationContext context);

        /// <summary>
        /// Connect to server
        /// 连接到服务器
        /// </summary>
        /// <param name="params"></param>
        void ConnectServer(SocketParams @params);

        /// <summary>
        /// Disconnect from server
        /// 关闭与服务器的连接
        /// </summary>
        void DisconnectServer();
    }
}