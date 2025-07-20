# TinyJson API 文档

## 命名空间: JLGames.Infra.TinyJson

### 静态类 (Static Classes)

#### JSONParser
JSON解析器类

```csharp
/// <summary>
/// JSON解析器类
/// 提供简单高效的JSON解析功能
/// 
/// 特性：
/// - 约300行代码的简单JSON解析器
/// - 尝试最小化GC分配
/// - 简洁的API："[1,2,3]".FromJson<List<int>>()
/// - 支持类和结构体解析
/// - 可以解析为Dictionary<string,object>和List<object>
/// - 支持AOT编译（无JIT Emit）
/// - 损坏的JSON返回null而不是抛出异常
/// - 只写入公共字段和属性setter
/// 
/// 限制：
/// - 无JIT Emit支持，解析结构体较慢
/// - 限制解析小于2GB的JSON文件
/// - 不支持抽象类或接口解析
/// </summary>
public static class JSONParser
{
    [ThreadStatic] static Stack<List<string>> splitArrayPool;
    [ThreadStatic] static StringBuilder stringBuilder;
    [ThreadStatic] static Dictionary<Type, Dictionary<string, FieldInfo>> fieldInfoCache;
    [ThreadStatic] static Dictionary<Type, Dictionary<string, PropertyInfo>> propertyInfoCache;

    /// <summary>
    /// 从JSON字符串解析为指定类型
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="json">JSON字符串</param>
    /// <returns>解析后的对象</returns>
    public static T FromJson<T>(this string json);

    /// <summary>
    /// 解析字符串值
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <returns>解析后的字符串</returns>
    static int AppendUntilStringEnd(bool appendEscapeCharacter, int startIdx, string json);

    /// <summary>
    /// 分割JSON对象和数组
    /// 将 { <value>:<value>, <value>:<value> } 和 [ <value>, <value> ] 分割为值字符串列表
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <returns>分割后的值列表</returns>
    static List<string> Split(string json);

    /// <summary>
    /// 解析值到指定类型
    /// </summary>
    /// <param name="type">目标类型</param>
    /// <param name="json">JSON字符串</param>
    /// <returns>解析后的对象</returns>
    internal static object ParseValue(Type type, string json);

    /// <summary>
    /// 解析匿名值
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <returns>解析后的对象</returns>
    static object ParseAnonymousValue(string json);

    /// <summary>
    /// 创建成员名称字典
    /// </summary>
    /// <typeparam name="T">成员信息类型</typeparam>
    /// <param name="members">成员数组</param>
    /// <returns>成员名称字典</returns>
    static Dictionary<string, T> CreateMemberNameDictionary<T>(T[] members) where T : MemberInfo;

    /// <summary>
    /// 解析对象
    /// </summary>
    /// <param name="type">对象类型</param>
    /// <param name="json">JSON字符串</param>
    /// <returns>解析后的对象</returns>
    static object ParseObject(Type type, string json);
}
```

#### JSONWriter
JSON写入器类

```csharp
/// <summary>
/// JSON写入器类
/// 提供简单高效的JSON序列化功能
/// 
/// 特性：
/// - 从对象输出JSON结构
/// - 简洁的API：(new List<int> { 1, 2, 3 }).ToJson() == "[1,2,3]"
/// - 只输出公共字段和属性getter
/// </summary>
public static class JSONWriter
{
    /// <summary>
    /// 将对象序列化为JSON字符串
    /// </summary>
    /// <param name="item">要序列化的对象</param>
    /// <returns>JSON字符串</returns>
    public static string ToJson(this object item);

    /// <summary>
    /// 将值追加到StringBuilder
    /// </summary>
    /// <param name="stringBuilder">StringBuilder</param>
    /// <param name="item">要追加的对象</param>
    static void AppendValue(StringBuilder stringBuilder, object item);

    /// <summary>
    /// 获取成员名称
    /// </summary>
    /// <param name="member">成员信息</param>
    /// <returns>成员名称</returns>
    static string GetMemberName(MemberInfo member);
}
```

### 功能说明

#### JSON解析特性

**支持的数据类型**
- **基本类型**：string, int, float, double, decimal, bool, DateTime
- **枚举类型**：支持枚举值的解析
- **数组类型**：支持各种数组类型
- **集合类型**：支持List<T>等泛型集合
- **字典类型**：支持Dictionary<string, T>
- **自定义类型**：支持类和结构体

**解析特性**
1. **线程安全**：使用ThreadStatic变量确保线程安全
2. **内存优化**：使用对象池减少GC压力
3. **错误容错**：损坏的JSON返回null而不是抛出异常
4. **反射缓存**：缓存字段和属性信息提高性能

#### JSON序列化特性

**序列化规则**
- **公共成员**：只序列化公共字段和属性
- **属性控制**：支持DataMember和IgnoreDataMember特性
- **类型支持**：支持所有基本类型和集合类型
- **编码处理**：正确处理特殊字符和Unicode

**输出格式**
1. **紧凑格式**：输出紧凑的JSON格式
2. **类型保持**：保持原始数据类型
3. **特殊字符**：正确处理转义字符
4. **Unicode支持**：支持Unicode字符编码

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

// 解析数组
string jsonArray = "[1, 2, 3, 4, 5]";
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
// 解析为动态对象
string jsonDynamic = "{\"name\":\"张三\",\"age\":25,\"skills\":[\"C#\",\"Java\",\"Python\"]}";

// 解析为Dictionary
Dictionary<string, object> dict = jsonDynamic.FromJson<Dictionary<string, object>>();
Console.WriteLine($"姓名: {dict["name"]}");
Console.WriteLine($"年龄: {dict["age"]}");

// 解析为object（自动推断类型）
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
    CreatedDate = DateTime.Now,
    InternalId = "INT-001",
    DisplayName = "高性能笔记本"
};

string json = product.ToJson();
Console.WriteLine(json);
// 输出: {"Name":"笔记本电脑","Price":5999.99,"InStock":true,"CreatedDate":"2024-01-01T12:00:00","product_name":"高性能笔记本"}
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
// 处理损坏的JSON
string corruptedJson = "{\"name\":\"张三\",\"age\":25,"; // 缺少闭合括号

try
{
    var result = corruptedJson.FromJson<Dictionary<string, object>>();
    if (result == null)
    {
        Console.WriteLine("JSON解析失败，返回null");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"解析异常: {ex.Message}");
}

// 处理类型不匹配
string typeMismatchJson = "{\"age\":\"not_a_number\"}";
var person = typeMismatchJson.FromJson<Person>();
Console.WriteLine($"年龄: {person.Age}"); // 可能返回默认值
```

#### 性能优化
```csharp
// 批量解析
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

var jsonList = new List<string>();
foreach (var product in products)
{
    var json = product.ToJson();
    jsonList.Add(json);
}
```

### 设计特点

1. **简洁API**：提供扩展方法，使用简单
2. **高性能**：使用ThreadStatic和对象池优化性能
3. **容错性强**：损坏的JSON返回null而不是抛出异常
4. **类型安全**：支持强类型解析
5. **内存优化**：最小化GC分配
6. **AOT支持**：支持AOT编译环境

### 注意事项

1. **类型限制**：不支持抽象类或接口解析
2. **文件大小**：限制解析小于2GB的JSON文件
3. **性能考虑**：大量数据时注意内存使用
4. **线程安全**：每个线程有独立的缓存
5. **编码问题**：注意JSON字符串的编码格式
6. **特性支持**：支持DataMember和IgnoreDataMember特性 