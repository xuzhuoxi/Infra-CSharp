# Threadx API Documentation

## Namespace: JLGames.Infra.Threadx

### Classes

#### FixedThreadContext
Fixed thread context class

```csharp
/// <summary>
/// Fixed thread context class
/// Provides thread-safe synchronization context, ensuring tasks are executed on the specified thread
/// </summary>
public class FixedThreadContext : SynchronizationContext, IDisposable, ICloneable<SynchronizationContext>
{
    private const int KAwqInitialCapacity = 20;
    private readonly BlockingCollection<ContextWorkRequest> m_AsyncWorkQueue;
    private readonly int m_MainThreadId;
    private int m_ExecFlag = 0;

    /// <summary>
    /// Main thread ID
    /// </summary>
    public int MainThreadId => m_MainThreadId;

    /// <summary>
    /// Number of pending tasks
    /// </summary>
    public int PendingCount => m_AsyncWorkQueue.Count;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mainThreadId">Main thread ID</param>
    public FixedThreadContext(int mainThreadId);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="queue">Work queue</param>
    /// <param name="mainThreadId">Main thread ID</param>
    public FixedThreadContext(BlockingCollection<ContextWorkRequest> queue, int mainThreadId);

    /// <summary>
    /// Post will add the call to a task list to be executed later on the main thread then work will continue asynchronously
    /// Asynchronously submit tasks to the queue, to be executed later on the main thread
    /// </summary>
    /// <param name="callback">Callback function</param>
    /// <param name="state">State object</param>
    public override void Post(SendOrPostCallback callback, object state);

    /// <summary>
    /// Send will process the call synchronously. If the call is processed on the main thread, we'll invoke it
    /// directly here. If the call is processed on another thread it will be queued up like POST to be executed
    /// on the main thread and it will wait. Once the main thread processes the work we can continue
    /// Synchronously execute tasks, if on the main thread then execute directly, otherwise queue and wait for main thread processing
    /// </summary>
    /// <param name="callback">Callback function</param>
    /// <param name="state">State object</param>
    public override void Send(SendOrPostCallback callback, object state);

    /// <summary>
    /// Dispose resources
    /// </summary>
    public void Dispose();

    /// <summary>
    /// Clone synchronization context
    /// </summary>
    /// <returns>New synchronization context</returns>
    public SynchronizationContext Clone();

    /// <summary>
    /// Start executing tasks
    /// </summary>
    public void StartExec();

    /// <summary>
    /// Exec will execute tasks off the task list
    /// Execute tasks in the task queue
    /// </summary>
    private void Exec();
}
```

### Structs

#### ContextWorkRequest
Context work request structure

```csharp
/// <summary>
/// Context work request structure
/// Encapsulates work tasks to be executed and synchronization objects
/// </summary>
public readonly struct ContextWorkRequest
{
    private readonly SendOrPostCallback m_DelegateCallback;
    private readonly object m_DelegateState;
    private readonly ManualResetEvent m_WaitHandle;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="callback">Callback function</param>
    /// <param name="state">State object</param>
    /// <param name="waitHandle">Wait handle</param>
    public ContextWorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null);

    /// <summary>
    /// Execute task
    /// </summary>
    public void Invoke();
}
```

### Function Description

#### Thread Context Features

**Core Features**
- **Thread Safety:** Ensures tasks are executed on the specified thread
- **Async Support:** Supports asynchronous task submission and execution
- **Sync Support:** Supports synchronous task execution and waiting
- **Queue Management:** Uses blocking queue to manage pending tasks

**Execution Modes**
1. **Post Mode:** Asynchronously submit tasks without waiting for completion
2. **Send Mode:** Synchronously submit tasks and wait for completion
3. **Queue Execution:** Background thread takes tasks from queue for execution

#### Thread Synchronization Mechanism

**FixedThreadContext**
- **Main Thread ID:** Identifies main thread through MainThreadId
- **Task Queue:** Uses BlockingCollection to manage tasks
- **Execution Flag:** Controls execution state through ExecFlag
- **Resource Management:** Implements IDisposable interface

**ContextWorkRequest**
- **Task Encapsulation:** Encapsulates callback functions and state objects
- **Sync Support:** Implements synchronization through ManualResetEvent
- **Exception Handling:** Ensures wait handle is properly set

### Usage Examples

