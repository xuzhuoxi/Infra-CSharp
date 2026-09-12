# JLGames.Infra.Extensions API 文档

## 概述

Extensions 模块为基本类型提供扩展方法：字符串富文本标签与基于正则的分割，以及 float / double 的近似相等比较。

## 命名空间

`JLGames.Infra.Extensions`

---

## 扩展方法类

### ExtString

字符串扩展方法：富文本标签与基于正则的分割。

```csharp
public static class ExtString
```

#### 常量

##### BoldTag

```csharp
public const string BoldTag = "<b>";
```

**描述：** 富文本粗体的标签

##### BoldTagEnd

```csharp
public const string BoldTagEnd = "</b>";
```

**描述：** 富文本粗体的结束标签

##### ItalicTag

```csharp
public const string ItalicTag = "<i>";
```

**描述：** 富文本斜体的标签

##### ItalicTagEnd

```csharp
public const string ItalicTagEnd = "</i>";
```

**描述：** 富文本斜体的结束标签

##### SizeTagEnd

```csharp
public const string SizeTagEnd = "</size>";
```

**描述：** 富文本字体大小的结束标签

##### ColorTagEnd

```csharp
public const string ColorTagEnd = "</color>";
```

**描述：** 富文本字体颜色的结束标签

#### 扩展方法

##### ToRichItalic(this string str)

```csharp
public static string ToRichItalic(this string str)
```

**描述：** 转化为斜体富文本

**参数：**
- `str` (string): 源文本。

**返回值：**
- `string`: 带斜体标签的富文本。

**示例：**
```csharp
string text = "Hello World";
string italicText = text.ToRichItalic(); // 结果: "<i>Hello World</i>"
```

##### ToRichBold(this string str)

```csharp
public static string ToRichBold(this string str)
```

**描述：** 转化为粗体富文本

**参数：**
- `str` (string): 源文本。

**返回值：**
- `string`: 带粗体标签的富文本。

**示例：**
```csharp
string text = "Hello World";
string boldText = text.ToRichBold(); // 结果: "<b>Hello World</b>"
```

##### ToRichSize(this string str, int size)

```csharp
public static string ToRichSize(this string str, int size)
```

**描述：** 转化为带文本大小富文本

**参数：**
- `str` (string): 源文本。
- `size` (int): 字体大小。

**返回值：**
- `string`: 带字号标签的富文本。

**示例：**
```csharp
string text = "Hello World";
string sizedText = text.ToRichSize(16); // 结果: "<size=16>Hello World</size>"
```

##### ToRichSize(this string str, string size)

```csharp
public static string ToRichSize(this string str, string size)
```

**描述：** 转化为带文本大小富文本

**参数：**
- `str` (string): 源文本。
- `size` (string): 字号数值或单位字符串。

**返回值：**
- `string`: 带字号标签的富文本。

**示例：**
```csharp
string text = "Hello World";
string sizedText = text.ToRichSize("large"); // 结果: "<size=large>Hello World</size>"
```

##### ToRichColor(this string str, string color)

```csharp
public static string ToRichColor(this string str, string color)
```

**描述：** 转化为带文本颜色富文本

**参数：**
- `str` (string): 源文本。
- `color` (string): 颜色（#RRGGBB、无 # 的十六进制或颜色名）。

**返回值：**
- `string`: 带颜色标签的富文本。

**示例：**
```csharp
string text = "Hello World";
string coloredText = text.ToRichColor("red"); // 结果: "<color=red>Hello World</color>"
string hexColoredText = text.ToRichColor("#FF0000"); // 结果: "<color=#FF0000>Hello World</color>"
string hexNoHash = text.ToRichColor("FF0000"); // 结果: "<color=#FF0000>Hello World</color>"
```

#### 委托

##### MatchedAction

```csharp
public delegate string MatchedAction(string matched);
```

**描述：** 正则匹配时的处理行为

**参数：**
- `matched` (string): 匹配到的子串。

