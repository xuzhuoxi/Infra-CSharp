# JLGames.Infra.Utils API 文档

## 概述

Utils 模块提供数组操作、位运算、文件与目录、路径处理、文本读写、反射取值、调试打印以及 MD5/SHA-1 哈希等工具类。

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

##### CloneArray\<T, TK\>(TK[] source)

```csharp
public static T[] CloneArray<T, TK>(TK[] source) where T : class where TK : class
```

**描述：** 克隆数组

**参数：**
- `source` (TK[]): 源数组

**返回值：**
- `T[]`: 克隆后的数组。`source` 为 `null` 时返回 `null`；长度为 0 时返回空数组。元素通过 `as T` 转换。

##### NewArray\<T\>(int len, T @default)

```csharp
public static T[] NewArray<T>(int len, T @default)
```

**描述：** 创建一维数组并赋值

**参数：**
- `len` (int): 数组长度
- `@default` (T): 默认值

**返回值：**
- `T[]`: 创建的一维数组

##### NewArray\<T\>(int yLen, int xLen)

```csharp
public static T[][] NewArray<T>(int yLen, int xLen)
```

**描述：** 创建二维数组

**参数：**
- `yLen` (int): 行数
- `xLen` (int): 列数

**返回值：**
- `T[][]`: 创建的二维数组

##### NewArray\<T\>(int yLen, int xLen, T @default)

```csharp
public static T[][] NewArray<T>(int yLen, int xLen, T @default)
```

**描述：** 创建二维数组并赋值

**参数：**
- `yLen` (int): 行数
- `xLen` (int): 列数
- `@default` (T): 默认值

**返回值：**
- `T[][]`: 创建并填充默认值的二维数组

##### MergeArray\<T\>(T first, T[] second)

```csharp
public static T[] MergeArray<T>(T first, T[] second)
```

**描述：** 合并数组

**参数：**
- `first` (T): 第一个元素
- `second` (T[]): 第二个数组

**返回值：**
- `T[]`: 合并后的数组(第一个元素+第二个数组，长度为两者长度和)

##### MergeArray\<T\>(T[] first, T second)

```csharp
public static T[] MergeArray<T>(T[] first, T second)
```

**描述：** 合并数组

**参数：**
- `first` (T[]): 第一个数组
- `second` (T): 第二个元素

**返回值：**
- `T[]`: 合并后的数组(第一个数组+第二个元素，长度为两者长度和)

##### MergeArray\<T\>(T[] first, T[] second)

```csharp
public static T[] MergeArray<T>(T[] first, T[] second)
```

**描述：** 合并数组

**参数：**
- `first` (T[]): 第一个数组
- `second` (T[]): 第二个数组

**返回值：**
- `T[]`: 合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)

##### MergeArray\<T\>(T[] first, T[] second, params T[][] other)

```csharp
public static T[] MergeArray<T>(T[] first, T[] second, params T[][] other)
```

**描述：** 合并数组

**参数：**
- `first` (T[]): 第一个数组
- `second` (T[]): 第二个数组
- `other` (T[][]): 不定个数组

**返回值：**
- `T[]`: 合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)

##### MergeArrayX\<T\>(T[][] first, params T[][][] other)

```csharp
public static T[][] MergeArrayX<T>(T[][] first, params T[][][] other)
```

**描述：** 以X方向合并二维数组

**参数：**
- `first` (T[][]): 第一个二维数组
- `other` (T[][][]): 其余二维数组

**返回值：**
- `T[][]`: 沿 X 方向合并后的二维数组。`first` 为 `null` 或行宽不一致时返回 `null`。

##### MergeArrayY\<T\>(T[][] first, params T[][][] other)

```csharp
public static T[][] MergeArrayY<T>(T[][] first, params T[][][] other)
```

**描述：** 以Y方向合并二维数组

**参数：**
- `first` (T[][]): 第一个二维数组
- `other` (T[][][]): 其余二维数组

**返回值：**
- `T[][]`: 沿 Y 方向合并后的二维数组。`first` 为 `null` 或列宽不一致时返回 `null`。

##### CopyDataTo\<T\>(T[][] target, T[][] source, uint startTargetX, uint startTargetY)

```csharp
public static void CopyDataTo<T>(T[][] target, T[][] source, uint startTargetX, uint startTargetY)
```

**描述：** 复制数据

**参数：**
- `target` (T[][]): 目标数组
- `source` (T[][]): 源数组
- `startTargetX` (uint): 目标数组开始X
- `startTargetY` (uint): 目标数组开始Y

##### ConcatArray\<T\>(T[] source, T element)

```csharp
public static T[] ConcatArray<T>(T[] source, T element)
```

**描述：** 数组追加

