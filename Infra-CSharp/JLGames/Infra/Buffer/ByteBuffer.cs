using System;

namespace JLGames.Infra.Buffer
{
    public class ByteBuffer : IByteBuffer
    {
        private const int DefaultBufferSize = 256;
        private const int DefaultBufferAdd = 512;
        private static readonly int[] m_BufferSize = { 16, 32, 64, 128, 256, 512, 1024, 2048 };

        private byte[] m_Buff; //buffer
        private int m_RPos; //read index
        private int m_WPos; //write index

        public int ReadPosition => m_RPos;
        public int WritePosition => m_WPos;
        public int Cap => m_Buff.Length;
        public int Len => m_WPos - m_RPos;
        public byte[] BuffData => m_Buff;

        public ByteBuffer()
        {
            m_Buff = new byte[DefaultBufferSize];
            m_RPos = m_WPos = 0;
        }

        public ByteBuffer(int buffSize)
        {
            m_Buff = new byte[buffSize];
            m_RPos = m_WPos = 0;
        }

        public ByteBuffer(byte[] buffer)
        {
            m_Buff = buffer;
            m_RPos = m_WPos = 0;
        }

        public void SetReadPosition(int pos)
        {
            m_RPos = pos;
        }

        public void SetWritePosition(int pos)
        {
            m_WPos = pos;
        }

        public void Clear()
        {
            m_RPos = 0;
            m_WPos = 0;
        }

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

        public void Write(byte b)
        {
            TryExpand(1);
            m_Buff[m_WPos] = b;
            m_WPos++;
        }

        public void Write(byte[] bytes, int startIndex, int size)
        {
            if (null == bytes || size == 0 || startIndex >= size || startIndex < 0)
                return;
            TryExpand(size);
            System.Buffer.BlockCopy(bytes, startIndex, m_Buff, m_WPos, size);
            m_WPos += size;
        }

        public void Write(byte[] bytes, int startIndex)
        {
            var len = bytes?.Length ?? 0;
            if (startIndex < 0 || startIndex >= len)
                return;
            var size = len - startIndex;
            Write(bytes, startIndex, size);
        }

        public void Write(byte[] bytes)
        {
            Write(bytes, 0);
        }

        public byte ReadByte()
        {
            if (Len <= 0) return 0;
            var rs = m_Buff[m_RPos];
            m_RPos += 1;
            return rs;
        }

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

        public int ReadBytesTo(ref byte[] dst)
        {
            var size = dst?.Length ?? 0;
            return ReadBytesTo(ref dst, size);
        }

        public int ReadBytesTo(ref byte[] dst, int size)
        {
            if (null == dst || dst.Length == 0) return 0;
            size = Math.Min(Len, size);
            if (size <= 0) return 0;

            System.Buffer.BlockCopy(m_Buff, m_RPos, dst, 0, size);
            m_RPos += size;
            return size;
        }

        public byte CopyByte()
        {
            return m_Buff[m_RPos];
        }

        public byte[] CopyBytes(int offset = 0)
        {
            return CopyBytes(Len - offset, offset);
        }

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

        public int CopyBytesTo(ref byte[] dst, int offset = 0)
        {
            return CopyBytesTo(ref dst, (dst?.Length ?? 0), offset);
        }

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
        /// Expand Capactity
        /// 扩展容量
        /// </summary>
        /// <param name="max"></param>
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


        //format buff
        /// <summary>
        /// 
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