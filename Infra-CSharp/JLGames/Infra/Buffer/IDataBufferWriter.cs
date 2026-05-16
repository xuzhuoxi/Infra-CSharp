namespace JLGames.Infra.Buffer
{
    /// <summary>
    /// Typed data buffer writer.
    /// 数据缓冲区写入器
    /// </summary>
    public interface IDataBufferWriter 
    {
        /// <summary>
        /// Write length info
        /// 写入长度信息
        /// </summary>
        /// <param name="len"></param>
        void WriteLen(int len);
        
        /// <summary>
        /// Write a boolean
        /// 写入一个布尔型数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(bool data);

        /// <summary>
        /// Write in one character data, occupying two bytes and 16 bits
        /// 写入一个字符数据，占两字节16位
        /// </summary>
        /// <param name="data"></param>
        void WriteData(char data);

        /// <summary>
        /// Write in an unsigned 8-bit integer
        /// 写入一个无符号8位整型数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(byte data);

        /// <summary>
        /// Write in an unsigned 16-bit integer
        /// 写入一个无符号16位整型数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(ushort data);

        /// <summary>
        /// Write in an unsigned 32-bit integer
        /// 写入一个无符号32位整型数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(uint data);

        /// <summary>
        /// Write in an unsigned 64-bit integer
        /// 写入一个无符号64位整型数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(ulong data);

        /// <summary>
        /// Write in a signed 8-bit integer
        /// 写入一个有符号8位整型数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(sbyte data);

        /// <summary>
        /// Write in a signed 16-bit integer
        /// 写入一个有符号16位整型数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(short data);

        /// <summary>
        /// Write in a signed 32-bit integer
        /// 写入一个有符号32位整型数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(int data);

        /// <summary>
        /// Write in a signed 64-bit integer
        /// 写入一个有符号64位整型数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(long data);

        /// <summary>
        /// Write in a 32-bit single-precision floating-point data
        /// 写入一个32位单精度浮点数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(float data);

        /// <summary>
        /// Write in a 64-bit double-precision floating-point data
        /// 写入一个64位双精度浮点数据
        /// </summary>
        /// <param name="data"></param>
        void WriteData(double data);

        /// <summary>
        /// Write in string data
        /// 写入字符串数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(string data);

        /// <summary>
        /// Write in bool array
        /// 写入 bool 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(bool[] data);

        /// <summary>
        /// Write in character array
        /// 写入 char 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(char[] data);

        /// <summary>
        /// Write in byte array data
        /// 写入 byte 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(byte[] data);

        /// <summary>
        /// Write in ushort array data
        /// 写入 ushort 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(ushort[] data);

        /// <summary>
        /// Write in uint array data
        /// 写入 uint 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(uint[] data);

        /// <summary>
        /// Write in ulong array data
        /// 写入 ulong 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(ulong[] data);

        /// <summary>
        /// Write in sbyte array data
        /// 写入 sbyte 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(sbyte[] data);

        /// <summary>
        /// Write in short array data
        /// 写入 short 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(short[] data);

        /// <summary>
        /// Write in int array data
        /// 写入 int 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(int[] data);

        /// <summary>
        /// Write in long array data
        /// 写入 long 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(long[] data);

        /// <summary>
        /// Write in float array data
        /// 写入 float 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(float[] data);

        /// <summary>
        /// Write in double array data
        /// 写入 double 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(double[] data);

        /// <summary>
        /// Write in string array data
        /// 写入 string 数组数据
        /// Note: Length information will be written
        /// 注意：会写入长度信息
        /// </summary>
        /// <param name="data"></param>
        void WriteData(string[] data);

        /// <summary>
        /// Write basic type data or its array
        /// 写入 基础类型数据 或 其数组
        /// </summary>
        /// <param name="data"></param>
        void WriteBaseData(object data); 
    }
}