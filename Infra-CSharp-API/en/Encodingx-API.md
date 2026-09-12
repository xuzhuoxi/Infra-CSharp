# JLGames.Infra.Encodingx API Documentation

## Overview

The Encodingx module provides Base64 encoding and decoding for four variants: standard, raw (no padding), URL-safe, and raw URL-safe. Strings and text are handled as UTF-8. Variant rules are defined by `IBase64Encoding` implementations; the same operations are also available as static methods on `Base64Utils`.

## Namespace

`JLGames.Infra.Encodingx.Base64x`

---

## Interfaces

### IBase64Encoding

Base64 encoding/decoding abstraction; variant behavior is defined by the implementation.

```csharp
public interface IBase64Encoding
```

#### Encoding Methods

##### EncodeToString(byte[] input)

```csharp
string EncodeToString(byte[] input);
```

**Description:** Encode binary data to a Base64 string.

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `string`: Base64 string

##### EncodeToString(string input)

```csharp
string EncodeToString(string input);
```

**Description:** Encode UTF-8 text to a Base64 string.

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `string`: Base64 string

##### EncodeToBytes(byte[] input)

```csharp
byte[] EncodeToBytes(byte[] input);
```

**Description:** Encode binary data to UTF-8 bytes of the Base64 string.

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

##### EncodeToBytes(string input)

```csharp
byte[] EncodeToBytes(string input);
```

**Description:** Encode UTF-8 text to UTF-8 bytes of the Base64 string.

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

#### Decoding Methods

##### DecodeBytesFrom(byte[] input)

```csharp
byte[] DecodeBytesFrom(byte[] input);
```

**Description:** Decode UTF-8 bytes of a Base64 string to binary data.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the Base64 string

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeBytesFrom(string input)

```csharp
byte[] DecodeBytesFrom(string input);
```

**Description:** Decode a Base64 string to binary data.

**Parameters:**
- `input` (string): Base64 string

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeStringFrom(byte[] input)

```csharp
string DecodeStringFrom(byte[] input);
```

**Description:** Decode UTF-8 bytes of a Base64 string to UTF-8 text.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the Base64 string

**Return Value:**
- `string`: Decoded UTF-8 text

##### DecodeStringFrom(string input)

```csharp
string DecodeStringFrom(string input);
```

**Description:** Decode a Base64 string to UTF-8 text.

**Parameters:**
- `input` (string): Base64 string

**Return Value:**
- `string`: Decoded UTF-8 text

---

## Implementation Classes

All four implementations implement `IBase64Encoding` and delegate to the matching `Base64Utils` static methods. Their public members match the interface; they add no extra members.

### Base64StdEncoding

Standard Base64 (RFC 4648): uses `+`/`/` and padding `=`.

```csharp
public sealed class Base64StdEncoding : IBase64Encoding
```

**Example:**
```csharp
var encoder = new Base64StdEncoding();
string encoded = encoder.EncodeToString("Hello World");
// Result: "SGVsbG8gV29ybGQ="
```

### Base64RawStdEncoding

Raw standard Base64: same alphabet as standard Base64, without padding `=`.

```csharp
public sealed class Base64RawStdEncoding : IBase64Encoding
```

**Example:**
```csharp
var encoder = new Base64RawStdEncoding();
string encoded = encoder.EncodeToString("Hello World");
// Result: "SGVsbG8gV29ybGQ"
```

### Base64UrlEncoding

URL-safe Base64: uses `-`/`_` instead of `+`/`/`, with padding `=`.

```csharp
public sealed class Base64UrlEncoding : IBase64Encoding
```

**Example:**
```csharp
var encoder = new Base64UrlEncoding();
string encoded = encoder.EncodeToString("Hello World");
// Result: "SGVsbG8gV29ybGQ="
```

### Base64RawUrlEncoding

Raw URL-safe Base64: URL-safe alphabet without padding `=`.

```csharp
public sealed class Base64RawUrlEncoding : IBase64Encoding
```

