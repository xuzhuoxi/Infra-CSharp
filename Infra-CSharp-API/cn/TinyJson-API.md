# TinyJson API 文档

## 命名空间: JLGames.Infra.TinyJson

### 静态类 (Static Classes)

#### JSONParser
简易 JSON 解析器。尽量以较少的 GC 分配解析 JSON；提供简洁的扩展方法 API；支持解析类和结构体；无类型信息时可解析为 `Dictionary<string, object>` 和 `List<object>`；不使用 JIT Emit，以支持 iOS 等 AOT 编译环境；损坏或无效的 JSON 尽量不抛异常，而是返回 `null`；仅写入类/结构体上的公共字段和属性 setter。

限制：无 JIT Emit，结构体解析较慢；受 `int.MaxValue` 限制，仅能解析小于 2GB 的 JSON；不支持抽象类或接口，解析时会抛出异常。

```csharp
/// <summary>
/// 约 300 行的简易 JSON 解析器。
/// 尽量以较少的 GC 分配解析 JSON。
/// 简洁 API："[1,2,3]".FromJson&lt;List&lt;int&gt;&gt;()
/// 支持解析类和结构体。
/// 无类型信息时可解析为 Dictionary&lt;string, object&gt; 和 List&lt;object&gt;。
/// 不使用 JIT Emit，以支持 iOS 等 AOT 编译环境。
/// 损坏或无效的 JSON 尽量不抛异常，而是返回 null。
/// 仅写入类/结构体上的公共字段和属性 setter。
///
/// 限制：
/// - 无 JIT Emit，结构体解析较慢
/// - 受 int.MaxValue 限制，仅能解析小于 2GB 的 JSON
/// - 不支持抽象类或接口，解析时会抛出异常
/// </summary>
public static class JSONParser
{
    /// <summary>
    /// 从 JSON 字符串解析为指定类型。
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="json">JSON 字符串</param>
    /// <returns>解析后的对象；损坏或无效的 JSON 通常返回 null（值类型则可能为默认值）</returns>
    public static T FromJson<T>(this string json);
}
```

#### JSONWriter
简易 JSON 写入器。从对象输出 JSON 结构；提供简洁的扩展方法 API；仅输出对象上的公共字段和属性 getter。

```csharp
/// <summary>
/// 简易 JSON 写入器。
/// 从对象输出 JSON 结构。
/// 简洁 API：(new List&lt;int&gt; { 1, 2, 3 }).ToJson() == "[1,2,3]"
/// 仅输出对象上的公共字段和属性 getter。
/// </summary>
public static class JSONWriter
{
    /// <summary>
    /// 将对象序列化为 JSON 字符串。
    /// </summary>
    /// <param name="item">要序列化的对象</param>
    /// <returns>紧凑格式的 JSON 字符串；<paramref name="item"/> 为 null 时返回 "null"</returns>
    public static string ToJson(this object item);
}
```

### 功能说明

#### JSON 解析特性

**支持的数据类型**
- **字符串**：`string`
- **基元类型**：`bool`、`char`、`sbyte`、`byte`、`short`、`ushort`、`int`、`uint`、`long`、`ulong`、`float`、`double`（通过 `Convert.ChangeType`，使用不变区域性）
- **decimal**：使用 `decimal.TryParse` 解析
- **DateTime**：去掉引号后使用不变区域性解析
- **枚举**：支持带或不带引号的枚举名；解析失败时返回 `0`
- **数组**：任意元素类型的数组
- **集合**：`List<T>`
- **字典**：仅支持 `Dictionary<string, T>`（键类型不是 `string` 时返回 `null`）
- **无类型**：`FromJson<object>()` 将对象解析为 `Dictionary<string, object>`，将数组解析为 `List<object>`
- **自定义类型**：类和结构体的公共实例字段与可写属性

**解析特性**
1. **线程安全**：使用 `ThreadStatic` 缓存字段/属性反射信息和临时缓冲区
2. **内存优化**：使用列表对象池减少 GC 压力
3. **错误容错**：损坏或无效的 JSON 尽量返回 `null` 而不是抛出异常
4. **特性控制**：支持 `DataMember`（可指定 JSON 名）和 `IgnoreDataMember`；成员名匹配忽略大小写
5. **构造函数**：使用未初始化对象创建实例，不调用构造函数

