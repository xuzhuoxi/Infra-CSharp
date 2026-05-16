using System;

namespace JLGames.Infra.Buffer
{
    /// <summary>
    /// Growable byte buffer with separate read/write cursors.
    /// 可自动扩容的字节缓冲区，读写下标独立
    /// </summary>
    public class ByteBuffer : IByteBuffer
    {
        private const int DefaultBufferSize = 256;
        private const int DefaultBufferAdd = 512;
        private static readonly int[] m_BufferSize = { 16, 32, 64, 128, 256, 512, 1024, 2048 };

        private byte[] m_Buff; //buffer
        private int m_RPos; //read index
        private int m_WPos; //write index

        /// <inheritdoc/>
        public int ReadPosition => m_RPos;

        /// <inheritdoc/>
        public int WritePosition => m_WPos;

        /// <inheritdoc/>
        public int Cap => m_Buff.Length;

        /// <inheritdoc/>
        public int Len => m_WPos - m_RPos;

        /// <summary>
        /// Underlying byte array backing store.
        /// 底层字节数组
        /// </summary>
        public byte[] BuffData => m_Buff;

        /// <summary>
        /// Create a buffer with default capacity (256 bytes).
        /// 使用默认容量（256 字节）创建缓冲区
        /// </summary>
        public ByteBuffer()
        {
            m_Buff = new byte[DefaultBufferSize];
            m_RPos = m_WPos = 0;
        }

        /// <summary>
        /// Create a buffer with the specified initial capacity.
        /// 使用指定初始容量创建缓冲区
        /// </summary>
        /// <param name="buffSize">Initial capacity; 初始容量</param>
        public ByteBuffer(int buffSize)
        {
            m_Buff = new byte[buffSize];
            m_RPos = m_WPos = 0;
        }

        /// <summary>
        /// Wrap an existing byte array as the backing store.
        /// 包装已有字节数组作为底层存储
        /// </summary>
        /// <param name="buffer">Backing byte array; 底层字节数组</param>
        public ByteBuffer(byte[] buffer)
        {
            m_Buff = buffer;
            m_RPos = m_WPos = 0;
        }

        /// <inheritdoc/>
        public void SetReadPosition(int pos)
        {
            m_RPos = pos;
        }

        /// <inheritdoc/>
        public void SetWritePosition(int pos)
        {
            m_WPos = pos;
        }

        /// <inheritdoc/>
        public void Clear()
        {
            m_RPos = 0;
            m_WPos = 0;
        }

        /// <inheritdoc/>
        public void WriteZero(int size)
        {
            if (size == 1)
            {
                Write(0);
                return;
            }

            var zero = new byte[size];
            Write(zero);
        }

        /// <inheritdoc/>
        public void Write(byte b)
        {
            TryExpand(1);
            m_Buff[m_WPos] = b;
            m_WPos++;
        }

        /// <inheritdoc/>
        public void Write(byte[] bytes, int startIndex, int size)
        {
            if (null == bytes || size == 0 || startIndex >= size || startIndex < 0)
                return;
            TryExpand(size);
            System.Buffer.BlockCopy(bytes, startIndex, m_Buff, m_WPos, size);
            m_WPos += size;
        }

        /// <inheritdoc/>
        public void Write(byte[] bytes, int startIndex)
        {
            var len = bytes?.Length ?? 0;
            if (startIndex < 0 || startIndex >= len)
                return;
            var size = len - startIndex;
            Write(bytes, startIndex, size);
        }

        /// <inheritdoc/>
        public void Write(byte[] bytes)
        {
            Write(bytes, 0);
        }

        /// <inheritdoc/>
        public byte ReadByte()
        {
            if (Len <= 0) return 0;
            var rs = m_Buff[m_RPos];
            m_RPos += 1;
            return rs;
        }

        /// <inheritdoc/>
        public byte[] ReadBytes()
        {
            var ln = Len;
            if (ln <= 0)
            {
                return null;
            }

            var copy = new byte[ln];
            System.Buffer.BlockCopy(m_Buff, m_RPos, copy, 0, ln);
            m_RPos = m_WPos = 0;
            return copy;
        }

