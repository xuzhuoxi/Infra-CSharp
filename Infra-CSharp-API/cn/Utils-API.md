# JLGames.Infra.Utils API 文档

## 概述

Utils模块提供了丰富的工具类集合，包括数组操作、位操作、文件操作、路径处理、文本处理、反射工具、加密工具等实用功能。

## 命名空间

`JLGames.Infra.Utils`

---

## 工具类

### ArrayUtil

数组操作工具类，提供数组的创建、合并、拆分、旋转等操作。

```csharp
public static class ArrayUtil
```

#### 主要方法

##### CloneArray<T, TK>(TK[] source)

```csharp
public static T[] CloneArray<T, TK>(TK[] source) where T : class where TK : class
```

**描述：** 克隆数组

**参数：**
- `source` (TK[]): 源数组

**返回值：**
- `T[]`: 克隆后的数组

##### NewArray<T>(int len, T @default)

```csharp
public static T[] NewArray<T>(int len, T @default)
```

**描述：** 创建一维数组并赋值

**参数：**
- `len` (int): 数组长度
- `@default` (T): 默认值

**返回值：**
- `T[]`: 创建的一维数组

##### NewArray<T>(int yLen, int xLen)

```csharp
public static T[][] NewArray<T>(int yLen, int xLen)
```

**描述：** 创建二维数组

**参数：**
- `yLen` (int): 行数
- `xLen` (int): 列数

**返回值：**
- `T[][]`: 创建的二维数组

##### MergeArray<T>(T first, T[] second)

```csharp
public static T[] MergeArray<T>(T first, T[] second)
```

**描述：** 合并数组

**参数：**
- `first` (T): 第一个元素
- `second` (T[]): 第二个数组

**返回值：**
- `T[]`: 合并后的数组(第一个元素+第二个数组，长度为两者长度和)

##### MergeArray<T>(T[] first, T second)

```csharp
public static T[] MergeArray<T>(T[] first, T second)
```

**描述：** 合并数组

**参数：**
- `first` (T[]): 第一个数组
- `second` (T): 第二个元素

**返回值：**
- `T[]`: 合并后的数组(第一个数组+第二个元素，长度为两者长度和)

##### MergeArray<T>(T[] first, T[] second)

```csharp
public static T[] MergeArray<T>(T[] first, T[] second)
```

**描述：** 合并数组

**参数：**
- `first` (T[]): 第一个数组
- `second` (T[]): 第二个数组

**返回值：**
- `T[]`: 合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)

##### SubArray<T>(T[] source, int startIndex, int len)

```csharp
public static T[] SubArray<T>(T[] source, int startIndex, int len)
```

**描述：** 截取数组子集

**参数：**
- `source` (T[]): 源数组
- `startIndex` (int): 开始索引
- `len` (int): 长度

**返回值：**
- `T[]`: 截取的子数组

##### Rotated<T>(T[][] array)

```csharp
public static T[][] Rotated<T>(T[][] array)
```

**描述：** 旋转二维数组90度

**参数：**
- `array` (T[][]): 源二维数组

**返回值：**
- `T[][]`: 旋转后的二维数组

---

### BitUtil

位操作工具类，提供位级别的操作功能。

```csharp
public static class BitUtil
```

#### 主要方法

##### IsValid<T>(T value, int bitIndex)

```csharp
public static bool IsValid(sbyte value, int bitIndex)
public static bool IsValid(ushort value, int bitIndex)
public static bool IsValid(int value, int bitIndex)
public static bool IsValid(uint value, int bitIndex)
public static bool IsValid(long value, int bitIndex)
public static bool IsValid(ulong value, int bitIndex)
```

**描述：** 是否有效。1为效，0为无效

**参数：**
- `value` (T): 待检测值
- `bitIndex` (int): 从低位开始，第1位索引为0

**返回值：**
- `bool`: 位是否有效

##### IsValidAnd<T>(T value, int bitIndex, params int[] otherIndexs)

```csharp
public static bool IsValidAnd(sbyte value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(ushort value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(int value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(uint value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(long value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(ulong value, int bitIndex, params int[] otherIndexs)
```

**描述：** 检查位是否全部为有效

**参数：**
- `value` (T): 待检测值
- `bitIndex` (int): 位索引
- `otherIndexs` (int[]): 其他位索引

**返回值：**
- `bool`: 所有位是否都有效

##### SetValid<T>(T value, int bitIndex, bool isValid)

```csharp
public static sbyte SetValid(sbyte value, int bitIndex, bool isValid)
public static ushort SetValid(ushort value, int bitIndex, bool isValid)
public static int SetValid(int value, int bitIndex, bool isValid)
public static uint SetValid(uint value, int bitIndex, bool isValid)
public static long SetValid(long value, int bitIndex, bool isValid)
public static ulong SetValid(ulong value, int bitIndex, bool isValid)
```

