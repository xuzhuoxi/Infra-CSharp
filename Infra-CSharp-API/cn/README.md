# JLGames.Infra API 文档总览

## 概述

JLGames.Infra是一个功能丰富的C#基础设施框架，提供了多种实用工具和功能模块。本文档包含了所有模块的详细API参考。

## 模块列表

### 核心模块

- **[Infra API](Infra-API.md)** - 核心接口和基础类
  - ICloneable<T> - 泛型克隆接口
  - Callback - 通用回调上下文类

### 数据处理模块

- **[Buffer API](Buffer-API.md)** - 缓冲区操作
  - IByteBuffer, IByteBufferReader, IByteBufferWriter - 字节缓冲区接口
  - DataBuffer - 数据缓冲区实现
  - 支持读写、复制、编码等功能

- **[TinyJson API](TinyJson-API.md)** - JSON处理
  - JSONParser - JSON解析器
  - JSONWriter - JSON写入器
  - 轻量级JSON处理工具

- **[Xml API](Xml-API.md)** - XML处理
  - XmlUtils - XML序列化和反序列化工具
  - 支持对象与XML字符串转换

### 加密和安全模块

- **[Crypto API](Crypto-API.md)** - 加密解密功能
  - ICipher, IAesCipher, IRsaCipher - 加密接口
  - AesCipher, RsaCipher - 加密实现
  - 支持AES、RSA、DES等多种加密算法

- **[Encodingx API](Encodingx-API.md)** - 编码处理
  - IBase64Encoding - Base64编码接口
  - Base64Utils - Base64工具类
  - 支持标准、URL安全、无填充等格式

### 事件和通信模块

- **[Event API](Event-API.md)** - 事件系统
  - IEventListener, IEventDispatcher - 事件接口
  - EventDispatcher, EventManager - 事件管理器
  - 支持同步和异步事件处理

- **[Net API](Net-API.md)** - 网络通信
  - ISocketClient, ISocketConn - 套接字接口
  - SocketClient, SocketReceiver, SocketSender - 网络组件
  - 支持TCP、HTTP等协议

### 服务管理模块

- **[Service API](Service-API.md)** - 服务管理
  - IService, IInitService - 服务接口
  - ServiceManager - 服务管理器
  - 支持服务生命周期管理

- **[Serial API](Serial-API.md)** - 串行化管理
  - ISerialManager, ISerialModule - 串行化接口
  - SerialManager - 串行化管理器
  - 支持模块串行启动和停止

### 算法和数学模块

- **[Algs API](Algs-API.md)** - 算法实现
  - IAStarAlg, IAStarGridMap - A*算法接口
  - AStarAlg, AStarGridMap - A*寻路算法
  - 支持网格地图寻路

- **[Mathx API](Mathx-API.md)** - 数学工具
  - Point2Int, Point3Int - 点结构
  - Bounds2Int, Array2D - 几何结构
  - MathUtil - 数学工具类

### 工具和扩展模块

- **[Utils API](Utils-API.md)** - 实用工具
  - ArrayUtil, BitUtil - 数组和位操作
  - FileUtil, PathUtil - 文件和路径处理
  - TextUtil, DirectoryUtil - 文本和目录操作
  - 加密工具：DESUtil, RijandelUtil, RSAUtil

- **[Extensions API](Extensions-API.md)** - 扩展方法
  - ExtString - 字符串扩展
  - ExtDouble, ExtFloat - 数值扩展
  - 富文本格式化和正则表达式支持

### 时间和数据管理模块

- **[DateTimex API](DateTimex-API.md)** - 时间处理
  - DateTimeUtil - 日期时间工具
  - TimeSeries, TimeSlice - 时间序列
  - StampTimer - 时间戳计时器

- **[Pool API](Pool-API.md)** - 对象池
  - ReuseObjectPool<T> - 重用对象池
  - KVObjectPool<K,V> - 键值对象池
  - MetaObjectPool<T> - 元数据对象池

### 线程和并发模块

- **[Threadx API](Threadx-API.md)** - 线程处理
  - FixedThreadContext - 固定线程上下文
  - ContextWorkRequest - 上下文工作请求
  - 支持线程池和工作队列

### 文件和数据模块

- **[Archive API](Archive-API.md)** - 归档处理
  - IUnarchive - 解压接口
  - Unzip, ArchiveUtil - 解压工具
  - 支持压缩文件处理

### 图像处理模块

- **[Imagex API](Imagex-API.md)** - 图像处理
  - IImage, IAlpha - 图像和透明度接口
  - RGBA - RGBA图像类
  - FilterKernel, FilterMatrix - 图像过滤器

### 脚本语言模块

- **[Languages API](Languages-API.md)** - 脚本语言
  - LuaInterpreter - Lua解释器
  - LuaValue - Lua值类型
  - 支持Lua脚本执行和标准库

## 快速开始

### 基本使用示例

```csharp
// 1. 事件系统
var eventDispatcher = new EventDispatcher();
eventDispatcher.AddEventListener("test", (evd) => Console.WriteLine("事件触发"));
eventDispatcher.DispatchEvent("test", null);

// 2. 服务管理
var serviceManager = new ServiceManager();
serviceManager.RegisterService(new MyService());
serviceManager.InitializeAll();

// 3. 网络通信
var socketClient = new SocketClient();
socketClient.Connect("127.0.0.1", 8080);

// 4. 加密处理
var aesCipher = new AesCipher();
byte[] encrypted = aesCipher.Encrypt(data, key);

// 5. JSON处理
var json = JSONWriter.ToJson(obj);
var obj = JSONParser.FromJson<MyClass>(json);
```

### 模块依赖关系

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

## 版本信息

- **框架版本：** Infra-CSharp
- **目标框架：** .NET Standard 2.0
- **C#版本：** 7.3+
- **文档版本：** 1.0

## 贡献指南

如需贡献代码或改进文档，请遵循以下规范：

1. 保持代码风格一致
2. 添加适当的XML注释
3. 包含中文和英文注释
4. 提供使用示例
5. 更新相关文档

## 许可证

本文档和相关代码遵循相应的开源许可证。

---

**注意：** 本文档涵盖了JLGames.Infra框架的所有主要模块。每个模块都有详细的API文档，包含接口定义、类说明、方法描述和使用示例。建议根据具体需求查阅相应的模块文档。 