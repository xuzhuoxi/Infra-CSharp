# JLGames.Infra.Serial API 文档

## 概述

Serial 模块提供串行模块管理器：按注册顺序依次启动多个模块，停止时按逆序处理。模块通过 `IEventDispatcher` 派发完成事件，管理器据此推进到下一个模块。该模块包含串行管理器接口、模块接口、状态枚举和生命周期事件常量。

## 命名空间

`JLGames.Infra.Serial`

---

## 接口

### ISerialManager

串行模块管理器契约；注册模块并按注册顺序依次启动（停止时逆序）。

```csharp
public interface ISerialManager
```

#### 方法

##### AppendModule(ISerialModule module)

```csharp
void AppendModule(ISerialModule module);
```

**描述：** 将模块追加到串行链末尾。

**参数：**
- `module` (ISerialModule): 待注册的模块；为 null 时忽略。

##### StartManager(Callback endCall = null)

```csharp
bool StartManager(Callback endCall = null);
```

**描述：** 按顺序启动所有已注册模块；全部启动完成后调用 `endCall`。

**参数：**
- `endCall` (`Callback`，可选): 管理器及全部模块启动完成后的可选回调。`Callback` 定义于 `JLGames.Infra`。

**返回值：**
- `bool`: 从 `SerialStatus.Stopped` 状态成功发起启动时返回 `true`，否则返回 `false`。

##### StopManager(Callback endCall = null)

```csharp
bool StopManager(Callback endCall = null);
```

**描述：** 按逆序停止所有已注册模块；全部停止完成后调用 `endCall`。

**参数：**
- `endCall` (`Callback`，可选): 管理器及全部模块停止完成后的可选回调。`Callback` 定义于 `JLGames.Infra`。

**返回值：**
- `bool`: 从 `SerialStatus.Started` 状态成功发起停止时返回 `true`，否则返回 `false`。

---

### ISerialModule

串行生命周期模块契约；支持启动/停止，并通过 `IEventDispatcher` 派发完成事件。继承自 `IEventDispatcher`（见 Event API）。

`Startup` 或 `Shutdown` 完成后，应分别派发 `SerialEvents.EventOnModuleStarted` 或 `SerialEvents.EventOnModuleStopped`，以便 `SerialManager` 继续处理下一个模块。

```csharp
public interface ISerialModule : IEventDispatcher
```

#### 方法

##### Startup()

```csharp
void Startup();
```

**描述：** 启动模块（可异步）；完成后派发 `SerialEvents.EventOnModuleStarted`。

##### Shutdown()

```csharp
void Shutdown();
```

**描述：** 停止模块（可异步）；完成后派发 `SerialEvents.EventOnModuleStopped`。

---

## 类

### SerialManager

默认 `ISerialManager` 实现；按序驱动各模块并派发管理器生命周期事件。继承自 `EventDispatcher` 并实现 `ISerialManager`。

```csharp
public sealed class SerialManager : EventDispatcher, ISerialManager
```

公开成员与 `ISerialManager` 一致。管理器通过继承的 `EventDispatcher` 派发 `SerialEvents.EventOnManagerStarted` / `EventOnManagerStopped`，调用方可使用 `AddEventListener` 等事件 API 监听。

#### 方法

##### AppendModule(ISerialModule module)

```csharp
public void AppendModule(ISerialModule module)
```

**描述：** 将模块追加到串行链末尾。`module` 为 null 时忽略。

**参数：**
- `module` (ISerialModule): 待注册的模块；为 null 时忽略。

##### StartManager(Callback endCall = null)

```csharp
public bool StartManager(Callback endCall = null)
```

**描述：** 按顺序启动所有已注册模块；全部启动完成后调用 `endCall`，并派发 `SerialEvents.EventOnManagerStarted`。仅当当前状态为 `SerialStatus.Stopped` 时可以启动。

**参数：**
- `endCall` (`Callback`，可选): 管理器及全部模块启动完成后的可选回调。完成时调用 `Callback.Invoke()`。

**返回值：**
- `bool`: 从 `SerialStatus.Stopped` 状态成功发起启动时返回 `true`，否则返回 `false`。

##### StopManager(Callback endCall = null)

```csharp
public bool StopManager(Callback endCall = null)
```

**描述：** 按逆序停止所有已注册模块；全部停止完成后调用 `endCall`，并派发 `SerialEvents.EventOnManagerStopped`。仅当当前状态为 `SerialStatus.Started` 时可以停止。

**参数：**
- `endCall` (`Callback`，可选): 管理器及全部模块停止完成后的可选回调。完成时调用 `Callback.Invoke()`。

