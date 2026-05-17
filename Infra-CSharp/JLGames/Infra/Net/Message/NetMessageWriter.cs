using JLGames.Infra.Buffer;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Default implementation of <see cref="INetMessageWriter"/> backed by <see cref="DataBuffer"/>.
    /// 基于 <see cref="DataBuffer"/> 的 <see cref="INetMessageWriter"/> 默认实现。
    /// </summary>
    public class NetMessageWriter : DataBuffer, INetMessageWriter
    {
        /// <summary>
        /// Create a message writer with the given endianness.
        /// 创建指定字节序的消息写入器。
        /// </summary>
        /// <param name="littleEndian">Use little-endian when true.<br/>为 true 时使用小端。</param>
        public NetMessageWriter(bool littleEndian) : base(littleEndian)
        {
        }

        /// <inheritdoc/>
        public void WriteMessage(INetMessage msg)
        {
            WriteData(msg.EncodeToBytes());
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public byte[] ReadMessageBytes()
        {
            return m_Buff.ReadBytes();
        }

        /// <inheritdoc/>
        public new void Clear()
        {
            m_Buff.Clear();
        }
    }
}
