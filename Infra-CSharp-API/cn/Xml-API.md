# JLGames.Infra.Xml API 文档

## 概述

Xml模块提供了XML序列化和反序列化的工具类，支持对象与XML字符串之间的相互转换。

## 命名空间

`JLGames.Infra.Xml`

---

## 工具类

### XmlUtils

XML工具类，提供对象与XML字符串之间的序列化和反序列化功能。

```csharp
public class XmlUtils
```

#### 静态方法

##### ToXml(object obj)

```csharp
public static string ToXml(object obj)
```

**描述：** Serialize to xml string / 序列化为xml字符串

**参数：**
- `obj` (object): 要序列化的对象

**返回值：**
- `string`: 序列化后的XML字符串

**异常：**
- `Exception`: 序列化过程中可能抛出的异常

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
```

##### FromXml<T>(string xml)

```csharp
public static T FromXml<T>(string xml)
```

**描述：** Deserialize from xml string to object / 从xml字符串反序列化为对象

**参数：**
- `xml` (string): XML字符串

**类型参数：**
- `T`: 目标类型

**返回值：**
- `T`: 反序列化后的对象

**异常：**
- `Exception`: 反序列化过程中可能抛出的异常

**示例：**
```csharp
string xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Name>张三</Name>
  <Age>25</Age>
</Person>";

var person = XmlUtils.FromXml<Person>(xml);
// 结果: person.Name = "张三", person.Age = 25
```

##### FromXml(string xml, System.Type type)

```csharp
public static object FromXml(string xml, System.Type type)
```

**描述：** Deserialize from xml string to object / 从xml字符串反序列化为对象

**参数：**
- `xml` (string): XML字符串
- `type` (System.Type): 目标类型

**返回值：**
- `object`: 反序列化后的对象

**异常：**
- `ArgumentNullException`: 当type参数为null时抛出
- `ArgumentException`: 当type为抽象类型时抛出
- `Exception`: 反序列化过程中可能抛出的异常

**示例：**
```csharp
string xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Name>张三</Name>
  <Age>25</Age>
</Person>";

var person = XmlUtils.FromXml(xml, typeof(Person)) as Person;
// 结果: person.Name = "张三", person.Age = 25
```

---

## 使用示例

### 基本序列化和反序列化

```csharp
// 定义数据类
[Serializable]
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

// 序列化为XML
string xmlString = XmlUtils.ToXml(user);
Console.WriteLine(xmlString);

// 反序列化回对象
var deserializedUser = XmlUtils.FromXml<User>(xmlString);
Console.WriteLine($"Name: {deserializedUser.Name}");
Console.WriteLine($"Email: {deserializedUser.Email}");
```

### 复杂对象序列化

```csharp
[Serializable]
public class Order
{
    public int OrderId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal TotalAmount { get; set; }
}

[Serializable]
public class Customer
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}

[Serializable]
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
    // 尝试反序列化无效的XML
    string invalidXml = "<Invalid>XML</Invalid>";
    var result = XmlUtils.FromXml<User>(invalidXml);
}
catch (Exception ex)
{
    Console.WriteLine($"反序列化失败: {ex.Message}");
}

try
{
    // 尝试序列化不支持序列化的对象
    var nonSerializableObject = new { Name = "Test" };
    string xml = XmlUtils.ToXml(nonSerializableObject);
}
catch (Exception ex)
{
    Console.WriteLine($"序列化失败: {ex.Message}");
}
```

---

## 注意事项

1. **序列化特性：** 要序列化的类必须标记`[Serializable]`特性，或者使用`XmlSerializer`支持的特性
2. **公共属性：** 只有公共属性才会被序列化，私有字段不会被序列化
3. **无参构造函数：** 反序列化的类必须有无参构造函数
4. **抽象类型：** 不能反序列化到抽象类型
5. **异常处理：** 序列化和反序列化过程可能抛出异常，建议使用try-catch处理
6. **性能考虑：** 对于大量数据，XML序列化可能影响性能，考虑使用其他序列化方式
7. **编码问题：** 默认使用UTF-16编码，注意中文字符的处理

---

## 依赖关系

- `System`: 基础类型
- `System.IO`: 文件流操作
- `System.Text`: 字符串构建
- `System.Xml.Serialization`: XML序列化功能 