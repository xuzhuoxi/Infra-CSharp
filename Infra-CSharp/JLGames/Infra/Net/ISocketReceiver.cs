using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    public interface ISocketReceiver : ISocketInfo, IEventDispatcher
    {
        /// <summary>
        /// Whether to handle the message receiving state
        /// 是否处理消息接收状态
        /// </summary>
        bool IsReceiving { get; }

        /// <summary>
        /// Start receiving message.
        /// 开始接收数据
        /// </summary>
        /// <returns></returns>
        void StartReceiving();

        /// <summary>
        /// Stop receiving message
        /// 停止接收数据
        /// </summary>
        /// <returns></returns>
        void StopReceiving();

        /// <summary>
        /// set the message handler function
        /// 设置消息处理函数
        /// </summary>
        /// <param name="handler"></param>
        void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler);
    }
}