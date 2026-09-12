# Archive API 文档

## 命名空间: JLGames.Infra.Archive

### 接口 (Interfaces)

#### IUnarchive
归档解压契约；支持参数配置、单文件/批量解压及进度事件。继承 `IEventDispatcher`。

```csharp
/// <summary>
/// 归档解压契约；支持参数配置、单文件/批量解压及进度事件。
/// </summary>
public interface IUnarchive : IEventDispatcher
{
    /// <summary>
    /// 设置参数
    /// </summary>
    /// <param name="params">解压参数</param>
    void SetUnarchiveParams(UnarchiveParams @params);

    /// <summary>
    /// 设置解压存储目录路径
    /// </summary>
    /// <param name="dstPath">目标路径</param>
    void SetDstPath(string dstPath);

    /// <summary>
    /// 解压单个归档文件
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    void UnarchiveFile(string srcFilePath);

    /// <summary>
    /// 解压单个归档文件
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    /// <param name="dstDir">目标目录</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    void UnarchiveFile(string srcFilePath, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// 解压单个归档文件
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    /// <param name="params">解压参数</param>
    void UnarchiveFile(string srcFilePath, UnarchiveParams @params);

    /// <summary>
    /// 解压多个归档文件
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    void UnarchiveFiles(IEnumerable<string> srcFilePaths);

    /// <summary>
    /// 解压多个归档文件
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    /// <param name="dstDir">目标目录</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    void UnarchiveFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// 解压多个归档文件
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    /// <param name="params">解压参数</param>
    void UnarchiveFiles(IEnumerable<string> srcFilePaths, UnarchiveParams @params);
}
```

### 类 (Classes)

#### UnarchiveParams
归档解压参数（目标目录、是否覆盖、条目名编码）。

```csharp
/// <summary>
/// 归档解压参数（目标目录、是否覆盖、条目名编码）。
/// </summary>
public class UnarchiveParams
{
    private string m_DstPath;
    private readonly bool m_Override;
    private readonly Encoding m_Encoding;

    /// <summary>
    /// 解压输出目录。
    /// </summary>
    public string DstPath { get; }

    /// <summary>
    /// 是否覆盖目标路径下已存在的文件。
    /// </summary>
    public bool Override { get; }

    /// <summary>
    /// 读取归档内条目名称时使用的编码。
    /// </summary>
    public Encoding Encoding { get; }

    /// <summary>
    /// 使用默认覆盖策略（true）与 UTF-8 编码创建参数。
    /// </summary>
    /// <param name="dstPath">目标路径</param>
    public UnarchiveParams(string dstPath);

    /// <summary>
    /// 使用指定的目标目录、覆盖策略与编码创建参数。
    /// </summary>
    /// <param name="dstPath">目标路径</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    public UnarchiveParams(string dstPath, bool @override, Encoding encoding);

    /// <summary>
    /// 更新解压输出目录路径。
    /// </summary>
    /// <param name="path">新路径</param>
    public void SetDstPath(string path);
}
```

#### Unzip
ZIP 归档解压实现；按条目与整包分发 `UnarchiveEvents` 事件。继承 `EventDispatcher`，实现 `IUnarchive`。

```csharp
/// <summary>
/// ZIP 归档解压实现；按条目与整包分发 <see cref="UnarchiveEvents"/> 事件。
/// </summary>
public class Unzip : EventDispatcher, IUnarchive
{
    /// <summary>
    /// 设置参数
    /// </summary>
    /// <param name="params">解压参数</param>
    public void SetUnarchiveParams(UnarchiveParams @params);

    /// <summary>
    /// 设置解压存储目录路径
    /// </summary>
    /// <param name="dstPath">目标路径</param>
    public void SetDstPath(string dstPath);

    /// <summary>
    /// 使用已设置的参数解压单个归档文件。
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    public void UnarchiveFile(string srcFilePath);

    /// <summary>
    /// 解压单个归档文件
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    /// <param name="dstDir">目标目录</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    public void UnarchiveFile(string srcFilePath, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// 解压单个归档文件
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    /// <param name="params">解压参数</param>
    public void UnarchiveFile(string srcFilePath, UnarchiveParams @params);

    /// <summary>
    /// 使用已设置的参数解压多个归档文件。
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    public void UnarchiveFiles(IEnumerable<string> srcFilePaths);

    /// <summary>
    /// 解压多个归档文件
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    /// <param name="dstDir">目标目录</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    public void UnarchiveFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// 解压多个归档文件
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    /// <param name="params">解压参数</param>
    public void UnarchiveFiles(IEnumerable<string> srcFilePaths, UnarchiveParams @params);
}
```

`SetDstPath`：若尚未设置参数，会以该路径创建 `UnarchiveParams`（默认覆盖、UTF-8）；若已有参数，则只更新目标目录。

`UnarchiveFile`：源文件不存在时直接返回；目标目录不存在时会创建。条目已存在且 `Override` 为 `false` 时跳过并记入忽略列表。每解压一个条目分发 `EventUnarchiveEntry`，整包处理完成后分发 `EventUnarchive`。

