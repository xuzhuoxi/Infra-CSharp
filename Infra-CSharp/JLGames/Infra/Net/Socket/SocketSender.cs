using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Default implementation of <see cref="ISocketSender"/>.
    /// <see cref="ISocketSender"/> 的默认实现。
    /// </summary>
    public class SocketSender : ISocketSender
    {
        private readonly Socket m_Socket;
        private readonly INetMessageWriter m_NetMessageWriter;

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public bool Connected => m_Socket.Connected;

        /// <summary>
        /// Create a sender for the given socket and message writer.
        /// 为指定 Socket 与消息写入器创建发送器。
        /// </summary>
        /// <param name="name">Sender name.<br/>发送器名称。</param>
        /// <param name="socket">Underlying socket.<br/>底层 Socket。</param>
        /// <param name="netMessageWriter">Message packer.<br/>消息封包器。</param>
        public SocketSender(string name, Socket socket, INetMessageWriter netMessageWriter)
        {
            Name = name;
            m_Socket = socket;
            m_NetMessageWriter = netMessageWriter;
        }

        /// <inheritdoc/>
        public void SendBytes(byte[] bytes)
        {
            m_Socket.Send(bytes);
        }

        /// <inheritdoc/>
        public void SendMessage(byte[] message)
        {
            m_NetMessageWriter.WriteData(message);
            var bs = m_NetMessageWriter.ReadMessageBytes();
            m_Socket.Send(bs);
        }

        /// <inheritdoc/>
        public void SendMessage(string[] messages)
        {
            m_NetMessageWriter.WriteData(messages);
            var bs = m_NetMessageWriter.ReadMessageBytes();
            m_Socket.Send(bs);
        }

        /// <inheritdoc/>
        public void SendMessage(string message, params string[] messages)
        {
            m_NetMessageWriter.WriteData(message);
            foreach (var s in messages)
            {
                m_NetMessageWriter.WriteData(s);
            }

            var bs = m_NetMessageWriter.ReadMessageBytes();
            m_Socket.Send(bs);
        }
    }
}
