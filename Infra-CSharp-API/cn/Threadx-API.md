# Threadx API 文档

## 命名空间: JLGames.Infra.Threadx

### 类 (Classes)

#### FixedThreadContext
固定线程上下文类

```csharp
/// <summary>
/// 固定线程上下文类
/// 提供线程安全的同步上下文，确保任务在指定线程上执行
/// </summary>
public class FixedThreadContext : SynchronizationContext, IDisposable, ICloneable<SynchronizationContext>
{
    private const int KAwqInitialCapacity = 20;
    private readonly BlockingCollection<ContextWorkRequest> m_AsyncWorkQueue;
    private readonly int m_MainThreadId;
    private int m_ExecFlag = 0;

    /// <summary>
    /// 主线程ID
    /// </summary>
    public int MainThreadId => m_MainThreadId;

    /// <summary>
    /// 待处理任务数量
    /// </summary>
    public int PendingCount => m_AsyncWorkQueue.Count;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="mainThreadId">主线程ID</param>
    public FixedThreadContext(int mainThreadId);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="queue">工作队列</param>
    /// <param name="mainThreadId">主线程ID</param>
    public FixedThreadContext(BlockingCollection<ContextWorkRequest> queue, int mainThreadId);

    /// <summary>
    /// Post will add the call to a task list to be executed later on the main thread then work will continue asynchronously
    /// 异步提交任务到队列，稍后在主线程上执行
    /// </summary>
    /// <param name="callback">回调函数</param>
    /// <param name="state">状态对象</param>
    public override void Post(SendOrPostCallback callback, object state);

    /// <summary>
    /// Send will process the call synchronously. If the call is processed on the main thread, we'll invoke it
    /// directly here. If the call is processed on another thread it will be queued up like POST to be executed
    /// on the main thread and it will wait. Once the main thread processes the work we can continue
    /// 同步执行任务，如果在主线程上则直接执行，否则排队等待主线程处理
    /// </summary>
    /// <param name="callback">回调函数</param>
    /// <param name="state">状态对象</param>
    public override void Send(SendOrPostCallback callback, object state);

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose();

    /// <summary>
    /// 克隆同步上下文
    /// </summary>
    /// <returns>新的同步上下文</returns>
    public SynchronizationContext Clone();

    /// <summary>
    /// 开始执行任务
    /// </summary>
    public void StartExec();

    /// <summary>
    /// Exec will execute tasks off the task list
    /// 执行任务队列中的任务
    /// </summary>
    private void Exec();
}
```

### 结构体 (Structs)

#### ContextWorkRequest
上下文工作请求结构体

```csharp
/// <summary>
/// 上下文工作请求结构体
/// 封装要执行的工作任务和同步对象
/// </summary>
public readonly struct ContextWorkRequest
{
    private readonly SendOrPostCallback m_DelegateCallback;
    private readonly object m_DelegateState;
    private readonly ManualResetEvent m_WaitHandle;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="callback">回调函数</param>
    /// <param name="state">状态对象</param>
    /// <param name="waitHandle">等待句柄</param>
    public ContextWorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null);

    /// <summary>
    /// 执行任务
    /// </summary>
    public void Invoke();
}
```

### 功能说明

#### 线程上下文特性

**核心功能**
- **线程安全**：确保任务在指定线程上执行
- **异步支持**：支持异步任务提交和执行
- **同步支持**：支持同步任务执行和等待
- **队列管理**：使用阻塞队列管理待执行任务

**执行模式**
1. **Post模式**：异步提交任务，不等待执行完成
2. **Send模式**：同步提交任务，等待执行完成
3. **队列执行**：后台线程从队列中取出任务执行

#### 线程同步机制

**FixedThreadContext**
- **主线程标识**：通过MainThreadId标识主线程
- **任务队列**：使用BlockingCollection管理任务
- **执行标志**：通过ExecFlag控制执行状态
- **资源管理**：实现IDisposable接口

**ContextWorkRequest**
- **任务封装**：封装回调函数和状态对象
- **同步支持**：通过ManualResetEvent实现同步
- **异常处理**：确保等待句柄正确设置

### 使用示例