#### ArchiveUtil
无需订阅事件的 ZIP 归档解压静态工具。内部创建 `Unzip` 并设置参数后执行解压。

```csharp
/// <summary>
/// 无需订阅事件的 ZIP 归档解压静态工具。
/// </summary>
public static class ArchiveUtil
{
    /// <summary>
    /// 解压单个归档
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    /// <param name="dstDir">目标目录</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    public static void UnzipFile(string srcFilePath, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// 解压单个归档
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    /// <param name="dstDir">目标目录</param>
    public static void UnzipFile(string srcFilePath, string dstDir);

    /// <summary>
    /// 解压多个归档
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    /// <param name="dstDir">目标目录</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    public static void UnzipFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// 解压多个归档
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    /// <param name="dstDir">目标目录</param>
    public static void UnzipFiles(IEnumerable<string> srcFilePaths, string dstDir);
}
```

两参数重载等价于 `@override = true`、`encoding = Encoding.UTF8`。

### 事件数据类 (Event Data Classes)

#### UnarchiveEventData
单个归档文件全部处理完成时的事件数据。

```csharp
/// <summary>
/// 单个归档文件全部处理完成时的事件数据。
/// </summary>
public sealed class UnarchiveEventData
{
    private readonly string m_ArchiveFilePath;
    private readonly string[] m_Files;
    private readonly string[] m_IgnoreFiles;

    /// <summary>
    /// 归档文件路径
    /// </summary>
    public string ArchiveFilePath { get; }

    /// <summary>
    /// 解除归档生成的文件对应的HeaderName
    /// </summary>
    public string[] Files { get; }

    /// <summary>
    /// 解除归档忽略的文件对应的HeaderName
    /// </summary>
    public string[] IgnoreFiles { get; }

    /// <summary>
    /// 为单个归档创建解压完成事件数据。
    /// </summary>
    /// <param name="archiveFilePath">归档文件路径</param>
    /// <param name="files">解压的文件列表</param>
    /// <param name="ignoreFiles">忽略的文件列表</param>
    public UnarchiveEventData(string archiveFilePath, string[] files, string[] ignoreFiles);

    /// <summary>
    /// 返回包含路径与条目数量的简要诊断字符串。
    /// </summary>
    public override string ToString();
}
```

#### UnarchiveEntryEventData
归档内单个条目解压（或跳过）时的事件数据。当前实现仅在成功解压条目时分发该事件。

```csharp
/// <summary>
/// 归档内单个条目解压（或跳过）时的事件数据。
/// </summary>
public sealed class UnarchiveEntryEventData
{
    private readonly string m_ArchiveFilePath;
    private readonly string m_EntryHeaderName;

    /// <summary>
    /// 归档文件路径
    /// </summary>
    public string ArchiveFilePath { get; }

    /// <summary>
    /// 解除归档生成的文件对应的HeaderName
    /// </summary>
    public string EntryHeaderName { get; }

    /// <summary>
    /// 创建单条目解压事件数据。
    /// </summary>
    /// <param name="archiveFilePath">归档文件路径</param>
    /// <param name="entryHeaderName">条目头名称</param>
    public UnarchiveEntryEventData(string archiveFilePath, string entryHeaderName);

    /// <summary>
    /// 返回包含归档路径与条目名的简要诊断字符串。
    /// </summary>
    public override string ToString();
}
```

### 静态类 (Static Classes)

#### UnarchiveEvents
归档解压过程中分发的事件名常量。

```csharp
/// <summary>
/// 归档解压过程中分发的事件名常量。
/// </summary>
public static class UnarchiveEvents
{
    /// <summary>
    /// 归档解压完成单个文件完成事件
    /// </summary>
    public const string EventUnarchiveEntry = "UnarchiveEvent.EventUnarchiveEntry";

    /// <summary>
    /// 归档解压完成事件
    /// </summary>
    public const string EventUnarchive = "UnarchiveEvent.EventUnarchive";
}
```

### 功能说明

#### 解压功能特性

**基本功能**
- **单文件解压**：支持解压单个 ZIP 归档文件
- **批量解压**：支持依次解压多个归档文件
- **参数配置**：通过 `UnarchiveParams` 或方法参数指定目标目录、覆盖策略与编码
- **事件通知**：`Unzip` 按条目与整包分发解压事件（`ArchiveUtil` 不订阅事件）

**高级功能**
1. **编码支持**：`Encoding` 用于读取归档内条目名称
2. **覆盖控制**：`Override` 为 `false` 时跳过已存在文件，并记入 `IgnoreFiles`
3. **路径管理**：`SetDstPath` 可在已有参数上更新目标目录，或在未设置参数时创建默认参数
4. **静默跳过**：源文件不存在时 `UnarchiveFile` 直接返回，不抛出异常

#### 事件驱动架构

**事件类型**
- **EventUnarchiveEntry**：单个条目解压完成事件
- **EventUnarchive**：整个归档处理完成事件

**事件数据**
- **UnarchiveEventData**：包含已解压条目列表与忽略条目列表
- **UnarchiveEntryEventData**：包含单个条目的归档路径与条目名

#### 参数管理

