# JLGames.Infra.Encodingx API 文档

## 概述

Encodingx模块提供了Base64编码和解码功能，支持标准Base64、URL安全的Base64以及无填充的Base64编码格式。

## 命名空间

`JLGames.Infra.Encodingx.Base64x`

---

## 接口

### IBase64Encoding

Base64编码接口，定义了Base64编码和解码的基本操作。

```csharp
public interface IBase64Encoding
```

#### 编码方法

##### EncodeToString(byte[] input)

```csharp
string EncodeToString(byte[] input);
```

**描述：** 编码

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `string`: 编码后的字符串

##### EncodeToString(string input)

```csharp
string EncodeToString(string input);
```

**描述：** 编码

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `string`: 编码后的字符串

##### EncodeToBytes(byte[] input)

```csharp
byte[] EncodeToBytes(byte[] input);
```

**描述：** 编码

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `byte[]`: 编码后的字节数组

##### EncodeToBytes(string input)

```csharp
byte[] EncodeToBytes(string input);
```

**描述：** 编码

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `byte[]`: 编码后的字节数组

#### 解码方法

##### DecodeBytesFrom(byte[] input)

```csharp
byte[] DecodeBytesFrom(byte[] input);
```

**描述：** 解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFrom(string input)

```csharp
byte[] DecodeBytesFrom(string input);
```

**描述：** 解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFrom(byte[] input)

```csharp
string DecodeStringFrom(byte[] input);
```

**描述：** 解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `string`: 解码后的字符串

##### DecodeStringFrom(string input)

```csharp
string DecodeStringFrom(string input);
```

**描述：** 解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `string`: 解码后的字符串

---

## 实现类

### Base64StdEncoding

标准Base64编码实现类。

```csharp
public sealed class Base64StdEncoding : IBase64Encoding
```

**描述：** 使用标准Base64编码格式，包含填充字符（=）

**示例：**
```csharp
var encoder = new Base64StdEncoding();
string encoded = encoder.EncodeToString("Hello World");
// 结果: "SGVsbG8gV29ybGQ="
```

### Base64RawStdEncoding

无填充标准Base64编码实现类。

```csharp
public sealed class Base64RawStdEncoding : IBase64Encoding
```

**描述：** 使用标准Base64编码格式，但不包含填充字符（=）

**示例：**
```csharp
var encoder = new Base64RawStdEncoding();
string encoded = encoder.EncodeToString("Hello World");
// 结果: "SGVsbG8gV29ybGQ"
```

### Base64UrlEncoding

URL安全Base64编码实现类。

```csharp
public sealed class Base64UrlEncoding : IBase64Encoding
```

**描述：** 使用URL安全的Base64编码格式，将+和/替换为-和_，保留填充字符

**示例：**
```csharp
var encoder = new Base64UrlEncoding();
string encoded = encoder.EncodeToString("Hello World");
// 结果: "SGVsbG8gV29ybGQ="
```

### Base64RawUrlEncoding

无填充URL安全Base64编码实现类。

```csharp
public sealed class Base64RawUrlEncoding : IBase64Encoding
```

**描述：** 使用URL安全的Base64编码格式，将+和/替换为-和_，不包含填充字符

**示例：**
```csharp
var encoder = new Base64RawUrlEncoding();
string encoded = encoder.EncodeToString("Hello World");
// 结果: "SGVsbG8gV29ybGQ"
```

---

## 工具类

### Base64Utils

Base64编码工具类，提供静态方法进行Base64编码和解码操作。

```csharp
public static class Base64Utils
```

#### 标准Base64编码方法

##### EncodeToStdString(byte[] input)

```csharp
public static string EncodeToStdString(byte[] input)
```

**描述：** 按标准Base64编码

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `string`: 编码后的字符串

##### EncodeToStdString(string input)

```csharp
public static string EncodeToStdString(string input)
```

**描述：** 按标准Base64编码，先把字符串按UTF8编码处理为字符数组

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `string`: 编码后的字符串

##### EncodeToStdBytes(byte[] input)

```csharp
public static byte[] EncodeToStdBytes(byte[] input)
```

**描述：** 按标准Base64编码

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `byte[]`: 编码后的字节数组

##### EncodeToStdBytes(string input)

```csharp
public static byte[] EncodeToStdBytes(string input)
```

**描述：** 按标准Base64编码

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `byte[]`: 编码后的字节数组

#### 标准Base64解码方法

##### DecodeBytesFromStd(string input)

```csharp
public static byte[] DecodeBytesFromStd(string input)
```

**描述：** 按标准Base64解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFromStd(byte[] input)

```csharp
public static byte[] DecodeBytesFromStd(byte[] input)
```

**描述：** 按标准Base64解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFromStd(string input)

```csharp
public static string DecodeStringFromStd(string input)
```

**描述：** 按标准Base64解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `string`: 解码后的字符串

##### DecodeStringFromStd(byte[] input)