**描述：** 设置位的有效性

**参数：**
- `value` (T): 原值
- `bitIndex` (int): 位索引
- `isValid` (bool): 是否有效

**返回值：**
- `T`: 设置后的值

---

### FileUtil

文件操作工具类，提供文件的读写、复制、删除等操作。

```csharp
public static class FileUtil
```

#### 主要方法

##### ReadAllText(string path)

```csharp
public static string ReadAllText(string path)
```

**描述：** 读取文件全部内容

**参数：**
- `path` (string): 文件路径

**返回值：**
- `string`: 文件内容

##### WriteAllText(string path, string content)

```csharp
public static void WriteAllText(string path, string content)
```

**描述：** 写入文件全部内容

**参数：**
- `path` (string): 文件路径
- `content` (string): 要写入的内容

##### CopyFile(string sourcePath, string targetPath)

```csharp
public static void CopyFile(string sourcePath, string targetPath)
```

**描述：** 复制文件

**参数：**
- `sourcePath` (string): 源文件路径
- `targetPath` (string): 目标文件路径

##### DeleteFile(string path)

```csharp
public static void DeleteFile(string path)
```

**描述：** 删除文件

**参数：**
- `path` (string): 文件路径

---

### PathUtil

路径处理工具类，提供路径的解析、组合、验证等操作。

```csharp
public static class PathUtil
```

#### 主要方法

##### Combine(params string[] paths)

```csharp
public static string Combine(params string[] paths)
```

**描述：** 组合路径

**参数：**
- `paths` (string[]): 路径数组

**返回值：**
- `string`: 组合后的路径

##### GetDirectoryName(string path)

```csharp
public static string GetDirectoryName(string path)
```

**描述：** 获取目录名

**参数：**
- `path` (string): 路径

**返回值：**
- `string`: 目录名

##### GetFileName(string path)

```csharp
public static string GetFileName(string path)
```

**描述：** 获取文件名

**参数：**
- `path` (string): 路径

**返回值：**
- `string`: 文件名

##### GetExtension(string path)

```csharp
public static string GetExtension(string path)
```

**描述：** 获取文件扩展名

**参数：**
- `path` (string): 路径

**返回值：**
- `string`: 文件扩展名

---

### TextUtil

文本处理工具类，提供字符串的处理、格式化等操作。

```csharp
public static class TextUtil
```

#### 主要方法

##### IsNullOrEmpty(string text)

```csharp
public static bool IsNullOrEmpty(string text)
```

**描述：** 检查字符串是否为空或null

**参数：**
- `text` (string): 待检查的字符串

**返回值：**
- `bool`: 是否为空或null

##### Format(string format, params object[] args)

```csharp
public static string Format(string format, params object[] args)
```

**描述：** 格式化字符串

**参数：**
- `format` (string): 格式字符串
- `args` (object[]): 参数数组

**返回值：**
- `string`: 格式化后的字符串

---

### DirectoryUtil

目录操作工具类，提供目录的创建、删除、遍历等操作。

```csharp
public static class DirectoryUtil
```

#### 主要方法

##### CreateDirectory(string path)

```csharp
public static void CreateDirectory(string path)
```

**描述：** 创建目录

**参数：**
- `path` (string): 目录路径

##### DeleteDirectory(string path)

```csharp
public static void DeleteDirectory(string path)
```

**描述：** 删除目录

**参数：**
- `path` (string): 目录路径

##### GetFiles(string path, string searchPattern = "*")

```csharp
public static string[] GetFiles(string path, string searchPattern = "*")
```

**描述：** 获取目录下的文件

**参数：**
- `path` (string): 目录路径
- `searchPattern` (string): 搜索模式

**返回值：**
- `string[]`: 文件路径数组

---

### ReflexUtil

反射工具类，提供反射相关的操作。

```csharp
public static class ReflexUtil
```

#### 主要方法

##### GetType(string typeName)

```csharp
public static Type GetType(string typeName)
```

**描述：** 根据类型名获取类型

**参数：**
- `typeName` (string): 类型名

**返回值：**
- `Type`: 类型对象

##### CreateInstance<T>(string typeName)

```csharp
public static T CreateInstance<T>(string typeName)
```

**描述：** 创建类型实例

**参数：**
- `typeName` (string): 类型名

**返回值：**
- `T`: 创建的实例

---

### PrintUtil

打印工具类，提供控制台输出格式化功能。

```csharp
public static class PrintUtil
```

#### 主要方法

##### Print(string message)

```csharp
public static void Print(string message)
```

**描述：** 打印消息

**参数：**
- `message` (string): 要打印的消息

##### PrintLine(string message)

```csharp
public static void PrintLine(string message)
```

**描述：** 打印消息并换行

**参数：**
- `message` (string): 要打印的消息

---

### ConfusedUtil

