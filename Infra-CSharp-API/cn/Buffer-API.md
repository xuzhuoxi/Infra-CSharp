# Buffer API 文档

## 命名空间: JLGames.Infra.Buffer

### 接口 (Interfaces)

#### IByteBuffer
具备读取、写入与窥视（复制）能力的字节缓冲区，继承自 `IByteBufferReader`、`IByteBufferWriter`、`IByteBufferCopier`。

```csharp
public interface IByteBuffer : IByteBufferReader, IByteBufferWriter, IByteBufferCopier
{
    /// <summary>
    /// 总容量
    /// </summary>
    int Cap { get; }

    /// <summary>
    /// 清理记录
    /// </summary>
    void Clear();
}
```

#### IByteBufferReader
字节缓冲区读取器。

```csharp
public interface IByteBufferReader
{
    /// <summary>
    /// 当前未读取长度
    /// </summary>
    int Len { get; }

    /// <summary>
    /// 读取索引位置
    /// </summary>
    int ReadPosition { get; }

    /// <summary>
    /// 设置读取索引位置
    /// </summary>
    /// <param name="pos"></param>
    void SetReadPosition(int pos);

    /// <summary>
    /// 读取一个字节数据
    /// </summary>
    /// <returns></returns>
    byte ReadByte();

    /// <summary>
    /// 读取全部未读取的数据
    /// </summary>
    /// <returns></returns>
    byte[] ReadBytes();

    /// <summary>
    /// 读取指定长度数据
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    byte[] ReadBytes(int size);

    /// <summary>
    /// 读取指定长度数据，并写入到目标数组中
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    int ReadBytesTo(ref byte[] dst);

    /// <summary>
    /// 读取指定长度数据，并写入到目标数组中
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    int ReadBytesTo(ref byte[] dst, int size);
}
```

#### IByteBufferWriter
字节缓冲区写入器。

```csharp
public interface IByteBufferWriter
{
    /// <summary>
    /// 写入索引位置
    /// </summary>
    int WritePosition { get; }

    /// <summary>
    /// 设置写入索引位置
    /// </summary>
    /// <param name="pos"></param>
    void SetWritePosition(int pos);

    /// <summary>
    /// 写入二进制0值
    /// </summary>
    /// <param name="size"></param>
    void WriteZero(int size);

    /// <summary>
    /// 写入单个字节
    /// </summary>
    /// <param name="b"></param>
    void Write(byte b);

    /// <summary>
    /// 写入字节数组
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">字节起始索引</param>
    /// <param name="size"></param>
    void Write(byte[] bytes, int startIndex, int size);

    /// <summary>
    /// 写入字节数组
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">字节起始索引</param>
    void Write(byte[] bytes, int startIndex);

    /// <summary>
    /// 写入字节数组
    /// </summary>
    /// <param name="bytes"></param>
    void Write(byte[] bytes);
}
```

#### IByteBufferCopier
字节缓冲区复制器；读取时不移动读下标。

```csharp
public interface IByteBufferCopier
{
    /// <summary>
    /// 复制一个字节，不移动读下标
    /// </summary>
    /// <returns></returns>
    byte CopyByte();

    /// <summary>
    /// 复制从偏移下标开始的全部未读取的数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    byte[] CopyBytes(int offset = 0);

    /// <summary>
    /// 复制指定长度的未读取数据
    /// </summary>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    byte[] CopyBytes(int size, int offset);

    /// <summary>
    /// 复制指定长度数据， 并写入到目标数组中
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    int CopyBytesTo(ref byte[] dst, int offset = 0);

    /// <summary>
    /// 复制指定长度数据， 并写入到目标数组中
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    int CopyBytesTo(ref byte[] dst, int size, int offset);
}
```

#### IDataBuffer
支持按字节序序列化基础类型的数据缓冲区，继承自 `IByteBuffer`、`IDataBufferWriter`、`IDataBufferReader`、`IDataBufferCopier`。

```csharp
public interface IDataBuffer : IByteBuffer, IDataBufferWriter, IDataBufferReader, IDataBufferCopier
{
    /// <summary>
    /// 字节转换器
    /// </summary>
    EndianCoverter EndianCoverter { get; }
}
```

#### IDataBufferReader
数据缓冲区读取器。无参数组读取会先读取长度信息；带 `num` 的重载按指定元素个数读取。

