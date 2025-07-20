# JLGames.Infra.Xml API Documentation

## Overview

The Xml module provides XML serialization and deserialization utility classes, supporting conversion between objects and XML strings.

## Namespace

`JLGames.Infra.Xml`

---

## Utility Classes

### XmlUtils

XML utility class that provides serialization and deserialization functionality between objects and XML strings.

```csharp
public class XmlUtils
```

#### Static Methods

##### ToXml(object obj)

```csharp
public static string ToXml(object obj)
```

**Description:** Serialize to xml string

**Parameters:**
- `obj` (object): Object to serialize

**Return Value:**
- `string`: Serialized XML string

**Exceptions:**
- `Exception`: Exceptions that may be thrown during serialization

**Example:**
```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

var person = new Person { Name = "John", Age = 25 };
string xml = XmlUtils.ToXml(person);
// Result: <?xml version="1.0" encoding="utf-16"?>
//         <Person xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
//           <Name>John</Name>
//           <Age>25</Age>
//         </Person>
```

##### FromXml<T>(string xml)

```csharp
public static T FromXml<T>(string xml)
```

**Description:** Deserialize from xml string to object

**Parameters:**
- `xml` (string): XML string

**Type Parameters:**
- `T`: Target type

**Return Value:**
- `T`: Deserialized object

**Exceptions:**
- `Exception`: Exceptions that may be thrown during deserialization

**Example:**
```csharp
string xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Name>John</Name>
  <Age>25</Age>
</Person>";

var person = XmlUtils.FromXml<Person>(xml);
// Result: person.Name = "John", person.Age = 25
```

##### FromXml(string xml, System.Type type)

```csharp
public static object FromXml(string xml, System.Type type)
```

**Description:** Deserialize from xml string to object

**Parameters:**
- `xml` (string): XML string
- `type` (System.Type): Target type

**Return Value:**
- `object`: Deserialized object

**Exceptions:**
- `ArgumentNullException`: Thrown when type parameter is null
- `ArgumentException`: Thrown when type is abstract
- `Exception`: Exceptions that may be thrown during deserialization

**Example:**
```csharp
string xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Name>John</Name>
  <Age>25</Age>
</Person>";

var person = XmlUtils.FromXml(xml, typeof(Person)) as Person;
// Result: person.Name = "John", person.Age = 25
```

---

## Usage Examples

### Basic Serialization and Deserialization

```csharp
// Define data class
[Serializable]
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
}

// Create object
var user = new User
{
    Name = "John Doe",
    Email = "john@example.com",
    CreatedDate = DateTime.Now,
    Tags = new List<string> { "VIP", "Active" }
};

// Serialize to XML
string xmlString = XmlUtils.ToXml(user);
Console.WriteLine(xmlString);

// Deserialize back to object
var deserializedUser = XmlUtils.FromXml<User>(xmlString);
Console.WriteLine($"Name: {deserializedUser.Name}");
Console.WriteLine($"Email: {deserializedUser.Email}");
```

### Complex Object Serialization

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

// Create complex object
var order = new Order
{
    OrderId = 1001,
    Customer = new Customer
    {
        Name = "Jane Smith",
        Address = "123 Main Street, New York",
        Phone = "555-0123"
    },
    Items = new List<OrderItem>
    {
        new OrderItem { ProductName = "Laptop", Quantity = 1, UnitPrice = 999.00m },
        new OrderItem { ProductName = "Mouse", Quantity = 2, UnitPrice = 25.00m }
    },
    TotalAmount = 1049.00m
};

// Serialize
string orderXml = XmlUtils.ToXml(order);

// Deserialize
var deserializedOrder = XmlUtils.FromXml<Order>(orderXml);
```

### Error Handling

```csharp
try
{
    // Try to deserialize invalid XML
    string invalidXml = "<Invalid>XML</Invalid>";
    var result = XmlUtils.FromXml<User>(invalidXml);
}
catch (Exception ex)
{
    Console.WriteLine($"Deserialization failed: {ex.Message}");
}

try
{
    // Try to serialize non-serializable object
    var nonSerializableObject = new { Name = "Test" };
    string xml = XmlUtils.ToXml(nonSerializableObject);
}
catch (Exception ex)
{
    Console.WriteLine($"Serialization failed: {ex.Message}");
}
```

---

## Considerations

1. **Serialization Attributes:** Classes to be serialized must be marked with `[Serializable]` attribute, or use attributes supported by `XmlSerializer`
2. **Public Properties:** Only public properties will be serialized, private fields will not be serialized
3. **Parameterless Constructor:** Classes for deserialization must have a parameterless constructor
4. **Abstract Types:** Cannot deserialize to abstract types
5. **Exception Handling:** Serialization and deserialization processes may throw exceptions, it is recommended to use try-catch handling
6. **Performance Considerations:** For large amounts of data, XML serialization may affect performance, consider using other serialization methods
7. **Encoding Issues:** Uses UTF-16 encoding by default, pay attention to Chinese character processing

---

## Dependencies

- `System`: Basic types
- `System.IO`: File stream operations
- `System.Text`: String building
- `System.Xml.Serialization`: XML serialization functionality 