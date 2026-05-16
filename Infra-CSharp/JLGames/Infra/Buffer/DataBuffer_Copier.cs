using System;
using System.Runtime.CompilerServices;

namespace JLGames.Infra.Buffer
{
    public partial class DataBuffer
    {
        // IDataBufferCopier

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public int CopyLen(int offset = 0)
        {
            return CopyUInt16(offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public ulong CopyUInt64(int offset = 0)
        {
            return m_Coverter.ToUInt64(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public uint CopyUInt32(int offset = 0)
        {
            return m_Coverter.ToUInt32(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public ushort CopyUInt16(int offset = 0)
        {
            return m_Coverter.ToUInt16(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public byte CopyUInt8(int offset = 0)
        {
            return m_Buff.BuffData[m_Buff.ReadPosition + offset];
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public long CopyInt64(int offset = 0)
        {
            return m_Coverter.ToInt64(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public int CopyInt32(int offset = 0)
        {
            return m_Coverter.ToInt32(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public short CopyInt16(int offset = 0)
        {
            return m_Coverter.ToInt16(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public sbyte CopyInt8(int offset = 0)
        {
            return (sbyte) m_Buff.BuffData[m_Buff.ReadPosition + offset];
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public bool CopyBool(int offset = 0)
        {
            return m_Coverter.ToBool(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public double CopyDouble(int offset = 0)
        {
            return m_Coverter.ToDouble(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public float CopyFloat(int offset = 0)
        {
            return m_Coverter.ToFloat(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions) 256)]
        public char CopyChar(int offset = 0)
        {
            return m_Coverter.ToChar(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        public string CopyString(int offset = 0)
        {
            return m_Coverter.ToString(m_Buff.BuffData, m_Buff.ReadPosition + offset);
        }

        /// <inheritdoc/>
        public bool[] CopyBoolArray()
        {
            var len = CopyLen();
            return CopyBoolArray(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public bool[] CopyBoolArray(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new bool[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < rs.Length; index++)
            {
                rs[index] = m_Coverter.ToBool(m_Buff.BuffData, readIndex + index * BinarySize.BoolSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public char[] CopyCharArray()
        {
            var len = CopyLen();
            return CopyCharArray(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public char[] CopyCharArray(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new char[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToChar(m_Buff.BuffData, readIndex + index * BinarySize.CharSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public byte[] CopyUInt8Array()
        {
            var len = CopyLen();
            return CopyUInt8Array(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public byte[] CopyUInt8Array(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new byte[num];
            var readIndex = m_Buff.ReadPosition + offset;
            System.Buffer.BlockCopy(m_Buff.BuffData, readIndex, rs, 0, num);
            return rs;
        }

        /// <inheritdoc/>
        public ushort[] CopyUInt16Array()
        {
            var len = CopyLen();
            return CopyUInt16Array(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public ushort[] CopyUInt16Array(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new ushort[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToUInt16(m_Buff.BuffData, readIndex + index * BinarySize.UShortSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public uint[] CopyUInt32Array()
        {
            var len = CopyLen();
            return CopyUInt32Array(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public uint[] CopyUInt32Array(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new uint[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToUInt32(m_Buff.BuffData, readIndex + index * BinarySize.UIntSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public ulong[] CopyUInt64Array()
        {
            var len = CopyLen();
            return CopyUInt64Array(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public ulong[] CopyUInt64Array(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new ulong[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToUInt64(m_Buff.BuffData, readIndex + index * BinarySize.ULongSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public sbyte[] CopyInt8Array()
        {
            var len = CopyLen();
            return CopyInt8Array(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public sbyte[] CopyInt8Array(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new sbyte[num];
            var readIndex = m_Buff.ReadPosition + offset;
            System.Buffer.BlockCopy(m_Buff.BuffData, readIndex, rs, 0, num);
            return rs;
        }

        /// <inheritdoc/>
        public short[] CopyInt16Array()
        {
            var len = CopyLen();
            return CopyInt16Array(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public short[] CopyInt16Array(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new short[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToInt16(m_Buff.BuffData, readIndex + index * BinarySize.ShortSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public int[] CopyInt32Array()
        {
            var len = CopyLen();
            return CopyInt32Array(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public int[] CopyInt32Array(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new int[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToInt32(m_Buff.BuffData, readIndex + index * BinarySize.IntSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public long[] CopyInt64Array()
        {
            var len = CopyLen();
            return CopyInt64Array(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public long[] CopyInt64Array(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new long[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToInt64(m_Buff.BuffData, readIndex + index * BinarySize.LongSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public float[] CopyFloatArray()
        {
            var len = CopyLen();
            return CopyFloatArray(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public float[] CopyFloatArray(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new float[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToFloat(m_Buff.BuffData, readIndex + index * BinarySize.FloatSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public double[] CopyDoubleArray()
        {
            var len = CopyLen();
            return CopyDoubleArray(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public double[] CopyDoubleArray(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new double[num];
            var readIndex = m_Buff.ReadPosition + offset;
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToDouble(m_Buff.BuffData, readIndex + index * BinarySize.DoubleSize);
            }

            return rs;
        }

        /// <inheritdoc/>
        public string[] CopyStringArray()
        {
            var len = CopyLen();
            return CopyStringArray(len, BinarySize.LenSize);
        }

        /// <inheritdoc/>
        public string[] CopyStringArray(int num, int offset = 0)
        {
            if (num <= 0) return null;
            var rs = new string[num];
            for (var index = 0; index < num; index++)
            {
                var ln = CopyLen(offset);
                rs[index] = CopyString(offset);
                offset = offset + ln + BinarySize.LenSize;
            }

            return rs;
        }

        /// <inheritdoc/>
        public void CopyBaseDataTo(ref object data)
        {
            if (data is bool)
            {
                data = CopyBool();
                return;
            }

            if (data is char)
            {
                data = CopyChar();
                return;
            }

            if (data is byte)
            {
                data = CopyUInt8();
                return;
            }

            if (data is ushort)
            {
                data = CopyUInt16();
                return;
            }

            if (data is uint)
            {
                data = CopyUInt32();
                return;
            }

            if (data is ulong)
            {
                data = CopyUInt64();
                return;
            }

            if (data is sbyte)
            {
                data = CopyInt8();
                return;
            }

            if (data is short)
            {
                data = CopyInt16();
                return;
            }

            if (data is int)
            {
                data = CopyInt32();
                return;
            }

            if (data is long)
            {
                data = CopyInt64();
                return;
            }

            if (data is float)
            {
                data = CopyFloat();
                return;
            }

            if (data is double)
            {
                data = CopyDouble();
                return;
            }

            if (data is string)
            {
                data = CopyString();
                return;
            }

            if (data is bool[])
            {
                data = CopyBoolArray();
                return;
            }

            if (data is char[])
            {
                data = CopyCharArray();
                return;
            }

            if (data is byte[])
            {
                data = CopyUInt8Array();
                return;
            }

            if (data is ushort[])
            {
                data = CopyUInt16Array();
                return;
            }

            if (data is uint[])
            {
                data = CopyUInt32Array();
                return;
            }

            if (data is ulong[])
            {
                data = CopyUInt64Array();
                return;
            }

            if (data is sbyte[])
            {
                data = CopyInt8Array();
                return;
            }

            if (data is short[])
            {
                data = CopyInt16Array();
                return;
            }

            if (data is int[])
            {
                data = CopyInt32Array();
                return;
            }

            if (data is long[])
            {
                data = CopyInt64Array();
                return;
            }

            if (data is float[])
            {
                data = CopyFloatArray();
                return;
            }

            if (data is double[])
            {
                data = CopyDoubleArray();
                return;
            }

            if (data is string[])
            {
                data = CopyStringArray();
                return;
            }

            throw new Exception($"Data type[{data.GetType()}] unsupport!");
        }
    }
}