**参数：**
- `source` (T[]): 原数组
- `element` (T): 元素

**返回值：**
- `T[]`: 合并后的数组(数组+字符串)

##### SubArray\<T\>(T[] source, int startIndex, int len)

```csharp
public static T[] SubArray<T>(T[] source, int startIndex, int len)
```

**描述：** 从数组中截取一部分成新的数组

**参数：**
- `source` (T[]): 原数组
- `startIndex` (int): 原数组的起始位置
- `len` (int): 长度

**返回值：**
- `T[]`: 截取的子数组

##### SubAry\<T\>(T[] source, int startIndex, int endIndex)

```csharp
public static T[] SubAry<T>(T[] source, int startIndex, int endIndex)
```

**描述：** 从数组中截取一部分成新的数组

**参数：**
- `source` (T[]): 原数组
- `startIndex` (int): 原数组的起始位置
- `endIndex` (int): 原数组的截止位置(不包含)

**返回值：**
- `T[]`: 截取的子数组

##### RemoveElement\<T\>(T[] source, int index, int len)

```csharp
public static T[] RemoveElement<T>(T[] source, int index, int len)
```

**描述：** 从数组中移除部分元素

**参数：**
- `source` (T[]): 原数组
- `index` (int): 起始索引
- `len` (int): 支持负数

**返回值：**
- `T[]`: 移除元素后的新数组。`source` 为空或 `len` 为 0 时返回原数组。

##### InsertElement\<T\>(T[] source, T e, int index)

```csharp
public static T[] InsertElement<T>(T[] source, T e, int index)
```

**描述：** 数组中插入元素

**参数：**
- `source` (T[]): 原数组
- `e` (T): 要插入的元素
- `index` (int): 插入位置

**返回值：**
- `T[]`: 插入后的新数组。`source` 为空时返回仅含 `e` 的数组。

##### InsertElement\<T\>(T[] source, IEnumerable\<T\> es, int index)

```csharp
public static T[] InsertElement<T>(T[] source, IEnumerable<T> es, int index)
```

**描述：** 数组中插入一些元素

**参数：**
- `source` (T[]): 原数组
- `es` (IEnumerable\<T\>): 要插入的元素集合
- `index` (int): 插入位置

**返回值：**
- `T[]`: 插入后的新数组。`es` 为 `null` 时返回 `source`。

##### Rotated\<T\>(T[][] array)

```csharp
public static T[][] Rotated<T>(T[][] array)
```

**描述：** 旋转二维数组

**参数：**
- `array` (T[][]): 源二维数组

**返回值：**
- `T[][]`: 旋转后的二维数组。`array` 为 `null` 或长度为 0 时返回 `null`。

##### Rotated\<T\>(T[] array, int width, int height)

```csharp
public static T[] Rotated<T>(T[] array, int width, int height)
```

**描述：** 按给定宽高旋转一维展平数组

**参数：**
- `array` (T[]): 源一维数组（按行主序展平）
- `width` (int): 宽度
- `height` (int): 高度

**返回值：**
- `T[]`: 旋转后的一维数组

---

### BitUtil

位操作工具类，提供位级别的检测、设置与位掩码生成。

```csharp
public static class BitUtil
```

#### 主要方法

##### IsValid

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
- `value`: 待检测值
- `bitIndex` (int): 从低位开始，第1位索引为0

**返回值：**
- `bool`: 位是否有效

##### IsValidAnd(value, int bitIndex, params int[] otherIndexs)

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
- `value`: 待检测值
- `bitIndex` (int): 位索引
- `otherIndexs` (int[]): 其他位索引

**返回值：**
- `bool`: 所有位是否都有效

##### IsValidAnd(value, int[] bitIndexs)

```csharp
public static bool IsValidAnd(sbyte value, int[] bitIndexs)
public static bool IsValidAnd(ushort value, int[] bitIndexs)
public static bool IsValidAnd(int value, int[] bitIndexs)
public static bool IsValidAnd(uint value, int[] bitIndexs)
public static bool IsValidAnd(long value, int[] bitIndexs)
public static bool IsValidAnd(ulong value, int[] bitIndexs)
```

**描述：** 检查位是否全部为有效

**参数：**
- `value`: 待检测值
- `bitIndexs` (int[]): 位索引数组

**返回值：**
- `bool`: 所有位是否都有效

##### IsValidOr(value, int bitIndex, params int[] otherIndexs)

```csharp
public static bool IsValidOr(sbyte value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(ushort value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(int value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(uint value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(long value, int bitIndex, params int[] otherIndexs)
public static bool IsValidOr(ulong value, int bitIndex, params int[] otherIndexs)
```

**描述：** 检查位是否其中一个有效