```csharp
public interface IDataBufferReader
{
    /// <summary>
    /// 读取长度信息
    /// </summary>
    int ReadLen();

    /// <summary>
    /// 读取一个布尔型数据
    /// </summary>
    /// <returns></returns>
    bool ReadBool();

    /// <summary>
    /// 读取一个字符数据，占两字节16位
    /// </summary>
    /// <returns></returns>
    char ReadChar();

    /// <summary>
    /// 读取一个无符号8位整型数据
    /// </summary>
    /// <returns></returns>
    byte ReadUInt8();

    /// <summary>
    /// 读取一个无符号16位整型数据
    /// </summary>
    /// <returns></returns>
    ushort ReadUInt16();

    /// <summary>
    /// 读取一个无符号32位整型数据
    /// </summary>
    /// <returns></returns>
    uint ReadUInt32();

    /// <summary>
    /// 读取一个无符号64位整型数据
    /// </summary>
    /// <returns></returns>
    ulong ReadUInt64();

    /// <summary>
    /// 读取一个有符号8位整型数据
    /// </summary>
    /// <returns></returns>
    sbyte ReadInt8();

    /// <summary>
    /// 读取一个有符号16位整型数据
    /// </summary>
    /// <returns></returns>
    short ReadInt16();

    /// <summary>
    /// 读取一个有符号32位整型数据
    /// </summary>
    /// <returns></returns>
    int ReadInt32();

    /// <summary>
    /// 读取一个有符号64位整型数据
    /// </summary>
    /// <returns></returns>
    long ReadInt64();

    /// <summary>
    /// 读取一个32位单精度浮点数据
    /// </summary>
    /// <returns></returns>
    float ReadFloat();

    /// <summary>
    /// 读取一个64位双精度浮点数据
    /// </summary>
    /// <returns></returns>
    double ReadDouble();

    /// <summary>
    /// 读取字符串数据
    /// </summary>
    /// <returns></returns>
    string ReadString();

    /// <summary>
    /// 读取 bool数组
    /// </summary>
    /// <returns></returns>
    bool[] ReadBoolArray();

    /// <summary>
    /// 读取指定长度 bool数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    bool[] ReadBoolArray(int num);

    /// <summary>
    /// 读取字符数组
    /// </summary>
    /// <returns></returns>
    char[] ReadCharArray();

    /// <summary>
    /// 读取指定长度字符数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    char[] ReadCharArray(int num);

    /// <summary>
    /// 读取 byte数组
    /// </summary>
    /// <returns></returns>
    byte[] ReadUInt8Array();

    /// <summary>
    /// 读取指定长度 byte数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    byte[] ReadUInt8Array(int num);

    /// <summary>
    /// 读取 ushort数组
    /// </summary>
    /// <returns></returns>
    ushort[] ReadUInt16Array();

    /// <summary>
    /// 读取指定长度 ushort数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    ushort[] ReadUInt16Array(int num);

    /// <summary>
    /// 读取 uint数组
    /// </summary>
    /// <returns></returns>
    uint[] ReadUInt32Array();

    /// <summary>
    /// 读取指定长度 uint数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    uint[] ReadUInt32Array(int num);

    /// <summary>
    /// 读取 ulong数组
    /// </summary>
    /// <returns></returns>
    ulong[] ReadUInt64Array();

    /// <summary>
    /// 读取指定长度 ulong数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    ulong[] ReadUInt64Array(int num);

    /// <summary>
    /// 读取 sbyte数组
    /// </summary>
    /// <returns></returns>
    sbyte[] ReadInt8Array();

    /// <summary>
    /// 读取指定长度 sbyte数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    sbyte[] ReadInt8Array(int num);

    /// <summary>
    /// 读取 short数组
    /// </summary>
    /// <returns></returns>
    short[] ReadInt16Array();

    /// <summary>
    /// 读取指定长度 short数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    short[] ReadInt16Array(int num);

    /// <summary>
    /// 读取 int数组
    /// </summary>
    /// <returns></returns>
    int[] ReadInt32Array();

    /// <summary>
    /// 读取指定长度 int数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    int[] ReadInt32Array(int num);

    /// <summary>
    /// 读取 long数组
    /// </summary>
    /// <returns></returns>
    long[] ReadInt64Array();

    /// <summary>
    /// 读取指定长度 long数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    long[] ReadInt64Array(int num);

    /// <summary>
    /// 读取 float数组
    /// </summary>
    /// <returns></returns>
    float[] ReadFloatArray();

    /// <summary>
    /// 读取指定长度 float数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    float[] ReadFloatArray(int num);

    /// <summary>
    /// 读取 double数组
    /// </summary>
    /// <returns></returns>
    double[] ReadDoubleArray();

    /// <summary>
    /// 读取指定长度 double数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    double[] ReadDoubleArray(int num);

    /// <summary>
    /// 读取 string数组
    /// </summary>
    /// <returns></returns>
    string[] ReadStringArray();

    /// <summary>
    /// 读取指定长度 string数组
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    string[] ReadStringArray(int num);

    /// <summary>
    /// 读取 基础类型数据 或 其数组
    /// </summary>
    /// <param name="data"></param>
    void ReadBaseDataTo(ref object data);
}
```

#### IDataBufferWriter
数据缓冲区写入器。字符串与数组写入会附带长度信息。

```csharp
public interface IDataBufferWriter
{
    /// <summary>
    /// 写入长度信息
    /// </summary>
    /// <param name="len"></param>
    void WriteLen(int len);

    /// <summary>
    /// 写入一个布尔型数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(bool data);

    /// <summary>
    /// 写入一个字符数据，占两字节16位
    /// </summary>
    /// <param name="data"></param>
    void WriteData(char data);

    /// <summary>
    /// 写入一个无符号8位整型数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(byte data);

    /// <summary>
    /// 写入一个无符号16位整型数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(ushort data);

    /// <summary>
    /// 写入一个无符号32位整型数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(uint data);

    /// <summary>
    /// 写入一个无符号64位整型数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(ulong data);

    /// <summary>
    /// 写入一个有符号8位整型数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(sbyte data);

    /// <summary>
    /// 写入一个有符号16位整型数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(short data);

    /// <summary>
    /// 写入一个有符号32位整型数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(int data);

    /// <summary>
    /// 写入一个有符号64位整型数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(long data);

    /// <summary>
    /// 写入一个32位单精度浮点数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(float data);

    /// <summary>
    /// 写入一个64位双精度浮点数据
    /// </summary>
    /// <param name="data"></param>
    void WriteData(double data);

    /// <summary>
    /// 写入字符串数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(string data);

    /// <summary>
    /// 写入 bool 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(bool[] data);

    /// <summary>
    /// 写入 char 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(char[] data);

    /// <summary>
    /// 写入 byte 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(byte[] data);

    /// <summary>
    /// 写入 ushort 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(ushort[] data);

    /// <summary>
    /// 写入 uint 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(uint[] data);

    /// <summary>
    /// 写入 ulong 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(ulong[] data);

    /// <summary>
    /// 写入 sbyte 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(sbyte[] data);

    /// <summary>
    /// 写入 short 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(short[] data);

    /// <summary>
    /// 写入 int 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(int[] data);

    /// <summary>
    /// 写入 long 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(long[] data);

    /// <summary>
    /// 写入 float 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(float[] data);

    /// <summary>
    /// 写入 double 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(double[] data);

    /// <summary>
    /// 写入 string 数组数据
    /// 注意：会写入长度信息
    /// </summary>
    /// <param name="data"></param>
    void WriteData(string[] data);

    /// <summary>
    /// 写入 基础类型数据 或 其数组
    /// </summary>
    /// <param name="data"></param>
    void WriteBaseData(object data);
}
```

#### IDataBufferCopier
数据缓冲区复制器；读取时不移动读下标。无参数组复制会先窥视长度信息；带 `num` 的重载按指定元素个数复制。

