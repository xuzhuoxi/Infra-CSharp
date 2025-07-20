# JLGames.Infra.Utils API Documentation

## Overview

The Utils module provides a rich collection of utility classes, including array operations, bit operations, file operations, path processing, text processing, reflection tools, encryption tools and other practical functions.

## Namespace

`JLGames.Infra.Utils`

---

## Utility Classes

### ArrayUtil

Array operation utility class that provides array creation, merging, splitting, rotation and other operations.

```csharp
public static class ArrayUtil
```

#### Main Methods

##### CloneArray<T, TK>(TK[] source)

```csharp
public static T[] CloneArray<T, TK>(TK[] source) where T : class where TK : class
```

**Description:** Clone array

**Parameters:**
- `source` (TK[]): Source array

**Return Value:**
- `T[]`: Cloned array

##### NewArray<T>(int len, T @default)

```csharp
public static T[] NewArray<T>(int len, T @default)
```

**Description:** Create a 1D array and set the values

**Parameters:**
- `len` (int): Array length
- `@default` (T): Default value

**Return Value:**
- `T[]`: Created 1D array

##### NewArray<T>(int yLen, int xLen)

```csharp
public static T[][] NewArray<T>(int yLen, int xLen)
```

**Description:** Create a 2D array

**Parameters:**
- `yLen` (int): Number of rows
- `xLen` (int): Number of columns

**Return Value:**
- `T[][]`: Created 2D array

##### MergeArray<T>(T first, T[] second)

```csharp
public static T[] MergeArray<T>(T first, T[] second)
```

**Description:** Merge array

**Parameters:**
- `first` (T): First element
- `second` (T[]): Second array

**Return Value:**
- `T[]`: Merged array (first element + second array, length is sum of both lengths)

##### MergeArray<T>(T[] first, T second)

```csharp
public static T[] MergeArray<T>(T[] first, T second)
```

**Description:** Merge array

**Parameters:**
- `first` (T[]): First array
- `second` (T): Second element

**Return Value:**
- `T[]`: Merged array (first array + second element, length is sum of both lengths)

##### MergeArray<T>(T[] first, T[] second)

```csharp
public static T[] MergeArray<T>(T[] first, T[] second)
```

**Description:** Merge array

**Parameters:**
- `first` (T[]): First array
- `second` (T[]): Second array

**Return Value:**
- `T[]`: Merged array (first array + second array, length is sum of both array lengths)

##### SubArray<T>(T[] source, int startIndex, int len)

```csharp
public static T[] SubArray<T>(T[] source, int startIndex, int len)
```

**Description:** Extract array subset

**Parameters:**
- `source` (T[]): Source array
- `startIndex` (int): Start index
- `len` (int): Length

**Return Value:**
- `T[]`: Extracted sub-array

##### Rotated<T>(T[][] array)

```csharp
public static T[][] Rotated<T>(T[][] array)
```

**Description:** Rotate 2D array by 90 degrees

**Parameters:**
- `array` (T[][]): Source 2D array

**Return Value:**
- `T[][]`: Rotated 2D array

---

### BitUtil

Bit operation utility class that provides bit-level operation functionality.

```csharp
public static class BitUtil
```

#### Main Methods

##### IsValid<T>(T value, int bitIndex)

```csharp
public static bool IsValid(sbyte value, int bitIndex)
public static bool IsValid(ushort value, int bitIndex)
public static bool IsValid(int value, int bitIndex)
public static bool IsValid(uint value, int bitIndex)
public static bool IsValid(long value, int bitIndex)
public static bool IsValid(ulong value, int bitIndex)
```

**Description:** Is it effective. 1 is valid, 0 is invalid

**Parameters:**
- `value` (T): Value to be detected
- `bitIndex` (int): Starting from the low order, the first bit index is 0

**Return Value:**
- `bool`: Whether the bit is valid

##### IsValidAnd<T>(T value, int bitIndex, params int[] otherIndexs)

```csharp
public static bool IsValidAnd(sbyte value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(ushort value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(int value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(uint value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(long value, int bitIndex, params int[] otherIndexs)
public static bool IsValidAnd(ulong value, int bitIndex, params int[] otherIndexs)
```

**Description:** Check if all bits are valid

**Parameters:**
- `value` (T): Value to check
- `bitIndex` (int): Bit index
- `otherIndexs` (int[]): Other bit indices

**Return Value:**
- `bool`: Whether all bits are valid

##### SetValid<T>(T value, int bitIndex, bool isValid)

```csharp
public static sbyte SetValid(sbyte value, int bitIndex, bool isValid)
public static ushort SetValid(ushort value, int bitIndex, bool isValid)
public static int SetValid(int value, int bitIndex, bool isValid)
public static uint SetValid(uint value, int bitIndex, bool isValid)
public static long SetValid(long value, int bitIndex, bool isValid)
public static ulong SetValid(ulong value, int bitIndex, bool isValid)
```

