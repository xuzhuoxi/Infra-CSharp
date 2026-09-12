# Threadx API Documentation

## Namespace: JLGames.Infra.Threadx

### Classes

#### FixedThreadContext
`SynchronizationContext` that marshals work to a fixed main thread via a blocking queue.

```csharp
/// <summary>
/// SynchronizationContext that marshals work to a fixed main thread via a blocking queue.
/// </summary>
public class FixedThreadContext : SynchronizationContext, IDisposable, ICloneable<SynchronizationContext>
{
    private const int KAwqInitialCapacity = 20;
    private readonly BlockingCollection<ContextWorkRequest> m_AsyncWorkQueue;
    private readonly int m_MainThreadId;
    private bool m_Disposed;
    private int m_ExecFlag = 0;

    /// <summary>
    /// Managed thread ID of the thread that owns this context (target for marshaled work).
    /// </summary>
    public int MainThreadId { get; }

    /// <summary>
    /// Number of work items currently queued and not yet taken.
    /// </summary>
    public int PendingCount { get; }

    /// <summary>
    /// Creates a context with a new internal queue bound to mainThreadId.
    /// </summary>
    /// <param name="mainThreadId">Managed thread ID of the main thread.</param>
    public FixedThreadContext(int mainThreadId);

    /// <summary>
    /// Creates a context that shares an existing queue (used by Clone).
    /// </summary>
    /// <param name="queue">Shared async work queue.</param>
    /// <param name="mainThreadId">Managed thread ID of the main thread.</param>
    public FixedThreadContext(BlockingCollection<ContextWorkRequest> queue, int mainThreadId);

    /// <summary>
    /// Queues the callback for asynchronous execution on the main thread; the caller returns immediately.
    /// </summary>
    /// <param name="callback">Delegate to run on the main thread.</param>
    /// <param name="state">State passed to callback.</param>
    public override void Post(SendOrPostCallback callback, object state);

    /// <summary>
    /// Runs the callback on the main thread and blocks until it completes.
    /// </summary>
    /// <remarks>
    /// If already on the main thread, invokes callback directly; otherwise enqueues with a wait handle and blocks.
    /// </remarks>
    /// <param name="callback">Delegate to run on the main thread.</param>
    /// <param name="state">State passed to callback.</param>
    public override void Send(SendOrPostCallback callback, object state);

    /// <summary>
    /// Stops background execution and marks the queue as complete for adding.
    /// </summary>
    public void Dispose();

    /// <summary>
    /// Returns a new context instance that shares the same queue and main thread ID.
    /// </summary>
    /// <returns>A cloned SynchronizationContext.</returns>
    public SynchronizationContext Clone();

    /// <summary>
    /// Drains all currently queued work items; intended to be called from the main thread (or an external pump).
    /// </summary>
    public void ProcessTasks();

    /// <summary>
    /// Dequeues and runs at most maxTaskSize work items without blocking on an empty queue.
    /// </summary>
    /// <param name="maxTaskSize">Maximum number of items to process this call.</param>
    public void ProcessTasks(int maxTaskSize);

    /// <summary>
    /// Starts a background thread that blocks on the queue and invokes work items until StopExec.
    /// </summary>
    public void StartExec();

    /// <summary>
    /// Stops accepting new work and completes the queue; the background thread exits after pending items are processed.
    /// </summary>
    public void StopExec();
}
```

### Structs

#### ContextWorkRequest
Immutable work item for `FixedThreadContext`; wraps a callback, state, and optional wait handle.

```csharp
/// <summary>
/// Immutable work item for FixedThreadContext; wraps a callback, state, and optional wait handle.
/// </summary>
public readonly struct ContextWorkRequest
{
    private readonly SendOrPostCallback m_DelegateCallback;
    private readonly object m_DelegateState;
    private readonly ManualResetEvent m_WaitHandle;

    /// <summary>
    /// Creates a work request to be queued and executed on the main thread.
    /// </summary>
    /// <param name="callback">Delegate to invoke on the main thread.</param>
    /// <param name="state">State passed to callback.</param>
    /// <param name="waitHandle">
    /// Optional signal set in Invoke after the callback completes; used by synchronous FixedThreadContext.Send.
    /// </param>
    public ContextWorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null);

    /// <summary>
    /// Invokes the callback and signals waitHandle when present.
    /// </summary>
    public void Invoke();
}
```

