# JLGames.Infra.Encodingx API 文档

## 概述

Encodingx 模块提供 Base64 编码与解码，覆盖标准、无填充、URL 安全以及无填充 URL 安全四种变体。字符串与文本均按 UTF-8 处理；具体变体规则由 `IBase64Encoding` 的实现类决定，也可通过 `Base64Utils` 静态方法直接调用。

## 命名空间

`JLGames.Infra.Encodingx.Base64x`

---

## 接口

### IBase64Encoding

Base64 编解码抽象；具体变体规则由实现类决定。

```csharp
public interface IBase64Encoding
```

#### 编码方法

##### EncodeToString(byte[] input)

```csharp
string EncodeToString(byte[] input);
```

**描述：** 将二进制数据编码为 Base64 字符串。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `string`: Base64 字符串

##### EncodeToString(string input)

```csharp
string EncodeToString(string input);
```

**描述：** 将 UTF-8 文本编码为 Base64 字符串。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `string`: Base64 字符串

##### EncodeToBytes(byte[] input)

```csharp
byte[] EncodeToBytes(byte[] input);
```

**描述：** 将二进制数据编码为 Base64 字符串的 UTF-8 字节形式。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

##### EncodeToBytes(string input)

```csharp
byte[] EncodeToBytes(string input);
```

**描述：** 将 UTF-8 文本编码为 Base64 字符串的 UTF-8 字节形式。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

#### 解码方法

##### DecodeBytesFrom(byte[] input)

```csharp
byte[] DecodeBytesFrom(byte[] input);
```

**描述：** 将 Base64 字符串的 UTF-8 字节解码为二进制数据。

**参数：**
- `input` (byte[]): Base64 字符串的 UTF-8 字节

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFrom(string input)

```csharp
byte[] DecodeBytesFrom(string input);
```

**描述：** 将 Base64 字符串解码为二进制数据。

**参数：**
- `input` (string): Base64 字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFrom(byte[] input)

```csharp
string DecodeStringFrom(byte[] input);
```

**描述：** 将 Base64 字符串的 UTF-8 字节解码为 UTF-8 文本。

**参数：**
- `input` (byte[]): Base64 字符串的 UTF-8 字节

**返回值：**
- `string`: 解码后的 UTF-8 文本

##### DecodeStringFrom(string input)

```csharp
string DecodeStringFrom(string input);
```

**描述：** 将 Base64 字符串解码为 UTF-8 文本。

**参数：**
- `input` (string): Base64 字符串

**返回值：**
- `string`: 解码后的 UTF-8 文本

---

## 实现类

四个实现类均实现 `IBase64Encoding`，并委托给 `Base64Utils` 中对应的静态方法。公开成员与接口一致，无额外成员。

### Base64StdEncoding

标准 Base64（RFC 4648）：使用 `+`/`/` 及填充符 `=`。

```csharp
public sealed class Base64StdEncoding : IBase64Encoding
```

**示例：**
```csharp
var encoder = new Base64StdEncoding();
string encoded = encoder.EncodeToString("Hello World");
// 结果: "SGVsbG8gV29ybGQ="
```

### Base64RawStdEncoding

无填充标准 Base64：字母表与标准 Base64 相同，省略填充符 `=`。

```csharp
public sealed class Base64RawStdEncoding : IBase64Encoding
```

**示例：**
```csharp
var encoder = new Base64RawStdEncoding();
string encoded = encoder.EncodeToString("Hello World");
// 结果: "SGVsbG8gV29ybGQ"
```

### Base64UrlEncoding

URL 安全 Base64：以 `-`/`_` 替代 `+`/`/`，保留填充符 `=`。

```csharp
public sealed class Base64UrlEncoding : IBase64Encoding
```

**示例：**
```csharp
var encoder = new Base64UrlEncoding();
string encoded = encoder.EncodeToString("Hello World");
// 结果: "SGVsbG8gV29ybGQ="
```

### Base64RawUrlEncoding

无填充 URL 安全 Base64：使用 URL 安全字母表，省略填充符 `=`。

```csharp
public sealed class Base64RawUrlEncoding : IBase64Encoding
```

**示例：**
```csharp
var encoder = new Base64RawUrlEncoding();
string encoded = encoder.EncodeToString("Hello World");
// 结果: "SGVsbG8gV29ybGQ"
```

---

## 工具类

### Base64Utils

标准、无填充、URL 安全及无填充 URL 安全等多种 Base64 变体的静态工具方法。

```csharp
public static class Base64Utils
```

#### 标准 Base64 编码方法

##### EncodeToStdString(byte[] input)

```csharp
public static string EncodeToStdString(byte[] input)
```

**描述：** 按标准 Base64（RFC 4648）编码字节数组。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `string`: Base64 字符串

