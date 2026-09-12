# JLGames.Infra Core API Documentation

## Overview

The Core module provides the Infra framework's core interfaces and base types: `ICloneable<T>` for creating a copy of type `T`, and `Callback` for wrapping a delegate with optional bound arguments for deferred invocation (e.g. service completion callbacks).

## Namespace

`JLGames.Infra`

---

## Interfaces

### ICloneable<T>

Supports creating a copy of type `T`. `T` is covariant (`out T`) and is typically the implementing type.

```csharp
public interface ICloneable<out T>
```

**Type Parameters:**
- `T`: Type of the clone result (typically the implementing type)

#### Methods

##### Clone()

```csharp
T Clone();
```

**Description:** Creates a copy of the current instance.

**Return Value:**
- `T`: A new instance; semantics (deep vs shallow) are defined by the implementer

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

## Classes

### Callback

Wraps a delegate with optional bound arguments for deferred invocation (e.g. service completion callbacks).

```csharp
public class Callback
```

#### Delegates

##### Func

```csharp
public delegate void Func(params object[] args);
```

**Description:** Callback delegate signature; receives invocation arguments.

**Parameters:**
- `args` (`object[]`): Arguments passed to `Invoke` or `Apply`

#### Properties

##### IsNone

```csharp
public bool IsNone { get; }
```

**Description:** True when no delegate is bound (`Clear` was called or the constructor received `null`).

#### Constructors

##### Callback(Func func, params object[] args)

```csharp
public Callback(Func func, params object[] args)
```

**Description:** Creates a callback with a delegate and optional arguments for `Invoke`.

**Parameters:**
- `func` (`Func`): Delegate to invoke; may be `null`
- `args` (`object[]`): Bound arguments used by `Invoke`; ignored by `Apply`

#### Methods

##### SetFunc(Func func)

```csharp
public void SetFunc(Func func)
```

**Description:** Replaces the bound delegate.

**Parameters:**
- `func` (`Func`): New delegate; may be `null`

##### SetArgs(params object[] args)

```csharp
public void SetArgs(params object[] args)
```

**Description:** Replaces bound arguments used by `Invoke`.

**Parameters:**
- `args` (`object[]`): New argument array

##### Apply(params object[] args)

```csharp
public void Apply(params object[] args)
```

**Description:** Invokes the delegate with the given arguments (does not use bound args from construction).

**Parameters:**
- `args` (`object[]`): Arguments passed to the delegate

##### Invoke()

```csharp
public void Invoke()
```

**Description:** Invokes the delegate with bound arguments from the constructor or `SetArgs`.

##### Clear()

```csharp
public void Clear()
```

**Description:** Clears the delegate and bound arguments; `IsNone` becomes `true`.

---

## Usage Examples

### ICloneable Interface Usage

```csharp
// Implement ICloneable
public class Person : ICloneable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; } = new List<string>();

    public Person Clone()
    {
        // Deep vs shallow copy is defined by the implementer; this copy clones the list
        var clone = new Person
        {
            Name = this.Name,
            Age = this.Age,
            Hobbies = new List<string>(this.Hobbies)
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

// Original object is not affected (list was deep-copied)
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

// Create callback (bound arguments are used only by Invoke)
var callback = new Callback(MyCallback, "Parameter1", 42, true);

// Check whether a delegate is bound
if (!callback.IsNone)
{
    Console.WriteLine("Callback object is not empty");
}

// Invoke with bound arguments
callback.Invoke();
// Output:
// Callback called, parameter count: 3
// Parameter 0: Parameter1
// Parameter 1: 42
// Parameter 2: True

// Apply uses call-time arguments and ignores bound args
callback.Apply("NewParameter1", "NewParameter2");
// Output:
// Callback called, parameter count: 2
// Parameter 0: NewParameter1
// Parameter 1: NewParameter2
```

### Dynamic Callback Setting

```csharp
// Constructor received null: no delegate is bound
var callback = new Callback(null);

Console.WriteLine(callback.IsNone); // Output: True

// Replace the bound delegate
callback.SetFunc((params object[] args) => {
    Console.WriteLine("Dynamically set callback function");
    foreach (var arg in args)
    {
        Console.WriteLine($"Parameter: {arg}");
    }
});

// Replace bound arguments used by Invoke
callback.SetArgs("DynamicParameter1", "DynamicParameter2");

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

// Clear the delegate and bound arguments
callback.Clear();

Console.WriteLine(callback.IsNone); // Output: True

// Check IsNone before calling; Invoke/Apply after Clear throws NullReferenceException
try
{
    callback.Invoke();
}
catch (NullReferenceException)
{
    Console.WriteLine("Callback has been cleared, cannot invoke");
}
```

### Application in Event System

```csharp
public class EventSystem
{
    private Dictionary<string, Callback> events = new Dictionary<string, Callback>();

    public void RegisterEvent(string eventName, Callback.Func handler, params object[] defaultArgs)
    {
        events[eventName] = new Callback(handler, defaultArgs);
    }

    public void TriggerEvent(string eventName, params object[] args)
    {
        if (events.TryGetValue(eventName, out var callback) && !callback.IsNone)
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

var eventSystem = new EventSystem();

eventSystem.RegisterEvent("userLogin", (params object[] args) => {
    Console.WriteLine($"User login event: {args[0]}");
}, "Default User");

eventSystem.TriggerEvent("userLogin", "Zhang San");
// Output: User login event: Zhang San

eventSystem.TriggerEvent("userLogin");
// Output: User login event: Default User
```

---

## Notes

1. **ICloneable interface:**
   - Uses covariant generic parameter `out T`, which supports upcasting
   - Clone semantics (deep vs shallow) are defined by the implementer
   - Implementers that need independent copies of reference-type members should deep-copy them

2. **Callback class:**
   - Use `SetFunc` / `SetArgs` to replace the bound delegate and arguments
   - Use `IsNone` to check whether a delegate is bound
   - `Apply` uses call-time arguments and ignores bound args; `Invoke` uses arguments from the constructor or `SetArgs`
   - After `Clear`, `IsNone` is `true`; calling `Invoke`/`Apply` without checking throws

3. **Performance considerations:**
   - Pay attention to memory management when using many callbacks
   - Avoid time-consuming operations in callbacks
   - Call `Clear` on unused callbacks promptly

4. **Thread safety:**
   - Current implementation is not thread-safe
   - Additional synchronization is needed in multi-threaded environments

---

## Dependencies

- `System`: Basic types
- `System.Collections.Generic`: Generic collections (used in examples)
