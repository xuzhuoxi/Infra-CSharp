# Service API Documentation

## Namespace: JLGames.Infra.Service

### Interfaces

#### IService
Service base interface

```csharp
/// <summary>
/// Service base interface
/// </summary>
public interface IService
{
    /// <summary>
    /// Unique service name; set from ServiceConfig.AddConfig.
    /// </summary>
    string ServiceName { get; set; }
}
```

#### IInitService
Service initialization interface. Only when the current interface is implemented and configured into ServiceConfig will the Init method run during the initialization process.

```csharp
/// <summary>
/// Service initialization interface
/// Only when the current interface is implemented and configured into ServiceConfig,
/// the init method will be executed during the initialization process
/// </summary>
public interface IInitService : IService, IEventDispatcher
{
    /// <summary>
    /// Whether the initialization has been completed
    /// </summary>
    bool IsInited { get; }

    /// <summary>
    /// Initialize the service; dispatch ServiceEvents.OnServiceInited with IService.ServiceName when done.
    /// </summary>
    void Init();
}
```

#### ILoadDataService
Load persisted or external data for a service. Invoked sequentially by `ServiceManager.LoadServicesData` for each registered implementation.

```csharp
/// <summary>
/// Load persisted or external data for a service.
/// Invoked sequentially by ServiceManager.LoadServicesData for each registered implementation.
/// </summary>
public interface ILoadDataService : IEventDispatcher
{
    /// <summary>
    /// Load data; dispatch ServiceEvents.OnServiceDataLoaded with the service name when done.
    /// </summary>
    void LoadData();
}
```

#### ISaveDataService
Persist service data. Invoked sequentially by `ServiceManager.SaveServicesData` for each registered implementation.

```csharp
/// <summary>
/// Persist service data.
/// Invoked sequentially by ServiceManager.SaveServicesData for each registered implementation.
/// </summary>
public interface ISaveDataService : IEventDispatcher
{
    /// <summary>
    /// Save data; dispatch ServiceEvents.OnServiceDataSaved with the service name when done.
    /// </summary>
    void SaveData();
}
```

#### IInitDataService
Data initialization phase for a service. Only when implemented and registered in ServiceConfig does InitData run after all IInitService instances complete during startup.

```csharp
/// <summary>
/// Data initialization phase for a service.
/// Only when implemented and registered in ServiceConfig,
/// InitData runs after all IInitService instances complete during startup.
/// </summary>
public interface IInitDataService : IService, IEventDispatcher
{
    /// <summary>
    /// Whether data initialization has been completed
    /// </summary>
    bool IsDataInited { get; }

    /// <summary>
    /// Initialize runtime data; dispatch ServiceEvents.OnServiceDataInited with IService.ServiceName when done.
    /// </summary>
    void InitData();
}
```

#### IArgumentService
Receives constructor-style arguments before initialization. Invoked by ServiceManager for each entry in ServiceConfig during `ServiceManager.StartInitalization`.

```csharp
/// <summary>
/// Receives constructor-style arguments before initialization.
/// Invoked by ServiceManager for each entry in ServiceConfig during ServiceManager.StartInitalization.
/// </summary>
public interface IArgumentService
{
    /// <summary>
    /// Inject data.
    /// </summary>
    /// <param name="args">Arguments from ServiceInfo.Args; may be null or empty.</param>
    void InjectArgument(object[] args);
}
```

#### IAwakableService
Early activation hook before `IInitService.Init`. Invoked synchronously by ServiceManager during startup; must not use async.

```csharp
/// <summary>
/// Early activation hook before IInitService.Init.
/// Invoked synchronously by ServiceManager during startup; must not use async.
/// </summary>
public interface IAwakableService
{
    /// <summary>
    /// Awake service
    /// Async is not allowed
    /// </summary>
    void Awake();
}
```

#### IClearService
Resets a service to its pre-init state (listeners, flags, timers, etc.). Called on each service by `ServiceManager.ClearServices`.

