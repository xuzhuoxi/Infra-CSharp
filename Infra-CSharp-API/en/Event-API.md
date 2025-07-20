# Event API Documentation

## Namespace: JLGames.Infra.Event

### Interfaces

#### IEventListener
Event listener interface

```csharp
public interface IEventListener
{
    /// <summary>
    /// Add single event listener
    /// </summary>
    /// <param name="type">Event Type</param>
    /// <param name="handler">Listener Function</param>
    /// <param name="weight">Response Weight</param>
    /// <param name="tag">Function Tag</param>
    void OnceEventListener(string type, EventDelegates.EventHandler handler, int weight = EventConst.DefaultWeight, string tag = null);

    /// <summary>
    /// Add event listener
    /// </summary>
    /// <param name="type">Event Type</param>
    /// <param name="handler">Listener Function</param>
    /// <param name="tag">Function Tag</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// Add event listener
    /// </summary>
    /// <param name="type">Event Type</param>
    /// <param name="handler">Listener Function</param>
    /// <param name="weight">Response Weight</param>
    /// <param name="tag">Function Tag</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, string tag = null);

    /// <summary>
    /// Add event listener
    /// </summary>
    /// <param name="type">Event Type</param>
    /// <param name="handler">Listener Function</param>
    /// <param name="listeningTimes">Times of responses</param>
    /// <param name="tag">Function Tag</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, uint listeningTimes, string tag = null);

    /// <summary>
    /// Add event listener
    /// </summary>
    /// <param name="type">Event Type</param>
    /// <param name="handler">Listener Function</param>
    /// <param name="weight">Response Weight</param>
    /// <param name="listeningTimes">Times of responses</param>
    /// <param name="tag">Function Tag</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, uint listeningTimes, string tag = null);

    /// <summary>
    /// Delete event listener
    /// </summary>
    /// <param name="type">Event type</param>
    /// <param name="handler">Listener function</param>
    /// <param name="tag"></param>
    void RemoveEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// Delete event listener
    /// </summary>
    /// <param name="type">Event type</param>
    /// <param name="tag"></param>
    void RemoveEventListener(string type, string tag);

    /// <summary>
    /// Delete a type of event listeners
    /// </summary>
    /// <param name="type">Event type</param>
    void RemoveEventListener(string type);

    /// <summary>
    /// Clear all event listeners
    /// </summary>
    void RemoveEventListener();

    /// <summary>
    /// Dispose
    /// </summary>
    void Dispose();
}
```

#### IEventDispatcher
Event dispatcher interface, inherits from IEventListener

```csharp
public interface IEventDispatcher : IEventListener
{
    /// <summary>
    /// Trigger an event of a certain type and pass data
    /// </summary>
    /// <param name="evd">Event data</param>
    void DispatchEvent(EventData evd);

    /// <summary>
    /// Trigger an event of a certain type and pass data
    /// </summary>
    /// <param name="type">Event type</param>
    /// <param name="data">Event data (can be null)</param>
    void DispatchEvent(string type, object data);
}
```

### Classes

#### EventData
Event data class

```csharp
/// <summary>
/// Event data
/// </summary>
public class EventData
{
    private readonly string m_Type;
    private readonly object m_Data;
    private readonly IEventDispatcher m_CurrentDispatcher;

    public EventData(string type, object data);
    public EventData(string type, object data, IEventDispatcher currentDispatcher);

    /// <summary>
    /// Event type
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Event data
    /// </summary>
    public object Data { get; }

    public IEventDispatcher CurrentDispatcher { get; }
}
```

#### EventDispatcher
Event dispatcher implementation class

```csharp
/// <summary>
/// Event dispatcher
/// Responsible for event registration, dispatch and management
/// </summary>
public class EventDispatcher : IEventDispatcher
{
    // Specific implementation requires further analysis of file content
}
```

#### EventManager
Event manager class

```csharp
/// <summary>
/// Event manager
/// Global event management, provides singleton pattern event dispatch
/// </summary>
public class EventManager
{
    // Specific implementation requires further analysis of file content
}
```

### Static Classes

#### EventDelegates
Event delegate definitions

```csharp
public static class EventDelegates
{
    /// <summary>
    /// Delegate function
    /// </summary>
    /// <param name="evd"></param>
    public delegate void EventHandler(EventData evd);
}
```

#### EventConst
Event constant definitions

```csharp
public static class EventConst
{
    /// <summary>
    /// Event default weight
    /// </summary>
    public const int DefaultWeight = 50;
}
```

## Namespace: JLGames.Infra.Event.Async

### Interfaces

#### IAsyncEventListener
Asynchronous event listener interface

```csharp
/// <summary>
/// Asynchronous event listener interface
/// Supports asynchronous event processing
/// </summary>
public interface IAsyncEventListener
{
    // Specific implementation requires further analysis of file content
}
```

#### IAsyncEventDispatcher
Asynchronous event dispatcher interface

```csharp
/// <summary>
/// Asynchronous event dispatcher interface
/// Supports asynchronous event dispatch and processing
/// </summary>
public interface IAsyncEventDispatcher : IAsyncEventListener
{
    // Specific implementation requires further analysis of file content
}
```