##### EncodeToStdString(string input)

```csharp
public static string EncodeToStdString(string input)
```

**描述：** 将 UTF-8 文本按标准 Base64（RFC 4648）编码。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `string`: Base64 字符串

##### EncodeToStdBytes(byte[] input)

```csharp
public static byte[] EncodeToStdBytes(byte[] input)
```

**描述：** 按标准 Base64 编码字节数组，并返回结果字符串的 UTF-8 字节。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

##### EncodeToStdBytes(string input)

```csharp
public static byte[] EncodeToStdBytes(string input)
```

**描述：** 将 UTF-8 文本按标准 Base64 编码，并返回结果字符串的 UTF-8 字节。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

#### 标准 Base64 解码方法

##### DecodeBytesFromStd(string input)

```csharp
public static byte[] DecodeBytesFromStd(string input)
```

**描述：** 将标准 Base64 字符串解码为字节数组。

**参数：**
- `input` (string): Base64 字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFromStd(byte[] input)

```csharp
public static byte[] DecodeBytesFromStd(byte[] input)
```

**描述：** 将标准 Base64 字符串的 UTF-8 字节解码为字节数组。

**参数：**
- `input` (byte[]): Base64 字符串的 UTF-8 字节

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFromStd(string input)

```csharp
public static string DecodeStringFromStd(string input)
```

**描述：** 将标准 Base64 字符串解码为 UTF-8 文本。

**参数：**
- `input` (string): Base64 字符串

**返回值：**
- `string`: 解码后的 UTF-8 文本

##### DecodeStringFromStd(byte[] input)

```csharp
public static string DecodeStringFromStd(byte[] input)
```

**描述：** 将标准 Base64 字符串的 UTF-8 字节解码为 UTF-8 文本。

**参数：**
- `input` (byte[]): Base64 字符串的 UTF-8 字节

**返回值：**
- `string`: 解码后的 UTF-8 文本

#### 无填充标准 Base64 编码方法

##### EncodeToRawStdString(byte[] input)

```csharp
public static string EncodeToRawStdString(byte[] input)
```

**描述：** 按标准 Base64 编码并去除填充符 `=`。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `string`: 无填充的 Base64 字符串

##### EncodeToRawStdString(string input)

```csharp
public static string EncodeToRawStdString(string input)
```

**描述：** 将 UTF-8 文本按标准 Base64 编码并去除填充符 `=`。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `string`: 无填充的 Base64 字符串

##### EncodeStrToRawStdBytes(byte[] input)

```csharp
public static byte[] EncodeStrToRawStdBytes(byte[] input)
```

**描述：** 按无填充标准 Base64 编码，并返回结果字符串的 UTF-8 字节。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

##### EncodeStrToRawStdBytes(string input)

```csharp
public static byte[] EncodeStrToRawStdBytes(string input)
```

**描述：** 将 UTF-8 文本按无填充标准 Base64 编码，并返回结果字符串的 UTF-8 字节。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

#### 无填充标准 Base64 解码方法

##### DecodeBytesFromRawStd(string input)

```csharp
public static byte[] DecodeBytesFromRawStd(string input)
```

**描述：** 补全填充符后，将无填充标准 Base64 解码为字节数组。

**参数：**
- `input` (string): 无填充的 Base64 字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFromRawStd(byte[] input)

```csharp
public static byte[] DecodeBytesFromRawStd(byte[] input)
```

**描述：** 补全填充符后，将无填充标准 Base64 的 UTF-8 字节解码为字节数组。

**参数：**
- `input` (byte[]): Base64 字符串的 UTF-8 字节

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFromRawStd(string input)

```csharp
public static string DecodeStringFromRawStd(string input)
```

**描述：** 补全填充符后，将无填充标准 Base64 解码为 UTF-8 文本。

**参数：**
- `input` (string): 无填充的 Base64 字符串

**返回值：**
- `string`: 解码后的 UTF-8 文本

##### DecodeStringFromRawStd(byte[] input)

```csharp
public static string DecodeStringFromRawStd(byte[] input)
```

**描述：** 补全填充符后，将无填充标准 Base64 的 UTF-8 字节解码为 UTF-8 文本。

**参数：**
- `input` (byte[]): Base64 字符串的 UTF-8 字节

**返回值：**
- `string`: 解码后的 UTF-8 文本

#### URL 安全 Base64 编码方法

##### EncodeToUrlString(byte[] input)

```csharp
public static string EncodeToUrlString(byte[] input)
```

**描述：** 按标准 Base64 编码后映射为 URL 安全字符（保留填充符）。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `string`: URL 安全 Base64 字符串

##### EncodeToUrlString(string input)

```csharp
public static string EncodeToUrlString(string input)
```

