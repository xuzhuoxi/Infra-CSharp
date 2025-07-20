# JLGames.Infra.Serial API 文档

## 概述

Serial模块提供了串行化管理器功能，用于按顺序启动和停止多个模块。该模块包含串行管理器接口、模块接口、状态枚举和事件定义。

## 命名空间

`JLGames.Infra.Serial`

---

## 接口

### ISerialManager

串行管理器接口，定义了串行化管理器的基本操作。

```csharp
public interface ISerialManager
```

#### 方法

##### AppendModule(ISerialModule module)

```csharp
void AppendModule(ISerialModule module);
```

**描述：** Append module / 添加模块

**参数：**
- `module` (ISerialModule): 要添加的串行模块

##### StartManager(Callback endCall = null)

```csharp
bool StartManager(Callback endCall = null);
```

**描述：** Manager start / 管理器启动

**参数：**
- `endCall` (Callback, 可选): 启动完成后的回调函数

**返回值：**
- `bool`: 启动是否成功

##### StopManager(Callback endCall = null)

```csharp
bool StopManager(Callback endCall = null);
```

**描述：** Manager stop / 管理器停止

**参数：**
- `endCall` (Callback, 可选): 停止完成后的回调函数

**返回值：**
- `bool`: 停止是否成功

---

### ISerialModule

串行模块接口，继承自IEventDispatcher，定义了串行模块的基本操作。

```csharp
public interface ISerialModule : IEventDispatcher
```

#### 方法

##### Startup()

```csharp
void Startup();
```

**描述：** Start / 启动

##### Shutdown()

```csharp
void Shutdown();
```

**描述：** Stop / 停止

---

## 类

### SerialManager

串行管理器实现类，继承自EventDispatcher并实现ISerialManager接口。

```csharp
public sealed class SerialManager : EventDispatcher, ISerialManager
```

#### 字段

- `m_Modules` (List<ISerialModule>): 模块列表
- `m_Status` (SerialStatus): 当前状态
- `m_Index` (int): 当前处理的模块索引
- `m_EndCall` (Callback): 结束回调

#### 方法

##### AppendModule(ISerialModule module)

```csharp
public void AppendModule(ISerialModule module)
```

**描述：** 添加模块到管理器

**参数：**
- `module` (ISerialModule): 要添加的模块

##### StartManager(Callback endCall = null)

```csharp
public bool StartManager(Callback endCall = null)
```

**描述：** 启动管理器，按顺序启动所有模块

**参数：**
- `endCall` (Callback, 可选): 启动完成后的回调

**返回值：**
- `bool`: 启动是否成功

##### StopManager(Callback endCall = null)

```csharp
public bool StopManager(Callback endCall = null)
```

**描述：** 停止管理器，按逆序停止所有模块

**参数：**
- `endCall` (Callback, 可选): 停止完成后的回调

**返回值：**
- `bool`: 停止是否成功

##### StartModule()

```csharp
private void StartModule()
```

**描述：** 启动当前索引的模块

##### OnModuleStartup(EventData evd)

```csharp
private void OnModuleStartup(EventData evd)
```

**描述：** 模块启动完成事件处理

**参数：**
- `evd` (EventData): 事件数据

##### StopModule()

```csharp
private void StopModule()
```

**描述：** 停止当前索引的模块

##### OnModuleShutdown(EventData evd)

```csharp
private void OnModuleShutdown(EventData evd)
```

**描述：** 模块停止完成事件处理

**参数：**
- `evd` (EventData): 事件数据

---

### SerialEvents

串行事件常量定义类。

```csharp
public static class SerialEvents
```

#### 常量

##### EventOnModuleStarted

```csharp
public const string EventOnModuleStarted = "SerialModule:EventOnObserverStarted";
```

**描述：** Serial module start finish event / 串行模块启动完成事件

##### EventOnModuleStopped

```csharp
public const string EventOnModuleStopped = "SerialModule:EventOnObserverStopped";
```

**描述：** Serial module stop finish event / 串行模块停止完成事件

##### EventOnManagerStarted

```csharp
public const string EventOnManagerStarted = "SerialManager:EventOnManagerStarted";
```

**描述：** Serial manger start finish event / 串行管理器启动完成事件

##### EventOnManagerStopped

```csharp
public const string EventOnManagerStopped = "SerialManager:EventOnManagerStopped";
```

**描述：** Serial manger stop finish event / 串行管理器停止完成事件

---

## 枚举

### SerialStatus

串行管理器状态枚举。

```csharp
public enum SerialStatus
```

#### 枚举值

##### Stopped

```csharp
Stopped
```

**描述：** 停止完成

##### Starting

```csharp
Starting
```

**描述：** 启动进行中

##### Started

```csharp
Started
```

**描述：** 启动完成

##### Stopping

```csharp
Stopping
```

**描述：** 停止进行中

---

## 使用示例

### 基本用法

```csharp
// 创建串行管理器
var serialManager = new SerialManager();

// 添加模块
serialManager.AppendModule(new MyModule1());
serialManager.AppendModule(new MyModule2());
serialManager.AppendModule(new MyModule3());

// 启动管理器
serialManager.StartManager(() => {
    Console.WriteLine("所有模块启动完成");
});

// 停止管理器
serialManager.StopManager(() => {
    Console.WriteLine("所有模块停止完成");
});
```

### 自定义模块实现

```csharp
public class MyModule : ISerialModule
{
    private EventDispatcher m_EventDispatcher = new EventDispatcher();

    public void Startup()
    {
        // 启动逻辑
        Console.WriteLine("模块启动中...");
        
        // 启动完成后发送事件
        m_EventDispatcher.DispatchEvent(SerialEvents.EventOnModuleStarted, null);
    }

    public void Shutdown()
    {
        // 停止逻辑
        Console.WriteLine("模块停止中...");
        
        // 停止完成后发送事件
        m_EventDispatcher.DispatchEvent(SerialEvents.EventOnModuleStopped, null);
    }

    // IEventDispatcher 接口实现
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

## 注意事项

1. **串行执行：** SerialManager会按照添加顺序串行启动模块，按照逆序串行停止模块
2. **事件驱动：** 模块必须通过发送相应事件来通知启动/停止完成
3. **状态管理：** 管理器会维护当前状态，防止重复启动或停止
4. **错误处理：** 如果模块启动或停止失败，整个流程会中断
5. **回调支持：** 支持在启动/停止完成后执行回调函数

---

## 依赖关系

- `JLGames.Infra.Event`: 依赖事件系统
- `System.Collections.Generic`: 使用List集合 