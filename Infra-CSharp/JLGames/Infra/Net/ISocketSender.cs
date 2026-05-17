namespace JLGames.Infra.Net
{
    /// <summary>
    /// Socket message sender.
    /// Socket 消息发送器。
    /// </summary>
    public interface ISocketSender: ISocketInfo
    {
        /// <summary>
        /// Send raw bytes without framing/packing.
        /// 发送原始字节，不进行封包。
        /// </summary>
        /// <param name="bytes">Payload bytes.<br/>待发送字节数据。</param>
        void SendBytes(byte[] bytes);

        /// <summary>
        /// Send a framed message (length-prefixed).
        /// 发送已封包的消息（含长度前缀）。
        /// </summary>
        /// <param name="message">Message payload.<br/>消息内容。</param>
        void SendMessage(byte[] message);

        /// <summary>
        /// Send one or more string messages (framed).
        /// 发送一个或多个字符串消息（封包）。
        /// </summary>
        /// <param name="messages">String messages.<br/>字符串消息数组。</param>
        void SendMessage(string[] messages);

        /// <summary>
        /// Send one or more string messages (framed).
        /// 发送一个或多个字符串消息（封包）。
        /// </summary>
        /// <param name="message">First message.<br/>第一条消息。</param>
        /// <param name="messages">Additional messages.<br/>其余消息。</param>
        void SendMessage(string message, params string[] messages);
    }
}