### Function Description

#### Thread Context Features

**Core Features**
- **Thread Safety:** Marshals work through a blocking queue to a fixed main thread or a background consumer
- **Async Support:** `Post` enqueues work and returns immediately
- **Sync Support:** `Send` invokes directly on the main thread, or enqueues and blocks until completion
- **Queue Management:** Uses `BlockingCollection<ContextWorkRequest>` for pending work items
- **Two Pump Modes:** `ProcessTasks` is pulled by an external loop; `StartExec` starts a dedicated background consumer

**Execution Modes**
1. **Post Mode:** Asynchronously submit work without waiting for completion
2. **Send Mode:** Synchronously submit work; invokes immediately when already on `MainThreadId`, otherwise enqueues and waits
3. **External Pump:** Call `ProcessTasks` / `ProcessTasks(int)` on the main thread (or game loop); does not block on an empty queue
4. **Background Execution:** After `StartExec`, a background thread blocks on the queue and invokes items until `StopExec`

#### Thread Synchronization Mechanism

**FixedThreadContext**
- **Main Thread ID:** Identifies the target thread via `MainThreadId`; `Send` uses it to decide whether to invoke directly
- **Task Queue:** Uses `BlockingCollection` to manage work; the two-argument constructor and `Clone` share the same queue
- **Execution Flag:** `StartExec` / `StopExec` use an internal flag so the background thread starts only once
- **Resource Management:** `Dispose` calls `StopExec`, stopping background execution and marking the queue complete for adding

**ContextWorkRequest**
- **Task Encapsulation:** Wraps a callback and state object
- **Sync Support:** Optional `ManualResetEvent` implements waiting for `Send`
- **Completion Signal:** `Invoke` sets the wait handle in `finally` so the caller is always released

### Usage Examples

#### Basic Usage
```csharp
// Create fixed thread context
var threadContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

// Start a background thread that consumes the queue
threadContext.StartExec();

// Asynchronously submit work
threadContext.Post((state) => {
    Console.WriteLine($"Async task executed: {state}");
    Console.WriteLine($"Current thread ID: {Thread.CurrentThread.ManagedThreadId}");
}, "Async data");

// Synchronously submit work (enqueues and waits when not on the main thread)
threadContext.Send((state) => {
    Console.WriteLine($"Sync task executed: {state}");
    Console.WriteLine($"Current thread ID: {Thread.CurrentThread.ManagedThreadId}");
}, "Sync data");

// Check number of pending tasks
Console.WriteLine($"Pending task count: {threadContext.PendingCount}");

// Wait for tasks to execute
Thread.Sleep(1000);

// Dispose resources (calls StopExec internally)
threadContext.Dispose();
```

#### Main-Thread Pump (ProcessTasks)
```csharp
// Suitable for a game loop / UI message pump: drain work on the target thread
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

Task.Run(() =>
{
    context.Post(_ => Console.WriteLine($"Posted from background, run on: {Thread.CurrentThread.ManagedThreadId}"), null);
    context.Send(_ => Console.WriteLine("Synchronous post completed"), null);
});

// Process per frame; empty queue does not block
while (running)
{
    context.ProcessTasks();          // Drain all currently queued items
    // Or cap work per frame:
    // context.ProcessTasks(8);
    Thread.Sleep(16);
}

context.Dispose();
```

#### UI Thread Synchronization
```csharp
// In WPF/WinForms applications, pump updates back onto the UI thread with ProcessTasks
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

            // Update UI (asynchronously)
            uiContext.Post((state) => {
                UpdateUI((string)state);
            }, "Background task completed");

            // Update UI (synchronously, wait until the UI thread finishes)
            uiContext.Send((state) => {
                ShowMessage((string)state);
            }, "Sync update completed");
        });
    }

    private void UpdateUI(string message)
    {
        Console.WriteLine($"UI update: {message}");
    }

    private void ShowMessage(string message)
    {
        Console.WriteLine($"Show message: {message}");
    }

    public void Dispose()
    {
        pumpTimer?.Stop();
        uiContext?.Dispose();
    }
}
```