**参数：**
- `value`: 待检测值
- `bitIndex` (int): 位索引
- `otherIndexs` (int[]): 其他位索引

**返回值：**
- `bool`: 指定位中是否至少有一位有效

##### IsValidOr(value, int[] bitIndexs)

```csharp
public static bool IsValidOr(sbyte value, int[] bitIndexs)
public static bool IsValidOr(ushort value, int[] bitIndexs)
public static bool IsValidOr(int value, int[] bitIndexs)
public static bool IsValidOr(uint value, int[] bitIndexs)
public static bool IsValidOr(long value, int[] bitIndexs)
public static bool IsValidOr(ulong value, int[] bitIndexs)
```

**描述：** 检查位是否其中一个有效

**参数：**
- `value`: 待检测值
- `bitIndexs` (int[]): 位索引数组

**返回值：**
- `bool`: 指定位中是否至少有一位有效

##### SetValid(value, int bitIndex, bool isValid)

```csharp
public static sbyte SetValid(sbyte value, int bitIndex, bool isValid)
public static ushort SetValid(ushort value, int bitIndex, bool isValid)
public static int SetValid(int value, int bitIndex, bool isValid)
public static uint SetValid(uint value, int bitIndex, bool isValid)
public static long SetValid(long value, int bitIndex, bool isValid)
public static ulong SetValid(ulong value, int bitIndex, bool isValid)
```

**描述：** 针对值的指定位进行赋值

**参数：**
- `value`: 原值
- `bitIndex` (int): 位索引
- `isValid` (bool): 是否有效

**返回值：**
- 设置后的值

##### SetValid(value, int[] bitIndexs, bool isValid)

```csharp
public static sbyte SetValid(sbyte value, int[] bitIndexs, bool isValid)
public static ushort SetValid(ushort value, int[] bitIndexs, bool isValid)
public static int SetValid(int value, int[] bitIndexs, bool isValid)
public static uint SetValid(uint value, int[] bitIndexs, bool isValid)
public static long SetValid(long value, int[] bitIndexs, bool isValid)
public static ulong SetValid(ulong value, int[] bitIndexs, bool isValid)
```

**描述：** 针对值的多个指定位进行赋值

**参数：**
- `value`: 原值
- `bitIndexs` (int[]): 位索引数组
- `isValid` (bool): 是否有效

**返回值：**
- 设置后的值。`sbyte` 重载在 `bitIndexs` 为 `null` 或空时返回原值。

##### Gen32BitValue1(int index, params int[] other)

```csharp
public static uint Gen32BitValue1(int index, params int[] other)
```

**描述：** 生成32位数据，指定下标值为1

**参数：**
- `index` (int): 位下标
- `other` (int[]): 其他位下标

**返回值：**
- `uint`: 指定位为 1 的 32 位掩码

##### Gen32BitValue1(int[] indexs)

```csharp
public static uint Gen32BitValue1(int[] indexs)
```

**描述：** 生成32位数据，指定下标值为1

**参数：**
- `indexs` (int[]): 位下标数组

**返回值：**
- `uint`: 指定位为 1 的 32 位掩码

##### Gen64BitValue1(int index, params int[] other)

```csharp
public static ulong Gen64BitValue1(int index, params int[] other)
```

**描述：** 生成64位数据，指定下标值为1

**参数：**
- `index` (int): 位下标
- `other` (int[]): 其他位下标

**返回值：**
- `ulong`: 指定位为 1 的 64 位掩码

##### Gen64BitValue1(int[] indexs)

```csharp
public static ulong Gen64BitValue1(int[] indexs)
```

**描述：** 生成64位数据，指定下标值为1

**参数：**
- `indexs` (int[]): 位下标数组

**返回值：**
- `ulong`: 指定位为 1 的 64 位掩码

##### Gen32BitValue0(int index, params int[] other)

```csharp
public static uint Gen32BitValue0(int index, params int[] other)
```

**描述：** 生成32位数据，指定下标值为0

**参数：**
- `index` (int): 位下标
- `other` (int[]): 其他位下标

**返回值：**
- `uint`: 指定位为 0、其余位为 1 的 32 位掩码

##### Gen32BitValue0(int[] indexs)

```csharp
public static uint Gen32BitValue0(int[] indexs)
```

**描述：** 生成32位数据，指定下标值为0

**参数：**
- `indexs` (int[]): 位下标数组

**返回值：**
- `uint`: 指定位为 0、其余位为 1 的 32 位掩码

##### Gen64BitValue0(int index, params int[] other)

```csharp
public static ulong Gen64BitValue0(int index, params int[] other)
```

**描述：** 生成64位数据，指定下标值为0

