# JLGames.Infra.Serial API Documentation

## Overview

The Serial module provides a serial module manager that starts registered modules in registration order and stops them in reverse order. Modules dispatch completion events through `IEventDispatcher`, and the manager uses those events to advance to the next module. This module includes the manager and module contracts, a status enum, and lifecycle event constants.

## Namespace

`JLGames.Infra.Serial`

---

## Interfaces

### ISerialManager

Serial module manager contract; registers modules and starts/stops them in registration order (reverse on stop).

```csharp
public interface ISerialManager
```

#### Methods

##### AppendModule(ISerialModule module)

```csharp
void AppendModule(ISerialModule module);
```

**Description:** Append a module to the end of the serial chain.

**Parameters:**
- `module` (ISerialModule): Module to register; ignored if null.

##### StartManager(Callback endCall = null)

```csharp
bool StartManager(Callback endCall = null);
```

**Description:** Start all registered modules in order; invokes `endCall` when all have started.

**Parameters:**
- `endCall` (`Callback`, optional): Optional callback after the manager and all modules have started. `Callback` is defined in `JLGames.Infra`.

**Returns:**
- `bool`: `true` if started from `SerialStatus.Stopped`; otherwise `false`.

##### StopManager(Callback endCall = null)

```csharp
bool StopManager(Callback endCall = null);
```

**Description:** Stop all registered modules in reverse order; invokes `endCall` when all have stopped.

**Parameters:**
- `endCall` (`Callback`, optional): Optional callback after the manager and all modules have stopped. `Callback` is defined in `JLGames.Infra`.

**Returns:**
- `bool`: `true` if stopped from `SerialStatus.Started`; otherwise `false`.

---

### ISerialModule

Serial lifecycle module contract; supports startup/shutdown and dispatches completion events via `IEventDispatcher`. Inherits `IEventDispatcher` (see the Event API).

After `Startup` or `Shutdown` completes, dispatch `SerialEvents.EventOnModuleStarted` or `SerialEvents.EventOnModuleStopped` respectively so `SerialManager` can proceed to the next module.

```csharp
public interface ISerialModule : IEventDispatcher
```

#### Methods

##### Startup()

```csharp
void Startup();
```

**Description:** Start the module asynchronously; dispatch `SerialEvents.EventOnModuleStarted` when finished.

##### Shutdown()

```csharp
void Shutdown();
```

**Description:** Stop the module asynchronously; dispatch `SerialEvents.EventOnModuleStopped` when finished.

---

## Classes

### SerialManager

Default `ISerialManager` implementation; drives modules sequentially and dispatches manager lifecycle events. Inherits `EventDispatcher` and implements `ISerialManager`.

```csharp
public sealed class SerialManager : EventDispatcher, ISerialManager
```

Public members match `ISerialManager`. The manager dispatches `SerialEvents.EventOnManagerStarted` / `EventOnManagerStopped` through the inherited `EventDispatcher`. Callers can listen with `AddEventListener` and the rest of the Event API.

#### Methods

##### AppendModule(ISerialModule module)

```csharp
public void AppendModule(ISerialModule module)
```

**Description:** Append a module to the end of the serial chain. Ignored if `module` is null.

**Parameters:**
- `module` (ISerialModule): Module to register; ignored if null.

##### StartManager(Callback endCall = null)

```csharp
public bool StartManager(Callback endCall = null)
```

**Description:** Start all registered modules in order; invokes `endCall` when all have started, then dispatches `SerialEvents.EventOnManagerStarted`. Startup is allowed only when the current status is `SerialStatus.Stopped`.

**Parameters:**
- `endCall` (`Callback`, optional): Optional callback after the manager and all modules have started. Completion calls `Callback.Invoke()`.

**Returns:**
- `bool`: `true` if started from `SerialStatus.Stopped`; otherwise `false`.

##### StopManager(Callback endCall = null)

```csharp
public bool StopManager(Callback endCall = null)
```

**Description:** Stop all registered modules in reverse order; invokes `endCall` when all have stopped, then dispatches `SerialEvents.EventOnManagerStopped`. Shutdown is allowed only when the current status is `SerialStatus.Started`.

**Parameters:**
- `endCall` (`Callback`, optional): Optional callback after the manager and all modules have stopped. Completion calls `Callback.Invoke()`.

**Returns:**
- `bool`: `true` if stopped from `SerialStatus.Started`; otherwise `false`.