#### JSON 序列化特性

**序列化规则**
- **公共成员**：只序列化公共实例字段和可读属性
- **空值省略**：字段或属性值为 `null` 时不写入该成员
- **特性控制**：支持 `DataMember`（可指定 JSON 名）和 `IgnoreDataMember`
- **字典**：仅输出键类型为 `string` 的 `Dictionary<,>`；其它键类型输出 `{}`
- **集合**：实现 `IList` 的类型按 JSON 数组输出

**输出格式**
1. **紧凑格式**：无额外空白
2. **数值**：整数按十进制输出；`float`/`double`/`decimal` 使用不变区域性
3. **布尔**：`true` / `false`
4. **DateTime / 枚举**：带引号的字符串（`DateTime` 使用不变区域性格式）
5. **字符串**：转义 `"\`、控制字符及 Unicode 控制字符

### 使用示例

#### 基本解析
```csharp
// 解析基本类型
string jsonString = "\"Hello, World!\"";
string result = jsonString.FromJson<string>();
Console.WriteLine(result); // Hello, World!

// 解析数字
string jsonNumber = "42";
int number = jsonNumber.FromJson<int>();
Console.WriteLine(number); // 42

// 解析布尔值
string jsonBool = "true";
bool value = jsonBool.FromJson<bool>();
Console.WriteLine(value); // True

// 解析数组 / List
string jsonArray = "[1,2,3,4,5]";
List<int> list = jsonArray.FromJson<List<int>>();
Console.WriteLine(string.Join(", ", list)); // 1, 2, 3, 4, 5
```

#### 对象解析
```csharp
// 定义数据类
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

// 解析对象
string jsonPerson = "{\"Name\":\"张三\",\"Age\":25,\"Email\":\"zhangsan@example.com\"}";
Person person = jsonPerson.FromJson<Person>();
Console.WriteLine($"姓名: {person.Name}, 年龄: {person.Age}, 邮箱: {person.Email}");
```

#### 复杂对象解析
```csharp
public class Company
{
    public string Name { get; set; }
    public List<Person> Employees { get; set; }
    public Dictionary<string, string> Properties { get; set; }
}

// 解析复杂对象
string jsonCompany = @"{
    ""Name"": ""示例公司"",
    ""Employees"": [
        {""Name"": ""张三"", ""Age"": 25},
        {""Name"": ""李四"", ""Age"": 30}
    ],
    ""Properties"": {
        ""Address"": ""北京市朝阳区"",
        ""Phone"": ""010-12345678""
    }
}";

Company company = jsonCompany.FromJson<Company>();
Console.WriteLine($"公司: {company.Name}");
Console.WriteLine($"员工数: {company.Employees.Count}");
Console.WriteLine($"地址: {company.Properties["Address"]}");
```

#### 动态解析
```csharp
// 无类型信息时解析
string jsonDynamic = "{\"name\":\"张三\",\"age\":25,\"skills\":[\"C#\",\"Java\",\"Python\"]}";

// 解析为 Dictionary
Dictionary<string, object> dict = jsonDynamic.FromJson<Dictionary<string, object>>();
Console.WriteLine($"姓名: {dict["name"]}");
Console.WriteLine($"年龄: {dict["age"]}");

// 解析为 object（对象 → Dictionary<string, object>，数组 → List<object>）
object obj = jsonDynamic.FromJson<object>();
if (obj is Dictionary<string, object> dynamicDict)
{
    Console.WriteLine($"动态解析姓名: {dynamicDict["name"]}");
}
```

#### 基本序列化
```csharp
// 序列化基本类型
string text = "Hello, World!";
string json = text.ToJson();
Console.WriteLine(json); // "Hello, World!"

// 序列化数字
int number = 42;
string jsonNumber = number.ToJson();
Console.WriteLine(jsonNumber); // 42

