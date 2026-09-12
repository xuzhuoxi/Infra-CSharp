# Threadx API 文档

## 命名空间: JLGames.Infra.Threadx

### 类 (Classes)

#### FixedThreadContext
将工作通过阻塞队列派发到固定主线程的 `SynchronizationContext`。

```csharp
/// <summary>
/// 将工作通过阻塞队列派发到固定主线程的 SynchronizationContext。
/// </summary>
public class FixedThreadContext : SynchronizationContext, IDisposable, ICloneable<SynchronizationContext>
{
    private const int KAwqInitialCapacity = 20;
    private readonly BlockingCollection<ContextWorkRequest> m_AsyncWorkQueue;
    private readonly int m_MainThreadId;
    private bool m_Disposed;
    private int m_ExecFlag = 0;

    /// <summary>
    /// 拥有本上下文的托管线程 ID（工作派发的目标线程）。
    /// </summary>
    public int MainThreadId { get; }

    /// <summary>
    /// 当前已入队、尚未取出的工作项数量。
    /// </summary>
    public int PendingCount { get; }

    /// <summary>
    /// 创建绑定到 mainThreadId 的上下文，并新建内部队列。
    /// </summary>
    /// <param name="mainThreadId">主线程的托管线程 ID。</param>
    public FixedThreadContext(int mainThreadId);

    /// <summary>
    /// 使用已有队列创建上下文（供 Clone 使用）。
    /// </summary>
    /// <param name="queue">共享的异步工作队列。</param>
    /// <param name="mainThreadId">主线程的托管线程 ID。</param>
    public FixedThreadContext(BlockingCollection<ContextWorkRequest> queue, int mainThreadId);

    /// <summary>
    /// 将回调加入队列，在主线程上异步执行；调用方立即返回。
    /// </summary>
    /// <param name="callback">在主线程上执行的委托。</param>
    /// <param name="state">传给 callback 的状态对象。</param>
    public override void Post(SendOrPostCallback callback, object state);

    /// <summary>
    /// 在主线程上执行回调并阻塞调用方直至完成。
    /// </summary>
    /// <remarks>
    /// 若已在主线程则直接调用 callback；否则入队并借助等待句柄阻塞直至执行完毕。
    /// </remarks>
    /// <param name="callback">在主线程上执行的委托。</param>
    /// <param name="state">传给 callback 的状态对象。</param>
    public override void Send(SendOrPostCallback callback, object state);

    /// <summary>
    /// 停止后台执行，并将队列标记为不再接受新项。
    /// </summary>
    public void Dispose();

    /// <summary>
    /// 返回共享同一队列与主线程 ID 的新上下文实例。
    /// </summary>
    /// <returns>克隆后的 SynchronizationContext。</returns>
    public SynchronizationContext Clone();

    /// <summary>
    /// 处理当前队列中的全部工作项；由主线程或外部泵循环调用。
    /// </summary>
    public void ProcessTasks();

    /// <summary>
    /// 最多取出并执行 maxTaskSize 个工作项；队列为空时不阻塞。
    /// </summary>
    /// <param name="maxTaskSize">本次调用最多处理的工作项数。</param>
    public void ProcessTasks(int maxTaskSize);

    /// <summary>
    /// 启动后台线程，阻塞等待队列中的工作项并执行，直至调用 StopExec。
    /// </summary>
    public void StartExec();

    /// <summary>
    /// 停止接受新工作并完成队列；待处理项执行完毕后后台线程退出。
    /// </summary>
    public void StopExec();
}
```

### 结构体 (Structs)

#### ContextWorkRequest
`FixedThreadContext` 的不可变工作项，封装回调、状态及可选的等待句柄。

```csharp
/// <summary>
/// FixedThreadContext 的不可变工作项，封装回调、状态及可选的等待句柄。
/// </summary>
public readonly struct ContextWorkRequest
{
    private readonly SendOrPostCallback m_DelegateCallback;
    private readonly object m_DelegateState;
    private readonly ManualResetEvent m_WaitHandle;

    /// <summary>
    /// 创建工作请求，供主线程队列调度执行。
    /// </summary>
    /// <param name="callback">在主线程上调用的委托。</param>
    /// <param name="state">传给 callback 的状态对象。</param>
    /// <param name="waitHandle">
    /// 可选信号量，在 Invoke 回调结束后置位；供 FixedThreadContext.Send 同步等待使用。
    /// </param>
    public ContextWorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null);

    /// <summary>
    /// 执行回调；若存在等待句柄则在完成后置位。
    /// </summary>
    public void Invoke();
}
```

