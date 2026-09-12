# Event API 文档

## 命名空间: JLGames.Infra.Event

### 接口 (Interfaces)

#### IEventListener
事件监听的注册与移除。

```csharp
/// <summary>
/// 事件监听的注册与移除。
/// </summary>
public interface IEventListener
{
    /// <summary>
    /// 添加单次事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="handler">监听函数</param>
    /// <param name="weight">响应权重</param>
    /// <param name="tag">函数的唯一标签</param>
    void OnceEventListener(string type, EventDelegates.EventHandler handler, int weight = EventConst.DefaultWeight, string tag = null);

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="handler">监听函数</param>
    /// <param name="tag">函数的唯一标签</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="handler">监听函数</param>
    /// <param name="weight">响应权重</param>
    /// <param name="tag">函数的唯一标签</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, string tag = null);

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="handler">监听函数</param>
    /// <param name="listeningTimes">最大响应次数；0 表示不限次数。</param>
    /// <param name="tag">函数的唯一标签</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, uint listeningTimes, string tag = null);

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="handler">监听函数</param>
    /// <param name="weight">响应权重</param>
    /// <param name="listeningTimes">最大响应次数；0 表示不限次数。</param>
    /// <param name="tag">函数的唯一标签</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, uint listeningTimes, string tag = null);

    /// <summary>
    /// 删除事件监听
    /// </summary>
    /// <param name="type">事件类型。</param>
    /// <param name="handler">监听函数。</param>
    /// <param name="tag">可选标签；为 null 时仅移除最后一个匹配的处理器。</param>
    void RemoveEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// 删除事件监听
    /// </summary>
    /// <param name="type">事件类型。</param>
    /// <param name="tag">要移除的监听标签。</param>
    void RemoveEventListener(string type, string tag);

    /// <summary>
    /// 删除一类事件监听
    /// </summary>
    /// <param name="type">事件类型。</param>
    void RemoveEventListener(string type);

    /// <summary>
    /// 清除全部事件监听
    /// </summary>
    void RemoveEventListener();

    /// <summary>
    /// 释放
    /// </summary>
    void Dispose();
}
```

#### IEventDispatcher
事件调度器：按类型注册监听并派发事件。继承自 `IEventListener`。

```csharp
/// <summary>
/// 事件调度器：按类型注册监听并派发事件。
/// </summary>
public interface IEventDispatcher : IEventListener
{
    /// <summary>
    /// 触发某一类型的事件,并传递数据
    /// </summary>
    /// <param name="evd">事件数据。</param>
    void DispatchEvent(EventData evd);

    /// <summary>
    /// 触发某一类型的事件,并传递数据
    /// </summary>
    /// <param name="type">事件类型。</param>
    /// <param name="data">事件数据（可为 null）。</param>
    void DispatchEvent(string type, object data);
}
```

#### IThreadEventDispatcher
可将事件派发到指定同步上下文的事件调度器。继承自 `IEventDispatcher`。

```csharp
/// <summary>
/// 可将事件派发到指定同步上下文的事件调度器。
/// </summary>
public interface IThreadEventDispatcher : IEventDispatcher
{
    /// <summary>
    /// 是否未设置线程同步上下文。
    /// </summary>
    bool IsNullContext { get; }

    /// <summary>
    /// 设置派发事件时使用的同步上下文。
    /// </summary>
    /// <param name="context">具备队列或调度能力的同步上下文。</param>
    void SetThreadEventContext(SynchronizationContext context);

    /// <summary>
    /// 清除线程同步上下文（随后在调用线程上直接派发）。
    /// </summary>
    void ClearThreadEventContext();
}
```

### 类 (Classes)

#### EventData
事件数据。

```csharp
/// <summary>
/// 事件数据
/// </summary>
public class EventData
{
    /// <summary>
    /// 使用事件类型与载荷创建事件数据。
    /// </summary>
    /// <param name="type">事件类型。</param>
    /// <param name="data">事件载荷（可为 null）。</param>
    public EventData(string type, object data);

    /// <summary>
    /// 使用事件类型、载荷及派发来源调度器创建事件数据。
    /// </summary>
    /// <param name="type">事件类型。</param>
    /// <param name="data">事件载荷（可为 null）。</param>
    /// <param name="currentDispatcher">派发该事件的调度器。</param>
    public EventData(string type, object data, IEventDispatcher currentDispatcher);

    /// <summary>
    /// 事件类型
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// 事件传递的数据
    /// </summary>
    public object Data { get; }

    /// <summary>
    /// 派发该事件的调度器；构造时未传入则为 null。
    /// </summary>
    public IEventDispatcher CurrentDispatcher { get; }
}
```

