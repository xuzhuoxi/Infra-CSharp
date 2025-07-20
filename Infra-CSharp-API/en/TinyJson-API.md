# TinyJson API Documentation

## Namespace: JLGames.Infra.TinyJson

### Static Classes

#### JSONParser
JSON parser class

```csharp
/// <summary>
/// JSON parser class
/// Provides simple and efficient JSON parsing functionality
/// 
/// Features:
/// - Simple JSON parser with about 300 lines of code
/// - Attempts to minimize GC allocation
/// - Concise API: "[1,2,3]".FromJson<List<int>>()
/// - Supports class and struct parsing
/// - Can parse to Dictionary<string,object> and List<object>
/// - Supports AOT compilation (no JIT Emit)
/// - Corrupted JSON returns null instead of throwing exceptions
/// - Only writes to public fields and property setters
/// 
/// Limitations:
/// - No JIT Emit support, struct parsing is slower
/// - Limited to parsing JSON files smaller than 2GB
/// - Does not support abstract class or interface parsing
/// </summary>
public static class JSONParser
{
    [ThreadStatic] static Stack<List<string>> splitArrayPool;
    [ThreadStatic] static StringBuilder stringBuilder;
    [ThreadStatic] static Dictionary<Type, Dictionary<string, FieldInfo>> fieldInfoCache;
    [ThreadStatic] static Dictionary<Type, Dictionary<string, PropertyInfo>> propertyInfoCache;

    /// <summary>
    /// Parse from JSON string to specified type
    /// </summary>
    /// <typeparam name="T">Target type</typeparam>
    /// <param name="json">JSON string</param>
    /// <returns>Parsed object</returns>
    public static T FromJson<T>(this string json);

    /// <summary>
    /// Parse string value
    /// </summary>
    /// <param name="json">JSON string</param>
    /// <returns>Parsed string</returns>
    static int AppendUntilStringEnd(bool appendEscapeCharacter, int startIdx, string json);

    /// <summary>
    /// Split JSON objects and arrays
    /// Splits { <value>:<value>, <value>:<value> } and [ <value>, <value> ] into value string lists
    /// </summary>
    /// <param name="json">JSON string</param>
    /// <returns>Split value list</returns>
    static List<string> Split(string json);

    /// <summary>
    /// Parse value to specified type
    /// </summary>
    /// <param name="type">Target type</param>
    /// <param name="json">JSON string</param>
    /// <returns>Parsed object</returns>
    internal static object ParseValue(Type type, string json);

    /// <summary>
    /// Parse anonymous value
    /// </summary>
    /// <param name="json">JSON string</param>
    /// <returns>Parsed object</returns>
    static object ParseAnonymousValue(string json);

    /// <summary>
    /// Create member name dictionary
    /// </summary>
    /// <typeparam name="T">Member info type</typeparam>
    /// <param name="members">Member array</param>
    /// <returns>Member name dictionary</returns>
    static Dictionary<string, T> CreateMemberNameDictionary<T>(T[] members) where T : MemberInfo;

    /// <summary>
    /// Parse object
    /// </summary>
    /// <param name="type">Object type</param>
    /// <param name="json">JSON string</param>
    /// <returns>Parsed object</returns>
    static object ParseObject(Type type, string json);
}
```

#### JSONWriter
JSON writer class

```csharp
/// <summary>
/// JSON writer class
/// Provides simple and efficient JSON serialization functionality
/// 
/// Features:
/// - Outputs JSON structure from objects
/// - Concise API: (new List<int> { 1, 2, 3 }).ToJson() == "[1,2,3]"
/// - Only outputs public fields and property getters
/// </summary>
public static class JSONWriter
{
    /// <summary>
    /// Serialize object to JSON string
    /// </summary>
    /// <param name="item">Object to serialize</param>
    /// <returns>JSON string</returns>
    public static string ToJson(this object item);

    /// <summary>
    /// Append value to StringBuilder
    /// </summary>
    /// <param name="stringBuilder">StringBuilder</param>
    /// <param name="item">Object to append</param>
    static void AppendValue(StringBuilder stringBuilder, object item);

    /// <summary>
    /// Get member name
    /// </summary>
    /// <param name="member">Member info</param>
    /// <returns>Member name</returns>
    static string GetMemberName(MemberInfo member);
}
```