**Example:**
```csharp
var encoder = new Base64RawUrlEncoding();
string encoded = encoder.EncodeToString("Hello World");
// Result: "SGVsbG8gV29ybGQ"
```

---

## Utility Classes

### Base64Utils

Static helpers for standard, raw, URL-safe, and raw URL-safe Base64 variants.

```csharp
public static class Base64Utils
```

#### Standard Base64 Encoding Methods

##### EncodeToStdString(byte[] input)

```csharp
public static string EncodeToStdString(byte[] input)
```

**Description:** Encode bytes with standard Base64 (RFC 4648).

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `string`: Base64 string

##### EncodeToStdString(string input)

```csharp
public static string EncodeToStdString(string input)
```

**Description:** Encode UTF-8 text with standard Base64 (RFC 4648).

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `string`: Base64 string

##### EncodeToStdBytes(byte[] input)

```csharp
public static byte[] EncodeToStdBytes(byte[] input)
```

**Description:** Encode bytes with standard Base64 and return UTF-8 bytes of the result string.

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

##### EncodeToStdBytes(string input)

```csharp
public static byte[] EncodeToStdBytes(string input)
```

**Description:** Encode UTF-8 text with standard Base64 and return UTF-8 bytes of the result string.

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

#### Standard Base64 Decoding Methods

##### DecodeBytesFromStd(string input)

```csharp
public static byte[] DecodeBytesFromStd(string input)
```

**Description:** Decode a standard Base64 string to bytes.

**Parameters:**
- `input` (string): Base64 string

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeBytesFromStd(byte[] input)

```csharp
public static byte[] DecodeBytesFromStd(byte[] input)
```

**Description:** Decode UTF-8 bytes of a standard Base64 string to bytes.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the Base64 string

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeStringFromStd(string input)

```csharp
public static string DecodeStringFromStd(string input)
```

**Description:** Decode a standard Base64 string to UTF-8 text.

**Parameters:**
- `input` (string): Base64 string

**Return Value:**
- `string`: Decoded UTF-8 text

##### DecodeStringFromStd(byte[] input)

```csharp
public static string DecodeStringFromStd(byte[] input)
```

**Description:** Decode UTF-8 bytes of a standard Base64 string to UTF-8 text.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the Base64 string

**Return Value:**
- `string`: Decoded UTF-8 text

#### No-Padding Standard Base64 Encoding Methods

##### EncodeToRawStdString(byte[] input)

```csharp
public static string EncodeToRawStdString(byte[] input)
```

**Description:** Encode with standard Base64 and strip padding `=`.

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `string`: Base64 string without padding

##### EncodeToRawStdString(string input)

```csharp
public static string EncodeToRawStdString(string input)
```

**Description:** Encode UTF-8 text with standard Base64 and strip padding `=`.

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `string`: Base64 string without padding

##### EncodeStrToRawStdBytes(byte[] input)

```csharp
public static byte[] EncodeStrToRawStdBytes(byte[] input)
```

**Description:** Encode with standard Base64 (no padding) and return UTF-8 bytes of the result string.

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

##### EncodeStrToRawStdBytes(string input)

```csharp
public static byte[] EncodeStrToRawStdBytes(string input)
```

**Description:** Encode UTF-8 text with standard Base64 (no padding) and return UTF-8 bytes of the result string.

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

#### No-Padding Standard Base64 Decoding Methods

##### DecodeBytesFromRawStd(string input)

```csharp
public static byte[] DecodeBytesFromRawStd(string input)
```

**Description:** Restore padding and decode raw standard Base64 to bytes.

**Parameters:**
- `input` (string): Base64 string without padding

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeBytesFromRawStd(byte[] input)

```csharp
public static byte[] DecodeBytesFromRawStd(byte[] input)
```

**Description:** Restore padding and decode UTF-8 bytes of raw standard Base64 to bytes.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the Base64 string

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeStringFromRawStd(string input)

```csharp
public static string DecodeStringFromRawStd(string input)
```

**Description:** Restore padding and decode raw standard Base64 to UTF-8 text.

**Parameters:**
- `input` (string): Base64 string without padding

