# JLGames.Infra.Xml API Documentation

## Overview

The Xml module provides XML serialization helpers based on `XmlSerializer`, supporting conversion between objects and XML strings.

## Namespace

`JLGames.Infra.Xml`

---

## Utility Classes

### XmlUtils

XML serialization helpers based on `XmlSerializer`.

```csharp
public static class XmlUtils
```

#### Static Methods

##### ToXml(object obj)

```csharp
public static string ToXml(object obj)
```

**Description:** Serialize an object to an XML string.

**Parameters:**
- `obj` (object): Object to serialize; returns an empty string when null.

**Return Value:**
- `string`: XML text, or an empty string if `obj` is null.

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

string empty = XmlUtils.ToXml(null); // ""
```

##### FromXml<T>(string xml)

```csharp
public static T FromXml<T>(string xml)
```

**Description:** Deserialize an XML string to an instance of `T`.

**Parameters:**
- `xml` (string): XML text; returns default when null or empty.

**Type Parameters:**
- `T`: Target type (must be concrete and XML-serializable).

**Return Value:**
- `T`: Deserialized instance, or `default(T)` when `xml` is null or empty.

**Example:**
```csharp
string xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Name>John</Name>
  <Age>25</Age>
</Person>";

var person = XmlUtils.FromXml<Person>(xml);
// Result: person.Name = "John", person.Age = 25

Person missing = XmlUtils.FromXml<Person>(null); // null
```

##### FromXml(string xml, System.Type type)

```csharp
public static object FromXml(string xml, System.Type type)
```

**Description:** Deserialize an XML string to an instance of the specified type.

**Parameters:**
- `xml` (string): XML text; returns null when null or empty.
- `type` (System.Type): Target type (must be concrete and XML-serializable).

**Return Value:**
- `object`: Deserialized instance, or null when `xml` is null or empty.

**Exceptions:**
- `ArgumentNullException`: `type` is null.
- `ArgumentException`: `type` is abstract and cannot be instantiated.

**Example:**
```csharp
string xml = @"<?xml version=""1.0"" encoding=""utf-16""?>
<Person xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Name>John</Name>
  <Age>25</Age>
</Person>";

var person = XmlUtils.FromXml(xml, typeof(Person)) as Person;
// Result: person.Name = "John", person.Age = 25

object missing = XmlUtils.FromXml("", typeof(Person)); // null
```

---

## Usage Examples

### Basic Serialization and Deserialization

```csharp
// Define a data class (parameterless constructor required; public properties/fields are serialized)
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
    // Abstract types cannot be instantiated
    object result = XmlUtils.FromXml("<Root />", typeof(Stream));
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid type: {ex.Message}");
}

try
{
    // Anonymous types are not supported by XmlSerializer
    var anonymous = new { Name = "Test" };
    string xml = XmlUtils.ToXml(anonymous);
}
catch (Exception ex)
{
    Console.WriteLine($"Serialization failed: {ex.Message}");
}
```

---

## Considerations

1. **Implementation:** Built on `System.Xml.Serialization.XmlSerializer`. The target type must be concrete and XML-serializable.
2. **Public Members:** Public properties and public fields are serialized; private members are not.
3. **Parameterless Constructor:** Types used for deserialization must have a parameterless constructor.
4. **Null / Empty Input:** `ToXml(null)` returns an empty string. `FromXml` returns `null` / `default(T)` when the XML is null or empty, without throwing.
5. **Abstract Types:** Cannot deserialize to abstract types. Throws `ArgumentNullException` when `type` is null, and `ArgumentException` when `type` is abstract.
6. **Exception Handling:** Serialization/deserialization failures are written to the console and then rethrown; use try-catch around these calls.
7. **Performance:** A new `XmlSerializer` is created on every call; consider the cost for large payloads or high-frequency use.
8. **Encoding:** Output goes through `StringWriter`, so the XML declaration defaults to UTF-16.

---

## Dependencies

- `System`: Basic types
- `System.IO`: String readers/writers
- `System.Text`: String building
- `System.Xml.Serialization`: XML serialization
