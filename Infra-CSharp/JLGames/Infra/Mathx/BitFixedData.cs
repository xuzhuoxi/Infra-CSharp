using System;
using System.Text;
using JLGames.Infra.Utils;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// Bit memory (fixed length)
    /// Three concepts: raw data, bit data, business data
    ///     Raw data(uint):         The real data type of the stored data
    ///     Bit data(bit:bool):     The bitwise data value (0/1) of the original data
    ///     Business data(value):   A data value composed of multiple bits of data spliced together
    /// 位存储器（定长）
    /// 三个概念：原始数据、位数据、业务数据
    ///     原始数据(uint)：        存储数据的真实数据类型
    ///     位数据(bit:bool)：      原始数据的按位数据值(0/1)
    ///     业务数据(value:uint)：  由多个位数据拼接起来组成的数据值
    /// </summary>
    public class BitFixedData
    {
        /// <summary>
        /// Number bits of per Raw data.
        /// 原始数据位数
        /// </summary>
        private const int BitPerUint = BitMark.BitsPerUint;

        private uint[] m_RawData;

        /// <summary>
        /// Number of bits per data.
        /// 每个数据的 占用位数量
        /// </summary>
        public int BitsPerValue { get; }

        /// <summary>
        /// Value length.
        /// 数据长度
        /// </summary>
        public int ValueLen { get; }

        /// <summary>
        /// Valid bit length of all data.
        /// 全部数据的 有效位数量
        /// BitLen = BitsPerValue * ValueLen
        /// </summary>
        public int BitLen { get; }

        /// <summary>
        /// Raw data length.
        /// 数据载体结构的存储长度
        /// </summary>
        public int RawDataLen { get; }

        /// <summary>
        /// Raw data.
        /// 原始数据
        /// </summary>
        public uint[] RawData => m_RawData;

        /// <summary>
        /// Construct a bit memory
        /// 构造一个位存储器
        /// </summary>
        /// <param name="valueLen"></param>
        public BitFixedData(int valueLen) : this(1, valueLen)
        {
        }

        /// <summary>
        /// Construct a bit memory
        /// 构造一个位存储器
        /// </summary>
        /// <param name="bitsPerValue">小于32</param>
        /// <param name="valueLen"></param>
        public BitFixedData(int bitsPerValue, int valueLen)
        {
            if (bitsPerValue < 0 || bitsPerValue >= BitMark.BitsPerUint)
            {
                throw new Exception("Bit count per value is out of range.");
            }

            BitsPerValue = bitsPerValue;
            ValueLen = valueLen;
            BitLen = bitsPerValue * valueLen;
            RawDataLen = (int) Math.Ceiling((double) BitLen / BitPerUint);

            m_RawData = new uint[RawDataLen];
        }

        public override string ToString()
        {
            return ToBitString(0, BitLen, " ");
        }

        /// <summary>
        /// Set raw data.
        /// 设置原始数据
        /// Lack: set 0.
        /// 不足：补0
        /// Excess: truncate
        /// 超量：截断
        /// </summary>
        /// <param name="data"></param>
        public void SetRawData(uint[] data)
        {
            if (null == data || data.Length == 0)
            {
                m_RawData = new uint[RawDataLen];
                return;
            }

            if (data.Length == RawDataLen)
            {
                m_RawData = data;
                return;
            }

            if (data.Length > RawDataLen)
            {
                m_RawData = ArrayUtil.SubArray(data, 0, RawDataLen);
                return;
            }

            m_RawData = new uint[RawDataLen];
            Array.Copy(data, 0, m_RawData, 0, RawDataLen);
        }

        /// <summary>
        /// Get all values.
        /// 取全部业务数据
        /// </summary>
        /// <returns></returns>
        public uint[] GetValues()
        {
            return GetValues(0, ValueLen);
        }

        /// <summary>
        /// Get a range of values.
        /// 取一个范围内的数据
        /// </summary>
        /// <param name="valueIndex"></param>
        /// <param name="valueCount"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public uint[] GetValues(int valueIndex, int valueCount)
        {
            if (valueIndex < 0 || valueIndex >= ValueLen || valueCount <= 0 || (valueIndex + valueCount) > ValueLen)
            {
                throw new Exception("Function \"GetValues\" Params out of range. ");
            }

            var rs = new uint[valueCount];
            for (var index = 0; index < valueCount; index++)
            {
                rs[index] = GetValue(valueIndex + index);
            }

            return rs;
        }

        /// <summary>
        /// Get a single value
        /// 取单个业务数据
        /// </summary>
        /// <param name="valueIndex"></param>
        /// <returns></returns>
        public uint GetValue(int valueIndex)
        {
            uint rs = 0;
            for (var add = 0; add < BitsPerValue; add++)
            {
                var bit = GetBit(valueIndex + add);
                if (bit)
                {
                    rs = rs | BitMark.GetUintMark(add);
                }
            }

            return rs;
        }

        /// <summary>
        /// Set a single value
        /// 设置单个业务数据
        /// </summary>
        /// <param name="valueIndex"></param>
        /// <param name="value"></param>
        public void SetValue(int valueIndex, uint value)
        {
            int startBitIndex;
            ValueIndexToBitIndex(valueIndex, out startBitIndex);
            for (var add = 0; add < BitsPerValue; add++)
            {
                var bit = (value & BitMark.GetUintMark(add)) > 0;
                SetBit(startBitIndex + add, bit);
            }
        }

        /// <summary>
        /// Take the binary string representation of all bits
        /// 取全部位数的二进制字符串表示
        /// </summary>
        /// <returns></returns>
        public string GetStringBits()
        {
            return ToBitString(0, BitLen);
        }

        /// <summary>
        /// Get all bits of data
        /// 取全部位数据
        /// </summary>
        /// <returns></returns>
        public bool[] GetBits()
        {
            return GetBits(0, BitLen);
        }

        /// <summary>
        /// Get bit data in a range.
        /// 取一个范围内的位数据
        /// </summary>
        /// <param name="bitIndex"></param>
        /// <param name="bitCount"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool[] GetBits(int bitIndex, int bitCount)
        {
            if (bitIndex < 0 || bitIndex >= BitLen || bitCount <= 0 || (bitIndex + bitCount) > BitLen)
            {
                throw new Exception("Function \"GetBits\" Params out of range. ");
            }

            var rs = new bool[bitCount];
            for (var index = 0; index < bitCount; index++)
            {
                rs[index] = GetBit(bitIndex + index);
            }

            return rs;
        }

        /// <summary>
        /// Get a single bit of data.
        /// 取单个位数据
        /// </summary>
        /// <param name="bitIndex"></param>
        /// <returns></returns>
        public bool GetBit(int bitIndex)
        {
            int outDataIndex;
            int inDataIndex;
            BitIndexToDataIndex(bitIndex, out outDataIndex, out inDataIndex);
            return (m_RawData[outDataIndex] & BitMark.GetUintMark(inDataIndex)) > 0;
        }

        /// <summary>
        /// Set a single bit of data.
        /// 设置单个位数据
        /// </summary>
        /// <param name="bitIndex"></param>
        /// <param name="isTrue"></param>
        public void SetBit(int bitIndex, bool isTrue)
        {
            int outDataIndex;
            int inDataIndex;
            BitIndexToDataIndex(bitIndex, out outDataIndex, out inDataIndex);

            if (isTrue)
            {
                m_RawData[outDataIndex] = m_RawData[outDataIndex] | BitMark.GetUintMark(inDataIndex);
            }
            else
            {
                m_RawData[outDataIndex] = m_RawData[outDataIndex] & ~BitMark.GetUintMark(inDataIndex);
            }
        }

        protected readonly StringBuilder TempSb = new StringBuilder();

        /// <summary>
        /// 取位数的二进制字符串表示
        /// </summary>
        /// <param name="bitIndex"></param>
        /// <param name="bitLen"></param>
        /// <param name="valueSpace"></param>
        /// <returns></returns>
        public string ToBitString(int bitIndex, int bitLen, string valueSpace = "")
        {
            if (null == m_RawData || m_RawData.Length == 0) return "";
            valueSpace = valueSpace ?? "";

            int outStartIdx;
            int inStartIdx;
            BitIndexToDataIndex(bitIndex, out outStartIdx, out inStartIdx);
            int outEndIdx;
            int inEndIdx;
            BitIndexToDataIndex(bitIndex + bitLen, out outEndIdx, out inEndIdx);

            // 同一Data内
            if (outStartIdx == outEndIdx)
            {
                return ToBinary(m_RawData[outStartIdx], inStartIdx, inEndIdx - inStartIdx);
            }

            TempSb.Clear();
            var startStr = ToBinary(m_RawData[outStartIdx], inStartIdx, BitPerUint - inStartIdx);
            TempSb.Append(startStr + valueSpace);

            for (var index = outStartIdx + 1; index <= outEndIdx - 1; index++)
            {
                TempSb.Append(Convert.ToString(m_RawData[index], 2).PadLeft(BitPerUint, '0') + valueSpace);
            }

            if (inEndIdx == 0)
            {
                if (valueSpace != "")
                {
                    TempSb.Remove(TempSb.Length - valueSpace.Length, valueSpace.Length);
                }
            }
            else
            {
                var endStr = ToBinary(m_RawData[outEndIdx], 0, inEndIdx);
                TempSb.Append(endStr);
            }

            return TempSb.ToString();
        }

        private string ToBinary(uint data, int startIndex, int len)
        {
            var mark = BitMark.GetUintMark(startIndex, len);
            var value = (m_RawData[data] & mark) >> startIndex;
            return Convert.ToString(value, 2).PadLeft(len, '0');
        }

        private void ValueIndexToDataIndex(int valueIndex, out int outDataIndex, out int inDataIndex)
        {
            var bitIndex = valueIndex * BitsPerValue;
            BitIndexToDataIndex(bitIndex, out outDataIndex, out inDataIndex);
        }

        private void ValueIndexToBitIndex(int valueIndex, out int bitIndex)
        {
            bitIndex = valueIndex * BitsPerValue;
        }

        private void BitIndexToDataIndex(int bitIndex, out int outDataIndex, out int inDataIndex)
        {
            outDataIndex = bitIndex / BitPerUint;
            inDataIndex = bitIndex % BitPerUint;
        }

        private void BitIndexToDataIndex(int bitIndex, out int outDataIndex)
        {
            outDataIndex = bitIndex / BitPerUint;
        }

        private void BitIndexToValueIndex(int bitIndex, out int outValueIndex, out int inValueIndex)
        {
            outValueIndex = bitIndex / BitsPerValue;
            inValueIndex = bitIndex % BitsPerValue;
        }

        private void BitIndexToValueIndex(int bitIndex, out int outValueIndex)
        {
            outValueIndex = bitIndex / BitsPerValue;
        }
    }
}