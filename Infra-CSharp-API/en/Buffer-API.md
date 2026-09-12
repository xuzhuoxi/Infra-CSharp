# Buffer API Documentation

## Namespace: JLGames.Infra.Buffer

### Interfaces

#### IByteBuffer
Byte buffer with read, write, and peek (copy) capabilities. Inherits from `IByteBufferReader`, `IByteBufferWriter`, and `IByteBufferCopier`.

```csharp
public interface IByteBuffer : IByteBufferReader, IByteBufferWriter, IByteBufferCopier
{
    /// <summary>
    /// Total capacity
    /// </summary>
    int Cap { get; }

    /// <summary>
    /// Clean up records
    /// </summary>
    void Clear();
}
```

#### IByteBufferReader
Byte buffer reader.

```csharp
public interface IByteBufferReader
{
    /// <summary>
    /// Current unread length
    /// </summary>
    int Len { get; }

    /// <summary>
    /// Reading index position
    /// </summary>
    int ReadPosition { get; }

    /// <summary>
    /// Set reading index position
    /// </summary>
    /// <param name="pos"></param>
    void SetReadPosition(int pos);

    /// <summary>
    /// Read a byte data
    /// </summary>
    /// <returns></returns>
    byte ReadByte();

    /// <summary>
    /// Read all unread data
    /// </summary>
    /// <returns></returns>
    byte[] ReadBytes();

    /// <summary>
    /// Read data of specified length
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    byte[] ReadBytes(int size);

    /// <summary>
    /// Read data of specified length and write it into the target array
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    int ReadBytesTo(ref byte[] dst);

    /// <summary>
    /// Read data of specified length and write it into the target array
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    int ReadBytesTo(ref byte[] dst, int size);
}
```

#### IByteBufferWriter
Byte buffer writer.

```csharp
public interface IByteBufferWriter
{
    /// <summary>
    /// Writing index position
    /// </summary>
    int WritePosition { get; }

    /// <summary>
    /// Set writing index position
    /// </summary>
    /// <param name="pos"></param>
    void SetWritePosition(int pos);

    /// <summary>
    /// Write binary 0 value
    /// </summary>
    /// <param name="size"></param>
    void WriteZero(int size);

    /// <summary>
    /// Write a byte
    /// </summary>
    /// <param name="b"></param>
    void Write(byte b);

    /// <summary>
    /// Write byte array
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">the start index of bytes</param>
    /// <param name="size"></param>
    void Write(byte[] bytes, int startIndex, int size);

    /// <summary>
    /// Write byte array
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">the start index of bytes</param>
    void Write(byte[] bytes, int startIndex);

    /// <summary>
    /// Write byte array
    /// </summary>
    /// <param name="bytes"></param>
    void Write(byte[] bytes);
}
```

#### IByteBufferCopier
Byte buffer copier; reads without advancing the read index.

```csharp
public interface IByteBufferCopier
{
    /// <summary>
    /// Copy a byte without advancing the read index.
    /// </summary>
    /// <returns></returns>
    byte CopyByte();

    /// <summary>
    /// Copy all unread data starting with offset subscripts
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    byte[] CopyBytes(int offset = 0);

    /// <summary>
    /// Copy unread data of specified length
    /// </summary>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    byte[] CopyBytes(int size, int offset);

    /// <summary>
    /// Copy the specified length of data and write it into the target array
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    int CopyBytesTo(ref byte[] dst, int offset = 0);

    /// <summary>
    /// Copy the specified length of data and write it into the target array
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    int CopyBytesTo(ref byte[] dst, int size, int offset);
}
```

#### IDataBuffer
Typed data buffer supporting endian-aware serialization of primitive types. Inherits from `IByteBuffer`, `IDataBufferWriter`, `IDataBufferReader`, and `IDataBufferCopier`.

```csharp
public interface IDataBuffer : IByteBuffer, IDataBufferWriter, IDataBufferReader, IDataBufferCopier
{
    /// <summary>
    /// Endian Coverter
    /// </summary>
    EndianCoverter EndianCoverter { get; }
}
```

#### IDataBufferReader
Typed data buffer reader. Parameterless array reads consume a length prefix first; overloads with `num` read that many elements.

```csharp
public interface IDataBufferReader
{
    /// <summary>
    /// Read length info
    /// </summary>
    int ReadLen();

    /// <summary>
    /// Read a boolean
    /// </summary>
    /// <returns></returns>
    bool ReadBool();

    /// <summary>
    /// Read one character data, occupying two bytes and 16 bits
    /// </summary>
    /// <returns></returns>
    char ReadChar();

    /// <summary>
    /// Read an unsigned 8-bit integer
    /// </summary>
    /// <returns></returns>
    byte ReadUInt8();

    /// <summary>
    /// Read an unsigned 16-bit integer
    /// </summary>
    /// <returns></returns>
    ushort ReadUInt16();

    /// <summary>
    /// Read an unsigned 32-bit integer
    /// </summary>
    /// <returns></returns>
    uint ReadUInt32();

    /// <summary>
    /// Read an unsigned 64-bit integer
    /// </summary>
    /// <returns></returns>
    ulong ReadUInt64();

    /// <summary>
    /// Read a signed 8-bit integer
    /// </summary>
    /// <returns></returns>
    sbyte ReadInt8();

    /// <summary>
    /// Read a signed 16-bit integer
    /// </summary>
    /// <returns></returns>
    short ReadInt16();

    /// <summary>
    /// Read a signed 32-bit integer
    /// </summary>
    /// <returns></returns>
    int ReadInt32();

    /// <summary>
    /// Read a signed 64-bit integer
    /// </summary>
    /// <returns></returns>
    long ReadInt64();

    /// <summary>
    /// Read a 32-bit single-precision floating-point data
    /// </summary>
    /// <returns></returns>
    float ReadFloat();

    /// <summary>
    /// Read a 64-bit double-precision floating-point data
    /// </summary>
    /// <returns></returns>
    double ReadDouble();

    /// <summary>
    /// Read string data
    /// </summary>
    /// <returns></returns>
    string ReadString();

    /// <summary>
    /// Read bool array
    /// </summary>
    /// <returns></returns>
    bool[] ReadBoolArray();

    /// <summary>
    /// Read bool array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    bool[] ReadBoolArray(int num);

    /// <summary>
    /// Read character array
    /// </summary>
    /// <returns></returns>
    char[] ReadCharArray();

    /// <summary>
    /// Read character array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    char[] ReadCharArray(int num);

    /// <summary>
    /// Read byte array
    /// </summary>
    /// <returns></returns>
    byte[] ReadUInt8Array();

    /// <summary>
    /// Read byte array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    byte[] ReadUInt8Array(int num);

    /// <summary>
    /// Read ushort array
    /// </summary>
    /// <returns></returns>
    ushort[] ReadUInt16Array();

    /// <summary>
    /// Read ushort array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    ushort[] ReadUInt16Array(int num);

    /// <summary>
    /// Read uint array
    /// </summary>
    /// <returns></returns>
    uint[] ReadUInt32Array();

    /// <summary>
    /// Read uint array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    uint[] ReadUInt32Array(int num);

    /// <summary>
    /// Read ulong array
    /// </summary>
    /// <returns></returns>
    ulong[] ReadUInt64Array();

    /// <summary>
    /// Read ulong array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    ulong[] ReadUInt64Array(int num);

    /// <summary>
    /// Read sbyte array
    /// </summary>
    /// <returns></returns>
    sbyte[] ReadInt8Array();

    /// <summary>
    /// Read sbyte array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    sbyte[] ReadInt8Array(int num);

    /// <summary>
    /// Read short array
    /// </summary>
    /// <returns></returns>
    short[] ReadInt16Array();

    /// <summary>
    /// Read short array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    short[] ReadInt16Array(int num);

    /// <summary>
    /// Read int array
    /// </summary>
    /// <returns></returns>
    int[] ReadInt32Array();

    /// <summary>
    /// Read int array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    int[] ReadInt32Array(int num);

    /// <summary>
    /// Read long array
    /// </summary>
    /// <returns></returns>
    long[] ReadInt64Array();

    /// <summary>
    /// Read long array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    long[] ReadInt64Array(int num);

    /// <summary>
    /// Read float array
    /// </summary>
    /// <returns></returns>
    float[] ReadFloatArray();

    /// <summary>
    /// Read float array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    float[] ReadFloatArray(int num);

    /// <summary>
    /// Read double array
    /// </summary>
    /// <returns></returns>
    double[] ReadDoubleArray();

    /// <summary>
    /// Read double array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    double[] ReadDoubleArray(int num);

    /// <summary>
    /// Read string array
    /// </summary>
    /// <returns></returns>
    string[] ReadStringArray();

    /// <summary>
    /// Read string array of specified length
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    string[] ReadStringArray(int num);

    /// <summary>
    /// Read basic type data or its array
    /// </summary>
    /// <param name="data"></param>
    void ReadBaseDataTo(ref object data);
}
```

