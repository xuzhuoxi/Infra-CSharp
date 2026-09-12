## Release Notes

+ 首个正式版本：提供面向 .NET Standard 2.0 的 JLGames.Infra 基础设施库（加密、网络、事件、服务、缓冲、寻路等），并配套 CI / Release 工作流与中英 API 文档。AES-CTR/GCM 改为内置实现，不再依赖 Portable.BouncyCastle。

### Known Issues

+ NuGet 打包尚未启用，目前通过 GitHub Release 的 Debug/Release zip 分发 DLL。
+ `SocketFactory` 对 WebSocket / QUIC 仍为预留，创建时返回 null。
+ 与 [infra-go](https://github.com/xuzhuoxi/infra-go) 网络互通时须使用相同 tag，否则消息分帧或解析可能失败。

### Notable Changes

+ 初始化 JLGames.Infra：AES / DES / XOR 对称加密、密钥派生，以及 RSA、ASN.1、PKCS#8 / X.509 密钥加载。
+ 自带 AES-CTR（`AesCtrEngine`）与 AES-GCM（`AesGcmEngine`），移除对 Portable.BouncyCastle 1.9.0 的依赖。
+ 从 RocketDriver 迁入纯 C# 能力并统一命名空间（缓冲、网络、事件、服务、对象池、线程、数学、A*、图像、Lua、归档等）。
+ 重写 Socket 连接与收发逻辑，补充 TCP 测试；`HttpClientProxy` 改为 async/await，并抽象 `IHttpClientProxy`。
+ 增加 `FixedThreadContext`、事件调度池，以及事件模块的多线程调度 / 线程上下文判断 / 调度器名称标记。
+ Base64 支持 Std、RawStd、Url、RawUrl 四种格式。
+ 接入 GitHub Actions：CI 构建测试、Release 产出 Debug/Release DLL（含 pdb 与 deps.json）、ReleaseNote 工作流；并提供 generate-note skill。
+ 补全公开 API 的中英 XML 注释，更新 `Infra-CSharp-API` 与根目录 README。

### Improvements

+ 构建脚本与 `CopyLocalLockFileAssemblies`，便于把依赖复制到输出目录。
+ `RsaUtils` 增加更多工具函数；`MetaObjectPool` 增加 `First` / `Last`。
+ `FileUtil.CopyFile` 增加默认 `overwrite` 参数。
+ `AStar` 部分数据结构改为公开，便于单元测试。
+ 各模块补全注释；增加 LICENSE、README、分析报告与 `.cursorignore`。

### Breaking Changes

+ `HttpClientProxy` 去掉回调写法，统一为 async/await。
+ `NetMessageCode` 重命名为 `NetResponseCode`。
+ 库项目不再引用 Portable.BouncyCastle 1.9.0，AES-CTR/GCM 改走内置引擎。

### API Changes

+ 新增加密：`AesCtrEngine`、`AesGcmEngine`，以及 PKCS#8 / X.509 与 `RsaUtils` 扩展。
+ 新增/完善网络：`IHttpClientProxy`、`SocketParams` / `SocketFactory` 连接模型、`NetResponseCode`。
+ 新增线程与事件：`FixedThreadContext`（含外部泵 `ProcessTasks`、`StopExec`）、`EventDispatcherPool`、线程上下文相关接口、`EventDispatcher` 名称标记。
+ 新增编码：四种 Base64 格式。
+ `MetaObjectPool` 增加 `First` / `Last`；`FileUtil.CopyFile` 增加覆盖默认参数。

### Changes

+ Socket 连接、接收与事件抛出改为可绑定线程上下文的调度方式。
+ Release 工作流同时产出 debug / release 包，并附带 `Infra-CSharp.pdb` 与 `Infra-CSharp.deps.json`。

### Notable Fixes

+ HttpClient 超时不再按 `TimeoutException` 处理，改为 `TaskCanceledException`。
+ 修复 `HttpClientProxy` 中 async/await 与回调混用的问题。
+ 修复 Socket 事件分发的多线程问题，以及部分事件未走线程管理的问题。
+ 修复 AES-CTR 算法错误，以及 `AesGcmEngine.BuildGHashInput` 在空密文块时的逻辑错误。
+ 修复 `SocketConnEventInfo` 内部逻辑错误与 `TestTcp` 用例问题。

### Changelog

+ 初始化项目，完成 AES / DES / XOR 与密钥派生 (`fb5c25d`)
+ 补充 AES / DES，增加 ASN.1 解释与 RSA (`37b3a2f`)
+ 删除 examples，添加构建配置与脚本 (`647dfac`)
+ csproj 增加 `CopyLocalLockFileAssemblies`，添加 publish 脚本 (`96f4440`)
+ 将 RocketDriver 中纯 C# 功能迁入并调整命名空间 (`4cfc42f`)
+ 增加 PKCS#8 私钥、X.509 公钥到 `RSAParameters` 的解释 (`44c0f3b`)
+ 重命名 Lua `AssemblyInfo.cs` 以避免特性重复定义 (`e6ed1eb`)
+ 完成 X.509 / PKCS#8 加载与测试 (`347a21c`)
+ 完善 `HttpClientProxy` 及其测试 (`c53bbe3`)
+ 修复 HttpClient 超时异常类型处理 (`3cbe0a5`)
+ Buffer 补充基础类型字节长度；重写 Socket 连接与接收；增加 TCP 测试 (`dc3d4e3`)
+ 增加 `FixedThreadContext`；修复 SocketClient 事件多线程问题 (`8f92046`)
+ `HttpClientProxy` 改为 async/await；增加四种 Base64 编码 (`b8ec694`)
+ 修复 HttpClientProxy 混用问题与 `SocketConnEventInfo` 逻辑错误 (`ff81eca`)
+ 临时保存 (`fd6ce35`)
+ 修复 TestTcp (`fcd906f`)
+ 增加 `AesCtrEngine` / `AesGcmEngine`，移除 BouncyCastle 依赖 (`db4bf7f`)
+ 分离 AES-CTR / AES-GCM 相关第三方代码 (`96b081a`)
+ 修复 AES-CTR 算法错误 (`8fc0030`)
+ 修复 GCM 空密文块 GHASH 逻辑，补充测试并再次移除 BouncyCastle (`165b200`)
+ 完成事件调度池，清理 AesCipher 无用代码 (`0a26df5`)
+ 保存 API 文档 (`d34dd4e`)
+ 添加 README、LICENSE 与分析报告 (`a9e5475`)
+ 添加 cursor ignore (`e62f580`)
+ 公开 A* 部分数据结构以便单测 (`1055cc7`)
+ 抽象 `IHttpClientProxy`；扩展 `RsaUtils` (`616b753`)
+ `MetaObjectPool` 增加 First/Last；优化 `MathUtil` (`9c80f00`)
+ `NetMessageCode` 重命名为 `NetResponseCode` (`b96b84f`)
+ 更新 `NetResponseCode.cs` (`51f174e`)
+ `FileUtil.CopyFile` 增加默认 overwrite (`71816c2`)
+ 修复 SocketClient 事件线程管理；`FixedThreadContext` 增加外部泵与 StopExec (`0bdc401`)
+ 事件模块增加多线程调度 (`8986973`)
+ 事件模块增加是否有线程上下文的接口 (`f6e1fd3`)
+ `EventDispatcher` 增加可自定义名称标记 (`c22ff2d`)
+ 补充注释 (`a585f65`)
+ 删除未使用 using (`38a8253`)
+ 补全 A* 注释 (`7440815`)
+ 补全归档/解档注释 (`8035d57`)
+ 补全缓冲区注释 (`fb96e28`)
+ 补全加解密注释 (`b398668`)
+ 补全时间工具注释 (`fed94f7`)
+ 补全编码注释 (`72207d1`)
+ 补全事件框架注释 (`2bf1e50`)
+ 补全 Extensions 注释 (`5d19be4`)
+ 补全 Imagex 注释 (`ecb79cf`)
+ 补全 Mathx 注释 (`bce3747`)
+ 补全 Net 注释 (`a2dfd4d`)
+ 补全 Pool 注释 (`91e75db`)
+ 补全 Serial 注释 (`041cbee`)
+ 补全 Xml 注释 (`5d736c2`)
+ 补全 Service 注释 (`d1ecadd`)
+ 补全 Threadx 注释 (`55c5387`)
+ 补全 Callback / ICloneable 注释 (`d4587b1`)
+ 删除不再使用的代码 (`976abf4`)
+ 增加 generate-note skill 与 CI / Release / ReleaseNote 工作流 (`354f60b`)
+ Release 工作流增加 debug DLL 产出 (`8b555a2`)
+ Release 产出增加 pdb 与 deps.json (`f035550`)
+ 更新 API 文档 (`bbb92b1`)
+ 更新 README (`9f38641`)

## Library Changes

+ Infra-CSharp：移除 Portable.BouncyCastle 1.9.0（AES-CTR/GCM 改为内置引擎，包引用已注释）
+ Infra-Tests：引入 coverlet.collector 6.0.0、Microsoft.NET.Test.Sdk 17.8.0、NUnit 3.14.0、NUnit.Analyzers 3.9.0、NUnit3TestAdapter 4.5.0