**返回值：**
- `string`: 纳入分割结果中的转换后字符串。

#### 扩展方法

##### Split(this string str, Regex regex, bool includeMatched = false, MatchedAction matchedAction = null)

```csharp
public static string[] Split(this string str, Regex regex, bool includeMatched = false, MatchedAction matchedAction = null)
```

**描述：** 使用正则表达式分割字符串

**参数：**
- `str` (string): 源字符串。
- `regex` (Regex): 用作分隔符的正则表达式。
- `includeMatched` (bool, 可选): 是否将匹配段纳入结果。
- `matchedAction` (MatchedAction, 可选): `includeMatched` 为 true 时对匹配段的转换；否则忽略。

**返回值：**
- `string[]`: 分割后的片段；`str` 为 null 或空时返回 null。`regex` 为 null 时返回仅含源字符串的数组。

**示例：**
```csharp
string text = "Hello123World456Test";
var parts = text.Split(new Regex(@"\d+"), false); // 结果: ["Hello", "World", "Test"]

var partsWithMatch = text.Split(new Regex(@"\d+"), true); // 结果: ["Hello", "123", "World", "456", "Test"]

var partsWithAction = text.Split(new Regex(@"\d+"), true, match => $"<num>{match}</num>");
// 结果: ["Hello", "<num>123</num>", "World", "<num>456</num>", "Test"]

string[] empty = "".Split(new Regex(@"\d+")); // null
string[] noRegex = text.Split((Regex)null); // ["Hello123World456Test"]
```

---

### ExtDouble

double 扩展方法：近似相等比较。

```csharp
public static class ExtDouble
```

#### 扩展方法

##### DoubleEquals(this double value, double value2)

```csharp
public static bool DoubleEquals(this double value, double value2)
```

**描述：** 判断两个double数据是否相近(相等)

**参数：**
- `value` (double): 第一个值。
- `value2` (double): 第二个值。

**返回值：**
- `bool`: 相等或差值在 1E-6 以内时为 true。

**示例：**
```csharp
double a = 0.1 + 0.2;
double b = 0.3;
bool isEqual = a.DoubleEquals(b); // 结果: true（考虑浮点数精度误差）
```

---

### ExtFloat

float 扩展方法：近似相等比较。

```csharp
public static class ExtFloat
```

#### 扩展方法

##### FloatEquals(this float value, float value2)

```csharp
public static bool FloatEquals(this float value, float value2)
```

**描述：** 判断两个float数据是否相近(相等)

**参数：**
- `value` (float): 第一个值。
- `value2` (float): 第二个值。

**返回值：**
- `bool`: 相等或差值在 1E-6 以内时为 true。

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
string hexNoHash = text.ToRichColor("FF0000"); // <color=#FF0000>Hello World</color>

// 组合使用：后调用的方法包在最外层
string richText = text.ToRichBold().ToRichColor("#FF0000").ToRichSize(20);
// 结果: <size=20><color=#FF0000><b>Hello World</b></color></size>
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

1. **富文本标签：** 扩展方法生成的富文本标签需要支持富文本的 UI 组件才能正确显示。
2. **颜色格式：** `ToRichColor` 支持 `#RRGGBB`、无 `#` 的十六进制（可解析时自动补 `#`）以及颜色名。
3. **正则分割：** `Split` 在源字符串为 null 或空时返回 null；`regex` 为 null 时返回仅含源字符串的数组。`includeMatched` 为 false 时忽略 `matchedAction`。
4. **浮点数精度：** `FloatEquals` 与 `DoubleEquals` 在完全相等或绝对差小于 1E-6 时视为相近；该阈值实现为内部常量，不可从外部调整。
5. **性能考虑：** 正则表达式分割对于大字符串可能影响性能，注意使用场景。

---

## 依赖关系

- `System`: 基础类型
- `System.Text.RegularExpressions`: 正则表达式功能
- `System.Collections.Generic`: 集合类型