**描述：** 将 UTF-8 文本按 URL 安全 Base64 编码（保留填充符）。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `string`: URL 安全 Base64 字符串

##### EncodeToUrlBytes(byte[] input)

```csharp
public static byte[] EncodeToUrlBytes(byte[] input)
```

**描述：** 按 URL 安全 Base64 编码，并返回结果字符串的 UTF-8 字节。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

##### EncodeToUrlBytes(string input)

```csharp
public static byte[] EncodeToUrlBytes(string input)
```

**描述：** 将 UTF-8 文本按 URL 安全 Base64 编码，并返回结果字符串的 UTF-8 字节。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

#### URL 安全 Base64 解码方法

##### DecodeBytesFromUrl(string input)

```csharp
public static byte[] DecodeBytesFromUrl(string input)
```

**描述：** 将 URL 安全字符映射回标准字母表后，解码 Base64 为字节数组。

**参数：**
- `input` (string): URL 安全 Base64 字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFromUrl(byte[] input)

```csharp
public static byte[] DecodeBytesFromUrl(byte[] input)
```

**描述：** 将 URL 安全字符映射回标准字母表后，将 Base64 的 UTF-8 字节解码为字节数组。

**参数：**
- `input` (byte[]): URL 安全 Base64 字符串的 UTF-8 字节

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFromUrl(string input)

```csharp
public static string DecodeStringFromUrl(string input)
```

**描述：** 将 URL 安全字符映射回标准字母表后，解码 Base64 为 UTF-8 文本。

**参数：**
- `input` (string): URL 安全 Base64 字符串

**返回值：**
- `string`: 解码后的 UTF-8 文本

##### DecodeStringFromUrl(byte[] input)

```csharp
public static string DecodeStringFromUrl(byte[] input)
```

**描述：** 将 URL 安全字符映射回标准字母表后，将 Base64 的 UTF-8 字节解码为 UTF-8 文本。

**参数：**
- `input` (byte[]): URL 安全 Base64 字符串的 UTF-8 字节

**返回值：**
- `string`: 解码后的 UTF-8 文本

#### 无填充 URL 安全 Base64 编码方法

##### EncodeToRawUrlString(byte[] input)

```csharp
public static string EncodeToRawUrlString(byte[] input)
```

**描述：** 按标准 Base64 编码、去除填充符后映射为 URL 安全字符。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `string`: 无填充的 URL 安全 Base64 字符串

##### EncodeToRawUrlString(string input)

```csharp
public static string EncodeToRawUrlString(string input)
```

**描述：** 将 UTF-8 文本按无填充 URL 安全 Base64 编码。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `string`: 无填充的 URL 安全 Base64 字符串

##### EncodeToRawUrlBytes(byte[] input)

```csharp
public static byte[] EncodeToRawUrlBytes(byte[] input)
```

**描述：** 按无填充 URL 安全 Base64 编码，并返回结果字符串的 UTF-8 字节。

**参数：**
- `input` (byte[]): 待编码的字节数组

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

##### EncodeToRawUrlBytes(string input)

```csharp
public static byte[] EncodeToRawUrlBytes(string input)
```

**描述：** 将 UTF-8 文本按无填充 URL 安全 Base64 编码，并返回结果字符串的 UTF-8 字节。

**参数：**
- `input` (string): 待编码的文本

**返回值：**
- `byte[]`: Base64 字符串的 UTF-8 字节

#### 无填充 URL 安全 Base64 解码方法

##### DecodeBytesFromRawUrl(string input)

```csharp
public static byte[] DecodeBytesFromRawUrl(string input)
```

**描述：** 将 URL 安全字符映射回标准字母表、补全填充符后解码为字节数组。

**参数：**
- `input` (string): 无填充的 URL 安全 Base64 字符串

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeBytesFromRawUrl(byte[] input)

```csharp
public static byte[] DecodeBytesFromRawUrl(byte[] input)
```

**描述：** 将 URL 安全字符映射回标准字母表、补全填充符后，将 Base64 的 UTF-8 字节解码为字节数组。

**参数：**
- `input` (byte[]): URL 安全 Base64 字符串的 UTF-8 字节

**返回值：**
- `byte[]`: 解码后的字节数组

##### DecodeStringFromRawUrl(string input)

```csharp
public static string DecodeStringFromRawUrl(string input)
```

**描述：** 将 URL 安全字符映射回标准字母表、补全填充符后解码为 UTF-8 文本。

**参数：**
- `input` (string): 无填充的 URL 安全 Base64 字符串

**返回值：**
- `string`: 解码后的 UTF-8 文本

##### DecodeStringFromRawUrl(byte[] input)

```csharp
public static string DecodeStringFromRawUrl(byte[] input)
```