```csharp
public interface IDataBufferCopier
{
    /// <summary>
    /// 复制长度信息
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    int CopyLen(int offset = 0);

    /// <summary>
    /// 复制一个无符号64位整型数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    ulong CopyUInt64(int offset = 0);

    /// <summary>
    /// 复制一个无符号32位整型数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    uint CopyUInt32(int offset = 0);

    /// <summary>
    /// 复制一个无符号16位整型数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    ushort CopyUInt16(int offset = 0);

    /// <summary>
    /// 复制一个无符号8位整型数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    byte CopyUInt8(int offset = 0);

    /// <summary>
    /// 复制一个有符号64位整型数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    long CopyInt64(int offset = 0);

    /// <summary>
    /// 复制一个有符号32位整型数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    int CopyInt32(int offset = 0);

    /// <summary>
    /// 复制一个有符号16位整型数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    short CopyInt16(int offset = 0);

    /// <summary>
    /// 复制一个有符号8位整型数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    sbyte CopyInt8(int offset = 0);

    /// <summary>
    /// 复制一个布尔型数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    bool CopyBool(int offset = 0);

    /// <summary>
    /// 复制一个64位双精度浮点数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    double CopyDouble(int offset = 0);

    /// <summary>
    /// 复制一个32位单精度浮点数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    float CopyFloat(int offset = 0);

    /// <summary>
    /// 复制一个字符数据，占两字节16位
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    char CopyChar(int offset = 0);

    /// <summary>
    /// 读取字符串数据, 不移动读下标
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    string CopyString(int offset = 0);

    /// <summary>
    /// 读取 bool数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    bool[] CopyBoolArray();

    /// <summary>
    /// 读取指定长度 bool数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    bool[] CopyBoolArray(int num, int offset = 0);

    /// <summary>
    /// 读取字符数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    char[] CopyCharArray();

    /// <summary>
    /// 读取指定长度字符数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    char[] CopyCharArray(int num, int offset = 0);

    /// <summary>
    /// 读取 byte数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    byte[] CopyUInt8Array();

    /// <summary>
    /// 读取指定长度 byte数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    byte[] CopyUInt8Array(int num, int offset = 0);

    /// <summary>
    /// 读取 ushort数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    ushort[] CopyUInt16Array();

    /// <summary>
    /// 读取指定长度 ushort数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    ushort[] CopyUInt16Array(int num, int offset = 0);

    /// <summary>
    /// 读取 uint数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    uint[] CopyUInt32Array();

    /// <summary>
    /// 读取指定长度 uint数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    uint[] CopyUInt32Array(int num, int offset = 0);

    /// <summary>
    /// 读取 ulong数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    ulong[] CopyUInt64Array();

    /// <summary>
    /// 读取指定长度 ulong数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    ulong[] CopyUInt64Array(int num, int offset = 0);

    /// <summary>
    /// 读取 sbyte数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    sbyte[] CopyInt8Array();

    /// <summary>
    /// 读取指定长度 sbyte数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    sbyte[] CopyInt8Array(int num, int offset = 0);

    /// <summary>
    /// 读取 short数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    short[] CopyInt16Array();

    /// <summary>
    /// 读取指定长度 short数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    short[] CopyInt16Array(int num, int offset = 0);

    /// <summary>
    /// 读取 int数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    int[] CopyInt32Array();

    /// <summary>
    /// 读取指定长度 int数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    int[] CopyInt32Array(int num, int offset = 0);

    /// <summary>
    /// 读取 long数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    long[] CopyInt64Array();

    /// <summary>
    /// 读取指定长度 long数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    long[] CopyInt64Array(int num, int offset = 0);

    /// <summary>
    /// 读取 float数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    float[] CopyFloatArray();

    /// <summary>
    /// 读取指定长度 float数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    float[] CopyFloatArray(int num, int offset = 0);

    /// <summary>
    /// 读取 double数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    double[] CopyDoubleArray();

    /// <summary>
    /// 读取指定长度 double数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    double[] CopyDoubleArray(int num, int offset = 0);

    /// <summary>
    /// 读取 string数组, 不移动读下标
    /// </summary>
    /// <returns></returns>
    string[] CopyStringArray();

    /// <summary>
    /// 读取指定长度 string数组, 不移动读下标
    /// </summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    string[] CopyStringArray(int num, int offset = 0);

    /// <summary>
    /// 读取 基础类型数据 或 其数组, 不移动读下标
    /// </summary>
    /// <param name="data"></param>
    void CopyBaseDataTo(ref object data);
}
```

### 枚举 (Enums)

#### ValueKind
可序列化基础类型及其数组的类型标记。

```csharp
public enum ValueKind
{
    /// <summary>无效或不支持</summary>
    KindNone = (byte)0,
    /// <summary>布尔</summary>
    KindBool,
    /// <summary>有符号 8 位整数</summary>
    KindInt8,
    /// <summary>有符号 16 位整数</summary>
    KindInt16,
    /// <summary>有符号 32 位整数</summary>
    KindInt32,
    /// <summary>有符号 64 位整数</summary>
    KindInt64,
    /// <summary>无符号 8 位整数</summary>
    KindUint8,
    /// <summary>无符号 16 位整数</summary>
    KindUint16,
    /// <summary>无符号 32 位整数</summary>
    KindUint32,
    /// <summary>无符号 64 位整数</summary>
    KindUint64,
    /// <summary>单精度浮点</summary>
    KindFloat32,
    /// <summary>双精度浮点</summary>
    KindFloat64,
    /// <summary>64位复数</summary>
    KindComplex64,
    /// <summary>128位复数</summary>
    KindComplex128,
    /// <summary>有符号整数</summary>
    KindInt,
    /// <summary>无符号整数</summary>
    KindUint,
    /// <summary>字符串</summary>
    KindString,

    /// <summary>无效数组类型</summary>
    KindSliceNone = (byte)128,
    /// <summary>布尔数组</summary>
    KindSliceBool,
    /// <summary>有符号 8 位整数数组</summary>
    KindSliceInt8,
    /// <summary>有符号 16 位整数数组</summary>
    KindSliceInt16,
    /// <summary>有符号 32 位整数数组</summary>
    KindSliceInt32,
    /// <summary>有符号 64 位整数数组</summary>
    KindSliceInt64,
    /// <summary>无符号 8 位整数数组</summary>
    KindSliceUint8,
    /// <summary>无符号 16 位整数数组</summary>
    KindSliceUint16,
    /// <summary>无符号 32 位整数数组</summary>
    KindSliceUint32,
    /// <summary>无符号 64 位整数数组</summary>
    KindSliceUint64,
    /// <summary>单精度浮点数组</summary>
    KindSliceFloat32,
    /// <summary>双精度浮点数组</summary>
    KindSliceFloat64,
    /// <summary>64位复数数组</summary>
    KindSliceComplex64,
    /// <summary>128位复数数组</summary>
    KindSliceComplex128,
    /// <summary>有符号整数数组</summary>
    KindSliceInt,
    /// <summary>无符号整数数组</summary>
    KindSliceUint,
    /// <summary>字符串数组</summary>
    KindSliceString
}
```

### 委托 (Delegates)

#### CodingList.FuncEach / CodingMap.FuncEach
遍历代理函数，定义在 `CodingList` 与 `CodingMap` 内部。

```csharp
/// <summary>
/// 遍历代理函数
/// </summary>
/// <param name="key">键</param>
/// <param name="value">值</param>
public delegate void FuncEach(string key, object value);
```

### 类 (Classes)

#### BinarySize
序列化所用基础类型的二进制字节数常量。

```csharp
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
```

#### EndianCoverter
字节序转换器。源码类型名即为 `EndianCoverter`（Coverter）。

