# Archive API 文档

## 命名空间: JLGames.Infra.Archive

### 接口 (Interfaces)

#### IUnarchive
解压接口

```csharp
/// <summary>
/// 解压接口
/// 提供归档文件的解压功能
/// </summary>
public interface IUnarchive : IEventDispatcher
{
    /// <summary>
    /// Set parameters
    /// 设置参数
    /// </summary>
    /// <param name="params">解压参数</param>
    void SetUnarchiveParams(UnarchiveParams @params);

    /// <summary>
    /// Set the unxip storage directory path
    /// 设置解压存储目录路径
    /// </summary>
    /// <param name="dstPath">目标路径</param>
    void SetDstPath(string dstPath);

    /// <summary>
    /// Extract a single archive
    /// 解压单个归档文件
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    void UnarchiveFile(string srcFilePath);

    /// <summary>
    /// Extract a single archive
    /// 解压单个归档文件
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    /// <param name="dstDir">目标目录</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    void UnarchiveFile(string srcFilePath, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// Extract a single archive
    /// 解压单个归档文件
    /// </summary>
    /// <param name="srcFilePath">源文件路径</param>
    /// <param name="params">解压参数</param>
    void UnarchiveFile(string srcFilePath, UnarchiveParams @params);

    /// <summary>
    /// Extract multiple archives
    /// 解压多个归档文件
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    void UnarchiveFiles(IEnumerable<string> srcFilePaths);

    /// <summary>
    /// Extract multiple archives
    /// 解压多个归档文件
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    /// <param name="dstDir">目标目录</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    void UnarchiveFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// Extract multiple archives
    /// 解压多个归档文件
    /// </summary>
    /// <param name="srcFilePaths">源文件路径集合</param>
    /// <param name="params">解压参数</param>
    void UnarchiveFiles(IEnumerable<string> srcFilePaths, UnarchiveParams @params);
}
```

### 类 (Classes)

#### UnarchiveParams
解压参数类

```csharp
/// <summary>
/// 解压参数类
/// 定义解压操作的各项参数
/// </summary>
public class UnarchiveParams
{
    private string m_DstPath;
    private readonly bool m_Override;
    private readonly Encoding m_Encoding;

    /// <summary>
    /// 目标路径
    /// </summary>
    public string DstPath => m_DstPath;

    /// <summary>
    /// 是否覆盖
    /// </summary>
    public bool Override => m_Override;

    /// <summary>
    /// 编码格式
    /// </summary>
    public Encoding Encoding => m_Encoding;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dstPath">目标路径</param>
    public UnarchiveParams(string dstPath);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dstPath">目标路径</param>
    /// <param name="override">是否覆盖</param>
    /// <param name="encoding">编码格式</param>
    public UnarchiveParams(string dstPath, bool @override, Encoding encoding);

    /// <summary>
    /// 设置目标路径
    /// </summary>
    /// <param name="path">新路径</param>
    public void SetDstPath(string path);
}
```

#### Unzip
解压实现类

```csharp
/// <summary>
/// 解压实现类
/// 提供ZIP等归档文件的解压功能
/// </summary>
public class Unzip : IUnarchive
{
    // 具体实现需要进一步分析文件内容
}
```

#### ArchiveUtil
归档工具类

```csharp
/// <summary>
/// 归档工具类
/// 提供归档文件操作的静态工具方法
/// </summary>
public static class ArchiveUtil
{
    /// <summary>
    /// 解压ZIP文件
    /// </summary>
    /// <param name="zipPath">ZIP文件路径</param>
    /// <param name="extractPath">解压路径</param>
    /// <param name="overwrite">是否覆盖</param>
    public static void ExtractZip(string zipPath, string extractPath, bool overwrite = true);

    /// <summary>
    /// 创建ZIP文件
    /// </summary>
    /// <param name="zipPath">ZIP文件路径</param>
    /// <param name="sourcePath">源文件路径</param>
    /// <param name="compressionLevel">压缩级别</param>
    public static void CreateZip(string zipPath, string sourcePath, int compressionLevel = 6);

    /// <summary>
    /// 检查文件是否为ZIP格式
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>是否为ZIP格式</returns>
    public static bool IsZipFile(string filePath);

    /// <summary>
    /// 获取ZIP文件中的文件列表
    /// </summary>
    /// <param name="zipPath">ZIP文件路径</param>
    /// <returns>文件列表</returns>
    public static string[] GetZipFileList(string zipPath);
}
```

### 事件数据类 (Event Data Classes)

#### UnarchiveEventData
解压事件数据类

