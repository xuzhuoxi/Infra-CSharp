# JLGames.Infra.Encodingx API Documentation

## Overview

The Encodingx module provides Base64 encoding and decoding functionality, supporting standard Base64, URL-safe Base64, and no-padding Base64 encoding formats.

## Namespace

`JLGames.Infra.Encodingx.Base64x`

---

## Interfaces

### IBase64Encoding

Base64 encoding interface that defines basic operations for Base64 encoding and decoding.

```csharp
public interface IBase64Encoding
```

#### Encoding Methods

##### EncodeToString(byte[] input)

```csharp
string EncodeToString(byte[] input);
```

**Description:** Encode

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `string`: Encoded string

##### EncodeToString(string input)

```csharp
string EncodeToString(string input);
```

**Description:** Encode

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `string`: Encoded string

##### EncodeToBytes(byte[] input)

```csharp
byte[] EncodeToBytes(byte[] input);
```

**Description:** Encode

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `byte[]`: Encoded byte array

##### EncodeToBytes(string input)

```csharp
byte[] EncodeToBytes(string input);
```

**Description:** Encode

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `byte[]`: Encoded byte array

#### Decoding Methods

##### DecodeBytesFrom(byte[] input)

```csharp
byte[] DecodeBytesFrom(byte[] input);
```

**Description:** Decode

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeBytesFrom(string input)

```csharp
byte[] DecodeBytesFrom(string input);
```

**Description:** Decode

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeStringFrom(byte[] input)

```csharp
string DecodeStringFrom(byte[] input);
```

**Description:** Decode

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `string`: Decoded string

##### DecodeStringFrom(string input)

```csharp
string DecodeStringFrom(string input);
```

**Description:** Decode

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `string`: Decoded string

---

## Implementation Classes

### Base64StdEncoding

Standard Base64 encoding implementation class.

```csharp
public sealed class Base64StdEncoding : IBase64Encoding
```

**Description:** Uses standard Base64 encoding format, includes padding characters (=)

**Example:**
```csharp
var encoder = new Base64StdEncoding();
string encoded = encoder.EncodeToString("Hello World");
// Result: "SGVsbG8gV29ybGQ="
```

### Base64RawStdEncoding

No-padding standard Base64 encoding implementation class.

```csharp
public sealed class Base64RawStdEncoding : IBase64Encoding
```

**Description:** Uses standard Base64 encoding format, but does not include padding characters (=)

**Example:**
```csharp
var encoder = new Base64RawStdEncoding();
string encoded = encoder.EncodeToString("Hello World");
// Result: "SGVsbG8gV29ybGQ"
```

### Base64UrlEncoding

URL-safe Base64 encoding implementation class.

```csharp
public sealed class Base64UrlEncoding : IBase64Encoding
```

**Description:** Uses URL-safe Base64 encoding format, replaces + and / with - and _, retains padding characters

**Example:**
```csharp
var encoder = new Base64UrlEncoding();
string encoded = encoder.EncodeToString("Hello World");
// Result: "SGVsbG8gV29ybGQ="
```

### Base64RawUrlEncoding

No-padding URL-safe Base64 encoding implementation class.

```csharp
public sealed class Base64RawUrlEncoding : IBase64Encoding
```

**Description:** Uses URL-safe Base64 encoding format, replaces + and / with - and _, does not include padding characters

**Example:**
```csharp
var encoder = new Base64RawUrlEncoding();
string encoded = encoder.EncodeToString("Hello World");
// Result: "SGVsbG8gV29ybGQ"
```

---

## Utility Classes

### Base64Utils

Base64 encoding utility class that provides static methods for Base64 encoding and decoding operations.

```csharp
public static class Base64Utils
```

#### Standard Base64 Encoding Methods

##### EncodeToStdString(byte[] input)

```csharp
public static string EncodeToStdString(byte[] input)
```

**Description:** Encode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `string`: Encoded string

##### EncodeToStdString(string input)

```csharp
public static string EncodeToStdString(string input)
```

**Description:** Encode using standard Base64, first convert string to byte array using UTF8 encoding

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `string`: Encoded string

##### EncodeToStdBytes(byte[] input)

```csharp
public static byte[] EncodeToStdBytes(byte[] input)
```

**Description:** Encode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `byte[]`: Encoded byte array

##### EncodeToStdBytes(string input)

```csharp
public static byte[] EncodeToStdBytes(string input)
```

**Description:** Encode using standard Base64

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `byte[]`: Encoded byte array

#### Standard Base64 Decoding Methods

##### DecodeBytesFromStd(string input)

```csharp
public static byte[] DecodeBytesFromStd(string input)
```

**Description:** Decode using standard Base64

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeBytesFromStd(byte[] input)

```csharp
public static byte[] DecodeBytesFromStd(byte[] input)
```

**Description:** Decode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeStringFromStd(string input)

```csharp
public static string DecodeStringFromStd(string input)
```

**Description:** Decode using standard Base64

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `string`: Decoded string