```csharp
public sealed class EndianCoverter
{
    /// <summary>
    /// 小端
    /// </summary>
    public static readonly EndianCoverter LittleEndianCoverter;

    /// <summary>
    /// 大端
    /// </summary>
    public static readonly EndianCoverter BigEndianCoverter;

    /// <summary>
    /// 取字节转换器
    /// </summary>
    /// <param name="littleEndian"></param>
    /// <returns></returns>
    public static EndianCoverter GetEndianCoverter(bool littleEndian);

    /// <summary>
    /// 是否使用小端字节序
    /// </summary>
    public bool IsLittleEndian { get; }

    /// <summary>
    /// 使用 UTF-8 字符串编码创建转换器
    /// </summary>
    /// <param name="littleEndian">true 表示小端</param>
    public EndianCoverter(bool littleEndian);

    /// <summary>
    /// 使用指定字符串编码创建转换器
    /// </summary>
    /// <param name="littleEndian">true 表示小端</param>
    /// <param name="encoding">字符串编码</param>
    public EndianCoverter(bool littleEndian, Encoding encoding);

    /// <summary>
    /// 从字节数组指定位置读取 ulong
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public ulong ToUInt64(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 uint
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public uint ToUInt32(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 ushort
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public ushort ToUInt16(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 byte
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public byte ToUInt8(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 long
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public long ToInt64(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 int
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public int ToInt32(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 short
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public short ToInt16(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 sbyte
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public sbyte ToInt8(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 double
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public double ToDouble(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 float
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public float ToFloat(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 char
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public char ToChar(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置读取 bool
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public bool ToBool(byte[] bytes, int startIndex);

    /// <summary>
    /// 从字节数组指定位置解码字符串
    /// </summary>
    /// <param name="bytes">源字节数组</param>
    /// <param name="startIndex">起始下标</param>
    public string ToString(byte[] bytes, int startIndex);

    /// <summary>
    /// 按配置的字节序将 ulong 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(ulong value);

    /// <summary>
    /// 按配置的字节序将 long 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(long value);

    /// <summary>
    /// 按配置的字节序将 uint 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(uint value);

    /// <summary>
    /// 按配置的字节序将 int 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(int value);

    /// <summary>
    /// 按配置的字节序将 ushort 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(ushort value);

    /// <summary>
    /// 按配置的字节序将 short 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(short value);

    /// <summary>
    /// 按配置的字节序将 double 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(double value);

    /// <summary>
    /// 按配置的字节序将 float 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(float value);

    /// <summary>
    /// 按配置的字节序将 char 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(char value);

    /// <summary>
    /// 按配置的字节序将 bool 编码为字节数组
    /// </summary>
    /// <param name="value">值</param>
    public byte[] GetBytes(bool value);

    /// <summary>
    /// 使用配置的编码将字符串编码为字节数组（不涉及字节序翻转）
    /// </summary>
    /// <param name="value">字符串值</param>
    public byte[] GetBytes(string value);
}
```

#### ByteBuffer
可自动扩容的字节缓冲区，读写下标独立。实现 `IByteBuffer`。

```csharp
public class ByteBuffer : IByteBuffer
{
    /// <summary>
    /// 读取索引位置
    /// </summary>
    public int ReadPosition { get; }

    /// <summary>
    /// 写入索引位置
    /// </summary>
    public int WritePosition { get; }

    /// <summary>
    /// 总容量
    /// </summary>
    public int Cap { get; }

    /// <summary>
    /// 当前未读取长度
    /// </summary>
    public int Len { get; }

    /// <summary>
    /// 底层字节数组
    /// </summary>
    public byte[] BuffData { get; }

    /// <summary>
    /// 使用默认容量（256 字节）创建缓冲区
    /// </summary>
    public ByteBuffer();

    /// <summary>
    /// 使用指定初始容量创建缓冲区
    /// </summary>
    /// <param name="buffSize">初始容量</param>
    public ByteBuffer(int buffSize);

    /// <summary>
    /// 包装已有字节数组作为底层存储
    /// </summary>
    /// <param name="buffer">底层字节数组</param>
    public ByteBuffer(byte[] buffer);

    /// <summary>
    /// 设置读取索引位置
    /// </summary>
    /// <param name="pos"></param>
    public void SetReadPosition(int pos);

    /// <summary>
    /// 设置写入索引位置
    /// </summary>
    /// <param name="pos"></param>
    public void SetWritePosition(int pos);

    /// <summary>
    /// 清理记录
    /// </summary>
    public void Clear();

    /// <summary>
    /// 写入二进制0值
    /// </summary>
    /// <param name="size"></param>
    public void WriteZero(int size);

    /// <summary>
    /// 写入单个字节
    /// </summary>
    /// <param name="b"></param>
    public void Write(byte b);

    /// <summary>
    /// 写入字节数组
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">字节起始索引</param>
    /// <param name="size"></param>
    public void Write(byte[] bytes, int startIndex, int size);

    /// <summary>
    /// 写入字节数组
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">字节起始索引</param>
    public void Write(byte[] bytes, int startIndex);

    /// <summary>
    /// 写入字节数组
    /// </summary>
    /// <param name="bytes"></param>
    public void Write(byte[] bytes);

    /// <summary>
    /// 读取一个字节数据
    /// </summary>
    /// <returns></returns>
    public byte ReadByte();

    /// <summary>
    /// 读取全部未读取的数据
    /// </summary>
    /// <returns></returns>
    public byte[] ReadBytes();

    /// <summary>
    /// 读取指定长度数据
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public byte[] ReadBytes(int size);

    /// <summary>
    /// 读取指定长度数据，并写入到目标数组中
    /// </summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public int ReadBytesTo(ref byte[] dst);

    /// <summary>
    /// 读取指定长度数据，并写入到目标数组中
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    public int ReadBytesTo(ref byte[] dst, int size);

    /// <summary>
    /// 复制一个字节，不移动读下标
    /// </summary>
    /// <returns></returns>
    public byte CopyByte();

    /// <summary>
    /// 复制从偏移下标开始的全部未读取的数据
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyBytes(int offset = 0);

    /// <summary>
    /// 复制指定长度的未读取数据
    /// </summary>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyBytes(int size, int offset);

    /// <summary>
    /// 复制指定长度数据， 并写入到目标数组中
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int CopyBytesTo(ref byte[] dst, int offset = 0);

    /// <summary>
    /// 复制指定长度数据， 并写入到目标数组中
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    public int CopyBytesTo(ref byte[] dst, int size, int offset);
}
```

