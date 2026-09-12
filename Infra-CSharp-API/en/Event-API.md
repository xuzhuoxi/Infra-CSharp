# Event API Documentation

## Namespace: JLGames.Infra.Event

### Interfaces

#### IEventListener
Event listener registration and removal.

```csharp
/// <summary>
/// Event listener registration and removal.
/// </summary>
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
    /// <param name="listeningTimes">Max responses; 0 means unlimited.</param>
    /// <param name="tag">Function Tag</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, uint listeningTimes, string tag = null);

    /// <summary>
    /// Add event listener
    /// </summary>
    /// <param name="type">Event Type</param>
    /// <param name="handler">Listener Function</param>
    /// <param name="weight">Response Weight</param>
    /// <param name="listeningTimes">Max responses; 0 means unlimited.</param>
    /// <param name="tag">Function Tag</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, uint listeningTimes, string tag = null);

    /// <summary>
    /// Delete event listener
    /// </summary>
    /// <param name="type">Event type.</param>
    /// <param name="handler">Listener function.</param>
    /// <param name="tag">Optional tag to match; null removes the last matching handler only.</param>
    void RemoveEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// Delete event listener
    /// </summary>
    /// <param name="type">Event type.</param>
    /// <param name="tag">Tag of listeners to remove.</param>
    void RemoveEventListener(string type, string tag);

    /// <summary>
    /// Delete a type of event listeners
    /// </summary>
    /// <param name="type">Event type.</param>
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
Event dispatcher: register listeners and dispatch events by type. Inherits from `IEventListener`.

```csharp
/// <summary>
/// Event dispatcher: register listeners and dispatch events by type.
/// </summary>
public interface IEventDispatcher : IEventListener
{
    /// <summary>
    /// Trigger an event of a certain type and pass data
    /// </summary>
    /// <param name="evd">Event data.</param>
    void DispatchEvent(EventData evd);

    /// <summary>
    /// Trigger an event of a certain type and pass data
    /// </summary>
    /// <param name="type">Event type.</param>
    /// <param name="data">Event payload (may be null).</param>
    void DispatchEvent(string type, object data);
}
```

#### IThreadEventDispatcher
Event dispatcher that can marshal dispatch onto a synchronization context. Inherits from `IEventDispatcher`.

```csharp
/// <summary>
/// Event dispatcher that can marshal dispatch onto a synchronization context.
/// </summary>
public interface IThreadEventDispatcher : IEventDispatcher
{
    /// <summary>
    /// Whether no thread synchronization context is set.
    /// </summary>
    bool IsNullContext { get; }

    /// <summary>
    /// Set the synchronization context used when dispatching events.
    /// </summary>
    /// <param name="context">Synchronization context with queue/scheduling support.</param>
    void SetThreadEventContext(SynchronizationContext context);

    /// <summary>
    /// Clear the thread synchronization context (dispatch runs on caller thread).
    /// </summary>
    void ClearThreadEventContext();
}
```

### Classes

#### EventData
Event data.

```csharp
/// <summary>
/// Event data.
/// </summary>
public class EventData
{
    /// <summary>
    /// Create event data with type and payload.
    /// </summary>
    /// <param name="type">Event type.</param>
    /// <param name="data">Event payload (may be null).</param>
    public EventData(string type, object data);

    /// <summary>
    /// Create event data with type, payload, and originating dispatcher.
    /// </summary>
    /// <param name="type">Event type.</param>
    /// <param name="data">Event payload (may be null).</param>
    /// <param name="currentDispatcher">Dispatcher that dispatched this event.</param>
    public EventData(string type, object data, IEventDispatcher currentDispatcher);

    /// <summary>
    /// Event type
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Event data
    /// </summary>
    public object Data { get; }

    /// <summary>
    /// Dispatcher that dispatched this event; null if not set at construction.
    /// </summary>
    public IEventDispatcher CurrentDispatcher { get; }
}
```

#### EventDispatcher
Event dispatcher. Implements `IThreadEventDispatcher` (and therefore `IEventDispatcher` and `IEventListener`).

```csharp
/// <summary>
/// Event dispatcher
/// </summary>
public class EventDispatcher : IThreadEventDispatcher
{
    /// <summary>
    /// Create a dispatcher with the default name.
    /// </summary>
    public EventDispatcher();

    /// <summary>
    /// Create a dispatcher with the given name.
    /// </summary>
    /// <param name="name">Instance name.</param>
    public EventDispatcher(string name);

    /// <summary>
    /// Dispatcher instance name.
    /// </summary>
    public string DispatcherName { get; }

    /// <summary>
    /// Whether no thread synchronization context is set.
    /// </summary>
    public bool IsNullContext { get; }

