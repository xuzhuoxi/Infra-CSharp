# JLGames.Infra.Utils API Documentation

## Overview

The Utils module provides utility classes for array operations, bit operations, files and directories, path processing, text I/O, reflection, debug printing, and MD5/SHA-1 hashing.

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

##### CloneArray\<T, TK\>(TK[] source)

```csharp
public static T[] CloneArray<T, TK>(TK[] source) where T : class where TK : class
```

**Description:** Clone array

**Parameters:**
- `source` (TK[]): Source array

**Return Value:**
- `T[]`: Cloned array. Returns `null` when `source` is `null`; returns an empty array when length is 0. Elements are converted with `as T`.

##### NewArray\<T\>(int len, T @default)

```csharp
public static T[] NewArray<T>(int len, T @default)
```

**Description:** Create a 1D array and set the values

**Parameters:**
- `len` (int): Array length
- `@default` (T): Default value

**Return Value:**
- `T[]`: Created 1D array

##### NewArray\<T\>(int yLen, int xLen)

```csharp
public static T[][] NewArray<T>(int yLen, int xLen)
```

**Description:** Create a 2D array

**Parameters:**
- `yLen` (int): Number of rows
- `xLen` (int): Number of columns

**Return Value:**
- `T[][]`: Created 2D array

##### NewArray\<T\>(int yLen, int xLen, T @default)

```csharp
public static T[][] NewArray<T>(int yLen, int xLen, T @default)
```

**Description:** Create a 2D array and set the values

**Parameters:**
- `yLen` (int): Number of rows
- `xLen` (int): Number of columns
- `@default` (T): Default value

**Return Value:**
- `T[][]`: Created 2D array filled with the default value

##### MergeArray\<T\>(T first, T[] second)

```csharp
public static T[] MergeArray<T>(T first, T[] second)
```

**Description:** Merge array

**Parameters:**
- `first` (T): First element
- `second` (T[]): Second array

**Return Value:**
- `T[]`: Merged array (first element + second array, length is sum of both lengths)

##### MergeArray\<T\>(T[] first, T second)

```csharp
public static T[] MergeArray<T>(T[] first, T second)
```

**Description:** Merge array

**Parameters:**
- `first` (T[]): First array
- `second` (T): Second element

**Return Value:**
- `T[]`: Merged array (first array + second element, length is sum of both lengths)

##### MergeArray\<T\>(T[] first, T[] second)

```csharp
public static T[] MergeArray<T>(T[] first, T[] second)
```

**Description:** Merge array

**Parameters:**
- `first` (T[]): First array
- `second` (T[]): Second array

**Return Value:**
- `T[]`: Merged array (first array + second array, length is sum of both array lengths)

##### MergeArray\<T\>(T[] first, T[] second, params T[][] other)

```csharp
public static T[] MergeArray<T>(T[] first, T[] second, params T[][] other)
```

**Description:** Merge array

**Parameters:**
- `first` (T[]): First array
- `second` (T[]): Second array
- `other` (T[][]): Additional arrays

**Return Value:**
- `T[]`: Merged array (first array + second array, length is sum of both array lengths)

##### MergeArrayX\<T\>(T[][] first, params T[][][] other)

```csharp
public static T[][] MergeArrayX<T>(T[][] first, params T[][][] other)
```

**Description:** Merge 2D arrays in X direction

**Parameters:**
- `first` (T[][]): First 2D array
- `other` (T[][][]): Additional 2D arrays

**Return Value:**
- `T[][]`: 2D array merged along X. Returns `null` when `first` is `null` or row widths do not match.

##### MergeArrayY\<T\>(T[][] first, params T[][][] other)

```csharp
public static T[][] MergeArrayY<T>(T[][] first, params T[][][] other)
```

**Description:** Merge 2D arrays in Y direction

**Parameters:**
- `first` (T[][]): First 2D array
- `other` (T[][][]): Additional 2D arrays

**Return Value:**
- `T[][]`: 2D array merged along Y. Returns `null` when `first` is `null` or column widths do not match.

##### CopyDataTo\<T\>(T[][] target, T[][] source, uint startTargetX, uint startTargetY)

```csharp
public static void CopyDataTo<T>(T[][] target, T[][] source, uint startTargetX, uint startTargetY)
```

**Description:** Copy data

**Parameters:**
- `target` (T[][]): Target array
- `source` (T[][]): Source array
- `startTargetX` (uint): Target start X
- `startTargetY` (uint): Target start Y

##### ConcatArray\<T\>(T[] source, T element)

```csharp
public static T[] ConcatArray<T>(T[] source, T element)
```

