# JLGames.Infra API Documentation Overview

## Overview

JLGames.Infra is a feature-rich C# infrastructure framework that provides various utility tools and functional modules. This documentation contains detailed API references for all modules.

## Module List

### Core Modules

- **[Infra API](Infra-API.md)** - Core interfaces and base classes
  - ICloneable<T> - Generic cloning interface
  - Callback - Universal callback context class

### Data Processing Modules

- **[Buffer API](Buffer-API.md)** - Buffer operations
  - IByteBuffer, IByteBufferReader, IByteBufferWriter - Byte buffer interfaces
  - DataBuffer - Data buffer implementation
  - Supports read/write, copy, encoding and other functions

- **[TinyJson API](TinyJson-API.md)** - JSON processing
  - JSONParser - JSON parser
  - JSONWriter - JSON writer
  - Lightweight JSON processing tool

- **[Xml API](Xml-API.md)** - XML processing
  - XmlUtils - XML serialization and deserialization tools
  - Supports object to XML string conversion

### Encryption and Security Modules

- **[Crypto API](Crypto-API.md)** - Encryption and decryption functionality
  - ICipher, IAesCipher, IRsaCipher - Encryption interfaces
  - AesCipher, RsaCipher - Encryption implementations
  - Supports AES, RSA, DES and other encryption algorithms

- **[Encodingx API](Encodingx-API.md)** - Encoding processing
  - IBase64Encoding - Base64 encoding interface
  - Base64Utils - Base64 utility class
  - Supports standard, URL-safe, no-padding formats

### Event and Communication Modules

- **[Event API](Event-API.md)** - Event system
  - IEventListener, IEventDispatcher - Event interfaces
  - EventDispatcher, EventManager - Event managers
  - Supports synchronous and asynchronous event processing

- **[Net API](Net-API.md)** - Network communication
  - ISocketClient, ISocketConn - Socket interfaces
  - SocketClient, SocketReceiver, SocketSender - Network components
  - Supports TCP, HTTP and other protocols

### Service Management Modules

- **[Service API](Service-API.md)** - Service management
  - IService, IInitService - Service interfaces
  - ServiceManager - Service manager
  - Supports service lifecycle management

- **[Serial API](Serial-API.md)** - Serialization management
  - ISerialManager, ISerialModule - Serialization interfaces
  - SerialManager - Serialization manager
  - Supports serial module startup and shutdown

### Algorithm and Mathematics Modules

- **[Algs API](Algs-API.md)** - Algorithm implementation
  - IAStarAlg, IAStarGridMap - A* algorithm interfaces
  - AStarAlg, AStarGridMap - A* pathfinding algorithm
  - Supports grid map pathfinding

- **[Mathx API](Mathx-API.md)** - Mathematics tools
  - Point2Int, Point3Int - Point structures
  - Bounds2Int, Array2D - Geometric structures
  - MathUtil - Mathematics utility class

### Utility and Extension Modules

- **[Utils API](Utils-API.md)** - Utility tools
  - ArrayUtil, BitUtil - Array and bit operations
  - FileUtil, PathUtil - File and path processing
  - TextUtil, DirectoryUtil - Text and directory operations
  - Encryption tools: DESUtil, RijandelUtil, RSAUtil

- **[Extensions API](Extensions-API.md)** - Extension methods
  - ExtString - String extensions
  - ExtDouble, ExtFloat - Numeric extensions
  - Rich text formatting and regular expression support

### Time and Data Management Modules

- **[DateTimex API](DateTimex-API.md)** - Time processing
  - DateTimeUtil - DateTime utilities
  - TimeSeries, TimeSlice - Time series
  - StampTimer - Timestamp timer

- **[Pool API](Pool-API.md)** - Object pools
  - ReuseObjectPool<T> - Reusable object pool
  - KVObjectPool<K,V> - Key-value object pool
  - MetaObjectPool<T> - Metadata object pool

### Threading and Concurrency Modules

- **[Threadx API](Threadx-API.md)** - Thread processing
  - FixedThreadContext - Fixed thread context
  - ContextWorkRequest - Context work request
  - Supports thread pools and work queues

### File and Data Modules

- **[Archive API](Archive-API.md)** - Archive processing
  - IUnarchive - Unarchive interface
  - Unzip, ArchiveUtil - Unarchive tools
  - Supports compressed file processing

### Image Processing Modules

- **[Imagex API](Imagex-API.md)** - Image processing
  - IImage, IAlpha - Image and alpha interfaces
  - RGBA - RGBA image class
  - FilterKernel, FilterMatrix - Image filters

### Scripting Language Modules

- **[Languages API](Languages-API.md)** - Scripting languages
  - LuaInterpreter - Lua interpreter
  - LuaValue - Lua value types
  - Supports Lua script execution and standard library

## Quick Start

### Basic Usage Examples

```csharp
// 1. Event system
var eventDispatcher = new EventDispatcher();
eventDispatcher.AddEventListener("test", (evd) => Console.WriteLine("Event triggered"));
eventDispatcher.DispatchEvent("test", null);

// 2. Service management
var serviceManager = new ServiceManager();
serviceManager.RegisterService(new MyService());
serviceManager.InitializeAll();

// 3. Network communication
var socketClient = new SocketClient();
socketClient.Connect("127.0.0.1", 8080);

// 4. Encryption processing
var aesCipher = new AesCipher();
byte[] encrypted = aesCipher.Encrypt(data, key);

// 5. JSON processing
var json = JSONWriter.ToJson(obj);
var obj = JSONParser.FromJson<MyClass>(json);
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
- **Documentation Version:** 1.0

## Contributing Guidelines

To contribute code or improve documentation, please follow these guidelines:

1. Maintain consistent code style
2. Add appropriate XML comments
3. Include both Chinese and English comments
4. Provide usage examples
5. Update related documentation

## License

This documentation and related code follow the corresponding open source license.

---

**Note:** This documentation covers all major modules of the JLGames.Infra framework. Each module has detailed API documentation including interface definitions, class descriptions, method descriptions and usage examples. It is recommended to consult the corresponding module documentation based on specific requirements. 