    /// <summary>
    /// Set the synchronization context used when dispatching events.
    /// </summary>
    /// <param name="context">Synchronization context with queue/scheduling support.</param>
    public void SetThreadEventContext(SynchronizationContext context);

    /// <summary>
    /// Clear the thread synchronization context (dispatch runs on caller thread).
    /// </summary>
    public void ClearThreadEventContext();

    /// <summary>
    /// Add single event listener
    /// </summary>
    public void OnceEventListener(string type, EventDelegates.EventHandler handler, int weight = EventConst.DefaultWeight, string tag = null);

    /// <summary>
    /// Add event listener
    /// </summary>
    public void AddEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// Add event listener
    /// </summary>
    public void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, string tag = null);

    /// <summary>
    /// Add event listener
    /// </summary>
    public void AddEventListener(string type, EventDelegates.EventHandler handler, uint listeningTimes, string tag = null);

    /// <summary>
    /// Add event listener
    /// </summary>
    public void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, uint listeningTimes, string tag = null);

    /// <summary>
    /// Delete event listener
    /// </summary>
    public void RemoveEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// Delete event listener
    /// </summary>
    public void RemoveEventListener(string type, string tag);

    /// <summary>
    /// Delete a type of event listeners
    /// </summary>
    public void RemoveEventListener(string type);

    /// <summary>
    /// Clear all event listeners
    /// </summary>
    public void RemoveEventListener();

    /// <summary>
    /// Dispose
    /// </summary>
    public virtual void Dispose();

    /// <summary>
    /// Trigger an event of a certain type and pass data
    /// </summary>
    public virtual void DispatchEvent(EventData evd);

    /// <summary>
    /// Trigger an event of a certain type and pass data
    /// </summary>
    public virtual void DispatchEvent(string type, object data);
}
```

The parameterless constructor uses the default name `"Default"`. `DispatchEvent(string, object)` builds an `EventData` that references the current dispatcher; `DispatchEvent(EventData)` uses the supplied object as-is. Dispatch returns immediately when the type has no listeners. When a synchronization context is set, dispatch runs through `SynchronizationContext.Send`; otherwise it runs on the caller thread.

#### EventDispatcherPool
Named `IEventDispatcher` instance pool.

```csharp
/// <summary>
/// Named IEventDispatcher instance pool.
/// </summary>
public sealed class EventDispatcherPool
{
    /// <summary>
    /// Get event dispatcher instance.
    /// </summary>
    /// <param name="instanceName">Instance name.</param>
    /// <param name="createIfNotExist">Create a new dispatcher when missing.</param>
    /// <returns>Dispatcher instance, or null when missing and not created.</returns>
    public IEventDispatcher GetInstance(string instanceName, bool createIfNotExist);

    /// <summary>
    /// Remove a named dispatcher from the pool, optionally clearing its listeners first.
    /// </summary>
    /// <param name="instanceName">Instance name.</param>
    /// <param name="removeListener">Clear listeners before removal.</param>
    /// <returns>Removed dispatcher, or null if not found.</returns>
    public IEventDispatcher Clear(string instanceName, bool removeListener = true);

    /// <summary>
    /// Remove event listeners.
    /// </summary>
    public void ClearAll();
}
```

When `createIfNotExist` is `true` and the name is missing, `GetInstance` creates `new EventDispatcher(instanceName)` and stores it in the pool. `ClearAll` removes listeners on every pooled instance, then clears the pool.

#### EventGroup
Per-event-type handler list: registration, ordered dispatch, and limited invoke counts.

```csharp
/// <summary>
/// Per-event-type handler list: registration, ordered dispatch, and limited invoke counts.
/// </summary>
public sealed class EventGroup
{
    /// <summary>
    /// Trigger the listener event
    /// </summary>
    /// <param name="data">Event data to pass to handlers.</param>
    public void Handle(EventData data);

    /// <summary>
    /// Add event handler with custom weight and invoke limit.
    /// </summary>
    /// <param name="handler">Handler callback.</param>
    /// <param name="weight">Dispatch order weight (higher runs first).</param>
    /// <param name="handleTimes">Max invocations; 0 means unlimited.</param>
    /// <param name="tag">Optional tag for later removal.</param>
    public void AddEventHandler(EventDelegates.EventHandler handler, int weight, uint handleTimes, string tag);

    /// <summary>
    /// Delete listener function
    /// </summary>
    /// <param name="handler">Handler to remove.</param>
    /// <param name="tag">Optional tag filter.</param>
    public void RemoveEventHandler(EventDelegates.EventHandler handler, string tag);

