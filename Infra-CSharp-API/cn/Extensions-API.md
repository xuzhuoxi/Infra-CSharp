# JLGames.Infra.Extensions API 文档

## 概述

Extensions模块提供了各种类型的扩展方法，包括字符串扩展、浮点数扩展、双精度数扩展等，为基本类型提供额外的实用功能。

## 命名空间

`JLGames.Infra.Extensions`

---

## 扩展方法类

### ExtString

字符串扩展方法类，提供字符串的富文本格式化、正则表达式分割等功能。

```csharp
public static class ExtString
```

#### 常量

##### BoldTag

```csharp
public const string BoldTag = "<b>";
```

**描述：** Tag with rich text bold / 富文本粗体的标签

##### BoldTagEnd

```csharp
public const string BoldTagEnd = "</b>";
```

**描述：** End tag with rich text bold / 富文本粗体的结束标签

##### ItalicTag

```csharp
public const string ItalicTag = "<i>";
```

**描述：** Tag with rich text italic / 富文本斜体的标签

##### ItalicTagEnd

```csharp
public const string ItalicTagEnd = "</i>";
```

**描述：** End tag with rich text italic / 富文本斜体的结束标签

##### SizeTagEnd

```csharp
public const string SizeTagEnd = "</size>";
```

**描述：** End tag with rich text font size / 富文本字体大小的结束标签

##### ColorTagEnd

```csharp
public const string ColorTagEnd = "</color>";
```

**描述：** End tag with rich text font color / 富文本字体颜色的结束标签

#### 扩展方法

##### ToRichItalic(this string str)

```csharp
public static string ToRichItalic(this string str)
```

**描述：** Convert to italic rich text / 转化为斜体富文本

**参数：**
- `str` (string): 源字符串

**返回值：**
- `string`: 斜体富文本字符串

**示例：**
```csharp
string text = "Hello World";
string italicText = text.ToRichItalic(); // 结果: "<i>Hello World</i>"
```

##### ToRichBold(this string str)

```csharp
public static string ToRichBold(this string str)
```

**描述：** Convert to bold rich text / 转化为粗体富文本

**参数：**
- `str` (string): 源字符串

**返回值：**
- `string`: 粗体富文本字符串

**示例：**
```csharp
string text = "Hello World";
string boldText = text.ToRichBold(); // 结果: "<b>Hello World</b>"
```

##### ToRichSize(this string str, int size)

```csharp
public static string ToRichSize(this string str, int size)
```

**描述：** Convert to rich text with text size / 转化为带文本大小富文本

**参数：**
- `str` (string): 源字符串
- `size` (int): 字体大小

**返回值：**
- `string`: 带字体大小的富文本字符串

**示例：**
```csharp
string text = "Hello World";
string sizedText = text.ToRichSize(16); // 结果: "<size=16>Hello World</size>"
```

##### ToRichSize(this string str, string size)

```csharp
public static string ToRichSize(this string str, string size)
```

**描述：** Convert to rich text with text size / 转化为带文本大小富文本

**参数：**
- `str` (string): 源字符串
- `size` (string): 字体大小字符串

**返回值：**
- `string`: 带字体大小的富文本字符串

**示例：**
```csharp
string text = "Hello World";
string sizedText = text.ToRichSize("large"); // 结果: "<size=large>Hello World</size>"
```

##### ToRichColor(this string str, string color)

```csharp
public static string ToRichColor(this string str, string color)
```

**描述：** Convert to rich text with text color / 转化为带文本颜色富文本

**参数：**
- `str` (string): 源字符串
- `color` (string): 颜色值（支持十六进制或颜色名称）

**返回值：**
- `string`: 带颜色的富文本字符串

**示例：**
```csharp
string text = "Hello World";
string coloredText = text.ToRichColor("red"); // 结果: "<color=red>Hello World</color>"
string hexColoredText = text.ToRichColor("#FF0000"); // 结果: "<color=#FF0000>Hello World</color>"
```

#### 委托

##### MatchedAction

```csharp
public delegate string MatchedAction(string matched);
```

**描述：** Processing behavior when regular matching / 正则匹配时的处理行为

**参数：**
- `matched` (string): 匹配的字符串

**返回值：**
- `string`: 处理后的字符串

#### 扩展方法

##### Split(this string str, Regex regex, bool includeMatched = false, MatchedAction matchedAction = null)

```csharp
public static string[] Split(this string str, Regex regex, bool includeMatched = false, MatchedAction matchedAction = null)
```

**描述：** Split string using regular expression / 使用正则表达式分割字符串

**参数：**
- `str` (string): 源字符串
- `regex` (Regex): 正则表达式
- `includeMatched` (bool, 可选): 是否包含匹配项
- `matchedAction` (MatchedAction, 可选): 匹配项处理函数（当includeMatched=false时忽略）