#### Multi-threaded Collaboration
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
context.StartExec();

// Multiple background threads post to the same context
Task.Run(() => {
    context.Post((state) => {
        Console.WriteLine($"Async task: {state}");
    }, "Task from thread 1");
});

Task.Run(() => {
    context.Send((state) => {
        Console.WriteLine($"Sync task: {state}");
    }, "Sync task from thread 2");
});

Thread.Sleep(2000);
context.Dispose();
```

#### Task Queue Management
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
context.StartExec();

for (int i = 0; i < 10; i++)
{
    int taskId = i;
    context.Post((state) => {
        Console.WriteLine($"Executing task {taskId}");
        Thread.Sleep(100);
    }, null);
}

while (context.PendingCount > 0)
{
    Console.WriteLine($"Remaining tasks: {context.PendingCount}");
    Thread.Sleep(100);
}

Console.WriteLine("All tasks completed");
context.StopExec();
```

#### Exception Handling
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

// Send on the main thread invokes directly, so exceptions propagate to the caller
try
{
    context.Send((state) => {
        throw new Exception("Sync task error");
    }, null);
}
catch (Exception ex)
{
    Console.WriteLine($"Sync task exception: {ex.Message}");
}

// Posted callbacks run on the consuming thread; background Exec swallows exceptions.
// With ProcessTasks, exceptions propagate to the pump caller.
context.Post((state) => {
    try
    {
        if (new Random().Next(2) == 0)
        {
            throw new Exception("Random error");
        }
        Console.WriteLine("Task executed successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Task execution failed: {ex.Message}");
    }
}, null);

context.ProcessTasks();
context.Dispose();
```

#### Cloning and Shared Queue
```csharp
var originalContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
originalContext.StartExec();

// Clone returns SynchronizationContext and shares the same queue and MainThreadId
var clonedContext = (FixedThreadContext)originalContext.Clone();
Console.WriteLine(clonedContext.MainThreadId == originalContext.MainThreadId);

// Work posted on the clone enters the same queue and is consumed by the original's background thread
clonedContext.Post((state) => {
    Console.WriteLine("Work on the shared queue");
}, null);

Thread.Sleep(1000);

// Dispose one owner of the shared queue; Dispose/StopExec completes that queue
originalContext.Dispose();
```

### Design Features

1. **Thread Marshaling**: `Post`/`Send` enqueue work onto a blocking queue bound to a fixed context
2. **Async Support**: `Post` returns immediately after enqueue
3. **Sync Support**: `Send` invokes on the target thread, or waits on a `ManualResetEvent`
4. **External Pump**: `ProcessTasks` lets a main loop drain a bounded batch; empty queue does not block
5. **Background Consumer**: `StartExec` starts a blocking worker; `StopExec`/`Dispose` shut it down
6. **Shallow Clone**: `Clone` shares the same queue and main thread ID rather than copying an independent queue

### Considerations

1. **Thread Affinity**: To run work on the `MainThreadId` thread, call `ProcessTasks` on that thread; `StartExec` runs items on a new background thread, not the main thread
2. **Resource Release**: Call `Dispose` or `StopExec` promptly; after completion the queue no longer accepts new items
3. **Exception Handling**: `Send` on the main thread throws callback exceptions; background `Exec` swallows them; `ProcessTasks` rethrows exceptions other than `InvalidOperationException`
4. **Deadlock Avoidance**: Calling `Send` from a non-main thread deadlocks if `StartExec` is not running and nobody calls `ProcessTasks`
5. **Clone Semantics**: Clones share the queue; do not call `StartExec` again on a clone, or two background threads will consume the same queue
6. **Performance**: Watch queue capacity and memory under heavy enqueue; use `ProcessTasks(int)` to cap work per pump