**参数：**
- `index` (int): 位下标
- `other` (int[]): 其他位下标

**返回值：**
- `ulong`: 指定位为 0、其余位为 1 的 64 位掩码

##### Gen64BitValue0(int[] indexs)

```csharp
public static ulong Gen64BitValue0(int[] indexs)
```

**描述：** 生成64位数据，指定下标值为0

**参数：**
- `indexs` (int[]): 位下标数组

**返回值：**
- `ulong`: 指定位为 0、其余位为 1 的 64 位掩码

---

### FileUtil

文件操作工具类，提供文件的存在检测、复制、移动、重命名与删除等操作。

```csharp
public static class FileUtil
```

#### 主要方法

##### Exists(string path)

```csharp
public static bool Exists(string path)
```

**描述：** 检测文件是否存在

**参数：**
- `path` (string): 文件路径

**返回值：**
- `bool`: 路径非空且文件存在时为 `true`

##### GetExtension(string path)

```csharp
public static string GetExtension(string path)
```

**描述：** 取文件扩展名

**参数：**
- `path` (string): 文件路径

**返回值：**
- `string`: 最后一个 `.` 之后的扩展名（不含点）。路径为空或无扩展名时返回空字符串。

##### MoveFile(string oldPath, string newPath)

```csharp
public static void MoveFile(string oldPath, string newPath)
```

**描述：** 移动文件。注意：自行保证路径的存在性。

**参数：**
- `oldPath` (string): 源文件路径
- `newPath` (string): 目标文件路径

##### CopyFile(string srcPath, string destPath, bool overwrite = false)

```csharp
public static void CopyFile(string srcPath, string destPath, bool overwrite = false)
```

**描述：** 复制文件。注意：自行保证路径的存在性。

**参数：**
- `srcPath` (string): 源文件路径
- `destPath` (string): 目标文件路径
- `overwrite` (bool): 是否覆盖已存在的目标文件，默认 `false`

##### RenameFile(string filePath, string newName)

```csharp
public static void RenameFile(string filePath, string newName)
```

**描述：** 文件重命名

**参数：**
- `filePath` (string): 文件路径
- `newName` (string): 新文件名

##### DeleteFile(string filePath, bool force)

```csharp
public static void DeleteFile(string filePath, bool force)
```

**描述：** 删除文件

**参数：**
- `filePath` (string): 文件路径
- `force` (bool): 是否强制。为 `true` 时会先清除只读属性。

##### DeleteAllFile(string rootPath, bool recursive, bool force)

```csharp
public static void DeleteAllFile(string rootPath, bool recursive, bool force)
```

**描述：** 删除全部文件

**参数：**
- `rootPath` (string): 根路径（文件或目录）
- `recursive` (bool): 是否递归
- `force` (bool): 强制

##### IsReadOnly(FileInfo fileInfo)

```csharp
public static bool IsReadOnly(FileInfo fileInfo)
```

**描述：** 判断文件是否为只读

**参数：**
- `fileInfo` (FileInfo): 文件信息

**返回值：**
- `bool`: 是否设置了只读属性

---

### PathUtil

路径处理工具类，提供路径格式化、合并、比较与解析等操作。

```csharp
public static class PathUtil
```

#### 主要方法

##### IsAbsPath(string path)

```csharp
public static bool IsAbsPath(string path)
```

**描述：** 是否为绝对路径

**参数：**
- `path` (string): 路径

**返回值：**
- `bool`: 是否为绝对路径

##### Format2LinuxPath(string path)

```csharp
public static string Format2LinuxPath(string path)
```

**描述：** 格式化为Linux路径格式

**参数：**
- `path` (string): 路径

