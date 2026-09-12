# JLGames.Infra.Extensions API Documentation

## Overview

The Extensions module provides extension methods for basic types: string rich-text tags and regex-based splitting, plus approximate equality comparison for float and double.

## Namespace

`JLGames.Infra.Extensions`

---

## Extension Method Classes

### ExtString

String extension methods: rich text tags and regex-based splitting.

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
- `str` (string): Source text.

**Return Value:**
- `string`: Rich text wrapped with italic tags.

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
- `str` (string): Source text.

**Return Value:**
- `string`: Rich text wrapped with bold tags.

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
- `str` (string): Source text.
- `size` (int): Font size.

**Return Value:**
- `string`: Rich text wrapped with size tag.

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
- `str` (string): Source text.
- `size` (string): Font size value or unit string.

**Return Value:**
- `string`: Rich text wrapped with size tag.

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
- `str` (string): Source text.
- `color` (string): Color as #RRGGBB, hex without #, or color name.

**Return Value:**
- `string`: Rich text wrapped with color tag.

**Example:**
```csharp
string text = "Hello World";
string coloredText = text.ToRichColor("red"); // Result: "<color=red>Hello World</color>"
string hexColoredText = text.ToRichColor("#FF0000"); // Result: "<color=#FF0000>Hello World</color>"
string hexNoHash = text.ToRichColor("FF0000"); // Result: "<color=#FF0000>Hello World</color>"
```

#### Delegates

##### MatchedAction

```csharp
public delegate string MatchedAction(string matched);
```

**Description:** Processing behavior when regular matching

**Parameters:**
- `matched` (string): Matched substring.

**Return Value:**
- `string`: Transformed string to include in split result.

#### Extension Methods

##### Split(this string str, Regex regex, bool includeMatched = false, MatchedAction matchedAction = null)

```csharp
public static string[] Split(this string str, Regex regex, bool includeMatched = false, MatchedAction matchedAction = null)
```

**Description:** Split string using regular expression

**Parameters:**
- `str` (string): Source string.
- `regex` (Regex): Pattern used as delimiters.
- `includeMatched` (bool, optional): Whether matched segments are included in the result.
- `matchedAction` (MatchedAction, optional): Transform for matched segments when `includeMatched` is true; ignored otherwise.

**Return Value:**
- `string[]`: Split segments; null when `str` is null or empty. When `regex` is null, returns an array containing the source string.

**Example:**
```csharp
string text = "Hello123World456Test";
var parts = text.Split(new Regex(@"\d+"), false); // Result: ["Hello", "World", "Test"]

var partsWithMatch = text.Split(new Regex(@"\d+"), true); // Result: ["Hello", "123", "World", "456", "Test"]

var partsWithAction = text.Split(new Regex(@"\d+"), true, match => $"<num>{match}</num>");
// Result: ["Hello", "<num>123</num>", "World", "<num>456</num>", "Test"]

string[] empty = "".Split(new Regex(@"\d+")); // null
string[] noRegex = text.Split((Regex)null); // ["Hello123World456Test"]
```

---

### ExtDouble

Double extension methods for approximate equality comparison.

```csharp
public static class ExtDouble
```

#### Extension Methods

##### DoubleEquals(this double value, double value2)

```csharp
public static bool DoubleEquals(this double value, double value2)
```

**Description:** Determine whether two double data are similar (equal)

**Parameters:**
- `value` (double): First value.
- `value2` (double): Second value.

**Return Value:**
- `bool`: True if values are equal or within epsilon (1E-6).

**Example:**
```csharp
double a = 0.1 + 0.2;
double b = 0.3;
bool isEqual = a.DoubleEquals(b); // Result: true (considering floating-point precision error)
```

---

### ExtFloat

Float extension methods for approximate equality comparison.

```csharp
public static class ExtFloat
```

#### Extension Methods

##### FloatEquals(this float value, float value2)

```csharp
public static bool FloatEquals(this float value, float value2)
```

**Description:** Determine whether two float data are similar (equal)

**Parameters:**
- `value` (float): First value.
- `value2` (float): Second value.

**Return Value:**
- `bool`: True if values are equal or within epsilon (1E-6).

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
string hexNoHash = text.ToRichColor("FF0000"); // <color=#FF0000>Hello World</color>

// Combined usage: later calls wrap the outer tags
string richText = text.ToRichBold().ToRichColor("#FF0000").ToRichSize(20);
// Result: <size=20><color=#FF0000><b>Hello World</b></color></size>
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

1. **Rich Text Tags:** Rich text tags generated by extension methods require UI components that support rich text to display correctly.
2. **Color Format:** `ToRichColor` accepts `#RRGGBB`, hex without `#` (a `#` prefix is added when the value parses as hex), or a color name.
3. **Regex Split:** `Split` returns null when the source string is null or empty; when `regex` is null, it returns an array containing the source string. `matchedAction` is ignored when `includeMatched` is false.
4. **Floating-Point Precision:** `FloatEquals` and `DoubleEquals` treat values as similar when they are exactly equal or the absolute difference is less than 1E-6. That epsilon is an internal constant and cannot be changed from outside.
5. **Performance Considerations:** Regular expression splitting may affect performance for large strings; consider the usage scenario.

---

## Dependencies

- `System`: Basic types
- `System.Text.RegularExpressions`: Regular expression functionality
- `System.Collections.Generic`: Collection types