### 功能说明

#### 线程上下文特性

**核心功能**
- **线程安全**：通过阻塞队列派发工作，由固定主线程或后台执行线程取出执行
- **异步支持**：`Post` 入队后立即返回，不等待执行完成
- **同步支持**：`Send` 在主线程上直接执行，或入队后阻塞直至完成
- **队列管理**：使用 `BlockingCollection<ContextWorkRequest>` 管理待执行工作项
- **两种泵模式**：`ProcessTasks` 由外部循环拉取；`StartExec` 启动专用后台线程消费队列

**执行模式**
1. **Post 模式**：异步提交任务，调用方立即返回
2. **Send 模式**：同步提交任务；若已在 `MainThreadId` 对应线程上则直接调用，否则入队并等待
3. **外部泵**：在主线程（或游戏循环）中调用 `ProcessTasks` / `ProcessTasks(int)`，队列为空时不阻塞
4. **后台执行**：调用 `StartExec` 后，后台线程阻塞在队列上取出并执行工作项，直至 `StopExec`

#### 线程同步机制

**FixedThreadContext**
- **主线程标识**：通过 `MainThreadId` 标识工作派发的目标线程；`Send` 用其判断是否可直接执行
- **任务队列**：使用 `BlockingCollection` 管理任务；双参数构造函数与 `Clone` 共享同一队列
- **执行标志**：`StartExec` / `StopExec` 通过内部执行标志保证后台线程只启动一次
- **资源管理**：`Dispose` 调用 `StopExec`，停止后台执行并将队列标记为完成添加

**ContextWorkRequest**
- **任务封装**：封装回调函数和状态对象
- **同步支持**：通过可选的 `ManualResetEvent` 实现 `Send` 的同步等待
- **完成通知**：`Invoke` 在 `finally` 中置位等待句柄，确保调用方一定被唤醒

### 使用示例

#### 基本使用
```csharp
// 创建固定线程上下文
var threadContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

// 启动后台线程消费队列
threadContext.StartExec();

// 异步提交任务
threadContext.Post((state) => {
    Console.WriteLine($"异步任务执行: {state}");
    Console.WriteLine($"当前线程ID: {Thread.CurrentThread.ManagedThreadId}");
}, "异步数据");

// 同步提交任务（非主线程时入队并等待完成）
threadContext.Send((state) => {
    Console.WriteLine($"同步任务执行: {state}");
    Console.WriteLine($"当前线程ID: {Thread.CurrentThread.ManagedThreadId}");
}, "同步数据");

// 检查待处理任务数量
Console.WriteLine($"待处理任务数: {threadContext.PendingCount}");

// 等待一段时间让任务执行
Thread.Sleep(1000);

// 释放资源（内部调用 StopExec）
threadContext.Dispose();
```

#### 主线程泵循环（ProcessTasks）
```csharp
// 适合游戏主循环 / UI 消息泵：在目标线程上主动取出任务，保证真正在该线程执行
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

Task.Run(() =>
{
    context.Post(_ => Console.WriteLine($"后台投递，执行线程: {Thread.CurrentThread.ManagedThreadId}"), null);
    context.Send(_ => Console.WriteLine("同步投递完成"), null);
});

// 主循环中按帧处理，队列为空时不阻塞
while (running)
{
    context.ProcessTasks();          // 处理当前队列中的全部工作项
    // 或限制本帧工作量：
    // context.ProcessTasks(8);
    Thread.Sleep(16);
}

context.Dispose();
```

