# Infra-CSharp

[![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.0-blue.svg)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
[![C#](https://img.shields.io/badge/C%23-7.3+-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![CI](https://github.com/xuzhuoxi/Infra-CSharp/actions/workflows/CI.yml/badge.svg)](https://github.com/xuzhuoxi/Infra-CSharp/actions/workflows/CI.yml)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

简体中文 | [English](README_EN.md)

一个功能全面的 C# 基础设施框架，为 .NET 应用程序提供丰富的工具集和功能模块。Go 语言对应实现见 [infra-go](https://github.com/xuzhuoxi/infra-go)。

## 📋 目录

- [概述](#概述)
- [注意事项](#注意事项)
- [功能特性](#功能特性)
- [技术栈](#技术栈)
- [安装说明](#安装说明)
- [快速开始](#快速开始)
- [模块介绍](#模块介绍)
- [使用示例](#使用示例)
- [文档](#文档)
- [构建说明](#构建说明)
- [测试](#测试)
- [贡献指南](#贡献指南)
- [致谢](#致谢)
- [支持](#支持)
- [许可证](#许可证)

## 🎯 概述

Infra-CSharp 是一个功能丰富的 C# 基础设施框架，旨在为开发者提供构建健壮 .NET 应用程序的工具集。框架基于 .NET Standard 2.0，可在 Windows、macOS 和 Linux 上使用。

本仓库与 [infra-go](https://github.com/xuzhuoxi/infra-go) 在模块划分和协议设计上保持对应（网络、缓冲、加密、事件、服务等），便于 C# 与 Go 进程互通。

### 主要优势

- **模块化设计**: 按功能领域组织，可按需引用命名空间
- **跨平台**: 基于 .NET Standard 2.0
- **双语文档**: 中英文 API 参考见 `Infra-CSharp-API/`
- **可扩展**: 接口驱动，便于替换实现

## ⚠️ 注意事项

- **与 infra-go 对接请对齐版本 tag。** 使用本库网络模块（`JLGames.Infra.Net`）与 [infra-go](https://github.com/xuzhuoxi/infra-go) 互通时，双方应使用**相同 tag** 的发布版本（例如两侧都用 `v1.0.3`）。消息分帧、字节序和套接字参数随版本演进，跨 tag 组合可能导致无法握手或解析失败。
- 公开 API 以源码为准。源码中的历史拼写（如 `EndianCoverter`、`StartInitalization`）在文档与示例中保持原样。
- 当前以 GitHub Release 的 Debug/Release DLL 压缩包分发；NuGet 打包尚未启用。

## ✨ 功能特性

### 🔐 安全与加密

- **AES**: 支持 CBC、CTR、GCM（默认 GCM），密钥 16/24/32 字节
- **RSA**: 非对称加解密与密钥工具
- **DES / 3DES / XOR**: 对称算法
- **哈希与密钥**: MD5、SHA、Diffie-Hellman、密钥派生
- **ASN.1 / TLV**: 证书与密钥格式处理

### 🌐 网络与通信

- **Socket**: `SocketFactory` + `SocketParams`，支持 TCP/UDP（WebSocket/QUIC 预留）
- **HTTP 客户端代理**: 请求/响应辅助
- **消息框架**: 长度前缀消息读写
- **与 infra-go `netx` 互通**: 须使用相同 tag，见[注意事项](#注意事项)

### 📡 事件系统

- **事件分发器**: 权重、标签、监听次数
- **事件组 / 调度器池**: `EventGroup`、`EventDispatcherPool`
- **线程同步派发**: `IThreadEventDispatcher`

### 🗄️ 数据处理

- **缓冲区**: `ByteBuffer` / `DataBuffer`，读写、复制、编码
- **JSON**: `JSONParser` / `JSONWriter` 扩展方法
- **XML**: `XmlUtils` 对象与字符串互转
- **Base64**: 标准、URL 安全、无填充

### 🧮 算法与数学

- **A\* 寻路**: 命名空间 `JLGames.Infra.AStar`，网格地图
- **几何与区间**: 点、包围盒、线段、区间、`Array2D`
- **位操作**: `BitMark`、`BitUtil`

### 🖼️ 图像处理

- **RGBA**: 32 位图像，颜色格式 `0xRRGGBBAA`
- **滤波器**: `FilterKernel` / `FilterMatrix`（翻转、旋转）

### 🔧 工具与扩展

- **字符串扩展**: 富文本格式化、正则拆分
- **文件 / 路径 / 目录 / 文本**
- **哈希混淆、打印、反射**: `ConfusedUtil`、`PrintUtil`、`ReflexUtil`

### ⏰ 时间与日期

- **DateTime 工具**: 时间戳与格式化
- **时间片**: `TimeSeries` / `TimeSlice`
- **可暂停计时器**: `StampTimer`

### 🎮 脚本语言

- **Lua 解释器**: 静态入口 `Interpreter` / `RunFile` / `Parse`
- **标准库与宿主扩展**: `LuaInterpreterExtra`

### 🏗️ 服务与串行模块

- **服务生命周期**: 初始化、加载/保存数据、进度事件
- **配置注册**: `ServiceConfig.AddConfig` + `ServiceInfo`
- **串行模块**: `SerialManager` 按顺序启动/停止

### 🧵 线程与对象池

- **固定线程上下文**: 后台消费或外部泵 `ProcessTasks`
- **重用池 / 键值池 / 原型数量池**

### 📦 归档

- **ZIP 解压**: `ArchiveUtil.UnzipFile` / `UnzipFiles`

## 🛠️ 技术栈

### 核心技术

- **.NET Standard 2.0**: 类库目标框架
- **C# 7.3+**: 类库语言版本（测试项目为 C# 10 / net8.0）
- **NUnit 3**: 单元测试
- **MSBuild / .NET CLI**: 构建

### 依赖项

类库当前**无必选第三方 NuGet 包**。AES CTR/GCM 默认使用内置引擎；`Portable.BouncyCastle` 相关实现保留在源码中，但项目文件里的包引用默认未启用。

### 构建工具

- **dotnet CLI / MSBuild**
- **PowerShell**（`Build/build.ps1`）
- **批处理**（`Build/build-*.bat`、`publish-*.bat`）

## 📦 安装说明

### 前置要求

- 兼容 .NET Standard 2.0 的运行时
- Visual Studio 2019+ 或 .NET SDK（CI 使用 .NET 8 构建与测试）

### 从源码构建

1. **从 GitHub Release 下载**（无需自行编译）

   在 [GitHub Releases](https://github.com/xuzhuoxi/Infra-CSharp/releases) 下载与目标 tag 对应的 zip（文件名形如 `Infra-CSharp_<tag>_release_netstandard2.0.zip` 或 `..._debug_...`），解压后引用其中的 `Infra-CSharp.dll`。包内同时包含 `.pdb`、`.deps.json` 与许可证/说明文件。NuGet 打包暂未启用。

   若需与 [infra-go](https://github.com/xuzhuoxi/infra-go) 网络互通，请下载**相同 tag** 的版本，见[注意事项](#注意事项)。

2. **克隆仓库**

   ```bash
   git clone https://github.com/xuzhuoxi/Infra-CSharp.git
   cd Infra-CSharp
   ```

3. **构建解决方案**

   ```bash
   # .NET CLI（推荐）
   dotnet build Infra-CSharp.sln --configuration Release

   # MSBuild
   msbuild Infra-CSharp.sln /p:Configuration=Release

   # PowerShell
   .\Build\build.ps1 -configuration Release

   # Windows 批处理
   .\Build\build-release.bat
   ```

4. **运行测试**

   ```bash
   dotnet test Infra-Tests/Infra-Tests.csproj
   ```

## 🚀 快速开始

### 事件系统

```csharp
using JLGames.Infra.Event;

var dispatcher = new EventDispatcher();
dispatcher.AddEventListener("user.login", evd =>
{
    Console.WriteLine($"用户登录: {evd.Data}");
});
dispatcher.DispatchEvent("user.login", new { userId = 123, username = "john" });
```

### AES 加密

```csharp
using System.Text;
using JLGames.Infra.Crypto.Symmetric;

byte[] key = Encoding.UTF8.GetBytes("MySecretKey12345"); // 16 字节 → AES-128
byte[] data = Encoding.UTF8.GetBytes("Hello, World!");

var aesCipher = new AesCipher(key); // 默认 GCM
byte[] encrypted = aesCipher.Encrypt(data);
byte[] decrypted = aesCipher.Decrypt(encrypted);

Console.WriteLine(Encoding.UTF8.GetString(decrypted));
```

### 网络通信

```csharp
using System.Text;
using JLGames.Infra.Net;

ISocketClient client = SocketFactory.CreateSocketClient("demo", true, false);
client.ConnectServer(new SocketParams
{
    Network = SocketNetworks.Network.Tcp,
    RemoteAddress = "127.0.0.1:8080"
});
client.SendBytes(Encoding.UTF8.GetBytes("Hello Server!"));
```

与 Go 侧 [infra-go](https://github.com/xuzhuoxi/infra-go) 互通时，请使用**相同 tag** 的版本。

### JSON 处理

```csharp
using JLGames.Infra.TinyJson;

public class Person
{
    public string Name;
    public int Age;
}

string json = new Person { Name = "John", Age = 30 }.ToJson();
Person parsed = json.FromJson<Person>();
```

## 📚 模块介绍

### 核心模块

- **[Infra](Infra-CSharp-API/cn/Infra-API.md)** - `ICloneable<T>`、`Callback`
- **[Event](Infra-CSharp-API/cn/Event-API.md)** - 事件系统
- **[Service](Infra-CSharp-API/cn/Service-API.md)** - 服务生命周期
- **[Serial](Infra-CSharp-API/cn/Serial-API.md)** - 串行模块启动/停止

### 数据与通信

- **[Buffer](Infra-CSharp-API/cn/Buffer-API.md)** - 字节与类型化缓冲
- **[Net](Infra-CSharp-API/cn/Net-API.md)** - 网络通信（与 infra-go 对接须同 tag）
- **[TinyJson](Infra-CSharp-API/cn/TinyJson-API.md)** - JSON
- **[Xml](Infra-CSharp-API/cn/Xml-API.md)** - XML
- **[Archive](Infra-CSharp-API/cn/Archive-API.md)** - ZIP 解压

### 安全与编码

- **[Crypto](Infra-CSharp-API/cn/Crypto-API.md)** - 加密与安全
- **[Encodingx](Infra-CSharp-API/cn/Encodingx-API.md)** - Base64 等编码

### 算法与数学

- **[Algs](Infra-CSharp-API/cn/Algs-API.md)** - A\* 寻路（`JLGames.Infra.AStar`）
- **[Mathx](Infra-CSharp-API/cn/Mathx-API.md)** - 数学与几何

### 工具与扩展

- **[Utils](Infra-CSharp-API/cn/Utils-API.md)** - 通用工具
- **[Extensions](Infra-CSharp-API/cn/Extensions-API.md)** - 扩展方法
- **[DateTimex](Infra-CSharp-API/cn/DateTimex-API.md)** - 时间工具
- **[Pool](Infra-CSharp-API/cn/Pool-API.md)** - 对象池
- **[Threadx](Infra-CSharp-API/cn/Threadx-API.md)** - 线程上下文

### 其他

- **[Imagex](Infra-CSharp-API/cn/Imagex-API.md)** - 图像处理
- **[Languages](Infra-CSharp-API/cn/Languages-API.md)** - Lua 解释器

## 💡 使用示例

### A\* 寻路

```csharp
using System;
using JLGames.Infra.AStar;

var gridMap = new AStarGridMap();
gridMap.InitGridMap(new Size { Width = 10, Height = 10, Depth = 1 });

var mapData = new int[10][];
for (int y = 0; y < 10; y++)
    mapData[y] = new int[10];
mapData[5][5] = AstarConst.GridObstacle;
gridMap.SetMapData(mapData);
gridMap.SetAllowedDirections(DirectionsStatic.DefaultDirections2D);

Position[] path = gridMap.SearchPath(
    Positions.NewPosition(0, 0),
    Positions.NewPosition(9, 9));
if (path == null)
{
    Console.WriteLine("无路径");
    return;
}
foreach (var pos in path)
    Console.WriteLine($"路径: ({pos.X}, {pos.Y})");
```

### 服务管理

```csharp
using System;
using JLGames.Infra;
using JLGames.Infra.Service;

public class UserService : ServiceBase, IInitService
{
    public void Init()
    {
        Console.WriteLine("UserService 已初始化");
        InvokeInited();
    }
}

ServiceConfig.Shared.AddConfig(new ServiceInfo("UserService", new UserService()));
ServiceManager.Shared.StartInitalization(new Callback(_ =>
{
    Console.WriteLine("初始化完成");
}));
```

### 图像处理

```csharp
using JLGames.Infra.Imagex;

var image = new RGBA(256, 256);
uint redColor = 0xFF0000FF; // 0xRRGGBBAA
image.Set(100, 100, redColor);
image.SetAlpha(100, 100, 128);
```

## 📖 文档

- **[中文 API 文档](Infra-CSharp-API/cn/README.md)**
- **[English API Documentation](Infra-CSharp-API/en/README.md)**

各模块文档包含接口、类型、方法说明与示例。发版流程见 [Release 说明](.github/workflows/Release.md)。

## 🔨 构建说明

### 构建脚本

```bash
# Debug
.\Build\build-debug.bat

# Release
.\Build\build-release.bat

# PowerShell（默认 Release）
.\Build\build.ps1 -configuration Release

# 发布输出
.\Build\publish-release.bat
```

### 构建输出

- **Debug**: `Infra-CSharp/bin/Debug/netstandard2.0/`
- **Release**: `Infra-CSharp/bin/Release/netstandard2.0/`
- **Tests**: `Infra-Tests/bin/Debug/net8.0/` 或 `Release/net8.0/`

打 `v*.*.*` tag 且提交在 `master` 上时，GitHub Actions 会构建并上传 Release 附件。

## 🧪 测试

```bash
# 全部测试
dotnet test Infra-Tests/Infra-Tests.csproj

# 与 CI 相同：跳过依赖本机服务的用例（如本机 TCP/HTTP）
dotnet test --filter "Category!=RunOnlyThis"

# 覆盖率
dotnet test --collect:"XPlat Code Coverage"
```

推送到 `master` 或向 `master` 开 Pull Request 时会运行 CI（`.github/workflows/CI.yml`）。

## 🤝 贡献指南

欢迎贡献：

1. Fork 仓库
2. 创建功能分支: `git checkout -b feature/amazing-feature`
3. 添加测试与文档
4. 确保测试通过
5. 提交 Pull Request

### 代码风格

- 遵循 C# 编码约定
- 为公开 API 添加 XML 注释（中英双语）
- 为新功能编写单元测试并更新 `Infra-CSharp-API/` 文档

### 开发环境

1. 安装 Visual Studio 2019+ 或 VS Code / Rider，以及 .NET SDK
2. 克隆仓库并打开 `Infra-CSharp.sln`
3. 构建并运行测试

## 🙏 致谢

- **Lua Interpreter**: 由 Liu Junfeng 提供的第三方 Lua 解释器
- **NUnit**: 单元测试框架
- **infra-go**: 同作者的 [Go 基础设施库](https://github.com/xuzhuoxi/infra-go)，网络协议应对齐相同 tag

## 📞 支持

- **问题反馈**: [GitHub Issues](https://github.com/xuzhuoxi/Infra-CSharp/issues)
- **文档**: [API 文档](Infra-CSharp-API/cn/README.md)
- **作者**: xuzhuoxi
- **邮箱**: xuzhuoxi@gmail.com / mailxuzhuoxi@163.com / m_xuzhuoxi@outlook.com
- **GitHub**: [@xuzhuoxi](https://github.com/xuzhuoxi)

## 📄 许可证

本项目采用 MIT 许可证，详见 [LICENSE](LICENSE)。