**Return Value:**
- `string`: Decoded UTF-8 text

##### DecodeStringFromRawStd(byte[] input)

```csharp
public static string DecodeStringFromRawStd(byte[] input)
```

**Description:** Restore padding and decode UTF-8 bytes of raw standard Base64 to UTF-8 text.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the Base64 string

**Return Value:**
- `string`: Decoded UTF-8 text

#### URL-Safe Base64 Encoding Methods

##### EncodeToUrlString(byte[] input)

```csharp
public static string EncodeToUrlString(byte[] input)
```

**Description:** Encode with standard Base64, then map to URL-safe characters (padding retained).

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `string`: URL-safe Base64 string

##### EncodeToUrlString(string input)

```csharp
public static string EncodeToUrlString(string input)
```

**Description:** Encode UTF-8 text with URL-safe Base64 (padding retained).

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `string`: URL-safe Base64 string

##### EncodeToUrlBytes(byte[] input)

```csharp
public static byte[] EncodeToUrlBytes(byte[] input)
```

**Description:** Encode with URL-safe Base64 and return UTF-8 bytes of the result string.

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

##### EncodeToUrlBytes(string input)

```csharp
public static byte[] EncodeToUrlBytes(string input)
```

**Description:** Encode UTF-8 text with URL-safe Base64 and return UTF-8 bytes of the result string.

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

#### URL-Safe Base64 Decoding Methods

##### DecodeBytesFromUrl(string input)

```csharp
public static byte[] DecodeBytesFromUrl(string input)
```

**Description:** Map URL-safe characters to standard alphabet, then decode Base64 to bytes.

**Parameters:**
- `input` (string): URL-safe Base64 string

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeBytesFromUrl(byte[] input)

```csharp
public static byte[] DecodeBytesFromUrl(byte[] input)
```

**Description:** Map URL-safe characters to standard alphabet, then decode UTF-8 bytes of Base64 to bytes.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the URL-safe Base64 string

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeStringFromUrl(string input)

```csharp
public static string DecodeStringFromUrl(string input)
```

**Description:** Map URL-safe characters to standard alphabet, then decode Base64 to UTF-8 text.

**Parameters:**
- `input` (string): URL-safe Base64 string

**Return Value:**
- `string`: Decoded UTF-8 text

##### DecodeStringFromUrl(byte[] input)

```csharp
public static string DecodeStringFromUrl(byte[] input)
```

**Description:** Map URL-safe characters to standard alphabet, then decode UTF-8 bytes of Base64 to UTF-8 text.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the URL-safe Base64 string

**Return Value:**
- `string`: Decoded UTF-8 text

#### No-Padding URL-Safe Base64 Encoding Methods

##### EncodeToRawUrlString(byte[] input)

```csharp
public static string EncodeToRawUrlString(byte[] input)
```

**Description:** Encode with standard Base64, strip padding, then map to URL-safe characters.

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `string`: URL-safe Base64 string without padding

##### EncodeToRawUrlString(string input)

```csharp
public static string EncodeToRawUrlString(string input)
```

**Description:** Encode UTF-8 text with raw URL-safe Base64 (no padding).

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `string`: URL-safe Base64 string without padding

##### EncodeToRawUrlBytes(byte[] input)

```csharp
public static byte[] EncodeToRawUrlBytes(byte[] input)
```

**Description:** Encode with raw URL-safe Base64 and return UTF-8 bytes of the result string.

**Parameters:**
- `input` (byte[]): Bytes to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

##### EncodeToRawUrlBytes(string input)

```csharp
public static byte[] EncodeToRawUrlBytes(string input)
```

**Description:** Encode UTF-8 text with raw URL-safe Base64 and return UTF-8 bytes of the result string.

**Parameters:**
- `input` (string): Text to encode

**Return Value:**
- `byte[]`: UTF-8 bytes of the Base64 string

#### No-Padding URL-Safe Base64 Decoding Methods

##### DecodeBytesFromRawUrl(string input)

```csharp
public static byte[] DecodeBytesFromRawUrl(string input)
```