---

### SerialEvents

Event type constants for serial module and manager lifecycle.

```csharp
public static class SerialEvents
```

#### Constants

##### EventOnModuleStarted

```csharp
public const string EventOnModuleStarted = "SerialModule:EventOnObserverStarted";
```

**Description:** Dispatched by a module when `ISerialModule.Startup` completes.

##### EventOnModuleStopped

```csharp
public const string EventOnModuleStopped = "SerialModule:EventOnObserverStopped";
```

**Description:** Dispatched by a module when `ISerialModule.Shutdown` completes.

##### EventOnManagerStarted

```csharp
public const string EventOnManagerStarted = "SerialManager:EventOnManagerStarted";
```

**Description:** Dispatched by `SerialManager` when all modules have started.

##### EventOnManagerStopped

```csharp
public const string EventOnManagerStopped = "SerialManager:EventOnManagerStopped";
```

**Description:** Dispatched by `SerialManager` when all modules have stopped.

---

## Enums

### SerialStatus

Lifecycle state of `SerialManager`.

```csharp
public enum SerialStatus
```

#### Enum Values

##### Stopped

```csharp
Stopped
```

**Description:** All modules stopped; ready to start.

##### Starting

```csharp
Starting
```

**Description:** Startup in progress (modules starting sequentially).

##### Started

```csharp
Started
```

**Description:** All modules started; ready to stop.

##### Stopping

```csharp
Stopping
```

**Description:** Shutdown in progress (modules stopping in reverse order).

---

## Usage Examples

### Basic Usage

```csharp
using JLGames.Infra;
using JLGames.Infra.Event;
using JLGames.Infra.Serial;

var serialManager = new SerialManager();

serialManager.AddEventListener(SerialEvents.EventOnManagerStarted, evd =>
{
    Console.WriteLine("Manager started event");
});
serialManager.AddEventListener(SerialEvents.EventOnManagerStopped, evd =>
{
    Console.WriteLine("Manager stopped event");
});

serialManager.AppendModule(new MyModule("Module1"));
serialManager.AppendModule(new MyModule("Module2"));
serialManager.AppendModule(new MyModule("Module3"));

bool started = serialManager.StartManager(new Callback(args =>
{
    Console.WriteLine("All modules started");
}));

bool stopped = serialManager.StopManager(new Callback(args =>
{
    Console.WriteLine("All modules stopped");
}));
```

`StartManager` returns `true` only when the status is `Stopped`; `StopManager` returns `true` only when the status is `Started`. Calling `StopManager` before startup has finished returns `false`.

### Custom Module Implementation

A module must implement `ISerialModule`. Inheriting `EventDispatcher` is the recommended way to reuse event dispatch:

```csharp
using JLGames.Infra.Event;
using JLGames.Infra.Serial;

public class MyModule : EventDispatcher, ISerialModule
{
    private readonly string m_Name;

    public MyModule(string name)
    {
        m_Name = name;
    }

    public void Startup()
    {
        Console.WriteLine($"{m_Name} starting...");
        DispatchEvent(SerialEvents.EventOnModuleStarted, null);
    }

    public void Shutdown()
    {
        Console.WriteLine($"{m_Name} stopping...");
        DispatchEvent(SerialEvents.EventOnModuleStopped, null);
    }
}
```

If startup or shutdown is asynchronous, dispatch the matching event only after the work actually finishes, not when the method returns. The manager waits for that event via `OnceEventListener` before moving to the next module.

---

## Notes

1. **Serial execution:** `SerialManager` starts modules in append order and stops them in reverse order. With no registered modules, start/stop completes immediately and still invokes the callback and manager events.
2. **Event-driven:** Modules must dispatch `EventOnModuleStarted` / `EventOnModuleStopped` after `Startup` / `Shutdown` complete; otherwise the serial chain stalls on the current module.
3. **State constraints:** Start is allowed only from `Stopped`, stop only from `Started`; other states return `false`.
4. **Null modules:** `AppendModule(null)` is ignored.
5. **Callbacks:** Completion calls `Callback.Invoke()` (using arguments bound at construction). `endCall` may be null.
6. **Registration timing:** Call `AppendModule` before `StartManager`.

---

## Dependencies

- `JLGames.Infra`: uses `Callback`
- `JLGames.Infra.Event`: depends on the event system (`IEventDispatcher`, `EventDispatcher`)