**Description:** Concat element to array

**Parameters:**
- `source` (T[]): Original array
- `element` (T): Element

**Return Value:**
- `T[]`: Merged array (array + string)

##### SubArray\<T\>(T[] source, int startIndex, int len)

```csharp
public static T[] SubArray<T>(T[] source, int startIndex, int len)
```

**Description:** Cut a part from an array into a new array

**Parameters:**
- `source` (T[]): Original array
- `startIndex` (int): Start index in the original array
- `len` (int): Length

**Return Value:**
- `T[]`: Extracted sub-array

##### SubAry\<T\>(T[] source, int startIndex, int endIndex)

```csharp
public static T[] SubAry<T>(T[] source, int startIndex, int endIndex)
```

**Description:** Cut a part from an array into a new array

**Parameters:**
- `source` (T[]): Original array
- `startIndex` (int): Start index in the original array
- `endIndex` (int): End index in the original array (exclusive)

**Return Value:**
- `T[]`: Extracted sub-array

##### RemoveElement\<T\>(T[] source, int index, int len)

```csharp
public static T[] RemoveElement<T>(T[] source, int index, int len)
```

**Description:** remove some elements from an array

**Parameters:**
- `source` (T[]): Original array
- `index` (int): Start index
- `len` (int): Supports negative values

**Return Value:**
- `T[]`: New array with elements removed. Returns `source` when it is empty or `len` is 0.

##### InsertElement\<T\>(T[] source, T e, int index)

```csharp
public static T[] InsertElement<T>(T[] source, T e, int index)
```

**Description:** Insert element into array

**Parameters:**
- `source` (T[]): Original array
- `e` (T): Element to insert
- `index` (int): Insertion index

**Return Value:**
- `T[]`: New array after insertion. Returns an array containing only `e` when `source` is empty.

##### InsertElement\<T\>(T[] source, IEnumerable\<T\> es, int index)

```csharp
public static T[] InsertElement<T>(T[] source, IEnumerable<T> es, int index)
```

**Description:** Insert some elements into array

**Parameters:**
- `source` (T[]): Original array
- `es` (IEnumerable\<T\>): Elements to insert
- `index` (int): Insertion index

**Return Value:**
- `T[]`: New array after insertion. Returns `source` when `es` is `null`.

##### Rotated\<T\>(T[][] array)

```csharp
public static T[][] Rotated<T>(T[][] array)
```

**Description:** Rotate a 2D array

**Parameters:**
- `array` (T[][]): Source 2D array

**Return Value:**
- `T[][]`: Rotated 2D array. Returns `null` when `array` is `null` or empty.

##### Rotated\<T\>(T[] array, int width, int height)

```csharp
public static T[] Rotated<T>(T[] array, int width, int height)
```

**Description:** Rotate a flattened 1D array using the given width and height

**Parameters:**
- `array` (T[]): Source 1D array (row-major flattened layout)
- `width` (int): Width
- `height` (int): Height

**Return Value:**
- `T[]`: Rotated 1D array

---

### BitUtil

Bit operation utility class that provides bit-level checks, updates, and mask generation.

```csharp
public static class BitUtil
```

#### Main Methods

##### IsValid

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
- `value`: value to be detected
- `bitIndex` (int): Starting from the low order, the first bit index is 0

**Return Value:**
- `bool`: Whether the bit is valid

##### IsValidAnd(value, int bitIndex, params int[] otherIndexs)

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
- `value`: Value to check
- `bitIndex` (int): Bit index
- `otherIndexs` (int[]): Other bit indices

**Return Value:**
- `bool`: Whether all bits are valid

##### IsValidAnd(value, int[] bitIndexs)

```csharp
public static bool IsValidAnd(sbyte value, int[] bitIndexs)
public static bool IsValidAnd(ushort value, int[] bitIndexs)
public static bool IsValidAnd(int value, int[] bitIndexs)
public static bool IsValidAnd(uint value, int[] bitIndexs)
public static bool IsValidAnd(long value, int[] bitIndexs)
public static bool IsValidAnd(ulong value, int[] bitIndexs)
```

**Description:** Check if all bits are valid

**Parameters:**
- `value`: Value to check
- `bitIndexs` (int[]): Bit index array

**Return Value:**
- `bool`: Whether all bits are valid

##### IsValidOr(value, int bitIndex, params int[] otherIndexs)

```csharp
public static bool IsValidOr(sbyte value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(ushort value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(int value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(uint value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(long value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(ulong value, int bitIndex, params int[] otherIndexs)
```

**Description:** Check if one of the bits is valid