#### IDataBufferWriter
Typed data buffer writer. String and array writes include length information.

```csharp
public interface IDataBufferWriter
{
    /// <summary>
    /// Write length info
    /// </summary>
    /// <param name="len"></param>
    void WriteLen(int len);

    /// <summary>
    /// Write a boolean
    /// </summary>
    /// <param name="data"></param>
    void WriteData(bool data);

    /// <summary>
    /// Write in one character data, occupying two bytes and 16 bits
    /// </summary>
    /// <param name="data"></param>
    void WriteData(char data);

    /// <summary>
    /// Write in an unsigned 8-bit integer
    /// </summary>
    /// <param name="data"></param>
    void WriteData(byte data);

    /// <summary>
    /// Write in an unsigned 16-bit integer
    /// </summary>
    /// <param name="data"></param>
    void WriteData(ushort data);

    /// <summary>
    /// Write in an unsigned 32-bit integer
    /// </summary>
    /// <param name="data"></param>
    void WriteData(uint data);

    /// <summary>
    /// Write in an unsigned 64-bit integer
    /// </summary>
    /// <param name="data"></param>
    void WriteData(ulong data);

    /// <summary>
    /// Write in a signed 8-bit integer
    /// </summary>
    /// <param name="data"></param>
    void WriteData(sbyte data);

    /// <summary>
    /// Write in a signed 16-bit integer
    /// </summary>
    /// <param name="data"></param>
    void WriteData(short data);

    /// <summary>
    /// Write in a signed 32-bit integer
    /// </summary>
    /// <param name="data"></param>
    void WriteData(int data);

    /// <summary>
    /// Write in a signed 64-bit integer
    /// </summary>
    /// <param name="data"></param>
    void WriteData(long data);

    /// <summary>
    /// Write in a 32-bit single-precision floating-point data
    /// </summary>
    /// <param name="data"></param>
    void WriteData(float data);

    /// <summary>
    /// Write in a 64-bit double-precision floating-point data
    /// </summary>
    /// <param name="data"></param>
    void WriteData(double data);

    /// <summary>
    /// Write in string data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(string data);

    /// <summary>
    /// Write in bool array
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(bool[] data);

    /// <summary>
    /// Write in character array
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(char[] data);

    /// <summary>
    /// Write in byte array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(byte[] data);

    /// <summary>
    /// Write in ushort array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(ushort[] data);

    /// <summary>
    /// Write in uint array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(uint[] data);

    /// <summary>
    /// Write in ulong array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(ulong[] data);

    /// <summary>
    /// Write in sbyte array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(sbyte[] data);

    /// <summary>
    /// Write in short array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(short[] data);

    /// <summary>
    /// Write in int array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(int[] data);

    /// <summary>
    /// Write in long array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(long[] data);

    /// <summary>
    /// Write in float array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(float[] data);

    /// <summary>
    /// Write in double array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(double[] data);

    /// <summary>
    /// Write in string array data
    /// Note: Length information will be written
    /// </summary>
    /// <param name="data"></param>
    void WriteData(string[] data);

    /// <summary>
    /// Write basic type data or its array
    /// </summary>
    /// <param name="data"></param>
    void WriteBaseData(object data);
}
```

#### IDataBufferCopier
Typed data buffer copier; reads without advancing the read index. Parameterless array copies peek the length prefix first; overloads with `num` copy that many elements.

