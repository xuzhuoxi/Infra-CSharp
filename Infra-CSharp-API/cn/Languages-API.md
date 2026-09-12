# JLGames.Infra.Languages API 文档

## 概述

Languages 模块提供嵌入式 Lua 解释器：解析 Lua 源码、在 `LuaTable` 环境中执行、以及一组标准库。实现基于第三方库 Lua Interpreter（MIT），语法接近 Lua 5.1。

入口类 `LuaInterpreter` 的方法全部为静态方法，无需实例化。不存在 `Execute` / `ExecuteFile` 这类实例方法。

## 命名空间

- `JLGames.Infra.Languages.Lua` — 解释器、值类型、解析器与扩展
- `JLGames.Infra.Languages.Lua.Library` — 标准库注册类

源码中部分标识符沿用原库拼写，例如 `Enviroment`、`GetEorrorMessages`，调用时必须使用这些实际名称。

---

## 主要组件

### LuaInterpreter

Lua 解释器入口。解析脚本、创建默认全局环境并执行。

```csharp
public class LuaInterpreter
```

#### 主要功能

- **脚本解析：** 将 Lua 源码解析为可执行 `Chunk`
- **代码执行：** 在全局或自定义 `LuaTable` 环境中运行
- **文件执行：** 读取文件内容后执行
- **标准库：** `CreateGlobalEnviroment` 会注册常用标准库
- **宿主扩展：** 通过环境表 `Register` / `RegisterMethodFunction` 注入 C# 函数

#### 主要方法

##### Interpreter(string luaCode)

```csharp
public static LuaValue Interpreter(string luaCode)
```

**描述：** 使用默认全局环境执行 Lua 源码。内部调用 `CreateGlobalEnviroment()` 再交给重载版本。

**参数：**
- `luaCode` (string): Lua 源码文本

**返回值：**
- `LuaValue`: 顶层 `return` 的结果；无 `return` 时为 `null`

##### Interpreter(string luaCode, LuaTable enviroment)

```csharp
public static LuaValue Interpreter(string luaCode, LuaTable enviroment)
```

**描述：** 在指定环境表中解析并执行 Lua 源码。环境表即脚本的全局作用域。

**参数：**
- `luaCode` (string): Lua 源码文本
- `enviroment` (LuaTable): 全局环境（源码拼写为 `enviroment`）

**返回值：**
- `LuaValue`: 顶层 `return` 的结果；无 `return` 时为 `null`

##### RunFile(string luaFile)

```csharp
public static LuaValue RunFile(string luaFile)
```

**描述：** 读取文件全部文本，使用默认全局环境执行。

**参数：**
- `luaFile` (string): Lua 文件路径

**返回值：**
- `LuaValue`: 顶层 `return` 的结果；无 `return` 时为 `null`

##### RunFile(string luaFile, LuaTable enviroment)

```csharp
public static LuaValue RunFile(string luaFile, LuaTable enviroment)
```

**描述：** 读取文件全部文本，在指定环境中执行。

**参数：**
- `luaFile` (string): Lua 文件路径
- `enviroment` (LuaTable): 全局环境

**返回值：**
- `LuaValue`: 顶层 `return` 的结果；无 `return` 时为 `null`

##### Parse(string luaCode)

```csharp
public static Chunk Parse(string luaCode)
```

**描述：** 解析 Lua 源码为 `Chunk`，不执行。解析失败时抛出 `ArgumentException`，消息中包含 `Parser.GetEorrorMessages()` 的诊断文本。

**参数：**
- `luaCode` (string): Lua 源码文本

**返回值：**
- `Chunk`: 可执行代码块（需自行设置 `Enviroment` 再 `Execute()`）

**异常：**
- `ArgumentException`: 语法错误

##### CreateGlobalEnviroment()

```csharp
public static LuaTable CreateGlobalEnviroment()
```

**描述：** 创建默认全局环境并注册标准库，然后设置 `_G` 指向该表自身。

