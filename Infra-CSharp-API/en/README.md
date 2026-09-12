# JLGames.Infra API Documentation Overview

## Overview

JLGames.Infra is a feature-rich C# infrastructure framework that provides various utility tools and functional modules. This documentation contains detailed API references for all modules and is aligned with the current public source API.

## Module List

### Core Modules

- **[Infra API](Infra-API.md)** - Core interfaces and base classes
  - `ICloneable<T>` - Generic cloning interface
  - `Callback` - Deferred invocation with a bound delegate and arguments

### Data Processing Modules

- **[Buffer API](Buffer-API.md)** - Buffer operations
  - `IByteBuffer` / `IDataBuffer` plus Reader / Writer / Copier interfaces
  - `ByteBuffer`, `DataBuffer` - Byte and typed data buffers
  - `EndianCoverter`, `ValueKind`, `CodingList` / `CodingMap` - Endianness, type encoding, and serialization helpers

- **[TinyJson API](TinyJson-API.md)** - JSON processing
  - `JSONParser.FromJson<T>` - Parse extension method
  - `JSONWriter.ToJson` - Serialize extension method
  - Lightweight JSON with `DataMember` / `IgnoreDataMember` support

- **[Xml API](Xml-API.md)** - XML processing
  - `XmlUtils` - Object to/from XML string (`ToXml` / `FromXml`)

### Encryption and Security Modules

- **[Crypto API](Crypto-API.md)** - Encryption and decryption
  - `ICipher`, `IAesCipher`, `IDesCipher`, `IRsaCipher`, `IXorCipher` - Cipher interfaces
  - `AesCipher`, `DesCipher`, `TripleDesCipher`, `XorCipher`, `RsaCipher` - Implementations
  - AES (including CTR / GCM), RSA, DES, Diffie-Hellman key exchange, and ASN.1/TLV
  - `DESUtil` / `RijndaelUtil` / `RSAUtil` live under `Deleted` and are deprecated

- **[Encodingx API](Encodingx-API.md)** - Encoding
  - `IBase64Encoding` - Base64 encoding interface
  - `Base64StdEncoding` / `Base64RawStdEncoding` / `Base64UrlEncoding` / `Base64RawUrlEncoding`
  - `Base64Utils` - Standard, URL-safe, and no-padding formats

### Event and Communication Modules

- **[Event API](Event-API.md)** - Event system
  - `IEventListener`, `IEventDispatcher`, `IThreadEventDispatcher` - Event interfaces
  - `EventDispatcher`, `EventManager`, `EventDispatcherPool`, `EventGroup`
  - Weights, tags, listen-count limits, and thread-context dispatch

- **[Net API](Net-API.md)** - Network communication
  - `ISocketClient`, `ISocketConn`, `ISocketReceiver`, `ISocketSender` - Socket interfaces
  - `SocketFactory`, `SocketClient`, `SocketParams` - Client factory and connection parameters
  - TCP/UDP, message read/write, and HTTP client proxy

### Service Management Modules

- **[Service API](Service-API.md)** - Service management
  - `IService`, `IInitService`, `ILoadDataService`, `ISaveDataService` and other lifecycle interfaces
  - `ServiceConfig` / `ServiceInfo` - Service registration
  - `ServiceManager` - Initialization, load/save, and progress events

- **[Serial API](Serial-API.md)** - Sequential module management
  - `ISerialManager`, `ISerialModule` - Sequential interfaces
  - `SerialManager` - Start/stop modules in order
  - `SerialStatus`, `SerialEvents` - Status and completion events

### Algorithm and Mathematics Modules

- **[Algs API](Algs-API.md)** - Algorithms (namespace `JLGames.Infra.AStar`)
  - `IAStarAlg`, `IAStarGridMap` - A* interfaces
  - `AStarAlg`, `AStarGridMap`, `AStarUtil` - Grid pathfinding
  - Direction groups and priority-queue helpers

- **[Mathx API](Mathx-API.md)** - Mathematics tools
  - `Point1` / `Point1Int`, `Point2Int`, `Point3Int` - Point structures
  - `Bounds2Int`, `Array2D`, `Line1`, `Interval`, `Range` - Geometry and ranges
  - `BitMark`, `BitFixedData`, `MathUtil` - Bit marks and numeric utilities

### Utility and Extension Modules

- **[Utils API](Utils-API.md)** - Utility tools
  - `ArrayUtil`, `BitUtil` - Array and bit operations
  - `FileUtil`, `PathUtil`, `DirectoryUtil`, `TextUtil` - File, path, and text helpers
  - `ConfusedUtil`, `PrintUtil`, `ReflexUtil` - Hashing, printing, and reflection