**Parameters:**
- `value`: Value to check
- `bitIndex` (int): Bit index
- `otherIndexs` (int[]): Other bit indices

**Return Value:**
- `bool`: Whether at least one of the specified bits is valid

##### IsValidOr(value, int[] bitIndexs)

```csharp
public static bool IsValidOr(sbyte value, int[] bitIndexs)
public static bool IsValidOr(ushort value, int[] bitIndexs)
public static bool IsValidOr(int value, int[] bitIndexs)
public static bool IsValidOr(uint value, int[] bitIndexs)
public static bool IsValidOr(long value, int[] bitIndexs)
public static bool IsValidOr(ulong value, int[] bitIndexs)
```

**Description:** Check if one of the bits is valid

**Parameters:**
- `value`: Value to check
- `bitIndexs` (int[]): Bit index array

**Return Value:**
- `bool`: Whether at least one of the specified bits is valid

##### SetValid(value, int bitIndex, bool isValid)

```csharp
public static sbyte SetValid(sbyte value, int bitIndex, bool isValid)
public static ushort SetValid(ushort value, int bitIndex, bool isValid)
public static int SetValid(int value, int bitIndex, bool isValid)
public static uint SetValid(uint value, int bitIndex, bool isValid)
public static long SetValid(long value, int bitIndex, bool isValid)
public static ulong SetValid(ulong value, int bitIndex, bool isValid)
```

**Description:** Set a value to a specified bit of a value

**Parameters:**
- `value`: Original value
- `bitIndex` (int): Bit index
- `isValid` (bool): Whether valid

**Return Value:**
- Value after setting

##### SetValid(value, int[] bitIndexs, bool isValid)

```csharp
public static sbyte SetValid(sbyte value, int[] bitIndexs, bool isValid)
public static ushort SetValid(ushort value, int[] bitIndexs, bool isValid)
public static int SetValid(int value, int[] bitIndexs, bool isValid)
public static uint SetValid(uint value, int[] bitIndexs, bool isValid)
public static long SetValid(long value, int[] bitIndexs, bool isValid)
public static ulong SetValid(ulong value, int[] bitIndexs, bool isValid)
```

**Description:** Set a value to a specified bit of a value (multiple bits)

**Parameters:**
- `value`: Original value
- `bitIndexs` (int[]): Bit index array
- `isValid` (bool): Whether valid

**Return Value:**
- Value after setting. The `sbyte` overload returns the original value when `bitIndexs` is `null` or empty.

##### Gen32BitValue1(int index, params int[] other)

```csharp
public static uint Gen32BitValue1(int index, params int[] other)
```

**Description:** Generate 32-bit data, specify the subscript value of 1

**Parameters:**
- `index` (int): Bit index
- `other` (int[]): Other bit indices

**Return Value:**
- `uint`: 32-bit mask with the specified bits set to 1

##### Gen32BitValue1(int[] indexs)

```csharp
public static uint Gen32BitValue1(int[] indexs)
```

**Description:** Generate 32-bit data, specify the subscript value of 1

**Parameters:**
- `indexs` (int[]): Bit index array

**Return Value:**
- `uint`: 32-bit mask with the specified bits set to 1

##### Gen64BitValue1(int index, params int[] other)

```csharp
public static ulong Gen64BitValue1(int index, params int[] other)
```

**Description:** Generate 64-bit data, specify the subscript value of 1

**Parameters:**
- `index` (int): Bit index
- `other` (int[]): Other bit indices

**Return Value:**
- `ulong`: 64-bit mask with the specified bits set to 1

##### Gen64BitValue1(int[] indexs)

```csharp
public static ulong Gen64BitValue1(int[] indexs)
```

**Description:** Generate 64-bit data, specify the subscript value of 1

**Parameters:**
- `indexs` (int[]): Bit index array

**Return Value:**
- `ulong`: 64-bit mask with the specified bits set to 1

##### Gen32BitValue0(int index, params int[] other)

```csharp
public static uint Gen32BitValue0(int index, params int[] other)
```

**Description:** Generate 32-bit data, specify the subscript value of 0

**Parameters:**
- `index` (int): Bit index
- `other` (int[]): Other bit indices

**Return Value:**
- `uint`: 32-bit mask with the specified bits set to 0 and other bits set to 1

##### Gen32BitValue0(int[] indexs)

```csharp
public static uint Gen32BitValue0(int[] indexs)
```

**Description:** Generate 32-bit data, specify the subscript value of 0

**Parameters:**
- `indexs` (int[]): Bit index array

**Return Value:**
- `uint`: 32-bit mask with the specified bits set to 0 and other bits set to 1

##### Gen64BitValue0(int index, params int[] other)