#### EventDispatcher
事件调度器。实现 `IThreadEventDispatcher`（因而也实现 `IEventDispatcher` 与 `IEventListener`）。

```csharp
/// <summary>
/// 事件调度器
/// </summary>
public class EventDispatcher : IThreadEventDispatcher
{
    /// <summary>
    /// 使用默认名称创建调度器。
    /// </summary>
    public EventDispatcher();

    /// <summary>
    /// 使用指定名称创建调度器。
    /// </summary>
    /// <param name="name">实例名称。</param>
    public EventDispatcher(string name);

    /// <summary>
    /// 调度器实例名称。
    /// </summary>
    public string DispatcherName { get; }

    /// <summary>
    /// 是否未设置线程同步上下文。
    /// </summary>
    public bool IsNullContext { get; }

    /// <summary>
    /// 设置派发事件时使用的同步上下文。
    /// </summary>
    /// <param name="context">具备队列或调度能力的同步上下文。</param>
    public void SetThreadEventContext(SynchronizationContext context);

    /// <summary>
    /// 清除线程同步上下文（随后在调用线程上直接派发）。
    /// </summary>
    public void ClearThreadEventContext();

    /// <summary>
    /// 添加单次事件监听
    /// </summary>
    public void OnceEventListener(string type, EventDelegates.EventHandler handler, int weight = EventConst.DefaultWeight, string tag = null);

    /// <summary>
    /// 添加事件监听
    /// </summary>
    public void AddEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// 添加事件监听
    /// </summary>
    public void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, string tag = null);

    /// <summary>
    /// 添加事件监听
    /// </summary>
    public void AddEventListener(string type, EventDelegates.EventHandler handler, uint listeningTimes, string tag = null);

    /// <summary>
    /// 添加事件监听
    /// </summary>
    public void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, uint listeningTimes, string tag = null);

    /// <summary>
    /// 删除事件监听
    /// </summary>
    public void RemoveEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// 删除事件监听
    /// </summary>
    public void RemoveEventListener(string type, string tag);

    /// <summary>
    /// 删除一类事件监听
    /// </summary>
    public void RemoveEventListener(string type);

    /// <summary>
    /// 清除全部事件监听
    /// </summary>
    public void RemoveEventListener();

    /// <summary>
    /// 释放
    /// </summary>
    public virtual void Dispose();

    /// <summary>
    /// 触发某一类型的事件,并传递数据
    /// </summary>
    public virtual void DispatchEvent(EventData evd);

    /// <summary>
    /// 触发某一类型的事件,并传递数据
    /// </summary>
    public virtual void DispatchEvent(string type, object data);
}
```

无参构造使用默认名称 `"Default"`。`DispatchEvent(string, object)` 会构造带当前调度器引用的 `EventData`；`DispatchEvent(EventData)` 直接使用传入对象。若该类型尚无监听，派发立即返回。已设置同步上下文时，通过 `SynchronizationContext.Send` 同步派发；未设置时在调用线程上直接派发。

#### EventDispatcherPool
按名称管理的事件调度器实例池。

```csharp
/// <summary>
/// 按名称管理的事件调度器实例池。
/// </summary>
public sealed class EventDispatcherPool
{
    /// <summary>
    /// 取事件调度实例
    /// </summary>
    /// <param name="instanceName">实例名称。</param>
    /// <param name="createIfNotExist">不存在时是否创建新实例。</param>
    /// <returns>调度器实例；未创建且不存在时返回 null。</returns>
    public IEventDispatcher GetInstance(string instanceName, bool createIfNotExist);

    /// <summary>
    /// 从池中移除指定名称的调度器，可选先清除其全部监听。
    /// </summary>
    /// <param name="instanceName">实例名称。</param>
    /// <param name="removeListener">移除前是否清除监听。</param>
    /// <returns>被移除的调度器；不存在时返回 null。</returns>
    public IEventDispatcher Clear(string instanceName, bool removeListener = true);

    /// <summary>
    /// 移除事件监听并清理全部事件调度实例
    /// </summary>
    public void ClearAll();
}
```