    /// <summary>
    /// Delete listener function
    /// </summary>
    /// <param name="tag">Tag of handlers to remove.</param>
    public void RemoveEventHandler(string tag);
}
```

`Handle` snapshots the current handlers, decreases remaining counts (an item with count 1 still runs this time, then is removed), then invokes in descending weight order. `RemoveEventHandler(handler, tag)` with an empty tag removes only the last matching handler.

### Static Classes

#### EventManager
Event manager. Internally caches dispatchers by name in an `EventDispatcherPool`.

```csharp
/// <summary>
/// Event manager.
/// </summary>
public static class EventManager
{
    /// <summary>
    /// Default named event dispatcher instance.
    /// </summary>
    public static IEventDispatcher DefaultDispatcher { get; }

    /// <summary>
    /// Get or create a named event dispatcher instance.
    /// </summary>
    /// <param name="instanceName">Instance name.</param>
    /// <returns>Event dispatcher.</returns>
    public static IEventDispatcher GetInstance(string instanceName);

    /// <summary>
    /// Remove all listeners for the instance, then remove it from the pool.
    /// </summary>
    /// <param name="instanceName">Instance name.</param>
    /// <returns>Removed dispatcher, or null if not found.</returns>
    public static IEventDispatcher RemoveInstance(string instanceName);

    /// <summary>
    /// Clear all event listeners on the named instance without removing the instance.
    /// </summary>
    /// <param name="instanceName">Instance name.</param>
    public static void RemoveListeners(string instanceName);

    /// <summary>
    /// Clear all event listeners on every pooled dispatcher instance.
    /// </summary>
    public static void RemoveListeners();
}
```

`DefaultDispatcher` is the instance named `"Default"` and is created on first access if missing. `GetInstance` always creates a missing instance. `RemoveListeners()` clears listeners only; it does not remove instances from the pool.

#### EventDelegates
Event-related delegate types.

```csharp
/// <summary>
/// Event-related delegate types.
/// </summary>
public static class EventDelegates
{
    /// <summary>
    /// Event listener callback.
    /// </summary>
    /// <param name="evd">Event data passed to the listener.</param>
    public delegate void EventHandler(EventData evd);
}
```

#### EventConst
Event module constants.

```csharp
/// <summary>
/// Event module constants.
/// </summary>
public static class EventConst
{
    /// <summary>
    /// Default listener weight when not specified (higher runs first).
    /// </summary>
    public const int DefaultWeight = 50;
}
```

### Function Description

#### Event System Architecture

**IEventListener**
Event listener registration and removal:
- **OnceEventListener**: Add a single-shot listener (registered with `listeningTimes = 1`, removed after one trigger)
- **AddEventListener**: Add a listener; overloads support weight, response count, and tag
- **RemoveEventListener**: Remove listeners by handler, tag, event type, or clear all
- **Dispose**: Release resources

**IEventDispatcher**
Event dispatcher interface, inherits from `IEventListener`, adds triggering:
- **DispatchEvent**: Trigger events via an `EventData` object or type + payload

**IThreadEventDispatcher**
Marshals dispatch onto a synchronization context:
- **IsNullContext**: Whether no synchronization context is set
- **SetThreadEventContext**: Set the context used when dispatching
- **ClearThreadEventContext**: Clear the context; dispatch then runs on the caller thread

**EventData**
Event data encapsulation:
- **Type**: Event type identifier
- **Data**: Payload passed with the event
- **CurrentDispatcher**: Dispatcher that dispatched this event; `null` if not set at construction

**EventDispatcher**
Default dispatcher implementation of `IThreadEventDispatcher`. Maintains an `EventGroup` per event type. The parameterless constructor names the instance `"Default"`.

**EventDispatcherPool**
Manages dispatcher instances by name: get or create, remove by name (optionally clearing listeners first), and clear all.

**EventGroup**
Per-event-type handler list for registration, weight-ordered dispatch, and limited invoke counts. `EventDispatcher` uses this type internally.

**EventManager**
Global static entry point that holds an `EventDispatcherPool` and exposes the default instance plus named get / remove.

#### Event Listening Features

1. **Weight system**: Higher `weight` runs first; the default is `EventConst.DefaultWeight` (50)
2. **Tag management**: Identify listeners with `tag` for later removal
3. **Response count**: `listeningTimes` / `handleTimes` cap the number of invocations; `0` means unlimited
4. **Single listening**: `OnceEventListener` registers with a count of 1; the current dispatch still runs, then the listener is dropped
5. **Overload resolution**: Integer literal `3` is `int` and binds to the weight overload; pass `3u` or an explicit `uint` for a response-count limit

#### Thread-Synchronized Dispatch

`IThreadEventDispatcher` can marshal listener callbacks onto a chosen thread:
- With a context set, dispatch uses `SynchronizationContext.Send` (waits until the target thread finishes)
- With no context, dispatch runs on the caller thread
- The default .NET `SynchronizationContext` is an empty shell: it does not keep a queue and cannot switch or schedule threads. Pass a derived type that actually queues and schedules work.

### Usage Examples

#### Basic Event Listening and Dispatch
```csharp
// Create event dispatcher
var dispatcher = new EventDispatcher();

