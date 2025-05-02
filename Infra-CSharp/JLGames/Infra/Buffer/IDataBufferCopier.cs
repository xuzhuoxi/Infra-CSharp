namespace JLGames.Infra.Buffer
{
    public interface IDataBufferCopier 
    {
        /// <summary>
        /// Copy length info
        /// 复制长度信息
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        int CopyLen(int offset = 0);

        /// <summary>
        /// Copy an unsigned 64-bit integer data
        /// 复制一个无符号64位整型数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        ulong CopyUInt64(int offset = 0);

        /// <summary>
        /// Copy an unsigned 32-bit integer data
        /// 复制一个无符号32位整型数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        uint CopyUInt32(int offset = 0);

        /// <summary>
        /// Copy an unsigned 16-bit integer data
        /// 复制一个无符号16位整型数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        ushort CopyUInt16(int offset = 0);

        /// <summary>
        /// Copy an unsigned 8-bit integer data
        /// 复制一个无符号8位整型数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        byte CopyUInt8(int offset = 0);

        /// <summary>
        /// Copy a signed 64-bit integer data
        /// 复制一个有符号64位整型数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        long CopyInt64(int offset = 0);

        /// <summary>
        /// Copy a signed 32-bit integer data
        /// 复制一个有符号32位整型数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        int CopyInt32(int offset = 0);

        /// <summary>
        /// Copy a signed 16-bit integer data
        /// 复制一个有符号16位整型数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        short CopyInt16(int offset = 0);

        /// <summary>
        /// Copy a signed 8-bit integer data
        /// 复制一个有符号8位整型数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        sbyte CopyInt8(int offset = 0);

        /// <summary>
        /// Copy a boolean
        /// 复制一个布尔型数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        bool CopyBool(int offset = 0);

        /// <summary>
        /// Copy a 64-bit double-precision floating-point data
        /// 复制一个64位双精度浮点数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        double CopyDouble(int offset = 0);

        /// <summary>
        /// Copy a 32-bit single-precision floating-point data
        /// 复制一个32位单精度浮点数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        float CopyFloat(int offset = 0);

        /// <summary>
        /// Copy one character data, occupying two bytes of 16 bits
        /// 复制一个字符数据，占两字节16位
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        char CopyChar(int offset = 0);

        /// <summary>
        /// Read string data, not move reader index
        /// 读取字符串数据, 不移动读下标
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        string CopyString(int offset = 0);

        // Array ----------------------

        /// <summary>
        /// Read bool array, not move reader index
        /// 读取 bool数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        bool[] CopyBoolArray();

        /// <summary>
        /// Read bool array of specified length, not move reader index
        /// 读取指定长度 bool数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        bool[] CopyBoolArray(int num, int offset = 0);

        /// <summary>
        /// Read character array, not move reader index
        /// 读取字符数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        char[] CopyCharArray();

        /// <summary>
        /// Read character array of specified length, not move reader index
        /// 读取指定长度字符数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        char[] CopyCharArray(int num, int offset = 0);

        /// <summary>
        /// Read byte array, not move reader index
        /// 读取 byte数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        byte[] CopyUInt8Array();

        /// <summary>
        /// Read byte array of specified length, not move reader index
        /// 读取指定长度 byte数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        byte[] CopyUInt8Array(int num, int offset = 0);

        /// <summary>
        /// Read ushort array, not move reader index
        /// 读取 ushort数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        ushort[] CopyUInt16Array();

        /// <summary>
        /// Read ushort array of specified length, not move reader index
        /// 读取指定长度 ushort数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        ushort[] CopyUInt16Array(int num, int offset = 0);

        /// <summary>
        /// Read uint array, not move reader index
        /// 读取 uint数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        uint[] CopyUInt32Array();

        /// <summary>
        /// Read uint array of specified length, not move reader index
        /// 读取指定长度 uint数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        uint[] CopyUInt32Array(int num, int offset = 0);

        /// <summary>
        /// Read ulong array, not move reader index
        /// 读取 ulong数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        ulong[] CopyUInt64Array();

        /// <summary>
        /// Read ulong array of specified length, not move reader index
        /// 读取指定长度 ulong数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        ulong[] CopyUInt64Array(int num, int offset = 0);

        /// <summary>
        /// Read sbyte array, not move reader index
        /// 读取 sbyte数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        sbyte[] CopyInt8Array();

        /// <summary>
        /// Read sbyte array of specified length, not move reader index
        /// 读取指定长度 sbyte数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        sbyte[] CopyInt8Array(int num, int offset = 0);

        /// <summary>
        /// Read short array, not move reader index
        /// 读取 short数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        short[] CopyInt16Array();

        /// <summary>
        /// Read short array of specified length, not move reader index
        /// 读取指定长度 short数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        short[] CopyInt16Array(int num, int offset = 0);

        /// <summary>
        /// Read int array, not move reader index
        /// 读取 int数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        int[] CopyInt32Array();

        /// <summary>
        /// Read int array of specified length, not move reader index
        /// 读取指定长度 int数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        int[] CopyInt32Array(int num, int offset = 0);

        /// <summary>
        /// Read long array, not move reader index
        /// 读取 long数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        long[] CopyInt64Array();

        /// <summary>
        /// Read long array of specified length, not move reader index
        /// 读取指定长度 long数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        long[] CopyInt64Array(int num, int offset = 0);

        /// <summary>
        /// Read float array, not move reader index
        /// 读取 float数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        float[] CopyFloatArray();

        /// <summary>
        /// Read float array of specified length, not move reader index
        /// 读取指定长度 float数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        float[] CopyFloatArray(int num, int offset = 0);

        /// <summary>
        /// Read double array, not move reader index
        /// 读取 double数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        double[] CopyDoubleArray();

        /// <summary>
        /// Read double array of specified length, not move reader index
        /// 读取指定长度 double数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        double[] CopyDoubleArray(int num, int offset = 0);

        /// <summary>
        /// Read string array, not move reader index
        /// 读取 string数组, 不移动读下标
        /// </summary>
        /// <returns></returns>
        string[] CopyStringArray();

        /// <summary>
        /// Read string array of specified length, not move reader index
        /// 读取指定长度 string数组, 不移动读下标
        /// </summary>
        /// <param name="num"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        string[] CopyStringArray(int num, int offset = 0);

        /// <summary>
        /// Read basic type data or its array, not move reader index
        /// 读取 基础类型数据 或 其数组, 不移动读下标
        /// </summary>
        /// <param name="data"></param>
        void CopyBaseDataTo(ref object data);
    }
}