# JLGames.Infra.Serial API Documentation

## Overview

The Serial module provides serialization manager functionality for starting and stopping multiple modules in sequence. This module includes serialization manager interfaces, module interfaces, status enumerations, and event definitions.

## Namespace

`JLGames.Infra.Serial`

---

## Interfaces

### ISerialManager

Serialization manager interface that defines basic operations for serialization managers.

```csharp
public interface ISerialManager
```

#### 方法

##### AppendModule(ISerialModule module)

```csharp
void AppendModule(ISerialModule module);
```

**Description:** Append module

**Parameters:**
- `module` (ISerialModule): Serial module to add

##### StartManager(Callback endCall = null)

```csharp
bool StartManager(Callback endCall = null);
```

**Description:** Manager start

**Parameters:**
- `endCall` (Callback, optional): Callback function after startup completion

**Returns:**
- `bool`: Whether startup was successful

##### StopManager(Callback endCall = null)

```csharp
bool StopManager(Callback endCall = null);
```

**Description:** Manager stop

**Parameters:**
- `endCall` (Callback, optional): Callback function after stop completion

**Returns:**
- `bool`: Whether stop was successful

---

### ISerialModule

Serialization module interface that inherits from IEventDispatcher and defines basic operations for serialization modules.

```csharp
public interface ISerialModule : IEventDispatcher
```

#### Methods

##### Startup()

```csharp
void Startup();
```

**Description:** Start

##### Shutdown()

```csharp
void Shutdown();
```

**Description:** Stop

---

## Classes

### SerialManager

Serialization manager implementation class that inherits from EventDispatcher and implements ISerialManager interface.

```csharp
public sealed class SerialManager : EventDispatcher, ISerialManager
```

#### Fields

- `m_Modules` (List<ISerialModule>): Module list
- `m_Status` (SerialStatus): Current status
- `m_Index` (int): Current processing module index
- `m_EndCall` (Callback): End callback

#### Methods

##### AppendModule(ISerialModule module)

```csharp
public void AppendModule(ISerialModule module)
```

**Description:** Add module to manager

**Parameters:**
- `module` (ISerialModule): Module to add

##### StartManager(Callback endCall = null)

```csharp
public bool StartManager(Callback endCall = null)
```

**Description:** Start manager, start all modules in sequence

**Parameters:**
- `endCall` (Callback, optional): Callback after startup completion

**Returns:**
- `bool`: Whether startup was successful

##### StopManager(Callback endCall = null)

```csharp
public bool StopManager(Callback endCall = null)
```

**Description:** Stop manager, stop all modules in reverse order

**Parameters:**
- `endCall` (Callback, optional): Callback after stop completion

**Returns:**
- `bool`: Whether stop was successful

##### StartModule()

```csharp
private void StartModule()
```

**Description:** Start the module at current index

##### OnModuleStartup(EventData evd)

```csharp
private void OnModuleStartup(EventData evd)
```

**Description:** Module startup completion event handling

**Parameters:**
- `evd` (EventData): Event data

##### StopModule()

```csharp
private void StopModule()
```

**Description:** Stop the module at current index

##### OnModuleShutdown(EventData evd)

```csharp
private void OnModuleShutdown(EventData evd)
```

**Description:** Module shutdown completion event handling

**Parameters:**
- `evd` (EventData): Event data

---

### SerialEvents

Serial event constant definition class.

```csharp
public static class SerialEvents
```

#### Constants

##### EventOnModuleStarted

```csharp
public const string EventOnModuleStarted = "SerialModule:EventOnObserverStarted";
```

**Description:** Serial module start finish event

##### EventOnModuleStopped

```csharp
public const string EventOnModuleStopped = "SerialModule:EventOnObserverStopped";
```

**Description:** Serial module stop finish event

##### EventOnManagerStarted

```csharp
public const string EventOnManagerStarted = "SerialManager:EventOnManagerStarted";
```

**Description:** Serial manager start finish event

##### EventOnManagerStopped

```csharp
public const string EventOnManagerStopped = "SerialManager:EventOnManagerStopped";
```

**Description:** Serial manager stop finish event

---

## Enums

### SerialStatus

Serial manager status enumeration.

```csharp
public enum SerialStatus
```

#### Enum Values

##### Stopped

```csharp
Stopped
```

**Description:** Stop completed

##### Starting

```csharp
Starting
```

**Description:** Starting in progress

##### Started

```csharp
Started
```

**Description:** Start completed

##### Stopping

```csharp
Stopping
```

**Description:** Stopping in progress

---

## Usage Examples

### Basic Usage

```csharp
// Create serial manager
var serialManager = new SerialManager();

// Add modules
serialManager.AppendModule(new MyModule1());
serialManager.AppendModule(new MyModule2());
serialManager.AppendModule(new MyModule3());

// Start manager
serialManager.StartManager(() => {
    Console.WriteLine("All modules started");
});

// Stop manager
serialManager.StopManager(() => {
    Console.WriteLine("All modules stopped");
});
```

### Custom Module Implementation

```csharp
public class MyModule : ISerialModule
{
    private EventDispatcher m_EventDispatcher = new EventDispatcher();

    public void Startup()
    {
        // Startup logic
        Console.WriteLine("Module starting...");
        
        // Send event after startup completion
        m_EventDispatcher.DispatchEvent(SerialEvents.EventOnModuleStarted, null);
    }

    public void Shutdown()
    {
        // Shutdown logic
        Console.WriteLine("Module stopping...");
        
        // Send event after shutdown completion
        m_EventDispatcher.DispatchEvent(SerialEvents.EventOnModuleStopped, null);
    }

    // IEventDispatcher interface implementation
    public void AddEventListener(string eventName, EventListener listener)
    {
        m_EventDispatcher.AddEventListener(eventName, listener);
    }

    public void RemoveEventListener(string eventName, EventListener listener)
    {
        m_EventDispatcher.RemoveEventListener(eventName, listener);
    }

    public void OnceEventListener(string eventName, EventListener listener)
    {
        m_EventDispatcher.OnceEventListener(eventName, listener);
    }

    public void DispatchEvent(string eventName, EventData eventData)
    {
        m_EventDispatcher.DispatchEvent(eventName, eventData);
    }
}
```

---

## Notes

1. **Serial Execution:** SerialManager starts modules serially in the order they were added, and stops them serially in reverse order
2. **Event-Driven:** Modules must notify startup/shutdown completion by sending appropriate events
3. **State Management:** Manager maintains current state to prevent duplicate startup or shutdown
4. **Error Handling:** If module startup or shutdown fails, the entire process will be interrupted
5. **Callback Support:** Supports executing callback functions after startup/shutdown completion

---

## Dependencies

- `JLGames.Infra.Event`: Depends on event system
- `System.Collections.Generic`: Uses List collection 