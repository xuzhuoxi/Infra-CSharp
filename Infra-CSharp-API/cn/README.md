# JLGames.Infra API 文档总览

## 概述

JLGames.Infra 是一个功能丰富的 C# 基础设施框架，提供多种实用工具和功能模块。本文档包含所有模块的详细 API 参考，内容与当前源码公开 API 对齐。

## 模块列表

### 核心模块

- **[Infra API](Infra-API.md)** - 核心接口和基础类
  - `ICloneable<T>` - 泛型克隆接口
  - `Callback` - 绑定委托与参数的延迟回调

### 数据处理模块

- **[Buffer API](Buffer-API.md)** - 缓冲区操作
  - `IByteBuffer` / `IDataBuffer` 及 Reader / Writer / Copier 接口
  - `ByteBuffer`、`DataBuffer` - 字节与类型化数据缓冲
  - `EndianCoverter`、`ValueKind`、`CodingList` / `CodingMap` - 字节序、类型编码与序列化辅助

- **[TinyJson API](TinyJson-API.md)** - JSON 处理
  - `JSONParser.FromJson<T>` - 解析扩展方法
  - `JSONWriter.ToJson` - 序列化扩展方法
  - 轻量级 JSON 处理，支持 `DataMember` / `IgnoreDataMember`

- **[Xml API](Xml-API.md)** - XML 处理
  - `XmlUtils` - 对象与 XML 字符串互转（`ToXml` / `FromXml`）

### 加密和安全模块

- **[Crypto API](Crypto-API.md)** - 加密解密功能
  - `ICipher`、`IAesCipher`、`IDesCipher`、`IRsaCipher`、`IXorCipher` - 加密接口
  - `AesCipher`、`DesCipher`、`TripleDesCipher`、`XorCipher`、`RsaCipher` - 实现
  - 支持 AES（含 CTR / GCM）、RSA、DES、密钥交换（Diffie-Hellman）与 ASN.1/TLV
  - `DESUtil` / `RijndaelUtil` / `RSAUtil` 位于 `Deleted`，已弃用

- **[Encodingx API](Encodingx-API.md)** - 编码处理
  - `IBase64Encoding` - Base64 编码接口
  - `Base64StdEncoding` / `Base64RawStdEncoding` / `Base64UrlEncoding` / `Base64RawUrlEncoding`
  - `Base64Utils` - 标准、URL 安全、无填充等格式

### 事件和通信模块

- **[Event API](Event-API.md)** - 事件系统
  - `IEventListener`、`IEventDispatcher`、`IThreadEventDispatcher` - 事件接口
  - `EventDispatcher`、`EventManager`、`EventDispatcherPool`、`EventGroup`
  - 支持权重、标签、次数限制，以及线程上下文同步派发

- **[Net API](Net-API.md)** - 网络通信
  - `ISocketClient`、`ISocketConn`、`ISocketReceiver`、`ISocketSender` - 套接字接口
  - `SocketFactory`、`SocketClient`、`SocketParams` - 客户端与连接参数
  - 支持 TCP/UDP、消息读写与 HTTP 客户端代理

### 服务管理模块

- **[Service API](Service-API.md)** - 服务管理
  - `IService`、`IInitService`、`ILoadDataService`、`ISaveDataService` 等生命周期接口
  - `ServiceConfig` / `ServiceInfo` - 服务注册
  - `ServiceManager` - 初始化、加载、保存与进度事件

- **[Serial API](Serial-API.md)** - 串行模块管理
  - `ISerialManager`、`ISerialModule` - 串行接口
  - `SerialManager` - 按顺序启动/停止模块
  - `SerialStatus`、`SerialEvents` - 状态与完成事件

### 算法和数学模块

- **[Algs API](Algs-API.md)** - 算法实现（命名空间 `JLGames.Infra.AStar`）
  - `IAStarAlg`、`IAStarGridMap` - A* 接口
  - `AStarAlg`、`AStarGridMap`、`AStarUtil` - 网格寻路
  - 方向组、优先队列等辅助类型

- **[Mathx API](Mathx-API.md)** - 数学工具
  - `Point1` / `Point1Int`、`Point2Int`、`Point3Int` - 点结构
  - `Bounds2Int`、`Array2D`、`Line1`、`Interval`、`Range` - 几何与区间
  - `BitMark`、`BitFixedData`、`MathUtil` - 位标记与数值工具

### 工具和扩展模块