**返回值：**
- `string`: 将 `\` 替换为 `/` 后的路径

##### Format2WindowsPath(string path)

```csharp
public static string Format2WindowsPath(string path)
```

**描述：** 格式化为Windows路径格式

**参数：**
- `path` (string): 路径

**返回值：**
- `string`: 将 `/` 替换为 `\` 后的路径

##### CombineLinuxPath(string basePath, string path, params string[] paths)

```csharp
public static string CombineLinuxPath(string basePath, string path, params string[] paths)
```

**描述：** 合并路径并转为Linux路径格式

**参数：**
- `basePath` (string): 基础路径
- `path` (string): 要合并的路径
- `paths` (string[]): 其余路径段

**返回值：**
- `string`: 合并并格式化为 Linux 风格的路径

##### CombineWindowsPath(string basePath, string path, params string[] paths)

```csharp
public static string CombineWindowsPath(string basePath, string path, params string[] paths)
```

**描述：** 合并路径并转为Windows路径格式

**参数：**
- `basePath` (string): 基础路径
- `path` (string): 要合并的路径
- `paths` (string[]): 其余路径段

**返回值：**
- `string`: 合并并格式化为 Windows 风格的路径

##### CombinePath(string basePath, string path, params string[] paths)

```csharp
public static string CombinePath(string basePath, string path, params string[] paths)
```

**描述：** 合并路径

**参数：**
- `basePath` (string): 基础路径
- `path` (string): 要合并的路径
- `paths` (string[]): 其余路径段

**返回值：**
- `string`: 合并后的路径。后续路径段若以分隔符开头会被去掉，以避免 `Path.Combine` 丢弃前半段路径。

##### GetParentDirectory(string path)

```csharp
public static string GetParentDirectory(string path)
```

**描述：** 取上一级目录

**参数：**
- `path` (string): 路径

**返回值：**
- `string`: Linux格式的路径

##### GetFileName(string path)

```csharp
public static string GetFileName(string path)
```

**描述：** 取路径下的 当前文件名 或 当前目录名

**参数：**
- `path` (string): 路径

**返回值：**
- `string`: 当前文件名或当前目录名

##### ClearExtension(string path)

```csharp
public static string ClearExtension(string path)
```

**描述：** 清除文件路径的扩展名

**参数：**
- `path` (string): 路径

**返回值：**
- `string`: 去掉扩展名后的路径

##### ComparePath(string path1, string path2)

```csharp
public static int ComparePath(string path1, string path2)
```

**描述：** 比较两条路径，常用于路径排序。比较条件：层级数 > 字符

**参数：**
- `path1` (string): 第一条路径
- `path2` (string): 第二条路径

**返回值：**
- `int`: 先按层级数量比较，相同再按忽略大小写的字符序比较

##### GetPathLevel(string path)

```csharp
public static int GetPathLevel(string path)
```

**描述：** 获取路径的层级数量

**参数：**
- `path` (string): 路径

**返回值：**
- `int`: 路径中 `/` 的个数

---

### TextUtil

文本处理工具类，提供字符文件的读取、覆盖写入与追加。

```csharp
public static class TextUtil
```

#### 主要方法

##### ReadText(string filePath)

```csharp
public static string ReadText(string filePath)
```

**描述：** 读取字符文件内容

**参数：**
- `filePath` (string): 文件路径

**返回值：**
- `string`: 以 UTF-8 读取的文件内容

##### ReadText(string filePath, Encoding encoding)

```csharp
public static string ReadText(string filePath, Encoding encoding)
```

**描述：** 读取字符文件内容

**参数：**
- `filePath` (string): 文件路径
- `encoding` (Encoding): 字符编码

**返回值：**
- `string`: 按指定编码读取的文件内容

##### CreateFileWithText(string filePath, string text)

```csharp
public static void CreateFileWithText(string filePath, string text)
```

**描述：** 创建或打开一个文本文件，并覆盖内容

**参数：**
- `filePath` (string): 文件路径
- `text` (string): 文件内容

##### CreateFileWithText(string filePath, string text, Encoding encoding)

```csharp
public static void CreateFileWithText(string filePath, string text, Encoding encoding)
```

**描述：** 创建或打开一个文本文件，并覆盖内容

**参数：**
- `filePath` (string): 文件路径
- `text` (string): 文件内容
- `encoding` (Encoding): 字符编码

##### AppendTextToFile(string filePath, string text)

```csharp
public static void AppendTextToFile(string filePath, string text)
```

**描述：** 创建或打开一个文本文件，并追加内容

**参数：**
- `filePath` (string): 文件路径
- `text` (string): 文件内容

##### AppendTextToFile(string filePath, string text, Encoding encoding)

```csharp
public static void AppendTextToFile(string filePath, string text, Encoding encoding)
```

**描述：** 创建或打开一个文本文件，并追加内容

**参数：**
- `filePath` (string): 文件路径
- `text` (string): 文件内容
- `encoding` (Encoding): 字符编码

---

### DirectoryUtil

目录操作工具类，提供目录的检测、创建、复制、移动、清空与遍历等操作。

```csharp
public static class DirectoryUtil
```

#### 主要方法

##### Exists(string path)

```csharp
public static bool Exists(string path)
```

**描述：** 检测目录是否存在。允许此参数 path 指定相对路径或绝对路径信息。相对路径信息将解释为相对于当前工作目录。

**参数：**
- `path` (string): 目录路径

**返回值：**
- `bool`: 路径非空且目录存在时为 `true`

##### IsEmpty(string path)

```csharp
public static bool IsEmpty(string path)
```

**描述：** 检测目录是否为空目录。当前目录不存在，返回为true。

**参数：**
- `path` (string): 目录路径

**返回值：**
- `bool`: 目录不存在或没有任何文件系统项时为 `true`

##### MoveDir(string oldPath, string newPath)

```csharp
public static void MoveDir(string oldPath, string newPath)
```

**描述：** 移动文件夹。注意：自行保证路径的存在性。

**参数：**
- `oldPath` (string): 源目录路径
- `newPath` (string): 目标目录路径

##### CopyDir(string oldPath, string newPath)

```csharp
public static void CopyDir(string oldPath, string newPath)
```

**描述：** 复制文件夹。注意：自行保证路径的存在性。

**参数：**
- `oldPath` (string): 源目录路径
- `newPath` (string): 目标目录路径

##### RenameDir(string dirPath, string newName)

```csharp
public static void RenameDir(string dirPath, string newName)
```

**描述：** 文件夹重命名

**参数：**
- `dirPath` (string): 目录路径
- `newName` (string): 新目录名

##### DeleteDir(string dir, bool force)

```csharp
public static void DeleteDir(string dir, bool force)
```

**描述：** 删除文件夹

**参数：**
- `dir` (string): 目录路径
- `force` (bool): 强制。为 `true` 时会先删除内部文件并尝试清除只读属性。

##### ClearDir(string dir, bool force)

```csharp
public static void ClearDir(string dir, bool force)
```

**描述：** 清空文件夹

**参数：**
- `dir` (string): 目录路径
- `force` (bool): 强制

##### MakeDir(string path, bool keepExist = true)

```csharp
public static void MakeDir(string path, bool keepExist = true)
```

**描述：** 创建一个文件夹

**参数：**
- `path` (string): 目录路径
- `keepExist` (bool): 目录已存在时是否保留，默认 `true`。为 `false` 时会先删除再创建。

##### MakeDirAll(string path, bool keepExist = true)

```csharp
public static void MakeDirAll(string path, bool keepExist = true)
```

**描述：** 创建一个文件夹

**参数：**
- `path` (string): 目录路径
- `keepExist` (bool): 目录已存在时是否保留，默认 `true`

##### GetFiles(string folderPath, string searchPattern = "*")

```csharp
public static string[] GetFiles(string folderPath, string searchPattern = "*")
```

**描述：** 取目录下文件列表, 仅限当前目录

**参数：**
- `folderPath` (string): 目录路径
- `searchPattern` (string): 搜索模式，默认 `"*"`

**返回值：**
- `string[]`: 文件路径数组。路径为空或目录不存在时返回 `null`。

##### GetAllFiles(string folderPath, string searchPattern = "*")

```csharp
public static string[] GetAllFiles(string folderPath, string searchPattern = "*")
```

**描述：** 取目录下文件列表, 递归

**参数：**
- `folderPath` (string): 目录路径
- `searchPattern` (string): 搜索模式，默认 `"*"`

**返回值：**
- `string[]`: 递归得到的文件路径数组。路径为空或目录不存在时返回 `null`。

---

### ReflexUtil

反射工具类，以扩展方法读取对象的字段与属性。

```csharp
public static class ReflexUtil
```

#### 主要方法

##### GetFieldInt(this object obj, string name)

```csharp
public static int GetFieldInt(this object obj, string name)
```

**描述：** 通过反射获取指定字段并转换为 `int`

**参数：**
- `obj` (object): 目标对象
- `name` (string): 字段名

**返回值：**
- `int`: 字段值；字段不存在或为 `null` 时返回 `0`

##### GetFieldUint(this object obj, string name)

```csharp
public static uint GetFieldUint(this object obj, string name)
```

**描述：** 通过反射获取指定字段并转换为 `uint`

**参数：**
- `obj` (object): 目标对象
- `name` (string): 字段名

**返回值：**
- `uint`: 字段值；字段不存在或为 `null` 时返回 `0`

##### GetFieldString(this object obj, string name)

```csharp
public static string GetFieldString(this object obj, string name)
```

**描述：** 通过反射获取指定字段并转换为字符串

**参数：**
- `obj` (object): 目标对象
- `name` (string): 字段名

**返回值：**
- `string`: 字段的字符串表示；值为 `null` 时返回 `null`

##### GetFieldBool(this object obj, string name)

```csharp
public static bool GetFieldBool(this object obj, string name)
```

**描述：** 通过反射获取指定字段并转换为 `bool`

**参数：**
- `obj` (object): 目标对象
- `name` (string): 字段名

**返回值：**
- `bool`: 字段值；字段不存在或为 `null` 时返回 `false`

##### GetField(this object obj, string name)

```csharp
public static object GetField(this object obj, string name)
```

**描述：** 通过反射获取指定字段的值

**参数：**
- `obj` (object): 目标对象
- `name` (string): 字段名

**返回值：**
- `object`: 字段值；字段不存在时返回 `null`

##### GetField\<T\>(this object obj, string name)

```csharp
public static T GetField<T>(this object obj, string name)
```

**描述：** 通过反射获取指定字段并转换为 `T`

**参数：**
- `obj` (object): 目标对象
- `name` (string): 字段名

**返回值：**
- `T`: 字段值；类型不匹配时返回 `default(T)`

##### GetPropertyInt(this object obj, string name)

```csharp
public static int GetPropertyInt(this object obj, string name)
```

**描述：** 通过反射获取指定属性并转换为 `int`

**参数：**
- `obj` (object): 目标对象
- `name` (string): 属性名

**返回值：**
- `int`: 属性值；属性不存在或为 `null` 时返回 `0`

##### GetPropertyUint(this object obj, string name)

```csharp
public static uint GetPropertyUint(this object obj, string name)
```

**描述：** 通过反射获取指定属性并转换为 `uint`

**参数：**
- `obj` (object): 目标对象
- `name` (string): 属性名

**返回值：**
- `uint`: 属性值；属性不存在或为 `null` 时返回 `0`

##### GetPropertyString(this object obj, string name)

```csharp
public static string GetPropertyString(this object obj, string name)
```

**描述：** 通过反射获取指定属性并转换为字符串

**参数：**
- `obj` (object): 目标对象
- `name` (string): 属性名

**返回值：**
- `string`: 属性的字符串表示；值为 `null` 时返回 `null`

##### GetPropertyBool(this object obj, string name)

```csharp
public static bool GetPropertyBool(this object obj, string name)
```

**描述：** 通过反射获取指定属性并转换为 `bool`

**参数：**
- `obj` (object): 目标对象
- `name` (string): 属性名

**返回值：**
- `bool`: 属性值；属性不存在或为 `null` 时返回 `false`

##### GetProperty(this object obj, string name)

```csharp
public static object GetProperty(this object obj, string name)
```

**描述：** 通过反射获取指定属性的值

**参数：**
- `obj` (object): 目标对象
- `name` (string): 属性名

**返回值：**
- `object`: 属性值；属性不存在时返回 `null`

##### GetProperty\<T\>(this object obj, string name)

```csharp
public static T GetProperty<T>(this object obj, string name)
```

**描述：** 通过反射获取指定属性并转换为 `T`

**参数：**
- `obj` (object): 目标对象
- `name` (string): 属性名

**返回值：**
- `T`: 属性值；类型不匹配时返回 `default(T)`

##### Object2T\<T\>(object value)

```csharp
public static T Object2T<T>(object value)
```

**描述：** 将 `object` 转换为 `T`

**参数：**
- `value` (object): 源值

**返回值：**
- `T`: 转换结果。`value` 不是 `T` 时返回 `default(T)`。

---

### PrintUtil

打印工具类，提供对象与数组的字符串化扩展方法，多用于调试或打印。

```csharp
public static class PrintUtil
```

#### 主要方法

##### ToStringText\<T\>(this T o)

```csharp
public static string ToStringText<T>(this T o)
```

**描述：** 字符串化

**参数：**
- `o` (T): 要转换的对象

**返回值：**
- `string`: 字符串表示。`int` 使用插值格式，其余调用 `ToString()`。

##### ToStringText\<T\>(this T[] arr)

```csharp
public static string ToStringText<T>(this T[] arr)
```

**描述：** 一维数组转字符串表示，多用于调试或打印

**参数：**
- `arr` (T[]): 一维数组

**返回值：**
- `string`: `null` 返回 `"null"`，空数组返回 `"[]"`，否则返回形如 `[a,b,c]` 的字符串。

##### ToStringText\<T\>(this T[][] arr)

```csharp
public static string ToStringText<T>(this T[][] arr)
```

**描述：** 二维数组转字符串表示，多用于调试或打印

**参数：**
- `arr` (T[][]): 二维数组

**返回值：**
- `string`: `null` 返回 `"null"`，空数组返回 `"[]"`，否则返回按行缩进的多行字符串。

---

### ConfusedUtil

哈希工具类，提供 MD5 与 SHA-1 摘要计算。

```csharp
public static class ConfusedUtil
```

#### 主要方法

##### Md5(byte[] dataByte)

```csharp
public static string Md5(byte[] dataByte)
```

**描述：** 计算字节数组的 MD5 摘要

**参数：**
- `dataByte` (byte[]): 输入数据

**返回值：**
- `string`: 32 位小写十六进制字符串（左侧以 `0` 补齐）

##### Md5(string dataStr)

```csharp
public static string Md5(string dataStr)
```

**描述：** 计算字符串的 MD5 摘要

**参数：**
- `dataStr` (string): 输入文本（按默认编码转为字节）

**返回值：**
- `string`: 32 位小写十六进制字符串

##### Sha1(byte[] dataByte)

```csharp
public static string Sha1(byte[] dataByte)
```

**描述：** 计算字节数组的 SHA-1 摘要

**参数：**
- `dataByte` (byte[]): 输入数据

**返回值：**
- `string`: 小写十六进制字符串

##### Sha1(string dataStr)

```csharp
public static string Sha1(string dataStr)
```

**描述：** 计算字符串的 SHA-1 摘要

**参数：**
- `dataStr` (string): 输入文本（按默认编码转为字节）

**返回值：**
- `string`: 小写十六进制字符串

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

// 旋转二维数组
var rotated = ArrayUtil.Rotated(array2);
```