```csharp
/// <summary>
/// Resets a service to its pre-init state (listeners, flags, timers, etc.).
/// Called on each service by ServiceManager.ClearServices.
/// </summary>
public interface IClearService
{
    /// <summary>
    /// reset
    /// Clear events, clear timers, etc.
    /// </summary>
    void Clear();
}
```

#### IProgressingService
Reports granular init progress; replaces the default one-step count per IInitService / IInitDataService. Increment progress and dispatch `ServiceEvents.OnServiceProcessing` from the service implementation.

```csharp
/// <summary>
/// Reports granular init progress; replaces the default one-step count per IInitService / IInitDataService.
/// Increment progress and dispatch ServiceEvents.OnServiceProcessing from the service implementation.
/// </summary>
public interface IProgressingService : IEventDispatcher
{
    /// <summary>
    /// Total progress
    /// [0,int.Max)
    /// </summary>
    uint ProgressingLen { get; }

    /// <summary>
    /// Current amount of progress
    /// [0,Total]
    /// </summary>
    uint ProgressingCurrent { get; }
}
```

### Classes

#### ServiceManager
Orchestrates service injection, activation, initialization, and load/save; reports aggregate progress via events.

Step-by-step Init / InitData / Load / Save is implemented by internal `ServiceHandler` and its derived types (`ServiceInitHandler`, `ServiceInitDataHandler`, `ServiceLoadDataHandler`, `ServiceSaveDataHandler`). Those types are `internal` and are not part of the public API.

```csharp
/// <summary>
/// Orchestrates service injection, activation, initialization, and load/save; reports aggregate progress via events.
/// </summary>
public sealed class ServiceManager : EventDispatcher
{
    /// <summary>
    /// Processing percentage
    /// </summary>
    public float ProcessingPercentage { get; }

    /// <summary>
    /// Current progress total length
    /// </summary>
    public uint ProcessingLen { get; }

    /// <summary>
    /// Progress completed
    /// </summary>
    public uint ProcessingFinished { get; }

    /// <summary>
    /// cleanup event listener
    /// </summary>
    public void ClearEvents();

    /// <summary>
    /// Service cleanup
    /// </summary>
    public void ClearServices();

    /// <summary>
    /// service data loading
    /// </summary>
    /// <param name="endCall">Invoked when all configured load-data services finish.</param>
    public void LoadServicesData(Callback endCall);

    /// <summary>
    /// Service data storage
    /// </summary>
    /// <param name="endCall">Invoked when all configured save-data services finish.</param>
    public void SaveServicesData(Callback endCall);

    /// <summary>
    /// Initialize the configured service
    /// </summary>
    /// <param name="endCall">Invoked after Init and InitData complete for all services.</param>
    public void StartInitalization(Callback endCall);

    /// <summary>
    /// Global service manager singleton.
    /// </summary>
    public static ServiceManager Shared { get; }

    /// <summary>
    /// Shorthand for ServiceConfig.Shared.
    /// </summary>
    public static ServiceConfig Config { get; }
}
```

`StartInitalization` reads the current snapshot from `ServiceConfig.Shared.ServiceInfos` and runs argument injection, activation, Init, then InitData. The method name matches source spelling (`StartInitalization`, missing an `i`). `endCall` is `JLGames.Infra.Callback`, not a delegate; construct it with `new Callback(...)`.

#### ServiceInfo
Describes one registered service: name, implementation instance, and optional constructor arguments. Implements `ICloneable<ServiceInfo>`.

