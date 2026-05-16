using System;
using System.Runtime.CompilerServices;

namespace JLGames.Infra.Buffer
{
    public partial class DataBuffer
    {
        // IDataBufferReader

        /// <inheritdoc/>
        public int ReadLen()
        {
            return ReadUInt16();
        }

        /// <inheritdoc/>
        public bool ReadBool()
        {
            var res = m_Buff.ReadBytes(BinarySize.BoolSize);
            return m_Coverter.ToBool(res, 0);
        }

        /// <inheritdoc/>
        public char ReadChar()
        {
            var res = m_Buff.ReadBytes(BinarySize.CharSize);
            return m_Coverter.ToChar(res, 0);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public byte ReadUInt8()
        {
            return m_Buff.ReadByte();
        }

        /// <inheritdoc/>
        public ushort ReadUInt16()
        {
            var res = m_Buff.ReadBytes(BinarySize.UShortSize);
            return m_Coverter.ToUInt16(res, 0);
        }

        /// <inheritdoc/>
        public uint ReadUInt32()
        {
            var res = m_Buff.ReadBytes(BinarySize.UIntSize);
            return m_Coverter.ToUInt32(res, 0);
        }

        /// <inheritdoc/>
        public ulong ReadUInt64()
        {
            var res = m_Buff.ReadBytes(BinarySize.ULongSize);
            return m_Coverter.ToUInt64(res, 0);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public sbyte ReadInt8()
        {
            return (sbyte)m_Buff.ReadByte();
        }

        /// <inheritdoc/>
        public short ReadInt16()
        {
            var res = m_Buff.ReadBytes(BinarySize.ShortSize);
            return m_Coverter.ToInt16(res, 0);
        }

        /// <inheritdoc/>
        public int ReadInt32()
        {
            var res = m_Buff.ReadBytes(BinarySize.IntSize);
            return m_Coverter.ToInt32(res, 0);
        }

        /// <inheritdoc/>
        public long ReadInt64()
        {
            var res = m_Buff.ReadBytes(BinarySize.LongSize);
            return m_Coverter.ToInt64(res, 0);
        }

        /// <inheritdoc/>
        public float ReadFloat()
        {
            var res = m_Buff.ReadBytes(BinarySize.FloatSize);
            return m_Coverter.ToFloat(res, 0);
        }

        /// <inheritdoc/>
        public double ReadDouble()
        {
            var res = m_Buff.ReadBytes(BinarySize.DoubleSize);
            return m_Coverter.ToDouble(res, 0);
        }

        /// <inheritdoc/>
        public string ReadString()
        {
            var len = ReadLen();
            if (0 == len)
            {
                return "";
            }

            var bytes = m_Buff.ReadBytes(len);
            return m_Coverter.ToString(bytes, 0);
        }

        // Array ------------------

        /// <inheritdoc/>
        public bool[] ReadBoolArray()
        {
            var len = ReadLen();
            return ReadBoolArray(len);
        }

        /// <inheritdoc/>
        public bool[] ReadBoolArray(int num)
        {
            if (num <= 0) return null;
            var rs = new bool[num];
            for (var index = 0; index < rs.Length; index++)
            {
                rs[index] = m_Coverter.ToBool(m_Buff.ReadBytes(BinarySize.BoolSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public char[] ReadCharArray()
        {
            var len = ReadLen();
            return ReadCharArray(len);
        }

        /// <inheritdoc/>
        public char[] ReadCharArray(int num)
        {
            if (num <= 0) return null;
            var rs = new char[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToChar(m_Buff.ReadBytes(BinarySize.CharSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public byte[] ReadUInt8Array()
        {
            var len = ReadLen();
            return ReadUInt8Array(len);
        }

        /// <inheritdoc/>
        public byte[] ReadUInt8Array(int num)
        {
            return num <= 0 ? null : m_Buff.ReadBytes(num);
        }

        /// <inheritdoc/>
        public ushort[] ReadUInt16Array()
        {
            var len = ReadLen();
            return ReadUInt16Array(len);
        }

        /// <inheritdoc/>
        public ushort[] ReadUInt16Array(int num)
        {
            if (num <= 0) return null;
            var rs = new ushort[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToUInt16(m_Buff.ReadBytes(BinarySize.UShortSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public uint[] ReadUInt32Array()
        {
            var len = ReadLen();
            return ReadUInt32Array(len);
        }

        /// <inheritdoc/>
        public uint[] ReadUInt32Array(int num)
        {
            if (num <= 0) return null;
            var rs = new uint[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToUInt32(m_Buff.ReadBytes(BinarySize.UIntSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public ulong[] ReadUInt64Array()
        {
            var len = ReadLen();
            return ReadUInt64Array(len);
        }

        /// <inheritdoc/>
        public ulong[] ReadUInt64Array(int num)
        {
            if (num <= 0) return null;
            var rs = new ulong[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToUInt64(m_Buff.ReadBytes(BinarySize.ULongSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public sbyte[] ReadInt8Array()
        {
            var len = ReadLen();
            return ReadInt8Array(len);
        }

        /// <inheritdoc/>
        public sbyte[] ReadInt8Array(int num)
        {
            if (num <= 0) return null;
            var rs = new sbyte[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = (sbyte)m_Buff.ReadByte();
            }

            return rs;
        }

        /// <inheritdoc/>
        public short[] ReadInt16Array()
        {
            var len = ReadLen();
            return ReadInt16Array(len);
        }

        /// <inheritdoc/>
        public short[] ReadInt16Array(int num)
        {
            if (num <= 0) return null;
            var rs = new short[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToInt16(m_Buff.ReadBytes(BinarySize.ShortSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public int[] ReadInt32Array()
        {
            var len = ReadLen();
            return ReadInt32Array(len);
        }

        /// <inheritdoc/>
        public int[] ReadInt32Array(int num)
        {
            if (num <= 0) return null;
            var rs = new int[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToInt32(m_Buff.ReadBytes(BinarySize.IntSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public long[] ReadInt64Array()
        {
            var len = ReadLen();
            return ReadInt64Array(len);
        }

        /// <inheritdoc/>
        public long[] ReadInt64Array(int num)
        {
            if (num <= 0) return null;
            var rs = new long[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToInt64(m_Buff.ReadBytes(BinarySize.LongSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public float[] ReadFloatArray()
        {
            var len = ReadLen();
            return ReadFloatArray(len);
        }

        /// <inheritdoc/>
        public float[] ReadFloatArray(int num)
        {
            if (num <= 0) return null;
            var rs = new float[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToFloat(m_Buff.ReadBytes(BinarySize.FloatSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public double[] ReadDoubleArray()
        {
            var len = ReadLen();
            return ReadDoubleArray(len);
        }

        /// <inheritdoc/>
        public double[] ReadDoubleArray(int num)
        {
            if (num <= 0) return null;
            var rs = new double[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = m_Coverter.ToDouble(m_Buff.ReadBytes(BinarySize.DoubleSize), 0);
            }

            return rs;
        }

        /// <inheritdoc/>
        public string[] ReadStringArray()
        {
            var len = ReadLen();
            return ReadStringArray(len);
        }

        /// <inheritdoc/>
        public string[] ReadStringArray(int num)
        {
            if (num <= 0) return null;
            var rs = new string[num];
            for (var index = 0; index < num; index++)
            {
                rs[index] = ReadString();
            }

            return rs;
        }

        /// <inheritdoc/>
        public void ReadBaseDataTo(ref object data)
        {
            if (data is bool)
            {
                data = ReadBool();
                return;
            }

            if (data is char)
            {
                data = ReadChar();
                return;
            }

            if (data is byte)
            {
                data = ReadUInt8();
                return;
            }

            if (data is ushort)
            {
                data = ReadUInt16();
                return;
            }

            if (data is uint)
            {
                data = ReadUInt32();
                return;
            }

            if (data is ulong)
            {
                data = ReadUInt64();
                return;
            }

            if (data is sbyte)
            {
                data = ReadInt8();
                return;
            }

            if (data is short)
            {
                data = ReadInt16();
                return;
            }

            if (data is int)
            {
                data = ReadInt32();
                return;
            }

            if (data is long)
            {
                data = ReadInt64();
                return;
            }

            if (data is float)
            {
                data = ReadFloat();
                return;
            }

            if (data is double)
            {
                data = ReadDouble();
                return;
            }

            if (data is string)
            {
                data = ReadString();
                return;
            }

            if (data is bool[])
            {
                data = null == data ? ReadBoolArray() : ReadBoolArray(((Array)data).Length);
                return;
            }

            if (data is char[])
            {
                data = null == data ? ReadCharArray() : ReadCharArray(((Array)data).Length);
                return;
            }

            if (data is byte[])
            {
                data = null == data ? ReadUInt8Array() : ReadUInt8Array(((Array)data).Length);
                return;
            }

            if (data is ushort[])
            {
                data = null == data ? ReadUInt16Array() : ReadUInt16Array(((Array)data).Length);
                return;
            }

            if (data is uint[])
            {
                data = null == data ? ReadUInt32Array() : ReadUInt32Array(((Array)data).Length);
                return;
            }

            if (data is ulong[])
            {
                data = null == data ? ReadUInt64Array() : ReadUInt64Array(((Array)data).Length);
                return;
            }

            if (data is sbyte[])
            {
                data = null == data ? ReadInt8Array() : ReadInt8Array(((Array)data).Length);
                return;
            }

            if (data is short[])
            {
                data = null == data ? ReadInt16Array() : ReadInt16Array(((Array)data).Length);
                return;
            }

            if (data is int[])
            {
                data = null == data ? ReadInt32Array() : ReadInt32Array(((Array)data).Length);
                return;
            }

            if (data is long[])
            {
                data = null == data ? ReadInt64Array() : ReadInt64Array(((Array)data).Length);
                return;
            }

            if (data is float[])
            {
                data = null == data ? ReadFloatArray() : ReadFloatArray(((Array)data).Length);
                return;
            }

            if (data is double[])
            {
                data = null == data ? ReadDoubleArray() : ReadDoubleArray(((Array)data).Length);
                return;
            }

            if (data is string[])
            {
                data = null == data ? ReadStringArray() : ReadStringArray(((Array)data).Length);
                return;
            }

            throw new Exception($"Data type[{data.GetType()}] is not support!");
        }
    }
}