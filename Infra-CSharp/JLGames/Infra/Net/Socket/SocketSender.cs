using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    public class SocketSender : ISocketSender
    {
        private readonly Socket m_Socket;
        private readonly INetMessageWriter m_NetMessageWriter;

        public string Name { get; private set; }
        public bool Connected => m_Socket.Connected;

        public SocketSender(string name, Socket socket, INetMessageWriter netMessageWriter)
        {
            Name = name;
            m_Socket = socket;
            m_NetMessageWriter = netMessageWriter;
        }

        public void SendBytes(byte[] bytes)
        {
            m_Socket.Send(bytes);
        }

        public void SendMessage(byte[] message)
        {
            m_NetMessageWriter.WriteData(message);
            var bs = m_NetMessageWriter.ReadMessageBytes();
            m_Socket.Send(bs);
        }

        public void SendMessage(string[] messages)
        {
            m_NetMessageWriter.WriteData(messages);
            var bs = m_NetMessageWriter.ReadMessageBytes();
            m_Socket.Send(bs);
        }

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