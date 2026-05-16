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

        /// <summary>
        /// Whether this converter uses little-endian byte order.
        /// 是否使用小端字节序
        /// </summary>
        public bool IsLittleEndian => m_LittleEndian;

        /// <summary>
        /// Create a converter with UTF-8 string encoding.
        /// 使用 UTF-8 字符串编码创建转换器
        /// </summary>
        /// <param name="littleEndian">True for little-endian; true 表示小端</param>
        public EndianCoverter(bool littleEndian)
        {
            m_LittleEndian = littleEndian;
            m_Reverse = m_LittleEndian != BitConverter.IsLittleEndian;
            m_Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Create a converter with the specified string encoding.
        /// 使用指定字符串编码创建转换器
        /// </summary>
        /// <param name="littleEndian">True for little-endian; true 表示小端</param>
        /// <param name="encoding">String encoding; 字符串编码</param>
        public EndianCoverter(bool littleEndian, Encoding encoding)
        {
            m_LittleEndian = littleEndian;
            m_Reverse = m_LittleEndian != BitConverter.IsLittleEndian;
            m_Encoding = encoding;
        }

        /// <summary>
        /// Read ulong from bytes at startIndex.
        /// 从字节数组指定位置读取 ulong
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public ulong ToUInt64(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToUInt64(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.ULongSize);
            return BitConverter.ToUInt64(rv, 0);
        }

        /// <summary>
        /// Read uint from bytes at startIndex.
        /// 从字节数组指定位置读取 uint
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public uint ToUInt32(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToUInt32(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.UIntSize);
            return BitConverter.ToUInt32(rv, 0);
        }

        /// <summary>
        /// Read ushort from bytes at startIndex.
        /// 从字节数组指定位置读取 ushort
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public ushort ToUInt16(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToUInt16(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.UShortSize);
            return BitConverter.ToUInt16(rv, 0);
        }

        /// <summary>
        /// Read byte from bytes at startIndex.
        /// 从字节数组指定位置读取 byte
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public byte ToUInt8(byte[] bytes, int startIndex)
        {
            return bytes[startIndex];
        }

        /// <summary>
        /// Read long from bytes at startIndex.
        /// 从字节数组指定位置读取 long
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public long ToInt64(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToInt64(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.LongSize);
            return BitConverter.ToInt64(rv, 0);
        }

        /// <summary>
        /// Read int from bytes at startIndex.
        /// 从字节数组指定位置读取 int
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public int ToInt32(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToInt32(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.IntSize);
            return BitConverter.ToInt32(rv, 0);
        }

        /// <summary>
        /// Read short from bytes at startIndex.
        /// 从字节数组指定位置读取 short
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public short ToInt16(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToInt16(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.ShortSize);
            return BitConverter.ToInt16(rv, 0);
        }

        /// <summary>
        /// Read sbyte from bytes at startIndex.
        /// 从字节数组指定位置读取 sbyte
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public sbyte ToInt8(byte[] bytes, int startIndex)
        {
            return (sbyte) bytes[startIndex];
        }

        /// <summary>
        /// Read double from bytes at startIndex.
        /// 从字节数组指定位置读取 double
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public double ToDouble(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToDouble(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.DoubleSize);
            return BitConverter.ToDouble(rv, 0);
        }

        /// <summary>
        /// Read float from bytes at startIndex.
        /// 从字节数组指定位置读取 float
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public float ToFloat(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToSingle(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.FloatSize);
            return BitConverter.ToSingle(rv, 0);
        }

        /// <summary>
        /// Read char from bytes at startIndex.
        /// 从字节数组指定位置读取 char
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public char ToChar(byte[] bytes, int startIndex)
        {
            if (!m_Reverse)
            {
                return BitConverter.ToChar(bytes, startIndex);
            }

            var rv = InnerGetReverseBytes(bytes, startIndex, BinarySize.CharSize);
            return BitConverter.ToChar(rv, 0);
        }

        /// <summary>
        /// Read bool from bytes at startIndex.
        /// 从字节数组指定位置读取 bool
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
        public bool ToBool(byte[] bytes, int startIndex)
        {
            return BitConverter.ToBoolean(bytes, startIndex);
        }

        /// <summary>
        /// Decode string from bytes starting at startIndex.
        /// 从字节数组指定位置解码字符串
        /// </summary>
        /// <param name="bytes">Source bytes; 源字节数组</param>
        /// <param name="startIndex">Start index; 起始下标</param>
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

        /// <summary>
        /// Encode ulong to bytes with configured endianness.
        /// 按配置的字节序将 ulong 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(ulong value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode long to bytes with configured endianness.
        /// 按配置的字节序将 long 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(long value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode uint to bytes with configured endianness.
        /// 按配置的字节序将 uint 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(uint value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode int to bytes with configured endianness.
        /// 按配置的字节序将 int 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(int value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode ushort to bytes with configured endianness.
        /// 按配置的字节序将 ushort 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(ushort value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode short to bytes with configured endianness.
        /// 按配置的字节序将 short 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(short value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode double to bytes with configured endianness.
        /// 按配置的字节序将 double 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(double value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode float to bytes with configured endianness.
        /// 按配置的字节序将 float 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(float value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode char to bytes with configured endianness.
        /// 按配置的字节序将 char 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(char value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode bool to bytes with configured endianness.
        /// 按配置的字节序将 bool 编码为字节数组
        /// </summary>
        /// <param name="value">Value; 值</param>
        public byte[] GetBytes(bool value)
        {
            var rs = BitConverter.GetBytes(value);
            if (m_Reverse)
            {
                Array.Reverse(rs);
            }

            return rs;
        }

        /// <summary>
        /// Encode string to bytes using configured encoding (no endian swap).
        /// 使用配置的编码将字符串编码为字节数组（不涉及字节序翻转）
        /// </summary>
        /// <param name="value">String value; 字符串值</param>
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