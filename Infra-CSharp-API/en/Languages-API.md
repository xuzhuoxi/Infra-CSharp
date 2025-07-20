# JLGames.Infra.Languages API Documentation

## Overview

The Languages module provides Lua scripting language interpreter functionality, including Lua syntax parsing, execution engine, standard library support, etc.

## Namespace

`JLGames.Infra.Languages.Lua`

---

## Main Components

### LuaInterpreter

Main Lua interpreter class that provides Lua script execution functionality.

```csharp
public class LuaInterpreter
```

#### Main Features

- **Script Parsing:** Parse Lua script syntax
- **Code Execution:** Execute parsed Lua code
- **Variable Management:** Manage Lua variables and scopes
- **Function Calls:** Support Lua function definition and calls
- **Standard Library:** Provide Lua standard library support

#### Main Methods

##### Execute(string script)

```csharp
public LuaValue Execute(string script)
```

**Description:** Execute Lua script

**Parameters:**
- `script` (string): Lua script content

**Return Value:**
- `LuaValue`: Execution result

##### ExecuteFile(string filePath)

```csharp
public LuaValue ExecuteFile(string filePath)
```

**Description:** Execute Lua file

**Parameters:**
- `filePath` (string): Lua file path

**Return Value:**
- `LuaValue`: Execution result

---

### LuaValue

Lua value type base class that represents various data types in Lua.

```csharp
public abstract class LuaValue
```

#### Derived Types

##### LuaNumber

```csharp
public class LuaNumber : LuaValue
```

**Description:** Represents number type in Lua

##### LuaString

```csharp
public class LuaString : LuaValue
```

**Description:** Represents string type in Lua

##### LuaBoolean

```csharp
public class LuaBoolean : LuaValue
```

**Description:** Represents boolean type in Lua

##### LuaTable

```csharp
public class LuaTable : LuaValue
```

**Description:** Represents table type in Lua

##### LuaFunction

```csharp
public class LuaFunction : LuaValue
```

**Description:** Represents function type in Lua

##### LuaNil

```csharp
public class LuaNil : LuaValue
```

**Description:** Represents nil type in Lua

---

### Standard Libraries

#### BaseLib

Base library that provides Lua basic functions.

```csharp
public class BaseLib
```

**Main functions:**
- `print()`: Print output
- `type()`: Get type
- `tonumber()`: Convert to number
- `tostring()`: Convert to string

#### MathLib

Math library that provides mathematical operation functions.

```csharp
public class MathLib
```

**Main functions:**
- `abs()`: Absolute value
- `floor()`: Floor function
- `ceil()`: Ceiling function
- `random()`: Random number
- `sin()`, `cos()`, `tan()`: Trigonometric functions

#### StringLib

String library that provides string processing functions.

```csharp
public class StringLib
```

**Main functions:**
- `len()`: String length
- `sub()`: Substring
- `find()`: Find string
- `gsub()`: Global substitution
- `format()`: Format string

#### TableLib

Table library that provides table operation functions.

```csharp
public class TableLib
```

**Main functions:**
- `insert()`: Insert element
- `remove()`: Remove element
- `sort()`: Sort
- `concat()`: Concatenate table elements

#### IOLib

Input/Output library that provides file operation functions.

```csharp
public class IOLib
```

**Main functions:**
- `open()`: Open file
- `read()`: Read file
- `write()`: Write file
- `close()`: Close file

#### OSLib

Operating system library that provides system-related functions.

```csharp
public class OSLib
```

**Main functions:**
- `time()`: Get time
- `date()`: Format date
- `clock()`: Get clock time

---

### Parser Components

#### Parser

Lua syntax parser.

```csharp
public class Parser
```

**Features:**
- Lexical analysis
- Syntax analysis
- Abstract syntax tree generation

#### Statement

Statement base class that represents various statements in Lua.

```csharp
public abstract class Statement
```

**Derived types:**
- `Assignment`: Assignment statement
- `IfStmt`: Conditional statement
- `WhileStmt`: Loop statement
- `ForStmt`: For loop statement
- `Function`: Function definition
- `ReturnStmt`: Return statement

#### Expression

Expression base class that represents various expressions in Lua.

```csharp
public abstract class Expression
```

**Derived types:**
- `NumberLiteral`: Number literal
- `StringLiteral`: String literal
- `BoolLiteral`: Boolean literal
- `FunctionCall`: Function call
- `TableConstructor`: Table constructor
- `BinaryOp`: Binary operation

---

## Usage Examples

### Basic Script Execution

```csharp
// Create Lua interpreter
var interpreter = new LuaInterpreter();

// Execute simple script
string script = @"
print('Hello, Lua!')
local x = 10
local y = 20
print('Sum: ' .. (x + y))
";

var result = interpreter.Execute(script);
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
local result = factorial(num)
print('Factorial of ' .. num .. ' is ' .. result)
";

var result = interpreter.Execute(script);
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
";

var result = interpreter.Execute(script);
```

### File Execution

```csharp
// Execute Lua file
var interpreter = new LuaInterpreter();
var result = interpreter.ExecuteFile("script.lua");
```

### Custom Function Registration

```csharp
var interpreter = new LuaInterpreter();

// Register C# function to Lua
interpreter.RegisterFunction("csharp_function", (LuaValue[] args) => {
    Console.WriteLine("Called from Lua!");
    return new LuaString("Hello from C#");
});

string script = @"
local result = csharp_function()
print(result)
";

var result = interpreter.Execute(script);
```

### Error Handling

```csharp
try
{
    string invalidScript = @"
    local x = 10
    print(x + )  -- Syntax error
    ";
    
    var result = interpreter.Execute(invalidScript);
}
catch (LuaException ex)
{
    Console.WriteLine($"Lua error: {ex.Message}");
    Console.WriteLine($"Line number: {ex.LineNumber}");
}
```

---

## Notes

1. **Syntax support:** Supports Lua 5.1 syntax specification
2. **Performance considerations:** Interpreter execution speed is relatively slow, suitable for script logic
3. **Memory management:** Pay attention to memory usage during large script execution
4. **Error handling:** Script errors will throw LuaException
5. **Standard library:** Provides commonly used Lua standard library functions
6. **Extensibility:** Supports registering custom C# functions to Lua environment

---

## Dependencies

- `System`: Basic types and collections
- `System.Collections.Generic`: Generic collections
- `System.Text`: String processing 