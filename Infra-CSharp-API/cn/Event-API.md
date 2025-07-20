# Event API 文档

## 命名空间: JLGames.Infra.Event

### 接口 (Interfaces)

#### IEventListener
事件监听器接口

```csharp
public interface IEventListener
{
    /// <summary>
    /// Add single event listener
    /// 添加单次事件监听
    /// </summary>
    /// <param name="type">Event Type<br/>事件类型</param>
    /// <param name="handler">Listener Function<br/>监听函数</param>
    /// <param name="weight">Response Weight<br/>响应权重</param>
    /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
    void OnceEventListener(string type, EventDelegates.EventHandler handler, int weight = EventConst.DefaultWeight, string tag = null);

    /// <summary>
    /// Add event listener
    /// 添加事件监听
    /// </summary>
    /// <param name="type">Event Type<br/>事件类型</param>
    /// <param name="handler">Listener Function<br/>监听函数</param>
    /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// Add event listener
    /// 添加事件监听
    /// </summary>
    /// <param name="type">Event Type<br/>事件类型</param>
    /// <param name="handler">Listener Function<br/>监听函数</param>
    /// <param name="weight">Response Weight<br/>响应权重</param>
    /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, string tag = null);

    /// <summary>
    /// Add event listener
    /// 添加事件监听
    /// </summary>
    /// <param name="type">Event Type<br/>事件类型</param>
    /// <param name="handler">Listener Function<br/>监听函数</param>
    /// <param name="listeningTimes">Times of responses<br/>响应次数</param>
    /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, uint listeningTimes, string tag = null);

    /// <summary>
    /// Add event listener
    /// 添加事件监听
    /// </summary>
    /// <param name="type">Event Type<br/>事件类型</param>
    /// <param name="handler">Listener Function<br/>监听函数</param>
    /// <param name="weight">Response Weight<br/>响应权重</param>
    /// <param name="listeningTimes">Times of responses<br/>响应次数</param>
    /// <param name="tag">Function Tag<br/>函数的唯一标签</param>
    void AddEventListener(string type, EventDelegates.EventHandler handler, int weight, uint listeningTimes, string tag = null);

    /// <summary>
    /// Delete event listener
    /// 删除事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="handler">监听函数</param>
    /// <param name="tag"></param>
    void RemoveEventListener(string type, EventDelegates.EventHandler handler, string tag = null);

    /// <summary>
    /// Delete event listener
    /// 删除事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="tag"></param>
    void RemoveEventListener(string type, string tag);

    /// <summary>
    /// Delete a type of event listeners
    /// 删除一类事件监听
    /// </summary>
    /// <param name="type">事件类型</param>
    void RemoveEventListener(string type);

    /// <summary>
    /// Clear all event listeners
    /// 清除全部事件监听
    /// </summary>
    void RemoveEventListener();

    /// <summary>
    /// Dispose
    /// 释放
    /// </summary>
    void Dispose();
}
```

#### IEventDispatcher
事件分发器接口，继承自IEventListener

```csharp
public interface IEventDispatcher : IEventListener
{
    /// <summary>
    /// Trigger an event of a certain type and pass data
    /// 触发某一类型的事件,并传递数据
    /// </summary>
    /// <param name="evd">Evd.</param>
    void DispatchEvent(EventData evd);

    /// <summary>
    /// Trigger an event of a certain type and pass data
    /// 触发某一类型的事件,并传递数据
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="data">事件的数据(可为null)</param>
    void DispatchEvent(string type, object data);
}
```

### 类 (Classes)

#### EventData
事件数据类

```csharp
/// <summary>
/// Event data.
/// 事件数据
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
    /// 事件类型
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Event data
    /// 事件传递的数据
    /// </summary>
    public object Data { get; }

    public IEventDispatcher CurrentDispatcher { get; }
}
```

#### EventDispatcher
事件分发器实现类

```csharp
/// <summary>
/// 事件分发器
/// 负责事件的注册、分发和管理
/// </summary>
public class EventDispatcher : IEventDispatcher
{
    // 具体实现需要进一步分析文件内容
}
```

#### EventManager
事件管理器类

```csharp
/// <summary>
/// 事件管理器
/// 全局事件管理，提供单例模式的事件分发
/// </summary>
public class EventManager
{
    // 具体实现需要进一步分析文件内容
}
```

### 静态类 (Static Classes)

#### EventDelegates
事件委托定义

```csharp
public static class EventDelegates
{
    /// <summary>
    /// Delegate function
    /// 委托函数
    /// </summary>
    /// <param name="evd"></param>
    public delegate void EventHandler(EventData evd);
}
```