**Description:** Map URL-safe characters to standard alphabet, restore padding, then decode to bytes.

**Parameters:**
- `input` (string): URL-safe Base64 string without padding

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeBytesFromRawUrl(byte[] input)

```csharp
public static byte[] DecodeBytesFromRawUrl(byte[] input)
```

**Description:** Map URL-safe characters to standard alphabet, restore padding, then decode UTF-8 bytes to bytes.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the URL-safe Base64 string

**Return Value:**
- `byte[]`: Decoded bytes

##### DecodeStringFromRawUrl(string input)

```csharp
public static string DecodeStringFromRawUrl(string input)
```

**Description:** Map URL-safe characters to standard alphabet, restore padding, then decode to UTF-8 text.

**Parameters:**
- `input` (string): URL-safe Base64 string without padding

**Return Value:**
- `string`: Decoded UTF-8 text

##### DecodeStringFromRawUrl(byte[] input)

```csharp
public static string DecodeStringFromRawUrl(byte[] input)
```

**Description:** Map URL-safe characters to standard alphabet, restore padding, then decode UTF-8 bytes to UTF-8 text.

**Parameters:**
- `input` (byte[]): UTF-8 bytes of the URL-safe Base64 string

**Return Value:**
- `string`: Decoded UTF-8 text

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

// No-padding standard Base64 encoding
IBase64Encoding rawEncoder = new Base64RawStdEncoding();
string rawEncoded = rawEncoder.EncodeToString("Hello World");
string rawDecoded = rawEncoder.DecodeStringFrom(rawEncoded);

// No-padding URL-safe Base64 encoding
IBase64Encoding rawUrlEncoder = new Base64RawUrlEncoding();
string rawUrlEncoded = rawUrlEncoder.EncodeToString("Hello World");
string rawUrlDecoded = rawUrlEncoder.DecodeStringFrom(rawUrlEncoded);
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

// Encode to byte array (UTF-8 bytes of the Base64 string)
byte[] encodedBytes = Base64Utils.EncodeToStdBytes(data);
byte[] decodedBytes = Base64Utils.DecodeBytesFromStd(encodedBytes);

// Verify result
string result = Encoding.UTF8.GetString(decodedBytes);
// Result: "Hello World"
```

### Different Encoding Format Comparison

```csharp
byte[] data = { 0xFB, 0xFF };

// Standard Base64 (with padding)
string std = Base64Utils.EncodeToStdString(data);
// Result: "+/8="

// No-padding standard Base64
string rawStd = Base64Utils.EncodeToRawStdString(data);
// Result: "+/8"

// URL-safe Base64 (with padding)
string url = Base64Utils.EncodeToUrlString(data);
// Result: "-_8="

// No-padding URL-safe Base64
string rawUrl = Base64Utils.EncodeToRawUrlString(data);
// Result: "-_8"
```

---

## Notes

1. **Encoding Format:** Standard Base64 uses A-Z, a-z, 0-9, `+`, `/`; the URL-safe variants replace `+` and `/` with `-` and `_`
2. **Padding Characters:** Standard and URL-safe formats use `=` as padding; raw variants strip padding on encode and restore it on decode
3. **Decoding Compatibility:** `DecodeBytesFromRawStd` / `DecodeBytesFromRawUrl` append `=` or `==` based on `length % 4`; URL-safe decode first maps `-`/`_` back to `+`/`/`
4. **Character Encoding:** String inputs are converted to bytes with UTF-8 before encoding; decoded text is interpreted as UTF-8
5. **EncodeToBytes:** Returns UTF-8 bytes of the Base64 **string**, not a second wrapping of the original binary
6. **Method Naming:** The raw-standard encode-to-bytes methods are named `EncodeStrToRawStdBytes` (both the `byte[]` and `string` overloads)
7. **Error Handling:** Invalid Base64 input causes `Convert.FromBase64String` to throw; callers should handle exceptions

---

## Dependencies

- `System`: Basic types (including `Convert`)
- `System.Text`: UTF-8 string encoding