```csharp
/// <summary>
/// Describes one registered service: name, implementation instance, and optional constructor arguments.
/// </summary>
public class ServiceInfo : ICloneable<ServiceInfo>
{
    /// <summary>
    /// Unique service name.
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// Service implementation as IService.
    /// </summary>
    public IService ServiceImpl { get; }

    /// <summary>
    /// Arguments passed to IArgumentService.InjectArgument when applicable.
    /// </summary>
    public object[] Args { get; }

    /// <summary>
    /// Whether the implementation implements IAwakableService.
    /// </summary>
    public bool IsAwakableService { get; }

    /// <summary>
    /// Whether the implementation implements IInitService.
    /// </summary>
    public bool IsInitService { get; }

    /// <summary>
    /// Whether the implementation implements IArgumentService.
    /// </summary>
    public bool IsArgumentService { get; }

    /// <summary>
    /// Whether the implementation implements IProgressingService.
    /// </summary>
    public bool IsProgressingService { get; }

    /// <summary>
    /// Whether the implementation implements IInitDataService.
    /// </summary>
    public bool IsInitDataService { get; }

    /// <summary>
    /// Whether the implementation implements ILoadDataService.
    /// </summary>
    public bool IsLoadDataService { get; }

    /// <summary>
    /// Whether the implementation implements ISaveDataService.
    /// </summary>
    public bool IsSaveDataService { get; }

    /// <summary>
    /// Check the interface state of the implementing object
    /// </summary>
    /// <typeparam name="T">Interface or base type to test.</typeparam>
    /// <returns>True if the implementation instance is assignable to T.</returns>
    public bool CheckServiceImpl<T>() where T : class;

    /// <summary>
    /// Get the implementation object of the service
    /// </summary>
    /// <typeparam name="T">Expected implementation type.</typeparam>
    /// <returns>Cast instance, or null if incompatible.</returns>
    public T GetServiceImpl<T>() where T : class;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceName">Unique service name.</param>
    /// <param name="serviceImpl">Service implementation instance.</param>
    /// <param name="args">Optional arguments for IArgumentService.</param>
    public ServiceInfo(string serviceName, IService serviceImpl, params object[] args);

    /// <summary>
    /// Clone
    /// </summary>
    /// <returns>A shallow copy sharing the same implementation reference.</returns>
    public ServiceInfo Clone();

    public override string ToString();
}
```

All properties are read-only; create instances via the constructor. `Clone()` shares the same implementation reference.

#### ServiceConfig
Registry of configured services; used by ServiceManager during startup. The constructor is private; access only through the `Shared` singleton.

```csharp
/// <summary>
/// Registry of configured services; used by ServiceManager during startup.
/// </summary>
public class ServiceConfig
{
    /// <summary>
    /// Total number of configured services
    /// </summary>
    public int ServiceSize { get; }

    /// <summary>
    /// Get all service configuration list
    /// </summary>
    public ServiceInfo[] ServiceInfos { get; }

    /// <summary>
    /// Add a service to the end of the configuration list
    /// </summary>
    /// <param name="sc">Service entry to append.</param>
    /// <param name="ignoreSame">When false, throws if ServiceInfo.ServiceName already exists.</param>
    /// <exception cref="Exception">Thrown when ignoreSame is false and the service name is duplicate.</exception>
    public void AddConfig(ServiceInfo sc, bool ignoreSame = true);

    /// <summary>
    /// Get the specified server implementation object
    /// </summary>
    /// <param name="serviceName">Unique service name.</param>
    /// <returns>Implementation instance, or null if not found.</returns>
    public IService GetServiceImpl(string serviceName);

    /// <summary>
    /// Take the specified server implementation object and use generics to reduce the amount of code writing
    /// </summary>
    /// <param name="serviceName">Unique service name.</param>
    /// <typeparam name="T">Expected implementation type.</typeparam>
    /// <returns>Cast implementation, or null if missing or incompatible.</returns>
    public T GetServiceImpl<T>(string serviceName) where T : class;

    /// <summary>
    /// Check service existence
    /// </summary>
    /// <param name="serviceName">Unique service name.</param>
    /// <returns>True if a service with this name is registered.</returns>
    public bool ContainsService(string serviceName);

    /// <summary>
    /// Get a specified service configuration information
    /// </summary>
    /// <param name="serviceName">Unique service name.</param>
    /// <returns>Configuration entry, or null if not found.</returns>
    public ServiceInfo GetServiceInfo(string serviceName);

    /// <summary>
    /// Global service configuration singleton.
    /// </summary>
    public static ServiceConfig Shared { get; }
}
```

