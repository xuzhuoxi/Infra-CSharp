# JLGames.Infra.Xml API 文档

## 概述

Xml 模块提供基于 `XmlSerializer` 的 XML 序列化与反序列化工具，支持对象与 XML 字符串之间的相互转换。

## 命名空间

`JLGames.Infra.Xml`

---

## 工具类

### XmlUtils

基于 `XmlSerializer` 的 XML 序列化与反序列化工具。

```csharp
public static class XmlUtils
```

#### 静态方法

##### ToXml(object obj)

```csharp
public static string ToXml(object obj)
```

**描述：** 将对象序列化为 XML 字符串。

**参数：**
- `obj` (object): 待序列化的对象；为 null 时返回空字符串。

**返回值：**
- `string`: XML 文本；`obj` 为 null 时为空字符串。

**示例：**
```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

var person = new Person { Name = "张三", Age = 25 };
string xml = XmlUtils.ToXml(person);
// 结果: <?xml version="1.0" encoding="utf-16"?>
//       <Person xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
//         <Name>张三</Name>
//         <Age>25</Age>
//       </Person>

string empty = XmlUtils.ToXml(null); // ""
```

##### FromXml<T>(string xml)

```csharp
public static T FromXml<T>(string xml)
```

**描述：** 将 XML 字符串反序列化为 `T` 类型实例。

**参数：**
- `xml` (string): XML 文本；为 null 或空时返回默认值。

**类型参数：**
- `T`: 目标类型（须为可实例化的可序列化类型）。

**返回值：**
- `T`: 反序列化结果；`xml` 为 null 或空时为 `default(T)`。

**示例：**
```csharp
string xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Name>张三</Name>
  <Age>25</Age>
</Person>";

var person = XmlUtils.FromXml<Person>(xml);
// 结果: person.Name = "张三", person.Age = 25

Person missing = XmlUtils.FromXml<Person>(null); // null
```

##### FromXml(string xml, System.Type type)

```csharp
public static object FromXml(string xml, System.Type type)
```

**描述：** 将 XML 字符串反序列化为指定类型的实例。

**参数：**
- `xml` (string): XML 文本；为 null 或空时返回 null。
- `type` (System.Type): 目标类型（须为可实例化的可序列化类型）。

**返回值：**
- `object`: 反序列化结果；`xml` 为 null 或空时为 null。

**异常：**
- `ArgumentNullException`: `type` 为 null。
- `ArgumentException`: `type` 为抽象类型，无法实例化。

**示例：**
```csharp
string xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Name>张三</Name>
  <Age>25</Age>
</Person>";

var person = XmlUtils.FromXml(xml, typeof(Person)) as Person;
// 结果: person.Name = "张三", person.Age = 25

object missing = XmlUtils.FromXml("", typeof(Person)); // null
```

---

## 使用示例

### 基本序列化和反序列化

```csharp
// 定义数据类（须有无参构造函数；序列化公共属性/字段）
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
}

// 创建对象
var user = new User
{
    Name = "李四",
    Email = "lisi@example.com",
    CreatedDate = DateTime.Now,
    Tags = new List<string> { "VIP", "Active" }
};

// 序列化为 XML
string xmlString = XmlUtils.ToXml(user);
Console.WriteLine(xmlString);

// 反序列化回对象
var deserializedUser = XmlUtils.FromXml<User>(xmlString);
Console.WriteLine($"Name: {deserializedUser.Name}");
Console.WriteLine($"Email: {deserializedUser.Email}");
```

### 复杂对象序列化

```csharp
public class Order
{
    public int OrderId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal TotalAmount { get; set; }
}

public class Customer
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}

public class OrderItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

// 创建复杂对象
var order = new Order
{
    OrderId = 1001,
    Customer = new Customer
    {
        Name = "王五",
        Address = "北京市朝阳区",
        Phone = "13800138000"
    },
    Items = new List<OrderItem>
    {
        new OrderItem { ProductName = "笔记本电脑", Quantity = 1, UnitPrice = 5999.00m },
        new OrderItem { ProductName = "鼠标", Quantity = 2, UnitPrice = 99.00m }
    },
    TotalAmount = 6197.00m
};

// 序列化
string orderXml = XmlUtils.ToXml(order);

// 反序列化
var deserializedOrder = XmlUtils.FromXml<Order>(orderXml);
```

### 错误处理

```csharp
try
{
    // 尝试反序列化无效的 XML
    string invalidXml = "<Invalid>XML</Invalid>";
    var result = XmlUtils.FromXml<User>(invalidXml);
}
catch (Exception ex)
{
    Console.WriteLine($"反序列化失败: {ex.Message}");
}

try
{
    // 抽象类型无法实例化
    object result = XmlUtils.FromXml("<Root />", typeof(Stream));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"类型无效: {ex.Message}");
}

try
{
    // 匿名类型不受 XmlSerializer 支持
    var anonymous = new { Name = "Test" };
    string xml = XmlUtils.ToXml(anonymous);
}
catch (Exception ex)
{
    Console.WriteLine($"序列化失败: {ex.Message}");
}
```

---

## 注意事项

1. **实现方式：** 基于 `System.Xml.Serialization.XmlSerializer`。目标类型须为可实例化的可序列化类型。
2. **公共成员：** 序列化公共属性与公共字段；私有成员不会被序列化。
3. **无参构造函数：** 反序列化的类必须有无参构造函数。
4. **空值行为：** `ToXml(null)` 返回空字符串；`FromXml` 在 XML 为 null 或空时返回 `null` / `default(T)`，不抛异常。
5. **抽象类型：** 不能反序列化到抽象类型；`type` 为 null 时抛出 `ArgumentNullException`，为抽象类型时抛出 `ArgumentException`。
6. **异常处理：** 序列化/反序列化失败时会先将异常写入控制台，再重新抛出，建议使用 try-catch 处理。
7. **性能考虑：** 每次调用都会新建 `XmlSerializer` 实例；大量数据或高频调用时需注意开销。
8. **编码：** 通过 `StringWriter` 输出，XML 声明默认为 UTF-16。

---

## 依赖关系

- `System`: 基础类型
- `System.IO`: 字符串读写
- `System.Text`: 字符串构建
- `System.Xml.Serialization`: XML 序列化功能