**注册内容：**
- `BaseLib.RegisterFunctions` — 全局基础函数
- `StringLib` → `string`
- `TableLib` → `table`
- `IOLib` → `io`
- `FileLib` → `file`
- `MathLib` → `math`
- `OSLib` → `os`
- `_G` — 环境表自身

**返回值：**
- `LuaTable`: 可传入 `Interpreter` / `RunFile` 的环境表

源码中的 `WinFormLib` 已全部注释，不参与编译，默认环境也不会注册 Gui 模块。

---

### LuaValue

Lua 值类型基类，表示脚本中的各种数据类型。实现 `IEquatable<LuaValue>`。

```csharp
public abstract class LuaValue : IEquatable<LuaValue>
```

#### 成员

```csharp
public abstract object Value { get; }
public abstract string GetTypeCode();
public virtual bool GetBooleanValue();          // 默认 true；LuaNil / LuaBoolean.False 为 false
public bool Equals(LuaValue other);
public static LuaValue GetKeyValue(LuaValue baseValue, LuaValue key);
```

`GetTypeCode()` 返回 Lua 类型名字符串，例如 `"number"`、`"string"`、`"boolean"`、`"nil"`、`"table"`、`"function"`、`"userdata"`。

`GetKeyValue` 从表取值；若对象是带 `__index` 元表的 `LuaUserdata`，则走元方法。非表访问会抛出 `Exception`。

#### 派生类型

##### LuaNumber

```csharp
public class LuaNumber : LuaValue
{
    public LuaNumber(double number);
    public double Number { get; set; }
}
```

**描述：** Lua 数字（内部为 `double`）。`GetTypeCode()` 为 `"number"`。

##### LuaString

```csharp
public class LuaString : LuaValue
{
    public LuaString(string text);
    public static readonly LuaString Empty;
    public string Text { get; set; }
}
```

**描述：** Lua 字符串。`GetTypeCode()` 为 `"string"`。

##### LuaBoolean

```csharp
public class LuaBoolean : LuaValue
{
    public static readonly LuaBoolean False;
    public static readonly LuaBoolean True;
    public bool BoolValue { get; set; }
    public static LuaBoolean From(bool value);
}
```

**描述：** Lua 布尔值。构造函数为私有，请使用 `True` / `False` 或 `From(bool)`。`GetTypeCode()` 为 `"boolean"`。

##### LuaNil

```csharp
public class LuaNil : LuaValue
{
    public static readonly LuaNil Nil;
}
```

**描述：** Lua `nil` 单例。`Value` 为 `null`，`GetBooleanValue()` 为 `false`，`GetTypeCode()` 为 `"nil"`。

##### LuaTable

```csharp
public class LuaTable : LuaValue
{
    public LuaTable();
    public LuaTable(LuaTable parent);   // 以 parent 作为 __index / __newindex
    public LuaTable MetaTable { get; set; }
    public int Length { get; }          // 数组部分长度
    public int Count { get; }           // 哈希部分条目数
    public IEnumerable<LuaValue> ListValues { get; }
    public IEnumerable<LuaValue> Keys { get; }
    public IEnumerable<KeyValuePair<LuaValue, LuaValue>> KeyValuePairs { get; }
}
```

**描述：** Lua 表。数组下标从 **1** 开始。`GetTypeCode()` 为 `"table"`。带 `parent` 的构造函数用于嵌套作用域。

**常用方法：**