- **[Utils API](Utils-API.md)** - 实用工具
  - `ArrayUtil`、`BitUtil` - 数组和位操作
  - `FileUtil`、`PathUtil`、`DirectoryUtil`、`TextUtil` - 文件、路径与文本
  - `ConfusedUtil`、`PrintUtil`、`ReflexUtil` - 哈希混淆、打印与反射

- **[Extensions API](Extensions-API.md)** - 扩展方法
  - `ExtString` - 富文本格式化、正则拆分
  - `ExtDouble`、`ExtFloat` - 近似相等与数值扩展

### 时间和数据管理模块

- **[DateTimex API](DateTimex-API.md)** - 时间处理
  - `DateTimeUtil` - 时间戳与格式化
  - `TimeSeries`、`TimeSlice` - 命名时间片
  - `StampTimer` - 可暂停时间戳计时器

- **[Pool API](Pool-API.md)** - 对象池
  - `ReuseObjectPool<T>` - 可取出/归还的重用对象池
  - `KVObjectPool<TKey, TValue>` - 键值映射池（取值 / 克隆）
  - `MetaObjectPool<T>` - 基于原型的数量池

### 线程和并发模块

- **[Threadx API](Threadx-API.md)** - 线程处理
  - `FixedThreadContext` - 固定线程上下文（后台消费或外部泵）
  - `ContextWorkRequest` - 上下文工作请求
  - 支持工作队列、`ProcessTasks` 与 `StopExec`

### 文件和数据模块

- **[Archive API](Archive-API.md)** - 归档处理
  - `IUnarchive` - 解压接口
  - `Unzip`、`ArchiveUtil.UnzipFile` / `UnzipFiles` - ZIP 解压
  - 支持覆盖策略与解压进度事件

### 图像处理模块

- **[Imagex API](Imagex-API.md)** - 图像处理
  - `IImage`、`IAlpha` - 图像和透明度接口
  - `RGBA` - RGBA 图像（颜色格式 `0xRRGGBBAA`）
  - `FilterKernel`、`FilterMatrix` - 卷积核与滤波矩阵变换

### 脚本语言模块

- **[Languages API](Languages-API.md)** - 脚本语言
  - `LuaInterpreter` - 静态入口：`Interpreter` / `RunFile` / `Parse`
  - `LuaValue` 及数字、布尔、表、Userdata 等类型
  - 标准库与 `LuaInterpreterExtra` 宿主扩展

## 快速开始

### 基本使用示例

```csharp
using JLGames.Infra;
using JLGames.Infra.Event;
using JLGames.Infra.Service;
using JLGames.Infra.Net;
using JLGames.Infra.Crypto.Symmetric;
using JLGames.Infra.TinyJson;

// 1. 事件系统
var eventDispatcher = new EventDispatcher();
eventDispatcher.AddEventListener("test", evd => Console.WriteLine("事件触发"));
eventDispatcher.DispatchEvent("test", null);

// 2. 服务管理
ServiceConfig.Shared.AddConfig(new ServiceInfo("MyService", new MyService()));
ServiceManager.Shared.StartInitalization(new Callback(_ =>
{
    Console.WriteLine("初始化完成");
}));

// 3. 网络通信
ISocketClient socketClient = SocketFactory.CreateSocketClient("demo", true, false);
socketClient.ConnectServer(new SocketParams
{
    Network = SocketNetworks.Network.Tcp,
    RemoteAddress = "127.0.0.1:8080"
});

// 4. 加密处理
byte[] key = /* 16/24/32 字节 AES 密钥 */;
var aesCipher = new AesCipher(key);
byte[] encrypted = aesCipher.Encrypt(data);

// 5. JSON 处理
string json = obj.ToJson();
MyClass parsed = json.FromJson<MyClass>();
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
- **C# 版本：** 7.3+
- **文档：** 与当前源码公开 API 同步

## 贡献指南

如需贡献代码或改进文档，请遵循以下规范：

1. 保持代码风格一致
2. 添加适当的 XML 注释
3. 包含中文和英文注释
4. 提供使用示例
5. 更新相关文档

## 许可证

本文档和相关代码遵循 [MIT License](../../LICENSE)。

---

**注意：** 本文档涵盖 JLGames.Infra 框架的所有主要模块。每个模块都有详细的 API 文档，包含接口定义、类说明、方法描述和使用示例。建议根据具体需求查阅相应的模块文档。签名以源码为准；源码中的历史拼写（如 `EndianCoverter`、`StartInitalization`、`enviroment`）在文档中保持原样。