```csharp
public interface IDataBufferCopier
{
    /// <summary>
    /// Copy length info
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    int CopyLen(int offset = 0);

    /// <summary>
    /// Copy an unsigned 64-bit integer data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    ulong CopyUInt64(int offset = 0);

    /// <summary>
    /// Copy an unsigned 32-bit integer data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    uint CopyUInt32(int offset = 0);

    /// <summary>
    /// Copy an unsigned 16-bit integer data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    ushort CopyUInt16(int offset = 0);

    /// <summary>
    /// Copy an unsigned 8-bit integer data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    byte CopyUInt8(int offset = 0);

    /// <summary>
    /// Copy a signed 64-bit integer data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    long CopyInt64(int offset = 0);

    /// <summary>
    /// Copy a signed 32-bit integer data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    int CopyInt32(int offset = 0);

    /// <summary>
    /// Copy a signed 16-bit integer data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    short CopyInt16(int offset = 0);

    /// <summary>
    /// Copy a signed 8-bit integer data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    sbyte CopyInt8(int offset = 0);

    /// <summary>
    /// Copy a boolean
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    bool CopyBool(int offset = 0);

    /// <summary>
    /// Copy a 64-bit double-precision floating-point data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    double CopyDouble(int offset = 0);

    /// <summary>
    /// Copy a 32-bit single-precision floating-point data
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    float CopyFloat(int offset = 0);

    /// <summary>
    /// Copy one character data, occupying two bytes of 16 bits
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    char CopyChar(int offset = 0);

    /// <summary>
    /// Read string data, not move reader index
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    string CopyString(int offset = 0);

    /// <summary>
    /// Read bool array, not move reader index
    /// </summary>
    /// <returns></returns>
    bool[] CopyBoolArray();

    /// <summary>
    /// Read bool array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    bool[] CopyBoolArray(int num, int offset = 0);

    /// <summary>
    /// Read character array, not move reader index
    /// </summary>
    /// <returns></returns>
    char[] CopyCharArray();

    /// <summary>
    /// Read character array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    char[] CopyCharArray(int num, int offset = 0);

    /// <summary>
    /// Read byte array, not move reader index
    /// </summary>
    /// <returns></returns>
    byte[] CopyUInt8Array();

    /// <summary>
    /// Read byte array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    byte[] CopyUInt8Array(int num, int offset = 0);

    /// <summary>
    /// Read ushort array, not move reader index
    /// </summary>
    /// <returns></returns>
    ushort[] CopyUInt16Array();

    /// <summary>
    /// Read ushort array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    ushort[] CopyUInt16Array(int num, int offset = 0);

    /// <summary>
    /// Read uint array, not move reader index
    /// </summary>
    /// <returns></returns>
    uint[] CopyUInt32Array();

    /// <summary>
    /// Read uint array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    uint[] CopyUInt32Array(int num, int offset = 0);

    /// <summary>
    /// Read ulong array, not move reader index
    /// </summary>
    /// <returns></returns>
    ulong[] CopyUInt64Array();

    /// <summary>
    /// Read ulong array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    ulong[] CopyUInt64Array(int num, int offset = 0);

    /// <summary>
    /// Read sbyte array, not move reader index
    /// </summary>
    /// <returns></returns>
    sbyte[] CopyInt8Array();

    /// <summary>
    /// Read sbyte array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    sbyte[] CopyInt8Array(int num, int offset = 0);

    /// <summary>
    /// Read short array, not move reader index
    /// </summary>
    /// <returns></returns>
    short[] CopyInt16Array();

    /// <summary>
    /// Read short array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    short[] CopyInt16Array(int num, int offset = 0);

    /// <summary>
    /// Read int array, not move reader index
    /// </summary>
    /// <returns></returns>
    int[] CopyInt32Array();

    /// <summary>
    /// Read int array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    int[] CopyInt32Array(int num, int offset = 0);

    /// <summary>
    /// Read long array, not move reader index
    /// </summary>
    /// <returns></returns>
    long[] CopyInt64Array();

    /// <summary>
    /// Read long array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    long[] CopyInt64Array(int num, int offset = 0);

    /// <summary>
    /// Read float array, not move reader index
    /// </summary>
    /// <returns></returns>
    float[] CopyFloatArray();

    /// <summary>
    /// Read float array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    float[] CopyFloatArray(int num, int offset = 0);

    /// <summary>
    /// Read double array, not move reader index
    /// </summary>
    /// <returns></returns>
    double[] CopyDoubleArray();

    /// <summary>
    /// Read double array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    double[] CopyDoubleArray(int num, int offset = 0);

    /// <summary>
    /// Read string array, not move reader index
    /// </summary>
    /// <returns></returns>
    string[] CopyStringArray();

    /// <summary>
    /// Read string array of specified length, not move reader index
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    string[] CopyStringArray(int num, int offset = 0);

    /// <summary>
    /// Read basic type data or its array, not move reader index
    /// </summary>
    /// <param name="data"></param>
    void CopyBaseDataTo(ref object data);
}
```

### Enums

#### ValueKind
Type tag for serializable primitive values and their arrays.

```csharp
public enum ValueKind
{
    /// <summary>Invalid / unsupported</summary>
    KindNone = (byte)0,
    /// <summary>bool</summary>
    KindBool,
    /// <summary>sbyte</summary>
    KindInt8,
    /// <summary>short</summary>
    KindInt16,
    /// <summary>int</summary>
    KindInt32,
    /// <summary>long</summary>
    KindInt64,
    /// <summary>byte</summary>
    KindUint8,
    /// <summary>ushort</summary>
    KindUint16,
    /// <summary>uint</summary>
    KindUint32,
    /// <summary>ulong</summary>
    KindUint64,
    /// <summary>float</summary>
    KindFloat32,
    /// <summary>double</summary>
    KindFloat64,
    /// <summary>complex64</summary>
    KindComplex64,
    /// <summary>complex128</summary>
    KindComplex128,
    /// <summary>int</summary>
    KindInt,
    /// <summary>uint</summary>
    KindUint,
    /// <summary>string</summary>
    KindString,

    /// <summary>Invalid array kind</summary>
    KindSliceNone = (byte)128,
    /// <summary>bool[]</summary>
    KindSliceBool,
    /// <summary>sbyte[]</summary>
    KindSliceInt8,
    /// <summary>short[]</summary>
    KindSliceInt16,
    /// <summary>int[]</summary>
    KindSliceInt32,
    /// <summary>long[]</summary>
    KindSliceInt64,
    /// <summary>byte[]</summary>
    KindSliceUint8,
    /// <summary>ushort[]</summary>
    KindSliceUint16,
    /// <summary>uint[]</summary>
    KindSliceUint32,
    /// <summary>ulong[]</summary>
    KindSliceUint64,
    /// <summary>float[]</summary>
    KindSliceFloat32,
    /// <summary>double[]</summary>
    KindSliceFloat64,
    /// <summary>complex64[]</summary>
    KindSliceComplex64,
    /// <summary>complex128[]</summary>
    KindSliceComplex128,
    /// <summary>int[]</summary>
    KindSliceInt,
    /// <summary>uint[]</summary>
    KindSliceUint,
    /// <summary>string[]</summary>
    KindSliceString
}
```

### Delegates

#### CodingList.FuncEach / CodingMap.FuncEach
Iteration function nested in `CodingList` and `CodingMap`.

```csharp
/// <summary>
/// iteration function
/// </summary>
/// <param name="key">Key</param>
/// <param name="value">Value</param>
public delegate void FuncEach(string key, object value);
```

### Classes

#### BinarySize
Binary size constants for primitive types used in serialization.