```csharp
/// <summary>
/// 解压事件数据类
/// 包含解压操作的详细信息
/// </summary>
public sealed class UnarchiveEventData
{
    private readonly string m_ArchiveFilePath;
    private readonly string[] m_Files;
    private readonly string[] m_IgnoreFiles;

    /// <summary>
    /// Archive file path
    /// 归档文件路径
    /// </summary>
    public string ArchiveFilePath => m_ArchiveFilePath;

    /// <summary>
    /// Unarchive the HeaderName corresponding to the generated file
    /// 解除归档生成的文件对应的HeaderName
    /// </summary>
    public string[] Files => m_Files;

    /// <summary>
    /// Unarchive the HeaderName corresponding to the ignored file
    /// 解除归档忽略的文件对应的HeaderName
    /// </summary>
    public string[] IgnoreFiles => m_IgnoreFiles;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="archiveFilePath">归档文件路径</param>
    /// <param name="files">解压的文件列表</param>
    /// <param name="ignoreFiles">忽略的文件列表</param>
    public UnarchiveEventData(string archiveFilePath, string[] files, string[] ignoreFiles);

    /// <summary>
    /// 字符串表示
    /// </summary>
    /// <returns>事件数据字符串</returns>
    public override string ToString();
}
```

#### UnarchiveEntryEventData
解压条目事件数据类

```csharp
/// <summary>
/// 解压条目事件数据类
/// 包含单个文件解压的详细信息
/// </summary>
public sealed class UnarchiveEntryEventData
{
    private readonly string m_ArchiveFilePath;
    private readonly string m_EntryHeaderName;

    /// <summary>
    /// Archive file path
    /// 归档文件路径
    /// </summary>
    public string ArchiveFilePath => m_ArchiveFilePath;

    /// <summary>
    /// Unarchive the HeaderName corresponding to the generated file
    /// 解除归档生成的文件对应的HeaderName
    /// </summary>
    public string EntryHeaderName => m_EntryHeaderName;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="archiveFilePath">归档文件路径</param>
    /// <param name="entryHeaderName">条目头名称</param>
    public UnarchiveEntryEventData(string archiveFilePath, string entryHeaderName);

    /// <summary>
    /// 字符串表示
    /// </summary>
    /// <returns>事件数据字符串</returns>
    public override string ToString();
}
```

### 静态类 (Static Classes)

#### UnarchiveEvents
解压事件常量

```csharp
/// <summary>
/// 解压事件常量
/// 定义解压过程中触发的事件类型
/// </summary>
public static class UnarchiveEvents
{
    /// <summary>
    /// Archive decompression complete single file complete event
    /// 归档解压完成单个文件完成事件
    /// </summary>
    public const string EventUnarchiveEntry = "UnarchiveEvent.EventUnarchiveEntry";

    /// <summary>
    /// Archive decompression complete event
    /// 归档解压完成事件
    /// </summary>
    public const string EventUnarchive = "UnarchiveEvent.EventUnarchive";
}
```

### 功能说明

#### 解压功能特性

**基本功能**
- **单文件解压**：支持解压单个归档文件
- **批量解压**：支持同时解压多个归档文件
- **参数配置**：支持自定义解压参数
- **事件通知**：提供解压进度和完成事件

**高级功能**
1. **编码支持**：支持多种字符编码格式
2. **覆盖控制**：可选择是否覆盖已存在的文件
3. **路径管理**：灵活的目标路径设置
4. **错误处理**：完善的异常处理机制

#### 事件驱动架构

**事件类型**
- **EventUnarchiveEntry**：单个文件解压完成事件
- **EventUnarchive**：整个归档解压完成事件

**事件数据**
- **UnarchiveEventData**：包含解压文件列表和忽略文件列表
- **UnarchiveEntryEventData**：包含单个文件的解压信息

#### 参数管理

**UnarchiveParams**
- **DstPath**：解压目标路径
- **Override**：是否覆盖现有文件
- **Encoding**：文件编码格式

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
// 创建解压参数
var params = new UnarchiveParams("output_folder", true, Encoding.UTF8);

// 设置解压器参数
var unarchive = new Unzip();
unarchive.SetUnarchiveParams(params);

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
// 检查是否为ZIP文件
bool isZip = ArchiveUtil.IsZipFile("file.zip");
Console.WriteLine($"是否为ZIP文件: {isZip}");

// 获取ZIP文件列表
string[] fileList = ArchiveUtil.GetZipFileList("archive.zip");
Console.WriteLine("ZIP文件内容:");
foreach (var file in fileList)
{
    Console.WriteLine($"  {file}");
}

// 解压ZIP文件
ArchiveUtil.ExtractZip("archive.zip", "extract_folder", true);

// 创建ZIP文件
ArchiveUtil.CreateZip("new_archive.zip", "source_folder", 6);
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

1. **事件驱动**：基于事件的通知机制
2. **参数化配置**：灵活的参数设置
3. **批量处理**：支持批量解压操作
4. **编码支持**：支持多种字符编码
5. **错误处理**：完善的异常处理
6. **工具类支持**：提供静态工具方法

### 注意事项

1. **文件权限**：确保有足够的文件读写权限
2. **磁盘空间**：解压前检查目标磁盘空间
3. **编码问题**：注意文件名的编码格式
4. **覆盖风险**：使用覆盖模式时注意数据安全
5. **内存使用**：大文件解压时注意内存使用
6. **事件监听**：及时清理不需要的事件监听器 