#### DataBuffer
基于 `ByteBuffer` 构建、支持按字节序读写基础类型的数据缓冲区。实现 `IDataBuffer`。公开成员分布在 `DataBuffer.cs`、`DataBuffer_Delegate.cs`、`DataBuffer_Writer.cs`、`DataBuffer_Reader.cs`、`DataBuffer_Copier.cs`。

```csharp
public partial class DataBuffer : IDataBuffer
{
    /// <summary>
    /// 使用指定字节容量与字节序创建缓冲区
    /// </summary>
    /// <param name="byteSize">初始字节容量</param>
    /// <param name="littleEndian">true 表示小端</param>
    public DataBuffer(int byteSize, bool littleEndian);

    /// <summary>
    /// 使用默认字节容量与指定字节序创建缓冲区
    /// </summary>
    /// <param name="littleEndian">true 表示小端</param>
    public DataBuffer(bool littleEndian);

    /// <summary>
    /// 使用指定字节序转换器创建缓冲区
    /// </summary>
    /// <param name="coverter">字节序转换器</param>
    public DataBuffer(EndianCoverter coverter);

    /// <summary>
    /// 字节转换器
    /// </summary>
    public EndianCoverter EndianCoverter { get; }

    // IByteBuffer / IByteBufferReader / IByteBufferWriter / IByteBufferCopier
    /// <summary>总容量</summary>
    public int Cap { get; }

    /// <summary>清理记录</summary>
    public void Clear();

    /// <summary>当前未读取长度</summary>
    public int Len { get; }

    /// <summary>读取索引位置</summary>
    public int ReadPosition { get; }

    /// <summary>设置读取索引位置</summary>
    /// <param name="pos"></param>
    public void SetReadPosition(int pos);

    /// <summary>读取一个字节数据</summary>
    /// <returns></returns>
    public byte ReadByte();

    /// <summary>读取全部未读取的数据</summary>
    /// <returns></returns>
    public byte[] ReadBytes();

    /// <summary>读取指定长度数据</summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public byte[] ReadBytes(int size);

    /// <summary>读取指定长度数据，并写入到目标数组中</summary>
    /// <param name="dst"></param>
    /// <returns></returns>
    public int ReadBytesTo(ref byte[] dst);

    /// <summary>读取指定长度数据，并写入到目标数组中</summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    public int ReadBytesTo(ref byte[] dst, int size);

    /// <summary>复制一个字节，不移动读下标</summary>
    /// <returns></returns>
    public byte CopyByte();

    /// <summary>复制从偏移下标开始的全部未读取的数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyBytes(int offset = 0);

    /// <summary>复制指定长度的未读取数据</summary>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyBytes(int size, int offset);

    /// <summary>复制指定长度数据， 并写入到目标数组中</summary>
    /// <param name="dst"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int CopyBytesTo(ref byte[] dst, int offset = 0);

    /// <summary>复制指定长度数据， 并写入到目标数组中</summary>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    /// <param name="offset"></param>
    public int CopyBytesTo(ref byte[] dst, int size, int offset);

    /// <summary>写入索引位置</summary>
    public int WritePosition { get; }

    /// <summary>设置写入索引位置</summary>
    /// <param name="pos"></param>
    public void SetWritePosition(int pos);

    /// <summary>写入二进制0值</summary>
    /// <param name="size"></param>
    public void WriteZero(int size);

    /// <summary>写入字节数组</summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">字节起始索引</param>
    /// <param name="size"></param>
    public void Write(byte[] bytes, int startIndex, int size);

    /// <summary>写入字节数组</summary>
    /// <param name="bytes"></param>
    /// <param name="startIndex">字节起始索引</param>
    public void Write(byte[] bytes, int startIndex);

    /// <summary>写入字节数组</summary>
    /// <param name="bytes"></param>
    public void Write(byte[] bytes);

    /// <summary>写入单个字节</summary>
    /// <param name="b"></param>
    public void Write(byte b);

    // IDataBufferWriter
    /// <summary>写入长度信息</summary>
    /// <param name="len"></param>
    public void WriteLen(int len);

    /// <summary>写入一个布尔型数据</summary>
    /// <param name="data"></param>
    public void WriteData(bool data);

    /// <summary>写入一个字符数据，占两字节16位</summary>
    /// <param name="data"></param>
    public void WriteData(char data);

    /// <summary>写入一个无符号8位整型数据</summary>
    /// <param name="data"></param>
    public void WriteData(byte data);

    /// <summary>写入一个无符号16位整型数据</summary>
    /// <param name="data"></param>
    public void WriteData(ushort data);

    /// <summary>写入一个无符号32位整型数据</summary>
    /// <param name="data"></param>
    public void WriteData(uint data);

    /// <summary>写入一个无符号64位整型数据</summary>
    /// <param name="data"></param>
    public void WriteData(ulong data);

    /// <summary>写入一个有符号8位整型数据</summary>
    /// <param name="data"></param>
    public void WriteData(sbyte data);

    /// <summary>写入一个有符号16位整型数据</summary>
    /// <param name="data"></param>
    public void WriteData(short data);

    /// <summary>写入一个有符号32位整型数据</summary>
    /// <param name="data"></param>
    public void WriteData(int data);

    /// <summary>写入一个有符号64位整型数据</summary>
    /// <param name="data"></param>
    public void WriteData(long data);

    /// <summary>写入一个32位单精度浮点数据</summary>
    /// <param name="data"></param>
    public void WriteData(float data);

    /// <summary>写入一个64位双精度浮点数据</summary>
    /// <param name="data"></param>
    public void WriteData(double data);

    /// <summary>写入字符串数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(string data);

    /// <summary>写入 bool 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(bool[] data);

    /// <summary>写入 char 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(char[] data);

    /// <summary>写入 byte 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(byte[] data);

    /// <summary>写入 ushort 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(ushort[] data);

    /// <summary>写入 uint 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(uint[] data);

    /// <summary>写入 ulong 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(ulong[] data);

    /// <summary>写入 sbyte 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(sbyte[] data);

    /// <summary>写入 short 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(short[] data);

    /// <summary>写入 int 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(int[] data);

    /// <summary>写入 long 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(long[] data);

    /// <summary>写入 float 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(float[] data);

    /// <summary>写入 double 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(double[] data);

    /// <summary>写入 string 数组数据。注意：会写入长度信息</summary>
    /// <param name="data"></param>
    public void WriteData(string[] data);

    /// <summary>写入 基础类型数据 或 其数组</summary>
    /// <param name="data"></param>
    public void WriteBaseData(object data);

    // IDataBufferReader
    /// <summary>读取长度信息</summary>
    public int ReadLen();

    /// <summary>读取一个布尔型数据</summary>
    /// <returns></returns>
    public bool ReadBool();

    /// <summary>读取一个字符数据，占两字节16位</summary>
    /// <returns></returns>
    public char ReadChar();

    /// <summary>读取一个无符号8位整型数据</summary>
    /// <returns></returns>
    public byte ReadUInt8();

    /// <summary>读取一个无符号16位整型数据</summary>
    /// <returns></returns>
    public ushort ReadUInt16();

    /// <summary>读取一个无符号32位整型数据</summary>
    /// <returns></returns>
    public uint ReadUInt32();

    /// <summary>读取一个无符号64位整型数据</summary>
    /// <returns></returns>
    public ulong ReadUInt64();

    /// <summary>读取一个有符号8位整型数据</summary>
    /// <returns></returns>
    public sbyte ReadInt8();

    /// <summary>读取一个有符号16位整型数据</summary>
    /// <returns></returns>
    public short ReadInt16();

    /// <summary>读取一个有符号32位整型数据</summary>
    /// <returns></returns>
    public int ReadInt32();

    /// <summary>读取一个有符号64位整型数据</summary>
    /// <returns></returns>
    public long ReadInt64();

    /// <summary>读取一个32位单精度浮点数据</summary>
    /// <returns></returns>
    public float ReadFloat();

    /// <summary>读取一个64位双精度浮点数据</summary>
    /// <returns></returns>
    public double ReadDouble();

    /// <summary>读取字符串数据</summary>
    /// <returns></returns>
    public string ReadString();

    /// <summary>读取 bool数组</summary>
    /// <returns></returns>
    public bool[] ReadBoolArray();

    /// <summary>读取指定长度 bool数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool[] ReadBoolArray(int num);

    /// <summary>读取字符数组</summary>
    /// <returns></returns>
    public char[] ReadCharArray();

    /// <summary>读取指定长度字符数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public char[] ReadCharArray(int num);

    /// <summary>读取 byte数组</summary>
    /// <returns></returns>
    public byte[] ReadUInt8Array();

    /// <summary>读取指定长度 byte数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public byte[] ReadUInt8Array(int num);

    /// <summary>读取 ushort数组</summary>
    /// <returns></returns>
    public ushort[] ReadUInt16Array();

    /// <summary>读取指定长度 ushort数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public ushort[] ReadUInt16Array(int num);

    /// <summary>读取 uint数组</summary>
    /// <returns></returns>
    public uint[] ReadUInt32Array();

    /// <summary>读取指定长度 uint数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public uint[] ReadUInt32Array(int num);

    /// <summary>读取 ulong数组</summary>
    /// <returns></returns>
    public ulong[] ReadUInt64Array();

    /// <summary>读取指定长度 ulong数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public ulong[] ReadUInt64Array(int num);

    /// <summary>读取 sbyte数组</summary>
    /// <returns></returns>
    public sbyte[] ReadInt8Array();

    /// <summary>读取指定长度 sbyte数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public sbyte[] ReadInt8Array(int num);

    /// <summary>读取 short数组</summary>
    /// <returns></returns>
    public short[] ReadInt16Array();

    /// <summary>读取指定长度 short数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public short[] ReadInt16Array(int num);

    /// <summary>读取 int数组</summary>
    /// <returns></returns>
    public int[] ReadInt32Array();

    /// <summary>读取指定长度 int数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int[] ReadInt32Array(int num);

    /// <summary>读取 long数组</summary>
    /// <returns></returns>
    public long[] ReadInt64Array();

    /// <summary>读取指定长度 long数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public long[] ReadInt64Array(int num);

    /// <summary>读取 float数组</summary>
    /// <returns></returns>
    public float[] ReadFloatArray();

    /// <summary>读取指定长度 float数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public float[] ReadFloatArray(int num);

    /// <summary>读取 double数组</summary>
    /// <returns></returns>
    public double[] ReadDoubleArray();

    /// <summary>读取指定长度 double数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public double[] ReadDoubleArray(int num);

    /// <summary>读取 string数组</summary>
    /// <returns></returns>
    public string[] ReadStringArray();

    /// <summary>读取指定长度 string数组</summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public string[] ReadStringArray(int num);

    /// <summary>读取 基础类型数据 或 其数组</summary>
    /// <param name="data"></param>
    public void ReadBaseDataTo(ref object data);

    // IDataBufferCopier
    /// <summary>复制长度信息</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int CopyLen(int offset = 0);

    /// <summary>复制一个无符号64位整型数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public ulong CopyUInt64(int offset = 0);

    /// <summary>复制一个无符号32位整型数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public uint CopyUInt32(int offset = 0);

    /// <summary>复制一个无符号16位整型数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public ushort CopyUInt16(int offset = 0);

    /// <summary>复制一个无符号8位整型数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte CopyUInt8(int offset = 0);

    /// <summary>复制一个有符号64位整型数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public long CopyInt64(int offset = 0);

    /// <summary>复制一个有符号32位整型数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int CopyInt32(int offset = 0);

    /// <summary>复制一个有符号16位整型数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public short CopyInt16(int offset = 0);

    /// <summary>复制一个有符号8位整型数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public sbyte CopyInt8(int offset = 0);

    /// <summary>复制一个布尔型数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public bool CopyBool(int offset = 0);

    /// <summary>复制一个64位双精度浮点数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public double CopyDouble(int offset = 0);

    /// <summary>复制一个32位单精度浮点数据</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public float CopyFloat(int offset = 0);

    /// <summary>复制一个字符数据，占两字节16位</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public char CopyChar(int offset = 0);

    /// <summary>读取字符串数据, 不移动读下标</summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public string CopyString(int offset = 0);

    /// <summary>读取 bool数组, 不移动读下标</summary>
    /// <returns></returns>
    public bool[] CopyBoolArray();

    /// <summary>读取指定长度 bool数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public bool[] CopyBoolArray(int num, int offset = 0);

    /// <summary>读取字符数组, 不移动读下标</summary>
    /// <returns></returns>
    public char[] CopyCharArray();

    /// <summary>读取指定长度字符数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public char[] CopyCharArray(int num, int offset = 0);

    /// <summary>读取 byte数组, 不移动读下标</summary>
    /// <returns></returns>
    public byte[] CopyUInt8Array();

    /// <summary>读取指定长度 byte数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public byte[] CopyUInt8Array(int num, int offset = 0);

    /// <summary>读取 ushort数组, 不移动读下标</summary>
    /// <returns></returns>
    public ushort[] CopyUInt16Array();

    /// <summary>读取指定长度 ushort数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public ushort[] CopyUInt16Array(int num, int offset = 0);

    /// <summary>读取 uint数组, 不移动读下标</summary>
    /// <returns></returns>
    public uint[] CopyUInt32Array();

    /// <summary>读取指定长度 uint数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public uint[] CopyUInt32Array(int num, int offset = 0);

    /// <summary>读取 ulong数组, 不移动读下标</summary>
    /// <returns></returns>
    public ulong[] CopyUInt64Array();

    /// <summary>读取指定长度 ulong数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public ulong[] CopyUInt64Array(int num, int offset = 0);

    /// <summary>读取 sbyte数组, 不移动读下标</summary>
    /// <returns></returns>
    public sbyte[] CopyInt8Array();

    /// <summary>读取指定长度 sbyte数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public sbyte[] CopyInt8Array(int num, int offset = 0);

    /// <summary>读取 short数组, 不移动读下标</summary>
    /// <returns></returns>
    public short[] CopyInt16Array();

    /// <summary>读取指定长度 short数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public short[] CopyInt16Array(int num, int offset = 0);

    /// <summary>读取 int数组, 不移动读下标</summary>
    /// <returns></returns>
    public int[] CopyInt32Array();

    /// <summary>读取指定长度 int数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public int[] CopyInt32Array(int num, int offset = 0);

    /// <summary>读取 long数组, 不移动读下标</summary>
    /// <returns></returns>
    public long[] CopyInt64Array();

    /// <summary>读取指定长度 long数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public long[] CopyInt64Array(int num, int offset = 0);

    /// <summary>读取 float数组, 不移动读下标</summary>
    /// <returns></returns>
    public float[] CopyFloatArray();

    /// <summary>读取指定长度 float数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public float[] CopyFloatArray(int num, int offset = 0);

    /// <summary>读取 double数组, 不移动读下标</summary>
    /// <returns></returns>
    public double[] CopyDoubleArray();

    /// <summary>读取指定长度 double数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public double[] CopyDoubleArray(int num, int offset = 0);

    /// <summary>读取 string数组, 不移动读下标</summary>
    /// <returns></returns>
    public string[] CopyStringArray();

    /// <summary>读取指定长度 string数组, 不移动读下标</summary>
    /// <param name="num"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public string[] CopyStringArray(int num, int offset = 0);

    /// <summary>读取 基础类型数据 或 其数组, 不移动读下标</summary>
    /// <param name="data"></param>
    public void CopyBaseDataTo(ref object data);
}
```

