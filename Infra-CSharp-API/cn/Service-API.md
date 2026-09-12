# Service API 文档

## 命名空间: JLGames.Infra.Service

### 接口 (Interfaces)

#### IService
服务的基础接口

```csharp
/// <summary>
/// 服务的基础接口
/// </summary>
public interface IService
{
    /// <summary>
    /// 服务唯一名称；由 ServiceConfig.AddConfig 写入。
    /// </summary>
    string ServiceName { get; set; }
}
```

#### IInitService
服务初始化接口。只有实现了当前接口，并配置到 ServiceConfig 中时，在初始化过程中才会执行 Init 方法。

```csharp
/// <summary>
/// 服务初始化接口
/// 只有实现了当前接口，并配置到ServiceConfig中时，在初始化过程中才会执行init方法
/// </summary>
public interface IInitService : IService, IEventDispatcher
{
    /// <summary>
    /// 是否已经完成初始化
    /// </summary>
    bool IsInited { get; }

    /// <summary>
    /// 初始化服务；完成后应派发 ServiceEvents.OnServiceInited，数据为 IService.ServiceName。
    /// </summary>
    void Init();
}
```

#### ILoadDataService
服务的数据加载接口。由 `ServiceManager.LoadServicesData` 按配置顺序依次调用。

```csharp
/// <summary>
/// 服务的数据加载接口。
/// 由 ServiceManager.LoadServicesData 按配置顺序依次调用。
/// </summary>
public interface ILoadDataService : IEventDispatcher
{
    /// <summary>
    /// 加载数据；完成后应派发 ServiceEvents.OnServiceDataLoaded，数据为服务名称。
    /// </summary>
    void LoadData();
}
```

#### ISaveDataService
服务的数据保存接口。由 `ServiceManager.SaveServicesData` 按配置顺序依次调用。

```csharp
/// <summary>
/// 服务的数据保存接口。
/// 由 ServiceManager.SaveServicesData 按配置顺序依次调用。
/// </summary>
public interface ISaveDataService : IEventDispatcher
{
    /// <summary>
    /// 保存数据；完成后应派发 ServiceEvents.OnServiceDataSaved，数据为服务名称。
    /// </summary>
    void SaveData();
}
```

#### IInitDataService
服务的数据初始化阶段接口。只有实现并注册到 ServiceConfig 后，启动时才会在所有 IInitService 完成之后执行 InitData。

```csharp
/// <summary>
/// 服务的数据初始化阶段接口。
/// 只有实现并注册到 ServiceConfig 后，启动时才会在所有 IInitService 完成之后执行 InitData。
/// </summary>
public interface IInitDataService : IService, IEventDispatcher
{
    /// <summary>
    /// 是否已经完成数据初始化
    /// </summary>
    bool IsDataInited { get; }

    /// <summary>
    /// 初始化运行时数据；完成后应派发 ServiceEvents.OnServiceDataInited，数据为 IService.ServiceName。
    /// </summary>
    void InitData();
}
```

#### IArgumentService
在初始化前接收构造/配置参数的接口。在 `ServiceManager.StartInitalization` 期间由 ServiceManager 对每个 ServiceConfig 项调用。

```csharp
/// <summary>
/// 在初始化前接收构造/配置参数的接口。
/// 在 ServiceManager.StartInitalization 期间由 ServiceManager 对每个 ServiceConfig 项调用。
/// </summary>
public interface IArgumentService
{
    /// <summary>
    /// 注入数据
    /// </summary>
    /// <param name="args">来自 ServiceInfo.Args 的参数；可为 null 或空数组。</param>
    void InjectArgument(object[] args);
}
```

#### IAwakableService
在 `IInitService.Init` 之前执行的早期激活钩子。由 ServiceManager 在启动时同步调用；不允许使用异步。

```csharp
/// <summary>
/// 在 IInitService.Init 之前执行的早期激活钩子。
/// 由 ServiceManager 在启动时同步调用；不允许使用异步。
/// </summary>
public interface IAwakableService
{
    /// <summary>
    /// 激活Service
    /// 不允许使用异步
    /// </summary>
    void Awake();
}
```

#### IClearService
将服务重置到未初始化状态（监听、标志、计时器等）。由 `ServiceManager.ClearServices` 对每个服务调用。

