# Service API 文档

## 命名空间: JLGames.Infra.Service

### 接口 (Interfaces)

#### IService
服务的基础接口

```csharp
/// <summary>
/// Service base interface
/// 服务的基础接口
/// </summary>
public interface IService
{
    /// <summary>
    /// Service name.
    /// 服务名称
    /// </summary>
    string ServiceName { get; set; }
}
```

#### IInitService
服务初始化接口

```csharp
/// <summary>
/// Service initialization interface
/// 服务初始化接口
/// Only when the current interface is implemented and configured into ServiceConfig,
/// the init method will be executed during the initialization process
/// 只有实现了当前接口，并配置到ServiceConfig中时，在初始化过程中才会执行init方法
/// </summary>
public interface IInitService : IService, IEventDispatcher
{
    /// <summary>
    /// Whether the initialization has been completed
    /// 是否已经完成初始化
    /// </summary>
    bool IsInited { get; }

    /// <summary>
    /// Initialize base data
    /// 初始化基础数据
    /// </summary>
    void Init();
}
```

#### ILoadDataService
加载数据处理接口

```csharp
/// <summary>
/// Load data processing interface
/// 加载数据处理接口
/// </summary>
public interface ILoadDataService : IEventDispatcher
{
    /// <summary>
    /// Load Data
    /// 加载数据 
    /// </summary>
    void LoadData();
}
```

#### ISaveDataService
保存数据处理接口

```csharp
/// <summary>
/// Save data processing interface
/// 保存数据处理接口
/// </summary>
public interface ISaveDataService : IEventDispatcher
{
    /// <summary>
    /// Save data
    /// 保存数据 
    /// </summary>
    void SaveData();
}
```

#### IInitDataService
初始化数据服务接口

```csharp
/// <summary>
/// 初始化数据服务接口
/// 用于服务的数据初始化阶段
/// </summary>
public interface IInitDataService : IEventDispatcher
{
    /// <summary>
    /// 是否已经完成数据初始化
    /// </summary>
    bool IsDataInited { get; }

    /// <summary>
    /// 初始化数据
    /// </summary>
    void InitData();
}
```

#### IArgumentService
参数服务接口

```csharp
/// <summary>
/// 参数服务接口
/// 用于接收外部参数注入
/// </summary>
public interface IArgumentService
{
    /// <summary>
    /// 注入参数
    /// </summary>
    /// <param name="args">参数数据</param>
    void InjectArgument(object args);
}
```

#### IAwakableService
可唤醒服务接口

```csharp
/// <summary>
/// 可唤醒服务接口
/// 用于服务的唤醒阶段
/// </summary>
public interface IAwakableService
{
    /// <summary>
    /// 唤醒服务
    /// </summary>
    void Awake();
}
```

#### IClearService
清理服务接口

```csharp
/// <summary>
/// 清理服务接口
/// 用于服务的清理阶段
/// </summary>
public interface IClearService
{
    /// <summary>
    /// 清理服务
    /// </summary>
    void Clear();
}
```

#### IProgressingService
进度服务接口

```csharp
/// <summary>
/// 进度服务接口
/// 用于支持进度报告的服务
/// </summary>
public interface IProgressingService
{
    /// <summary>
    /// 进度总长度
    /// </summary>
    uint ProgressingLen { get; }

    /// <summary>
    /// 当前进度
    /// </summary>
    uint ProgressingCurrent { get; }
}
```

### 类 (Classes)

#### ServiceManager
服务管理器类

```csharp
/// <summary>
/// 服务管理器
/// 负责服务的生命周期管理，包括初始化、数据加载、保存等
/// </summary>
public sealed class ServiceManager : EventDispatcher
{
    /// <summary>
    /// Callback after all initialization is complete, including Init function and InitData function
    /// 全部初始化完成后回调，包括Init函数与InitData函数
    /// </summary>
    private Callback m_FinishCall;

    /// <summary>
    /// Processing percentage
    /// 处理百分比
    /// </summary>
    public float ProcessingPercentage { get; }

    /// <summary>
    /// Current progress total length
    /// 当前进度总长
    /// </summary>
    public uint ProcessingLen { get; }

    /// <summary>
    /// Progress completed
    /// 已完成进度
    /// </summary>
    public uint ProcessingFinished { get; }

    /// <summary>
    /// cleanup event listener
    /// 清理事件监听
    /// </summary>
    public void ClearEvents();

    /// <summary>
    /// Service cleanup
    /// 服务清理
    /// </summary>
    public void ClearServices();

    /// <summary>
    /// service data loading
    /// 服务数据加载
    /// </summary>
    /// <param name="endCall"></param>
    public void LoadServicesData(Callback endCall);

    /// <summary>
    /// Service data storage
    /// 服务数据保存
    /// </summary>
    /// <param name="endCall"></param>
    public void SaveServicesData(Callback endCall);

    /// <summary>
    /// Initialize the configured service
    /// 初始化配置好的服务
    /// If it has been initialized once, try to execute the callback directly
    /// 如果已经初始化一次，就尝试直接执行回调
    /// </summary>
    /// <param name="endCall"></param>
    public void StartInitalization(Callback endCall);

    /// <summary>
    /// 单例实例
    /// </summary>
    public static ServiceManager Shared { get; }

    /// <summary>
    /// 配置访问器
    /// </summary>
    public static ServiceConfig Config { get; }
}
```