| 方法 | 说明 |
| --- | --- |
| `GetValue(int index)` | 按 1 基数组下标取值，越界返回 `LuaNil.Nil` |
| `GetValue(string name)` | 按名字取值，可走 `__index` |
| `GetValue(LuaValue key)` | 按键取值，可走 `__index` |
| `SetNameValue(string name, LuaValue value)` | 按名字写入；值为 `LuaNil.Nil` 时删除键 |
| `SetKeyValue(LuaValue key, LuaValue value)` | 按键写入（整数键写入数组部分） |
| `RawGetValue(LuaValue key)` / `RawSetValue(string name, LuaValue value)` | 不走元表 |
| `Register(string name, LuaFunc function)` | 把 C# 委托注册为表上的 Lua 函数 |
| `AddValue` / `InsertValue` / `Remove` / `RemoveAt` | 数组部分增删（`InsertValue`/`RemoveAt` 为 1 基下标） |
| `Sort()` / `Sort(LuaFunction compare)` | 排序数组部分 |
| `ContainsKey(LuaValue key)` | 是否包含键 |
| `GetKey(string key)` | 在哈希部分查找同名 `LuaString` 键 |

##### LuaFunction

```csharp
public delegate LuaValue LuaFunc(LuaValue[] args);

public class LuaFunction : LuaValue
{
    public LuaFunction(LuaFunc function);
    public LuaFunc Function { get; set; }
    public LuaValue Invoke(LuaValue[] args);
}
```

**描述：** Lua 函数。`GetTypeCode()` 为 `"function"`。宿主向脚本注入函数时，通常使用 `LuaTable.Register` 或 `LuaMethodInfo`。

##### LuaUserdata

```csharp
public class LuaUserdata : LuaValue
{
    public LuaUserdata(object obj);
    public LuaUserdata(object obj, LuaTable metatable);
    public LuaTable MetaTable { get; set; }
}
```

**描述：** 包装任意 CLR 对象。`Value` 为被包装对象，`GetTypeCode()` 为 `"userdata"`。文件句柄等通过 userdata + 元表暴露给脚本。

##### LuaMultiValue

```csharp
public class LuaMultiValue : LuaValue
{
    public LuaMultiValue(LuaValue[] values);
    public LuaValue[] Values { get; set; }
    public static LuaValue WrapLuaValues(LuaValue[] values);
    public static LuaValue[] UnWrapLuaValues(LuaValue[] values);
}
```

**描述：** 多返回值容器。`GetTypeCode()` 会抛出 `InvalidOperationException`。`WrapLuaValues`：空 → `LuaNil.Nil`，单值原样返回，多值包装为 `LuaMultiValue`。

##### LuaError

```csharp
public class LuaError : Exception
{
    public LuaError(string message);
    public LuaError(string message, Exception innerException);
    public LuaError(string messageformat, params object[] args);
}
```

**描述：** 运行期 Lua 错误（`error()` / `assert()` 等）。**不是** `LuaException`，也没有 `LineNumber` 属性。语法错误由 `Parse` 抛出 `ArgumentException`。

---

### 标准库

标准库位于 `JLGames.Infra.Languages.Lua.Library`。嵌入方一般不必直接调用库函数；`CreateGlobalEnviroment` 会完成注册。若要自定义环境，可按同样方式调用 `RegisterModule` / `RegisterFunctions`。

#### BaseLib

```csharp
public class BaseLib
{
    public static void RegisterFunctions(LuaTable module);
}
```

**描述：** 把基础函数注册到传入的表上（默认环境直接注册到全局表）。

**Lua 侧函数：** `print`、`type`、`getmetatable`、`setmetatable`、`tostring`、`tonumber`、`ipairs`、`pairs`、`next`、`assert`、`error`、`rawget`、`rawset`、`select`、`dofile`、`loadstring`、`unpack`、`pcall`

#### MathLib

```csharp
public static class MathLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**描述：** 注册为环境表上的 `math`。常量：`huge`（`double.MaxValue`）、`pi`。

**Lua 侧函数：** `abs`、`acos`、`asin`、`atan`、`atan2`、`ceil`、`cos`、`cosh`、`deg`、`exp`、`floor`、`fmod`、`log`、`log10`、`max`、`min`、`modf`、`pow`、`rad`、`random`、`randomseed`、`sin`、`sinh`、`sqrt`、`tan`、`tanh`

#### StringLib

```csharp
public static class StringLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**描述：** 注册为 `string`。`format` 使用 .NET `string.Format`，不是 Lua `printf` 风格。本实现**没有** `string.find` / `string.gsub`。

