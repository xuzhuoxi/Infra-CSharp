namespace JLGames.Infra.Buffer
{
    public enum ValueKind
    {
        KindNone = (byte)0,
        KindBool,
        KindInt8,
        KindInt16,
        KindInt32,
        KindInt64,
        KindUint8,
        KindUint16,
        KindUint32,
        KindUint64,
        KindFloat32,
        KindFloat64,
        KindComplex64,
        KindComplex128,
        KindInt,
        KindUint,
        KindString,

        KindSliceNone = (byte)128,
        KindSliceBool,
        KindSliceInt8,
        KindSliceInt16,
        KindSliceInt32,
        KindSliceInt64,
        KindSliceUint8,
        KindSliceUint16,
        KindSliceUint32,
        KindSliceUint64,
        KindSliceFloat32,
        KindSliceFloat64,
        KindSliceComplex64,
        KindSliceComplex128,
        KindSliceInt,
        KindSliceUint,
        KindSliceString
    }
}