- **[Extensions API](Extensions-API.md)** - Extension methods
  - `ExtString` - Rich-text formatting and regex split
  - `ExtDouble`, `ExtFloat` - Approximate equality and numeric helpers

### Time and Data Management Modules

- **[DateTimex API](DateTimex-API.md)** - Time processing
  - `DateTimeUtil` - Timestamps and formatting
  - `TimeSeries`, `TimeSlice` - Named time slices
  - `StampTimer` - Pausable timestamp timer

- **[Pool API](Pool-API.md)** - Object pools
  - `ReuseObjectPool<T>` - Take/return reuse pool
  - `KVObjectPool<TKey, TValue>` - Key-value mapping pool (get / clone)
  - `MetaObjectPool<T>` - Prototype-based sized pool

### Threading and Concurrency Modules

- **[Threadx API](Threadx-API.md)** - Thread processing
  - `FixedThreadContext` - Fixed-thread context (background consumer or external pump)
  - `ContextWorkRequest` - Work request
  - Work queue, `ProcessTasks`, and `StopExec`

### File and Data Modules

- **[Archive API](Archive-API.md)** - Archive processing
  - `IUnarchive` - Unarchive interface
  - `Unzip`, `ArchiveUtil.UnzipFile` / `UnzipFiles` - ZIP extraction
  - Overwrite policy and extraction progress events

### Image Processing Modules

- **[Imagex API](Imagex-API.md)** - Image processing
  - `IImage`, `IAlpha` - Image and alpha interfaces
  - `RGBA` - RGBA image (color format `0xRRGGBBAA`)
  - `FilterKernel`, `FilterMatrix` - Convolution kernels and matrix transforms

### Scripting Language Modules

- **[Languages API](Languages-API.md)** - Scripting languages
  - `LuaInterpreter` - Static entry points: `Interpreter` / `RunFile` / `Parse`
  - `LuaValue` plus number, boolean, table, userdata, and related types
  - Standard libraries and `LuaInterpreterExtra` host extensions

## Quick Start

### Basic Usage Examples

```csharp
using JLGames.Infra;
using JLGames.Infra.Event;
using JLGames.Infra.Service;
using JLGames.Infra.Net;
using JLGames.Infra.Crypto.Symmetric;
using JLGames.Infra.TinyJson;

// 1. Event system
var eventDispatcher = new EventDispatcher();
eventDispatcher.AddEventListener("test", evd => Console.WriteLine("Event triggered"));
eventDispatcher.DispatchEvent("test", null);

// 2. Service management
ServiceConfig.Shared.AddConfig(new ServiceInfo("MyService", new MyService()));
ServiceManager.Shared.StartInitalization(new Callback(_ =>
{
    Console.WriteLine("Initialization finished");
}));

// 3. Network communication
ISocketClient socketClient = SocketFactory.CreateSocketClient("demo", true, false);
socketClient.ConnectServer(new SocketParams
{
    Network = SocketNetworks.Network.Tcp,
    RemoteAddress = "127.0.0.1:8080"
});

// 4. Encryption
byte[] key = /* 16/24/32-byte AES key */;
var aesCipher = new AesCipher(key);
byte[] encrypted = aesCipher.Encrypt(data);

// 5. JSON
string json = obj.ToJson();
MyClass parsed = json.FromJson<MyClass>();
```

### Module Dependencies

```
Core
├── Event
│   ├── Service
│   └── Serial
├── Buffer
│   ├── Net
│   └── Crypto
├── Utils
│   ├── Extensions
│   └── DateTimex
├── Mathx
│   └── Algs
├── Pool
│   └── Threadx
├── Archive
├── Imagex
├── Languages
├── TinyJson
├── Xml
└── Encodingx
```

## Version Information

- **Framework Version:** Infra-CSharp
- **Target Framework:** .NET Standard 2.0
- **C# Version:** 7.3+
- **Documentation:** Synced with the current public source API

## Contributing Guidelines

To contribute code or improve documentation, please follow these guidelines:

1. Maintain consistent code style
2. Add appropriate XML comments
3. Include both Chinese and English comments
4. Provide usage examples
5. Update related documentation

## License

This documentation and related code follow the [MIT License](../../LICENSE).

---

**Note:** This documentation covers all major modules of the JLGames.Infra framework. Each module has detailed API documentation including interface definitions, class descriptions, method descriptions, and usage examples. Consult the corresponding module documentation for specifics. Signatures follow the source; historical spellings in source (such as `EndianCoverter`, `StartInitalization`, `enviroment`) are preserved as-is.