#### ServiceInfo
服务信息类

```csharp
/// <summary>
/// 服务信息类
/// 包含服务的配置信息和实例
/// </summary>
public class ServiceInfo
{
    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// 服务实现
    /// </summary>
    public object ServiceImpl { get; set; }

    /// <summary>
    /// 参数数据
    /// </summary>
    public object Args { get; set; }

    /// <summary>
    /// 是否为初始化服务
    /// </summary>
    public bool IsInitService { get; }

    /// <summary>
    /// 是否为数据初始化服务
    /// </summary>
    public bool IsInitDataService { get; }

    /// <summary>
    /// 是否为参数服务
    /// </summary>
    public bool IsArgumentService { get; }

    /// <summary>
    /// 是否为可唤醒服务
    /// </summary>
    public bool IsAwakableService { get; }

    /// <summary>
    /// 是否为进度服务
    /// </summary>
    public bool IsProgressingService { get; }

    /// <summary>
    /// 获取服务实现
    /// </summary>
    /// <typeparam name="T">服务类型</typeparam>
    /// <returns>服务实例</returns>
    public T GetServiceImpl<T>() where T : class;
}
```

#### ServiceConfig
服务配置类

```csharp
/// <summary>
/// 服务配置类
/// 管理服务的配置信息
/// </summary>
public class ServiceConfig
{
    /// <summary>
    /// 服务信息数组
    /// </summary>
    public ServiceInfo[] ServiceInfos { get; set; }

    /// <summary>
    /// 单例实例
    /// </summary>
    public static ServiceConfig Shared { get; }
}
```

#### ServiceBase
服务基类

```csharp
/// <summary>
/// 服务基类
/// 提供基础的服务功能实现
/// </summary>
public abstract class ServiceBase : IService
{
    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }
}
```

### 事件 (Events)

#### ServiceEvents
服务事件常量

```csharp
/// <summary>
/// 服务事件常量
/// 定义服务生命周期中的各种事件
/// </summary>
public static class ServiceEvents
{
    /// <summary>
    /// 服务注入事件
    /// </summary>
    public const string OnServiceInjected = "OnServiceInjected";

    /// <summary>
    /// 所有服务注入完成事件
    /// </summary>
    public const string OnServiceAllInjected = "OnServiceAllInjected";

    /// <summary>
    /// 服务唤醒事件
    /// </summary>
    public const string OnServiceAwaked = "OnServiceAwaked";

    /// <summary>
    /// 所有服务唤醒完成事件
    /// </summary>
    public const string OnServiceAllAwaked = "OnServiceAllAwaked";

    /// <summary>
    /// 服务初始化事件
    /// </summary>
    public const string OnServiceInited = "OnServiceInited";

    /// <summary>
    /// 所有服务初始化完成事件
    /// </summary>
    public const string OnServiceAllInited = "OnServiceAllInited";

    /// <summary>
    /// 服务数据初始化事件
    /// </summary>
    public const string OnServiceDataInited = "OnServiceDataInited";

    /// <summary>
    /// 所有服务数据初始化完成事件
    /// </summary>
    public const string OnServiceDataAllInited = "OnServiceDataAllInited";

    /// <summary>
    /// 服务处理进度事件
    /// </summary>
    public const string OnServiceProcessing = "OnServiceProcessing";

    /// <summary>
    /// 初始化进度事件
    /// </summary>
    public const string OnInitializationProcessing = "OnInitializationProcessing";

    /// <summary>
    /// 初始化完成事件
    /// </summary>
    public const string OnInitializationFinish = "OnInitializationFinish";
}
```

### 数据类 (Data Classes)

#### ServiceResultData
服务结果数据

