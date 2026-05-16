namespace JLGames.Infra.Buffer
{
    /// <summary>
    /// Typed data buffer reader.
    /// 数据缓冲区读取器
    /// </summary>
    public interface IDataBufferReader 
    {
        /// <summary>
        /// Read length info
        /// 读取长度信息
        /// </summary>
        int ReadLen();

        /// <summary>
        /// Read a boolean
        /// 读取一个布尔型数据
        /// </summary>
        /// <returns></returns>
        bool ReadBool();

        /// <summary>
        /// Read one character data, occupying two bytes and 16 bits
        /// 读取一个字符数据，占两字节16位
        /// </summary>
        /// <returns></returns>
        char ReadChar();

        /// <summary>
        /// Read an unsigned 8-bit integer
        /// 读取一个无符号8位整型数据
        /// </summary>
        /// <returns></returns>
        byte ReadUInt8();

        /// <summary>
        /// Read an unsigned 16-bit integer
        /// 读取一个无符号16位整型数据
        /// </summary>
        /// <returns></returns>
        ushort ReadUInt16();

        /// <summary>
        /// Read an unsigned 32-bit integer
        /// 读取一个无符号32位整型数据
        /// </summary>
        /// <returns></returns>
        uint ReadUInt32();

        /// <summary>
        /// Read an unsigned 64-bit integer
        /// 读取一个无符号64位整型数据
        /// </summary>
        /// <returns></returns>
        ulong ReadUInt64();

        /// <summary>
        /// Read a signed 8-bit integer
        /// 读取一个有符号8位整型数据
        /// </summary>
        /// <returns></returns>
        sbyte ReadInt8();

        /// <summary>
        /// Read a signed 16-bit integer
        /// 读取一个有符号16位整型数据
        /// </summary>
        /// <returns></returns>
        short ReadInt16();

        /// <summary>
        /// Read a signed 32-bit integer
        /// 读取一个有符号32位整型数据
        /// </summary>
        /// <returns></returns>
        int ReadInt32();

        /// <summary>
        /// Read a signed 64-bit integer
        /// 读取一个有符号64位整型数据
        /// </summary>
        /// <returns></returns>
        long ReadInt64();

        /// <summary>
        /// Read a 32-bit single-precision floating-point data
        /// 读取一个32位单精度浮点数据
        /// </summary>
        /// <returns></returns>
        float ReadFloat();

        /// <summary>
        /// Read a 64-bit double-precision floating-point data
        /// 读取一个64位双精度浮点数据
        /// </summary>
        /// <returns></returns>
        double ReadDouble();

        /// <summary>
        /// read string data
        /// 读取字符串数据
        /// </summary>
        /// <returns></returns>
        string ReadString();

        /// <summary>
        /// Read bool array
        /// 读取 bool数组
        /// </summary>
        /// <returns></returns>
        bool[] ReadBoolArray();

        /// <summary>
        /// Read bool array of specified length
        /// 读取指定长度 bool数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        bool[] ReadBoolArray(int num);

        /// <summary>
        /// Read character array
        /// 读取字符数组
        /// </summary>
        /// <returns></returns>
        char[] ReadCharArray();

        /// <summary>
        /// Read character array of specified length
        /// 读取指定长度字符数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        char[] ReadCharArray(int num);

        /// <summary>
        /// Read byte array
        /// 读取 byte数组
        /// </summary>
        /// <returns></returns>
        byte[] ReadUInt8Array();

        /// <summary>
        /// Read byte array of specified length
        /// 读取指定长度 byte数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        byte[] ReadUInt8Array(int num);

        /// <summary>
        /// Read ushort array
        /// 读取 ushort数组
        /// </summary>
        /// <returns></returns>
        ushort[] ReadUInt16Array();

        /// <summary>
        /// Read ushort array of specified length
        /// 读取指定长度 ushort数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        ushort[] ReadUInt16Array(int num);

        /// <summary>
        /// Read uint array
        /// 读取 uint数组
        /// </summary>
        /// <returns></returns>
        uint[] ReadUInt32Array();

        /// <summary>
        /// Read uint array of specified length
        /// 读取指定长度 uint数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        uint[] ReadUInt32Array(int num);

        /// <summary>
        /// Read ulong array
        /// 读取 ulong数组
        /// </summary>
        /// <returns></returns>
        ulong[] ReadUInt64Array();

        /// <summary>
        /// Read ulong array of specified length
        /// 读取指定长度 ulong数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        ulong[] ReadUInt64Array(int num);

        /// <summary>
        /// Read sbyte array
        /// 读取 sbyte数组
        /// </summary>
        /// <returns></returns>
        sbyte[] ReadInt8Array();

        /// <summary>
        /// Read sbyte array of specified length
        /// 读取指定长度 sbyte数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        sbyte[] ReadInt8Array(int num);

        /// <summary>
        /// Read short array
        /// 读取 short数组
        /// </summary>
        /// <returns></returns>
        short[] ReadInt16Array();

        /// <summary>
        /// Read short array of specified length
        /// 读取指定长度 short数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        short[] ReadInt16Array(int num);

        /// <summary>
        /// Read int array
        /// 读取 int数组
        /// </summary>
        /// <returns></returns>
        int[] ReadInt32Array();

        /// <summary>
        /// Read int array of specified length
        /// 读取指定长度 int数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        int[] ReadInt32Array(int num);

        /// <summary>
        /// Read long array
        /// 读取 long数组
        /// </summary>
        /// <returns></returns>
        long[] ReadInt64Array();

        /// <summary>
        /// Read long array of specified length
        /// 读取指定长度 long数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        long[] ReadInt64Array(int num);

        /// <summary>
        /// Read float array
        /// 读取 float数组
        /// </summary>
        /// <returns></returns>
        float[] ReadFloatArray();

        /// <summary>
        /// Read float array of specified length
        /// 读取指定长度 float数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        float[] ReadFloatArray(int num);

        /// <summary>
        /// Read double array
        /// 读取 double数组
        /// </summary>
        /// <returns></returns>
        double[] ReadDoubleArray();

        /// <summary>
        /// Read double array of specified length
        /// 读取指定长度 double数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        double[] ReadDoubleArray(int num);

        /// <summary>
        /// Read string array
        /// 读取 string数组
        /// </summary>
        /// <returns></returns>
        string[] ReadStringArray();

        /// <summary>
        /// Read string array of specified length
        /// 读取指定长度 string数组
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        string[] ReadStringArray(int num);

        /// <summary>
        /// Read basic type data or its array
        /// 读取 基础类型数据 或 其数组
        /// </summary>
        /// <param name="data"></param>
        void ReadBaseDataTo(ref object data);
    }
}