// 序列化布尔值
bool value = true;
string jsonBool = value.ToJson();
Console.WriteLine(jsonBool); // true

// 序列化数组
List<int> list = new List<int> { 1, 2, 3, 4, 5 };
string jsonArray = list.ToJson();
Console.WriteLine(jsonArray); // [1,2,3,4,5]
```

#### 对象序列化
```csharp
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
    public DateTime CreatedDate { get; set; }

    [IgnoreDataMember]
    public string InternalId { get; set; }

    [DataMember(Name = "product_name")]
    public string DisplayName { get; set; }
}

// 序列化对象
var product = new Product
{
    Name = "笔记本电脑",
    Price = 5999.99m,
    InStock = true,
    CreatedDate = new DateTime(2024, 1, 1, 12, 0, 0),
    InternalId = "INT-001",
    DisplayName = "高性能笔记本"
};

string json = product.ToJson();
Console.WriteLine(json);
// InternalId 因 IgnoreDataMember 被忽略
// DisplayName 因 DataMember.Name 输出为 product_name
// CreatedDate 以不变区域性格式输出为带引号的字符串
```

#### 复杂对象序列化
```csharp
public class Order
{
    public string OrderId { get; set; }
    public List<Product> Products { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
}

// 序列化复杂对象
var order = new Order
{
    OrderId = "ORD-001",
    Products = new List<Product>
    {
        new Product { Name = "鼠标", Price = 99.99m, InStock = true },
        new Product { Name = "键盘", Price = 299.99m, InStock = true }
    },
    Metadata = new Dictionary<string, object>
    {
        ["customer_id"] = "CUST-001",
        ["order_date"] = DateTime.Now,
        ["priority"] = "high"
    }
};

string json = order.ToJson();
Console.WriteLine(json);
```

#### 错误处理
```csharp
// 损坏的 JSON 通常返回 null，而不是抛出异常
string corruptedJson = "{\"name\":\"张三\",\"age\":25,"; // 缺少闭合括号

var result = corruptedJson.FromJson<Dictionary<string, object>>();
if (result == null)
{
    Console.WriteLine("JSON 解析失败，返回 null");
}

// 枚举名无法识别时返回 0
// 抽象类或接口会抛出异常（不支持）
```

#### 性能优化
```csharp
// 批量解析（每个线程有独立的 ThreadStatic 缓存）
var jsonList = new List<string>
{
    "{\"name\":\"张三\",\"age\":25}",
    "{\"name\":\"李四\",\"age\":30}",
    "{\"name\":\"王五\",\"age\":35}"
};

var persons = new List<Person>();
foreach (var json in jsonList)
{
    var person = json.FromJson<Person>();
    persons.Add(person);
}

// 批量序列化
var products = new List<Product>
{
    new Product { Name = "产品1", Price = 100 },
    new Product { Name = "产品2", Price = 200 },
    new Product { Name = "产品3", Price = 300 }
};

var serialized = new List<string>();
foreach (var product in products)
{
    serialized.Add(product.ToJson());
}
```

### 设计特点

1. **简洁 API**：`FromJson<T>()` / `ToJson()` 扩展方法
2. **高性能**：`ThreadStatic` 缓存与列表对象池
3. **容错性强**：损坏的 JSON 尽量返回 `null` 而不是抛出异常
4. **类型安全**：支持强类型解析
5. **内存优化**：尽量减少 GC 分配
6. **AOT 支持**：无 JIT Emit，可用于 iOS 等 AOT 环境

### 注意事项

1. **类型限制**：不支持抽象类或接口解析（会抛出异常）
2. **文件大小**：仅能解析小于 2GB 的 JSON（受 `int.MaxValue` 限制）
3. **字典键**：解析和序列化均要求字典键为 `string`
4. **线程安全**：每个线程有独立的解析缓存
5. **构造函数**：解析自定义类型时不调用构造函数
6. **空值**：序列化时跳过值为 `null` 的字段和属性
7. **特性支持**：支持 `DataMember` 和 `IgnoreDataMember`
8. **成员匹配**：解析时成员名忽略大小写
