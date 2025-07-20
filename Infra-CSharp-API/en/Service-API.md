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
    /// Service name
    /// </summary>
    string ServiceName { get; set; }
}
```

#### IInitService
Service initialization interface

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
    /// Initialize base data
    /// </summary>
    void Init();
}
```

#### ILoadDataService
Load data processing interface

```csharp
/// <summary>
/// Load data processing interface
/// </summary>
public interface ILoadDataService : IEventDispatcher
{
    /// <summary>
    /// Load Data
    /// </summary>
    void LoadData();
}
```

#### ISaveDataService
Save data processing interface

```csharp
/// <summary>
/// Save data processing interface
/// </summary>
public interface ISaveDataService : IEventDispatcher
{
    /// <summary>
    /// Save data
    /// </summary>
    void SaveData();
}
```

#### IInitDataService
Initialize data service interface

```csharp
/// <summary>
/// Initialize data service interface
/// Used for the data initialization phase of services
/// </summary>
public interface IInitDataService : IEventDispatcher
{
    /// <summary>
    /// Whether data initialization has been completed
    /// </summary>
    bool IsDataInited { get; }

    /// <summary>
    /// Initialize data
    /// </summary>
    void InitData();
}
```

#### IArgumentService
Argument service interface

```csharp
/// <summary>
/// Argument service interface
/// Used for receiving external parameter injection
/// </summary>
public interface IArgumentService
{
    /// <summary>
    /// Inject arguments
    /// </summary>
    /// <param name="args">Argument data</param>
    void InjectArgument(object args);
}
```

#### IAwakableService
Awakable service interface

```csharp
/// <summary>
/// Awakable service interface
/// Used for the service awakening phase
/// </summary>
public interface IAwakableService
{
    /// <summary>
    /// Awake service
    /// </summary>
    void Awake();
}
```

#### IClearService
Clear service interface

```csharp
/// <summary>
/// Clear service interface
/// Used for the service cleanup phase
/// </summary>
public interface IClearService
{
    /// <summary>
    /// Clear service
    /// </summary>
    void Clear();
}
```

#### IProgressingService
Progress service interface

```csharp
/// <summary>
/// Progress service interface
/// Used for services that support progress reporting
/// </summary>
public interface IProgressingService
{
    /// <summary>
    /// Total progress length
    /// </summary>
    uint ProgressingLen { get; }

    /// <summary>
    /// Current progress
    /// </summary>
    uint ProgressingCurrent { get; }
}
```

### Classes

#### ServiceManager
Service manager class

```csharp
/// <summary>
/// Service manager
/// Responsible for service lifecycle management, including initialization, data loading, saving, etc.
/// </summary>
public sealed class ServiceManager : EventDispatcher
{
    /// <summary>
    /// Callback after all initialization is complete, including Init function and InitData function
    /// </summary>
    private Callback m_FinishCall;

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
    /// Cleanup event listener
    /// </summary>
    public void ClearEvents();

    /// <summary>
    /// Service cleanup
    /// </summary>
    public void ClearServices();

    /// <summary>
    /// Service data loading
    /// </summary>
    /// <param name="endCall"></param>
    public void LoadServicesData(Callback endCall);

    /// <summary>
    /// Service data storage
    /// </summary>
    /// <param name="endCall"></param>
    public void SaveServicesData(Callback endCall);

    /// <summary>
    /// Initialize the configured service
    /// If it has been initialized once, try to execute the callback directly
    /// </summary>
    /// <param name="endCall"></param>
    public void StartInitalization(Callback endCall);

    /// <summary>
    /// Singleton instance
    /// </summary>
    public static ServiceManager Shared { get; }

    /// <summary>
    /// Configuration accessor
    /// </summary>
    public static ServiceConfig Config { get; }
}
```

#### ServiceInfo
Service information class

```csharp
/// <summary>
/// Service information class
/// Contains service configuration information and instances
/// </summary>
public class ServiceInfo
{
    /// <summary>
    /// Service name
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Service implementation
    /// </summary>
    public object ServiceImpl { get; set; }

    /// <summary>
    /// Argument data
    /// </summary>
    public object Args { get; set; }

    /// <summary>
    /// Whether it is an initialization service
    /// </summary>
    public bool IsInitService { get; }

    /// <summary>
    /// Whether it is a data initialization service
    /// </summary>
    public bool IsInitDataService { get; }

    /// <summary>
    /// Whether it is an argument service
    /// </summary>
    public bool IsArgumentService { get; }

    /// <summary>
    /// Whether it is an awakable service
    /// </summary>
    public bool IsAwakableService { get; }

    /// <summary>
    /// Whether it is a progress service
    /// </summary>
    public bool IsProgressingService { get; }

    /// <summary>
    /// Get service implementation
    /// </summary>
    /// <typeparam name="T">Service type</typeparam>
    /// <returns>Service instance</returns>
    public T GetServiceImpl<T>() where T : class;
}
```

#### ServiceConfig
Service configuration class

```csharp
/// <summary>
/// Service configuration class
/// Manages service configuration information
/// </summary>
public class ServiceConfig
{
    /// <summary>
    /// Service information array
    /// </summary>
    public ServiceInfo[] ServiceInfos { get; set; }

    /// <summary>
    /// Singleton instance
    /// </summary>
    public static ServiceConfig Shared { get; }
}
```

#### ServiceBase
Service base class

```csharp
/// <summary>
/// Service base class
/// Provides basic service functionality implementation
/// </summary>
public abstract class ServiceBase : IService
{
    /// <summary>
    /// Service name
    /// </summary>
    public string ServiceName { get; set; }
}
```

### Events

#### ServiceEvents
Service event constants

