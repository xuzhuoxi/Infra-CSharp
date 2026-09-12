# JLGames.Infra.Languages API Documentation

## Overview

The Languages module provides an embedded Lua interpreter: parse Lua source, execute it against a `LuaTable` environment, and register a set of standard libraries. The implementation is based on the third-party Lua Interpreter library (MIT) and follows Lua 5.1-style syntax.

All entry points on `LuaInterpreter` are static. There are no instance methods named `Execute` or `ExecuteFile`.

## Namespace

- `JLGames.Infra.Languages.Lua` — interpreter, value types, parser, and extras
- `JLGames.Infra.Languages.Lua.Library` — standard-library registration types

Some identifiers keep the original library spelling, such as `Enviroment` and `GetEorrorMessages`. Call sites must use those exact names.

---

## Main Components

### LuaInterpreter

Main interpreter entry point. Parses scripts, builds the default global environment, and executes code.

```csharp
public class LuaInterpreter
```

#### Main Features

- **Script parsing:** Parse Lua source into an executable `Chunk`
- **Code execution:** Run against the default or a custom `LuaTable` environment
- **File execution:** Read a file and run it
- **Standard library:** `CreateGlobalEnviroment` registers the built-in modules
- **Host extension:** Inject C# functions via `Register` / `RegisterMethodFunction` on the environment table

#### Main Methods

##### Interpreter(string luaCode)

```csharp
public static LuaValue Interpreter(string luaCode)
```

**Description:** Execute Lua source with the default global environment. Internally calls `CreateGlobalEnviroment()` and the overload below.

**Parameters:**
- `luaCode` (string): Lua source text

**Return Value:**
- `LuaValue`: Result of a top-level `return`, or `null` if the chunk does not return

##### Interpreter(string luaCode, LuaTable enviroment)

```csharp
public static LuaValue Interpreter(string luaCode, LuaTable enviroment)
```

**Description:** Parse and execute Lua source in the given environment table (the script global scope).

**Parameters:**
- `luaCode` (string): Lua source text
- `enviroment` (LuaTable): Global environment (spelled `enviroment` in source)

**Return Value:**
- `LuaValue`: Result of a top-level `return`, or `null` if the chunk does not return

##### RunFile(string luaFile)

```csharp
public static LuaValue RunFile(string luaFile)
```

**Description:** Read the entire file and execute it with the default global environment.

**Parameters:**
- `luaFile` (string): Path to a Lua file

**Return Value:**
- `LuaValue`: Result of a top-level `return`, or `null` if the chunk does not return

##### RunFile(string luaFile, LuaTable enviroment)

```csharp
public static LuaValue RunFile(string luaFile, LuaTable enviroment)
```

**Description:** Read the entire file and execute it in the given environment.

**Parameters:**
- `luaFile` (string): Path to a Lua file
- `enviroment` (LuaTable): Global environment

**Return Value:**
- `LuaValue`: Result of a top-level `return`, or `null` if the chunk does not return

##### Parse(string luaCode)

```csharp
public static Chunk Parse(string luaCode)
```

**Description:** Parse Lua source into a `Chunk` without executing it. On failure throws `ArgumentException` whose message includes `Parser.GetEorrorMessages()`.

**Parameters:**
- `luaCode` (string): Lua source text

**Return Value:**
- `Chunk`: Executable chunk (set `Enviroment` yourself, then call `Execute()`)

**Exceptions:**
- `ArgumentException`: Syntax error

##### CreateGlobalEnviroment()

```csharp
public static LuaTable CreateGlobalEnviroment()
```

**Description:** Create the default global environment, register standard libraries, and set `_G` to the table itself.

**Registered content:**
- `BaseLib.RegisterFunctions` — global base functions
- `StringLib` → `string`
- `TableLib` → `table`
- `IOLib` → `io`
- `FileLib` → `file`
- `MathLib` → `math`
- `OSLib` → `os`
- `_G` — the environment table itself

**Return Value:**
- `LuaTable`: Environment suitable for `Interpreter` / `RunFile`

`WinFormLib` in source is fully commented out, is not compiled, and is not registered by the default environment.

---

### LuaValue

Base type for Lua values. Implements `IEquatable<LuaValue>`.

```csharp
public abstract class LuaValue : IEquatable<LuaValue>
```

#### Members

```csharp
public abstract object Value { get; }
public abstract string GetTypeCode();
public virtual bool GetBooleanValue();          // default true; LuaNil / LuaBoolean.False are false
public bool Equals(LuaValue other);
public static LuaValue GetKeyValue(LuaValue baseValue, LuaValue key);
```

