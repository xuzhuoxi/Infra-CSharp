using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    public class SockSender : ISockSender
    {
        private readonly Socket m_Socket;
        private readonly IMessageWriter m_Packer;

        public SockSender(Socket socket, IMessageWriter packer)
        {
            m_Socket = socket;
            m_Packer = packer;
        }

        public void SendBytes(byte[] bytes)
        {
            m_Socket.Send(bytes);
        }

        public void SendMessage(byte[] message)
        {
            m_Packer.WriteData(message);
            var bs = m_Packer.ReadMessageBytes();
            m_Socket.Send(bs);
        }

        public void SendMessage(string[] messages)
        {
            m_Packer.WriteData(messages);
            var bs = m_Packer.ReadMessageBytes();
            m_Socket.Send(bs);
        }

        public void SendMessage(string message, params string[] messages)
        {
            m_Packer.WriteData(message);
            foreach (var s in messages)
            {
                m_Packer.WriteData(s);
            }

            var bs = m_Packer.ReadMessageBytes();
            m_Socket.Send(bs);
        }
    }
}