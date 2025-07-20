# Buffer API Documentation

## Namespace: JLGames.Infra.Buffer

### Interfaces

#### IByteBuffer
Byte buffer interface, inherits from IByteBufferReader, IByteBufferWriter, IByteBufferCopier

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
Byte buffer reader interface

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
Byte buffer writer interface

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
Byte buffer copier interface

```csharp
public interface IByteBufferCopier
{
    /// <summary>
    /// Copy a byte data.
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

### Enums

#### ValueKind
Value type enumeration

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

### Classes

#### ByteBuffer
Byte buffer implementation class

```csharp
public class ByteBuffer : IByteBuffer
{
    // Specific implementation requires further analysis of file content
}
```

### Function Description

#### IByteBuffer
Main byte buffer interface that integrates reading, writing, and copying functionality:
- **Cap**: Get the total capacity of the buffer
- **Clear**: Clean up buffer records and reset state

#### IByteBufferReader
Byte buffer reading interface that provides data reading functionality:
- **Len**: Get the current unread data length
- **ReadPosition**: Get the current reading position
- **SetReadPosition**: Set the reading position
- **ReadByte**: Read a single byte
- **ReadBytes**: Read data, supports reading all or specified length
- **ReadBytesTo**: Read data to target array

#### IByteBufferWriter
Byte buffer writing interface that provides data writing functionality:
- **WritePosition**: Get the current writing position
- **SetWritePosition**: Set the writing position
- **WriteZero**: Write zero values of specified length
- **Write**: Write byte data, supports single byte or byte array

#### IByteBufferCopier
Byte buffer copying interface that provides data copying functionality:
- **CopyByte**: Copy a single byte (does not move reading position)
- **CopyBytes**: Copy data, supports specified length and offset
- **CopyBytesTo**: Copy data to target array

#### ValueKind
Value type enumeration that defines supported data types:
- **Basic types**: Boolean, integer, float, complex, string
- **Slice types**: Array/slice corresponding to basic types

### Usage Examples

#### Basic Read/Write Operations
```csharp
// Create byte buffer
var buffer = new ByteBuffer(1024);

// Write data
buffer.Write(new byte[] { 1, 2, 3, 4, 5 });
buffer.WriteByte(6);

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
byte[] copiedData = buffer.CopyBytes(3); // [1, 2, 3], reading position still 0

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
buffer.WriteByte(100);
```

### Design Features

1. **Interface Separation**: Reading, writing, and copying functionality are separated for easy implementation and extension
2. **Position Control**: Supports independent read/write position control
3. **Type Support**: Supports multiple data types through ValueKind enumeration
4. **Memory Efficiency**: Provides copying functionality to avoid unnecessary data movement
5. **Flexibility**: Supports partial reading and writing operations

### Notes

1. **Boundary Checking**: Check buffer capacity and available data before use
2. **Position Management**: Pay attention to the valid range of read/write positions
3. **Memory Management**: Pay attention to memory usage during large data operations
4. **Thread Safety**: Additional synchronization mechanisms are required in multi-threaded environments 