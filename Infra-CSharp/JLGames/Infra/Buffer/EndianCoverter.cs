using System;
using System.Text;

namespace JLGames.Infra.Buffer
{
    /// <summary>
    /// Endian Coverter
    /// 字节序转换器
    /// </summary>
    public sealed class EndianCoverter
    {
        #region static

        /// <summary>
        /// Little Endian
        /// 小端
        /// </summary>
        public static readonly EndianCoverter LittleEndianCoverter = new EndianCoverter(true);

        /// <summary>
        /// Big Endian
        /// 大端
        /// </summary>
        public static readonly EndianCoverter BigEndianCoverter = new EndianCoverter(false);

        /// <summary>
        /// Get Endian Coverter
        /// 取字节转换器
        /// </summary>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static EndianCoverter GetEndianCoverter(bool littleEndian)
        {
            return littleEndian ? LittleEndianCoverter : BigEndianCoverter;
        }

        #endregion

        private readonly bool m_LittleEndian;
        private readonly bool m_Reverse;
        private readonly Encoding m_Encoding;

        public bool IsLittleEndian => m_LittleEndian;

        public EndianCoverter(bool littleEndian)
        {
            m_LittleEndian = littleEndian;
            m_Reverse = m_LittleEndian != BitConverter.IsLittleEndian;
            m_Encoding = Encoding.UTF8;
        }

        public EndianCoverter(bool littleEndian, Encoding encoding)
        {
            m_LittleEndian = littleEndian;
            m_Reverse = m_LittleEndian != BitConverter.IsLittleEndian;
            m_Encoding = encoding;
        }

        public ulong ToUInt64(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToUInt64(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.ULongSize);
            return BitConverter.ToUInt64(rv, 0);
        }

        public uint ToUInt32(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToUInt32(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.UIntSize);
            return BitConverter.ToUInt32(rv, 0);
        }

        public ushort ToUInt16(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToUInt16(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.UShortSize);
            return BitConverter.ToUInt16(rv, 0);
        }

        public byte ToUInt8(byte[] bytes, int startIndex)
        {
            return bytes[startIndex];
        }

        public long ToInt64(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToInt64(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.LongSize);
            return BitConverter.ToInt64(rv, 0);
        }

        public int ToInt32(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToInt32(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.IntSize);
            return BitConverter.ToInt32(rv, 0);
        }

        public short ToInt16(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToInt16(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.ShortSize);
            return BitConverter.ToInt16(rv, 0);
        }

        public sbyte ToInt8(byte[] bytes, int startIndex)
        {
            return (sbyte) bytes[startIndex];
        }

        public double ToDouble(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToDouble(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.DoubleSize);
            return BitConverter.ToDouble(rv, 0);
        }

        public float ToFloat(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToSingle(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.FloatSize);
            return BitConverter.ToSingle(rv, 0);
        }

        public char ToChar(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToChar(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.CharSize);
            return BitConverter.ToChar(rv, 0);
        }

        public bool ToBool(byte[] bytes, int startIndex)
        {
            return BitConverter.ToBoolean(bytes, startIndex);
        }

        public string ToString(byte[] bytes, int startIndex)
        {
            if (0 == startIndex)
            {
                return m_Encoding.GetString(bytes);
            }

            var ln = bytes.Length - startIndex;
            var newBytes = new byte[ln];
            Array.Copy(bytes, startIndex, newBytes, 0, ln);
            return m_Encoding.GetString(newBytes);
        }

        public byte[] GetBytes(ulong value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(long value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(uint value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(int value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(ushort value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(short value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(double value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(float value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(char value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(bool value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        public byte[] GetBytes(string value)
        {
            return m_Encoding.GetBytes(value);
        }

        private byte[] InnerGetReverseBytes(byte[] bytes, int startIndex, int size)
        {
            if (bytes == null || bytes.Length <= startIndex + size)
            {
                return null;
            }

            var rs = new byte[size];
            Array.Copy(bytes, startIndex, rs, 0, size);
            Array.Reverse(rs);
            return rs;
        }
    }
}