### Classes

#### AsyncEventDispatcher
Asynchronous event dispatcher implementation class

```csharp
/// <summary>
/// Asynchronous event dispatcher
/// Dispatcher implementation supporting asynchronous event processing
/// </summary>
public class AsyncEventDispatcher : IAsyncEventDispatcher
{
    // Specific implementation requires further analysis of file content
}
```

### Function Description

#### Event System Architecture

**IEventListener**
Event listener interface that provides event listening management functionality:
- **OnceEventListener**: Add single event listener, automatically removed after one trigger
- **AddEventListener**: Add event listener, supports multiple overload forms
- **RemoveEventListener**: Delete event listener, supports deletion by type, function, tag
- **Dispose**: Release resources

**IEventDispatcher**
Event dispatcher interface, inherits from IEventListener, adds event triggering functionality:
- **DispatchEvent**: Trigger events, supports EventData object or type+data form

**EventData**
Event data encapsulation class:
- **Type**: Event type identifier
- **Data**: Data passed by the event
- **CurrentDispatcher**: Current dispatcher reference

#### Event Listening Features

1. **Weight System**: Controls listener execution order through weight parameter
2. **Tag Management**: Identifies and manages listeners through tag parameter
3. **Response Count**: Supports limiting listener response times
4. **Single Listening**: OnceEventListener automatic removal mechanism

#### Asynchronous Event Support

Provides asynchronous event processing capabilities through Async namespace:
- Supports asynchronous event listeners
- Supports asynchronous event dispatch
- Provides asynchronous event processing mechanism

### Usage Examples

#### Basic Event Listening and Dispatch
```csharp
// Create event dispatcher
var dispatcher = new EventDispatcher();

// Add event listener
dispatcher.AddEventListener("user.login", (evd) => {
    Console.WriteLine($"User login: {evd.Data}");
});

// Add event listener with weight
dispatcher.AddEventListener("user.login", (evd) => {
    Console.WriteLine("Record login log");
}, 10, "log_handler");

// Add single event listener
dispatcher.OnceEventListener("user.login", (evd) => {
    Console.WriteLine("Welcome new user!");
});

// Trigger event
dispatcher.DispatchEvent("user.login", new { userId = 123, username = "Zhang San" });
```

#### Event Data Usage
```csharp
// Create event data
var eventData = new EventData("data.update", new { id = 1, value = "new_value" });

// Add listener
dispatcher.AddEventListener("data.update", (evd) => {
    Console.WriteLine($"Event type: {evd.Type}");
    Console.WriteLine($"Event data: {evd.Data}");
    Console.WriteLine($"Dispatcher: {evd.CurrentDispatcher}");
});

// Dispatch event
dispatcher.DispatchEvent(eventData);
```

#### Event Listener Management
```csharp
// Add listeners with tags
dispatcher.AddEventListener("system.startup", StartupHandler, "startup_handler");
dispatcher.AddEventListener("system.shutdown", ShutdownHandler, "shutdown_handler");

// Remove listener by tag
dispatcher.RemoveEventListener("system.startup", "startup_handler");

// Remove all listeners of specific type
dispatcher.RemoveEventListener("system.shutdown");

// Clear all listeners
dispatcher.RemoveEventListener();

// Release resources
dispatcher.Dispose();
```

#### Asynchronous Event Processing
```csharp
// Create asynchronous event dispatcher
var asyncDispatcher = new AsyncEventDispatcher();

// Add asynchronous event listener
asyncDispatcher.AddEventListener("async.task", async (evd) => {
    await Task.Delay(1000); // Simulate asynchronous operation
    Console.WriteLine("Asynchronous task completed");
});

// Asynchronously trigger event
await asyncDispatcher.DispatchEventAsync("async.task", null);
```

#### Weight and Response Count Control
```csharp
// High priority listener (lower weight means higher priority)
dispatcher.AddEventListener("critical.event", CriticalHandler, 1);

// Normal priority listener
dispatcher.AddEventListener("critical.event", NormalHandler, 50);

// Low priority listener
dispatcher.AddEventListener("critical.event", LowPriorityHandler, 100);

// Listener with limited response count
dispatcher.AddEventListener("limited.event", LimitedHandler, 3); // Only respond 3 times

// Listener with weight and response count limit
dispatcher.AddEventListener("complex.event", ComplexHandler, 25, 5); // Weight 25, respond 5 times
```

### Design Features

1. **Flexibility**: Supports multiple event listening methods
2. **Extensibility**: Easy to extend through interface design
3. **Asynchronous Support**: Provides asynchronous event processing capabilities
4. **Resource Management**: Supports listener lifecycle management
5. **Performance Optimization**: Optimizes event processing through weight and tag systems

### Notes

1. **Memory Management**: Remove unnecessary listeners in time to avoid memory leaks
2. **Event Loop**: Avoid triggering the same event in event processing to prevent loops
3. **Exception Handling**: Exceptions in event processing need to be handled properly
4. **Thread Safety**: Additional synchronization mechanisms are required in multi-threaded environments
5. **Performance Considerations**: Large numbers of event listeners may affect performance 