```csharp
/// <summary>
/// 将服务重置到未初始化状态（监听、标志、计时器等）。
/// 由 ServiceManager.ClearServices 对每个服务调用。
/// </summary>
public interface IClearService
{
    /// <summary>
    /// 重置
    /// 清除事件、清除计时器等
    /// </summary>
    void Clear();
}
```

#### IProgressingService
上报细粒度初始化进度；可替代每个 IInitService / IInitDataService 默认的单步计数。由服务实现递增进度并派发 `ServiceEvents.OnServiceProcessing`。

```csharp
/// <summary>
/// 上报细粒度初始化进度；可替代每个 IInitService / IInitDataService 默认的单步计数。
/// 由服务实现递增进度并派发 ServiceEvents.OnServiceProcessing。
/// </summary>
public interface IProgressingService : IEventDispatcher
{
    /// <summary>
    /// 进度总量
    /// [0,int.Max)
    /// </summary>
    uint ProgressingLen { get; }

    /// <summary>
    /// 进度当前量
    /// [0,Total]
    /// </summary>
    uint ProgressingCurrent { get; }
}
```

### 类 (Classes)

#### ServiceManager
编排服务的参数注入、激活、初始化及加载/保存，并通过事件上报整体进度。

Init / InitData / Load / Save 的逐步推进由内部 `ServiceHandler` 及其派生类（`ServiceInitHandler`、`ServiceInitDataHandler`、`ServiceLoadDataHandler`、`ServiceSaveDataHandler`）实现；这些类型为 `internal`，不是公开 API。

```csharp
/// <summary>
/// 编排服务的参数注入、激活、初始化及加载/保存，并通过事件上报整体进度。
/// </summary>
public sealed class ServiceManager : EventDispatcher
{
    /// <summary>
    /// 处理百分比
    /// </summary>
    public float ProcessingPercentage { get; }

    /// <summary>
    /// 当前进度总长
    /// </summary>
    public uint ProcessingLen { get; }

    /// <summary>
    /// 已完成进度
    /// </summary>
    public uint ProcessingFinished { get; }

    /// <summary>
    /// 清理事件监听
    /// </summary>
    public void ClearEvents();

    /// <summary>
    /// 服务清理
    /// </summary>
    public void ClearServices();

    /// <summary>
    /// 服务数据加载
    /// </summary>
    /// <param name="endCall">全部配置的数据加载服务完成后的回调。</param>
    public void LoadServicesData(Callback endCall);

    /// <summary>
    /// 服务数据保存
    /// </summary>
    /// <param name="endCall">全部配置的数据保存服务完成后的回调。</param>
    public void SaveServicesData(Callback endCall);

    /// <summary>
    /// 初始化配置好的服务
    /// </summary>
    /// <param name="endCall">全部服务 Init 与 InitData 完成后的回调。</param>
    public void StartInitalization(Callback endCall);

    /// <summary>
    /// 全局服务管理器单例。
    /// </summary>
    public static ServiceManager Shared { get; }

    /// <summary>
    /// ServiceConfig.Shared 的简写访问。
    /// </summary>
    public static ServiceConfig Config { get; }
}
```

`StartInitalization` 会从 `ServiceConfig.Shared.ServiceInfos` 读取当前配置，依次执行参数注入、激活、Init、InitData。方法名按源码拼写为 `StartInitalization`（缺少一个 `i`）。`endCall` 类型为 `JLGames.Infra.Callback`，不是委托，需使用 `new Callback(...)` 构造。

#### ServiceInfo
描述单个已注册服务：名称、实现实例及可选的构造/注入参数。实现 `ICloneable<ServiceInfo>`。