```csharp
public static ulong Gen64BitValue0(int index, params int[] other)
```

**Description:** Generate 64-bit data, specify the subscript value of 0

**Parameters:**
- `index` (int): Bit index
- `other` (int[]): Other bit indices

**Return Value:**
- `ulong`: 64-bit mask with the specified bits set to 0 and other bits set to 1

##### Gen64BitValue0(int[] indexs)

```csharp
public static ulong Gen64BitValue0(int[] indexs)
```

**Description:** Generate 64-bit data, specify the subscript value of 0

**Parameters:**
- `indexs` (int[]): Bit index array

**Return Value:**
- `ulong`: 64-bit mask with the specified bits set to 0 and other bits set to 1

---

### FileUtil

File operation utility class that provides existence checks, copy, move, rename, and delete.

```csharp
public static class FileUtil
```

#### Main Methods

##### Exists(string path)

```csharp
public static bool Exists(string path)
```

**Description:** Check if file exists

**Parameters:**
- `path` (string): File path

**Return Value:**
- `bool`: `true` when the path is not empty and the file exists

##### GetExtension(string path)

```csharp
public static string GetExtension(string path)
```

**Description:** Get file extension

**Parameters:**
- `path` (string): File path

**Return Value:**
- `string`: Text after the last `.` (without the dot). Returns an empty string when the path is empty or has no extension.

##### MoveFile(string oldPath, string newPath)

```csharp
public static void MoveFile(string oldPath, string newPath)
```

**Description:** Move the file. Note: Self-assurance path existence.

**Parameters:**
- `oldPath` (string): Source file path
- `newPath` (string): Destination file path

##### CopyFile(string srcPath, string destPath, bool overwrite = false)

```csharp
public static void CopyFile(string srcPath, string destPath, bool overwrite = false)
```

**Description:** Copy the file. Note: Self-assurance path existence.

**Parameters:**
- `srcPath` (string): Source file path
- `destPath` (string): Destination file path
- `overwrite` (bool): Whether to overwrite an existing destination file; default `false`

##### RenameFile(string filePath, string newName)

```csharp
public static void RenameFile(string filePath, string newName)
```

**Description:** Rename the file

**Parameters:**
- `filePath` (string): File path
- `newName` (string): New file name

##### DeleteFile(string filePath, bool force)

```csharp
public static void DeleteFile(string filePath, bool force)
```

**Description:** Delete the file

**Parameters:**
- `filePath` (string): File path
- `force` (bool): Whether to force. When `true`, clears the read-only attribute first.

##### DeleteAllFile(string rootPath, bool recursive, bool force)

```csharp
public static void DeleteAllFile(string rootPath, bool recursive, bool force)
```

**Description:** Delete all files.

**Parameters:**
- `rootPath` (string): Root path (file or directory)
- `recursive` (bool): Whether to recurse
- `force` (bool): Force

##### IsReadOnly(FileInfo fileInfo)

```csharp
public static bool IsReadOnly(FileInfo fileInfo)
```

**Description:** Check whether the file is read-only

**Parameters:**
- `fileInfo` (FileInfo): File information

**Return Value:**
- `bool`: Whether the read-only attribute is set

---

### PathUtil

Path processing utility class that provides path formatting, combining, comparison, and parsing.

```csharp
public static class PathUtil
```

#### Main Methods

##### IsAbsPath(string path)

```csharp
public static bool IsAbsPath(string path)
```

**Description:** Is it an absolute path

**Parameters:**
- `path` (string): Path

**Return Value:**
- `bool`: Whether the path is absolute

##### Format2LinuxPath(string path)

```csharp
public static string Format2LinuxPath(string path)
```

**Description:** Format to Linux path format

**Parameters:**
- `path` (string): Path

