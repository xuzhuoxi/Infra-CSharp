using JLGames.Infra.Buffer;

namespace JLGames.Infra.Net
{
    public class MessageWriter : DataBuffer, IMessageWriter
    {
        public MessageWriter(bool littleEndian) : base(littleEndian)
        {
        }

        public void WriteMessage(INetMessage msg)
        {
            WriteData(msg.EncodeToBytes());
        }

        public void WriteMessage<T>(T[] msg) where T : INetMessage
        {
            if (msg == null || msg.Length == 0)
            {
                WriteZero(LenSize);
                return;
            }

            WriteLen(msg.Length);
            for (var index = 0; index < msg.Length; index++)
            {
                WriteData(msg[index].EncodeToBytes());
            }
        }

        public byte[] ReadMessageBytes()
        {
            return m_Buff.ReadBytes();
        }

        public new void Clear()
        {
            m_Buff.Clear();
        }
    }
}