```csharp
public static class BinarySize
{
    /// <summary>
    /// Size of length prefix (ushort).
    /// Occupies 2 bytes / 16 bits.
    /// Used to represent length.
    /// </summary>
    public const int LenSize = sizeof(ushort);

    /// <summary>
    /// Size of bool (System.Boolean).
    /// Occupies 1 byte / 8 bits.
    /// Typically true/false; actual size is implementation-defined, usually 1 byte.
    /// </summary>
    public const int BoolSize = sizeof(bool);

    /// <summary>
    /// C# type: byte, System.Byte
    /// Occupies 1 byte / 8 bits.
    /// 8-bit unsigned integer
    /// </summary>
    public const int ByteSize = sizeof(byte);

    /// <summary>
    /// C# type: sbyte, System.SByte
    /// Occupies 1 byte / 8 bits.
    /// 8-bit signed integer
    /// </summary>
    public const int SByteSize = sizeof(sbyte);

    /// <summary>
    /// C# type: short, System.Int16
    /// Occupies 2 bytes / 16 bits.
    /// 16-bit signed integer
    /// </summary>
    public const int ShortSize = sizeof(short);

    /// <summary>
    /// C# type: ushort, System.UInt16
    /// Occupies 2 bytes / 16 bits.
    /// 16-bit unsigned integer
    /// </summary>
    public const int UShortSize = sizeof(ushort);

    /// <summary>
    /// C# type: int, System.Int32
    /// Occupies 4 bytes / 32 bits.
    /// 32-bit signed integer
    /// </summary>
    public const int IntSize = sizeof(int);

    /// <summary>
    /// C# type: uint, System.UInt32
    /// Occupies 4 bytes / 32 bits.
    /// 32-bit unsigned integer
    /// </summary>
    public const int UIntSize = sizeof(uint);

    /// <summary>
    /// C# type: long, System.Int64
    /// Occupies 8 bytes / 64 bits.
    /// 64-bit signed integer
    /// </summary>
    public const int LongSize = sizeof(long);

    /// <summary>
    /// C# type: ulong, System.UInt64
    /// Occupies 8 bytes / 64 bits.
    /// 64-bit unsigned integer
    /// </summary>
    public const int ULongSize = sizeof(ulong);

    /// <summary>
    /// C# type: char, System.Char
    /// Occupies 2 bytes / 16 bits.
    /// Typically represents a character
    /// </summary>
    public const int CharSize = sizeof(char);

    /// <summary>
    /// C# type: float, System.Single
    /// Occupies 4 bytes / 32 bits.
    /// 32-bit single-precision floating-point
    /// </summary>
    public const int FloatSize = sizeof(float);

    /// <summary>
    /// C# type: double, System.Double
    /// Occupies 8 bytes / 64 bits.
    /// 64-bit double-precision floating-point
    /// </summary>
    public const int DoubleSize = sizeof(double);

    /// <summary>
    /// C# type: decimal, System.Decimal
    /// Occupies 16 bytes / 128 bits.
    /// 128-bit high-precision decimal, often used in finance
    /// </summary>
    public const int DecimalSize = sizeof(decimal);
}
```

#### EndianCoverter
Endian converter. The public type name is `EndianCoverter` (as spelled in source).

```csharp
public sealed class EndianCoverter
{
    /// <summary>
    /// Little Endian
    /// </summary>
    public static readonly EndianCoverter LittleEndianCoverter;

    /// <summary>
    /// Big Endian
    /// </summary>
    public static readonly EndianCoverter BigEndianCoverter;

    /// <summary>
    /// Get Endian Coverter
    /// </summary>
    /// <param name="littleEndian"></param>
    /// <returns></returns>
    public static EndianCoverter GetEndianCoverter(bool littleEndian);

    /// <summary>
    /// Whether this converter uses little-endian byte order.
    /// </summary>
    public bool IsLittleEndian { get; }

    /// <summary>
    /// Create a converter with UTF-8 string encoding.
    /// </summary>
    /// <param name="littleEndian">True for little-endian</param>
    public EndianCoverter(bool littleEndian);

    /// <summary>
    /// Create a converter with the specified string encoding.
    /// </summary>
    /// <param name="littleEndian">True for little-endian</param>
    /// <param name="encoding">String encoding</param>
    public EndianCoverter(bool littleEndian, Encoding encoding);

    /// <summary>
    /// Read ulong from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public ulong ToUInt64(byte[] bytes, int startIndex);

    /// <summary>
    /// Read uint from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public uint ToUInt32(byte[] bytes, int startIndex);

    /// <summary>
    /// Read ushort from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public ushort ToUInt16(byte[] bytes, int startIndex);

    /// <summary>
    /// Read byte from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public byte ToUInt8(byte[] bytes, int startIndex);

    /// <summary>
    /// Read long from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public long ToInt64(byte[] bytes, int startIndex);

    /// <summary>
    /// Read int from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public int ToInt32(byte[] bytes, int startIndex);

    /// <summary>
    /// Read short from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public short ToInt16(byte[] bytes, int startIndex);

    /// <summary>
    /// Read sbyte from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public sbyte ToInt8(byte[] bytes, int startIndex);

    /// <summary>
    /// Read double from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public double ToDouble(byte[] bytes, int startIndex);

    /// <summary>
    /// Read float from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public float ToFloat(byte[] bytes, int startIndex);

    /// <summary>
    /// Read char from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public char ToChar(byte[] bytes, int startIndex);

    /// <summary>
    /// Read bool from bytes at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public bool ToBool(byte[] bytes, int startIndex);

    /// <summary>
    /// Decode string from bytes starting at startIndex.
    /// </summary>
    /// <param name="bytes">Source bytes</param>
    /// <param name="startIndex">Start index</param>
    public string ToString(byte[] bytes, int startIndex);

    /// <summary>
    /// Encode ulong to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(ulong value);

    /// <summary>
    /// Encode long to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(long value);

    /// <summary>
    /// Encode uint to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(uint value);

    /// <summary>
    /// Encode int to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(int value);

    /// <summary>
    /// Encode ushort to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(ushort value);

    /// <summary>
    /// Encode short to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(short value);

    /// <summary>
    /// Encode double to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(double value);

    /// <summary>
    /// Encode float to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(float value);

    /// <summary>
    /// Encode char to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(char value);

    /// <summary>
    /// Encode bool to bytes with configured endianness.
    /// </summary>
    /// <param name="value">Value</param>
    public byte[] GetBytes(bool value);

    /// <summary>
    /// Encode string to bytes using configured encoding (no endian swap).
    /// </summary>
    /// <param name="value">String value</param>
    public byte[] GetBytes(string value);
}
```

#### ByteBuffer
Growable byte buffer with separate read/write cursors. Implements `IByteBuffer`.