```csharp
/// <summary>
/// 描述单个已注册服务：名称、实现实例及可选的构造/注入参数。
/// </summary>
public class ServiceInfo : ICloneable<ServiceInfo>
{
    /// <summary>
    /// 服务唯一名称。
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// 以 IService 形式暴露的实现实例。
    /// </summary>
    public IService ServiceImpl { get; }

    /// <summary>
    /// 在适用时传给 IArgumentService.InjectArgument 的参数。
    /// </summary>
    public object[] Args { get; }

    /// <summary>
    /// 判断当前实例是否实现了 IAwakableService。true: 实现了; false: 未实现。
    /// </summary>
    public bool IsAwakableService { get; }

    /// <summary>
    /// 判断当前实例是否实现了 IInitService。true: 实现了; false: 未实现。
    /// </summary>
    public bool IsInitService { get; }

    /// <summary>
    /// 判断当前实例是否实现了 IArgumentService。true: 实现了; false: 未实现。
    /// </summary>
    public bool IsArgumentService { get; }

    /// <summary>
    /// 判断当前实例是否实现了 IProgressingService。true: 实现了; false: 未实现。
    /// </summary>
    public bool IsProgressingService { get; }

    /// <summary>
    /// 判断当前实例是否实现了 IInitDataService。true: 实现了; false: 未实现。
    /// </summary>
    public bool IsInitDataService { get; }

    /// <summary>
    /// 判断当前实例是否实现了 ILoadDataService。true: 实现了; false: 未实现。
    /// </summary>
    public bool IsLoadDataService { get; }

    /// <summary>
    /// 判断当前实例是否实现了 ISaveDataService。true: 实现了; false: 未实现。
    /// </summary>
    public bool IsSaveDataService { get; }

    /// <summary>
    /// 检查实现对象的接口状态
    /// </summary>
    /// <typeparam name="T">要检测的接口或基类型。</typeparam>
    /// <returns>实现实例可赋给 T 时返回 true。</returns>
    public bool CheckServiceImpl<T>() where T : class;

    /// <summary>
    /// 取服务的实现对象
    /// </summary>
    /// <typeparam name="T">期望的实现类型。</typeparam>
    /// <returns>转换后的实例；类型不兼容时为 null。</returns>
    public T GetServiceImpl<T>() where T : class;

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="serviceName">服务唯一名称。</param>
    /// <param name="serviceImpl">服务实现实例。</param>
    /// <param name="args">供 IArgumentService 使用的可选参数。</param>
    public ServiceInfo(string serviceName, IService serviceImpl, params object[] args);

    /// <summary>
    /// 克隆
    /// </summary>
    /// <returns>共享同一实现引用的浅拷贝。</returns>
    public ServiceInfo Clone();

    public override string ToString();
}
```

属性均为只读，须通过构造函数创建实例。`Clone()` 共享同一实现引用。

#### ServiceConfig
已注册服务的配置表，供 ServiceManager 在启动流程中使用。构造函数为私有，只能通过 `Shared` 单例访问。

```csharp
/// <summary>
/// 已注册服务的配置表，供 ServiceManager 在启动流程中使用。
/// </summary>
public class ServiceConfig
{
    /// <summary>
    /// 已配置的服务的总个数
    /// </summary>
    public int ServiceSize { get; }

    /// <summary>
    /// 取全部的服务配置列表
    /// </summary>
    public ServiceInfo[] ServiceInfos { get; }

    /// <summary>
    /// 添加服务到配置列表尾部
    /// </summary>
    /// <param name="sc">要追加的服务配置项。</param>
    /// <param name="ignoreSame">为 false 时，若 ServiceInfo.ServiceName 已存在则抛出异常。</param>
    /// <exception cref="Exception">当 ignoreSame 为 false 且服务名重复时抛出。</exception>
    public void AddConfig(ServiceInfo sc, bool ignoreSame = true);

    /// <summary>
    /// 取指定服务器实现对象
    /// </summary>
    /// <param name="serviceName">服务唯一名称。</param>
    /// <returns>实现实例；未找到时为 null。</returns>
    public IService GetServiceImpl(string serviceName);

    /// <summary>
    /// 取指定服务器实现对象，使用泛型为了减少代码编写量
    /// </summary>
    /// <param name="serviceName">服务唯一名称。</param>
    /// <typeparam name="T">期望的实现类型。</typeparam>
    /// <returns>转换后的实现；不存在或类型不匹配时为 null。</returns>
    public T GetServiceImpl<T>(string serviceName) where T : class;

    /// <summary>
    /// 检查服务存在性
    /// </summary>
    /// <param name="serviceName">服务唯一名称。</param>
    /// <returns>已注册同名服务时返回 true。</returns>
    public bool ContainsService(string serviceName);

    /// <summary>
    /// 取指定一个的服务配置信息
    /// </summary>
    /// <param name="serviceName">服务唯一名称。</param>
    /// <returns>配置项；未找到时为 null。</returns>
    public ServiceInfo GetServiceInfo(string serviceName);

    /// <summary>
    /// 全局服务配置单例。
    /// </summary>
    public static ServiceConfig Shared { get; }
}
```