**Lua 侧函数：** `byte`、`char`、`format`、`len`、`sub`、`lower`、`upper`、`rep`、`reverse`

#### TableLib

```csharp
public static class TableLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**描述：** 注册为 `table`。`removeitem` 为对本实现的扩展（按值删除）。

**Lua 侧函数：** `concat`、`insert`、`remove`、`removeitem`、`maxn`、`sort`

#### IOLib

```csharp
public static class IOLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**描述：** 注册为 `io`。打开的文件句柄是带 `FileLib` 元表的 `LuaUserdata`。

**Lua 侧函数：** `input`、`output`、`open`、`read`、`write`、`flush`、`tmpfile`

`open` 模式：`"r"`/`"r+"` 读，`"w"`/`"w+"` 写，`"a"`/`"a+"` 追加。

#### FileLib

```csharp
public static class FileLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
    public static LuaTable CreateMetaTable();
}
```

**描述：** 注册为 `file`。`CreateMetaTable` 供 `io.open` 给文件 userdata 挂方法（`__index` 指向元表自身）。

**Lua 侧函数：** `close`、`read`、`write`、`lines`、`flush`、`seek`

`read` 模式：`*l` 一行、`*a` 全部、`*n` 数字，或传入字符数。

#### OSLib

```csharp
public static class OSLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**描述：** 注册为 `os`。

**Lua 侧函数：** `clock`、`date`、`time`、`execute`、`exit`、`getenv`、`remove`、`rename`、`tmpname`

#### WinFormLib

源文件 `WinFormLib.cs` 中的类型已全部注释，**不是**当前公共 API，`CreateGlobalEnviroment` 也不会注册它。

---

### LuaInterpreterExtra

针对解释器的宿主侧扩展，位于同一命名空间 `JLGames.Infra.Languages.Lua`。

#### LuaValueUtils

C# 与 `LuaValue` 之间的转换，以及从 lambda 提取 `MethodInfo`。

```csharp
public static class LuaValueUtils
```

##### ObjectToLuaValue(object o)

```csharp
public static LuaValue ObjectToLuaValue(object o)
```

**描述：** 将对象包装为 `LuaValue`。`null` → `LuaNil.Nil`；`bool` / `string` / 常见数值类型 → 对应 Lua 类型；已是 `LuaValue` 则原样返回；其余调用 `ToString()` 得到 `LuaString`。

##### LuaValueToObject(LuaValue luaValue)

```csharp
public static object LuaValueToObject(LuaValue luaValue)
```

**描述：** 从 `LuaValue` 取出 CLR 值。`LuaNumber` 转为 `float`；其它类型返回 `Value`。

##### StringToInt / StringToFloat / StringToDouble

```csharp
public static int StringToInt(string s);
public static float StringToFloat(string s);
public static double StringToDouble(string s);
```

**描述：** 解析失败返回 `0`。`float`/`double` 使用不变区域性。

##### GetMethodInfo

```csharp
public static MethodInfo GetMethodInfo(LambdaExpression expression);
public static MethodInfo GetMethodInfo<T, TResult>(Expression<Func<T, TResult>> expression);
public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression);
public static MethodInfo GetMethodInfo(Expression<Action> expression);
```

**描述：** 从只包含一次方法调用的 lambda 取出 `MethodInfo`。表达式为空或不是方法调用时抛出 `ArgumentException`。

#### LuaMethodInfo

```csharp
public class LuaMethodInfo : LuaFunction
{
    public LuaMethodInfo(object target, MethodInfo method);
    public object Target { get; }
    public MethodInfo Method { get; }
    public LuaValue InvokeMethod(LuaValue[] args);
}
```

**描述：** C# 函数包装类，用于注册到 Lua 中去。调用时用 `LuaValueUtils` 做参数/返回值转换，再 `Method.Invoke`。

#### LuaTableExtension

```csharp
public static class LuaTableExtension
```

##### RegisterMethodFunction

```csharp
public static LuaFunction RegisterMethodFunction(
    this LuaTable luaTable, string funcName, object target, MethodInfo methodInfo)
