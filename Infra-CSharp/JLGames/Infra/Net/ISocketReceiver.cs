using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Socket message receiver with event dispatch.
    /// Socket 消息接收器，支持事件派发。
    /// </summary>
    public interface ISocketReceiver : ISocketInfo, IEventDispatcher
    {
        /// <summary>
        /// Whether message receiving is active.
        /// 是否正在接收消息。
        /// </summary>
        bool IsReceiving { get; }

        /// <summary>
        /// Start receiving messages.
        /// 开始接收数据。
        /// </summary>
        void StartReceiving();

        /// <summary>
        /// Stop receiving messages.
        /// 停止接收数据。
        /// </summary>
        void StopReceiving();

        /// <summary>
        /// Set the binary message handler callback.
        /// 设置二进制消息处理回调。
        /// </summary>
        /// <param name="handler">Message handler (may be null).<br/>消息处理函数（可为 null）。</param>
        void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler);
    }
}