### Function Description

#### JSON Parsing Features

**Supported Data Types**
- **Basic Types:** string, int, float, double, decimal, bool, DateTime
- **Enum Types:** Support enum value parsing
- **Array Types:** Support various array types
- **Collection Types:** Support generic collections like List<T>
- **Dictionary Types:** Support Dictionary<string, T>
- **Custom Types:** Support classes and structs

**Parsing Features**
1. **Thread Safety:** Uses ThreadStatic variables to ensure thread safety
2. **Memory Optimization:** Uses object pools to reduce GC pressure
3. **Error Tolerance:** Returns null instead of throwing exceptions for corrupted JSON
4. **Reflection Caching:** Caches field and property information to improve performance

#### JSON Serialization Features

**Serialization Rules**
- **Public Members:** Only serializes public fields and properties
- **Attribute Control:** Supports DataMember and IgnoreDataMember attributes
- **Type Support:** Supports all basic types and collection types
- **Encoding Handling:** Properly handles special characters and Unicode

**Output Format**
1. **Compact Format:** Outputs compact JSON format
2. **Type Preservation:** Maintains original data types
3. **Special Characters:** Properly handles escape characters
4. **Unicode Support:** Supports Unicode character encoding

### Usage Examples

#### Basic Parsing
```csharp
// Parse basic types
string jsonString = "\"Hello, World!\"";
string result = jsonString.FromJson<string>();
Console.WriteLine(result); // Hello, World!

// Parse numbers
string jsonNumber = "42";
int number = jsonNumber.FromJson<int>();
Console.WriteLine(number); // 42

// Parse boolean values
string jsonBool = "true";
bool value = jsonBool.FromJson<bool>();
Console.WriteLine(value); // True

// Parse arrays
string jsonArray = "[1, 2, 3, 4, 5]";
List<int> list = jsonArray.FromJson<List<int>>();
Console.WriteLine(string.Join(", ", list)); // 1, 2, 3, 4, 5
```

#### Object Parsing
```csharp
// Define data class
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

// Parse object
string jsonPerson = "{\"Name\":\"John\",\"Age\":25,\"Email\":\"john@example.com\"}";
Person person = jsonPerson.FromJson<Person>();
```
Console.WriteLine($"Name: {person.Name}, Age: {person.Age}, Email: {person.Email}");
```

#### Complex Object Parsing
```csharp
public class Company
{
    public string Name { get; set; }
    public List<Person> Employees { get; set; }
    public Dictionary<string, string> Properties { get; set; }
}

// Parse complex object
string jsonCompany = @"{
    ""Name"": ""Example Company"",
    ""Employees"": [
        {""Name"": ""John"", ""Age"": 25},
        {""Name"": ""Jane"", ""Age"": 30}
    ],
    ""Properties"": {
        ""Address"": ""123 Main Street, New York"",
        ""Phone"": ""555-0123""
    }
}";

Company company = jsonCompany.FromJson<Company>();
Console.WriteLine($"Company: {company.Name}");
Console.WriteLine($"Employee count: {company.Employees.Count}");
Console.WriteLine($"Address: {company.Properties["Address"]}");
```

#### Dynamic Parsing
```csharp
// Parse to dynamic object
string jsonDynamic = "{\"name\":\"John\",\"age\":25,\"skills\":[\"C#\",\"Java\",\"Python\"]}";

// Parse to Dictionary
Dictionary<string, object> dict = jsonDynamic.FromJson<Dictionary<string, object>>();
Console.WriteLine($"Name: {dict["name"]}");
Console.WriteLine($"Age: {dict["age"]}");

