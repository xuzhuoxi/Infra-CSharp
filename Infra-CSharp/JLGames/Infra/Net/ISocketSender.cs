namespace JLGames.Infra.Net
{
    public interface ISocketSender: ISocketInfo
    {
        /// <summary>
        /// Send a message. no packed
        /// 发送消息,不作封包处理
        /// </summary>
        /// <param name="bytes"></param>
        void SendBytes(byte[] bytes);

        /// <summary>
        /// Send a message. packed
        /// 发送消息,封包
        /// </summary>
        /// <param name="message"></param>
        void SendMessage(byte[] message);

        /// <summary>
        /// Send one or more messages.
        /// 发送一个或多个消息
        /// </summary>
        /// <param name="messages"></param>
        void SendMessage(string[] messages);

        /// <summary>
        /// Send one or more messages.
        /// 发送一个或多个消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        void SendMessage(string message, params string[] messages);
    }
}