#### UI 线程同步
```csharp
// 在 WPF/WinForms 应用中，用 ProcessTasks 把更新泵回 UI 线程
public class MainWindow
{
    private FixedThreadContext uiContext;
    private System.Windows.Threading.DispatcherTimer pumpTimer;

    public MainWindow()
    {
        uiContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
        pumpTimer = new System.Windows.Threading.DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(16)
        };
        pumpTimer.Tick += (_, __) => uiContext.ProcessTasks();
        pumpTimer.Start();
    }

    private void StartBackgroundWork()
    {
        Task.Run(() => {
            Thread.Sleep(2000);

            // 更新 UI（异步）
            uiContext.Post((state) => {
                UpdateUI((string)state);
            }, "后台任务完成");

            // 更新 UI（同步，等待 UI 线程执行完毕）
            uiContext.Send((state) => {
                ShowMessage((string)state);
            }, "同步更新完成");
        });
    }

    private void UpdateUI(string message)
    {
        Console.WriteLine($"UI更新: {message}");
    }

    private void ShowMessage(string message)
    {
        Console.WriteLine($"显示消息: {message}");
    }

    public void Dispose()
    {
        pumpTimer?.Stop();
        uiContext?.Dispose();
    }
}
```

#### 多线程协作
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
context.StartExec();

// 多个后台线程向同一上下文投递
Task.Run(() => {
    context.Post((state) => {
        Console.WriteLine($"异步任务: {state}");
    }, "来自线程1的任务");
});

Task.Run(() => {
    context.Send((state) => {
        Console.WriteLine($"同步任务: {state}");
    }, "来自线程2的同步任务");
});

Thread.Sleep(2000);
context.Dispose();
```

#### 任务队列管理
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
context.StartExec();

for (int i = 0; i < 10; i++)
{
    int taskId = i;
    context.Post((state) => {
        Console.WriteLine($"执行任务 {taskId}");
        Thread.Sleep(100);
    }, null);
}

while (context.PendingCount > 0)
{
    Console.WriteLine($"剩余任务数: {context.PendingCount}");
    Thread.Sleep(100);
}

Console.WriteLine("所有任务完成");
context.StopExec();
```

#### 异常处理
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

// 在主线程上 Send 会直接调用，异常会传播到调用方
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

// Post 的回调在消费线程上执行；后台 Exec 会吞掉异常。
// 若使用 ProcessTasks，异常会传播到泵循环调用方。
context.Post((state) => {
    try
    {
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

context.ProcessTasks();
context.Dispose();
```

#### 克隆和共享队列
```csharp
var originalContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
originalContext.StartExec();

// Clone 返回 SynchronizationContext，且与原实例共享同一队列和 MainThreadId
var clonedContext = (FixedThreadContext)originalContext.Clone();
Console.WriteLine(clonedContext.MainThreadId == originalContext.MainThreadId);

// 向克隆实例 Post 的工作会进入同一队列，由原实例的后台线程消费
clonedContext.Post((state) => {
    Console.WriteLine("共享队列中的任务");
}, null);

Thread.Sleep(1000);

// 只需释放其中一个持有队列所有权的实例；Dispose/StopExec 会完成该共享队列
originalContext.Dispose();
```

### 设计特点

1. **线程派发**：通过阻塞队列将 `Post`/`Send` 的工作派发到固定上下文
2. **异步支持**：`Post` 入队后立即返回
3. **同步支持**：`Send` 在目标线程直接执行，或入队后等待 `ManualResetEvent`
4. **外部泵**：`ProcessTasks` 供主循环按批取出，空队列不阻塞
5. **后台消费**：`StartExec` 启动后台线程阻塞消费，`StopExec`/`Dispose` 结束
6. **浅克隆**：`Clone` 共享同一队列与主线程 ID，而非复制一份独立队列

### 注意事项

1. **线程亲和**：若工作必须在 `MainThreadId` 对应线程上执行，应在该线程调用 `ProcessTasks`；`StartExec` 会在新的后台线程上执行工作项，并不等于主线程
2. **资源释放**：及时调用 `Dispose` 或 `StopExec`；完成后队列不再接受新项
3. **异常处理**：主线程上的 `Send` 会抛出回调异常；后台 `Exec` 会忽略回调异常；`ProcessTasks` 会将非 `InvalidOperationException` 的异常抛给调用方
4. **死锁避免**：在未启动 `StartExec` 且无人调用 `ProcessTasks` 时，从非主线程调用 `Send` 会一直阻塞
5. **克隆语义**：克隆实例共享队列；不要对克隆再调用 `StartExec`，以免两个后台线程同时消费同一队列
6. **性能考虑**：大量入队时注意队列容量与内存；可用 `ProcessTasks(int)` 限制单次处理数量