```csharp
public class ByteBuffer : IByteBuffer
{
    /// <summary>
    /// Reading index position
    /// </summary>
    public int ReadPosition { get; }

    /// <summary>
    /// Writing index position
    /// </summary>
    public int WritePosition { get; }

    /// <summary>
    /// Total capacity
    /// </summary>
    public int Cap { get; }

    /// <summary>
    /// Current unread length
    /// </summary>
    public int Len { get; }

    /// <summary>
    /// Underlying byte array backing store.
    /// </summary>
    public byte[] BuffData { get; }

    /// <summary>
    /// Create a buffer with default capacity (256 bytes).
    /// </summary>
    public ByteBuffer();

    /// <summary>
    /// Create a buffer with the specified initial capacity.
    /// </summary>
    /// <param name="buffSize">Initial capacity</param>
    public ByteBuffer(int buffSize);

    /// <summary>
    /// Wrap an existing byte array as the backing store.
    /// </summary>
    /// <param name="buffer">Backing byte array</param>
    public ByteBuffer(byte[] buffer);

    /// <summary>
    /// Set reading index position
    /// </summary>
    /// <param name="pos"></param>
    public void SetReadPosition(int pos);

    /// <summary>
    /// Set writing index position
    /// </summary>
    /// <param name="pos"></param>
    public void SetWritePosition(int pos);

    /// <summary>
    /// Clean up records
    /// </summary>
    public void Clear();

    /// <summary>
    /// Write binary 0 value
    /// </summary>
    /// <param name="size"></param>
    public void WriteZero(int size);

    /// <summary>
    /// Write a byte
    /// </summary>
    /// <param name="b"></param>
    public void Write(byte b);

    /// <summary>
    /// Write byte array
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">the start index of bytes</param>
    /// <param name="size"></param>
    public void Write(byte[] bytes, int startIndex, int size);

    /// <summary>
    /// Write byte array
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">the start index of bytes</param>
    public void Write(byte[] bytes, int startIndex);

    /// <summary>
    /// Write byte array
    /// </summary>
    /// <param name="bytes"></param>
    public void Write(byte[] bytes);

    /// <summary>
    /// Read a byte data
    /// </summary>
    /// <returns></returns>
    public byte ReadByte();

    /// <summary>
    /// Read all unread data
    /// </summary>
    /// <returns></returns>
    public byte[] ReadBytes();

    /// <summary>
    /// Read data of specified length
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public byte[] ReadBytes(int size);

    /// <summary>
    /// Read data of specified length and write it into the target array
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public int ReadBytesTo(ref byte[] dst);

    /// <summary>
    /// Read data of specified length and write it into the target array
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    public int ReadBytesTo(ref byte[] dst, int size);

    /// <summary>
    /// Copy a byte without advancing the read index.
    /// </summary>
    /// <returns></returns>
    public byte CopyByte();

    /// <summary>
    /// Copy all unread data starting with offset subscripts
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyBytes(int offset = 0);

    /// <summary>
    /// Copy unread data of specified length
    /// </summary>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyBytes(int size, int offset);

    /// <summary>
    /// Copy the specified length of data and write it into the target array
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int CopyBytesTo(ref byte[] dst, int offset = 0);

    /// <summary>
    /// Copy the specified length of data and write it into the target array
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    public int CopyBytesTo(ref byte[] dst, int size, int offset);
}
```

#### DataBuffer
Typed data buffer built on `ByteBuffer` with endian-aware read/write. Implements `IDataBuffer`. Public members are split across `DataBuffer.cs`, `DataBuffer_Delegate.cs`, `DataBuffer_Writer.cs`, `DataBuffer_Reader.cs`, and `DataBuffer_Copier.cs`.