`GetTypeCode()` returns Lua type names such as `"number"`, `"string"`, `"boolean"`, `"nil"`, `"table"`, `"function"`, `"userdata"`.

`GetKeyValue` looks up a key on a table. For `LuaUserdata` with an `__index` metatable, it uses that metamethod. Accessing a non-table throws `Exception`.

#### Derived Types

##### LuaNumber

```csharp
public class LuaNumber : LuaValue
{
    public LuaNumber(double number);
    public double Number { get; set; }
}
```

**Description:** Lua number (stored as `double`). `GetTypeCode()` is `"number"`.

##### LuaString

```csharp
public class LuaString : LuaValue
{
    public LuaString(string text);
    public static readonly LuaString Empty;
    public string Text { get; set; }
}
```

**Description:** Lua string. `GetTypeCode()` is `"string"`.

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

**Description:** Lua boolean. The constructor is private; use `True` / `False` or `From(bool)`. `GetTypeCode()` is `"boolean"`.

##### LuaNil

```csharp
public class LuaNil : LuaValue
{
    public static readonly LuaNil Nil;
}
```

**Description:** Lua `nil` singleton. `Value` is `null`, `GetBooleanValue()` is `false`, `GetTypeCode()` is `"nil"`.

##### LuaTable

```csharp
public class LuaTable : LuaValue
{
    public LuaTable();
    public LuaTable(LuaTable parent);   // parent as __index / __newindex
    public LuaTable MetaTable { get; set; }
    public int Length { get; }          // array-part length
    public int Count { get; }           // hash-part entry count
    public IEnumerable<LuaValue> ListValues { get; }
    public IEnumerable<LuaValue> Keys { get; }
    public IEnumerable<KeyValuePair<LuaValue, LuaValue>> KeyValuePairs { get; }
}
```

**Description:** Lua table. Array indices are **1-based**. `GetTypeCode()` is `"table"`. The `parent` constructor is used for nested scopes.

**Common methods:**

| Method | Description |
| --- | --- |
| `GetValue(int index)` | 1-based array lookup; out of range returns `LuaNil.Nil` |
| `GetValue(string name)` | Named lookup; may use `__index` |
| `GetValue(LuaValue key)` | Key lookup; may use `__index` |
| `SetNameValue(string name, LuaValue value)` | Named write; `LuaNil.Nil` removes the key |
| `SetKeyValue(LuaValue key, LuaValue value)` | Key write (integer keys go to the array part) |
| `RawGetValue(LuaValue key)` / `RawSetValue(string name, LuaValue value)` | Bypass metatable |
| `Register(string name, LuaFunc function)` | Register a C# delegate as a Lua function |
| `AddValue` / `InsertValue` / `Remove` / `RemoveAt` | Array-part mutation (`InsertValue`/`RemoveAt` are 1-based) |
| `Sort()` / `Sort(LuaFunction compare)` | Sort the array part |
| `ContainsKey(LuaValue key)` | Whether the key exists |
| `GetKey(string key)` | Find a matching `LuaString` key in the hash part |

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

**Description:** Lua function. `GetTypeCode()` is `"function"`. Hosts typically inject functions with `LuaTable.Register` or `LuaMethodInfo`.

##### LuaUserdata

```csharp
public class LuaUserdata : LuaValue
{
    public LuaUserdata(object obj);
    public LuaUserdata(object obj, LuaTable metatable);
    public LuaTable MetaTable { get; set; }
}
```

**Description:** Wraps an arbitrary CLR object. `Value` is the wrapped object. `GetTypeCode()` is `"userdata"`. File handles are exposed as userdata plus a metatable.

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

**Description:** Multiple-return container. `GetTypeCode()` throws `InvalidOperationException`. `WrapLuaValues`: empty → `LuaNil.Nil`, single value returned as-is, multiple values wrapped.

##### LuaError

```csharp
public class LuaError : Exception
{
    public LuaError(string message);
    public LuaError(string message, Exception innerException);
    public LuaError(string messageformat, params object[] args);
}
```

**Description:** Runtime Lua error (`error()` / `assert()`, and similar). This is **not** `LuaException` and has no `LineNumber` property. Syntax errors are `ArgumentException` from `Parse`.

---

### Standard Libraries

Libraries live in `JLGames.Infra.Languages.Lua.Library`. Embedders usually do not call library methods directly; `CreateGlobalEnviroment` registers them. For a custom environment, call the same `RegisterModule` / `RegisterFunctions` APIs.

