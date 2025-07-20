# JLGames.Infra.Extensions API Documentation

## Overview

The Extensions module provides various types of extension methods, including string extensions, float extensions, double extensions, etc., providing additional utility functions for basic types.

## Namespace

`JLGames.Infra.Extensions`

---

## Extension Method Classes

### ExtString

String extension method class that provides rich text formatting, regular expression splitting and other functions for strings.

```csharp
public static class ExtString
```

#### Constants

##### BoldTag

```csharp
public const string BoldTag = "<b>";
```

**Description:** Tag with rich text bold

##### BoldTagEnd

```csharp
public const string BoldTagEnd = "</b>";
```

**Description:** End tag with rich text bold

##### ItalicTag

```csharp
public const string ItalicTag = "<i>";
```

**Description:** Tag with rich text italic

##### ItalicTagEnd

```csharp
public const string ItalicTagEnd = "</i>";
```

**Description:** End tag with rich text italic

##### SizeTagEnd

```csharp
public const string SizeTagEnd = "</size>";
```

**Description:** End tag with rich text font size

##### ColorTagEnd

```csharp
public const string ColorTagEnd = "</color>";
```

**Description:** End tag with rich text font color

#### Extension Methods

##### ToRichItalic(this string str)

```csharp
public static string ToRichItalic(this string str)
```

**Description:** Convert to italic rich text

**Parameters:**
- `str` (string): Source string

**Return Value:**
- `string`: Italic rich text string

**Example:**
```csharp
string text = "Hello World";
string italicText = text.ToRichItalic(); // Result: "<i>Hello World</i>"
```

##### ToRichBold(this string str)

```csharp
public static string ToRichBold(this string str)
```

**Description:** Convert to bold rich text

**Parameters:**
- `str` (string): Source string

**Return Value:**
- `string`: Bold rich text string

**Example:**
```csharp
string text = "Hello World";
string boldText = text.ToRichBold(); // Result: "<b>Hello World</b>"
```

##### ToRichSize(this string str, int size)

```csharp
public static string ToRichSize(this string str, int size)
```

**Description:** Convert to rich text with text size

**Parameters:**
- `str` (string): Source string
- `size` (int): Font size

**Return Value:**
- `string`: Rich text string with font size

**Example:**
```csharp
string text = "Hello World";
string sizedText = text.ToRichSize(16); // Result: "<size=16>Hello World</size>"
```

##### ToRichSize(this string str, string size)

```csharp
public static string ToRichSize(this string str, string size)
```

**Description:** Convert to rich text with text size

**Parameters:**
- `str` (string): Source string
- `size` (string): Font size string

**Return Value:**
- `string`: Rich text string with font size

**Example:**
```csharp
string text = "Hello World";
string sizedText = text.ToRichSize("large"); // Result: "<size=large>Hello World</size>"
```

##### ToRichColor(this string str, string color)

```csharp
public static string ToRichColor(this string str, string color)
```

**Description:** Convert to rich text with text color

**Parameters:**
- `str` (string): Source string
- `color` (string): Color value (supports hexadecimal or color names)

**Return Value:**
- `string`: Rich text string with color

**Example:**
```csharp
string text = "Hello World";
string coloredText = text.ToRichColor("red"); // Result: "<color=red>Hello World</color>"
string hexColoredText = text.ToRichColor("#FF0000"); // Result: "<color=#FF0000>Hello World</color>"
```

#### Delegates

##### MatchedAction

```csharp
public delegate string MatchedAction(string matched);
```

**Description:** Processing behavior when regular matching

**Parameters:**
- `matched` (string): Matched string

**Return Value:**
- `string`: Processed string

#### Extension Methods

##### Split(this string str, Regex regex, bool includeMatched = false, MatchedAction matchedAction = null)

```csharp
public static string[] Split(this string str, Regex regex, bool includeMatched = false, MatchedAction matchedAction = null)
```

**Description:** Split string using regular expression

**Parameters:**
- `str` (string): Source string
- `regex` (Regex): Regular expression
- `includeMatched` (bool, optional): Whether to include matched items
- `matchedAction` (MatchedAction, optional): Matched item processing function (ignored when includeMatched=false)

**Return Value:**
- `string[]`: Array of split strings