#### ValueKindUtil
`ValueKind` 分类与默认值创建工具。

```csharp
public static class ValueKindUtil
{
    /// <summary>
    /// 判断是否为不可用类型（KindNone 或 KindSliceNone）
    /// </summary>
    /// <param name="kind">类型标记</param>
    /// <returns>不可用时返回 true</returns>
    public static bool IsForbidKind(ValueKind kind);

    /// <summary>
    /// 判断是否为简单（标量）类型
    /// </summary>
    /// <param name="kind">类型标记</param>
    /// <returns>标量类型时返回 true</returns>
    public static bool IsSimpleKind(ValueKind kind);

    /// <summary>
    /// 判断是否为简单数组类型
    /// </summary>
    /// <param name="kind">类型标记</param>
    /// <returns>数组类型时返回 true</returns>
    public static bool IsArrayKind(ValueKind kind);

    /// <summary>
    /// 根据运行时值取得对应的 ValueKind
    /// </summary>
    /// <param name="value">运行时值</param>
    /// <returns>匹配的类型标记，不支持时返回 KindNone</returns>
    public static ValueKind GetValueKind(object value);

    /// <summary>
    /// 根据类型标记创建默认值或指定长度的空数组
    /// </summary>
    /// <param name="kind">类型标记</param>
    /// <param name="arrayLen">数组类型时的长度</param>
    /// <returns>默认实例，不支持时返回 null</returns>
    public static object GetKindValue(ValueKind kind, int arrayLen);
}
```