**UnarchiveParams**
- **DstPath**：解压输出目录
- **Override**：是否覆盖目标路径下已存在的文件（单参数构造时默认为 `true`）
- **Encoding**：读取归档内条目名称时使用的编码（单参数构造时默认为 `Encoding.UTF8`）

### 使用示例

#### 基本解压操作
```csharp
// 创建解压器
var unarchive = new Unzip();

// 设置事件监听
unarchive.AddEventListener(UnarchiveEvents.EventUnarchive, (evd) => {
    var eventData = evd.Data as UnarchiveEventData;
    Console.WriteLine($"解压完成: {eventData.ArchiveFilePath}");
    Console.WriteLine($"解压文件数: {eventData.Files.Length}");
    Console.WriteLine($"忽略文件数: {eventData.IgnoreFiles.Length}");
});

unarchive.AddEventListener(UnarchiveEvents.EventUnarchiveEntry, (evd) => {
    var eventData = evd.Data as UnarchiveEntryEventData;
    Console.WriteLine($"文件解压完成: {eventData.EntryHeaderName}");
});

// 解压单个文件
unarchive.UnarchiveFile("archive.zip", "extract_folder", true, Encoding.UTF8);
```

#### 使用参数配置
```csharp
// 创建解压参数（覆盖 + UTF-8）
var unarchiveParams = new UnarchiveParams("output_folder", true, Encoding.UTF8);

// 设置解压器参数
var unarchive = new Unzip();
unarchive.SetUnarchiveParams(unarchiveParams);

// 解压文件
unarchive.UnarchiveFile("archive.zip");
```

#### 批量解压
```csharp
var unarchive = new Unzip();

// 准备要解压的文件列表
var fileList = new List<string>
{
    "archive1.zip",
    "archive2.zip",
    "archive3.zip"
};

// 批量解压
unarchive.UnarchiveFiles(fileList, "batch_output", true, Encoding.UTF8);
```

#### 使用工具类
```csharp
// 解压单个 ZIP（默认覆盖、UTF-8）
ArchiveUtil.UnzipFile("archive.zip", "extract_folder");

// 解压单个 ZIP（指定覆盖与编码）
ArchiveUtil.UnzipFile("archive.zip", "extract_folder", true, Encoding.UTF8);

// 批量解压（默认覆盖、UTF-8）
var fileList = new List<string>
{
    "archive1.zip",
    "archive2.zip"
};
ArchiveUtil.UnzipFiles(fileList, "batch_output");

// 批量解压（指定覆盖与编码）
ArchiveUtil.UnzipFiles(fileList, "batch_output", false, Encoding.GetEncoding("GBK"));
```

#### 高级解压配置
```csharp
// 创建自定义解压参数
var customParams = new UnarchiveParams("custom_output", false, Encoding.GetEncoding("GBK"));

// 创建解压器并设置参数
var unarchive = new Unzip();
unarchive.SetUnarchiveParams(customParams);
unarchive.SetDstPath("alternative_output");

// 监听解压进度
unarchive.AddEventListener(UnarchiveEvents.EventUnarchiveEntry, (evd) => {
    var eventData = evd.Data as UnarchiveEntryEventData;
    Console.WriteLine($"正在解压: {eventData.EntryHeaderName}");
});

// 执行解压
unarchive.UnarchiveFile("large_archive.zip", customParams);
```

#### 错误处理
```csharp
var unarchive = new Unzip();

try
{
    // 设置事件监听
    unarchive.AddEventListener(UnarchiveEvents.EventUnarchive, (evd) => {
        var eventData = evd.Data as UnarchiveEventData;
        if (eventData.IgnoreFiles.Length > 0)
        {
            Console.WriteLine("警告：以下文件被忽略:");
            foreach (var ignoredFile in eventData.IgnoreFiles)
            {
                Console.WriteLine($"  {ignoredFile}");
            }
        }
    });

    // 执行解压
    unarchive.UnarchiveFile("corrupted_archive.zip", "output", true, Encoding.UTF8);
}
catch (Exception ex)
{
    Console.WriteLine($"解压失败: {ex.Message}");
}
```

### 设计特点

1. **事件驱动**：`Unzip` 基于 `EventDispatcher` 分发条目与整包完成事件
2. **参数化配置**：通过 `UnarchiveParams` 设置目标目录、覆盖策略与条目名编码
3. **批量处理**：支持按路径集合依次解压
4. **编码支持**：`Encoding` 用于读取 ZIP 条目名称
5. **覆盖与忽略**：不覆盖时将已存在条目记入 `IgnoreFiles`
6. **工具类支持**：`ArchiveUtil` 提供无需订阅事件的静态解压方法

### 注意事项

1. **文件权限**：确保有足够的文件读写权限
2. **磁盘空间**：解压前检查目标磁盘空间
3. **编码问题**：`Encoding` 作用于 ZIP 条目名称，而非文件内容
4. **覆盖风险**：使用覆盖模式时注意数据安全
5. **源文件缺失**：源路径不存在时静默返回，不会抛出异常
6. **事件监听**：`ArchiveUtil` 不对外暴露事件；使用 `Unzip` 时及时清理不需要的事件监听器