##### DecodeStringFromStd(byte[] input)

```csharp
public static string DecodeStringFromStd(byte[] input)
```

**Description:** Decode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `string`: Decoded string

#### No-Padding Standard Base64 Encoding Methods

##### EncodeToRawStdString(byte[] input)

```csharp
public static string EncodeToRawStdString(byte[] input)
```

**Description:** 1. Encode using standard Base64 2. Remove padding

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `string`: Encoded string

##### EncodeToRawStdString(string input)

```csharp
public static string EncodeToRawStdString(string input)
```

**Description:** 1. Encode using standard Base64 2. Remove padding

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `string`: Encoded string

##### EncodeStrToRawStdBytes(byte[] input)

```csharp
public static byte[] EncodeStrToRawStdBytes(byte[] input)
```

**Description:** 1. Encode using standard Base64 2. Remove padding

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `byte[]`: Encoded byte array

##### EncodeStrToRawStdBytes(string input)

```csharp
public static byte[] EncodeStrToRawStdBytes(string input)
```

**Description:** 1. Encode using standard Base64 2. Remove padding

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `byte[]`: Encoded byte array

#### No-Padding Standard Base64 Decoding Methods

##### DecodeBytesFromRawStd(string input)

```csharp
public static byte[] DecodeBytesFromRawStd(string input)
```

**Description:** 1. Add padding 2. Decode using standard Base64

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeBytesFromRawStd(byte[] input)

```csharp
public static byte[] DecodeBytesFromRawStd(byte[] input)
```

**Description:** 1. Add padding 2. Decode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeStringFromRawStd(string input)

```csharp
public static string DecodeStringFromRawStd(string input)
```

**Description:** 1. Add padding 2. Decode using standard Base64

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `string`: Decoded string

##### DecodeStringFromRawStd(byte[] input)

```csharp
public static string DecodeStringFromRawStd(byte[] input)
```

**Description:** 1. Add padding 2. Decode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `string`: Decoded string

#### URL-Safe Base64 Encoding Methods

##### EncodeToUrlString(byte[] input)

```csharp
public static string EncodeToUrlString(byte[] input)
```

**Description:** 1. Encode using standard Base64 2. Replace with URL-safe characters (retain padding)

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `string`: Encoded string

##### EncodeToUrlString(string input)

```csharp
public static string EncodeToUrlString(string input)
```

**Description:** 1. Encode using standard Base64 2. Replace with URL-safe characters (retain padding)

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `string`: Encoded string

##### EncodeToUrlBytes(byte[] input)

```csharp
public static byte[] EncodeToUrlBytes(byte[] input)
```

**Description:** 1. Encode using standard Base64 2. Replace with URL-safe characters (retain padding)

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `byte[]`: Encoded byte array

##### EncodeToUrlBytes(string input)

```csharp
public static byte[] EncodeToUrlBytes(string input)
```

**Description:** 1. Encode using standard Base64 2. Replace with URL-safe characters (retain padding)

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `byte[]`: Encoded byte array

#### URL-Safe Base64 Decoding Methods

##### DecodeBytesFromUrl(string input)

```csharp
public static byte[] DecodeBytesFromUrl(string input)
```

**Description:** 1. Replace with URL-safe characters 2. Decode using standard Base64

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeBytesFromUrl(byte[] input)

```csharp
public static byte[] DecodeBytesFromUrl(byte[] input)
```

**Description:** 1. Replace with URL-safe characters 2. Decode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeStringFromUrl(string input)

```csharp
public static string DecodeStringFromUrl(string input)
```

**Description:** 1. Replace with URL-safe characters 2. Decode using standard Base64

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `string`: Decoded string

##### DecodeStringFromUrl(byte[] input)

```csharp
public static string DecodeStringFromUrl(byte[] input)
```

**Description:** 1. Replace with URL-safe characters 2. Decode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `string`: Decoded string

#### No-Padding URL-Safe Base64 Encoding Methods

##### EncodeToRawUrlString(byte[] input)

```csharp
public static string EncodeToRawUrlString(byte[] input)
```

**Description:** 1. Encode using standard Base64 2. Replace with URL-safe characters 3. Remove padding

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `string`: Encoded string

##### EncodeToRawUrlString(string input)

```csharp
public static string EncodeToRawUrlString(string input)
```

**Description:** 1. Encode using standard Base64 2. Replace with URL-safe characters 3. Remove padding

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `string`: Encoded string

##### EncodeToRawUrlBytes(byte[] input)

```csharp
public static byte[] EncodeToRawUrlBytes(byte[] input)
```

**Description:** 1. Encode using standard Base64 2. Replace with URL-safe characters 3. Remove padding

**Parameters:**
- `input` (byte[]): Byte array to encode

**Return Value:**
- `byte[]`: Encoded byte array

##### EncodeToRawUrlBytes(string input)

```csharp
public static byte[] EncodeToRawUrlBytes(string input)
```

