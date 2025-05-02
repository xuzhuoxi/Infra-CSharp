using System.Runtime.CompilerServices;

namespace JLGames.Infra.Buffer
{
    public partial class DataBuffer
    {
        // Delegate IByteBuffer

        public int Cap => m_Buff.Cap;

        public void Clear()
        {
            m_Buff.Clear();
        }

        // Delegate IByteBufferReader

        public int Len => m_Buff.Len;

        public int ReadPosition => m_Buff.ReadPosition;

        [MethodImpl((MethodImplOptions) 256)]
        public void SetReadPosition(int pos)
        {
            m_Buff.SetReadPosition(pos);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public byte ReadByte()
        {
            return m_Buff.ReadByte();
        }

        [MethodImpl((MethodImplOptions) 256)]
        public byte[] ReadBytes()
        {
            return m_Buff.ReadBytes();
        }

        [MethodImpl((MethodImplOptions) 256)]
        public byte[] ReadBytes(int size)
        {
            return m_Buff.ReadBytes(size);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public int ReadBytesTo(ref byte[] dst)
        {
            return m_Buff.ReadBytesTo(ref dst);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public int ReadBytesTo(ref byte[] dst, int size)
        {
            return m_Buff.ReadBytesTo(ref dst, size);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public byte CopyByte()
        {
            return m_Buff.CopyByte();
        }

        [MethodImpl((MethodImplOptions) 256)]
        public byte[] CopyBytes(int offset = 0)
        {
            return m_Buff.CopyBytes(offset);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public byte[] CopyBytes(int size, int offset)
        {
            return m_Buff.CopyBytes(size, offset);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public int CopyBytesTo(ref byte[] dst, int offset = 0)
        {
            return m_Buff.CopyBytesTo(ref dst, offset);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public int CopyBytesTo(ref byte[] dst, int size, int offset)
        {
            return m_Buff.CopyBytesTo(ref dst, size, offset);
        }

        // Delegate IByteBufferWriter

        public int WritePosition => m_Buff.WritePosition;

        [MethodImpl((MethodImplOptions) 256)]
        public void SetWritePosition(int pos)
        {
            m_Buff.SetWritePosition(pos);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public void WriteZero(int size)
        {
            m_Buff.WriteZero(size);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public void Write(byte[] bytes, int startIndex, int size)
        {
            m_Buff.Write(bytes, startIndex, size);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public void Write(byte[] bytes, int startIndex)
        {
            m_Buff.Write(bytes, startIndex);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public void Write(byte[] bytes)
        {
            m_Buff.Write(bytes);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public void Write(byte b)
        {
            m_Buff.Write(b);
        }
    }
}