#### BaseLib

```csharp
public class BaseLib
{
    public static void RegisterFunctions(LuaTable module);
}
```

**Description:** Registers base functions on the given table (the default environment registers them on the global table).

**Lua functions:** `print`, `type`, `getmetatable`, `setmetatable`, `tostring`, `tonumber`, `ipairs`, `pairs`, `next`, `assert`, `error`, `rawget`, `rawset`, `select`, `dofile`, `loadstring`, `unpack`, `pcall`

#### MathLib

```csharp
public static class MathLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**Description:** Registered as `math`. Constants: `huge` (`double.MaxValue`), `pi`.

**Lua functions:** `abs`, `acos`, `asin`, `atan`, `atan2`, `ceil`, `cos`, `cosh`, `deg`, `exp`, `floor`, `fmod`, `log`, `log10`, `max`, `min`, `modf`, `pow`, `rad`, `random`, `randomseed`, `sin`, `sinh`, `sqrt`, `tan`, `tanh`

#### StringLib

```csharp
public static class StringLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**Description:** Registered as `string`. `format` uses .NET `string.Format`, not Lua `printf` style. This implementation has **no** `string.find` / `string.gsub`.

**Lua functions:** `byte`, `char`, `format`, `len`, `sub`, `lower`, `upper`, `rep`, `reverse`

#### TableLib

```csharp
public static class TableLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**Description:** Registered as `table`. `removeitem` is an extension that removes by value.

**Lua functions:** `concat`, `insert`, `remove`, `removeitem`, `maxn`, `sort`

#### IOLib

```csharp
public static class IOLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**Description:** Registered as `io`. Opened files are `LuaUserdata` with a `FileLib` metatable.

**Lua functions:** `input`, `output`, `open`, `read`, `write`, `flush`, `tmpfile`

`open` modes: `"r"`/`"r+"` read, `"w"`/`"w+"` write, `"a"`/`"a+"` append.

#### FileLib

```csharp
public static class FileLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
    public static LuaTable CreateMetaTable();
}
```

**Description:** Registered as `file`. `CreateMetaTable` is used by `io.open` so file userdata expose methods (`__index` points at the metatable).

**Lua functions:** `close`, `read`, `write`, `lines`, `flush`, `seek`

`read` modes: `*l` one line, `*a` all, `*n` number, or a character count.

#### OSLib

```csharp
public static class OSLib
{
    public static void RegisterModule(LuaTable enviroment);
    public static void RegisterFunctions(LuaTable module);
}
```

**Description:** Registered as `os`.

**Lua functions:** `clock`, `date`, `time`, `execute`, `exit`, `getenv`, `remove`, `rename`, `tmpname`

#### WinFormLib

The `WinFormLib.cs` type is fully commented out. It is **not** part of the current public API and is not registered by `CreateGlobalEnviroment`.

---

### LuaInterpreterExtra

Host-side extensions in the same namespace `JLGames.Infra.Languages.Lua`.

#### LuaValueUtils

Converts between C# values and `LuaValue`, and extracts `MethodInfo` from lambdas.

```csharp
public static class LuaValueUtils
```

##### ObjectToLuaValue(object o)

```csharp
public static LuaValue ObjectToLuaValue(object o)
```

**Description:** Returns a `LuaValue` containing the object's value. `null` → `LuaNil.Nil`; `bool` / `string` / common numeric types map to the matching Lua type; an existing `LuaValue` is returned as-is; other types become `LuaString` via `ToString()`.

##### LuaValueToObject(LuaValue luaValue)

```csharp
public static object LuaValueToObject(LuaValue luaValue)
```

**Description:** Returns C# data from a `LuaValue`. `LuaNumber` is converted to `float`; other types return `Value`.

##### StringToInt / StringToFloat / StringToDouble

```csharp
public static int StringToInt(string s);
public static float StringToFloat(string s);
public static double StringToDouble(string s);
```

**Description:** Returns `0` on parse failure. `float`/`double` use invariant culture.

##### GetMethodInfo

```csharp
public static MethodInfo GetMethodInfo(LambdaExpression expression);
public static MethodInfo GetMethodInfo<T, TResult>(Expression<Func<T, TResult>> expression);
public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression);
public static MethodInfo GetMethodInfo(Expression<Action> expression);
```

**Description:** Returns the `MethodInfo` of a lambda that consists of a single method call. Throws `ArgumentException` if the expression is null or is not a method call.

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

**Description:** C# function wrapper class for registering in Lua. Arguments and the return value are converted with `LuaValueUtils`, then `Method.Invoke` is called.

