# JLGames.Infra Core API Documentation

## Overview

The Core module provides the core interfaces and base classes of the Infra framework, including cloning interfaces and callback mechanisms.

## Namespace

`JLGames.Infra`

---

## 接口

### ICloneable<T>

Generic cloning interface that defines basic operations for object cloning.

```csharp
public interface ICloneable<out T>
```

#### Methods

##### Clone()

```csharp
T Clone();
```

**Description:** Clone object

**Return Value:**
- `T`: Cloned object

**Example:**
```csharp
public class MyClass : ICloneable<MyClass>
{
    public string Name { get; set; }
    public int Value { get; set; }

    public MyClass Clone()
    {
        return new MyClass
        {
            Name = this.Name,
            Value = this.Value
        };
    }
}

// Usage
var original = new MyClass { Name = "Test", Value = 42 };
var cloned = original.Clone();
```

---

## 类

### Callback

Generic callback context class that provides encapsulation and management of function callbacks.

```csharp
public class Callback
```

#### Delegates

##### Func

```csharp
public delegate void Func(params object[] args);
```

**Description:** Callback function delegate

**Parameters:**
- `args` (object[]): Variable parameter array

#### Fields

##### m_Func

```csharp
private Func m_Func;
```

**Description:** Callback function

##### m_Args

```csharp
private object[] m_Args;
```

**Description:** Callback parameters

#### Properties

##### IsNone

```csharp
public bool IsNone => m_Func == null;
```

**Description:** Whether it's an empty callback

#### Constructors

##### Callback(Func func, params object[] args)

```csharp
public Callback(Func func, params object[] args)
```

**Description:** Create callback object

**Parameters:**
- `func` (Func): Callback function
- `args` (object[]): Callback parameters

#### Methods

##### SetFunc(Func func)

```csharp
public void SetFunc(Func func)
```

**Description:** Set callback function

**Parameters:**
- `func` (Func): Callback function

##### SetArgs(params object[] args)

```csharp
public void SetArgs(params object[] args)
```

**Description:** Set callback parameters

**Parameters:**
- `args` (object[]): Callback parameters

##### Apply(params object[] args)

```csharp
public void Apply(params object[] args)
```

**Description:** Apply callback function using passed parameters

**Parameters:**
- `args` (object[]): Parameters to use

##### Invoke()

```csharp
public void Invoke()
```

**Description:** Invoke callback function using stored parameters

##### Clear()

```csharp
public void Clear()
```

**Description:** Clear callback function and parameters

---

## Usage Examples

### ICloneable Interface Usage

```csharp
// Implement ICloneable interface
public class Person : ICloneable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; } = new List<string>();

    public Person Clone()
    {
        var clone = new Person
        {
            Name = this.Name,
            Age = this.Age,
            Hobbies = new List<string>(this.Hobbies) // Deep copy list
        };
        return clone;
    }
}

// Use cloning
var original = new Person
{
    Name = "Zhang San",
    Age = 25,
    Hobbies = { "Reading", "Swimming" }
};

var cloned = original.Clone();
cloned.Name = "Li Si";
cloned.Hobbies.Add("Running");

// Original object is not affected
Console.WriteLine(original.Name); // Output: Zhang San
Console.WriteLine(original.Hobbies.Count); // Output: 2
```

### Callback Class Usage

```csharp
// Define callback function
void MyCallback(params object[] args)
{
    Console.WriteLine($"Callback called, parameter count: {args.Length}");
    for (int i = 0; i < args.Length; i++)
    {
        Console.WriteLine($"Parameter {i}: {args[i]}");
    }
}

// Create callback object
var callback = new Callback(MyCallback, "Parameter1", 42, true);

// Check if empty
if (!callback.IsNone)
{
    Console.WriteLine("Callback object is not empty");
}

// Invoke callback
callback.Invoke();
// Output:
// Callback called, parameter count: 3
// Parameter 0: Parameter1
// Parameter 1: 42
// Parameter 2: True

// Use Apply method to pass new parameters
callback.Apply("NewParameter1", "NewParameter2");
// Output:
// Callback called, parameter count: 2
// Parameter 0: NewParameter1
// Parameter 1: NewParameter2
```

### Dynamic Callback Setting

```csharp
// Create empty callback
var callback = new Callback(null);

// Check if empty
Console.WriteLine(callback.IsNone); // Output: True

// Set callback function
callback.SetFunc((params object[] args) => {
    Console.WriteLine("Dynamically set callback function");
    foreach (var arg in args)
    {
        Console.WriteLine($"Parameter: {arg}");
    }
});

// Set parameters
callback.SetArgs("DynamicParameter1", "DynamicParameter2");

// Invoke callback
callback.Invoke();
// Output:
// Dynamically set callback function
// Parameter: DynamicParameter1
// Parameter: DynamicParameter2
```

### Clear Callback

```csharp
var callback = new Callback((params object[] args) => {
    Console.WriteLine("This is a callback function");
});

Console.WriteLine(callback.IsNone); // Output: False

// Clear callback
callback.Clear();

Console.WriteLine(callback.IsNone); // Output: True

// Try to invoke cleared callback
try
{
    callback.Invoke(); // Will throw NullReferenceException
}
catch (NullReferenceException)
{
    Console.WriteLine("Callback has been cleared, cannot invoke");
}
```

### Application in Event System

```csharp
// Use Callback in event system
public class EventSystem
{
    private Dictionary<string, Callback> events = new Dictionary<string, Callback>();

    public void RegisterEvent(string eventName, Callback.Func handler, params object[] defaultArgs)
    {
        events[eventName] = new Callback(handler, defaultArgs);
    }

    public void TriggerEvent(string eventName, params object[] args)
    {
        if (events.TryGetValue(eventName, out var callback))
        {
            if (args.Length > 0)
            {
                callback.Apply(args);
            }
            else
            {
                callback.Invoke();
            }
        }
    }
}

// Usage example
var eventSystem = new EventSystem();

// Register event
eventSystem.RegisterEvent("userLogin", (params object[] args) => {
    Console.WriteLine($"User login event: {args[0]}");
}, "Default User");

// Trigger event
eventSystem.TriggerEvent("userLogin", "Zhang San");
// Output: User login event: Zhang San

eventSystem.TriggerEvent("userLogin");
// Output: User login event: Default User
```

---

## Notes

1. **ICloneable interface:**
   - Uses covariant generic parameter `out T`, supports upcasting
   - Recommend implementing deep copy to avoid reference type sharing
   - Clone operation should create completely independent object copies

2. **Callback class:**
   - Supports dynamic setting of callbacks and parameters
   - Use `IsNone` property to check if callback is valid
   - Check if callback is empty before invoking
   - `Apply` method uses passed parameters, `Invoke` method uses stored parameters
   - Invoking after clearing callback will throw exception

3. **Performance considerations:**
   - Pay attention to memory management when using many callbacks
   - Avoid time-consuming operations in callbacks
   - Clear unused callback objects promptly

4. **Thread safety:**
   - Current implementation is not thread-safe
   - Additional synchronization mechanisms needed in multi-threaded environments

---

## Dependencies

- `System`: Basic types
- `System.Collections.Generic`: Generic collections (used in examples) 