#### CodingList
基础类型键值列表，支持二进制序列化（列表存储，插入时键排序）。

```csharp
public sealed class CodingList
{
    /// <summary>
    /// 遍历代理函数
    /// </summary>
    public delegate void FuncEach(string key, object value);

    /// <summary>
    /// 创建空列表，序列化时使用指定字节序
    /// </summary>
    /// <param name="littleEndian">true 表示小端</param>
    public CodingList(bool littleEndian);

    /// <summary>
    /// 将键值对格式化为可读字符串
    /// </summary>
    public override string ToString();

    /// <summary>
    /// 键值对数量
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// 清理集合
    /// </summary>
    public void Clear();

    /// <summary>
    /// 设置键值对
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value">基础数据类型及它们的数组类型</param>
    public void SetValue(string key, object value);

    /// <summary>
    /// 删除键值对，并返回值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object DeleteValue(string key);

    /// <summary>
    /// 批量设置键值对
    /// </summary>
    /// <param name="vars"></param>
    public void SetValues(Dictionary<string, object> vars);

    /// <summary>
    /// 检查键的存在性
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool CheckKey(string key);

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="key"></param>
    /// <returns>基础数据类型及它们的数组类型</returns>
    public object GetValue(string key);

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T">基础数据类型及它们的数组类型</typeparam>
    /// <returns></returns>
    public T GetValue<T>(string key);

    /// <summary>
    /// 遍历
    /// </summary>
    /// <param name="each"></param>
    public void ForEach(FuncEach each);

    /// <summary>
    /// 序列化为字节数组
    /// </summary>
    /// <returns></returns>
    public byte[] ToBinary();

    /// <summary>
    /// 从字节数组更新数据
    /// </summary>
    /// <param name="bytes"></param>
    public void FromBinaryOverride(byte[] bytes);
}
```

#### CodingMap
基础类型键值映射，支持二进制序列化（字典存储，导出时键排序）。

```csharp
public sealed class CodingMap
{
    /// <summary>
    /// 遍历代理函数
    /// </summary>
    public delegate void FuncEach(string key, object value);

    /// <summary>
    /// 创建空映射，序列化时使用指定字节序
    /// </summary>
    /// <param name="littleEndian">true 表示小端</param>
    public CodingMap(bool littleEndian);

    /// <summary>
    /// 将键值对格式化为可读字符串（键已排序）
    /// </summary>
    public override string ToString();

    /// <summary>
    /// 键值对数量
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// 清理集合
    /// </summary>
    public void Clear();

    /// <summary>
    /// 设置键值对
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value">基础数据类型及他们的数组类型</param>
    public void SetValue(string key, object value);

    /// <summary>
    /// 删除键值对，并返回值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public object DeleteValue(string key);

    /// <summary>
    /// 批量设置键值对
    /// </summary>
    /// <param name="vars"></param>
    public void SetValues(Dictionary<string, object> vars);

    /// <summary>
    /// 检查键的存在性
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool CheckKey(string key);

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="key"></param>
    /// <returns>基础数据类型及他们的数组类型</returns>
    public object GetValue(string key);

    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="T">基础数据类型及他们的数组类型</typeparam>
    /// <returns></returns>
    public T GetValue<T>(string key);

    /// <summary>
    /// 遍历
    /// </summary>
    /// <param name="each"></param>
    public void ForEach(FuncEach each);

    /// <summary>
    /// 序列化为字节数组
    /// </summary>
    /// <returns></returns>
    public byte[] ToBinary();

    /// <summary>
    /// 从字节数组更新数据
    /// </summary>
    /// <param name="bytes"></param>
    public void FromBinaryOverride(byte[] bytes);
}
```