**Return Value:**
- `string`: Path with `\` replaced by `/`

##### Format2WindowsPath(string path)

```csharp
public static string Format2WindowsPath(string path)
```

**Description:** Format to Windows path format

**Parameters:**
- `path` (string): Path

**Return Value:**
- `string`: Path with `/` replaced by `\`

##### CombineLinuxPath(string basePath, string path, params string[] paths)

```csharp
public static string CombineLinuxPath(string basePath, string path, params string[] paths)
```

**Description:** Combine paths and convert to Linux path format

**Parameters:**
- `basePath` (string): Base path
- `path` (string): Path to combine
- `paths` (string[]): Additional path segments

**Return Value:**
- `string`: Combined path formatted as Linux style

##### CombineWindowsPath(string basePath, string path, params string[] paths)

```csharp
public static string CombineWindowsPath(string basePath, string path, params string[] paths)
```

**Description:** Combine paths and convert to Windows path format

**Parameters:**
- `basePath` (string): Base path
- `path` (string): Path to combine
- `paths` (string[]): Additional path segments

**Return Value:**
- `string`: Combined path formatted as Windows style

##### CombinePath(string basePath, string path, params string[] paths)

```csharp
public static string CombinePath(string basePath, string path, params string[] paths)
```

**Description:** Combine paths

**Parameters:**
- `basePath` (string): Base path
- `path` (string): Path to combine
- `paths` (string[]): Additional path segments

**Return Value:**
- `string`: Combined path. Leading separators on later segments are stripped so `Path.Combine` does not drop the earlier part.

##### GetParentDirectory(string path)

```csharp
public static string GetParentDirectory(string path)
```

**Description:** Take the parent directory

**Parameters:**
- `path` (string): Path

**Return Value:**
- `string`: Path in Linux format

##### GetFileName(string path)

```csharp
public static string GetFileName(string path)
```

**Description:** Take the current file name or current directory name under the path

**Parameters:**
- `path` (string): Path

**Return Value:**
- `string`: Current file name or current directory name

##### ClearExtension(string path)

```csharp
public static string ClearExtension(string path)
```

**Description:** Clear file path extension

**Parameters:**
- `path` (string): Path

**Return Value:**
- `string`: Path with the extension removed

##### ComparePath(string path1, string path2)

```csharp
public static int ComparePath(string path1, string path2)
```

**Description:** Compare two paths, often used for path ordering. Comparison condition: levels > characters

**Parameters:**
- `path1` (string): First path
- `path2` (string): Second path

**Return Value:**
- `int`: Compared by level count first, then by case-insensitive character order

##### GetPathLevel(string path)

```csharp
public static int GetPathLevel(string path)
```

**Description:** Get the number of levels of the path

**Parameters:**
- `path` (string): Path

**Return Value:**
- `int`: Number of `/` characters in the path

---

### TextUtil

Text processing utility class that provides reading, overwriting, and appending character files.

```csharp
public static class TextUtil
```

#### Main Methods

##### ReadText(string filePath)

```csharp
public static string ReadText(string filePath)
```

**Description:** Read character file content

**Parameters:**
- `filePath` (string): File path

**Return Value:**
- `string`: File content read as UTF-8

##### ReadText(string filePath, Encoding encoding)

```csharp
public static string ReadText(string filePath, Encoding encoding)
```

**Description:** Read character file content

**Parameters:**
- `filePath` (string): File path
- `encoding` (Encoding): Character encoding

**Return Value:**
- `string`: File content read with the specified encoding

##### CreateFileWithText(string filePath, string text)

```csharp
public static void CreateFileWithText(string filePath, string text)
```

**Description:** Create or open a text file and overwrite the contents

**Parameters:**
- `filePath` (string): File path
- `text` (string): File content

##### CreateFileWithText(string filePath, string text, Encoding encoding)

```csharp
public static void CreateFileWithText(string filePath, string text, Encoding encoding)
```

**Description:** Create or open a text file and overwrite the contents

**Parameters:**
- `filePath` (string): File path
- `text` (string): File content
- `encoding` (Encoding): Character encoding

##### AppendTextToFile(string filePath, string text)

```csharp
public static void AppendTextToFile(string filePath, string text)
```

**Description:** Create or open a text file and append the contents

**Parameters:**
- `filePath` (string): File path
- `text` (string): File content

##### AppendTextToFile(string filePath, string text, Encoding encoding)

```csharp
public static void AppendTextToFile(string filePath, string text, Encoding encoding)
```

**Description:** Create or open a text file and append the contents

**Parameters:**
- `filePath` (string): File path
- `text` (string): File content
- `encoding` (Encoding): Character encoding

---

### DirectoryUtil

Directory operation utility class that provides existence checks, creation, copy, move, clearing, and listing.

```csharp
public static class DirectoryUtil
```

#### Main Methods

##### Exists(string path)

```csharp
public static bool Exists(string path)
```

**Description:** Check if the directory exists. This parameter path is allowed to specify relative path or absolute path information. Relative path information will be interpreted relative to the current working directory.

**Parameters:**
- `path` (string): Directory path

**Return Value:**
- `bool`: `true` when the path is not empty and the directory exists

##### IsEmpty(string path)

```csharp
public static bool IsEmpty(string path)
```

**Description:** Check if the directory is empty. The current directory does not exist, return true.

**Parameters:**
- `path` (string): Directory path

**Return Value:**
- `bool`: `true` when the directory does not exist or has no file-system entries

##### MoveDir(string oldPath, string newPath)

```csharp
public static void MoveDir(string oldPath, string newPath)
```

**Description:** Move directory. Note: Self-assurance path existence.

**Parameters:**
- `oldPath` (string): Source directory path
- `newPath` (string): Destination directory path

##### CopyDir(string oldPath, string newPath)

```csharp
public static void CopyDir(string oldPath, string newPath)
```

**Description:** Copy directory. Note: Self-assurance path existence.

**Parameters:**
- `oldPath` (string): Source directory path
- `newPath` (string): Destination directory path

##### RenameDir(string dirPath, string newName)

```csharp
public static void RenameDir(string dirPath, string newName)
```

**Description:** Rename directory

**Parameters:**
- `dirPath` (string): Directory path
- `newName` (string): New directory name

##### DeleteDir(string dir, bool force)

```csharp
public static void DeleteDir(string dir, bool force)
```

**Description:** Delete directory

**Parameters:**
- `dir` (string): Directory path
- `force` (bool): Force. When `true`, deletes inner files first and tries to clear the read-only attribute.

##### ClearDir(string dir, bool force)

```csharp
public static void ClearDir(string dir, bool force)
```

**Description:** Clear directory

**Parameters:**
- `dir` (string): Directory path
- `force` (bool): Force

##### MakeDir(string path, bool keepExist = true)

```csharp
public static void MakeDir(string path, bool keepExist = true)
```

**Description:** Create a directory.

**Parameters:**
- `path` (string): Directory path
- `keepExist` (bool): Keep the directory if it already exists; default `true`. When `false`, deletes then recreates.

##### MakeDirAll(string path, bool keepExist = true)

```csharp
public static void MakeDirAll(string path, bool keepExist = true)
```

**Description:** Create a directory.

**Parameters:**
- `path` (string): Directory path
- `keepExist` (bool): Keep the directory if it already exists; default `true`

##### GetFiles(string folderPath, string searchPattern = "*")

```csharp
public static string[] GetFiles(string folderPath, string searchPattern = "*")
```

**Description:** Get a list of files in a directory, current directory only

**Parameters:**
- `folderPath` (string): Directory path
- `searchPattern` (string): Search pattern, default `"*"`

**Return Value:**
- `string[]`: File path array. Returns `null` when the path is empty or the directory does not exist.

##### GetAllFiles(string folderPath, string searchPattern = "*")

```csharp
public static string[] GetAllFiles(string folderPath, string searchPattern = "*")
```

**Description:** Get a list of files in a directory, recursively

**Parameters:**
- `folderPath` (string): Directory path
- `searchPattern` (string): Search pattern, default `"*"`

**Return Value:**
- `string[]`: Recursively collected file path array. Returns `null` when the path is empty or the directory does not exist.

---

### ReflexUtil

Reflection utility class that reads object fields and properties via extension methods.

```csharp
public static class ReflexUtil
```

#### Main Methods

##### GetFieldInt(this object obj, string name)

```csharp
public static int GetFieldInt(this object obj, string name)
```

**Description:** Get the specified field by reflection and convert it to `int`

**Parameters:**
- `obj` (object): Target object
- `name` (string): Field name

**Return Value:**
- `int`: Field value; returns `0` when the field is missing or `null`

##### GetFieldUint(this object obj, string name)

```csharp
public static uint GetFieldUint(this object obj, string name)
```

**Description:** Get the specified field by reflection and convert it to `uint`

**Parameters:**
- `obj` (object): Target object
- `name` (string): Field name

**Return Value:**
- `uint`: Field value; returns `0` when the field is missing or `null`

##### GetFieldString(this object obj, string name)

```csharp
public static string GetFieldString(this object obj, string name)
```

**Description:** Get the specified field by reflection and convert it to a string

**Parameters:**
- `obj` (object): Target object
- `name` (string): Field name

**Return Value:**
- `string`: String representation of the field; returns `null` when the value is `null`

##### GetFieldBool(this object obj, string name)

```csharp
public static bool GetFieldBool(this object obj, string name)
```

**Description:** Get the specified field by reflection and convert it to `bool`

**Parameters:**
- `obj` (object): Target object
- `name` (string): Field name

**Return Value:**
- `bool`: Field value; returns `false` when the field is missing or `null`

##### GetField(this object obj, string name)

```csharp
public static object GetField(this object obj, string name)
```

**Description:** Get the specified field value by reflection

**Parameters:**
- `obj` (object): Target object
- `name` (string): Field name

**Return Value:**
- `object`: Field value; returns `null` when the field does not exist

##### GetField\<T\>(this object obj, string name)

```csharp
public static T GetField<T>(this object obj, string name)
```

**Description:** Get the specified field by reflection and convert it to `T`

**Parameters:**
- `obj` (object): Target object
- `name` (string): Field name

**Return Value:**
- `T`: Field value; returns `default(T)` when the type does not match

##### GetPropertyInt(this object obj, string name)

```csharp
public static int GetPropertyInt(this object obj, string name)
```

**Description:** Get the specified property by reflection and convert it to `int`

**Parameters:**
- `obj` (object): Target object
- `name` (string): Property name

**Return Value:**
- `int`: Property value; returns `0` when the property is missing or `null`

##### GetPropertyUint(this object obj, string name)

```csharp
public static uint GetPropertyUint(this object obj, string name)
```

**Description:** Get the specified property by reflection and convert it to `uint`

**Parameters:**
- `obj` (object): Target object
- `name` (string): Property name

**Return Value:**
- `uint`: Property value; returns `0` when the property is missing or `null`

##### GetPropertyString(this object obj, string name)

```csharp
public static string GetPropertyString(this object obj, string name)
```

**Description:** Get the specified property by reflection and convert it to a string

**Parameters:**
- `obj` (object): Target object
- `name` (string): Property name

**Return Value:**
- `string`: String representation of the property; returns `null` when the value is `null`

##### GetPropertyBool(this object obj, string name)

```csharp
public static bool GetPropertyBool(this object obj, string name)
```

**Description:** Get the specified property by reflection and convert it to `bool`

**Parameters:**
- `obj` (object): Target object
- `name` (string): Property name

**Return Value:**
- `bool`: Property value; returns `false` when the property is missing or `null`

##### GetProperty(this object obj, string name)

```csharp
public static object GetProperty(this object obj, string name)
```

**Description:** Get the specified property value by reflection

**Parameters:**
- `obj` (object): Target object
- `name` (string): Property name

**Return Value:**
- `object`: Property value; returns `null` when the property does not exist

##### GetProperty\<T\>(this object obj, string name)

```csharp
public static T GetProperty<T>(this object obj, string name)
```

**Description:** Get the specified property by reflection and convert it to `T`

**Parameters:**
- `obj` (object): Target object
- `name` (string): Property name

**Return Value:**
- `T`: Property value; returns `default(T)` when the type does not match

##### Object2T\<T\>(object value)

```csharp
public static T Object2T<T>(object value)
```

**Description:** Convert an `object` to `T`

**Parameters:**
- `value` (object): Source value

**Return Value:**
- `T`: Converted value. Returns `default(T)` when `value` is not `T`.

---

### PrintUtil

Print utility class that provides stringify extension methods for objects and arrays, mostly used for debugging or printing.

```csharp
public static class PrintUtil
```

#### Main Methods

##### ToStringText\<T\>(this T o)

```csharp
public static string ToStringText<T>(this T o)
```

**Description:** stringify

**Parameters:**
- `o` (T): Object to convert

**Return Value:**
- `string`: String representation. `int` uses interpolation; other types call `ToString()`.

##### ToStringText\<T\>(this T[] arr)

```csharp
public static string ToStringText<T>(this T[] arr)
```

**Description:** Convert a 1D array to a string representation, mostly used for debugging or printing

**Parameters:**
- `arr` (T[]): 1D array

**Return Value:**
- `string`: `"null"` when the array is `null`, `"[]"` when empty, otherwise a string like `[a,b,c]`.

##### ToStringText\<T\>(this T[][] arr)

```csharp
public static string ToStringText<T>(this T[][] arr)
```

**Description:** Convert a 2D array to a string representation, mostly used for debugging or printing

**Parameters:**
- `arr` (T[][]): 2D array

**Return Value:**
- `string`: `"null"` when the array is `null`, `"[]"` when empty, otherwise a multi-line indented string.

---

### ConfusedUtil

Hash utility class that computes MD5 and SHA-1 digests.

```csharp
public static class ConfusedUtil
```

#### Main Methods

##### Md5(byte[] dataByte)

```csharp
public static string Md5(byte[] dataByte)
```

**Description:** Compute the MD5 digest of a byte array

**Parameters:**
- `dataByte` (byte[]): Input data

**Return Value:**
- `string`: 32-character lowercase hexadecimal string (left-padded with `0`)

##### Md5(string dataStr)

```csharp
public static string Md5(string dataStr)
```

**Description:** Compute the MD5 digest of a string

**Parameters:**
- `dataStr` (string): Input text (converted to bytes with the default encoding)

**Return Value:**
- `string`: 32-character lowercase hexadecimal string

##### Sha1(byte[] dataByte)

```csharp
public static string Sha1(byte[] dataByte)
```

**Description:** Compute the SHA-1 digest of a byte array

**Parameters:**
- `dataByte` (byte[]): Input data

**Return Value:**
- `string`: Lowercase hexadecimal string

##### Sha1(string dataStr)

```csharp
public static string Sha1(string dataStr)
```

**Description:** Compute the SHA-1 digest of a string

**Parameters:**
- `dataStr` (string): Input text (converted to bytes with the default encoding)

**Return Value:**
- `string`: Lowercase hexadecimal string

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

// Rotate 2D array
var rotated = ArrayUtil.Rotated(array2);
```