// Add event listener
dispatcher.AddEventListener("user.login", (evd) => {
    Console.WriteLine($"User login: {evd.Data}");
});

// Add event listener with weight and tag
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

// Remove by handler (null tag removes only the last match)
dispatcher.RemoveEventListener("system.shutdown", ShutdownHandler);

// Remove all listeners of a specific type
dispatcher.RemoveEventListener("system.shutdown");

// Clear all listeners
dispatcher.RemoveEventListener();

// Release resources
dispatcher.Dispose();
```

#### Global Event Manager
```csharp
// Default instance (name "Default")
EventManager.DefaultDispatcher.AddEventListener("app.ready", OnReady);
EventManager.DefaultDispatcher.DispatchEvent("app.ready", null);

// Get or create by name
var combat = EventManager.GetInstance("combat");
combat.DispatchEvent("skill.cast", skillId);

// Clear listeners on that instance; keep the instance in the pool
EventManager.RemoveListeners("combat");

// Clear listeners and remove the instance from the pool
EventManager.RemoveInstance("combat");

// Clear listeners on every pooled instance (does not remove instances)
EventManager.RemoveListeners();
```

#### Event Dispatcher Pool
```csharp
var pool = new EventDispatcherPool();

// Create if missing
var combat = pool.GetInstance("combat", createIfNotExist: true);

// Missing and not created: returns null
var missing = pool.GetInstance("unknown", createIfNotExist: false);

// Remove a named instance (clears listeners by default)
pool.Clear("combat");

// Remove listeners and clear the pool
pool.ClearAll();
```

#### Thread-Synchronized Dispatch
```csharp
var dispatcher = new EventDispatcher("ui");

// context must be a SynchronizationContext subclass with queue/scheduling support
dispatcher.SetThreadEventContext(context);
Console.WriteLine(dispatcher.IsNullContext); // false

dispatcher.AddEventListener("ui.refresh", OnRefresh);
dispatcher.DispatchEvent("ui.refresh", null); // dispatched via context.Send

dispatcher.ClearThreadEventContext();
dispatcher.DispatchEvent("ui.refresh", null); // runs on the caller thread
```

#### Weight and Response Count Control
```csharp
// High-priority listener (higher weight runs first)
dispatcher.AddEventListener("critical.event", CriticalHandler, 100);

// Normal-priority listener
dispatcher.AddEventListener("critical.event", NormalHandler, EventConst.DefaultWeight);

// Low-priority listener
dispatcher.AddEventListener("critical.event", LowPriorityHandler, 1);

// Limited response count (must use uint; an int literal binds to the weight overload)
dispatcher.AddEventListener("limited.event", LimitedHandler, 3u); // respond 3 times

// Weight plus response-count limit
dispatcher.AddEventListener("complex.event", ComplexHandler, 25, 5u); // weight 25, respond 5 times

// Unlimited
dispatcher.AddEventListener("forever.event", ForeverHandler, EventConst.DefaultWeight, 0u, "forever");
```

#### Using EventGroup Directly
```csharp
var group = new EventGroup();
group.AddEventHandler(OnTick, EventConst.DefaultWeight, 0, "tick");
group.Handle(new EventData("tick", null));
group.RemoveEventHandler("tick");
```

### Design Features

1. **Flexibility**: Registration supports weight, tags, response counts, and one-shot listeners
2. **Extensibility**: Layered `IEventListener` / `IEventDispatcher` / `IThreadEventDispatcher` interfaces
3. **Thread marshaling**: Dispatch can be synchronized onto a scheduling-capable `SynchronizationContext`
4. **Named instances**: `EventManager` and `EventDispatcherPool` reuse dispatchers by name
5. **Resource management**: Remove by type, tag, or handler, and `Dispose` to release

### Notes

1. **Memory management**: Remove unused listeners promptly to avoid leaks
2. **Event loop**: Avoid dispatching the same event from inside its handler in a way that loops
3. **Exception handling**: Exceptions in handlers need to be handled properly
4. **Thread safety**: Registration and dispatch have no extra locking; cross-thread dispatch requires a synchronization context that actually queues work, not the default empty-shell `SynchronizationContext`
5. **Weight direction**: Higher values run first; lower is not higher priority
6. **Count overload**: `AddEventListener(type, handler, 3)` binds to weight; pass a `uint` such as `3u` to limit response count
7. **Performance considerations**: Large numbers of listeners may affect performance