```csharp
/// <summary>
/// 服务结果数据
/// 用于事件传递的服务执行结果
/// </summary>
public class ServiceResultData
{
    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Succ { get; set; }
}
```

### 功能说明

#### 服务生命周期

**初始化阶段**
1. **参数注入**：IArgumentService接口的服务接收外部参数
2. **服务唤醒**：IAwakableService接口的服务执行唤醒操作
3. **服务初始化**：IInitService接口的服务执行初始化
4. **数据初始化**：IInitDataService接口的服务执行数据初始化

**运行阶段**
- 服务正常运行，处理业务逻辑
- 支持数据加载和保存操作

**清理阶段**
- IClearService接口的服务执行清理操作
- 释放资源和事件监听

#### 进度管理

通过IProgressingService接口支持进度报告：
- **ProgressingLen**：总进度长度
- **ProgressingCurrent**：当前进度
- ServiceManager自动计算整体进度百分比

#### 事件驱动

服务系统基于事件驱动架构：
- 每个生命周期阶段都有对应的事件
- 支持进度报告和状态通知
- 便于监控和调试

### 使用示例

#### 基本服务实现
```csharp
// 实现一个基础服务
public class MyService : ServiceBase, IInitService, ILoadDataService, ISaveDataService
{
    public bool IsInited { get; private set; }

    public void Init()
    {
        Console.WriteLine($"初始化服务: {ServiceName}");
        IsInited = true;
    }

    public void LoadData()
    {
        Console.WriteLine($"加载数据: {ServiceName}");
    }

    public void SaveData()
    {
        Console.WriteLine($"保存数据: {ServiceName}");
    }
}
```

#### 服务配置
```csharp
// 配置服务
var serviceConfig = ServiceConfig.Shared;
serviceConfig.ServiceInfos = new ServiceInfo[]
{
    new ServiceInfo
    {
        ServiceName = "UserService",
        ServiceImpl = new UserService(),
        Args = new { database = "userdb" }
    },
    new ServiceInfo
    {
        ServiceName = "ConfigService",
        ServiceImpl = new ConfigService()
    }
};
```

#### 服务管理器使用
```csharp
// 获取服务管理器
var serviceManager = ServiceManager.Shared;

// 监听初始化进度
serviceManager.AddEventListener(ServiceEvents.OnInitializationProcessing, (evd) => {
    Console.WriteLine($"初始化进度: {serviceManager.ProcessingPercentage:P}");
});

// 监听初始化完成
serviceManager.AddEventListener(ServiceEvents.OnInitializationFinish, (evd) => {
    Console.WriteLine("所有服务初始化完成！");
});

// 开始初始化
serviceManager.StartInitalization(() => {
    Console.WriteLine("初始化回调执行");
});
```

#### 数据操作
```csharp
// 加载服务数据
serviceManager.LoadServicesData(() => {
    Console.WriteLine("数据加载完成");
});

// 保存服务数据
serviceManager.SaveServicesData(() => {
    Console.WriteLine("数据保存完成");
});
```

#### 参数服务示例
```csharp
public class DatabaseService : ServiceBase, IArgumentService
{
    private string connectionString;

    public void InjectArgument(object args)
    {
        var config = args as dynamic;
        connectionString = config?.connectionString;
        Console.WriteLine($"注入数据库连接: {connectionString}");
    }
}
```

#### 进度服务示例
```csharp
public class FileProcessService : ServiceBase, IProgressingService
{
    private uint totalFiles = 100;
    private uint processedFiles = 0;

    public uint ProgressingLen => totalFiles;
    public uint ProgressingCurrent => processedFiles;

    public void ProcessFiles()
    {
        for (int i = 0; i < totalFiles; i++)
        {
            // 处理文件
            processedFiles++;
            
            // 报告进度
            DispatchEvent(ServiceEvents.OnServiceProcessing, null);
        }
    }
}
```

### 设计特点

1. **生命周期管理**：完整的服务生命周期管理
2. **事件驱动**：基于事件的状态通知和进度报告
3. **接口分离**：不同功能通过接口分离，便于实现
4. **配置驱动**：通过配置管理服务，支持参数注入
5. **进度支持**：内置进度报告机制
6. **单例模式**：ServiceManager提供全局访问点

### 注意事项

1. **初始化顺序**：服务按配置顺序初始化
2. **事件监听**：及时清理不需要的事件监听
3. **异常处理**：服务初始化失败需要妥善处理
4. **资源管理**：确保服务正确清理资源
5. **线程安全**：多线程环境需要额外的同步机制 