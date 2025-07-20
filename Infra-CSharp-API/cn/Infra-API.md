# JLGames.Infra Core API 文档

## 概述

Core模块提供了Infra框架的核心接口和基础类，包括克隆接口和回调机制。

## 命名空间

`JLGames.Infra`

---

## 接口

### ICloneable<T>

泛型克隆接口，定义了对象克隆的基本操作。

```csharp
public interface ICloneable<out T>
```

#### 方法

##### Clone()

```csharp
T Clone();
```

**描述：** 克隆对象

**返回值：**
- `T`: 克隆后的对象

**示例：**
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

// 使用
var original = new MyClass { Name = "Test", Value = 42 };
var cloned = original.Clone();
```

---

## 类

### Callback

通用回调上下文类，提供函数回调的封装和管理。

```csharp
public class Callback
```

#### 委托

##### Func

```csharp
public delegate void Func(params object[] args);
```

**描述：** 回调函数委托

**参数：**
- `args` (object[]): 可变参数数组

#### 字段

##### m_Func

```csharp
private Func m_Func;
```

**描述：** 回调函数

##### m_Args

```csharp
private object[] m_Args;
```

**描述：** 回调参数

#### 属性

##### IsNone

```csharp
public bool IsNone => m_Func == null;
```

**描述：** 是否为空回调

#### 构造函数

##### Callback(Func func, params object[] args)

```csharp
public Callback(Func func, params object[] args)
```

**描述：** 创建回调对象

**参数：**
- `func` (Func): 回调函数
- `args` (object[]): 回调参数

#### 方法

##### SetFunc(Func func)

```csharp
public void SetFunc(Func func)
```

**描述：** 设置回调函数

**参数：**
- `func` (Func): 回调函数

##### SetArgs(params object[] args)

```csharp
public void SetArgs(params object[] args)
```

**描述：** 设置回调参数

**参数：**
- `args` (object[]): 回调参数

##### Apply(params object[] args)

```csharp
public void Apply(params object[] args)
```

**描述：** 应用回调函数，使用传入的参数

**参数：**
- `args` (object[]): 要使用的参数

##### Invoke()

```csharp
public void Invoke()
```

**描述：** 调用回调函数，使用存储的参数

##### Clear()

```csharp
public void Clear()
```

**描述：** 清除回调函数和参数

---

## 使用示例

### ICloneable接口使用

```csharp
// 实现ICloneable接口
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
            Hobbies = new List<string>(this.Hobbies) // 深拷贝列表
        };
        return clone;
    }
}

// 使用克隆
var original = new Person
{
    Name = "张三",
    Age = 25,
    Hobbies = { "读书", "游泳" }
};

var cloned = original.Clone();
cloned.Name = "李四";
cloned.Hobbies.Add("跑步");

// 原始对象不受影响
Console.WriteLine(original.Name); // 输出: 张三
Console.WriteLine(original.Hobbies.Count); // 输出: 2
```

### Callback类使用

```csharp
// 定义回调函数
void MyCallback(params object[] args)
{
    Console.WriteLine($"回调被调用，参数数量: {args.Length}");
    for (int i = 0; i < args.Length; i++)
    {
        Console.WriteLine($"参数 {i}: {args[i]}");
    }
}

// 创建回调对象
var callback = new Callback(MyCallback, "参数1", 42, true);

// 检查是否为空
if (!callback.IsNone)
{
    Console.WriteLine("回调对象不为空");
}

// 调用回调
callback.Invoke();
// 输出:
// 回调被调用，参数数量: 3
// 参数 0: 参数1
// 参数 1: 42
// 参数 2: True

// 使用Apply方法传入新参数
callback.Apply("新参数1", "新参数2");
// 输出:
// 回调被调用，参数数量: 2
// 参数 0: 新参数1
// 参数 1: 新参数2
```

### 动态设置回调

```csharp
// 创建空回调
var callback = new Callback(null);

// 检查是否为空
Console.WriteLine(callback.IsNone); // 输出: True

// 设置回调函数
callback.SetFunc((params object[] args) => {
    Console.WriteLine("动态设置的回调函数");
    foreach (var arg in args)
    {
        Console.WriteLine($"参数: {arg}");
    }
});

// 设置参数
callback.SetArgs("动态参数1", "动态参数2");

// 调用回调
callback.Invoke();
// 输出:
// 动态设置的回调函数
// 参数: 动态参数1
// 参数: 动态参数2
```

### 清除回调

```csharp
var callback = new Callback((params object[] args) => {
    Console.WriteLine("这是一个回调函数");
});

Console.WriteLine(callback.IsNone); // 输出: False

// 清除回调
callback.Clear();

Console.WriteLine(callback.IsNone); // 输出: True

// 尝试调用已清除的回调
try
{
    callback.Invoke(); // 会抛出NullReferenceException
}
catch (NullReferenceException)
{
    Console.WriteLine("回调已被清除，无法调用");
}
```

### 在事件系统中的应用

```csharp
// 在事件系统中使用Callback
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

// 使用示例
var eventSystem = new EventSystem();

// 注册事件
eventSystem.RegisterEvent("userLogin", (params object[] args) => {
    Console.WriteLine($"用户登录事件: {args[0]}");
}, "默认用户");

// 触发事件
eventSystem.TriggerEvent("userLogin", "张三");
// 输出: 用户登录事件: 张三

eventSystem.TriggerEvent("userLogin");
// 输出: 用户登录事件: 默认用户
```

---

## 注意事项

1. **ICloneable接口：**
   - 使用协变泛型参数`out T`，支持向上转型
   - 建议实现深拷贝以避免引用类型共享
   - 克隆操作应该创建完全独立的对象副本

2. **Callback类：**
   - 支持动态设置回调和参数
   - 使用`IsNone`属性检查回调是否有效
   - 调用前应检查回调是否为空
   - `Apply`方法使用传入的参数，`Invoke`方法使用存储的参数
   - 清除回调后再次调用会抛出异常

3. **性能考虑：**
   - 大量使用回调时注意内存管理
   - 避免在回调中执行耗时操作
   - 及时清除不再使用的回调对象

4. **线程安全：**
   - 当前实现不是线程安全的
   - 多线程环境下需要额外的同步机制

---

## 依赖关系

- `System`: 基础类型
- `System.Collections.Generic`: 泛型集合（在示例中使用） 