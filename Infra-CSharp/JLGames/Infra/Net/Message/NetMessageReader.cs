using JLGames.Infra.Buffer;

namespace JLGames.Infra.Net
{
    public class NetMessageReader : DataBuffer, INetMessageReader
    {
        public NetMessageReader(bool littleEndian) : base(littleEndian)
        {
        }

        public bool CheckMessage()
        {
            var ln = CopyLen();
            return Len >= ln + LenSize;
        }

        public byte[] ReadMessage()
        {
            var ln = ReadLen();
            return ReadBytes(ln);
        }

        public void ReadMessageTo<T>(ref T o) where T : INetMessage
        {
            var ln = ReadLen();
            o.DecodeFromBytes(ReadBytes(ln));
        }

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

        public byte[] CopyMessage()
        {
            var ln = CopyUInt16();
            return CopyBytes(ln, BinarySize.LenSize);
        }

        public void CopyMessageTo<T>(ref T o) where T : INetMessage
        {
            var ln = CopyLen();
            o.DecodeFromBytes(CopyBytes(ln, BinarySize.LenSize));
        }

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

        public void WriteMessageBytes(byte[] src)
        {
            m_Buff.Write(src);
        }

        public void WriteMessageBytes(byte[] src, int srcIndex, int size)
        {
            m_Buff.Write(src, srcIndex, size);
        }
    }
}