        /// <inheritdoc/>
        public byte[] ReadBytes(int size)
        {
            if (size > Len)
            {
                return null;
            }

            if (0 == size)
            {
                return new byte[] { };
            }

            var tmpBuffer = new byte[size];
            System.Buffer.BlockCopy(m_Buff, m_RPos, tmpBuffer, 0, size);
            m_RPos += size;
            return tmpBuffer;
        }

        /// <inheritdoc/>
        public int ReadBytesTo(ref byte[] dst)
        {
            var size = dst?.Length ?? 0;
            return ReadBytesTo(ref dst, size);
        }

        /// <inheritdoc/>
        public int ReadBytesTo(ref byte[] dst, int size)
        {
            if (null == dst || dst.Length == 0) return 0;
            size = Math.Min(Len, size);
            if (size <= 0) return 0;

            System.Buffer.BlockCopy(m_Buff, m_RPos, dst, 0, size);
            m_RPos += size;
            return size;
        }

        /// <inheritdoc/>
        public byte CopyByte()
        {
            return m_Buff[m_RPos];
        }

        /// <inheritdoc/>
        public byte[] CopyBytes(int offset = 0)
        {
            return CopyBytes(Len - offset, offset);
        }

        /// <inheritdoc/>
        public byte[] CopyBytes(int size, int offset)
        {
            var maxLen = Len - offset;
            if (size <= 0 || size > maxLen)
            {
                return null;
            }

            var copy = new byte[size];
            System.Buffer.BlockCopy(m_Buff, m_RPos + offset, copy, 0, size);
            return copy;
        }

        /// <inheritdoc/>
        public int CopyBytesTo(ref byte[] dst, int offset = 0)
        {
            return CopyBytesTo(ref dst, (dst?.Length ?? 0), offset);
        }

        /// <inheritdoc/>
        public int CopyBytesTo(ref byte[] dst, int size, int offset)
        {
            if (null == dst || dst.Length == 0) return 0;
            size = Math.Min(size, Len - offset);
            if (size <= 0) return 0;

            System.Buffer.BlockCopy(m_Buff, m_RPos + offset, dst, 0, size);
            return size;
        }

        private void TryExpand(int addSize)
        {
            if (addSize + Len > Cap)
            {
                InnerExpandCapacity(addSize + Len);
            }

            if (m_WPos + addSize >= Cap)
            {
                InnerFormatBuffer();
            }
        }

        /// <summary>
        /// Expand capacity when needed.
        /// 按需扩展容量
        /// </summary>
        /// <param name="max">Minimum required capacity; 所需最小容量</param>
        private void InnerExpandCapacity(int max)
        {
            if (max <= Cap)
            {
                return;
            }

            byte[] array;
            if (max > m_BufferSize[m_BufferSize.Length - 1])
            {
                var newMax = ((max - 1024) / DefaultBufferAdd + 1) * DefaultBufferAdd + 1024;
                array = new byte[newMax];
            }
            else
            {
                var s = m_BufferSize[m_BufferSize.Length - 1];
                foreach (var size in m_BufferSize)
                {
                    if (max > size) continue;
                    s = size;
                    break;
                }

                array = new byte[s];
            }

            System.Buffer.BlockCopy(m_Buff, m_RPos, array, 0, Len);
            m_Buff = array;
            m_WPos -= m_RPos;
            m_RPos = 0;
        }

        /// <summary>
        /// Compact unread data to the front of the buffer.
        /// 将未读数据紧凑到缓冲区前端
        /// </summary>
        private void InnerFormatBuffer()
        {
            if (m_RPos == 0)
            {
                return;
            }

            var index = 0;
            for (var i = 0; i < Len; i++)
            {
                m_Buff[index] = m_Buff[m_RPos + i];
                index++;
            }

            System.Buffer.BlockCopy(m_Buff, m_RPos + 1, m_Buff, 0, Len);

            m_WPos -= m_RPos;
            m_RPos = 0;
        }
    }
}