### 位操作

```csharp
// 检查位是否有效
bool isValid = BitUtil.IsValid(15, 2); // 检查第2位是否为1

// 设置位
int result = BitUtil.SetValid(8, 1, true); // 设置第1位为1

// 检查多个位
bool allValid = BitUtil.IsValidAnd(15, 0, 1, 2); // 检查第0、1、2位是否都为1
bool anyValid = BitUtil.IsValidOr(8, 0, 1, 3);

// 生成掩码
uint mask = BitUtil.Gen32BitValue1(0, 2, 4);
```

### 文件操作

```csharp
if (FileUtil.Exists("test.txt"))
{
    FileUtil.CopyFile("test.txt", "backup.txt", overwrite: true);
    FileUtil.RenameFile("backup.txt", "backup-renamed.txt");
}

FileUtil.DeleteFile("temp.txt", force: true);
FileUtil.DeleteAllFile("output", recursive: true, force: false);
```

### 路径操作

```csharp
string path = PathUtil.CombinePath("C:", "Users", "Documents", "file.txt");
string linuxPath = PathUtil.CombineLinuxPath("home", "user", "docs", "file.txt");

string fileName = PathUtil.GetFileName(path);
string withoutExt = PathUtil.ClearExtension(path);
string parent = PathUtil.GetParentDirectory(PathUtil.Format2LinuxPath(path));
```

