# Infra-CSharp

[![.NET Standard](https://img.shields.io/badge/.NET%20Standard-2.0-blue.svg)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
[![C#](https://img.shields.io/badge/C%23-7.3+-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![CI](https://github.com/xuzhuoxi/Infra-CSharp/actions/workflows/CI.yml/badge.svg)](https://github.com/xuzhuoxi/Infra-CSharp/actions/workflows/CI.yml)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

一个功能全面的C#基础设施框架，为.NET应用程序提供丰富的工具集和功能模块。

## 📋 目录

- [概述](#概述)
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
- [许可证](#许可证)

## 🎯 概述

Infra-CSharp是一个功能丰富的C#基础设施框架，旨在为开发者提供构建健壮.NET应用程序的全面工具集。该框架基于.NET Standard 2.0构建，确保在Windows、macOS和Linux上的跨平台兼容性。

### 主要优势

- **模块化设计**: 针对不同功能领域的良好组织模块
- **跨平台**: 基于.NET Standard 2.0，确保最大兼容性
- **性能优化**: 高效的实现，注重性能考虑
- **全面文档**: 中英文双语详细API文档
- **可扩展架构**: 易于扩展和定制特定需求

## ✨ 功能特性

### 🔐 安全与加密
- **AES加密**: 高级加密标准，支持多种模式（CBC、CTR、GCM）
- **RSA加密**: 非对称加密，支持密钥管理
- **DES加密**: 数据加密标准支持
- **哈希函数**: SHA、MD5和自定义哈希实现
- **密钥管理**: Diffie-Hellman密钥交换和密钥派生
- **ASN.1支持**: 证书和密钥格式处理

### 🌐 网络与通信
- **Socket通信**: TCP/UDP/Websocket socket实现
- **HTTP客户端**: HTTP请求/响应处理
- **消息框架**: 网络消息序列化/反序列化
- **连接管理**: 连接池和生命周期管理

### 📡 事件系统
- **事件分发器**: 同步和异步事件处理
- **事件组**: 带优先级的有组织事件管理
- **事件池**: 高效的事件对象重用
- **事件过滤**: 基于标签的事件过滤和管理

### 🗄️ 数据处理
- **缓冲区管理**: 高效的字节缓冲区操作
- **JSON处理**: 轻量级JSON解析器和写入器
- **XML处理**: XML序列化和反序列化
- **Base64编码**: 多种Base64编码格式（标准、URL安全、无填充）

### 🧮 算法与数学
- **A*寻路**: 基于网格的寻路算法
- **数学工具**: 点、向量和几何运算
- **数组操作**: 2D数组工具和矩阵运算
- **位操作**: 高效的位操作工具

### 🖼️ 图像处理
- **RGBA图像支持**: 32位RGBA图像处理
- **图像过滤器**: 基于内核的图像过滤
- **Alpha通道**: 透明度处理
- **图像变换**: 旋转、翻转和缩放

### 🔧 工具与扩展
- **字符串扩展**: 富文本格式化和正则表达式支持
- **文件操作**: 文件和目录工具
- **路径管理**: 跨平台路径操作
- **文本处理**: 字符串操作和验证

### ⏰ 时间与日期
- **DateTime工具**: 增强的日期/时间操作
- **时间序列**: 基于时间的数据结构
- **时间戳管理**: 高精度计时工具

### 🎮 脚本语言
- **Lua解释器**: 嵌入式Lua脚本引擎
- **脚本执行**: 运行时脚本求值
- **标准库**: Lua标准库支持

### 🏗️ 服务管理
- **服务生命周期**: 服务初始化和清理
- **依赖注入**: 服务参数注入
- **进度跟踪**: 服务执行进度监控
- **事件驱动架构**: 基于事件的服务通信

### 🧵 线程与并发
- **线程上下文**: 固定线程上下文管理
- **工作队列**: 异步工作处理
- **线程池**: 高效的线程资源管理

### 📦 对象池
- **可重用池**: 通用对象池
- **键值池**: 专门的键值对象池
- **元数据池**: 元数据感知的对象池

## 🛠️ 技术栈

### 核心技术
- **.NET Standard 2.0**: 跨平台兼容性
- **C# 7.3+**: 现代C#语言特性
- **NUnit**: 单元测试框架
- **MSBuild**: 构建系统

### 依赖项
- **Portable.BouncyCastle**: 加密操作（可选）
- **System.Text.RegularExpressions**: 正则表达式支持
- **System.Collections.Generic**: 泛型集合

### 构建工具
- **MSBuild**: 主要构建系统
- **PowerShell**: 构建自动化脚本
- **批处理脚本**: Windows构建自动化

## 📦 安装说明

### 前置要求
- .NET Standard 2.0兼容运行时
- Visual Studio 2019+或.NET CLI
- PowerShell（用于构建脚本）

### 从源码构建

1. **克隆仓库**
   ```bash
   git clone https://github.com/xuzhuoxi/Infra-CSharp.git
   cd Infra-CSharp
   ```

2. **构建解决方案**
   ```bash
   # 使用MSBuild
   msbuild Infra-CSharp.sln /p:Configuration=Release
   
   # 使用.NET CLI
   dotnet build Infra-CSharp.sln --configuration Release
   
   # 使用PowerShell脚本
   .\Build\build-release.ps1
   ```

3. **运行测试**
   ```bash
   dotnet test Infra-Tests/Infra-Tests.csproj
   ```

### 作为NuGet包使用（未来）

```xml
<PackageReference Include="JLGames.Infra" Version="1.0.0" />
```

## 🚀 快速开始

### 基本事件系统使用

```csharp
using JLGames.Infra.Event;

// 创建事件分发器
var dispatcher = new EventDispatcher();

// 添加事件监听器
dispatcher.AddEventListener("user.login", (evd) => {
    Console.WriteLine($"用户登录: {evd.Data}");
});

// 分发事件
dispatcher.DispatchEvent("user.login", new { userId = 123, username = "john" });
```

### 加密示例

```csharp
using JLGames.Infra.Crypto;

// AES加密
var aesCipher = new AesCipher();
byte[] key = Encoding.UTF8.GetBytes("MySecretKey12345");
byte[] data = Encoding.UTF8.GetBytes("Hello, World!");

byte[] encrypted = aesCipher.Encrypt(data, key);
byte[] decrypted = aesCipher.Decrypt(encrypted, key);

Console.WriteLine(Encoding.UTF8.GetString(decrypted)); // "Hello, World!"
```

### 网络通信

```csharp
using JLGames.Infra.Net;

// 创建socket客户端
var client = new SocketClient();
client.Connect("127.0.0.1", 8080);

// 发送数据
byte[] message = Encoding.UTF8.GetBytes("Hello Server!");
client.Send(message);
```

### JSON处理

```csharp
using JLGames.Infra.TinyJson;

// 序列化对象为JSON
var person = new { Name = "John", Age = 30 };
string json = JSONWriter.ToJson(person);

// 反序列化JSON为对象
var deserialized = JSONParser.FromJson<dynamic>(json);
```

## 📚 模块介绍

### 核心模块
- **[Infra](Infra-CSharp-API/cn/Infra-API.md)** - 核心接口和基类
- **[Event](Infra-CSharp-API/cn/Event-API.md)** - 事件系统和分发
- **[Service](Infra-CSharp-API/cn/Service-API.md)** - 服务生命周期管理

### 数据与通信
- **[Buffer](Infra-CSharp-API/cn/Buffer-API.md)** - 字节缓冲区操作
- **[Net](Infra-CSharp-API/cn/Net-API.md)** - 网络通信
- **[TinyJson](Infra-CSharp-API/cn/TinyJson-API.md)** - JSON处理
- **[Xml](Infra-CSharp-API/cn/Xml-API.md)** - XML处理

### 安全与编码
- **[Crypto](Infra-CSharp-API/cn/Crypto-API.md)** - 加密和安全
- **[Encodingx](Infra-CSharp-API/cn/Encodingx-API.md)** - 编码工具

### 算法与数学
- **[Algs](Infra-CSharp-API/cn/Algs-API.md)** - 算法实现（A*寻路）
- **[Mathx](Infra-CSharp-API/cn/Mathx-API.md)** - 数学工具

### 工具与扩展
- **[Utils](Infra-CSharp-API/cn/Utils-API.md)** - 通用工具
- **[Extensions](Infra-CSharp-API/cn/Extensions-API.md)** - 扩展方法
- **[DateTimex](Infra-CSharp-API/cn/DateTimex-API.md)** - 时间工具

### 高级功能
- **[Imagex](Infra-CSharp-API/cn/Imagex-API.md)** - 图像处理
- **[Languages](Infra-CSharp-API/cn/Languages-API.md)** - 脚本语言（Lua）
- **[Pool](Infra-CSharp-API/cn/Pool-API.md)** - 对象池
- **[Threadx](Infra-CSharp-API/cn/Threadx-API.md)** - 线程工具

## 💡 使用示例

### A*寻路算法

```csharp
using JLGames.Infra.Algs;

// 创建网格地图
var gridMap = new AStarGridMap(10, 10);
gridMap.SetWalkable(5, 5, false); // 设置障碍物

// 创建A*算法
var astar = new AStarAlg();
astar.SetGridMap(gridMap);

// 寻找路径
var path = astar.FindPath(new Position(0, 0), new Position(9, 9));
foreach (var pos in path)
{
    Console.WriteLine($"路径: ({pos.X}, {pos.Y})");
}
```

### 服务管理

```csharp
using JLGames.Infra.Service;

// 创建服务
public class UserService : ServiceBase, IInitService
{
    public void Init()
    {
        Console.WriteLine("UserService已初始化");
        InvokeInited();
    }
}

// 注册并初始化服务
var serviceManager = ServiceManager.Shared;
serviceManager.RegisterService(new UserService());
serviceManager.InitializeAll();
```

### 图像处理

```csharp
using JLGames.Infra.Imagex;

// 创建RGBA图像
var image = new RGBA(256, 256);

// 设置像素颜色
uint redColor = 0xFF0000FF; // 红色
image.Set(100, 100, redColor);

// 设置透明度
image.SetAlpha(100, 100, 128); // 半透明
```

## 📖 文档

提供中英文双语全面API文档：

- **[中文文档](Infra-CSharp-API/cn/README.md)**
- **[English Documentation](Infra-CSharp-API/en/README.md)**

每个模块都包含详细文档：
- 接口定义
- 类描述
- 方法文档
- 使用示例
- 性能考虑

## 🔨 构建说明

### 构建脚本

项目包含多个不同场景的构建脚本：

```bash
# Debug构建
.\Build\build-debug.bat

# Release构建
.\Build\build-release.bat

# PowerShell构建
.\Build\build.ps1 -configuration Release

# 发布
.\Build\publish-release.bat
```

### 构建输出

- **Debug**: `Infra-CSharp/bin/Debug/netstandard2.0/`
- **Release**: `Infra-CSharp/bin/Release/netstandard2.0/`
- **Tests**: `Infra-Tests/bin/Debug/net8.0/`

## 🧪 测试

项目使用NUnit进行单元测试：

```bash
# 运行所有测试
dotnet test Infra-Tests/Infra-Tests.csproj

# 运行特定测试类别
dotnet test --filter "Category=Crypto"

# 与 CI 相同：跳过依赖本机服务的用例
dotnet test --filter "Category!=RunOnlyThis"

# 运行覆盖率测试
dotnet test --collect:"XPlat Code Coverage"
```

推送到 `master` 或向 `master` 开 Pull Request 时，GitHub Actions 会构建并运行测试（见 `.github/workflows/CI.yml`）。打 `v*.*.*` tag 发版见 [Release 说明](.github/workflows/Release.md)。

### 测试类别

- **Crypto**: 加密和安全测试
- **Net**: 网络通信测试
- **Threadx**: 线程工具测试

## 🤝 贡献指南

我们欢迎贡献！请遵循以下指南：

1. **Fork仓库**
2. **创建功能分支**: `git checkout -b feature/amazing-feature`
3. **进行更改**并添加适当的测试
4. **为新功能添加文档**
5. **确保所有测试通过**
6. **提交Pull Request**

### 代码风格指南

- 遵循C#编码约定
- 添加XML文档注释
- 包含中英文注释
- 为新功能编写单元测试
- 更新相关文档

### 开发环境设置

1. 安装Visual Studio 2019+或VS Code
2. 安装.NET SDK
3. 克隆仓库
4. 在IDE中打开`Infra-CSharp.sln`
5. 构建并运行测试

## 🙏 致谢

- **Lua Interpreter**: 由Liu Junfeng提供的第三方Lua解释器库
- **BouncyCastle**: .NET加密库
- **NUnit**: 单元测试框架

## 📞 支持

- **问题反馈**: [GitHub Issues](https://github.com/xuzhuoxi/Infra-CSharp/issues)
- **文档**: [API文档](Infra-CSharp-API/cn/README.md)
- **示例**: 查看上面的[使用示例](#使用示例)部分
- **作者**：xuzhuoxi
- **邮箱**：xuzhuoxi@gmail.com / mailxuzhuoxi@163.com / m_xuzhuoxi@outlook.com
- **GitHub**: [@xuzhuoxi](https://github.com/xuzhuoxi)


## 📄 许可证

本项目采用MIT许可证 - 查看[LICENSE](LICENSE)文件了解详情。