### Bit Operations

```csharp
// Check if bit is valid
bool isValid = BitUtil.IsValid(15, 2); // Check if bit 2 is 1

// Set bit
int result = BitUtil.SetValid(8, 1, true); // Set bit 1 to 1

// Check multiple bits
bool allValid = BitUtil.IsValidAnd(15, 0, 1, 2); // Check if bits 0, 1, 2 are all 1
bool anyValid = BitUtil.IsValidOr(8, 0, 1, 3);

// Generate mask
uint mask = BitUtil.Gen32BitValue1(0, 2, 4);
```

### File Operations

```csharp
if (FileUtil.Exists("test.txt"))
{
    FileUtil.CopyFile("test.txt", "backup.txt", overwrite: true);
    FileUtil.RenameFile("backup.txt", "backup-renamed.txt");
}

FileUtil.DeleteFile("temp.txt", force: true);
FileUtil.DeleteAllFile("output", recursive: true, force: false);
```

### Path Operations

```csharp
string path = PathUtil.CombinePath("C:", "Users", "Documents", "file.txt");
string linuxPath = PathUtil.CombineLinuxPath("home", "user", "docs", "file.txt");

string fileName = PathUtil.GetFileName(path);
string withoutExt = PathUtil.ClearExtension(path);
string parent = PathUtil.GetParentDirectory(PathUtil.Format2LinuxPath(path));
```