### 文本操作

```csharp
string content = TextUtil.ReadText("test.txt");
TextUtil.CreateFileWithText("output.txt", "Hello World");
TextUtil.AppendTextToFile("output.txt", " more");
```

### 目录操作

```csharp
DirectoryUtil.MakeDir("data");
DirectoryUtil.MakeDirAll("data/sub/nested");

string[] files = DirectoryUtil.GetFiles("data", "*.txt");
string[] allFiles = DirectoryUtil.GetAllFiles("data");

DirectoryUtil.CopyDir("data", "data-backup");
DirectoryUtil.ClearDir("tmp", force: true);
```

### 反射操作

```csharp
int id = obj.GetFieldInt("id");
string name = obj.GetPropertyString("Name");
var nested = obj.GetField<MyType>("nested");
```

### 打印操作

```csharp
int[] nums = { 1, 2, 3 };
string text = nums.ToStringText(); // [1,2,3]

int[][] grid = ArrayUtil.NewArray<int>(2, 3, 0);
string gridText = grid.ToStringText();
```

### 哈希操作

```csharp
string md5 = ConfusedUtil.Md5("Hello World");
string sha1 = ConfusedUtil.Sha1("Hello World");

byte[] bytes = Encoding.UTF8.GetBytes("Hello World");
string md5FromBytes = ConfusedUtil.Md5(bytes);
```

---

## 注意事项

1. **性能考虑：** 大量数组操作时注意内存使用
2. **位操作：** 位索引从0开始，注意边界检查
3. **文件与目录：** `MoveFile` / `CopyFile` / `MoveDir` / `CopyDir` 需自行保证路径存在；删除接口通过 `force` 处理只读文件
4. **路径合并：** `CombinePath` 会去掉后续路径段开头的分隔符，避免 `Path.Combine` 因绝对路径丢弃前半段
5. **文本编码：** `ReadText` / `CreateFileWithText` / `AppendTextToFile` 无编码重载时使用 UTF-8
6. **反射性能：** 反射操作性能较低，避免频繁使用
7. **哈希用途：** `ConfusedUtil` 提供 MD5 / SHA-1 摘要，不是对称或非对称加解密

---

## 依赖关系

- `System`: 基础类型
- `System.Collections.Generic`: 集合类型
- `System.IO`: 文件与目录
- `System.Linq`: LINQ 查询
- `System.Runtime.CompilerServices`: 编译器服务（位运算内联）
- `System.Security.Cryptography`: MD5 / SHA-1
- `System.Text`: 编码与字符串构建