### 功能说明

#### IByteBuffer
字节缓冲区主接口，集成读取、写入和复制功能：
- **Cap**：获取缓冲区总容量
- **Clear**：清理缓冲区记录，重置读写位置

#### IByteBufferReader
提供按读下标消费数据的能力：
- **Len**：当前未读取长度（写入位置减去读取位置）
- **ReadPosition / SetReadPosition**：读取位置
- **ReadByte / ReadBytes / ReadBytesTo**：读取并前进读下标

#### IByteBufferWriter
提供按写下标追加数据的能力：
- **WritePosition / SetWritePosition**：写入位置
- **WriteZero**：写入指定长度的零值
- **Write**：写入单个字节或字节数组（无类型、无长度前缀）

#### IByteBufferCopier
提供窥视能力：复制数据但不移动读下标。
- **CopyByte / CopyBytes / CopyBytesTo**：可指定偏移

#### IDataBuffer
在字节缓冲区之上增加按字节序的基础类型读写，并暴露 **EndianCoverter**。

#### IDataBufferReader / IDataBufferWriter / IDataBufferCopier
- **WriteLen / ReadLen / CopyLen**：长度信息（`ushort`，2 字节）
- **WriteData / Read* / Copy***：按配置字节序处理标量与数组
- 字符串与数组的 `WriteData` 会写入长度信息
- **WriteBaseData / ReadBaseDataTo / CopyBaseDataTo**：按运行时类型分派到对应基础类型或数组重载

#### ByteBuffer
`IByteBuffer` 的实现。默认容量 256 字节，写入时自动扩容；额外公开 **BuffData** 访问底层数组。包装已有数组时读写位置均从 0 开始。

#### DataBuffer
`IDataBuffer` 的实现。内部持有 `ByteBuffer` 与 `EndianCoverter`，字节级操作委托给内部缓冲区。

#### ValueKind / ValueKindUtil
`ValueKind` 为序列化类型标记（含切片/数组区间，从 128 起）。`ValueKindUtil` 用于判断标量/数组、从运行时值映射类型、以及按标记创建默认值。`GetValueKind` 未覆盖复数、`KindInt`/`KindUint` 及对应切片标记。

#### EndianCoverter
在本机字节序与目标大小端之间转换多字节数值；字符串使用指定 `Encoding`（默认 UTF-8），不做字节序翻转。

#### BinarySize
各基础类型及长度前缀的占用字节常量。

#### CodingList / CodingMap
以 `ValueKind` 标记序列化字符串键与基础类型（及数组）值：
- **CodingList**：列表存储，插入时按键排序
- **CodingMap**：字典存储，`ToString` 导出时按键排序
- **ToBinary / FromBinaryOverride**：与 `DataBuffer` 编解码互转

### 使用示例

#### 基本读写操作
```csharp
// 创建字节缓冲区（指定初始容量）
var buffer = new ByteBuffer(1024);

// 写入数据
buffer.Write(new byte[] { 1, 2, 3, 4, 5 });
buffer.Write((byte)6);

// 读取数据
byte firstByte = buffer.ReadByte(); // 1
byte[] data = buffer.ReadBytes(3);  // [2, 3, 4]

// 检查状态
Console.WriteLine($"容量: {buffer.Cap}");
Console.WriteLine($"未读取长度: {buffer.Len}");
Console.WriteLine($"读取位置: {buffer.ReadPosition}");
Console.WriteLine($"写入位置: {buffer.WritePosition}");
```

#### 复制操作
```csharp
var buffer = new ByteBuffer(100);
buffer.Write(new byte[] { 1, 2, 3, 4, 5 });

// 复制数据（不移动读取位置）
byte copiedByte = buffer.CopyByte(); // 1，读取位置仍为 0
byte[] copiedData = buffer.CopyBytes(3, 0); // [1, 2, 3]，读取位置仍为 0

// 读取数据（移动读取位置）
byte readByte = buffer.ReadByte(); // 1，读取位置变为 1
```

#### 位置控制
```csharp
var buffer = new ByteBuffer(100);
buffer.Write(new byte[] { 1, 2, 3, 4, 5 });

// 设置读取位置
buffer.SetReadPosition(2);
byte value = buffer.ReadByte(); // 3

// 设置写入位置
buffer.SetWritePosition(10);
buffer.Write((byte)100);
```

#### DataBuffer 类型化读写
```csharp
var buffer = new DataBuffer(littleEndian: true);

buffer.WriteData(42);
buffer.WriteData("hello");
buffer.WriteData(new int[] { 1, 2, 3 });

int n = buffer.ReadInt32();          // 42
string s = buffer.ReadString();      // "hello"
int[] arr = buffer.ReadInt32Array(); // [1, 2, 3]
```

#### CodingList / CodingMap 序列化
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

### 设计特点

1. **接口分离**：字节级读写/复制与类型化读写/复制分层，便于实现和扩展
2. **位置控制**：读写下标独立；复制（窥视）不移动读下标
3. **字节序**：通过 `EndianCoverter` 支持大小端
4. **类型标记**：`ValueKind` 用于键值集合的二进制编解码
5. **自动扩容**：`ByteBuffer` 在容量不足时扩展并紧凑未读数据
6. **长度前缀**：长度、字符串与数组使用 2 字节 `ushort` 长度信息

### 注意事项

1. **边界检查**：读取前应确认 `Len` 足够；部分读取在数据不足时返回 `null` 或 `0`
2. **位置管理**：读写位置需落在有效范围内；`Clear` 只重置下标，不填充零
3. **Write 与 WriteData**：`Write` 写入原始字节；`WriteData` 按类型与字节序编码，字符串/数组带长度前缀
4. **类型名拼写**：公开类型名为 `EndianCoverter`（非 Converter）
5. **线程安全**：多线程环境需要额外同步
6. **内存管理**：大量写入会触发扩容；可通过 `BuffData` 观察底层数组，但不要绕过读写 API 破坏下标一致性