**Description:** 1. Encode using standard Base64 2. Replace with URL-safe characters 3. Remove padding

**Parameters:**
- `input` (string): String to encode

**Return Value:**
- `byte[]`: Encoded byte array

#### No-Padding URL-Safe Base64 Decoding Methods

##### DecodeBytesFromRawUrl(string input)

```csharp
public static byte[] DecodeBytesFromRawUrl(string input)
```

**Description:** 1. Add padding 2. Replace with URL-safe characters 3. Decode using standard Base64

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeBytesFromRawUrl(byte[] input)

```csharp
public static byte[] DecodeBytesFromRawUrl(byte[] input)
```

**Description:** 1. Add padding 2. Replace with URL-safe characters 3. Decode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `byte[]`: Decoded byte array

##### DecodeStringFromRawUrl(string input)

```csharp
public static string DecodeStringFromRawUrl(string input)
```

**Description:** 1. Add padding 2. Replace with URL-safe characters 3. Decode using standard Base64

**Parameters:**
- `input` (string): String to decode

**Return Value:**
- `string`: Decoded string

##### DecodeStringFromRawUrl(byte[] input)

```csharp
public static string DecodeStringFromRawUrl(byte[] input)
```

**Description:** 1. Add padding 2. Replace with URL-safe characters 3. Decode using standard Base64

**Parameters:**
- `input` (byte[]): Byte array to decode

**Return Value:**
- `string`: Decoded string

---

## Usage Examples

### Using Interface Implementation

```csharp
// Standard Base64 encoding
IBase64Encoding stdEncoder = new Base64StdEncoding();
string encoded = stdEncoder.EncodeToString("Hello World");
string decoded = stdEncoder.DecodeStringFrom(encoded);

// URL-safe Base64 encoding
IBase64Encoding urlEncoder = new Base64UrlEncoding();
string urlEncoded = urlEncoder.EncodeToString("Hello World");
string urlDecoded = urlEncoder.DecodeStringFrom(urlEncoded);

// No-padding Base64 encoding
IBase64Encoding rawEncoder = new Base64RawStdEncoding();
string rawEncoded = rawEncoder.EncodeToString("Hello World");
string rawDecoded = rawEncoder.DecodeStringFrom(rawEncoded);
```

### Using Utility Class

```csharp
// Standard Base64 encoding
string stdEncoded = Base64Utils.EncodeToStdString("Hello World");
string stdDecoded = Base64Utils.DecodeStringFromStd(stdEncoded);

// No-padding standard Base64 encoding
string rawStdEncoded = Base64Utils.EncodeToRawStdString("Hello World");
string rawStdDecoded = Base64Utils.DecodeStringFromRawStd(rawStdEncoded);

// URL-safe Base64 encoding
string urlEncoded = Base64Utils.EncodeToUrlString("Hello World");
string urlDecoded = Base64Utils.DecodeStringFromUrl(urlEncoded);

// No-padding URL-safe Base64 encoding
string rawUrlEncoded = Base64Utils.EncodeToRawUrlString("Hello World");
string rawUrlDecoded = Base64Utils.DecodeStringFromRawUrl(rawUrlEncoded);
```

### Byte Array Encoding

```csharp
byte[] data = Encoding.UTF8.GetBytes("Hello World");

// Encode to byte array
byte[] encodedBytes = Base64Utils.EncodeToStdBytes(data);
byte[] decodedBytes = Base64Utils.DecodeBytesFromStd(encodedBytes);

// Verify result
string result = Encoding.UTF8.GetString(decodedBytes);
// Result: "Hello World"
```

### Different Encoding Format Comparison

```csharp
string original = "Hello World!";

// Standard Base64 (with padding)
string std = Base64Utils.EncodeToStdString(original);
// Result: "SGVsbG8gV29ybGQh"

// No-padding standard Base64
string rawStd = Base64Utils.EncodeToRawStdString(original);
// Result: "SGVsbG8gV29ybGQh"

// URL-safe Base64 (with padding)
string url = Base64Utils.EncodeToUrlString(original);
// Result: "SGVsbG8gV29ybGQh"

// No-padding URL-safe Base64
string rawUrl = Base64Utils.EncodeToRawUrlString(original);
// Result: "SGVsbG8gV29ybGQh"
```

---

## Notes

1. **Encoding Format:** Standard Base64 uses A-Z, a-z, 0-9, +, / characters, URL-safe version replaces + and / with - and _
2. **Padding Characters:** Standard format uses = as padding, no-padding version removes padding characters
3. **Decoding Compatibility:** No-padding version automatically adds padding characters during decoding
4. **Character Encoding:** String encoding defaults to UTF-8 encoding
5. **Performance Considerations:** For large amounts of data, recommend using byte array methods instead of string methods
6. **Error Handling:** Decoding invalid Base64 strings will throw exceptions, pay attention to exception handling

---

## Dependencies

- `System`: Basic types
- `System.Text`: String encoding functionality 