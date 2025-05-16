namespace JLGames.Infra.Buffer
{
    public static class BinarySize
    {
        /// <summary>
        /// 长度数据占用字节数
        /// 占用字节: 2; 位数: 16
        /// 说明: 用于表示长度，为 2 字节
        /// </summary>
        public const int LenSize = sizeof(ushort);

        /// <summary>
        /// C# 类型: bool, System.Boolean
        /// 占用字节: 1; 位数:  8
        /// 说明: 通常表示 true/false，实际大小依赖实现，通常为 1 字节
        /// </summary>
        public const int BoolSize = sizeof(bool);


        /// <summary>
        /// C# 类型: byte, System.Byte
        /// 占用字节: 1; 位数:  8
        /// 说明: 8 位无符号整数
        /// </summary>
        public const int ByteSize = sizeof(byte);

        /// <summary>
        /// C# 类型: sbyte, System.SByte
        /// 占用字节: 1; 位数:  8
        /// 说明: 8 位有符号整数
        /// </summary>
        public const int SByteSize = sizeof(sbyte);

        /// <summary>
        /// C# 类型: short, System.Int16
        /// 占用字节: 2; 位数: 16
        /// 说明: 16 位有符号整数
        /// </summary>
        public const int ShortSize = sizeof(short);

        /// <summary>
        /// C# 类型: ushort, System.UInt16
        /// 占用字节: 2; 位数: 16
        /// 说明: 16 位无符号整数
        /// </summary>
        public const int UShortSize = sizeof(ushort);

        /// <summary>
        /// C# 类型: int, System.Int32
        /// 占用字节: 4; 位数: 32
        /// 说明: 32 位有符号整数
        /// </summary>
        public const int IntSize = sizeof(int);

        /// <summary>
        /// C# 类型: uint, System.UInt32
        /// 占用字节: 4; 位数: 32
        /// 说明: 32 位无符号整数
        /// </summary>
        public const int UIntSize = sizeof(uint);

        /// <summary>
        /// C# 类型: long, System.Int64
        /// 占用字节: 8; 位数: 64
        /// 说明: 64 位有符号整数
        /// </summary>
        public const int LongSize = sizeof(long);

        /// <summary>
        /// C# 类型: ulong, System.UInt64
        /// 占用字节: 8; 位数: 64
        /// 说明: 64 位无符号整数
        /// </summary>
        public const int ULongSize = sizeof(ulong);

        /// <summary>
        /// C# 类型: char, System.Char
        /// 占用字节: 2; 位数: 16
        /// 说明: 通常表示字符
        /// </summary>
        public const int CharSize = sizeof(char);

        /// <summary>
        /// C# 类型: float, System.Single
        /// 占用字节: 4; 位数: 32
        /// 说明: 32 位单精度浮点数
        /// </summary>
        public const int FloatSize = sizeof(float);

        /// <summary>
        /// C# 类型: double, System.Double
        /// 占用字节: 8; 位数: 64
        /// 说明: 64 位双精度浮点数
        /// </summary>
        public const int DoubleSize = sizeof(double);

        /// <summary>
        /// C# 类型: decimal, System.Decimal
        /// 占用字节: 16; 位数: 128
        /// 说明: 128 位高精度浮点数, 常用于金融
        /// </summary>
        public const int DecimalSize = sizeof(decimal);
    }
}