`AddConfig` 会把 `sc.ServiceName` 写入 `sc.ServiceImpl.ServiceName`。`ignoreSame` 默认为 `true`（允许同名追加）；为 `false` 且重名时抛出 `Exception`，消息形如 `重复的ServiceName:{name}`。`ServiceInfos` 返回内部列表的快照数组，不可直接赋值。

#### ServiceBase
服务抽象基类：生命周期状态、进度上报与事件派发。继承 `EventDispatcher`，并实现 `IService`、`IClearService`。本身不实现 `IInitService` / `IInitDataService` / `IProgressingService` 等阶段接口，子类按需声明。

```csharp
/// <summary>
/// 服务抽象基类：生命周期状态、进度上报与事件派发。
/// </summary>
public abstract class ServiceBase : EventDispatcher, IService, IClearService
{
    protected delegate void ProcessingCall();

    /// <inheritdoc />
    public string ServiceName { get; set; }

    protected bool m_Inited = false;
    protected bool m_DataInited = false;

    /// <summary>
    /// 本服务是否已完成 IInitService.Init 初始化。
    /// </summary>
    public bool IsInited { get; }

    /// <summary>
    /// 本服务是否已完成 IInitDataService.InitData 数据初始化。
    /// </summary>
    public bool IsDataInited { get; }

    protected uint m_ProgressingLen = 1;
    protected uint m_ProgressingCurrent = 0;

    /// <summary>
    /// 实现 IProgressingService 时的进度总步数。
    /// </summary>
    public uint ProgressingLen { get; }

    /// <summary>
    /// 已完成的进度步数，与 ProgressingLen 配合使用。
    /// </summary>
    public uint ProgressingCurrent { get; }

    /// <inheritdoc />
    public virtual void Clear();

    /// <summary>
    /// 递增进度并派发 OnServiceProcessing，再执行 call。
    /// </summary>
    protected virtual void InvokdProcessing(ProcessingCall call);

    /// <summary>
    /// 标记已初始化并派发 OnServiceInited（数据为 ServiceName）。
    /// </summary>
    protected virtual void InvokeInited();

    /// <summary>
    /// 标记数据已初始化并派发 OnServiceDataInited（数据为 ServiceName）。
    /// </summary>
    protected virtual void InvokeDataInited();

    /// <summary>
    /// 派发 OnServiceDataLoaded（数据为 ServiceName）。
    /// </summary>
    protected virtual void InvokeDataLoaded();

    /// <summary>
    /// 派发 OnServiceDataSaved（数据为 ServiceName）。
    /// </summary>
    protected virtual void InvokeDataSaved();
}
```

`InvokdProcessing` 按源码拼写（缺少一个 `e`）。子类完成对应阶段后应调用这些辅助方法，否则 ServiceManager 会一直等待完成事件。

### 事件 (Events)

#### ServiceEvents
服务事件常量。注意：`OnServiceProcessing` 的字符串值使用点号（`.`），其余常量使用冒号（`:`）。