**Example:**
```csharp
string text = "Hello123World456Test";
var parts = text.Split(new Regex(@"\d+"), false); // Result: ["Hello", "World", "Test"]

var partsWithMatch = text.Split(new Regex(@"\d+"), true); // Result: ["Hello", "123", "World", "456", "Test"]

var partsWithAction = text.Split(new Regex(@"\d+"), true, match => $"<num>{match}</num>"); 
// Result: ["Hello", "<num>123</num>", "World", "<num>456</num>", "Test"]
```

---

### ExtDouble

Double precision floating-point extension methods class that provides comparison functionality for double precision numbers.

```csharp
public static class ExtDouble
```

#### Fields

##### DOUBLE_DELTA

```csharp
private static double DOUBLE_DELTA = 1E-6;
```

**Description:** Precision threshold for double precision number comparison

#### Extension Methods

##### DoubleEquals(this double value, double value2)

```csharp
public static bool DoubleEquals(this double value, double value2)
```

**Description:** Determine whether two double data are similar (equal)

**Parameters:**
- `value` (double): First double precision number
- `value2` (double): Second double precision number

**Return Value:**
- `bool`: Whether the two numbers are equal or similar

**Example:**
```csharp
double a = 0.1 + 0.2;
double b = 0.3;
bool isEqual = a.DoubleEquals(b); // Result: true (considering floating-point precision error)
```

---

### ExtFloat

Single precision floating-point extension methods class that provides comparison functionality for single precision numbers.

```csharp
public static class ExtFloat
```

#### Fields

##### FLOAT_DELTA

```csharp
private static double FLOAT_DELTA = 1E-6;
```

**Description:** Precision threshold for single precision number comparison

#### Extension Methods

##### FloatEquals(this float value, float value2)

```csharp
public static bool FloatEquals(this float value, float value2)
```

**Description:** Determine whether two float data are similar (equal)

**Parameters:**
- `value` (float): First single precision number
- `value2` (float): Second single precision number

**Return Value:**
- `bool`: Whether the two numbers are equal or similar

**Example:**
```csharp
float a = 0.1f + 0.2f;
float b = 0.3f;
bool isEqual = a.FloatEquals(b); // Result: true (considering floating-point precision error)
```

---

## Usage Examples

### String Rich Text Formatting

```csharp
string text = "Hello World";

// Basic formatting
string boldText = text.ToRichBold();           // <b>Hello World</b>
string italicText = text.ToRichItalic();       // <i>Hello World</i>
string sizedText = text.ToRichSize(18);        // <size=18>Hello World</size>
string coloredText = text.ToRichColor("red");  // <color=red>Hello World</color>

// Combined usage
string richText = text.ToRichBold().ToRichColor("#FF0000").ToRichSize(20);
// Result: <b><color=#FF0000><size=20>Hello World</size></color></b>
```

### Regular Expression Splitting

```csharp
string text = "Hello123World456Test789";

// Basic splitting
var parts = text.Split(new Regex(@"\d+"), false);
// Result: ["Hello", "World", "Test"]

// Include matched items
var partsWithMatch = text.Split(new Regex(@"\d+"), true);
// Result: ["Hello", "123", "World", "456", "Test", "789"]

// Custom matched item processing
var partsWithAction = text.Split(new Regex(@"\d+"), true, match => $"<num>{match}</num>");
// Result: ["Hello", "<num>123</num>", "World", "<num>456</num>", "Test", "<num>789</num>"]
```

### Floating-Point Number Comparison

```csharp
// Double precision comparison
double a = 0.1 + 0.2;
double b = 0.3;
bool doubleEqual = a.DoubleEquals(b); // true

// Single precision comparison
float c = 0.1f + 0.2f;
float d = 0.3f;
bool floatEqual = c.FloatEquals(d); // true

// Difference from standard comparison
bool standardEqual = (a == b); // false (due to floating-point precision issues)
bool extensionEqual = a.DoubleEquals(b); // true (considering precision error)
```

---

## Notes

1. **Rich Text Tags:** Rich text tags generated by extension methods require UI components that support rich text to display correctly
2. **Color Format:** ToRichColor method supports color names and hexadecimal format, hexadecimal format automatically adds # prefix
3. **Regular Expressions:** Split method uses regular expressions for splitting, ensure regular expressions are correct
4. **Floating-Point Precision:** FloatEquals and DoubleEquals methods use 1E-6 as precision threshold, can be adjusted as needed
5. **Performance Considerations:** Regular expression splitting may affect performance for large strings, pay attention to usage scenarios

---

## Dependencies

- `System`: Basic types
- `System.Text.RegularExpressions`: Regular expression functionality
- `System.Collections.Generic`: Collection types 