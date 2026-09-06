# Infra-CSharp

[![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.0-blue.svg)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
[![C#](https://img.shields.io/badge/C%23-7.3+-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![CI](https://github.com/xuzhuoxi/Infra-CSharp/actions/workflows/CI.yml/badge.svg)](https://github.com/xuzhuoxi/Infra-CSharp/actions/workflows/CI.yml)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

A comprehensive C# infrastructure framework providing a rich set of utility tools and functional modules for .NET applications.

## 📋 Table of Contents

- [Overview](#overview)
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

Infra-CSharp is a feature-rich C# infrastructure framework designed to provide developers with a comprehensive set of tools and utilities for building robust .NET applications. The framework is built on .NET Standard 2.0, ensuring cross-platform compatibility across Windows, macOS, and Linux.

### Key Benefits

- **Modular Design**: Well-organized modules for different functional areas
- **Cross-Platform**: Built on .NET Standard 2.0 for maximum compatibility
- **Performance Optimized**: Efficient implementations with performance considerations
- **Comprehensive Documentation**: Detailed API documentation in both English and Chinese
- **Extensible Architecture**: Easy to extend and customize for specific needs

## ✨ Features

### 🔐 Security & Cryptography
- **AES Encryption**: Advanced Encryption Standard with multiple modes (CBC, CTR, GCM)
- **RSA Encryption**: Asymmetric encryption with key management
- **DES Encryption**: Data Encryption Standard support
- **Hash Functions**: SHA, MD5, and custom hash implementations
- **Key Management**: Diffie-Hellman key exchange and key derivation
- **ASN.1 Support**: Certificate and key format processing

### 🌐 Networking & Communication
- **Socket Communication**: TCP/UDP/Websocket socket implementations
- **HTTP Client**: HTTP request/response handling
- **Message Framing**: Network message serialization/deserialization
- **Connection Management**: Connection pooling and lifecycle management

### 📡 Event System
- **Event Dispatcher**: Synchronous and asynchronous event processing
- **Event Groups**: Organized event management with priorities
- **Event Pooling**: Efficient event object reuse
- **Event Filtering**: Tag-based event filtering and management

### 🗄️ Data Processing
- **Buffer Management**: Efficient byte buffer operations
- **JSON Processing**: Lightweight JSON parser and writer
- **XML Processing**: XML serialization and deserialization
- **Base64 Encoding**: Multiple Base64 encoding formats (standard, URL-safe, no-padding)

### 🧮 Algorithms & Mathematics
- **A* Pathfinding**: Grid-based pathfinding algorithm
- **Mathematical Utilities**: Point, vector, and geometric operations
- **Array Operations**: 2D array utilities and matrix operations
- **Bit Operations**: Efficient bit manipulation utilities

### 🖼️ Image Processing
- **RGBA Image Support**: 32-bit RGBA image processing
- **Image Filters**: Kernel-based image filtering
- **Alpha Channel**: Transparency processing
- **Image Transformations**: Rotation, flipping, and scaling

### 🔧 Utilities & Extensions
- **String Extensions**: Rich text formatting and regex support
- **File Operations**: File and directory utilities
- **Path Management**: Cross-platform path operations
- **Text Processing**: String manipulation and validation

### ⏰ Time & Date
- **DateTime Utilities**: Enhanced date/time operations
- **Time Series**: Time-based data structures
- **Timestamp Management**: High-precision timing utilities

### 🎮 Scripting
- **Lua Interpreter**: Embedded Lua scripting engine
- **Script Execution**: Runtime script evaluation
- **Standard Library**: Lua standard library support

### 🏗️ Service Management
- **Service Lifecycle**: Service initialization and cleanup
- **Dependency Injection**: Service argument injection
- **Progress Tracking**: Service execution progress monitoring
- **Event-Driven Architecture**: Service communication via events

### 🧵 Threading & Concurrency
- **Thread Contexts**: Fixed thread context management
- **Work Queues**: Asynchronous work processing
- **Thread Pools**: Efficient thread resource management

### 📦 Object Pooling
- **Reusable Pools**: Generic object pooling
- **Key-Value Pools**: Specialized key-value object pools
- **Metadata Pools**: Metadata-aware object pooling

## 🛠️ Technology Stack

### Core Technologies
- **.NET Standard 2.0**: Cross-platform compatibility
- **C# 7.3+**: Modern C# language features
- **NUnit**: Unit testing framework
- **MSBuild**: Build system

### Dependencies
- **Portable.BouncyCastle**: Cryptographic operations (optional)
- **System.Text.RegularExpressions**: Regular expression support
- **System.Collections.Generic**: Generic collections

### Build Tools
- **MSBuild**: Primary build system
- **PowerShell**: Build automation scripts
- **Batch Scripts**: Windows build automation

## 📦 Installation

### Prerequisites
- .NET Standard 2.0 compatible runtime
- Visual Studio 2019+ or .NET CLI
- PowerShell (for build scripts)

### Building from Source

1. **Clone the repository**
   ```bash
   git clone https://github.com/xuzhuoxi/Infra-CSharp.git
   cd Infra-CSharp
   ```

2. **Build the solution**
   ```bash
   # Using MSBuild
   msbuild Infra-CSharp.sln /p:Configuration=Release
   
   # Using .NET CLI
   dotnet build Infra-CSharp.sln --configuration Release
   
   # Using PowerShell script
   .\Build\build-release.ps1
   ```

3. **Run tests**
   ```bash
   dotnet test Infra-Tests/Infra-Tests.csproj
   ```

### Using as NuGet Package (Future)

```xml
<PackageReference Include="JLGames.Infra" Version="1.0.0" />
```

## 🚀 Quick Start

### Basic Event System Usage

```csharp
using JLGames.Infra.Event;

// Create event dispatcher
var dispatcher = new EventDispatcher();

// Add event listener
dispatcher.AddEventListener("user.login", (evd) => {
    Console.WriteLine($"User logged in: {evd.Data}");
});

// Dispatch event
dispatcher.DispatchEvent("user.login", new { userId = 123, username = "john" });
```

### Encryption Example

```csharp
using JLGames.Infra.Crypto;

// AES encryption
var aesCipher = new AesCipher();
byte[] key = Encoding.UTF8.GetBytes("MySecretKey12345");
byte[] data = Encoding.UTF8.GetBytes("Hello, World!");

byte[] encrypted = aesCipher.Encrypt(data, key);
byte[] decrypted = aesCipher.Decrypt(encrypted, key);

Console.WriteLine(Encoding.UTF8.GetString(decrypted)); // "Hello, World!"
```

### Network Communication

```csharp
using JLGames.Infra.Net;

// Create socket client
var client = new SocketClient();
client.Connect("127.0.0.1", 8080);

// Send data
byte[] message = Encoding.UTF8.GetBytes("Hello Server!");
client.Send(message);
```

### JSON Processing

```csharp
using JLGames.Infra.TinyJson;

// Serialize object to JSON
var person = new { Name = "John", Age = 30 };
string json = JSONWriter.ToJson(person);

// Deserialize JSON to object
var deserialized = JSONParser.FromJson<dynamic>(json);
```

## 📚 Modules

### Core Modules
- **[Infra](Infra-CSharp-API/en/Infra-API.md)** - Core interfaces and base classes
- **[Event](Infra-CSharp-API/en/Event-API.md)** - Event system and dispatching
- **[Service](Infra-CSharp-API/en/Service-API.md)** - Service lifecycle management

### Data & Communication
- **[Buffer](Infra-CSharp-API/en/Buffer-API.md)** - Byte buffer operations
- **[Net](Infra-CSharp-API/en/Net-API.md)** - Network communication
- **[TinyJson](Infra-CSharp-API/en/TinyJson-API.md)** - JSON processing
- **[Xml](Infra-CSharp-API/en/Xml-API.md)** - XML processing

### Security & Encoding
- **[Crypto](Infra-CSharp-API/en/Crypto-API.md)** - Encryption and security
- **[Encodingx](Infra-CSharp-API/en/Encodingx-API.md)** - Encoding utilities

### Algorithms & Mathematics
- **[Algs](Infra-CSharp-API/en/Algs-API.md)** - Algorithm implementations (A* pathfinding)
- **[Mathx](Infra-CSharp-API/en/Mathx-API.md)** - Mathematical utilities

### Utilities & Extensions
- **[Utils](Infra-CSharp-API/en/Utils-API.md)** - General utilities
- **[Extensions](Infra-CSharp-API/en/Extensions-API.md)** - Extension methods
- **[DateTimex](Infra-CSharp-API/en/DateTimex-API.md)** - Time utilities

### Advanced Features
- **[Imagex](Infra-CSharp-API/en/Imagex-API.md)** - Image processing
- **[Languages](Infra-CSharp-API/en/Languages-API.md)** - Scripting (Lua)
- **[Pool](Infra-CSharp-API/en/Pool-API.md)** - Object pooling
- **[Threadx](Infra-CSharp-API/en/Threadx-API.md)** - Threading utilities

## 💡 Examples

### A* Pathfinding Algorithm

```csharp
using JLGames.Infra.Algs;

// Create grid map
var gridMap = new AStarGridMap(10, 10);
gridMap.SetWalkable(5, 5, false); // Set obstacle

// Create A* algorithm
var astar = new AStarAlg();
astar.SetGridMap(gridMap);

// Find path
var path = astar.FindPath(new Position(0, 0), new Position(9, 9));
foreach (var pos in path)
{
    Console.WriteLine($"Path: ({pos.X}, {pos.Y})");
}
```

### Service Management

```csharp
using JLGames.Infra.Service;

// Create service
public class UserService : ServiceBase, IInitService
{
    public void Init()
    {
        Console.WriteLine("UserService initialized");
        InvokeInited();
    }
}

// Register and initialize services
var serviceManager = ServiceManager.Shared;
serviceManager.RegisterService(new UserService());
serviceManager.InitializeAll();
```

### Image Processing

```csharp
using JLGames.Infra.Imagex;

// Create RGBA image
var image = new RGBA(256, 256);

// Set pixel color
uint redColor = 0xFF0000FF; // Red
image.Set(100, 100, redColor);

// Set transparency
image.SetAlpha(100, 100, 128); // Semi-transparent
```

## 📖 Documentation

Comprehensive API documentation is available in both English and Chinese:

- **[English Documentation](Infra-CSharp-API/en/README.md)**
- **[Chinese Documentation](Infra-CSharp-API/cn/README.md)**

Each module has detailed documentation including:
- Interface definitions
- Class descriptions
- Method documentation
- Usage examples
- Performance considerations

## 🔨 Building

### Build Scripts

The project includes several build scripts for different scenarios:

```bash
# Debug build
.\Build\build-debug.bat

# Release build
.\Build\build-release.bat

# PowerShell build
.\Build\build.ps1 -configuration Release

# Publish
.\Build\publish-release.bat
```

### Build Outputs

- **Debug**: `Infra-CSharp/bin/Debug/netstandard2.0/`
- **Release**: `Infra-CSharp/bin/Release/netstandard2.0/`
- **Tests**: `Infra-Tests/bin/Debug/net8.0/`

## 🧪 Testing

The project uses NUnit for unit testing:

```bash
# Run all tests
dotnet test Infra-Tests/Infra-Tests.csproj

# Run specific test category
dotnet test --filter "Category=Crypto"

# Same as CI: skip tests that need a local server
dotnet test --filter "Category!=RunOnlyThis"

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

Pushes and pull requests to `master` run build and tests via GitHub Actions (see `.github/workflows/CI.yml`). Tagging `v*.*.*` for a release is documented in [Release.md](.github/workflows/Release.md).

### Test Categories

- **Crypto**: Encryption and security tests
- **Net**: Network communication tests
- **Threadx**: Threading utility tests

## 🤝 Contributing

We welcome contributions! Please follow these guidelines:

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Make your changes** with appropriate tests
4. **Add documentation** for new features
5. **Ensure all tests pass**
6. **Submit a pull request**

### Code Style Guidelines

- Follow C# coding conventions
- Add XML documentation comments
- Include both English and Chinese comments
- Write unit tests for new features
- Update relevant documentation

### Development Setup

1. Install Visual Studio 2019+ or VS Code
2. Install .NET SDK
3. Clone the repository
4. Open `Infra-CSharp.sln` in your IDE
5. Build and run tests

## 🙏 Acknowledgments

- **Lua Interpreter**: Third-party Lua interpreter library by Liu Junfeng
- **BouncyCastle**: Cryptographic library for .NET
- **NUnit**: Unit testing framework

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/xuzhuoxi/Infra-CSharp/issues)
- **Documentation**: [API Documentation](Infra-CSharp-API/en/README.md)
- **Examples**: See the [Examples](#examples) section above
- **Author**: xuzhuoxi
- **Email**: xuzhuoxi@gmail.com / mailxuzhuoxi@163.com / m_xuzhuoxi@outlook.com
- **GitHub**: [@xuzhuoxi](https://github.com/xuzhuoxi)


## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details. 