```

**描述：** 通过 `MethodInfo` 注册 C# 方法。Lua 脚本中通过 `funcName` 调用。

##### Reset

```csharp
public static void Reset(this LuaTable luaTable)
```

**描述：** 清空数组部分、哈希部分，并将 `MetaTable` 置为 `null`。

---

### 解析器组件

解析器把源码变成语法树；执行器（`Chunk` / `Statement` / `Expr`）再在环境表上求值。嵌入方通常只需 `LuaInterpreter.Parse` 或 `Interpreter`；下列类型在需要“先解析、后多次执行”时有用。不要依赖逐个语法节点作为稳定宿主 API。

#### Parser

```csharp
public partial class Parser
{
    public Parser();
    public int Position { get; set; }
    public List<Tuple<int, string>> Errors;
    public void SetInput(ParserInput<char> input);
    public Chunk ParseChunk(ParserInput<char> input, out bool success);
    public string GetEorrorMessages();   // 源码拼写
}
```

**描述：** Lua 语法解析器。`ParseChunk` 在输入未消耗完时将 `success` 设为 `false`。错误信息由 `GetEorrorMessages()` 格式化为带行列的文本。

`LuaInterpreter.Parse` 内部使用共享的静态 `Parser` 实例。

#### TextInput / ParserInput&lt;T&gt;

```csharp
public interface ParserInput<T> { /* Length, HasInput, GetInputSymbol, GetSubSection, FormErrorMessage */ }

public class TextInput : ParserInput<char>
{
    public TextInput(string text);
    public void GetLineColumnNumber(int pos, out int line, out int col);
    public string GetSubString(int start, int length);
}
```

**描述：** 字符流输入。`LuaInterpreter.Parse` 使用 `new TextInput(luaCode)`。

#### Chunk

```csharp
public partial class Chunk
{
    public List<Statement> Statements;
    public LuaTable Enviroment;
    public LuaValue Execute();
    public LuaValue Execute(out bool isBreak);
    public LuaValue Execute(LuaTable enviroment, out bool isBreak);
}
```

**描述：** 可执行代码块。`Execute()` 在当前 `Enviroment` 上运行语句列表；遇到 `return` 返回值，遇到 `break` 设置 `isBreak`。带环境参数的重载会用 `new LuaTable(enviroment)` 建立子作用域。

#### Statement / Expr

```csharp
public abstract partial class Statement
{
    public abstract LuaValue Execute(LuaTable enviroment, out bool isBreak);
}

public abstract partial class Expr
{
    public abstract LuaValue Evaluate(LuaTable enviroment);
    public abstract Term Simplify();
}
```

**描述：** 语句与表达式基类。派生节点（`IfStmt`、`WhileStmt`、`ForStmt`、`Function`、`ReturnStmt`、`Assignment`、字面量、函数调用、表构造等）由解析器生成，供执行器用。嵌入方一般不直接构造这些节点。

---

## 使用示例

### 基本脚本执行

```csharp
using JLGames.Infra.Languages.Lua;

string script = @"
print('Hello, Lua!')
local x = 10
local y = 20
print('Sum: ' .. (x + y))
";

LuaValue result = LuaInterpreter.Interpreter(script);
```

### 变量和函数

```csharp
string script = @"
function factorial(n)
    if n <= 1 then
        return 1
    else
        return n * factorial(n - 1)
    end
end

local num = 5
return factorial(num)
";