### Text Operations

```csharp
string content = TextUtil.ReadText("test.txt");
TextUtil.CreateFileWithText("output.txt", "Hello World");
TextUtil.AppendTextToFile("output.txt", " more");
```

### Directory Operations

```csharp
DirectoryUtil.MakeDir("data");
DirectoryUtil.MakeDirAll("data/sub/nested");

string[] files = DirectoryUtil.GetFiles("data", "*.txt");
string[] allFiles = DirectoryUtil.GetAllFiles("data");

DirectoryUtil.CopyDir("data", "data-backup");
DirectoryUtil.ClearDir("tmp", force: true);
```

### Reflection Operations

```csharp
int id = obj.GetFieldInt("id");
string name = obj.GetPropertyString("Name");
var nested = obj.GetField<MyType>("nested");
```

### Print Operations

```csharp
int[] nums = { 1, 2, 3 };
string text = nums.ToStringText(); // [1,2,3]

int[][] grid = ArrayUtil.NewArray<int>(2, 3, 0);
string gridText = grid.ToStringText();
```

### Hash Operations

```csharp
string md5 = ConfusedUtil.Md5("Hello World");
string sha1 = ConfusedUtil.Sha1("Hello World");

byte[] bytes = Encoding.UTF8.GetBytes("Hello World");
string md5FromBytes = ConfusedUtil.Md5(bytes);
```

---

## Notes

1. **Performance considerations:** Pay attention to memory usage during large array operations
2. **Bit operations:** Bit indices start from 0, pay attention to boundary checking
3. **Files and directories:** `MoveFile` / `CopyFile` / `MoveDir` / `CopyDir` require the caller to ensure paths exist; delete APIs use `force` for read-only files
4. **Path combining:** `CombinePath` strips leading separators from later segments so `Path.Combine` does not drop the earlier part
5. **Text encoding:** Overloads of `ReadText` / `CreateFileWithText` / `AppendTextToFile` without an encoding argument use UTF-8
6. **Reflection performance:** Reflection operations have low performance, avoid frequent use
7. **Hashing:** `ConfusedUtil` provides MD5 / SHA-1 digests; it is not symmetric or asymmetric encryption

---

## Dependencies

- `System`: Basic types
- `System.Collections.Generic`: Collection types
- `System.IO`: Files and directories
- `System.Linq`: LINQ queries
- `System.Runtime.CompilerServices`: Compiler services (bit-operation inlining)
- `System.Security.Cryptography`: MD5 / SHA-1
- `System.Text`: Encoding and string building