#### 基本使用
```csharp
// 创建固定线程上下文
var threadContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

// 开始执行任务
threadContext.StartExec();

// 异步提交任务
threadContext.Post((state) => {
    Console.WriteLine($"异步任务执行: {state}");
    Console.WriteLine($"当前线程ID: {Thread.CurrentThread.ManagedThreadId}");
}, "异步数据");

// 同步提交任务
threadContext.Send((state) => {
    Console.WriteLine($"同步任务执行: {state}");
    Console.WriteLine($"当前线程ID: {Thread.CurrentThread.ManagedThreadId}");
}, "同步数据");

// 检查待处理任务数量
Console.WriteLine($"待处理任务数: {threadContext.PendingCount}");

// 等待一段时间让任务执行
Thread.Sleep(1000);

// 释放资源
threadContext.Dispose();
```

#### UI线程同步
```csharp
// 在WPF/WinForms应用中
public class MainWindow
{
    private FixedThreadContext uiContext;

    public MainWindow()
    {
        // 创建UI线程上下文
        uiContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
        uiContext.StartExec();
    }

    private void StartBackgroundWork()
    {
        // 在后台线程中执行耗时操作
        Task.Run(() => {
            // 模拟耗时操作
            Thread.Sleep(2000);

            // 更新UI（异步）
            uiContext.Post((state) => {
                UpdateUI((string)state);
            }, "后台任务完成");

            // 更新UI（同步）
            uiContext.Send((state) => {
                ShowMessage((string)state);
            }, "同步更新完成");
        });
    }

    private void UpdateUI(string message)
    {
        // 在UI线程上执行
        Console.WriteLine($"UI更新: {message}");
    }

    private void ShowMessage(string message)
    {
        // 在UI线程上执行
        Console.WriteLine($"显示消息: {message}");
    }

    public void Dispose()
    {
        uiContext?.Dispose();
    }
}
```

#### 多线程协作
```csharp
// 创建多个线程上下文
var context1 = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
var context2 = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

context1.StartExec();
context2.StartExec();

// 线程1提交任务到线程2
Task.Run(() => {
    context2.Post((state) => {
        Console.WriteLine($"线程2执行任务: {state}");
    }, "来自线程1的任务");
});

// 线程2提交任务到线程1
Task.Run(() => {
    context1.Send((state) => {
        Console.WriteLine($"线程1执行同步任务: {state}");
    }, "来自线程2的同步任务");
});

// 等待任务完成
Thread.Sleep(2000);

// 清理资源
context1.Dispose();
context2.Dispose();
```

#### 任务队列管理
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
context.StartExec();

// 提交多个任务
for (int i = 0; i < 10; i++)
{
    int taskId = i;
    context.Post((state) => {
        Console.WriteLine($"执行任务 {taskId}");
        Thread.Sleep(100); // 模拟工作
    }, null);
}

// 监控任务队列
while (context.PendingCount > 0)
{
    Console.WriteLine($"剩余任务数: {context.PendingCount}");
    Thread.Sleep(100);
}

Console.WriteLine("所有任务完成");
context.Dispose();
```

#### 异常处理
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
context.StartExec();

// 提交可能抛出异常的任务
context.Post((state) => {
    try
    {
        // 模拟可能出错的代码
        if (new Random().Next(2) == 0)
        {
            throw new Exception("随机错误");
        }
        Console.WriteLine("任务执行成功");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"任务执行失败: {ex.Message}");
    }
}, null);

// 提交同步任务并处理异常
try
{
    context.Send((state) => {
        throw new Exception("同步任务错误");
    }, null);
}
catch (Exception ex)
{
    Console.WriteLine($"同步任务异常: {ex.Message}");
}

Thread.Sleep(1000);
context.Dispose();
```

#### 克隆和复制
```csharp
var originalContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
originalContext.StartExec();

// 克隆上下文
var clonedContext = originalContext.Clone();
clonedContext.StartExec();

// 在两个上下文中提交任务
originalContext.Post((state) => {
    Console.WriteLine("原始上下文任务");
}, null);

clonedContext.Post((state) => {
    Console.WriteLine("克隆上下文任务");
}, null);

Thread.Sleep(1000);

// 清理资源
originalContext.Dispose();
clonedContext.Dispose();
```

### 设计特点

1. **线程安全**：确保任务在指定线程上执行
2. **异步支持**：支持异步任务提交
3. **同步支持**：支持同步任务执行和等待
4. **资源管理**：实现IDisposable接口
5. **队列管理**：使用阻塞队列管理任务
6. **克隆支持**：支持上下文克隆

### 注意事项

1. **线程ID**：确保MainThreadId正确设置
2. **资源释放**：及时调用Dispose方法
3. **异常处理**：妥善处理任务执行中的异常
4. **死锁避免**：避免在Send回调中再次调用Send
5. **性能考虑**：大量任务时注意内存使用
6. **线程安全**：多线程访问时注意同步 