#### Basic Usage
```csharp
// Create fixed thread context
var threadContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

// Start executing tasks
threadContext.StartExec();

// Asynchronously submit tasks
threadContext.Post((state) => {
    Console.WriteLine($"Async task executed: {state}");
    Console.WriteLine($"Current thread ID: {Thread.CurrentThread.ManagedThreadId}");
}, "Async data");

// Synchronously submit tasks
threadContext.Send((state) => {
    Console.WriteLine($"Sync task executed: {state}");
    Console.WriteLine($"Current thread ID: {Thread.CurrentThread.ManagedThreadId}");
}, "Sync data");

// Check number of pending tasks
Console.WriteLine($"Pending task count: {threadContext.PendingCount}");

// Wait for tasks to execute
Thread.Sleep(1000);

// Dispose resources
threadContext.Dispose();
```

#### UI Thread Synchronization
```csharp
// In WPF/WinForms applications
public class MainWindow
{
    private FixedThreadContext uiContext;

    public MainWindow()
    {
        // Create UI thread context
        uiContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
        uiContext.StartExec();
    }

    private void StartBackgroundWork()
    {
        // Execute time-consuming operations in background thread
        Task.Run(() => {
            // Simulate time-consuming operation
            Thread.Sleep(2000);

            // Update UI (asynchronously)
            uiContext.Post((state) => {
```
                UpdateUI((string)state);
            }, "Background task completed");

            // Update UI (synchronously)
            uiContext.Send((state) => {
                ShowMessage((string)state);
            }, "Sync update completed");
        });
    }

    private void UpdateUI(string message)
    {
        // Execute on UI thread
        Console.WriteLine($"UI update: {message}");
    }

    private void ShowMessage(string message)
    {
        // Execute on UI thread
        Console.WriteLine($"Show message: {message}");
    }

    public void Dispose()
    {
        uiContext?.Dispose();
    }
}
```

#### Multi-threaded Collaboration
```csharp
// Create multiple thread contexts
var context1 = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
var context2 = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);

context1.StartExec();
context2.StartExec();

// Thread 1 submits task to thread 2
Task.Run(() => {
    context2.Post((state) => {
        Console.WriteLine($"Thread 2 executing task: {state}");
    }, "Task from thread 1");
});

// Thread 2 submits task to thread 1
Task.Run(() => {
    context1.Send((state) => {
        Console.WriteLine($"Thread 1 executing sync task: {state}");
    }, "Sync task from thread 2");
});

// Wait for tasks to complete
Thread.Sleep(2000);

// Clean up resources
context1.Dispose();
context2.Dispose();
```

#### Task Queue Management
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
context.StartExec();

// Submit multiple tasks
for (int i = 0; i < 10; i++)
{
    int taskId = i;
    context.Post((state) => {
        Console.WriteLine($"Executing task {taskId}");
        Thread.Sleep(100); // Simulate work
    }, null);
}

// Monitor task queue
while (context.PendingCount > 0)
{
    Console.WriteLine($"Remaining tasks: {context.PendingCount}");
    Thread.Sleep(100);
}

Console.WriteLine("All tasks completed");
context.Dispose();
```

#### Exception Handling
```csharp
var context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
context.StartExec();

// Submit task that may throw exception
context.Post((state) => {
    try
    {
        // Simulate code that may fail
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

// Submit sync task and handle exception
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

Thread.Sleep(1000);
context.Dispose();
```

#### Cloning and Copying
```csharp
var originalContext = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
originalContext.StartExec();

// Clone context
var clonedContext = originalContext.Clone();
clonedContext.StartExec();

// Submit tasks in both contexts
originalContext.Post((state) => {
    Console.WriteLine("Original context task");
}, null);

clonedContext.Post((state) => {
    Console.WriteLine("Cloned context task");
}, null);

Thread.Sleep(1000);

// Clean up resources
originalContext.Dispose();
clonedContext.Dispose();
```

### Design Features

1. **Thread Safety**: Ensures tasks are executed on the specified thread
2. **Async Support**: Supports asynchronous task submission
3. **Sync Support**: Supports synchronous task execution and waiting
4. **Resource Management**: Implements IDisposable interface
5. **Queue Management**: Uses blocking queue to manage tasks
6. **Clone Support**: Supports context cloning

### Considerations

1. **Thread ID**: Ensure MainThreadId is set correctly
2. **Resource Release**: Call Dispose method in time
3. **Exception Handling**: Properly handle exceptions during task execution
4. **Deadlock Avoidance**: Avoid calling Send again in Send callback
5. **Performance Considerations**: Pay attention to memory usage with large numbers of tasks
6. **Thread Safety**: Pay attention to synchronization when accessed by multiple threads 