```csharp
/// <summary>
/// Service event constants
/// Defines various events in the service lifecycle
/// </summary>
public static class ServiceEvents
{
    /// <summary>
    /// Service injection event
    /// </summary>
    public const string OnServiceInjected = "OnServiceInjected";

    /// <summary>
    /// All services injection completed event
    /// </summary>
    public const string OnServiceAllInjected = "OnServiceAllInjected";

    /// <summary>
    /// Service awakening event
    /// </summary>
    public const string OnServiceAwaked = "OnServiceAwaked";

    /// <summary>
    /// All services awakening completed event
    /// </summary>
    public const string OnServiceAllAwaked = "OnServiceAllAwaked";

    /// <summary>
    /// Service initialization event
    /// </summary>
    public const string OnServiceInited = "OnServiceInited";

    /// <summary>
    /// All services initialization completed event
    /// </summary>
    public const string OnServiceAllInited = "OnServiceAllInited";

    /// <summary>
    /// Service data initialization event
    /// </summary>
    public const string OnServiceDataInited = "OnServiceDataInited";

    /// <summary>
    /// All service data initialization completion event
    /// </summary>
    public const string OnServiceDataAllInited = "OnServiceDataAllInited";

    /// <summary>
    /// Service processing progress event
    /// </summary>
    public const string OnServiceProcessing = "OnServiceProcessing";

    /// <summary>
    /// Initialization progress event
    /// </summary>
    public const string OnInitializationProcessing = "OnInitializationProcessing";

    /// <summary>
    /// Initialization completion event
    /// </summary>
    public const string OnInitializationFinish = "OnInitializationFinish";
}
```

### Data Classes

#### ServiceResultData
Service result data

```csharp
/// <summary>
/// Service result data
/// Used for event passing of service execution results
/// </summary>
public class ServiceResultData
{
    /// <summary>
    /// Service name
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Whether successful
    /// </summary>
    public bool Succ { get; set; }
}
```

### Function Description

#### Service Lifecycle

**Initialization Phase**
1. **Argument Injection**: Services implementing IArgumentService interface receive external parameters
2. **Service Awakening**: Services implementing IAwakableService interface perform awakening operations
3. **Service Initialization**: Services implementing IInitService interface perform initialization
4. **Data Initialization**: Services implementing IInitDataService interface perform data initialization

**Runtime Phase**
- Services run normally, handling business logic
- Supports data loading and saving operations

**Cleanup Phase**
- Services implementing IClearService interface perform cleanup operations
- Release resources and event listeners

#### Progress Management

Supports progress reporting through IProgressingService interface:
- **ProgressingLen**: Total progress length
- **ProgressingCurrent**: Current progress
- ServiceManager automatically calculates overall progress percentage

#### Event-Driven

Service system based on event-driven architecture:
- Each lifecycle phase has corresponding events
- Supports progress reporting and status notifications
- Facilitates monitoring and debugging

### Usage Examples

#### Basic Service Implementation
```csharp
// Implement a basic service
public class MyService : ServiceBase, IInitService, ILoadDataService, ISaveDataService
{
    public bool IsInited { get; private set; }

    public void Init()
    {
        Console.WriteLine($"Initialize service: {ServiceName}");
        IsInited = true;
    }

    public void LoadData()
    {
        Console.WriteLine($"Load data: {ServiceName}");
    }

    public void SaveData()
    {
        Console.WriteLine($"Save data: {ServiceName}");
    }
}
```

#### Service Configuration
```csharp
// Configure services
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

#### Service Manager Usage
```csharp
// Get service manager
var serviceManager = ServiceManager.Shared;

// Listen to initialization progress
serviceManager.AddEventListener(ServiceEvents.OnInitializationProcessing, (evd) => {
    Console.WriteLine($"Initialization progress: {serviceManager.ProcessingPercentage:P}");
});

// Listen to initialization completion
serviceManager.AddEventListener(ServiceEvents.OnInitializationFinish, (evd) => {
    Console.WriteLine("All services initialization completed!");
});

// Start initialization
serviceManager.StartInitalization(() => {
    Console.WriteLine("Initialization callback executed");
});
```

#### Data Operations
```csharp
// Load service data
serviceManager.LoadServicesData(() => {
    Console.WriteLine("Data loading completed");
});

// Save service data
serviceManager.SaveServicesData(() => {
    Console.WriteLine("Data saving completed");
});
```

#### Argument Service Example
```csharp
public class DatabaseService : ServiceBase, IArgumentService
{
    private string connectionString;

    public void InjectArgument(object args)
    {
        var config = args as dynamic;
        connectionString = config?.connectionString;
        Console.WriteLine($"Inject database connection: {connectionString}");
    }
}
```

#### Progress Service Example
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
            // Process file
            processedFiles++;
            
            // Report progress
            DispatchEvent(ServiceEvents.OnServiceProcessing, null);
        }
    }
}
```

### Design Features

1. **Lifecycle Management**: Complete service lifecycle management
2. **Event-Driven**: Event-based status notifications and progress reporting
3. **Interface Separation**: Different functionalities separated through interfaces for easy implementation
4. **Configuration-Driven**: Service management through configuration, supports parameter injection
5. **Progress Support**: Built-in progress reporting mechanism
6. **Singleton Pattern**: ServiceManager provides global access point

### Notes

1. **Initialization Order**: Services initialize according to configuration order
2. **Event Listening**: Clean up unnecessary event listeners in time
3. **Exception Handling**: Service initialization failures need to be handled properly
4. **Resource Management**: Ensure services properly clean up resources
5. **Thread Safety**: Additional synchronization mechanisms required in multi-threaded environments 