**Description:** Set bit validity

**Parameters:**
- `value` (T): Original value
- `bitIndex` (int): Bit index
- `isValid` (bool): Whether valid

**Return Value:**
- `T`: Value after setting

---

### FileUtil

File operation utility class that provides file read/write, copy, delete and other operations.

```csharp
public static class FileUtil
```

#### Main Methods

##### ReadAllText(string path)

```csharp
public static string ReadAllText(string path)
```

**Description:** Read all file content

**Parameters:**
- `path` (string): File path

**Return Value:**
- `string`: File content

##### WriteAllText(string path, string content)

```csharp
public static void WriteAllText(string path, string content)
```

**Description:** Write all content to file

**Parameters:**
- `path` (string): File path
- `content` (string): Content to write

##### CopyFile(string sourcePath, string targetPath)

```csharp
public static void CopyFile(string sourcePath, string targetPath)
```

**Description:** Copy file

**Parameters:**
- `sourcePath` (string): Source file path
- `targetPath` (string): Target file path

##### DeleteFile(string path)

```csharp
public static void DeleteFile(string path)
```

**Description:** Delete file

**Parameters:**
- `path` (string): File path

---

### PathUtil

Path processing utility class that provides path parsing, combination, validation and other operations.

```csharp
public static class PathUtil
```

#### Main Methods

##### Combine(params string[] paths)

```csharp
public static string Combine(params string[] paths)
```

**Description:** Combine paths

**Parameters:**
- `paths` (string[]): Path array

**Return Value:**
- `string`: Combined path

##### GetDirectoryName(string path)

```csharp
public static string GetDirectoryName(string path)
```

**Description:** Get directory name

**Parameters:**
- `path` (string): Path

**Return Value:**
- `string`: Directory name

##### GetFileName(string path)

```csharp
public static string GetFileName(string path)
```

**Description:** Get file name

**Parameters:**
- `path` (string): Path

**Return Value:**
- `string`: File name

##### GetExtension(string path)

```csharp
public static string GetExtension(string path)
```

**Description:** Get file extension

**Parameters:**
- `path` (string): Path

**Return Value:**
- `string`: File extension

---

### TextUtil

Text processing utility class that provides string processing, formatting and other operations.

```csharp
public static class TextUtil
```

#### Main Methods

##### IsNullOrEmpty(string text)

```csharp
public static bool IsNullOrEmpty(string text)
```

**Description:** Check if string is empty or null

**Parameters:**
- `text` (string): String to check

**Return Value:**
- `bool`: Whether empty or null

##### Format(string format, params object[] args)

```csharp
public static string Format(string format, params object[] args)
```

**Description:** Format string

**Parameters:**
- `format` (string): Format string
- `args` (object[]): Parameter array

**Return Value:**
- `string`: Formatted string

---

### DirectoryUtil

Directory operation utility class that provides directory creation, deletion, traversal and other operations.

```csharp
public static class DirectoryUtil
```

#### Main Methods

##### CreateDirectory(string path)

```csharp
public static void CreateDirectory(string path)
```

**Description:** Create directory

**Parameters:**
- `path` (string): Directory path

##### DeleteDirectory(string path)

```csharp
public static void DeleteDirectory(string path)
```

**Description:** Delete directory

**Parameters:**
- `path` (string): Directory path

##### GetFiles(string path, string searchPattern = "*")

```csharp
public static string[] GetFiles(string path, string searchPattern = "*")
```

**Description:** Get files in directory

**Parameters:**
- `path` (string): Directory path
- `searchPattern` (string): Search pattern

**Return Value:**
- `string[]`: File path array

---

### ReflexUtil

Reflection utility class that provides reflection-related operations.

```csharp
public static class ReflexUtil
```

#### Main Methods

##### GetType(string typeName)

```csharp
public static Type GetType(string typeName)
```

**Description:** Get type by type name

**Parameters:**
- `typeName` (string): Type name

**Return Value:**
- `Type`: Type object

##### CreateInstance<T>(string typeName)

```csharp
public static T CreateInstance<T>(string typeName)
```

**Description:** Create type instance

**Parameters:**
- `typeName` (string): Type name

**Return Value:**
- `T`: Created instance

---

### PrintUtil

Print utility class that provides console output formatting functionality.

```csharp
public static class PrintUtil
```

#### Main Methods

##### Print(string message)

```csharp
public static void Print(string message)
```

**Description:** Print message

**Parameters:**
- `message` (string): Message to print

##### PrintLine(string message)

```csharp
public static void PrintLine(string message)
```

**Description:** Print message with line break

**Parameters:**
- `message` (string): Message to print

---

### ConfusedUtil

Obfuscation utility class that provides data obfuscation functionality.

```csharp
public static class ConfusedUtil
```