**返回值：**
- `bool`: 从 `SerialStatus.Started` 状态成功发起停止时返回 `true`，否则返回 `false`。

---

### SerialEvents

串行模块与管理器生命周期相关的事件类型常量。

```csharp
public static class SerialEvents
```

#### 常量

##### EventOnModuleStarted

```csharp
public const string EventOnModuleStarted = "SerialModule:EventOnObserverStarted";
```

**描述：** 模块在 `ISerialModule.Startup` 完成后派发。

##### EventOnModuleStopped

```csharp
public const string EventOnModuleStopped = "SerialModule:EventOnObserverStopped";
```

**描述：** 模块在 `ISerialModule.Shutdown` 完成后派发。

##### EventOnManagerStarted

```csharp
public const string EventOnManagerStarted = "SerialManager:EventOnManagerStarted";
```

**描述：** `SerialManager` 在所有模块启动完成后派发。

##### EventOnManagerStopped

```csharp
public const string EventOnManagerStopped = "SerialManager:EventOnManagerStopped";
```

**描述：** `SerialManager` 在所有模块停止完成后派发。

---

## 枚举

### SerialStatus

`SerialManager` 的生命周期状态。

```csharp
public enum SerialStatus
```

#### 枚举值

##### Stopped

```csharp
Stopped
```

**描述：** 全部模块已停止；可发起启动。

##### Starting

```csharp
Starting
```

**描述：** 启动进行中（模块按序启动）。

##### Started

```csharp
Started
```

**描述：** 全部模块已启动；可发起停止。

##### Stopping

```csharp
Stopping
```

**描述：** 停止进行中（模块按逆序停止）。

---

## 使用示例

### 基本用法

```csharp
using JLGames.Infra;
using JLGames.Infra.Event;
using JLGames.Infra.Serial;

var serialManager = new SerialManager();

serialManager.AddEventListener(SerialEvents.EventOnManagerStarted, evd =>
{
    Console.WriteLine("管理器启动完成事件");
});
serialManager.AddEventListener(SerialEvents.EventOnManagerStopped, evd =>
{
    Console.WriteLine("管理器停止完成事件");
});

serialManager.AppendModule(new MyModule("模块1"));
serialManager.AppendModule(new MyModule("模块2"));
serialManager.AppendModule(new MyModule("模块3"));

bool started = serialManager.StartManager(new Callback(args =>
{
    Console.WriteLine("所有模块启动完成");
}));

bool stopped = serialManager.StopManager(new Callback(args =>
{
    Console.WriteLine("所有模块停止完成");
}));
```

`StartManager` 仅在状态为 `Stopped` 时返回 `true`；`StopManager` 仅在状态为 `Started` 时返回 `true`。若在启动尚未完成时调用 `StopManager`，将返回 `false`。

### 自定义模块实现

模块需实现 `ISerialModule`。推荐继承 `EventDispatcher`，以便复用事件派发能力：

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
        Console.WriteLine($"{m_Name} 启动中...");
        DispatchEvent(SerialEvents.EventOnModuleStarted, null);
    }

    public void Shutdown()
    {
        Console.WriteLine($"{m_Name} 停止中...");
        DispatchEvent(SerialEvents.EventOnModuleStopped, null);
    }
}
```

若启动/停止是异步的，应在真正完成后再派发对应事件，而不是在方法返回时立即派发。管理器通过 `OnceEventListener` 等待上述事件后才会处理下一个模块。

---

## 注意事项

1. **串行执行：** `SerialManager` 按添加顺序依次启动模块，按逆序依次停止模块。未注册模块（空列表）时，启动/停止会立即完成并触发回调与管理器事件。
2. **事件驱动：** 模块必须在 `Startup` / `Shutdown` 完成后分别派发 `EventOnModuleStarted` / `EventOnModuleStopped`，否则串行链会停在当前模块。
3. **状态约束：** 仅能从 `Stopped` 发起启动、从 `Started` 发起停止；其它状态调用对应方法会返回 `false`。
4. **空模块：** `AppendModule(null)` 会被忽略。
5. **回调：** 完成时调用 `Callback.Invoke()`（使用构造时绑定的参数）。`endCall` 可为 null。
6. **注册时机：** 应在调用 `StartManager` 之前完成 `AppendModule`。

---

## 依赖关系

- `JLGames.Infra`: 使用 `Callback`
- `JLGames.Infra.Event`: 依赖事件系统（`IEventDispatcher`、`EventDispatcher`）