`AddConfig` writes `sc.ServiceName` onto `sc.ServiceImpl.ServiceName`. `ignoreSame` defaults to `true` (duplicate names are allowed); when `false` and the name already exists, it throws `Exception` with a message like `重复的ServiceName:{name}`. `ServiceInfos` returns a snapshot array of the internal list and is not assignable.

#### ServiceBase
Abstract base for services: lifecycle flags, progress reporting, and event dispatch. Extends `EventDispatcher` and implements `IService` and `IClearService`. It does not itself implement phase interfaces such as `IInitService` / `IInitDataService` / `IProgressingService`; subclasses declare those as needed.

```csharp
/// <summary>
/// Abstract base for services: lifecycle flags, progress reporting, and event dispatch.
/// </summary>
public abstract class ServiceBase : EventDispatcher, IService, IClearService
{
    protected delegate void ProcessingCall();

    /// <inheritdoc />
    public string ServiceName { get; set; }

    protected bool m_Inited = false;
    protected bool m_DataInited = false;

    /// <summary>
    /// Whether IInitService.Init has completed for this service.
    /// </summary>
    public bool IsInited { get; }

    /// <summary>
    /// Whether IInitDataService.InitData has completed for this service.
    /// </summary>
    public bool IsDataInited { get; }

    protected uint m_ProgressingLen = 1;
    protected uint m_ProgressingCurrent = 0;

    /// <summary>
    /// Total progress steps when implementing IProgressingService.
    /// </summary>
    public uint ProgressingLen { get; }

    /// <summary>
    /// Completed progress steps; used with ProgressingLen.
    /// </summary>
    public uint ProgressingCurrent { get; }

    /// <inheritdoc />
    public virtual void Clear();

    /// <summary>
    /// Increments progress, dispatches OnServiceProcessing, then invokes call.
    /// </summary>
    protected virtual void InvokdProcessing(ProcessingCall call);

    /// <summary>
    /// Marks init complete and dispatches OnServiceInited (data is ServiceName).
    /// </summary>
    protected virtual void InvokeInited();

    /// <summary>
    /// Marks data init complete and dispatches OnServiceDataInited (data is ServiceName).
    /// </summary>
    protected virtual void InvokeDataInited();

    /// <summary>
    /// Dispatches OnServiceDataLoaded (data is ServiceName).
    /// </summary>
    protected virtual void InvokeDataLoaded();

    /// <summary>
    /// Dispatches OnServiceDataSaved (data is ServiceName).
    /// </summary>
    protected virtual void InvokeDataSaved();
}
```

`InvokdProcessing` matches source spelling (missing an `e`). Subclasses should call these helpers when a phase finishes; otherwise ServiceManager waits indefinitely for the completion event.

### Events

#### ServiceEvents
Service event constants. Note: `OnServiceProcessing` uses a period (`.`) in its string value; all other constants use a colon (`:`).

