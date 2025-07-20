# JLGames.Infra.Languages API 文档

## 概述

Languages模块提供了Lua脚本语言的解释器功能，包括Lua语法解析、执行引擎、标准库支持等。

## 命名空间

`JLGames.Infra.Languages.Lua`

---

## 主要组件

### LuaInterpreter

Lua解释器主类，提供Lua脚本的执行功能。

```csharp
public class LuaInterpreter
```

#### 主要功能

- **脚本解析：** 解析Lua脚本语法
- **代码执行：** 执行解析后的Lua代码
- **变量管理：** 管理Lua变量和作用域
- **函数调用：** 支持Lua函数定义和调用
- **标准库：** 提供Lua标准库支持

#### 主要方法

##### Execute(string script)

```csharp
public LuaValue Execute(string script)
```

**描述：** 执行Lua脚本

**参数：**
- `script` (string): Lua脚本内容

**返回值：**
- `LuaValue`: 执行结果

##### ExecuteFile(string filePath)

```csharp
public LuaValue ExecuteFile(string filePath)
```

**描述：** 执行Lua文件

**参数：**
- `filePath` (string): Lua文件路径

**返回值：**
- `LuaValue`: 执行结果

---

### LuaValue

Lua值类型基类，表示Lua中的各种数据类型。

```csharp
public abstract class LuaValue
```

#### 派生类型

##### LuaNumber

```csharp
public class LuaNumber : LuaValue
```

**描述：** 表示Lua中的数字类型

##### LuaString

```csharp
public class LuaString : LuaValue
```

**描述：** 表示Lua中的字符串类型

##### LuaBoolean

```csharp
public class LuaBoolean : LuaValue
```

**描述：** 表示Lua中的布尔类型

##### LuaTable

```csharp
public class LuaTable : LuaValue
```

**描述：** 表示Lua中的表类型

##### LuaFunction

```csharp
public class LuaFunction : LuaValue
```

**描述：** 表示Lua中的函数类型

##### LuaNil

```csharp
public class LuaNil : LuaValue
```

**描述：** 表示Lua中的nil类型

---

### 标准库

#### BaseLib

基础库，提供Lua基础函数。

```csharp
public class BaseLib
```

**主要函数：**
- `print()`: 打印输出
- `type()`: 获取类型
- `tonumber()`: 转换为数字
- `tostring()`: 转换为字符串

#### MathLib

数学库，提供数学运算函数。

```csharp
public class MathLib
```

**主要函数：**
- `abs()`: 绝对值
- `floor()`: 向下取整
- `ceil()`: 向上取整
- `random()`: 随机数
- `sin()`, `cos()`, `tan()`: 三角函数

#### StringLib

字符串库，提供字符串处理函数。

```csharp
public class StringLib
```

**主要函数：**
- `len()`: 字符串长度
- `sub()`: 子字符串
- `find()`: 查找字符串
- `gsub()`: 全局替换
- `format()`: 格式化字符串

#### TableLib

表库，提供表操作函数。

```csharp
public class TableLib
```

**主要函数：**
- `insert()`: 插入元素
- `remove()`: 删除元素
- `sort()`: 排序
- `concat()`: 连接表元素

#### IOLib

输入输出库，提供文件操作函数。

```csharp
public class IOLib
```

**主要函数：**
- `open()`: 打开文件
- `read()`: 读取文件
- `write()`: 写入文件
- `close()`: 关闭文件

#### OSLib

操作系统库，提供系统相关函数。

```csharp
public class OSLib
```

**主要函数：**
- `time()`: 获取时间
- `date()`: 格式化日期
- `clock()`: 获取时钟时间

---

### 解析器组件

#### Parser

Lua语法解析器。

```csharp
public class Parser
```

**功能：**
- 词法分析
- 语法分析
- 抽象语法树生成

#### Statement

语句基类，表示Lua中的各种语句。

```csharp
public abstract class Statement
```

**派生类型：**
- `Assignment`: 赋值语句
- `IfStmt`: 条件语句
- `WhileStmt`: 循环语句
- `ForStmt`: for循环语句
- `Function`: 函数定义
- `ReturnStmt`: 返回语句

#### Expression

表达式基类，表示Lua中的各种表达式。

```csharp
public abstract class Expression
```

**派生类型：**
- `NumberLiteral`: 数字字面量
- `StringLiteral`: 字符串字面量
- `BoolLiteral`: 布尔字面量
- `FunctionCall`: 函数调用
- `TableConstructor`: 表构造器
- `BinaryOp`: 二元运算

---

## 使用示例

### 基本脚本执行

```csharp
// 创建Lua解释器
var interpreter = new LuaInterpreter();

// 执行简单脚本
string script = @"
print('Hello, Lua!')
local x = 10
local y = 20
print('Sum: ' .. (x + y))
";

var result = interpreter.Execute(script);
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
local result = factorial(num)
print('Factorial of ' .. num .. ' is ' .. result)
";

var result = interpreter.Execute(script);
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
";

var result = interpreter.Execute(script);
```

### 文件执行

```csharp
// 执行Lua文件
var interpreter = new LuaInterpreter();
var result = interpreter.ExecuteFile("script.lua");
```

### 自定义函数注册

```csharp
var interpreter = new LuaInterpreter();

// 注册C#函数到Lua
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

### 错误处理

```csharp
try
{
    string invalidScript = @"
    local x = 10
    print(x + )  -- 语法错误
    ";
    
    var result = interpreter.Execute(invalidScript);
}
catch (LuaException ex)
{
    Console.WriteLine($"Lua错误: {ex.Message}");
    Console.WriteLine($"行号: {ex.LineNumber}");
}
```

---

## 注意事项

1. **语法支持：** 支持Lua 5.1语法规范
2. **性能考虑：** 解释器执行速度相对较慢，适合脚本逻辑
3. **内存管理：** 大量脚本执行时注意内存使用
4. **错误处理：** 脚本错误会抛出LuaException异常
5. **标准库：** 提供常用的Lua标准库函数
6. **扩展性：** 支持注册自定义C#函数到Lua环境

---

## 依赖关系

- `System`: 基础类型和集合
- `System.Collections.Generic`: 泛型集合
- `System.Text`: 字符串处理 