```csharp
public partial class DataBuffer : IDataBuffer
{
    /// <summary>
    /// Create a buffer with the specified byte capacity and endianness.
    /// </summary>
    /// <param name="byteSize">Initial byte capacity</param>
    /// <param name="littleEndian">True for little-endian</param>
    public DataBuffer(int byteSize, bool littleEndian);

    /// <summary>
    /// Create a buffer with default byte capacity and the specified endianness.
    /// </summary>
    /// <param name="littleEndian">True for little-endian</param>
    public DataBuffer(bool littleEndian);

    /// <summary>
    /// Create a buffer with the specified endian converter.
    /// </summary>
    /// <param name="coverter">Endian converter</param>
    public DataBuffer(EndianCoverter coverter);

    /// <summary>
    /// Endian Coverter
    /// </summary>
    public EndianCoverter EndianCoverter { get; }

    // IByteBuffer / IByteBufferReader / IByteBufferWriter / IByteBufferCopier
    /// <summary>Total capacity</summary>
    public int Cap { get; }

    /// <summary>Clean up records</summary>
    public void Clear();

    /// <summary>Current unread length</summary>
    public int Len { get; }

    /// <summary>Reading index position</summary>
    public int ReadPosition { get; }

    /// <summary>Set reading index position</summary>
    /// <param name="pos"></param>
    public void SetReadPosition(int pos);

    /// <summary>Read a byte data</summary>
    /// <returns></returns>
    public byte ReadByte();

    /// <summary>Read all unread data</summary>
    /// <returns></returns>
    public byte[] ReadBytes();

    /// <summary>Read data of specified length</summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public byte[] ReadBytes(int size);

    /// <summary>Read data of specified length and write it into the target array</summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public int ReadBytesTo(ref byte[] dst);

    /// <summary>Read data of specified length and write it into the target array</summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    public int ReadBytesTo(ref byte[] dst, int size);

    /// <summary>Copy a byte without advancing the read index.</summary>
    /// <returns></returns>
    public byte CopyByte();

    /// <summary>Copy all unread data starting with offset subscripts</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyBytes(int offset = 0);

    /// <summary>Copy unread data of specified length</summary>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyBytes(int size, int offset);

    /// <summary>Copy the specified length of data and write it into the target array</summary>
    /// <param name="dst"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int CopyBytesTo(ref byte[] dst, int offset = 0);

    /// <summary>Copy the specified length of data and write it into the target array</summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    public int CopyBytesTo(ref byte[] dst, int size, int offset);

    /// <summary>Writing index position</summary>
    public int WritePosition { get; }

    /// <summary>Set writing index position</summary>
    /// <param name="pos"></param>
    public void SetWritePosition(int pos);

    /// <summary>Write binary 0 value</summary>
    /// <param name="size"></param>
    public void WriteZero(int size);

    /// <summary>Write byte array</summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">the start index of bytes</param>
    /// <param name="size"></param>
    public void Write(byte[] bytes, int startIndex, int size);

    /// <summary>Write byte array</summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">the start index of bytes</param>
    public void Write(byte[] bytes, int startIndex);

    /// <summary>Write byte array</summary>
    /// <param name="bytes"></param>
    public void Write(byte[] bytes);

    /// <summary>Write a byte</summary>
    /// <param name="b"></param>
    public void Write(byte b);

    // IDataBufferWriter
    /// <summary>Write length info</summary>
    /// <param name="len"></param>
    public void WriteLen(int len);

    /// <summary>Write a boolean</summary>
    /// <param name="data"></param>
    public void WriteData(bool data);

    /// <summary>Write in one character data, occupying two bytes and 16 bits</summary>
    /// <param name="data"></param>
    public void WriteData(char data);

    /// <summary>Write in an unsigned 8-bit integer</summary>
    /// <param name="data"></param>
    public void WriteData(byte data);

    /// <summary>Write in an unsigned 16-bit integer</summary>
    /// <param name="data"></param>
    public void WriteData(ushort data);

    /// <summary>Write in an unsigned 32-bit integer</summary>
    /// <param name="data"></param>
    public void WriteData(uint data);

    /// <summary>Write in an unsigned 64-bit integer</summary>
    /// <param name="data"></param>
    public void WriteData(ulong data);

    /// <summary>Write in a signed 8-bit integer</summary>
    /// <param name="data"></param>
    public void WriteData(sbyte data);

    /// <summary>Write in a signed 16-bit integer</summary>
    /// <param name="data"></param>
    public void WriteData(short data);

    /// <summary>Write in a signed 32-bit integer</summary>
    /// <param name="data"></param>
    public void WriteData(int data);

    /// <summary>Write in a signed 64-bit integer</summary>
    /// <param name="data"></param>
    public void WriteData(long data);

    /// <summary>Write in a 32-bit single-precision floating-point data</summary>
    /// <param name="data"></param>
    public void WriteData(float data);

    /// <summary>Write in a 64-bit double-precision floating-point data</summary>
    /// <param name="data"></param>
    public void WriteData(double data);

    /// <summary>Write in string data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(string data);

    /// <summary>Write in bool array. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(bool[] data);

    /// <summary>Write in character array. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(char[] data);

    /// <summary>Write in byte array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(byte[] data);

    /// <summary>Write in ushort array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(ushort[] data);

    /// <summary>Write in uint array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(uint[] data);

    /// <summary>Write in ulong array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(ulong[] data);

    /// <summary>Write in sbyte array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(sbyte[] data);

    /// <summary>Write in short array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(short[] data);

    /// <summary>Write in int array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(int[] data);

    /// <summary>Write in long array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(long[] data);

    /// <summary>Write in float array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(float[] data);

    /// <summary>Write in double array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(double[] data);

    /// <summary>Write in string array data. Note: Length information will be written</summary>
    /// <param name="data"></param>
    public void WriteData(string[] data);

    /// <summary>Write basic type data or its array</summary>
    /// <param name="data"></param>
    public void WriteBaseData(object data);

    // IDataBufferReader
    /// <summary>Read length info</summary>
    public int ReadLen();

    /// <summary>Read a boolean</summary>
    /// <returns></returns>
    public bool ReadBool();

    /// <summary>Read one character data, occupying two bytes and 16 bits</summary>
    /// <returns></returns>
    public char ReadChar();

    /// <summary>Read an unsigned 8-bit integer</summary>
    /// <returns></returns>
    public byte ReadUInt8();

    /// <summary>Read an unsigned 16-bit integer</summary>
    /// <returns></returns>
    public ushort ReadUInt16();

    /// <summary>Read an unsigned 32-bit integer</summary>
    /// <returns></returns>
    public uint ReadUInt32();

    /// <summary>Read an unsigned 64-bit integer</summary>
    /// <returns></returns>
    public ulong ReadUInt64();

    /// <summary>Read a signed 8-bit integer</summary>
    /// <returns></returns>
    public sbyte ReadInt8();

    /// <summary>Read a signed 16-bit integer</summary>
    /// <returns></returns>
    public short ReadInt16();

    /// <summary>Read a signed 32-bit integer</summary>
    /// <returns></returns>
    public int ReadInt32();

    /// <summary>Read a signed 64-bit integer</summary>
    /// <returns></returns>
    public long ReadInt64();

    /// <summary>Read a 32-bit single-precision floating-point data</summary>
    /// <returns></returns>
    public float ReadFloat();

    /// <summary>Read a 64-bit double-precision floating-point data</summary>
    /// <returns></returns>
    public double ReadDouble();

    /// <summary>Read string data</summary>
    /// <returns></returns>
    public string ReadString();

    /// <summary>Read bool array</summary>
    /// <returns></returns>
    public bool[] ReadBoolArray();

    /// <summary>Read bool array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool[] ReadBoolArray(int num);

    /// <summary>Read character array</summary>
    /// <returns></returns>
    public char[] ReadCharArray();

    /// <summary>Read character array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public char[] ReadCharArray(int num);

    /// <summary>Read byte array</summary>
    /// <returns></returns>
    public byte[] ReadUInt8Array();

    /// <summary>Read byte array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public byte[] ReadUInt8Array(int num);

    /// <summary>Read ushort array</summary>
    /// <returns></returns>
    public ushort[] ReadUInt16Array();

    /// <summary>Read ushort array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public ushort[] ReadUInt16Array(int num);

    /// <summary>Read uint array</summary>
    /// <returns></returns>
    public uint[] ReadUInt32Array();

    /// <summary>Read uint array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public uint[] ReadUInt32Array(int num);

    /// <summary>Read ulong array</summary>
    /// <returns></returns>
    public ulong[] ReadUInt64Array();

    /// <summary>Read ulong array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public ulong[] ReadUInt64Array(int num);

    /// <summary>Read sbyte array</summary>
    /// <returns></returns>
    public sbyte[] ReadInt8Array();

    /// <summary>Read sbyte array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public sbyte[] ReadInt8Array(int num);

    /// <summary>Read short array</summary>
    /// <returns></returns>
    public short[] ReadInt16Array();

    /// <summary>Read short array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public short[] ReadInt16Array(int num);

    /// <summary>Read int array</summary>
    /// <returns></returns>
    public int[] ReadInt32Array();

    /// <summary>Read int array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int[] ReadInt32Array(int num);

    /// <summary>Read long array</summary>
    /// <returns></returns>
    public long[] ReadInt64Array();

    /// <summary>Read long array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public long[] ReadInt64Array(int num);

    /// <summary>Read float array</summary>
    /// <returns></returns>
    public float[] ReadFloatArray();

    /// <summary>Read float array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public float[] ReadFloatArray(int num);

    /// <summary>Read double array</summary>
    /// <returns></returns>
    public double[] ReadDoubleArray();

    /// <summary>Read double array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public double[] ReadDoubleArray(int num);

    /// <summary>Read string array</summary>
    /// <returns></returns>
    public string[] ReadStringArray();

    /// <summary>Read string array of specified length</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public string[] ReadStringArray(int num);

    /// <summary>Read basic type data or its array</summary>
    /// <param name="data"></param>
    public void ReadBaseDataTo(ref object data);

    // IDataBufferCopier
    /// <summary>Copy length info</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int CopyLen(int offset = 0);

    /// <summary>Copy an unsigned 64-bit integer data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public ulong CopyUInt64(int offset = 0);

    /// <summary>Copy an unsigned 32-bit integer data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public uint CopyUInt32(int offset = 0);

    /// <summary>Copy an unsigned 16-bit integer data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public ushort CopyUInt16(int offset = 0);

    /// <summary>Copy an unsigned 8-bit integer data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte CopyUInt8(int offset = 0);

    /// <summary>Copy a signed 64-bit integer data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public long CopyInt64(int offset = 0);

    /// <summary>Copy a signed 32-bit integer data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int CopyInt32(int offset = 0);

    /// <summary>Copy a signed 16-bit integer data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public short CopyInt16(int offset = 0);

    /// <summary>Copy a signed 8-bit integer data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public sbyte CopyInt8(int offset = 0);

    /// <summary>Copy a boolean</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public bool CopyBool(int offset = 0);

    /// <summary>Copy a 64-bit double-precision floating-point data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public double CopyDouble(int offset = 0);

    /// <summary>Copy a 32-bit single-precision floating-point data</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public float CopyFloat(int offset = 0);

    /// <summary>Copy one character data, occupying two bytes of 16 bits</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public char CopyChar(int offset = 0);

    /// <summary>Read string data, not move reader index</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public string CopyString(int offset = 0);

    /// <summary>Read bool array, not move reader index</summary>
    /// <returns></returns>
    public bool[] CopyBoolArray();

    /// <summary>Read bool array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public bool[] CopyBoolArray(int num, int offset = 0);

    /// <summary>Read character array, not move reader index</summary>
    /// <returns></returns>
    public char[] CopyCharArray();

    /// <summary>Read character array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public char[] CopyCharArray(int num, int offset = 0);

    /// <summary>Read byte array, not move reader index</summary>
    /// <returns></returns>
    public byte[] CopyUInt8Array();

    /// <summary>Read byte array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyUInt8Array(int num, int offset = 0);

    /// <summary>Read ushort array, not move reader index</summary>
    /// <returns></returns>
    public ushort[] CopyUInt16Array();

    /// <summary>Read ushort array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public ushort[] CopyUInt16Array(int num, int offset = 0);

    /// <summary>Read uint array, not move reader index</summary>
    /// <returns></returns>
    public uint[] CopyUInt32Array();

    /// <summary>Read uint array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public uint[] CopyUInt32Array(int num, int offset = 0);

    /// <summary>Read ulong array, not move reader index</summary>
    /// <returns></returns>
    public ulong[] CopyUInt64Array();

    /// <summary>Read ulong array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public ulong[] CopyUInt64Array(int num, int offset = 0);

    /// <summary>Read sbyte array, not move reader index</summary>
    /// <returns></returns>
    public sbyte[] CopyInt8Array();

    /// <summary>Read sbyte array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public sbyte[] CopyInt8Array(int num, int offset = 0);

    /// <summary>Read short array, not move reader index</summary>
    /// <returns></returns>
    public short[] CopyInt16Array();

    /// <summary>Read short array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public short[] CopyInt16Array(int num, int offset = 0);

    /// <summary>Read int array, not move reader index</summary>
    /// <returns></returns>
    public int[] CopyInt32Array();

    /// <summary>Read int array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int[] CopyInt32Array(int num, int offset = 0);

    /// <summary>Read long array, not move reader index</summary>
    /// <returns></returns>
    public long[] CopyInt64Array();

    /// <summary>Read long array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public long[] CopyInt64Array(int num, int offset = 0);

    /// <summary>Read float array, not move reader index</summary>
    /// <returns></returns>
    public float[] CopyFloatArray();

    /// <summary>Read float array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public float[] CopyFloatArray(int num, int offset = 0);

    /// <summary>Read double array, not move reader index</summary>
    /// <returns></returns>
    public double[] CopyDoubleArray();

    /// <summary>Read double array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public double[] CopyDoubleArray(int num, int offset = 0);

    /// <summary>Read string array, not move reader index</summary>
    /// <returns></returns>
    public string[] CopyStringArray();

    /// <summary>Read string array of specified length, not move reader index</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public string[] CopyStringArray(int num, int offset = 0);

    /// <summary>Read basic type data or its array, not move reader index</summary>
    /// <param name="data"></param>
    public void CopyBaseDataTo(ref object data);
}
```