**描述：** 将 URL 安全字符映射回标准字母表、补全填充符后，将 Base64 的 UTF-8 字节解码为 UTF-8 文本。

**参数：**
- `input` (byte[]): URL 安全 Base64 字符串的 UTF-8 字节

**返回值：**
- `string`: 解码后的 UTF-8 文本

---

## 使用示例

### 使用接口实现

```csharp
// 标准 Base64 编码
IBase64Encoding stdEncoder = new Base64StdEncoding();
string encoded = stdEncoder.EncodeToString("Hello World");
string decoded = stdEncoder.DecodeStringFrom(encoded);

// URL 安全 Base64 编码
IBase64Encoding urlEncoder = new Base64UrlEncoding();
string urlEncoded = urlEncoder.EncodeToString("Hello World");
string urlDecoded = urlEncoder.DecodeStringFrom(urlEncoded);

// 无填充标准 Base64 编码
IBase64Encoding rawEncoder = new Base64RawStdEncoding();
string rawEncoded = rawEncoder.EncodeToString("Hello World");
string rawDecoded = rawEncoder.DecodeStringFrom(rawEncoded);

// 无填充 URL 安全 Base64 编码
IBase64Encoding rawUrlEncoder = new Base64RawUrlEncoding();
string rawUrlEncoded = rawUrlEncoder.EncodeToString("Hello World");
string rawUrlDecoded = rawUrlEncoder.DecodeStringFrom(rawUrlEncoded);
```

### 使用工具类

```csharp
// 标准 Base64 编码
string stdEncoded = Base64Utils.EncodeToStdString("Hello World");
string stdDecoded = Base64Utils.DecodeStringFromStd(stdEncoded);

// 无填充标准 Base64 编码
string rawStdEncoded = Base64Utils.EncodeToRawStdString("Hello World");
string rawStdDecoded = Base64Utils.DecodeStringFromRawStd(rawStdEncoded);

// URL 安全 Base64 编码
string urlEncoded = Base64Utils.EncodeToUrlString("Hello World");
string urlDecoded = Base64Utils.DecodeStringFromUrl(urlEncoded);

// 无填充 URL 安全 Base64 编码
string rawUrlEncoded = Base64Utils.EncodeToRawUrlString("Hello World");
string rawUrlDecoded = Base64Utils.DecodeStringFromRawUrl(rawUrlEncoded);
```

### 字节数组编码

```csharp
byte[] data = Encoding.UTF8.GetBytes("Hello World");

// 编码为字节数组（结果为 Base64 字符串的 UTF-8 字节）
byte[] encodedBytes = Base64Utils.EncodeToStdBytes(data);
byte[] decodedBytes = Base64Utils.DecodeBytesFromStd(encodedBytes);

// 验证结果
string result = Encoding.UTF8.GetString(decodedBytes);
// 结果: "Hello World"
```

### 不同编码格式对比

```csharp
byte[] data = { 0xFB, 0xFF };

// 标准 Base64（带填充）
string std = Base64Utils.EncodeToStdString(data);
// 结果: "+/8="

// 无填充标准 Base64
string rawStd = Base64Utils.EncodeToRawStdString(data);
// 结果: "+/8"

// URL 安全 Base64（带填充）
string url = Base64Utils.EncodeToUrlString(data);
// 结果: "-_8="

// 无填充 URL 安全 Base64
string rawUrl = Base64Utils.EncodeToRawUrlString(data);
// 结果: "-_8"
```

---

## 注意事项

1. **编码格式：** 标准 Base64 使用 A-Z、a-z、0-9、`+`、`/`；URL 安全版本将 `+` 和 `/` 替换为 `-` 和 `_`
2. **填充字符：** 标准与 URL 安全格式使用 `=` 作为填充；无填充版本编码时删除填充，解码时按长度补全
3. **解码兼容性：** `DecodeBytesFromRawStd` / `DecodeBytesFromRawUrl` 会按 `length % 4` 补 `=` 或 `==`；URL 安全解码会先把 `-`/`_` 映射回 `+`/`/`
4. **字符编码：** 字符串入参按 UTF-8 转为字节后再编码；解码得到的文本同样按 UTF-8 解释
5. **EncodeToBytes：** 返回的是 Base64 **字符串** 的 UTF-8 字节，不是原始二进制的另一种封装
6. **方法命名：** 无填充标准变体编码到字节的方法名为 `EncodeStrToRawStdBytes`（`byte[]` 与 `string` 均为此名）
7. **错误处理：** 无效 Base64 输入会由 `Convert.FromBase64String` 抛出异常，调用方需自行处理

---

## 依赖关系

- `System`：基础类型（含 `Convert`）
- `System.Text`：UTF-8 字符串编解码