// Parse to object (automatic type inference)
object obj = jsonDynamic.FromJson<object>();
if (obj is Dictionary<string, object> dynamicDict)
{
    Console.WriteLine($"Dynamic parsing name: {dynamicDict["name"]}");
}
```

#### Basic Serialization
```csharp
// Serialize basic types
string text = "Hello, World!";
string json = text.ToJson();
Console.WriteLine(json); // "Hello, World!"

// Serialize numbers
int number = 42;
string jsonNumber = number.ToJson();
Console.WriteLine(jsonNumber); // 42

// Serialize boolean values
bool value = true;
string jsonBool = value.ToJson();
Console.WriteLine(jsonBool); // true

// Serialize arrays
List<int> list = new List<int> { 1, 2, 3, 4, 5 };
string jsonArray = list.ToJson();
Console.WriteLine(jsonArray); // [1,2,3,4,5]
```

#### Object Serialization
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

// Serialize object
var product = new Product
{
    Name = "Laptop",
    Price = 999.99m,
    InStock = true,
    CreatedDate = DateTime.Now,
    InternalId = "INT-001",
    DisplayName = "High Performance Laptop"
};

string json = product.ToJson();
Console.WriteLine(json);
// Output: {"Name":"Laptop","Price":999.99,"InStock":true,"CreatedDate":"2024-01-01T12:00:00","product_name":"High Performance Laptop"}
```

#### Complex Object Serialization
```csharp
public class Order
{
    public string OrderId { get; set; }
    public List<Product> Products { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
}

// Serialize complex object
var order = new Order
{
    OrderId = "ORD-001",
    Products = new List<Product>
    {
        new Product { Name = "Mouse", Price = 25.99m, InStock = true },
        new Product { Name = "Keyboard", Price = 89.99m, InStock = true }
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

#### Error Handling
```csharp
// Handle corrupted JSON
string corruptedJson = "{\"name\":\"John\",\"age\":25,"; // Missing closing brace

try
{
    var result = corruptedJson.FromJson<Dictionary<string, object>>();
    if (result == null)
    {
        Console.WriteLine("JSON parsing failed, returned null");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Parsing exception: {ex.Message}");
}

// Handle type mismatch
string typeMismatchJson = "{\"age\":\"not_a_number\"}";
var person = typeMismatchJson.FromJson<Person>();
Console.WriteLine($"Age: {person.Age}"); // May return default value
```

#### Performance Optimization
```csharp
// Batch parsing
var jsonList = new List<string>
{
    "{\"name\":\"John\",\"age\":25}",
    "{\"name\":\"Jane\",\"age\":30}",
    "{\"name\":\"Bob\",\"age\":35}"
};

var persons = new List<Person>();
foreach (var json in jsonList)
{
    var person = json.FromJson<Person>();
    persons.Add(person);
}

// Batch serialization
var products = new List<Product>
{
    new Product { Name = "Product1", Price = 100 },
    new Product { Name = "Product2", Price = 200 },
    new Product { Name = "Product3", Price = 300 }
};

var jsonList = new List<string>();
foreach (var product in products)
{
    var json = product.ToJson();
    jsonList.Add(json);
}
```

### Design Features

1. **Concise API**: Provides extension methods, simple to use
2. **High Performance**: Uses ThreadStatic and object pools to optimize performance
3. **Error Tolerant**: Corrupted JSON returns null instead of throwing exceptions
4. **Type Safe**: Supports strongly typed parsing
5. **Memory Optimized**: Minimizes GC allocation
6. **AOT Support**: Supports AOT compilation environment

### Considerations

1. **Type Limitations**: Does not support abstract class or interface parsing
2. **File Size**: Limited to parsing JSON files smaller than 2GB
3. **Performance Considerations**: Pay attention to memory usage for large amounts of data
4. **Thread Safety**: Each thread has independent cache
5. **Encoding Issues**: Pay attention to JSON string encoding format
6. **Attribute Support**: Supports DataMember and IgnoreDataMember attributes 