LuaValue result = LuaInterpreter.Interpreter(script);
LuaNumber number = result as LuaNumber;
// number.Number == 120
```

### 表操作

```csharp
string script = @"
local t = {1, 2, 3, 4, 5}
table.insert(t, 6)
table.remove(t, 1)

for i, v in ipairs(t) do
    print('Index ' .. i .. ': ' .. v)
end

return table.concat(t, ',')
";

LuaValue result = LuaInterpreter.Interpreter(script);
```

### 文件执行

```csharp
LuaValue result = LuaInterpreter.RunFile("script.lua");
```

### 自定义环境与 C# 函数注册

```csharp
using System;
using JLGames.Infra.Languages.Lua;

LuaTable env = LuaInterpreter.CreateGlobalEnviroment();

env.Register("csharp_function", (LuaValue[] args) =>
{
    Console.WriteLine("Called from Lua!");
    return new LuaString("Hello from C#");
});

string script = @"
local result = csharp_function()
print(result)
return result
";

LuaValue result = LuaInterpreter.Interpreter(script, env);
```

### 通过 MethodInfo 注册 C# 方法

```csharp
using System;
using JLGames.Infra.Languages.Lua;

public class Host
{
    public string Greet(string name)
    {
        return "Hello, " + name;
    }
}

var host = new Host();
LuaTable env = LuaInterpreter.CreateGlobalEnviroment();
env.RegisterMethodFunction(
    "greet",
    host,
    LuaValueUtils.GetMethodInfo<Host, string>(h => h.Greet(null)));

LuaValue result = LuaInterpreter.Interpreter(@"return greet('Lua')", env);
// ((LuaString)result).Text == "Hello, Lua"
```

### 先解析后执行

```csharp
Chunk chunk = LuaInterpreter.Parse("return 1 + 2");
chunk.Enviroment = LuaInterpreter.CreateGlobalEnviroment();
LuaValue result = chunk.Execute();
```

### 错误处理

```csharp
try
{
    string invalidScript = @"
    local x = 10
    print(x + )  -- 语法错误
    ";
    LuaValue result = LuaInterpreter.Interpreter(invalidScript);
}
catch (ArgumentException ex)
{
    // Parse 失败：语法错误（消息来自 Parser.GetEorrorMessages）
    Console.WriteLine(ex.Message);
}

try
{
    LuaInterpreter.Interpreter("error('boom')");
}
catch (LuaError ex)
{
    Console.WriteLine("Lua 运行错误: " + ex.Message);
}
```

脚本内也可用 `pcall` 捕获运行期异常（包括 `LuaError` 与其它 `Exception`）。

---

## 注意事项

1. **入口方法：** 使用静态方法 `Interpreter` / `RunFile` / `Parse`，不要调用不存在的 `Execute` / `ExecuteFile`。
2. **语法：** 接近 Lua 5.1；字符串库无 `find`/`gsub`，`string.format` 走 .NET 格式化。
3. **返回值：** 顶层无 `return` 时 `Interpreter`/`RunFile`/`Chunk.Execute` 返回 `null`（C# `null`，不是 `LuaNil`）。
4. **错误类型：** 语法错误为 `ArgumentException`；`error`/`assert` 等抛 `LuaError`。没有 `LuaException`。
5. **环境拼写：** 参数与字段名为 `enviroment` / `Enviroment`。
6. **扩展性：** 用 `LuaTable.Register` 注册 `LuaFunc`，或用 `RegisterMethodFunction` 绑定 CLR 方法。
7. **WinForm：** `WinFormLib` 已禁用。
8. **性能：** 解释执行，适合脚本逻辑而非热点计算。

---

## 依赖关系

- `System`：基础类型、异常、`Math`、`Environment`
- `System.Collections.Generic`：泛型集合
- `System.IO`：文件读写（`RunFile`、`io`/`file`/`os`）
- `System.Text`：字符串构建
- `System.Reflection` / `System.Linq.Expressions`：`LuaInterpreterExtra` 方法绑定
