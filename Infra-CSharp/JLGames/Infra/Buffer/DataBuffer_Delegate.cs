using System.Runtime.CompilerServices;

namespace JLGames.Infra.Buffer
{
    public partial class DataBuffer
    {
        // Delegate IByteBuffer

        /// <inheritdoc/>
        public int Cap => m_Buff.Cap;

        /// <inheritdoc/>
        public void Clear()
        {
            m_Buff.Clear();
        }

        // Delegate IByteBufferReader

        /// <inheritdoc/>
        public int Len => m_Buff.Len;

        /// <inheritdoc/>
        public int ReadPosition => m_Buff.ReadPosition;

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public void SetReadPosition(int pos)
        {
            m_Buff.SetReadPosition(pos);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public byte ReadByte()
        {
            return m_Buff.ReadByte();
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public byte[] ReadBytes()
        {
            return m_Buff.ReadBytes();
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public byte[] ReadBytes(int size)
        {
            return m_Buff.ReadBytes(size);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public int ReadBytesTo(ref byte[] dst)
        {
            return m_Buff.ReadBytesTo(ref dst);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public int ReadBytesTo(ref byte[] dst, int size)
        {
            return m_Buff.ReadBytesTo(ref dst, size);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public byte CopyByte()
        {
            return m_Buff.CopyByte();
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public byte[] CopyBytes(int offset = 0)
        {
            return m_Buff.CopyBytes(offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public byte[] CopyBytes(int size, int offset)
        {
            return m_Buff.CopyBytes(size, offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public int CopyBytesTo(ref byte[] dst, int offset = 0)
        {
            return m_Buff.CopyBytesTo(ref dst, offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public int CopyBytesTo(ref byte[] dst, int size, int offset)
        {
            return m_Buff.CopyBytesTo(ref dst, size, offset);
        }

        // Delegate IByteBufferWriter

        /// <inheritdoc/>
        public int WritePosition => m_Buff.WritePosition;

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public void SetWritePosition(int pos)
        {
            m_Buff.SetWritePosition(pos);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public void WriteZero(int size)
        {
            m_Buff.WriteZero(size);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public void Write(byte[] bytes, int startIndex, int size)
        {
            m_Buff.Write(bytes, startIndex, size);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public void Write(byte[] bytes, int startIndex)
        {
            m_Buff.Write(bytes, startIndex);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public void Write(byte[] bytes)
        {
            m_Buff.Write(bytes);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public void Write(byte b)
        {
            m_Buff.Write(b);
        }
    }
}