```csharp
/// <summary>
/// service event
/// </summary>
public static class ServiceEvents
{
    /// <summary>
    /// A single service initializes the progress update event, which is dispatched by the service instance.
    /// The service instance must be an implementation class of the IProgressingService interface
    /// </summary>
    public const string OnServiceProcessing = "ServiceEvents.OnServiceProcessing";

    /// <summary>
    /// Service initialization process progress update
    /// </summary>
    public const string OnInitializationProcessing = "ServiceEvents:OnInitializationProcessing";

    /// <summary>
    /// Service initialization process completed
    /// </summary>
    public const string OnInitializationFinish = "ServiceEvents:OnInitializationFinish";

    /// <summary>
    /// Service argument injection result event, dispatched by ServiceManager.
    /// Succ=true when the service implements the IArgumentService interface.
    /// Event data format: ServiceResultData
    /// </summary>
    public const string OnServiceInjected = "ServiceEvents:OnServiceInjected";

    /// <summary>
    /// All service argument injection completion event
    /// Event data format: null
    /// </summary>
    public const string OnServiceAllInjected = "ServiceEvents:OnServiceAllInjected";

    /// <summary>
    /// Service activation result event, dispatched by ServiceManager.
    /// Succ=true when the service implements the IAwakableService interface.
    /// Event data format: ServiceResultData
    /// </summary>
    public const string OnServiceAwaked = "ServiceEvents:OnServiceAwaked";

    /// <summary>
    /// All service activation completion event
    /// Event data format: null
    /// </summary>
    public const string OnServiceAllAwaked = "ServiceEvents:OnServiceAllAwaked";

    /// <summary>
    /// Single service initialization start event, dispatched by ServiceManager
    /// Event data format: ServiceResultData
    /// </summary>
    public const string OnServiceInitStart = "ServiceEvents:OnServiceInitStart";

    /// <summary>
    /// A single service initialization complete event, which are dispatched by the service instance.
    /// Re-dispatched after being captured by ServiceManager.
    /// Event data format: service name string
    /// </summary>
    public const string OnServiceInited = "ServiceEvents:OnServiceInited";

    /// <summary>
    /// All service initialization complete event
    /// Event data: null
    /// </summary>
    public const string OnServiceAllInited = "ServiceEvents:OnServiceAllInited";

    /// <summary>
    /// Single service data initialization start event, dispatched by ServiceManager
    /// Event data format: ServiceResultData
    /// </summary>
    public const string OnServiceDataInitStart = "ServiceEvents:OnServiceDataInitStart";

    /// <summary>
    /// A single service data initialization complete event, which are dispatched by the service instance.
    /// Re-dispatched after being captured by ServiceManager.
    /// Event data format: service name string
    /// </summary>
    public const string OnServiceDataInited = "ServiceEvents:OnServiceDataInited";

    /// <summary>
    /// All service data initialization complete event
    /// Event data: null
    /// </summary>
    public const string OnServiceDataAllInited = "ServiceEvents:OnServiceDataAllInited";

    /// <summary>
    /// A single data service load data start event, dispatched by ServiceManager
    /// Succ=true when the service implements the ILoadDataService interface.
    /// Event data format: ServiceResultData
    /// </summary>
    public const string OnServiceDataLoadStart = "ServiceEvents:OnServiceDataLoadStart";

    /// <summary>
    /// A single data service load data completion event, which are dispatched by the service instance.
    /// Re-dispatched after being captured by ServiceManager.
    /// Event data format: service name string
    /// </summary>
    public const string OnServiceDataLoaded = "ServiceEvents:OnServiceDataLoaded";

    /// <summary>
    /// All data service loading data completion event
    /// Event data: null
    /// </summary>
    public const string OnServiceDataAllLoaded = "ServiceEvents:OnServiceDataAllLoaded";

    /// <summary>
    /// A single data service save data start event, dispatched by ServiceManager
    /// Succ=true when the service implements the ISaveDataService interface.
    /// Event data format: ServiceResultData
    /// </summary>
    public const string OnServiceDataSaveStart = "ServiceEvents:OnServiceDataSaveStart";

    /// <summary>
    /// A single data service saves data completion events, which are dispatched by the service instance.
    /// Re-dispatched after being captured by ServiceManager.
    /// Event data format: service name string
    /// </summary>
    public const string OnServiceDataSaved = "ServiceEvents:OnServiceDataSaved";

    /// <summary>
    /// All data services save data complete event
    /// Event data: null
    /// </summary>
    public const string OnServiceDataAllSaved = "ServiceEvents:OnServiceDataAllSaved";
}
```