```csharp
public static string DecodeStringFromStd(byte[] input)
```

**描述：** 按标准Base64解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `string`: 解码后的字符串

#### 无填充标准Base64编码方法

##### EncodeToRawStdString(byte[] input)

```csharp
public static string EncodeToRawStdString(byte[] input)
```

**描述：** 1. 按标准Base64编码 2. 删除填充

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `string`: 编码后的字符串

##### EncodeToRawStdString(string input)

```csharp
public static string EncodeToRawStdString(string input)
```

**描述：** 1. 按标准Base64编码 2. 删除填充

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `string`: 编码后的字符串

##### EncodeStrToRawStdBytes(byte[] input)

```csharp
public static byte[] EncodeStrToRawStdBytes(byte[] input)
```

**描述：** 1. 按标准Base64编码 2. 删除填充

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `byte[]`: 编码后的字节数组

##### EncodeStrToRawStdBytes(string input)

```csharp
public static byte[] EncodeStrToRawStdBytes(string input)
```

**描述：** 1. 按标准Base64编码 2. 删除填充

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `byte[]`: 编码后的字节数组

#### 无填充标准Base64解码方法

##### DecodeBytesFromRawStd(string input)

```csharp
public static byte[] DecodeBytesFromRawStd(string input)
```

**描述：** 1. 补充填充 2. 按标准Base64解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFromRawStd(byte[] input)

```csharp
public static byte[] DecodeBytesFromRawStd(byte[] input)
```

**描述：** 1. 补充填充 2. 按标准Base64解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFromRawStd(string input)

```csharp
public static string DecodeStringFromRawStd(string input)
```

**描述：** 1. 补充填充 2. 按标准Base64解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `string`: 解码后的字符串

##### DecodeStringFromRawStd(byte[] input)

```csharp
public static string DecodeStringFromRawStd(byte[] input)
```

**描述：** 1. 补充填充 2. 按标准Base64解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `string`: 解码后的字符串

#### URL安全Base64编码方法

##### EncodeToUrlString(byte[] input)

```csharp
public static string EncodeToUrlString(byte[] input)
```

**描述：** 1. 按标准Base64编码 2. 替换为 URL 安全字符（保留填充）

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `string`: 编码后的字符串

##### EncodeToUrlString(string input)

```csharp
public static string EncodeToUrlString(string input)
```

**描述：** 1. 按标准Base64编码 2. 替换为 URL 安全字符（保留填充）

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `string`: 编码后的字符串

##### EncodeToUrlBytes(byte[] input)

```csharp
public static byte[] EncodeToUrlBytes(byte[] input)
```

**描述：** 1. 按标准Base64编码 2. 替换为 URL 安全字符（保留填充）

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `byte[]`: 编码后的字节数组

##### EncodeToUrlBytes(string input)

```csharp
public static byte[] EncodeToUrlBytes(string input)
```

**描述：** 1. 按标准Base64编码 2. 替换为 URL 安全字符（保留填充）

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `byte[]`: 编码后的字节数组

#### URL安全Base64解码方法

##### DecodeBytesFromUrl(string input)

```csharp
public static byte[] DecodeBytesFromUrl(string input)
```

**描述：** 1. 替换为 URL 安全字符 2. 按标准Base64解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFromUrl(byte[] input)

```csharp
public static byte[] DecodeBytesFromUrl(byte[] input)
```

**描述：** 1. 替换为 URL 安全字符 2. 按标准Base64解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFromUrl(string input)

```csharp
public static string DecodeStringFromUrl(string input)
```

**描述：** 1. 替换为 URL 安全字符 2. 按标准Base64解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `string`: 解码后的字符串

##### DecodeStringFromUrl(byte[] input)

```csharp
public static string DecodeStringFromUrl(byte[] input)
```

**描述：** 1. 替换为 URL 安全字符 2. 按标准Base64解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `string`: 解码后的字符串

#### 无填充URL安全Base64编码方法

##### EncodeToRawUrlString(byte[] input)

```csharp
public static string EncodeToRawUrlString(byte[] input)
```

**描述：** 1. 按标准Base64编码 2. 替换为 URL 安全字符 3. 删除填充

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `string`: 编码后的字符串

##### EncodeToRawUrlString(string input)

```csharp
public static string EncodeToRawUrlString(string input)
```

**描述：** 1. 按标准Base64编码 2. 替换为 URL 安全字符 3. 删除填充

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `string`: 编码后的字符串

##### EncodeToRawUrlBytes(byte[] input)

```csharp
public static byte[] EncodeToRawUrlBytes(byte[] input)
```

**描述：** 1. 按标准Base64编码 2. 替换为 URL 安全字符 3. 删除填充

**参数：**
- `input` (byte[]): 要编码的字节数组

**返回值：**
- `byte[]`: 编码后的字节数组

##### EncodeToRawUrlBytes(string input)