`GetInstance` 在 `createIfNotExist` 为 `true` 且名称不存在时，会创建 `new EventDispatcher(instanceName)` 并放入池中。

#### EventGroup
单一事件类型的处理器列表：注册、按权重派发及限定调用次数。

```csharp
/// <summary>
/// 单一事件类型的处理器列表：注册、按权重派发及限定调用次数。
/// </summary>
public sealed class EventGroup
{
    /// <summary>
    /// 触发监听事件
    /// </summary>
    /// <param name="data">传递给处理器的事件数据。</param>
    public void Handle(EventData data);

    /// <summary>
    /// 添加事件处理函数，使用自定义权重与响应次数。
    /// </summary>
    /// <param name="handler">处理回调。</param>
    /// <param name="weight">派发权重（越大越先执行）。</param>
    /// <param name="handleTimes">最大调用次数；0 表示不限次数。</param>
    /// <param name="tag">可选标签，便于后续按标签移除。</param>
    public void AddEventHandler(EventDelegates.EventHandler handler, int weight, uint handleTimes, string tag);

    /// <summary>
    /// 删除监听函数
    /// </summary>
    /// <param name="handler">要移除的处理器。</param>
    /// <param name="tag">可选标签过滤。</param>
    public void RemoveEventHandler(EventDelegates.EventHandler handler, string tag);

    /// <summary>
    /// 删除监听函数
    /// </summary>
    /// <param name="tag">要移除的处理器标签。</param>
    public void RemoveEventHandler(string tag);
}
```

`Handle` 先快照当前处理器，再递减剩余次数（次数为 1 的项在本次仍会执行，之后从列表移除），然后按权重降序调用。`RemoveEventHandler(handler, tag)` 在 `tag` 为空时仅移除最后一个匹配的处理器。

### 静态类 (Static Classes)

#### EventManager
事件管理器。内部使用一个 `EventDispatcherPool` 按名称缓存调度器。

```csharp
/// <summary>
/// 事件管理器
/// </summary>
public static class EventManager
{
    /// <summary>
    /// 默认名称的事件调度器实例。
    /// </summary>
    public static IEventDispatcher DefaultDispatcher { get; }

    /// <summary>
    /// 获取或创建指定名称的事件调度器实例。
    /// </summary>
    /// <param name="instanceName">实例名称。</param>
    /// <returns>事件调度器。</returns>
    public static IEventDispatcher GetInstance(string instanceName);

    /// <summary>
    /// 移除该实例的全部监听后，从池中删除该实例。
    /// </summary>
    /// <param name="instanceName">实例名称。</param>
    /// <returns>被移除的调度器；不存在时返回 null。</returns>
    public static IEventDispatcher RemoveInstance(string instanceName);

    /// <summary>
    /// 清除指定实例上的全部事件监听，但不从池中移除该实例。
    /// </summary>
    /// <param name="instanceName">实例名称。</param>
    public static void RemoveListeners(string instanceName);

    /// <summary>
    /// 清除池中每个事件调度器实例上的全部监听。
    /// </summary>
    public static void RemoveListeners();
}
```

`DefaultDispatcher` 对应名称为 `"Default"` 的实例；访问时若不存在会创建。`GetInstance` 始终创建缺失实例。`RemoveListeners()` 只清监听，不从池中删除实例。

#### EventDelegates
事件相关委托类型。

```csharp
/// <summary>
/// 事件相关委托类型。
/// </summary>
public static class EventDelegates
{
    /// <summary>
    /// 事件监听回调。
    /// </summary>
    /// <param name="evd">传递给监听器的事件数据。</param>
    public delegate void EventHandler(EventData evd);
}
```

#### EventConst
事件模块常量。

```csharp
/// <summary>
/// 事件模块常量。
/// </summary>
public static class EventConst
{
    /// <summary>
    /// 未指定时的默认监听权重（数值越大越先执行）。
    /// </summary>
    public const int DefaultWeight = 50;
}
```

### 功能说明

#### 事件系统架构

