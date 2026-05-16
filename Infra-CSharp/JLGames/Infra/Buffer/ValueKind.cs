namespace JLGames.Infra.Buffer
{
    /// <summary>
    /// Type tag for serializable primitive values and their arrays.
    /// 可序列化基础类型及其数组的类型标记
    /// </summary>
    public enum ValueKind
    {
        /// <summary>Invalid / unsupported; 无效或不支持</summary>
        KindNone = (byte)0,
        /// <summary>bool; 布尔</summary>
        KindBool,
        /// <summary>sbyte; 有符号 8 位整数</summary>
        KindInt8,
        /// <summary>short; 有符号 16 位整数</summary>
        KindInt16,
        /// <summary>int; 有符号 32 位整数</summary>
        KindInt32,
        /// <summary>long; 有符号 64 位整数</summary>
        KindInt64,
        /// <summary>byte; 无符号 8 位整数</summary>
        KindUint8,
        /// <summary>ushort; 无符号 16 位整数</summary>
        KindUint16,
        /// <summary>uint; 无符号 32 位整数</summary>
        KindUint32,
        /// <summary>ulong; 无符号 64 位整数</summary>
        KindUint64,
        /// <summary>float; 单精度浮点</summary>
        KindFloat32,
        /// <summary>double; 双精度浮点</summary>
        KindFloat64,
        /// <summary>complex64; 64位复数</summary>
        KindComplex64,
        /// <summary>complex128; 128位复数</summary>
        KindComplex128,
        /// <summary>int; 有符号整数</summary>
        KindInt,
        /// <summary>uint; 无符号整数</summary>
        KindUint,
        /// <summary>string; 字符串</summary>
        KindString,

        /// <summary>Invalid array kind; 无效数组类型</summary>
        KindSliceNone = (byte)128,
        /// <summary>bool[]; 布尔数组</summary>
        KindSliceBool,
        /// <summary>sbyte[]; 有符号 8 位整数数组</summary>
        KindSliceInt8,
        /// <summary>short[]; 有符号 16 位整数数组</summary>
        KindSliceInt16,
        /// <summary>int[]; 有符号 32 位整数数组</summary>
        KindSliceInt32,
        /// <summary>long[]; 有符号 64 位整数数组</summary>
        KindSliceInt64,
        /// <summary>byte[]; 无符号 8 位整数数组</summary>
        KindSliceUint8,
        /// <summary>ushort[]; 无符号 16 位整数数组</summary>
        KindSliceUint16,
        /// <summary>uint[]; 无符号 32 位整数数组</summary>
        KindSliceUint32,
        /// <summary>ulong[]; 无符号 64 位整数数组</summary>
        KindSliceUint64,
        /// <summary>float[]; 单精度浮点数组</summary>
        KindSliceFloat32,
        /// <summary>double[]; 双精度浮点数组</summary>
        KindSliceFloat64,
        /// <summary>complex64[]; 64位复数数组</summary>
        KindSliceComplex64,
        /// <summary>complex128[]; 128位复数数组</summary>
        KindSliceComplex128,
        /// <summary>int[]; 有符号整数数组</summary>
        KindSliceInt,
        /// <summary>uint[]; 无符号整数数组</summary>
        KindSliceUint,
        /// <summary>string[]; 字符串数组</summary>
        KindSliceString
    }
}