#### ValueKindUtil
Utilities for `ValueKind` classification and default value creation.

```csharp
public static class ValueKindUtil
{
    /// <summary>
    /// Whether the kind is invalid (none or slice-none).
    /// </summary>
    /// <param name="kind">Type kind</param>
    /// <returns>True if forbidden</returns>
    public static bool IsForbidKind(ValueKind kind);

    /// <summary>
    /// Whether the kind is a scalar primitive.
    /// </summary>
    /// <param name="kind">Type kind</param>
    /// <returns>True if scalar</returns>
    public static bool IsSimpleKind(ValueKind kind);

    /// <summary>
    /// Whether the kind is a primitive array.
    /// </summary>
    /// <param name="kind">Type kind</param>
    /// <returns>True if array kind</returns>
    public static bool IsArrayKind(ValueKind kind);

    /// <summary>
    /// Map a runtime value to its ValueKind.
    /// </summary>
    /// <param name="value">Runtime value</param>
    /// <returns>Matching kind, or KindNone</returns>
    public static ValueKind GetValueKind(object value);

    /// <summary>
    /// Create a default value or empty array for the given kind.
    /// </summary>
    /// <param name="kind">Type kind</param>
    /// <param name="arrayLen">Array length when kind is array</param>
    /// <returns>Default instance, or null if unsupported</returns>
    public static object GetKindValue(ValueKind kind, int arrayLen);
}
```

#### CodingList
Key-value list for primitive types with binary serialization (list-backed, keys sorted on insert).

```csharp
public sealed class CodingList
{
    /// <summary>
    /// iteration function
    /// </summary>
    public delegate void FuncEach(string key, object value);

    /// <summary>
    /// Create an empty list with the specified endianness for serialization.
    /// </summary>
    /// <param name="littleEndian">True for little-endian</param>
    public CodingList(bool littleEndian);

    /// <summary>
    /// Format key-value pairs as a readable string.
    /// </summary>
    public override string ToString();

    /// <summary>
    /// key-value size
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Clear the var set.
    /// </summary>
    public void Clear();

    /// <summary>
    /// Set key-value.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value">Basic data types and their array types</param>
    public void SetValue(string key, object value);

    /// <summary>
    /// Remove key-value and return value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object DeleteValue(string key);

    /// <summary>
    /// Set key-value in batch
    /// </summary>
    /// <param name="vars"></param>
    public void SetValues(Dictionary<string, object> vars);

    /// <summary>
    /// Check key is exist or not.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool CheckKey(string key);

    /// <summary>
    /// Get value
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Basic data types and their array types</returns>
    public object GetValue(string key);

    /// <summary>
    /// Get value
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T">Basic data types and their array types</typeparam>
    /// <returns></returns>
    public T GetValue<T>(string key);

    /// <summary>
    /// iteration
    /// </summary>
    /// <param name="each"></param>
    public void ForEach(FuncEach each);

    /// <summary>
    /// Serialized to a byte array.
    /// </summary>
    /// <returns></returns>
    public byte[] ToBinary();

    /// <summary>
    /// Update data from a byte array.
    /// </summary>
    /// <param name="bytes"></param>
    public void FromBinaryOverride(byte[] bytes);
}
```

#### CodingMap
Key-value map for primitive types with binary serialization (dictionary-backed, keys sorted on export).

