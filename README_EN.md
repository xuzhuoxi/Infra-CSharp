# Infra-CSharp

[![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.0-blue.svg)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
[![C#](https://img.shields.io/badge/C%23-7.3+-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![CI](https://github.com/xuzhuoxi/Infra-CSharp/actions/workflows/CI.yml/badge.svg)](https://github.com/xuzhuoxi/Infra-CSharp/actions/workflows/CI.yml)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

[简体中文](README.md) | English

A comprehensive C# infrastructure framework providing a rich set of tools and modules for .NET applications. The Go counterpart is [infra-go](https://github.com/xuzhuoxi/infra-go).

## 📋 Table of Contents

- [Overview](#overview)
- [Notes](#notes)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Modules](#modules)
- [Examples](#examples)
- [Documentation](#documentation)
- [Building](#building)
- [Testing](#testing)
- [Contributing](#contributing)
- [Acknowledgments](#acknowledgments)
- [Support](#support)
- [License](#license)

## 🎯 Overview

Infra-CSharp is a C# infrastructure library for building .NET applications. It targets .NET Standard 2.0 and runs on Windows, macOS, and Linux.

This repository is designed to align with [infra-go](https://github.com/xuzhuoxi/infra-go) in module layout and protocol design (networking, buffers, cryptography, events, services), so C# and Go processes can interoperate.

### Key Benefits

- **Modular**: Namespaces organized by concern
- **Cross-platform**: .NET Standard 2.0
- **Bilingual docs**: API reference under `Infra-CSharp-API/`
- **Extensible**: Interface-driven implementations

## ⚠️ Notes

- **Match release tags when talking to infra-go.** If you use this library's networking module (`JLGames.Infra.Net`) with [infra-go](https://github.com/xuzhuoxi/infra-go), both sides should use the **same git tag** (for example both `v1.0.3`). Message framing, endianness, and socket parameters evolve across versions; mixing tags can break handshake or parsing.
- Public APIs follow the source. Historical spellings in source (such as `EndianCoverter`, `StartInitalization`) are kept as-is in docs and samples.
- Distribution is currently GitHub Release zip packages of Debug/Release DLLs. NuGet packing is not enabled yet.

## ✨ Features

### 🔐 Security & Cryptography

- **AES**: CBC, CTR, GCM (GCM by default); 16/24/32-byte keys
- **RSA**: Asymmetric encryption and key helpers
- **DES / 3DES / XOR**: Symmetric ciphers
- **Hashing & keys**: MD5, SHA, Diffie-Hellman, key derivation
- **ASN.1 / TLV**: Certificate and key formats

### 🌐 Networking & Communication

- **Sockets**: `SocketFactory` + `SocketParams` for TCP/UDP (WebSocket/QUIC reserved)
- **HTTP client proxy**: Request/response helpers
- **Message framing**: Length-prefixed read/write
- **interop with infra-go `netx`**: same tag required; see [Notes](#notes)

### 📡 Event System

- **Dispatcher**: Weights, tags, listen-count limits
- **Groups / pool**: `EventGroup`, `EventDispatcherPool`
- **Thread-context dispatch**: `IThreadEventDispatcher`

### 🗄️ Data Processing

- **Buffers**: `ByteBuffer` / `DataBuffer`
- **JSON**: `JSONParser` / `JSONWriter` extension methods
- **XML**: `XmlUtils`
- **Base64**: Standard, URL-safe, no-padding

### 🧮 Algorithms & Mathematics

- **A\* pathfinding**: Namespace `JLGames.Infra.AStar`
- **Geometry**: Points, bounds, lines, intervals, `Array2D`
- **Bit utilities**: `BitMark`, `BitUtil`

### 🖼️ Image Processing

- **RGBA**: 32-bit images, color format `0xRRGGBBAA`
- **Filters**: `FilterKernel` / `FilterMatrix`

### 🔧 Utilities & Extensions

- **String extensions**: Rich-text formatting, regex split
- **File / path / directory / text**
- **Hashing, printing, reflection**: `ConfusedUtil`, `PrintUtil`, `ReflexUtil`

### ⏰ Time & Date

- **DateTime utilities**, named time slices, pausable `StampTimer`

### 🎮 Scripting

- **Lua interpreter**: Static `Interpreter` / `RunFile` / `Parse`
- **Standard libraries** and `LuaInterpreterExtra`

### 🏗️ Services & Serial Modules

- **Lifecycle**: Init, load/save data, progress events
- **Registration**: `ServiceConfig.AddConfig` + `ServiceInfo`
- **Sequential modules**: `SerialManager`

### 🧵 Threading & Pools

- **Fixed thread context**: Background consumer or external `ProcessTasks`
- **Reuse / key-value / prototype-sized pools**

### 📦 Archives

- **ZIP extraction**: `ArchiveUtil.UnzipFile` / `UnzipFiles`

## 🛠️ Technology Stack

### Core

- **.NET Standard 2.0** (library)
- **C# 7.3+** (library; tests use C# 10 / net8.0)
- **NUnit 3**
- **MSBuild / .NET CLI**

### Dependencies

The library currently has **no required third-party NuGet packages**. AES CTR/GCM uses the built-in engines by default. BouncyCastle-backed implementations exist in source, but the package reference is not enabled in the project file.

### Build Tools

- **dotnet CLI / MSBuild**
- **PowerShell** (`Build/build.ps1`)
- **Batch scripts** (`Build/build-*.bat`, `publish-*.bat`)

## 📦 Installation

### Prerequisites

- A .NET Standard 2.0 compatible runtime
- Visual Studio 2019+ or the .NET SDK (CI uses .NET 8 to build and test)

### Building from Source

1. **Download from GitHub Releases** (no local build required)

   From [GitHub Releases](https://github.com/xuzhuoxi/Infra-CSharp/releases), download the zip for the target tag (names like `Infra-CSharp_<tag>_release_netstandard2.0.zip` or `..._debug_...`), then reference `Infra-CSharp.dll`. The archive also includes `.pdb`, `.deps.json`, and license/readme files. NuGet packing is not enabled yet.

   For networking with [infra-go](https://github.com/xuzhuoxi/infra-go), download the **same tag**; see [Notes](#notes).

2. **Clone**

   ```bash
   git clone https://github.com/xuzhuoxi/Infra-CSharp.git
   cd Infra-CSharp
   ```

3. **Build**

   ```bash
   # .NET CLI (recommended)
   dotnet build Infra-CSharp.sln --configuration Release

   # MSBuild
   msbuild Infra-CSharp.sln /p:Configuration=Release

   # PowerShell
   .\Build\build.ps1 -configuration Release

   # Windows batch
   .\Build\build-release.bat
   ```

4. **Test**

   ```bash
   dotnet test Infra-Tests/Infra-Tests.csproj
   ```

## 🚀 Quick Start

### Event System

```csharp
using JLGames.Infra.Event;

var dispatcher = new EventDispatcher();
dispatcher.AddEventListener("user.login", evd =>
{
    Console.WriteLine($"User logged in: {evd.Data}");
});
dispatcher.DispatchEvent("user.login", new { userId = 123, username = "john" });
```

### AES Encryption

```csharp
using System.Text;
using JLGames.Infra.Crypto.Symmetric;

byte[] key = Encoding.UTF8.GetBytes("MySecretKey12345"); // 16 bytes → AES-128
byte[] data = Encoding.UTF8.GetBytes("Hello, World!");

var aesCipher = new AesCipher(key); // GCM by default
byte[] encrypted = aesCipher.Encrypt(data);
byte[] decrypted = aesCipher.Decrypt(encrypted);

Console.WriteLine(Encoding.UTF8.GetString(decrypted));
```

### Networking

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

When interoperating with [infra-go](https://github.com/xuzhuoxi/infra-go), use the **same tag** on both sides.

### JSON

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

## 📚 Modules

### Core

- **[Infra](Infra-CSharp-API/en/Infra-API.md)** - `ICloneable<T>`, `Callback`
- **[Event](Infra-CSharp-API/en/Event-API.md)** - Event system
- **[Service](Infra-CSharp-API/en/Service-API.md)** - Service lifecycle
- **[Serial](Infra-CSharp-API/en/Serial-API.md)** - Sequential module start/stop

### Data & Communication

- **[Buffer](Infra-CSharp-API/en/Buffer-API.md)** - Byte and typed buffers
- **[Net](Infra-CSharp-API/en/Net-API.md)** - Networking (same tag required for infra-go)
- **[TinyJson](Infra-CSharp-API/en/TinyJson-API.md)** - JSON
- **[Xml](Infra-CSharp-API/en/Xml-API.md)** - XML
- **[Archive](Infra-CSharp-API/en/Archive-API.md)** - ZIP extraction

### Security & Encoding

- **[Crypto](Infra-CSharp-API/en/Crypto-API.md)** - Cryptography
- **[Encodingx](Infra-CSharp-API/en/Encodingx-API.md)** - Base64 and related encodings

### Algorithms & Mathematics

- **[Algs](Infra-CSharp-API/en/Algs-API.md)** - A\* (`JLGames.Infra.AStar`)
- **[Mathx](Infra-CSharp-API/en/Mathx-API.md)** - Math and geometry

### Utilities

- **[Utils](Infra-CSharp-API/en/Utils-API.md)** - General utilities
- **[Extensions](Infra-CSharp-API/en/Extensions-API.md)** - Extension methods
- **[DateTimex](Infra-CSharp-API/en/DateTimex-API.md)** - Time utilities
- **[Pool](Infra-CSharp-API/en/Pool-API.md)** - Object pools
- **[Threadx](Infra-CSharp-API/en/Threadx-API.md)** - Thread contexts

### Other

- **[Imagex](Infra-CSharp-API/en/Imagex-API.md)** - Image processing
- **[Languages](Infra-CSharp-API/en/Languages-API.md)** - Lua interpreter

## 💡 Examples

### A\* Pathfinding

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
    Console.WriteLine("No path");
    return;
}
foreach (var pos in path)
    Console.WriteLine($"Path: ({pos.X}, {pos.Y})");
```

### Service Management

```csharp
using System;
using JLGames.Infra;
using JLGames.Infra.Service;

public class UserService : ServiceBase, IInitService
{
    public void Init()
    {
        Console.WriteLine("UserService initialized");
        InvokeInited();
    }
}

ServiceConfig.Shared.AddConfig(new ServiceInfo("UserService", new UserService()));
ServiceManager.Shared.StartInitalization(new Callback(_ =>
{
    Console.WriteLine("Initialization finished");
}));
```

### Image Processing

```csharp
using JLGames.Infra.Imagex;

var image = new RGBA(256, 256);
uint redColor = 0xFF0000FF; // 0xRRGGBBAA
image.Set(100, 100, redColor);
image.SetAlpha(100, 100, 128);
```

## 📖 Documentation

- **[English API docs](Infra-CSharp-API/en/README.md)**
- **[Chinese API docs](Infra-CSharp-API/cn/README.md)**

Release tagging is documented in [Release.md](.github/workflows/Release.md).

## 🔨 Building

```bash
# Debug
.\Build\build-debug.bat

# Release
.\Build\build-release.bat

# PowerShell (Release by default)
.\Build\build.ps1 -configuration Release

# Publish output
.\Build\publish-release.bat
```

### Outputs

- **Debug**: `Infra-CSharp/bin/Debug/netstandard2.0/`
- **Release**: `Infra-CSharp/bin/Release/netstandard2.0/`
- **Tests**: `Infra-Tests/bin/Debug/net8.0/` or `Release/net8.0/`

Pushing a `v*.*.*` tag whose commit is on `master` builds and uploads Release assets.

## 🧪 Testing

```bash
dotnet test Infra-Tests/Infra-Tests.csproj

# Same as CI: skip tests that need a local server
dotnet test --filter "Category!=RunOnlyThis"

dotnet test --collect:"XPlat Code Coverage"
```

Pushes and pull requests to `master` run CI (`.github/workflows/CI.yml`).

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Add tests and documentation
4. Ensure tests pass
5. Open a pull request

### Style

- Follow C# conventions
- Add bilingual XML comments on public APIs
- Update `Infra-CSharp-API/` when public APIs change

## 🙏 Acknowledgments

- **Lua Interpreter**: Third-party Lua interpreter by Liu Junfeng
- **NUnit**: Unit testing framework
- **infra-go**: The author's [Go infrastructure library](https://github.com/xuzhuoxi/infra-go); keep networking tags aligned

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/xuzhuoxi/Infra-CSharp/issues)
- **Documentation**: [API Documentation](Infra-CSharp-API/en/README.md)
- **Author**: xuzhuoxi
- **Email**: xuzhuoxi@gmail.com / mailxuzhuoxi@163.com / m_xuzhuoxi@outlook.com
- **GitHub**: [@xuzhuoxi](https://github.com/xuzhuoxi)

## 📄 License

This project is licensed under the MIT License — see [LICENSE](LICENSE).
