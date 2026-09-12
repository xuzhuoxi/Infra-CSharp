# TinyJson API Documentation

## Namespace: JLGames.Infra.TinyJson

### Static Classes

#### JSONParser
A simple JSON parser. Attempts to parse JSON with minimal GC allocation; provides a concise extension-method API; supports class and struct parsing; can parse JSON without type information into `Dictionary<string, object>` and `List<object>`; does not use JIT Emit so AOT compilation (including iOS) is supported; attempts not to throw when JSON is corrupted or invalid and returns `null` instead; only writes to public fields and property setters on classes/structs.

Limitations: no JIT Emit, so struct parsing is slower; limited to JSON smaller than 2GB (`int.MaxValue`); parsing abstract classes or interfaces is not supported and will throw.

```csharp
/// <summary>
/// Really simple JSON parser in ~300 lines.
/// Attempts to parse JSON files with minimal GC allocation.
/// Nice and simple "[1,2,3]".FromJson&lt;List&lt;int&gt;&gt;() API.
/// Classes and structs can be parsed too.
/// Can parse JSON without type information into Dictionary&lt;string, object&gt; and List&lt;object&gt;.
/// No JIT Emit support to support AOT compilation on iOS.
/// Attempts are made to NOT throw an exception if the JSON is corrupted or invalid: returns null instead.
/// Only public fields and property setters on classes/structs will be written to.
///
/// Limitations:
/// - No JIT Emit support to parse structures quickly
/// - Limited to parsing &lt;2GB JSON files (due to int.MaxValue)
/// - Parsing of abstract classes or interfaces is NOT supported and will throw an exception
/// </summary>
public static class JSONParser
{
    /// <summary>
    /// Parse a JSON string into the specified type.
    /// </summary>
    /// <typeparam name="T">Target type</typeparam>
    /// <param name="json">JSON string</param>
    /// <returns>Parsed object; corrupted or invalid JSON typically returns null (or a default value type)</returns>
    public static T FromJson<T>(this string json);
}
```

#### JSONWriter
A simple JSON writer. Outputs JSON structures from an object; provides a concise extension-method API; only outputs public fields and property getters on objects.

```csharp
/// <summary>
/// Really simple JSON writer.
/// Outputs JSON structures from an object.
/// Really simple API: (new List&lt;int&gt; { 1, 2, 3 }).ToJson() == "[1,2,3]"
/// Will only output public fields and property getters on objects.
/// </summary>
public static class JSONWriter
{
    /// <summary>
    /// Serialize an object to a JSON string.
    /// </summary>
    /// <param name="item">Object to serialize</param>
    /// <returns>Compact JSON string; returns "null" when <paramref name="item"/> is null</returns>
    public static string ToJson(this object item);
}
```

### Function Description

#### JSON Parsing Features

**Supported Data Types**
- **String:** `string`
- **Primitive types:** `bool`, `char`, `sbyte`, `byte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `float`, `double` (via `Convert.ChangeType` with invariant culture)
- **decimal:** parsed with `decimal.TryParse`
- **DateTime:** quotes stripped, then parsed with invariant culture
- **Enums:** quoted or unquoted enum names; parse failure returns `0`
- **Arrays:** arrays of any element type
- **Collections:** `List<T>`
- **Dictionaries:** `Dictionary<string, T>` only (returns `null` when the key type is not `string`)
- **Untyped:** `FromJson<object>()` parses objects as `Dictionary<string, object>` and arrays as `List<object>`
- **Custom types:** public instance fields and writable properties on classes and structs

**Parsing Features**
1. **Thread Safety:** Uses `ThreadStatic` caches for field/property reflection and temporary buffers
2. **Memory Optimization:** Uses a list object pool to reduce GC pressure
3. **Error Tolerance:** Corrupted or invalid JSON typically returns `null` instead of throwing
4. **Attribute Control:** Supports `DataMember` (optional JSON name) and `IgnoreDataMember`; member names match case-insensitively
5. **Constructors:** Instances are created uninitialized; constructors are not called

#### JSON Serialization Features

**Serialization Rules**
- **Public Members:** Only serializes public instance fields and readable properties
- **Null Omission:** Members whose value is `null` are omitted
- **Attribute Control:** Supports `DataMember` (optional JSON name) and `IgnoreDataMember`
- **Dictionaries:** Only `Dictionary<,>` with `string` keys is written; other key types emit `{}`
- **Collections:** Types that implement `IList` are written as JSON arrays

**Output Format**
1. **Compact Format:** No extra whitespace
2. **Numbers:** Integers in decimal; `float`/`double`/`decimal` use invariant culture
3. **Booleans:** `true` / `false`
4. **DateTime / enums:** Quoted strings (`DateTime` uses invariant culture formatting)
5. **Strings:** Escapes `"\`, control characters, and Unicode control characters

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

// Parse arrays / List
string jsonArray = "[1,2,3,4,5]";
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
// Parse without type information
string jsonDynamic = "{\"name\":\"John\",\"age\":25,\"skills\":[\"C#\",\"Java\",\"Python\"]}";

// Parse to Dictionary
Dictionary<string, object> dict = jsonDynamic.FromJson<Dictionary<string, object>>();
Console.WriteLine($"Name: {dict["name"]}");
Console.WriteLine($"Age: {dict["age"]}");

// Parse to object (objects → Dictionary<string, object>, arrays → List<object>)
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
    CreatedDate = new DateTime(2024, 1, 1, 12, 0, 0),
    InternalId = "INT-001",
    DisplayName = "High Performance Laptop"
};

string json = product.ToJson();
Console.WriteLine(json);
// InternalId is omitted because of IgnoreDataMember
// DisplayName is written as product_name because of DataMember.Name
// CreatedDate is a quoted string formatted with invariant culture
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
// Corrupted JSON typically returns null instead of throwing
string corruptedJson = "{\"name\":\"John\",\"age\":25,"; // Missing closing brace

var result = corruptedJson.FromJson<Dictionary<string, object>>();
if (result == null)
{
    Console.WriteLine("JSON parsing failed, returned null");
}

// Unrecognized enum names return 0
// Abstract classes or interfaces throw (not supported)
```

#### Performance Optimization
```csharp
// Batch parsing (each thread has its own ThreadStatic cache)
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

var serialized = new List<string>();
foreach (var product in products)
{
    serialized.Add(product.ToJson());
}
```

### Design Features

1. **Concise API:** `FromJson<T>()` / `ToJson()` extension methods
2. **High Performance:** `ThreadStatic` caches and a list object pool
3. **Error Tolerant:** Corrupted JSON typically returns `null` instead of throwing
4. **Type Safe:** Supports strongly typed parsing
5. **Memory Optimized:** Minimizes GC allocation
6. **AOT Support:** No JIT Emit; usable in AOT environments such as iOS

### Considerations

1. **Type Limitations:** Abstract classes or interfaces are not supported (throws)
2. **File Size:** Limited to JSON smaller than 2GB (`int.MaxValue`)
3. **Dictionary Keys:** Both parse and serialize require dictionary keys to be `string`
4. **Thread Safety:** Each thread has an independent parse cache
5. **Constructors:** Custom types are created uninitialized; constructors are not called
6. **Null Values:** Serialization skips fields and properties whose value is `null`
7. **Attribute Support:** Supports `DataMember` and `IgnoreDataMember`
8. **Member Matching:** Parse matches member names case-insensitively