### Data Classes

#### ServiceResultData
Payload for per-service result events (inject, awake, init start, load/save start, etc.). This is a struct with public fields, not properties.

```csharp
/// <summary>
/// Payload for per-service result events data structure (inject, awake, init start, load/save start, etc.).
/// </summary>
public struct ServiceResultData
{
    /// <summary>
    /// Affected service name.
    /// </summary>
    public string ServiceName;

    /// <summary>
    /// Whether the service implements the capability required for that step (see event docs in ServiceEvents).
    /// </summary>
    public bool Succ;
}
```

`Succ` means the service implements the interface required for that step (for example `IArgumentService` during injection), not that business logic succeeded.

### Function Description

#### Service Lifecycle

**Initialization Phase** (`StartInitalization`)
1. **Argument Injection**: For each configured entry, if it implements `IArgumentService`, call `InjectArgument(info.Args)` and dispatch `OnServiceInjected`; then dispatch `OnServiceAllInjected`.
2. **Service Activation**: For each configured entry, if it implements `IAwakableService`, call `Awake()` synchronously and dispatch `OnServiceAwaked`; then dispatch `OnServiceAllAwaked`. Async is not allowed.
3. **Service Initialization**: Process entries in configuration order. Dispatch `OnServiceInitStart` for each; if it implements `IInitService`, call `Init()` and wait for that service to dispatch `OnServiceInited`. Then dispatch `OnServiceAllInited`.
4. **Data Initialization**: After all Init work finishes, process entries in configuration order. Dispatch `OnServiceDataInitStart` for each; if it implements `IInitDataService`, call `InitData()` and wait for `OnServiceDataInited`. Then dispatch `OnServiceDataAllInited`, invoke `endCall`, and finally dispatch `OnInitializationFinish`.

**Runtime Phase**
- `LoadServicesData`: calls `ILoadDataService.LoadData()` in configuration order and waits for `OnServiceDataLoaded`; dispatches `OnServiceDataLoadStart` / `OnServiceDataLoaded` / `OnServiceDataAllLoaded`.
- `SaveServicesData`: calls `ISaveDataService.SaveData()` in configuration order and waits for `OnServiceDataSaved`; dispatches `OnServiceDataSaveStart` / `OnServiceDataSaved` / `OnServiceDataAllSaved`.

**Cleanup Phase**
- `ClearServices` first removes listeners on the manager, then calls `Clear()` on each `IClearService` in **reverse** configuration order.
- `ClearEvents` only removes listeners on ServiceManager itself.

Entries that do not implement a given interface are skipped, but start events are still dispatched with `ServiceResultData.Succ` set to `false`.

#### Progress Management

Supports granular progress through `IProgressingService`:
- **ProgressingLen**: total progress
- **ProgressingCurrent**: current amount of progress
- When a service implements `IProgressingService`, those fields are used for its contribution; otherwise each `IInitService` / `IInitDataService` counts as 1 step (gated by `IsInited` / `IsDataInited` for the current value).
- ServiceManager updates `ProcessingFinished` on `OnServiceProcessing`, `OnServiceInited`, and `OnServiceDataInited`, then dispatches `OnInitializationProcessing`.
- `ProcessingPercentage` returns `1` when finished count reaches total length; otherwise `ProcessingFinished / ProcessingLen`.

#### Event-Driven

The service system is event-driven:
- Each lifecycle phase has corresponding start/complete events
- Per-service completion events for Init / InitData / Load / Save must be dispatched by the service instance; ServiceManager captures and re-dispatches them
- Supports progress reporting and status notifications

### Usage Examples

#### Basic Service Implementation
```csharp
public class MyService : ServiceBase, IInitService, ILoadDataService, ISaveDataService
{
    public void Init()
    {
        Console.WriteLine($"Initialize service: {ServiceName}");
        InvokeInited();
    }

    public void LoadData()
    {
        Console.WriteLine($"Load data: {ServiceName}");
        InvokeDataLoaded();
    }

    public void SaveData()
    {
        Console.WriteLine($"Save data: {ServiceName}");
        InvokeDataSaved();
    }
}
```