**IEventListener**
事件监听的注册与移除：
- **OnceEventListener**：添加单次事件监听（内部以 `listeningTimes = 1` 注册，触发一次后自动移除）
- **AddEventListener**：添加事件监听，支持权重、响应次数与标签的多种重载
- **RemoveEventListener**：删除事件监听，支持按处理器、标签、事件类型或全部清除
- **Dispose**：释放资源

**IEventDispatcher**
事件调度器接口，继承自 `IEventListener`，增加事件触发：
- **DispatchEvent**：触发事件，支持 `EventData` 对象或类型 + 数据形式

**IThreadEventDispatcher**
可将派发封送到指定同步上下文：
- **IsNullContext**：是否未设置同步上下文
- **SetThreadEventContext**：设置派发时使用的同步上下文
- **ClearThreadEventContext**：清除上下文，之后在调用线程上直接派发

**EventData**
事件数据封装：
- **Type**：事件类型标识
- **Data**：事件传递的数据
- **CurrentDispatcher**：派发该事件的调度器；构造时未传入则为 `null`

**EventDispatcher**
默认调度器实现，实现 `IThreadEventDispatcher`。按事件类型维护 `EventGroup`。无参构造的实例名为 `"Default"`。

**EventDispatcherPool**
按名称管理调度器实例：获取或创建、按名移除（可选先清监听）、清空全部。

**EventGroup**
单一事件类型的处理器列表，负责注册、按权重排序派发及限定调用次数。`EventDispatcher` 内部按类型使用此类。

**EventManager**
全局静态入口，内部持有一个 `EventDispatcherPool`，提供默认实例与按名获取 / 移除。

#### 事件监听特性

1. **权重系统**：`weight` 数值越大越先执行，默认值为 `EventConst.DefaultWeight`（50）
2. **标签管理**：通过 `tag` 标识监听器，便于按标签移除
3. **响应次数**：`listeningTimes` / `handleTimes` 限制最大响应次数；`0` 表示不限次数
4. **单次监听**：`OnceEventListener` 以响应次数 1 注册，本次派发仍会执行，之后不再保留
5. **重载解析**：整数字面量 `3` 为 `int`，会匹配权重重载；限定次数需写 `3u` 或显式 `uint`

#### 线程同步派发

通过 `IThreadEventDispatcher` 把监听回调封送到指定线程：
- 已设置上下文时使用 `SynchronizationContext.Send`（同步等待目标线程执行完毕）
- 未设置上下文时在调用线程上直接执行
- C# 默认的 `SynchronizationContext` 实现只是空壳，不维护队列，也不具备线程切换或调度能力；应传入具备队列调度能力的派生类

### 使用示例

#### 基本事件监听和分发
```csharp
// 创建事件分发器
var dispatcher = new EventDispatcher();

// 添加事件监听器
dispatcher.AddEventListener("user.login", (evd) => {
    Console.WriteLine($"用户登录: {evd.Data}");
});

// 添加带权重和标签的事件监听器
dispatcher.AddEventListener("user.login", (evd) => {
    Console.WriteLine("记录登录日志");
}, 10, "log_handler");

// 添加单次事件监听器
dispatcher.OnceEventListener("user.login", (evd) => {
    Console.WriteLine("欢迎新用户！");
});

// 触发事件
dispatcher.DispatchEvent("user.login", new { userId = 123, username = "张三" });
```

#### 事件数据使用
```csharp
// 创建事件数据
var eventData = new EventData("data.update", new { id = 1, value = "new_value" });

// 添加监听器
dispatcher.AddEventListener("data.update", (evd) => {
    Console.WriteLine($"事件类型: {evd.Type}");
    Console.WriteLine($"事件数据: {evd.Data}");
    Console.WriteLine($"分发器: {evd.CurrentDispatcher}");
});

// 分发事件
dispatcher.DispatchEvent(eventData);
```

#### 事件监听器管理
```csharp
// 添加带标签的监听器
dispatcher.AddEventListener("system.startup", StartupHandler, "startup_handler");
dispatcher.AddEventListener("system.shutdown", ShutdownHandler, "shutdown_handler");

// 按标签删除监听器
dispatcher.RemoveEventListener("system.startup", "startup_handler");

// 按处理器删除监听器（tag 为 null 时仅移除最后一个匹配项）
dispatcher.RemoveEventListener("system.shutdown", ShutdownHandler);

// 删除特定类型的所有监听器
dispatcher.RemoveEventListener("system.shutdown");

// 清除所有监听器
dispatcher.RemoveEventListener();

// 释放资源
dispatcher.Dispose();
```