```csharp
/// <summary>
/// 服务事件
/// </summary>
public static class ServiceEvents
{
    /// <summary>
    /// 单个服务初始化进度更新事件，由服务实例调度。服务实例必须为 IProgressingService 接口的实现类
    /// </summary>
    public const string OnServiceProcessing = "ServiceEvents.OnServiceProcessing";

    /// <summary>
    /// 服务初始化进程进度更新
    /// </summary>
    public const string OnInitializationProcessing = "ServiceEvents:OnInitializationProcessing";

    /// <summary>
    /// 服务初始化进程完成
    /// </summary>
    public const string OnInitializationFinish = "ServiceEvents:OnInitializationFinish";

    /// <summary>
    /// 服务参数注入结果事件, 由 ServiceManager 调度。当服务实现 IArgumentService 接口时，Succ=true。
    /// 事件数据格式：ServiceResultData
    /// </summary>
    public const string OnServiceInjected = "ServiceEvents:OnServiceInjected";

    /// <summary>
    /// 全部服务参数注入完成事件
    /// 事件数据格式：null
    /// </summary>
    public const string OnServiceAllInjected = "ServiceEvents:OnServiceAllInjected";

    /// <summary>
    /// 服务激活结果事件, 由 ServiceManager 调度。当服务实现 IAwakableService 接口时，Succ=true。
    /// 事件数据格式：ServiceResultData
    /// </summary>
    public const string OnServiceAwaked = "ServiceEvents:OnServiceAwaked";

    /// <summary>
    /// 全部服务激活完成事件
    /// 事件数据格式：null
    /// </summary>
    public const string OnServiceAllAwaked = "ServiceEvents:OnServiceAllAwaked";

    /// <summary>
    /// 单个服务初始化开始事件，由 ServiceManager 调度
    /// 事件数据格式：ServiceResultData
    /// </summary>
    public const string OnServiceInitStart = "ServiceEvents:OnServiceInitStart";

    /// <summary>
    /// 单个服务初始化完成事件，由服务实例调度。被 ServiceManager 捕获后重新调度。
    /// 事件数据格式：服务名称字符串
    /// </summary>
    public const string OnServiceInited = "ServiceEvents:OnServiceInited";

    /// <summary>
    /// 全部服务初始化完成事件
    /// 事件数据：null
    /// </summary>
    public const string OnServiceAllInited = "ServiceEvents:OnServiceAllInited";

    /// <summary>
    /// 单个服务数据初始化开始事件，由 ServiceManager 调度
    /// 事件数据格式：ServiceResultData
    /// </summary>
    public const string OnServiceDataInitStart = "ServiceEvents:OnServiceDataInitStart";

    /// <summary>
    /// 单个服务数据初始化完成事件，由服务实例调度。被 ServiceManager 捕获后重新调度。
    /// 事件数据格式：服务名称字符串
    /// </summary>
    public const string OnServiceDataInited = "ServiceEvents:OnServiceDataInited";

    /// <summary>
    /// 全部服务数据初始化完成事件
    /// 事件数据：null
    /// </summary>
    public const string OnServiceDataAllInited = "ServiceEvents:OnServiceDataAllInited";

    /// <summary>
    /// 单个服务数据加载数据开始事件，由 ServiceManager 调度。当服务实现 ILoadDataService 接口时，Succ=true。
    /// 事件数据格式：ServiceResultData
    /// </summary>
    public const string OnServiceDataLoadStart = "ServiceEvents:OnServiceDataLoadStart";

    /// <summary>
    /// 单个数据服务加载数据完成事件，由服务实例调度。被 ServiceManager 捕获后重新调度。
    /// 事件数据格式：服务名称字符串
    /// </summary>
    public const string OnServiceDataLoaded = "ServiceEvents:OnServiceDataLoaded";

    /// <summary>
    /// 全部数据服务加载数据完成事件
    /// 事件数据：null
    /// </summary>
    public const string OnServiceDataAllLoaded = "ServiceEvents:OnServiceDataAllLoaded";

    /// <summary>
    /// 单个服务数据保存数据开始事件，由 ServiceManager 调度。当服务实现 ISaveDataService 接口时，Succ=true。
    /// 事件数据格式：ServiceResultData
    /// </summary>
    public const string OnServiceDataSaveStart = "ServiceEvents:OnServiceDataSaveStart";

    /// <summary>
    /// 单个数据服务保存数据完成事件，由服务实例调度。被 ServiceManager 捕获后重新调度。
    /// 事件数据格式：服务名称字符串
    /// </summary>
    public const string OnServiceDataSaved = "ServiceEvents:OnServiceDataSaved";

    /// <summary>
    /// 全部数据服务保存数据完成事件
    /// 事件数据：null
    /// </summary>
    public const string OnServiceDataAllSaved = "ServiceEvents:OnServiceDataAllSaved";
}
```

### 数据类 (Data Classes)

