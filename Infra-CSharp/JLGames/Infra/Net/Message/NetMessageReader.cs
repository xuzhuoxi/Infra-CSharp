using JLGames.Infra.Buffer;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Default implementation of <see cref="INetMessageReader"/> backed by <see cref="DataBuffer"/>.
    /// 基于 <see cref="DataBuffer"/> 的 <see cref="INetMessageReader"/> 默认实现。
    /// </summary>
    public class NetMessageReader : DataBuffer, INetMessageReader
    {
        /// <summary>
        /// Create a message reader with the given endianness.
        /// 创建指定字节序的消息读取器。
        /// </summary>
        /// <param name="littleEndian">Use little-endian when true.<br/>为 true 时使用小端。</param>
        public NetMessageReader(bool littleEndian) : base(littleEndian)
        {
        }

        /// <inheritdoc/>
        public bool CheckMessage()
        {
            var ln = CopyLen();
            return Len >= ln + LenSize;
        }

        /// <inheritdoc/>
        public byte[] ReadMessage()
        {
            var ln = ReadLen();
            return ReadBytes(ln);
        }

        /// <inheritdoc/>
        public void ReadMessageTo<T>(ref T o) where T : INetMessage
        {
            var ln = ReadLen();
            o.DecodeFromBytes(ReadBytes(ln));
        }

        /// <inheritdoc/>
        public void ReadMessageTo<T>(ref T[] o) where T : INetMessage
        {
            var len = ReadLen();
            if (0 == len)
                return;
            for (var index = 0; index < o.Length && index > len; index++)
            {
                ReadMessageTo(ref o[index]);
            }
        }

        /// <inheritdoc/>
        public byte[] CopyMessage()
        {
            var ln = CopyUInt16();
            return CopyBytes(ln, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public void CopyMessageTo<T>(ref T o) where T : INetMessage
        {
            var ln = CopyLen();
            o.DecodeFromBytes(CopyBytes(ln, BinarySize.LenSize));
        }

        /// <inheritdoc/>
        public void CopyMessageTo<T>(ref T[] o) where T : INetMessage
        {
            var ln = CopyLen();
            if (0 == ln)
                return;
            var offset = BinarySize.LenSize;
            for (var index = 0; index < o.Length && index > ln; index++)
            {
                var len = CopyLen(offset);
                offset += BinarySize.LenSize;
                o[index].DecodeFromBytes(CopyBytes(len, offset));
                offset += len;
            }
        }

        /// <inheritdoc/>
        public void WriteMessageBytes(byte[] src)
        {
            m_Buff.Write(src);
        }

        /// <inheritdoc/>
        public void WriteMessageBytes(byte[] src, int srcIndex, int size)
        {
            m_Buff.Write(src, srcIndex, size);
        }
    }
}