#### Main Methods

##### Confuse(byte[] data)

```csharp
public static void Confuse(byte[] data)
```

**Description:** Obfuscate data

**Parameters:**
- `data` (byte[]): Data to obfuscate

##### Deconfuse(byte[] data)

```csharp
public static void Deconfuse(byte[] data)
```

**Description:** Deobfuscate data

**Parameters:**
- `data` (byte[]): Data to deobfuscate

---

## Encryption Utility Classes

### DESUtil

DES encryption utility class.

```csharp
public static class DESUtil
```

#### Main Methods

##### Encrypt(string data, string key)

```csharp
public static string Encrypt(string data, string key)
```

**Description:** DES encryption

**Parameters:**
- `data` (string): Data to encrypt
- `key` (string): Key

**Return Value:**
- `string`: Encrypted data

##### Decrypt(string data, string key)

```csharp
public static string Decrypt(string data, string key)
```

**Description:** DES decryption

**Parameters:**
- `data` (string): Data to decrypt
- `key` (string): Key

**Return Value:**
- `string`: Decrypted data

---

### RijandelUtil

Rijndael encryption utility class.

```csharp
public static class RijandelUtil
```

#### Main Methods

##### Encrypt(string data, string key, string iv)

```csharp
public static string Encrypt(string data, string key, string iv)
```

**Description:** Rijndael encryption

**Parameters:**
- `data` (string): Data to encrypt
- `key` (string): Key
- `iv` (string): Initialization vector

**Return Value:**
- `string`: Encrypted data

##### Decrypt(string data, string key, string iv)

```csharp
public static string Decrypt(string data, string key, string iv)
```

**Description:** Rijndael decryption

**Parameters:**
- `data` (string): Data to decrypt
- `key` (string): Key
- `iv` (string): Initialization vector

**Return Value:**
- `string`: Decrypted data

---

### RSAUtil

RSA encryption utility class.

```csharp
public static class RSAUtil
```

#### Main Methods

##### Encrypt(string data, string publicKey)

```csharp
public static string Encrypt(string data, string publicKey)
```

**Description:** RSA encryption

**Parameters:**
- `data` (string): Data to encrypt
- `publicKey` (string): Public key

**Return Value:**
- `string`: Encrypted data

##### Decrypt(string data, string privateKey)

```csharp
public static string Decrypt(string data, string privateKey)
```

**Description:** RSA decryption

**Parameters:**
- `data` (string): Data to decrypt
- `privateKey` (string): Private key

**Return Value:**
- `string`: Decrypted data

---

## Usage Examples

### Array Operations

```csharp
// Create arrays
var array1 = ArrayUtil.NewArray<int>(5, 0);
var array2 = ArrayUtil.NewArray<string>(3, 2, "default");

// Merge arrays
var merged = ArrayUtil.MergeArray(array1, new int[] { 6, 7, 8 });

// Extract sub-array
var subArray = ArrayUtil.SubArray(merged, 1, 3);
```

### Bit Operations

```csharp
// Check if bit is valid
bool isValid = BitUtil.IsValid(15, 2); // Check if bit 2 is 1

// Set bit
int result = BitUtil.SetValid(8, 1, true); // Set bit 1 to 1

// Check multiple bits
bool allValid = BitUtil.IsValidAnd(15, 0, 1, 2); // Check if bits 0, 1, 2 are all 1
```

### File Operations

```csharp
// Read file
string content = FileUtil.ReadAllText("test.txt");

// Write file
FileUtil.WriteAllText("output.txt", "Hello World");

// Copy file
FileUtil.CopyFile("source.txt", "target.txt");
```

### Path Operations

```csharp
// Combine paths
string path = PathUtil.Combine("C:", "Users", "Documents", "file.txt");

// Get file name
string fileName = PathUtil.GetFileName(path);

// Get extension
string extension = PathUtil.GetExtension(path);
```

### Encryption Operations

```csharp
// DES encryption
string encrypted = DESUtil.Encrypt("Hello World", "mykey123");

// DES decryption
string decrypted = DESUtil.Decrypt(encrypted, "mykey123");

// RSA encryption
string rsaEncrypted = RSAUtil.Encrypt("Hello World", publicKey);

// RSA decryption
string rsaDecrypted = RSAUtil.Decrypt(rsaEncrypted, privateKey);
```

---

## Notes

1. **Performance considerations:** Pay attention to memory usage during large array operations
2. **Bit operations:** Bit indices start from 0, pay attention to boundary checking
3. **File operations:** Ensure file paths exist and have appropriate permissions
4. **Encryption security:** Properly store keys, avoid hardcoding
5. **Reflection performance:** Reflection operations have low performance, avoid frequent use

---

## Dependencies

- `System`: Basic types and collections
- `System.Runtime.CompilerServices`: Compiler services
- `System.Linq`: LINQ query functionality 