#### ServiceResultData
单个服务结果类事件（注入、激活、初始化开始、加载/保存开始等）的数据结构。这是一个结构体，字段为公开字段而非属性。

```csharp
/// <summary>
/// 单个服务结果类事件（注入、激活、初始化开始、加载/保存开始等）的数据结构。
/// </summary>
public struct ServiceResultData
{
    /// <summary>
    /// 相关服务名称。
    /// </summary>
    public string ServiceName;

    /// <summary>
    /// 该服务是否实现了对应步骤所需的 capability（详见 ServiceEvents 中的事件说明）。
    /// </summary>
    public bool Succ;
}
```

`Succ` 表示该服务是否实现了当前步骤所需接口（例如注入步骤对应 `IArgumentService`），不表示业务执行成功与否。

### 功能说明

#### 服务生命周期

**初始化阶段**（`StartInitalization`）
1. **参数注入**：对每个配置项，若实现 `IArgumentService` 则调用 `InjectArgument(info.Args)`，并派发 `OnServiceInjected`；全部完成后派发 `OnServiceAllInjected`。
2. **服务激活**：对每个配置项，若实现 `IAwakableService` 则同步调用 `Awake()`，并派发 `OnServiceAwaked`；全部完成后派发 `OnServiceAllAwaked`。不允许异步。
3. **服务初始化**：按配置顺序逐个处理。对每个服务派发 `OnServiceInitStart`；若实现 `IInitService` 则调用 `Init()`，并等待该服务派发 `OnServiceInited`。全部完成后派发 `OnServiceAllInited`。
4. **数据初始化**：在全部 Init 完成后按配置顺序逐个处理。对每个服务派发 `OnServiceDataInitStart`；若实现 `IInitDataService` 则调用 `InitData()`，并等待 `OnServiceDataInited`。全部完成后派发 `OnServiceDataAllInited`，再调用 `endCall`，最后派发 `OnInitializationFinish`。

**运行阶段**
- `LoadServicesData`：按配置顺序调用 `ILoadDataService.LoadData()`，等待 `OnServiceDataLoaded`；过程中派发 `OnServiceDataLoadStart` / `OnServiceDataLoaded` / `OnServiceDataAllLoaded`。
- `SaveServicesData`：按配置顺序调用 `ISaveDataService.SaveData()`，等待 `OnServiceDataSaved`；过程中派发 `OnServiceDataSaveStart` / `OnServiceDataSaved` / `OnServiceDataAllSaved`。

**清理阶段**
- `ClearServices` 先移除管理器上的事件监听，再按配置**逆序**对实现 `IClearService` 的服务调用 `Clear()`。
- `ClearEvents` 仅移除 ServiceManager 自身的事件监听。

未实现对应接口的配置项会被跳过，但开始类事件仍会派发，且 `ServiceResultData.Succ` 为 `false`。

#### 进度管理

通过 `IProgressingService` 支持细粒度进度：
- **ProgressingLen**：进度总量
- **ProgressingCurrent**：进度当前量
- 实现了 `IProgressingService` 时，该服务按上述字段计入总进度；否则每个 `IInitService` / `IInitDataService` 各计 1 步（完成时看 `IsInited` / `IsDataInited`）。
- ServiceManager 在收到 `OnServiceProcessing`、`OnServiceInited`、`OnServiceDataInited` 时更新 `ProcessingFinished`，并派发 `OnInitializationProcessing`。
- `ProcessingPercentage` 在已完成量达到总量时返回 `1`，否则返回 `ProcessingFinished / ProcessingLen`。

#### 事件驱动

服务系统基于事件驱动架构：
- 每个生命周期阶段都有对应的开始/完成事件
- Init / InitData / Load / Save 的单服务完成事件必须由服务实例自行派发，ServiceManager 捕获后再转发
- 支持进度报告和状态通知

### 使用示例

#### 基本服务实现
```csharp
public class MyService : ServiceBase, IInitService, ILoadDataService, ISaveDataService
{
    public void Init()
    {
        Console.WriteLine($"初始化服务: {ServiceName}");
        InvokeInited();
    }

    public void LoadData()
    {
        Console.WriteLine($"加载数据: {ServiceName}");
        InvokeDataLoaded();
    }

    public void SaveData()
    {
        Console.WriteLine($"保存数据: {ServiceName}");
        InvokeDataSaved();
    }
}
```

