using System;
using System.Runtime.CompilerServices;

namespace JLGames.Infra.Buffer
{
    public partial class DataBuffer
    {
        // IDataBufferWriter

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteLen(int len)
        {
            m_Buff.Write(m_Coverter.GetBytes((ushort)len));
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(bool data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(char data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(byte data)
        {
            m_Buff.Write(data);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(ushort data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(uint data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(ulong data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(sbyte data)
        {
            m_Buff.Write((byte)data);
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(short data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(int data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(long data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }

        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(float data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }


        /// <inheritdoc/>
        [MethodImpl((MethodImplOptions)256)]
        public void WriteData(double data)
        {
            m_Buff.Write(m_Coverter.GetBytes(data));
        }

        /// <inheritdoc/>
        public void WriteData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            var bytes = m_Coverter.GetBytes(data);
            WriteLen(bytes.Length);
            m_Buff.Write(bytes);
        }

        private readonly IByteBuffer m_TempByteBuff = new ByteBuffer();

        /// <inheritdoc/>
        public void WriteData(bool[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(char[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            m_Buff.Write(data);
        }

        /// <inheritdoc/>
        public void WriteData(ushort[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(uint[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(ulong[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(sbyte[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(short[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(int[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(long[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(float[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(double[] data)
        {
            if (data == null || data.Length == 0)
            {
                m_Buff.WriteZero(LenSize);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteData(string[] data)
        {
            if (data == null || data.Length == 0)
            {
                WriteLen(0);
                return;
            }

            WriteLen(data.Length);
            for (var i = 0; i < data.Length; i++)
            {
                WriteData(data[i]);
            }
        }

        /// <inheritdoc/>
        public void WriteBaseData(object data)
        {
            if (data is bool b)
            {
                WriteData(b);
                return;
            }

            if (data is char c)
            {
                WriteData(c);
                return;
            }

            if (data is byte uint8)
            {
                WriteData(uint8);
                return;
            }

            if (data is ushort uint16)
            {
                WriteData(uint16);
                return;
            }

            if (data is uint uint32)
            {
                WriteData(uint32);
                return;
            }

            if (data is ulong uint64)
            {
                WriteData(uint64);
                return;
            }

            if (data is sbyte int8)
            {
                WriteData(int8);
                return;
            }

            if (data is short int16)
            {
                WriteData(int16);
                return;
            }

            if (data is int int32)
            {
                WriteData(int32);
                return;
            }

            if (data is long int64)
            {
                WriteData(int64);
                return;
            }

            if (data is float float32)
            {
                WriteData(float32);
                return;
            }

            if (data is double double64)
            {
                WriteData(double64);
                return;
            }

            if (data is string str)
            {
                WriteData(str);
                return;
            }

            if (data is bool[] bs)
            {
                WriteData(bs);
                return;
            }

            if (data is char[] cs)
            {
                WriteData(cs);
                return;
            }

            if (data is byte[] uint8S)
            {
                WriteData(uint8S);
                return;
            }

            if (data is ushort[] uint16S)
            {
                WriteData(uint16S);
                return;
            }

            if (data is uint[] uint32S)
            {
                WriteData(uint32S);
                return;
            }

            if (data is ulong[] uint64S)
            {
                WriteData(uint64S);
                return;
            }

            if (data is sbyte[] int8S)
            {
                WriteData(int8S);
                return;
            }

            if (data is short[] int16S)
            {
                WriteData(int16S);
                return;
            }

            if (data is int[] int32S)
            {
                WriteData(int32S);
                return;
            }

            if (data is long[] int64S)
            {
                WriteData(int64S);
                return;
            }

            if (data is float[] float32S)
            {
                WriteData(float32S);
                return;
            }

            if (data is double[] double64S)
            {
                WriteData(double64S);
                return;
            }

            if (data is string[] strs)
            {
                WriteData(strs);
                return;
            }

            throw new Exception($"Data type[{data.GetType()}] unsupport!");
        }
    }
}