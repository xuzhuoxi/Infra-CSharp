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
        /// Open client
        /// 打开连接
        /// </summary>
        /// <param name="params"></param>
        void OpenClient(SocketParams @params);

        /// <summary>
        /// Close client
        /// 关闭客户端
        /// </summary>
        void CloseClient();
    }
}