不继承 `ServiceBase` 时，完成对应阶段后必须自行 `DispatchEvent` 完成事件（例如 `ServiceEvents.OnServiceInited`，数据为服务名称），否则流程不会继续。

#### 服务配置
```csharp
var serviceConfig = ServiceConfig.Shared;
serviceConfig.AddConfig(new ServiceInfo("UserService", new UserService(), "userdb"));
serviceConfig.AddConfig(new ServiceInfo("ConfigService", new ConfigService()));

var user = serviceConfig.GetServiceImpl<UserService>("UserService");
```

`ServiceInfos` 只读，不能直接赋值。通过 `AddConfig` 注册；`AddConfig` 会把服务名写回实现实例的 `ServiceName`。

#### 服务管理器使用
```csharp
var serviceManager = ServiceManager.Shared;

serviceManager.AddEventListener(ServiceEvents.OnInitializationProcessing, evd =>
{
    Console.WriteLine($"初始化进度: {serviceManager.ProcessingPercentage:P}");
});

serviceManager.AddEventListener(ServiceEvents.OnInitializationFinish, evd =>
{
    Console.WriteLine("所有服务初始化完成！");
});

serviceManager.StartInitalization(new Callback(_ =>
{
    Console.WriteLine("初始化回调执行");
}));
```

#### 数据操作
```csharp
serviceManager.LoadServicesData(new Callback(_ =>
{
    Console.WriteLine("数据加载完成");
}));

serviceManager.SaveServicesData(new Callback(_ =>
{
    Console.WriteLine("数据保存完成");
}));
```

#### 参数服务示例
```csharp
public class DatabaseService : ServiceBase, IArgumentService
{
    private string connectionString;

    public void InjectArgument(object[] args)
    {
        connectionString = args != null && args.Length > 0 ? args[0] as string : null;
        Console.WriteLine($"注入数据库连接: {connectionString}");
    }
}

ServiceConfig.Shared.AddConfig(
    new ServiceInfo("DatabaseService", new DatabaseService(), "Server=.;Database=app"));
```

#### 进度服务示例
```csharp
public class FileProcessService : ServiceBase, IInitService, IProgressingService
{
    public FileProcessService()
    {
        m_ProgressingLen = 100;
    }

    public void Init()
    {
        for (uint i = 0; i < m_ProgressingLen; i++)
        {
            InvokdProcessing(null);
        }
        InvokeInited();
    }
}
```

`InvokdProcessing` 会递增 `m_ProgressingCurrent` 并派发 `OnServiceProcessing`（数据为 `ServiceName`）。实现 `IProgressingService` 时仍需在 Init / InitData 结束时派发对应完成事件。

### 设计特点

1. **生命周期管理**：完整的服务生命周期管理（注入 → 激活 → Init → InitData，以及 Load / Save / Clear）
2. **事件驱动**：基于事件的状态通知和进度报告；阶段完成依赖服务自行派发完成事件
3. **接口分离**：不同功能通过接口分离，便于按需实现
4. **配置驱动**：通过 `ServiceConfig.AddConfig` 注册服务，支持参数注入
5. **进度支持**：内置进度报告机制
6. **单例模式**：`ServiceManager.Shared` 与 `ServiceConfig.Shared` 提供全局访问点

### 注意事项

1. **初始化顺序**：服务按 `ServiceConfig` 中的注册顺序处理；清理按逆序调用 `IClearService.Clear`
2. **完成事件**：`Init` / `InitData` / `LoadData` / `SaveData` 必须派发对应完成事件，否则后续服务不会开始
3. **事件监听**：及时清理不需要的事件监听（`ClearEvents` / `ClearServices` / 服务自身的 `Clear`）
4. **资源管理**：确保服务正确清理资源；`ServiceBase.Clear` 会移除监听并复位 `IsInited` / `IsDataInited`
5. **线程安全**：多线程环境需要额外的同步机制；`IAwakableService.Awake` 不允许异步
6. **Callback**：`StartInitalization` / `LoadServicesData` / `SaveServicesData` 的回调类型是 `Callback` 类，不能直接传入无参 lambda
