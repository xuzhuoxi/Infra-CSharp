namespace JLGames.Infra.Buffer
{
    public static class ValueKindUtil
    {
        /// <summary>
        /// 判断是否为不可用的类型
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static bool IsForbidKind(ValueKind kind)
        {
            return kind == ValueKind.KindNone || kind == ValueKind.KindSliceNone;
        }

        /// <summary>
        /// 判断是否为简单类型
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static bool IsSimpleKind(ValueKind kind)
        {
            return kind > ValueKind.KindNone && kind < ValueKind.KindSliceNone;
        }

        /// <summary>
        /// 判断是否为简单数组类型
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static bool IsArrayKind(ValueKind kind)
        {
            return kind > ValueKind.KindSliceNone;
        }

        /// <summary>
        /// 取值对应的定义
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ValueKind GetValueKind(object value)
        {
            if (value is bool)
                return ValueKind.KindBool;
            if (value is sbyte)
                return ValueKind.KindInt8;
            if (value is byte)
                return ValueKind.KindUint8;
            if (value is short)
                return ValueKind.KindInt16;
            if (value is ushort)
                return ValueKind.KindUint16;
            if (value is int)
                return ValueKind.KindInt32;
            if (value is uint)
                return ValueKind.KindUint32;
            if (value is long)
                return ValueKind.KindInt64;
            if (value is ulong)
                return ValueKind.KindUint64;
            if (value is float)
                return ValueKind.KindFloat32;
            if (value is double)
                return ValueKind.KindFloat64;
            if (value is string)
                return ValueKind.KindString;

            if (value is bool[])
                return ValueKind.KindSliceBool;
            if (value is sbyte[])
                return ValueKind.KindSliceInt8;
            if (value is byte[])
                return ValueKind.KindSliceUint8;
            if (value is short[])
                return ValueKind.KindSliceInt16;
            if (value is ushort[])
                return ValueKind.KindSliceUint16;
            if (value is int[])
                return ValueKind.KindSliceInt32;
            if (value is uint[])
                return ValueKind.KindSliceUint32;
            if (value is long[])
                return ValueKind.KindSliceInt64;
            if (value is ulong[])
                return ValueKind.KindSliceUint64;
            if (value is float[])
                return ValueKind.KindSliceFloat32;
            if (value is double[])
                return ValueKind.KindSliceFloat64;
            if (value is string[])
                return ValueKind.KindSliceString;

            return ValueKind.KindNone;
        }

        public static object GetKindValue(ValueKind kind, int arrayLen)
        {
            switch (kind)
            {
                case ValueKind.KindBool:
                    return default(bool);
                case ValueKind.KindInt8:
                    return default(sbyte);
                case ValueKind.KindUint8:
                    return default(byte);
                case ValueKind.KindInt16:
                    return default(short);
                case ValueKind.KindUint16:
                    return default(ushort);
                case ValueKind.KindInt32:
                    return default(int);
                case ValueKind.KindUint32:
                    return default(uint);
                case ValueKind.KindInt64:
                    return default(long);
                case ValueKind.KindUint64:
                    return default(ulong);
                case ValueKind.KindFloat32:
                    return default(float);
                case ValueKind.KindFloat64:
                    return default(double);
                case ValueKind.KindString:
                    return "";

                case ValueKind.KindSliceBool:
                    return new bool[arrayLen];
                case ValueKind.KindSliceInt8:
                    return new sbyte[arrayLen];
                case ValueKind.KindSliceUint8:
                    return new byte[arrayLen];
                case ValueKind.KindSliceInt16:
                    return new short[arrayLen];
                case ValueKind.KindSliceUint16:
                    return new ushort[arrayLen];
                case ValueKind.KindSliceInt32:
                    return new int[arrayLen];
                case ValueKind.KindSliceUint32:
                    return new uint[arrayLen];
                case ValueKind.KindSliceInt64:
                    return new long[arrayLen];
                case ValueKind.KindSliceUint64:
                    return new ulong[arrayLen];
                case ValueKind.KindSliceFloat32:
                    return new float[arrayLen];
                case ValueKind.KindSliceFloat64:
                    return new double[arrayLen];
                case ValueKind.KindSliceString:
                    return new string[arrayLen];
            }

            return null;
        }
    }
}