#### 全局事件管理器
```csharp
// 默认实例（名称为 "Default"）
EventManager.DefaultDispatcher.AddEventListener("app.ready", OnReady);
EventManager.DefaultDispatcher.DispatchEvent("app.ready", null);

// 按名称获取或创建
var combat = EventManager.GetInstance("combat");
combat.DispatchEvent("skill.cast", skillId);

// 只清除该实例上的监听，实例仍留在池中
EventManager.RemoveListeners("combat");

// 清除监听并从池中移除实例
EventManager.RemoveInstance("combat");

// 清除池中每个实例上的全部监听（不删除实例）
EventManager.RemoveListeners();
```

#### 事件调度器池
```csharp
var pool = new EventDispatcherPool();

// 不存在则创建
var combat = pool.GetInstance("combat", createIfNotExist: true);

// 不存在且不创建时返回 null
var missing = pool.GetInstance("unknown", createIfNotExist: false);

// 移除指定实例（默认先清除监听）
pool.Clear("combat");

// 清除全部监听并清空池
pool.ClearAll();
```

#### 线程同步派发
```csharp
var dispatcher = new EventDispatcher("ui");

// context 须为具备队列或调度能力的 SynchronizationContext 派生类
dispatcher.SetThreadEventContext(context);
Console.WriteLine(dispatcher.IsNullContext); // false

dispatcher.AddEventListener("ui.refresh", OnRefresh);
dispatcher.DispatchEvent("ui.refresh", null); // 经 context.Send 派发

dispatcher.ClearThreadEventContext();
dispatcher.DispatchEvent("ui.refresh", null); // 在调用线程上直接派发
```

#### 权重和响应次数控制
```csharp
// 高优先级监听器（权重越大越先执行）
dispatcher.AddEventListener("critical.event", CriticalHandler, 100);

// 普通优先级监听器
dispatcher.AddEventListener("critical.event", NormalHandler, EventConst.DefaultWeight);

// 低优先级监听器
dispatcher.AddEventListener("critical.event", LowPriorityHandler, 1);

// 限制响应次数（须使用 uint，否则整数字面量会匹配权重重载）
dispatcher.AddEventListener("limited.event", LimitedHandler, 3u); // 只响应 3 次

// 带权重和响应次数限制的监听器
dispatcher.AddEventListener("complex.event", ComplexHandler, 25, 5u); // 权重 25，响应 5 次

// 不限次数
dispatcher.AddEventListener("forever.event", ForeverHandler, EventConst.DefaultWeight, 0u, "forever");
```

#### EventGroup 直接使用
```csharp
var group = new EventGroup();
group.AddEventHandler(OnTick, EventConst.DefaultWeight, 0, "tick");
group.Handle(new EventData("tick", null));
group.RemoveEventHandler("tick");
```

### 设计特点

1. **灵活性**：支持权重、标签、响应次数与单次监听等多种注册方式
2. **可扩展性**：通过 `IEventListener` / `IEventDispatcher` / `IThreadEventDispatcher` 分层扩展
3. **线程封送**：可将派发同步到具备调度能力的 `SynchronizationContext`
4. **命名实例**：`EventManager` 与 `EventDispatcherPool` 按名称复用调度器
5. **资源管理**：支持按类型、标签、处理器移除，以及 `Dispose` 释放

### 注意事项

1. **内存管理**：及时移除不需要的监听器，避免内存泄漏
2. **事件循环**：避免在事件处理中再次触发相同事件造成循环
3. **异常处理**：事件处理中的异常需要妥善处理
4. **线程安全**：注册与派发本身无额外同步；跨线程派发应设置具备队列调度能力的同步上下文，不要使用默认空壳 `SynchronizationContext`
5. **权重方向**：数值越大越先执行，不是越小越高优先
6. **次数重载**：`AddEventListener(type, handler, 3)` 匹配的是权重；限定次数请传 `uint`（如 `3u`）
7. **性能考虑**：大量事件监听器可能影响性能