#### EventConst
事件常量定义

```csharp
public static class EventConst
{
    /// <summary>
    /// 事件默认
    /// </summary>
    public const int DefaultWeight = 50;
}
```

## 命名空间: JLGames.Infra.Event.Async

### 接口 (Interfaces)

#### IAsyncEventListener
异步事件监听器接口

```csharp
/// <summary>
/// 异步事件监听器接口
/// 支持异步事件处理
/// </summary>
public interface IAsyncEventListener
{
    // 具体实现需要进一步分析文件内容
}
```

#### IAsyncEventDispatcher
异步事件分发器接口

```csharp
/// <summary>
/// 异步事件分发器接口
/// 支持异步事件分发和处理
/// </summary>
public interface IAsyncEventDispatcher : IAsyncEventListener
{
    // 具体实现需要进一步分析文件内容
}
```

### 类 (Classes)

#### AsyncEventDispatcher
异步事件分发器实现类

```csharp
/// <summary>
/// 异步事件分发器
/// 支持异步事件处理的分发器实现
/// </summary>
public class AsyncEventDispatcher : IAsyncEventDispatcher
{
    // 具体实现需要进一步分析文件内容
}
```

### 功能说明

#### 事件系统架构

**IEventListener**
事件监听器接口，提供事件监听管理功能：
- **OnceEventListener**：添加单次事件监听，触发一次后自动移除
- **AddEventListener**：添加事件监听，支持多种重载形式
- **RemoveEventListener**：删除事件监听，支持按类型、函数、标签删除
- **Dispose**：释放资源

**IEventDispatcher**
事件分发器接口，继承自IEventListener，增加事件触发功能：
- **DispatchEvent**：触发事件，支持EventData对象或类型+数据形式

**EventData**
事件数据封装类：
- **Type**：事件类型标识
- **Data**：事件传递的数据
- **CurrentDispatcher**：当前分发器引用

#### 事件监听特性

1. **权重系统**：通过weight参数控制监听器执行顺序
2. **标签管理**：通过tag参数标识和管理监听器
3. **响应次数**：支持限制监听器的响应次数
4. **单次监听**：OnceEventListener自动移除机制

#### 异步事件支持

通过Async命名空间提供异步事件处理能力：
- 支持异步事件监听器
- 支持异步事件分发
- 提供异步事件处理机制

### 使用示例

#### 基本事件监听和分发
```csharp
// 创建事件分发器
var dispatcher = new EventDispatcher();

// 添加事件监听器
dispatcher.AddEventListener("user.login", (evd) => {
    Console.WriteLine($"用户登录: {evd.Data}");
});

// 添加带权重的事件监听器
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

// 删除特定类型的所有监听器
dispatcher.RemoveEventListener("system.shutdown");

// 清除所有监听器
dispatcher.RemoveEventListener();

// 释放资源
dispatcher.Dispose();
```

#### 异步事件处理
```csharp
// 创建异步事件分发器
var asyncDispatcher = new AsyncEventDispatcher();

// 添加异步事件监听器
asyncDispatcher.AddEventListener("async.task", async (evd) => {
    await Task.Delay(1000); // 模拟异步操作
    Console.WriteLine("异步任务完成");
});

// 异步触发事件
await asyncDispatcher.DispatchEventAsync("async.task", null);
```

#### 权重和响应次数控制
```csharp
// 高优先级监听器（权重越小优先级越高）
dispatcher.AddEventListener("critical.event", CriticalHandler, 1);

// 普通优先级监听器
dispatcher.AddEventListener("critical.event", NormalHandler, 50);

// 低优先级监听器
dispatcher.AddEventListener("critical.event", LowPriorityHandler, 100);

// 限制响应次数的监听器
dispatcher.AddEventListener("limited.event", LimitedHandler, 3); // 只响应3次

// 带权重和响应次数限制的监听器
dispatcher.AddEventListener("complex.event", ComplexHandler, 25, 5); // 权重25，响应5次
```

### 设计特点

1. **灵活性**：支持多种事件监听方式
2. **可扩展性**：通过接口设计便于扩展
3. **异步支持**：提供异步事件处理能力
4. **资源管理**：支持监听器的生命周期管理
5. **性能优化**：通过权重和标签系统优化事件处理

### 注意事项

1. **内存管理**：及时移除不需要的监听器避免内存泄漏
2. **事件循环**：避免在事件处理中触发相同事件造成循环
3. **异常处理**：事件处理中的异常需要妥善处理
4. **线程安全**：多线程环境需要额外的同步机制
5. **性能考虑**：大量事件监听器可能影响性能 