```csharp
public sealed class CodingMap
{
    /// <summary>
    /// iteration function
    /// </summary>
    public delegate void FuncEach(string key, object value);

    /// <summary>
    /// Create an empty map with the specified endianness for serialization.
    /// </summary>
    /// <param name="littleEndian">True for little-endian</param>
    public CodingMap(bool littleEndian);

    /// <summary>
    /// Format key-value pairs as a readable string (keys sorted).
    /// </summary>
    public override string ToString();

    /// <summary>
    /// key-value size
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Clear the var set.
    /// </summary>
    public void Clear();

    /// <summary>
    /// Set key-value.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value">Basic data types and their array types</param>
    public void SetValue(string key, object value);

    /// <summary>
    /// Remove key-value and return value.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object DeleteValue(string key);

    /// <summary>
    /// Set key-value in batch
    /// </summary>
    /// <param name="vars"></param>
    public void SetValues(Dictionary<string, object> vars);

    /// <summary>
    /// Check key is exist or not.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool CheckKey(string key);

    /// <summary>
    /// Get value
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Basic data types and their array types</returns>
    public object GetValue(string key);

    /// <summary>
    /// Get value
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T">Basic data types and their array types</typeparam>
    /// <returns></returns>
    public T GetValue<T>(string key);

    /// <summary>
    /// iteration
    /// </summary>
    /// <param name="each"></param>
    public void ForEach(FuncEach each);

    /// <summary>
    /// Serialized to a byte array.
    /// </summary>
    /// <returns></returns>
    public byte[] ToBinary();

    /// <summary>
    /// Update data from a byte array.
    /// </summary>
    /// <param name="bytes"></param>
    public void FromBinaryOverride(byte[] bytes);
}
```

### Function Description

#### IByteBuffer
Main byte buffer interface that integrates reading, writing, and copying:
- **Cap**: Total buffer capacity
- **Clear**: Reset read/write positions

#### IByteBufferReader
Consumes data by advancing the read index:
- **Len**: Unread length (write position minus read position)
- **ReadPosition / SetReadPosition**: Read cursor
- **ReadByte / ReadBytes / ReadBytesTo**: Read and advance the read index

#### IByteBufferWriter
Appends data at the write index:
- **WritePosition / SetWritePosition**: Write cursor
- **WriteZero**: Write a run of zero bytes
- **Write**: Write a single byte or raw byte array (no type tag, no length prefix)

#### IByteBufferCopier
Peek API: copy data without advancing the read index.
- **CopyByte / CopyBytes / CopyBytesTo**: Optional offset

#### IDataBuffer
Adds endian-aware primitive serialization on top of the byte buffer, and exposes **EndianCoverter**.

#### IDataBufferReader / IDataBufferWriter / IDataBufferCopier
- **WriteLen / ReadLen / CopyLen**: Length prefix (`ushort`, 2 bytes)
- **WriteData / Read* / Copy***: Scalars and arrays using the configured endianness
- String and array `WriteData` methods write length information
- **WriteBaseData / ReadBaseDataTo / CopyBaseDataTo**: Dispatch by runtime type to the matching primitive or array overload

#### ByteBuffer
Implementation of `IByteBuffer`. Default capacity is 256 bytes; the buffer grows on write. Additionally exposes **BuffData**. Wrapping an existing array starts both cursors at 0.

#### DataBuffer
Implementation of `IDataBuffer`. Holds a `ByteBuffer` and an `EndianCoverter`; byte-level operations are delegated to the inner buffer.

#### ValueKind / ValueKindUtil
`ValueKind` is the serialization type tag (array kinds start at 128). `ValueKindUtil` classifies scalar vs array, maps runtime values, and creates defaults. `GetValueKind` does not cover complex kinds, `KindInt`/`KindUint`, or their slice counterparts.

#### EndianCoverter
Converts multi-byte values between host endianness and the configured little/big endian. Strings use the configured `Encoding` (UTF-8 by default) with no endian swap.

#### BinarySize
Byte-size constants for primitives and the length prefix.

#### CodingList / CodingMap
Serialize string keys with primitive (and array) values using `ValueKind` tags:
- **CodingList**: list storage; keys are sorted on insert
- **CodingMap**: dictionary storage; keys are sorted in `ToString` export
- **ToBinary / FromBinaryOverride**: encode/decode via `DataBuffer`

### Usage Examples

#### Basic Read/Write Operations
```csharp
// Create byte buffer (specified initial capacity)
var buffer = new ByteBuffer(1024);

// Write data
buffer.Write(new byte[] { 1, 2, 3, 4, 5 });
buffer.Write((byte)6);

// Read data
byte firstByte = buffer.ReadByte(); // 1
byte[] data = buffer.ReadBytes(3);  // [2, 3, 4]

// Check status
Console.WriteLine($"Capacity: {buffer.Cap}");
Console.WriteLine($"Unread length: {buffer.Len}");
Console.WriteLine($"Read position: {buffer.ReadPosition}");
Console.WriteLine($"Write position: {buffer.WritePosition}");
```

#### Copy Operations
```csharp
var buffer = new ByteBuffer(100);
buffer.Write(new byte[] { 1, 2, 3, 4, 5 });

// Copy data (does not move reading position)
byte copiedByte = buffer.CopyByte(); // 1, reading position still 0
byte[] copiedData = buffer.CopyBytes(3, 0); // [1, 2, 3], reading position still 0

// Read data (moves reading position)
byte readByte = buffer.ReadByte(); // 1, reading position becomes 1
```

#### Position Control
```csharp
var buffer = new ByteBuffer(100);
buffer.Write(new byte[] { 1, 2, 3, 4, 5 });

// Set reading position
buffer.SetReadPosition(2);
byte value = buffer.ReadByte(); // 3

// Set writing position
buffer.SetWritePosition(10);
buffer.Write((byte)100);
```

#### DataBuffer Typed Read/Write
```csharp
var buffer = new DataBuffer(littleEndian: true);

buffer.WriteData(42);
buffer.WriteData("hello");
buffer.WriteData(new int[] { 1, 2, 3 });

int n = buffer.ReadInt32();          // 42
string s = buffer.ReadString();      // "hello"
int[] arr = buffer.ReadInt32Array(); // [1, 2, 3]
```

#### CodingList / CodingMap Serialization
```csharp
var list = new CodingList(littleEndian: true);
list.SetValue("hp", 100);
list.SetValue("name", "hero");
byte[] bin = list.ToBinary();

var restored = new CodingList(littleEndian: true);
restored.FromBinaryOverride(bin);
int hp = restored.GetValue<int>("hp");
string name = restored.GetValue<string>("name");
```

### Design Features

1. **Interface Separation**: Byte-level and typed read/write/copy are layered for implementation and extension
2. **Position Control**: Independent read/write cursors; copy (peek) does not move the read index
3. **Endianness**: Little/big endian via `EndianCoverter`
4. **Type Tags**: `ValueKind` drives binary encoding of key-value collections
5. **Auto Growth**: `ByteBuffer` expands and compacts unread data when capacity is insufficient
6. **Length Prefix**: Length, strings, and arrays use a 2-byte `ushort` length field

### Notes

1. **Boundary Checking**: Ensure `Len` is sufficient before reading; some reads return `null` or `0` when data is short
2. **Position Management**: Keep cursors in range; `Clear` resets indexes and does not zero-fill
3. **Write vs WriteData**: `Write` stores raw bytes; `WriteData` encodes by type and endianness, with a length prefix for strings/arrays
4. **Type-name spelling**: The public type is `EndianCoverter` (not Converter)
5. **Thread Safety**: Additional synchronization is required in multi-threaded use
6. **Memory Management**: Large writes may expand the backing array; `BuffData` exposes it, but bypassing the read/write API can desynchronize cursors