混淆工具类，提供数据混淆功能。

```csharp
public static class ConfusedUtil
```

#### 主要方法

##### Confuse(byte[] data)

```csharp
public static void Confuse(byte[] data)
```

**描述：** 混淆数据

**参数：**
- `data` (byte[]): 要混淆的数据

##### Deconfuse(byte[] data)

```csharp
public static void Deconfuse(byte[] data)
```

**描述：** 解混淆数据

**参数：**
- `data` (byte[]): 要解混淆的数据

---

## 加密工具类

### DESUtil

DES加密工具类。

```csharp
public static class DESUtil
```

#### 主要方法

##### Encrypt(string data, string key)

```csharp
public static string Encrypt(string data, string key)
```

**描述：** DES加密

**参数：**
- `data` (string): 要加密的数据
- `key` (string): 密钥

**返回值：**
- `string`: 加密后的数据

##### Decrypt(string data, string key)

```csharp
public static string Decrypt(string data, string key)
```

**描述：** DES解密

**参数：**
- `data` (string): 要解密的数据
- `key` (string): 密钥

**返回值：**
- `string`: 解密后的数据

---

### RijandelUtil

Rijndael加密工具类。

```csharp
public static class RijandelUtil
```

#### 主要方法

##### Encrypt(string data, string key, string iv)

```csharp
public static string Encrypt(string data, string key, string iv)
```

**描述：** Rijndael加密

**参数：**
- `data` (string): 要加密的数据
- `key` (string): 密钥
- `iv` (string): 初始化向量

**返回值：**
- `string`: 加密后的数据

##### Decrypt(string data, string key, string iv)

```csharp
public static string Decrypt(string data, string key, string iv)
```

**描述：** Rijndael解密

**参数：**
- `data` (string): 要解密的数据
- `key` (string): 密钥
- `iv` (string): 初始化向量

**返回值：**
- `string`: 解密后的数据

---

### RSAUtil

RSA加密工具类。

```csharp
public static class RSAUtil
```

#### 主要方法

##### Encrypt(string data, string publicKey)

```csharp
public static string Encrypt(string data, string publicKey)
```

**描述：** RSA加密

**参数：**
- `data` (string): 要加密的数据
- `publicKey` (string): 公钥

**返回值：**
- `string`: 加密后的数据

##### Decrypt(string data, string privateKey)

```csharp
public static string Decrypt(string data, string privateKey)
```

**描述：** RSA解密

**参数：**
- `data` (string): 要解密的数据
- `privateKey` (string): 私钥

**返回值：**
- `string`: 解密后的数据

---

## 使用示例

### 数组操作

```csharp
// 创建数组
var array1 = ArrayUtil.NewArray<int>(5, 0);
var array2 = ArrayUtil.NewArray<string>(3, 2, "default");

// 合并数组
var merged = ArrayUtil.MergeArray(array1, new int[] { 6, 7, 8 });

// 截取子数组
var subArray = ArrayUtil.SubArray(merged, 1, 3);
```

### 位操作

```csharp
// 检查位是否有效
bool isValid = BitUtil.IsValid(15, 2); // 检查第2位是否为1

// 设置位
int result = BitUtil.SetValid(8, 1, true); // 设置第1位为1

// 检查多个位
bool allValid = BitUtil.IsValidAnd(15, 0, 1, 2); // 检查第0、1、2位是否都为1
```

### 文件操作

```csharp
// 读取文件
string content = FileUtil.ReadAllText("test.txt");

// 写入文件
FileUtil.WriteAllText("output.txt", "Hello World");

// 复制文件
FileUtil.CopyFile("source.txt", "target.txt");
```

### 路径操作

```csharp
// 组合路径
string path = PathUtil.Combine("C:", "Users", "Documents", "file.txt");

// 获取文件名
string fileName = PathUtil.GetFileName(path);

// 获取扩展名
string extension = PathUtil.GetExtension(path);
```

### 加密操作

```csharp
// DES加密
string encrypted = DESUtil.Encrypt("Hello World", "mykey123");

// DES解密
string decrypted = DESUtil.Decrypt(encrypted, "mykey123");

// RSA加密
string rsaEncrypted = RSAUtil.Encrypt("Hello World", publicKey);

// RSA解密
string rsaDecrypted = RSAUtil.Decrypt(rsaEncrypted, privateKey);
```

---

## 注意事项

1. **性能考虑：** 大量数组操作时注意内存使用
2. **位操作：** 位索引从0开始，注意边界检查
3. **文件操作：** 确保文件路径存在且有相应权限
4. **加密安全：** 妥善保管密钥，避免硬编码
5. **反射性能：** 反射操作性能较低，避免频繁使用

---

## 依赖关系

- `System`: 基础类型和集合
- `System.Runtime.CompilerServices`: 编译器服务
- `System.Linq`: LINQ查询功能 