If you do not inherit `ServiceBase`, you must `DispatchEvent` the matching completion event yourself (for example `ServiceEvents.OnServiceInited` with the service name); otherwise the pipeline does not continue.

#### Service Configuration
```csharp
var serviceConfig = ServiceConfig.Shared;
serviceConfig.AddConfig(new ServiceInfo("UserService", new UserService(), "userdb"));
serviceConfig.AddConfig(new ServiceInfo("ConfigService", new ConfigService()));

var user = serviceConfig.GetServiceImpl<UserService>("UserService");
```

`ServiceInfos` is read-only and cannot be assigned. Register services with `AddConfig`, which writes the service name back onto the implementation instance's `ServiceName`.

#### Service Manager Usage
```csharp
var serviceManager = ServiceManager.Shared;

serviceManager.AddEventListener(ServiceEvents.OnInitializationProcessing, evd =>
{
    Console.WriteLine($"Initialization progress: {serviceManager.ProcessingPercentage:P}");
});

serviceManager.AddEventListener(ServiceEvents.OnInitializationFinish, evd =>
{
    Console.WriteLine("All services initialization completed!");
});

serviceManager.StartInitalization(new Callback(_ =>
{
    Console.WriteLine("Initialization callback executed");
}));
```

#### Data Operations
```csharp
serviceManager.LoadServicesData(new Callback(_ =>
{
    Console.WriteLine("Data loading completed");
}));

serviceManager.SaveServicesData(new Callback(_ =>
{
    Console.WriteLine("Data saving completed");
}));
```

#### Argument Service Example
```csharp
public class DatabaseService : ServiceBase, IArgumentService
{
    private string connectionString;

    public void InjectArgument(object[] args)
    {
        connectionString = args != null && args.Length > 0 ? args[0] as string : null;
        Console.WriteLine($"Inject database connection: {connectionString}");
    }
}

ServiceConfig.Shared.AddConfig(
    new ServiceInfo("DatabaseService", new DatabaseService(), "Server=.;Database=app"));
```

#### Progress Service Example
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

`InvokdProcessing` increments `m_ProgressingCurrent` and dispatches `OnServiceProcessing` (data is `ServiceName`). When implementing `IProgressingService`, still dispatch the matching Init / InitData completion event when that phase finishes.

### Design Features

1. **Lifecycle Management**: Full lifecycle (inject → awake → Init → InitData, plus Load / Save / Clear)
2. **Event-Driven**: Status notifications and progress reporting via events; phase completion depends on services dispatching their own completion events
3. **Interface Separation**: Capabilities are split across interfaces so implementations can opt in
4. **Configuration-Driven**: Register services through `ServiceConfig.AddConfig`, with optional argument injection
5. **Progress Support**: Built-in progress reporting
6. **Singleton Pattern**: `ServiceManager.Shared` and `ServiceConfig.Shared` provide global access points

### Notes

1. **Initialization Order**: Services are processed in `ServiceConfig` registration order; cleanup calls `IClearService.Clear` in reverse order
2. **Completion Events**: `Init` / `InitData` / `LoadData` / `SaveData` must dispatch the matching completion event, or later services will not start
3. **Event Listening**: Remove unused listeners (`ClearEvents` / `ClearServices` / the service's own `Clear`)
4. **Resource Management**: Ensure services clean up resources; `ServiceBase.Clear` removes listeners and resets `IsInited` / `IsDataInited`
5. **Thread Safety**: Additional synchronization is required in multi-threaded environments; `IAwakableService.Awake` must not be async
6. **Callback**: The callback type for `StartInitalization` / `LoadServicesData` / `SaveServicesData` is the `Callback` class, not a parameterless lambda