#### LuaTableExtension

```csharp
public static class LuaTableExtension
```

##### RegisterMethodFunction

```csharp
public static LuaFunction RegisterMethodFunction(
    this LuaTable luaTable, string funcName, object target, MethodInfo methodInfo)
```

**Description:** Register the C# method through the `MethodInfo` object. Lua scripts call it by `funcName`.

##### Reset

```csharp
public static void Reset(this LuaTable luaTable)
```

**Description:** Clears the array part and hash part, then sets `MetaTable` to `null`.

---

### Parser Components

The parser builds a syntax tree; the executor (`Chunk` / `Statement` / `Expr`) evaluates it against an environment table. Embedders normally use `LuaInterpreter.Parse` or `Interpreter`. The types below are useful when you need to parse once and execute later. Do not treat every syntax node as a stable host API.

#### Parser

```csharp
public partial class Parser
{
    public Parser();
    public int Position { get; set; }
    public List<Tuple<int, string>> Errors;
    public void SetInput(ParserInput<char> input);
    public Chunk ParseChunk(ParserInput<char> input, out bool success);
    public string GetEorrorMessages();   // spelling as in source
}
```

**Description:** Lua syntax parser. `ParseChunk` sets `success` to `false` if leftover input remains. `GetEorrorMessages()` formats errors with line and column.

`LuaInterpreter.Parse` uses a shared static `Parser` instance.

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

**Description:** Character-stream input. `LuaInterpreter.Parse` uses `new TextInput(luaCode)`.

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

**Description:** Executable code block. `Execute()` runs the statement list on the current `Enviroment`; `return` yields a value, `break` sets `isBreak`. The overload that takes an environment builds a child scope with `new LuaTable(enviroment)`.

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

**Description:** Statement and expression bases. Derived nodes (`IfStmt`, `WhileStmt`, `ForStmt`, `Function`, `ReturnStmt`, `Assignment`, literals, calls, table constructors, and so on) are produced by the parser for the executor. Embedders generally should not construct these nodes by hand.

---

## Usage Examples

### Basic Script Execution

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

### Variables and Functions

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

### Table Operations

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

### File Execution

```csharp
LuaValue result = LuaInterpreter.RunFile("script.lua");
```

### Custom Environment and C# Function Registration

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

### Registering a C# Method via MethodInfo

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

### Parse Then Execute

```csharp
Chunk chunk = LuaInterpreter.Parse("return 1 + 2");
chunk.Enviroment = LuaInterpreter.CreateGlobalEnviroment();
LuaValue result = chunk.Execute();
```

### Error Handling

```csharp
try
{
    string invalidScript = @"
    local x = 10
    print(x + )  -- syntax error
    ";
    LuaValue result = LuaInterpreter.Interpreter(invalidScript);
}
catch (ArgumentException ex)
{
    // Parse failed: syntax error (message from Parser.GetEorrorMessages)
    Console.WriteLine(ex.Message);
}

try
{
    LuaInterpreter.Interpreter("error('boom')");
}
catch (LuaError ex)
{
    Console.WriteLine("Lua runtime error: " + ex.Message);
}
```

Scripts can also use `pcall` to catch runtime exceptions (including `LuaError` and other `Exception` types).

---

## Notes

1. **Entry points:** Use the static methods `Interpreter` / `RunFile` / `Parse`. Do not call non-existent `Execute` / `ExecuteFile`.
2. **Syntax:** Lua 5.1-style; the string library has no `find`/`gsub`, and `string.format` uses .NET formatting.
3. **Return value:** With no top-level `return`, `Interpreter`/`RunFile`/`Chunk.Execute` return C# `null` (not `LuaNil`).
4. **Error types:** Syntax errors are `ArgumentException`; `error`/`assert` throw `LuaError`. There is no `LuaException`.
5. **Spelling:** Parameters and fields are named `enviroment` / `Enviroment`.
6. **Extensibility:** Register a `LuaFunc` with `LuaTable.Register`, or bind a CLR method with `RegisterMethodFunction`.
7. **WinForm:** `WinFormLib` is disabled.
8. **Performance:** Interpreted execution is suited to script logic, not hot paths.

---

## Dependencies

- `System`: base types, exceptions, `Math`, `Environment`
- `System.Collections.Generic`: generic collections
- `System.IO`: file I/O (`RunFile`, `io`/`file`/`os`)
- `System.Text`: string building
- `System.Reflection` / `System.Linq.Expressions`: `LuaInterpreterExtra` method binding