```csharp
public static byte[] EncodeToRawUrlBytes(string input)
```

**描述：** 1. 按标准Base64编码 2. 替换为 URL 安全字符 3. 删除填充

**参数：**
- `input` (string): 要编码的字符串

**返回值：**
- `byte[]`: 编码后的字节数组

#### 无填充URL安全Base64解码方法

##### DecodeBytesFromRawUrl(string input)

```csharp
public static byte[] DecodeBytesFromRawUrl(string input)
```

**描述：** 1. 补充填充 2. 替换为 URL 安全字符 3. 按标准Base64解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFromRawUrl(byte[] input)

```csharp
public static byte[] DecodeBytesFromRawUrl(byte[] input)
```

**描述：** 1. 补充填充 2. 替换为 URL 安全字符 3. 按标准Base64解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFromRawUrl(string input)

```csharp
public static string DecodeStringFromRawUrl(string input)
```

**描述：** 1. 补充填充 2. 替换为 URL 安全字符 3. 按标准Base64解码

**参数：**
- `input` (string): 要解码的字符串

**返回值：**
- `string`: 解码后的字符串

##### DecodeStringFromRawUrl(byte[] input)

```csharp
public static string DecodeStringFromRawUrl(byte[] input)
```

**描述：** 1. 补充填充 2. 替换为 URL 安全字符 3. 按标准Base64解码

**参数：**
- `input` (byte[]): 要解码的字节数组

**返回值：**
- `string`: 解码后的字符串

---

## 使用示例

### 使用接口实现

```csharp
// 标准Base64编码
IBase64Encoding stdEncoder = new Base64StdEncoding();
string encoded = stdEncoder.EncodeToString("Hello World");
string decoded = stdEncoder.DecodeStringFrom(encoded);

// URL安全Base64编码
IBase64Encoding urlEncoder = new Base64UrlEncoding();
string urlEncoded = urlEncoder.EncodeToString("Hello World");
string urlDecoded = urlEncoder.DecodeStringFrom(urlEncoded);

// 无填充Base64编码
IBase64Encoding rawEncoder = new Base64RawStdEncoding();
string rawEncoded = rawEncoder.EncodeToString("Hello World");
string rawDecoded = rawEncoder.DecodeStringFrom(rawEncoded);
```

### 使用工具类

```csharp
// 标准Base64编码
string stdEncoded = Base64Utils.EncodeToStdString("Hello World");
string stdDecoded = Base64Utils.DecodeStringFromStd(stdEncoded);

// 无填充标准Base64编码
string rawStdEncoded = Base64Utils.EncodeToRawStdString("Hello World");
string rawStdDecoded = Base64Utils.DecodeStringFromRawStd(rawStdEncoded);

// URL安全Base64编码
string urlEncoded = Base64Utils.EncodeToUrlString("Hello World");
string urlDecoded = Base64Utils.DecodeStringFromUrl(urlEncoded);

// 无填充URL安全Base64编码
string rawUrlEncoded = Base64Utils.EncodeToRawUrlString("Hello World");
string rawUrlDecoded = Base64Utils.DecodeStringFromRawUrl(rawUrlEncoded);
```

### 字节数组编码

```csharp
byte[] data = Encoding.UTF8.GetBytes("Hello World");

// 编码为字节数组
byte[] encodedBytes = Base64Utils.EncodeToStdBytes(data);
byte[] decodedBytes = Base64Utils.DecodeBytesFromStd(encodedBytes);

// 验证结果
string result = Encoding.UTF8.GetString(decodedBytes);
// 结果: "Hello World"
```

### 不同编码格式对比

```csharp
string original = "Hello World!";

// 标准Base64（带填充）
string std = Base64Utils.EncodeToStdString(original);
// 结果: "SGVsbG8gV29ybGQh"

// 无填充标准Base64
string rawStd = Base64Utils.EncodeToRawStdString(original);
// 结果: "SGVsbG8gV29ybGQh"

// URL安全Base64（带填充）
string url = Base64Utils.EncodeToUrlString(original);
// 结果: "SGVsbG8gV29ybGQh"

// 无填充URL安全Base64
string rawUrl = Base64Utils.EncodeToRawUrlString(original);
// 结果: "SGVsbG8gV29ybGQh"
```

---

## 注意事项

1. **编码格式：** 标准Base64使用A-Z、a-z、0-9、+、/字符，URL安全版本将+和/替换为-和_
2. **填充字符：** 标准格式使用=作为填充，无填充版本会删除填充字符
3. **解码兼容性：** 无填充版本在解码时会自动补充填充字符
4. **字符编码：** 字符串编码默认使用UTF-8编码
5. **性能考虑：** 对于大量数据，建议使用字节数组方法而不是字符串方法
6. **错误处理：** 解码无效的Base64字符串会抛出异常，注意异常处理

---

## 依赖关系

- `System`: 基础类型
- `System.Text`: 字符串编码功能 