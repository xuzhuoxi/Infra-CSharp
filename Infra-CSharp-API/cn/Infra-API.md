# JLGames.Infra Core API 文档

## 概述

Core 模块提供 Infra 框架的核心接口与基础类型：`ICloneable<T>` 用于创建指定类型的副本，`Callback` 用于封装委托及可选绑定参数，供延迟调用（如服务完成回调）。

## 命名空间

`JLGames.Infra`

---

## 接口

### ICloneable<T>

支持创建 `T` 类型副本的泛型克隆接口。`T` 为协变（`out T`），通常为实现类型本身。

```csharp
public interface ICloneable<out T>
```

**类型参数：**
- `T`: 克隆结果的类型（通常为实现类型本身）

#### 方法

##### Clone()

```csharp
T Clone();
```

**描述：** 创建当前实例的副本。

**返回值：**
- `T`: 新实例；深拷贝或浅拷贝由实现方定义

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

封装委托及可选绑定参数，供延迟调用（如服务完成回调）。

```csharp
public class Callback
```

#### 委托

##### Func

```csharp
public delegate void Func(params object[] args);
```

**描述：** 回调委托签名；接收调用时传入的参数。

**参数：**
- `args` (`object[]`): 传给 `Invoke` 或 `Apply` 的参数

#### 属性

##### IsNone

```csharp
public bool IsNone { get; }
```

**描述：** 未绑定委托时为 `true`（已调用 `Clear`，或构造时传入 `null`）。

#### 构造函数

##### Callback(Func func, params object[] args)

```csharp
public Callback(Func func, params object[] args)
```

**描述：** 创建回调，绑定委托及供 `Invoke` 使用的可选参数。

**参数：**
- `func` (`Func`): 要调用的委托；可为 `null`
- `args` (`object[]`): `Invoke` 使用的绑定参数；`Apply` 不使用

#### 方法

##### SetFunc(Func func)

```csharp
public void SetFunc(Func func)
```

**描述：** 替换已绑定的委托。

**参数：**
- `func` (`Func`): 新委托；可为 `null`

##### SetArgs(params object[] args)

```csharp
public void SetArgs(params object[] args)
```

**描述：** 替换 `Invoke` 使用的绑定参数。

**参数：**
- `args` (`object[]`): 新的参数数组

##### Apply(params object[] args)

```csharp
public void Apply(params object[] args)
```

**描述：** 使用传入参数调用委托（不使用构造时绑定的参数）。

**参数：**
- `args` (`object[]`): 传给委托的参数

##### Invoke()

```csharp
public void Invoke()
```

**描述：** 使用构造或 `SetArgs` 绑定的参数调用委托。

##### Clear()

```csharp
public void Clear()
```

**描述：** 清除委托与绑定参数；此后 `IsNone` 为 `true`。

---

## 使用示例

### ICloneable 接口使用

```csharp
// 实现 ICloneable 接口
public class Person : ICloneable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; } = new List<string>();

    public Person Clone()
    {
        // 深拷贝或浅拷贝由实现方定义；此处对列表做独立副本
        var clone = new Person
        {
            Name = this.Name,
            Age = this.Age,
            Hobbies = new List<string>(this.Hobbies)
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

// 原始对象不受影响（因实现为深拷贝列表）
Console.WriteLine(original.Name); // 输出: 张三
Console.WriteLine(original.Hobbies.Count); // 输出: 2
```

### Callback 类使用

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

// 创建回调对象（绑定参数仅供 Invoke 使用）
var callback = new Callback(MyCallback, "参数1", 42, true);

// 检查是否已绑定委托
if (!callback.IsNone)
{
    Console.WriteLine("回调对象不为空");
}

// 使用绑定参数调用
callback.Invoke();
// 输出:
// 回调被调用，参数数量: 3
// 参数 0: 参数1
// 参数 1: 42
// 参数 2: True

// 使用 Apply 传入调用时参数（忽略绑定参数）
callback.Apply("新参数1", "新参数2");
// 输出:
// 回调被调用，参数数量: 2
// 参数 0: 新参数1
// 参数 1: 新参数2
```

### 动态设置回调

```csharp
// 构造时传入 null，未绑定委托
var callback = new Callback(null);

Console.WriteLine(callback.IsNone); // 输出: True

// 替换委托
callback.SetFunc((params object[] args) => {
    Console.WriteLine("动态设置的回调函数");
    foreach (var arg in args)
    {
        Console.WriteLine($"参数: {arg}");
    }
});

// 替换 Invoke 使用的绑定参数
callback.SetArgs("动态参数1", "动态参数2");

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

// 清除委托与绑定参数
callback.Clear();

Console.WriteLine(callback.IsNone); // 输出: True

// 调用前应检查 IsNone；已清除后再 Invoke/Apply 会抛出 NullReferenceException
try
{
    callback.Invoke();
}
catch (NullReferenceException)
{
    Console.WriteLine("回调已被清除，无法调用");
}
```

### 在事件系统中的应用

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
    Console.WriteLine($"用户登录事件: {args[0]}");
}, "默认用户");

eventSystem.TriggerEvent("userLogin", "张三");
// 输出: 用户登录事件: 张三

eventSystem.TriggerEvent("userLogin");
// 输出: 用户登录事件: 默认用户
```

---

## 注意事项

1. **ICloneable 接口：**
   - 使用协变泛型参数 `out T`，支持向上转型
   - `Clone` 的语义（深拷贝或浅拷贝）由实现方定义
   - 若需避免引用类型共享，实现方应自行处理深拷贝

2. **Callback 类：**
   - 可用 `SetFunc` / `SetArgs` 动态替换委托与绑定参数
   - 使用 `IsNone` 检查是否已绑定委托
   - `Apply` 使用调用时传入的参数，不使用绑定参数；`Invoke` 使用构造或 `SetArgs` 绑定的参数
   - `Clear` 之后 `IsNone` 为 `true`；未检查直接 `Invoke`/`Apply` 会抛出异常

3. **性能考虑：**
   - 大量使用回调时注意内存管理
   - 避免在回调中执行耗时操作
   - 及时 `Clear` 不再使用的回调

4. **线程安全：**
   - 当前实现不是线程安全的
   - 多线程环境下需要额外的同步机制

---

## 依赖关系

- `System`: 基础类型
- `System.Collections.Generic`: 泛型集合（在示例中使用）
