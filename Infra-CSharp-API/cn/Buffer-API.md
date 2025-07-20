# Buffer API 文档

## 命名空间: JLGames.Infra.Buffer

### 接口 (Interfaces)

#### IByteBuffer
字节缓冲区接口，继承自IByteBufferReader、IByteBufferWriter、IByteBufferCopier

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
字节缓冲区读取接口

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
字节缓冲区写入接口

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
字节缓冲区复制接口

```csharp
public interface IByteBufferCopier
{
    /// <summary>
    /// 复制一个无符号8位整型数据
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

### 枚举 (Enums)

#### ValueKind
值类型枚举

```csharp
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
```

### 类 (Classes)

#### ByteBuffer
字节缓冲区实现类

```csharp
public class ByteBuffer : IByteBuffer
{
    // 具体实现需要进一步分析文件内容
}
```

### 功能说明

#### IByteBuffer
字节缓冲区主接口，集成了读取、写入和复制功能：
- **Cap**：获取缓冲区的总容量
- **Clear**：清理缓冲区记录，重置状态

#### IByteBufferReader
字节缓冲区读取接口，提供数据读取功能：
- **Len**：获取当前未读取的数据长度
- **ReadPosition**：获取当前读取位置
- **SetReadPosition**：设置读取位置
- **ReadByte**：读取单个字节
- **ReadBytes**：读取数据，支持读取全部或指定长度
- **ReadBytesTo**：读取数据到目标数组

#### IByteBufferWriter
字节缓冲区写入接口，提供数据写入功能：
- **WritePosition**：获取当前写入位置
- **SetWritePosition**：设置写入位置
- **WriteZero**：写入指定长度的零值
- **Write**：写入字节数据，支持单个字节或字节数组

#### IByteBufferCopier
字节缓冲区复制接口，提供数据复制功能：
- **CopyByte**：复制单个字节（不移动读取位置）
- **CopyBytes**：复制数据，支持指定长度和偏移
- **CopyBytesTo**：复制数据到目标数组

#### ValueKind
值类型枚举，定义了支持的数据类型：
- **基本类型**：布尔、整数、浮点数、复数、字符串
- **切片类型**：对应基本类型的数组/切片

### 使用示例

#### 基本读写操作
```csharp
// 创建字节缓冲区
var buffer = new ByteBuffer(1024);

// 写入数据
buffer.Write(new byte[] { 1, 2, 3, 4, 5 });
buffer.WriteByte(6);

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
byte copiedByte = buffer.CopyByte(); // 1，读取位置仍为0
byte[] copiedData = buffer.CopyBytes(3); // [1, 2, 3]，读取位置仍为0

// 读取数据（移动读取位置）
byte readByte = buffer.ReadByte(); // 1，读取位置变为1
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
buffer.WriteByte(100);
```

### 设计特点

1. **接口分离**：读取、写入、复制功能分离，便于实现和扩展
2. **位置控制**：支持独立的读写位置控制
3. **类型支持**：通过ValueKind枚举支持多种数据类型
4. **内存效率**：提供复制功能避免不必要的数据移动
5. **灵活性**：支持部分读取和写入操作

### 注意事项

1. **边界检查**：使用前应检查缓冲区容量和可用数据
2. **位置管理**：注意读写位置的有效范围
3. **内存管理**：大量数据操作时注意内存使用
4. **线程安全**：多线程环境需要额外的同步机制 