**返回值：**
- `string[]`: 分割后的字符串数组

**示例：**
```csharp
string text = "Hello123World456Test";
var parts = text.Split(new Regex(@"\d+"), false); // 结果: ["Hello", "World", "Test"]

var partsWithMatch = text.Split(new Regex(@"\d+"), true); // 结果: ["Hello", "123", "World", "456", "Test"]

var partsWithAction = text.Split(new Regex(@"\d+"), true, match => $"<num>{match}</num>"); 
// 结果: ["Hello", "<num>123</num>", "World", "<num>456</num>", "Test"]
```

---

### ExtDouble

双精度浮点数扩展方法类，提供双精度数的比较功能。

```csharp
public static class ExtDouble
```

#### 字段

##### DOUBLE_DELTA

```csharp
private static double DOUBLE_DELTA = 1E-6;
```

**描述：** 双精度数比较的精度阈值

#### 扩展方法

##### DoubleEquals(this double value, double value2)

```csharp
public static bool DoubleEquals(this double value, double value2)
```

**描述：** Determine whether two double data are similar (equal) / 判断两个double数据是否相近(相等)

**参数：**
- `value` (double): 第一个双精度数
- `value2` (double): 第二个双精度数

**返回值：**
- `bool`: 两个数是否相等或相近

**示例：**
```csharp
double a = 0.1 + 0.2;
double b = 0.3;
bool isEqual = a.DoubleEquals(b); // 结果: true（考虑浮点数精度误差）
```

---

### ExtFloat

单精度浮点数扩展方法类，提供单精度数的比较功能。

```csharp
public static class ExtFloat
```

#### 字段

##### FLOAT_DELTA

```csharp
private static double FLOAT_DELTA = 1E-6;
```

**描述：** 单精度数比较的精度阈值

#### 扩展方法

##### FloatEquals(this float value, float value2)

```csharp
public static bool FloatEquals(this float value, float value2)
```

**描述：** Determine whether two float data are similar (equal) / 判断两个float数据是否相近(相等)

**参数：**
- `value` (float): 第一个单精度数
- `value2` (float): 第二个单精度数

**返回值：**
- `bool`: 两个数是否相等或相近

**示例：**
```csharp
float a = 0.1f + 0.2f;
float b = 0.3f;
bool isEqual = a.FloatEquals(b); // 结果: true（考虑浮点数精度误差）
```

---

## 使用示例

### 字符串富文本格式化

```csharp
string text = "Hello World";

// 基本格式化
string boldText = text.ToRichBold();           // <b>Hello World</b>
string italicText = text.ToRichItalic();       // <i>Hello World</i>
string sizedText = text.ToRichSize(18);        // <size=18>Hello World</size>
string coloredText = text.ToRichColor("red");  // <color=red>Hello World</color>

// 组合使用
string richText = text.ToRichBold().ToRichColor("#FF0000").ToRichSize(20);
// 结果: <b><color=#FF0000><size=20>Hello World</size></color></b>
```

### 正则表达式分割

```csharp
string text = "Hello123World456Test789";

// 基本分割
var parts = text.Split(new Regex(@"\d+"), false);
// 结果: ["Hello", "World", "Test"]

// 包含匹配项
var partsWithMatch = text.Split(new Regex(@"\d+"), true);
// 结果: ["Hello", "123", "World", "456", "Test", "789"]

// 自定义匹配项处理
var partsWithAction = text.Split(new Regex(@"\d+"), true, match => $"<num>{match}</num>");
// 结果: ["Hello", "<num>123</num>", "World", "<num>456</num>", "Test", "<num>789</num>"]
```

### 浮点数比较

```csharp
// 双精度数比较
double a = 0.1 + 0.2;
double b = 0.3;
bool doubleEqual = a.DoubleEquals(b); // true

// 单精度数比较
float c = 0.1f + 0.2f;
float d = 0.3f;
bool floatEqual = c.FloatEquals(d); // true

// 与标准比较的区别
bool standardEqual = (a == b); // false（由于浮点数精度问题）
bool extensionEqual = a.DoubleEquals(b); // true（考虑精度误差）
```

---

## 注意事项

1. **富文本标签：** 扩展方法生成的富文本标签需要支持富文本的UI组件才能正确显示
2. **颜色格式：** ToRichColor方法支持颜色名称和十六进制格式，十六进制格式会自动添加#前缀
3. **正则表达式：** Split方法使用正则表达式进行分割，确保正则表达式正确
4. **浮点数精度：** FloatEquals和DoubleEquals方法使用1E-6作为精度阈值，可以根据需要调整
5. **性能考虑：** 正则表达式分割对于大字符串可能影响性能，注意使用场景

---

## 依赖关系

- `System`: 基础类型
- `System.Text.RegularExpressions`: 正则表达式功能
- `System.Collections.Generic`: 集合类型 