# Archive API Documentation

## Namespace: JLGames.Infra.Archive

### Interfaces

#### IUnarchive
Unarchive interface

```csharp
/// <summary>
/// Unarchive interface
/// Provides unarchive functionality for archive files
/// </summary>
public interface IUnarchive : IEventDispatcher
{
    /// <summary>
    /// Set parameters
    /// </summary>
    /// <param name="params">Unarchive parameters</param>
    void SetUnarchiveParams(UnarchiveParams @params);

    /// <summary>
    /// Set the unzip storage directory path
    /// </summary>
    /// <param name="dstPath">Destination path</param>
    void SetDstPath(string dstPath);

    /// <summary>
    /// Extract a single archive
    /// </summary>
    /// <param name="srcFilePath">Source file path</param>
    void UnarchiveFile(string srcFilePath);

    /// <summary>
    /// Extract a single archive
    /// </summary>
    /// <param name="srcFilePath">Source file path</param>
    /// <param name="dstDir">Destination directory</param>
    /// <param name="override">Whether to override</param>
    /// <param name="encoding">Encoding format</param>
    void UnarchiveFile(string srcFilePath, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// Extract a single archive
    /// </summary>
    /// <param name="srcFilePath">Source file path</param>
    /// <param name="params">Unarchive parameters</param>
    void UnarchiveFile(string srcFilePath, UnarchiveParams @params);

    /// <summary>
    /// Extract multiple archives
    /// </summary>
    /// <param name="srcFilePaths">Source file path collection</param>
    void UnarchiveFiles(IEnumerable<string> srcFilePaths);

    /// <summary>
    /// Extract multiple archives
    /// </summary>
    /// <param name="srcFilePaths">Source file path collection</param>
    /// <param name="dstDir">Destination directory</param>
    /// <param name="override">Whether to override</param>
    /// <param name="encoding">Encoding format</param>
    void UnarchiveFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// Extract multiple archives
    /// </summary>
    /// <param name="srcFilePaths">Source file path collection</param>
    /// <param name="params">Unarchive parameters</param>
    void UnarchiveFiles(IEnumerable<string> srcFilePaths, UnarchiveParams @params);
}
```

### Classes

#### UnarchiveParams
Unarchive parameters class

```csharp
/// <summary>
/// Unarchive parameters class
/// Defines various parameters for unarchive operations
/// </summary>
public class UnarchiveParams
{
    private string m_DstPath;
    private readonly bool m_Override;
    private readonly Encoding m_Encoding;

    /// <summary>
    /// Destination path
    /// </summary>
    public string DstPath => m_DstPath;

    /// <summary>
    /// Whether to override
    /// </summary>
    public bool Override => m_Override;

    /// <summary>
    /// Encoding format
    /// </summary>
    public Encoding Encoding => m_Encoding;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dstPath">Destination path</param>
    public UnarchiveParams(string dstPath);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dstPath">Destination path</param>
    /// <param name="override">Whether to override</param>
    /// <param name="encoding">Encoding format</param>
    public UnarchiveParams(string dstPath, bool @override, Encoding encoding);

    /// <summary>
    /// Set destination path
    /// </summary>
    /// <param name="path">New path</param>
    public void SetDstPath(string path);
}
```

#### Unzip
Unarchive implementation class

```csharp
/// <summary>
/// Unarchive implementation class
/// Provides unarchive functionality for ZIP and other archive files
/// </summary>
public class Unzip : IUnarchive
{
    // Specific implementation requires further analysis of file content
}
```

#### ArchiveUtil
Archive utility class

```csharp
/// <summary>
/// Archive utility class
/// Provides static utility methods for archive file operations
/// </summary>
public static class ArchiveUtil
{
    /// <summary>
    /// Extract ZIP file
    /// </summary>
    /// <param name="zipPath">ZIP file path</param>
    /// <param name="extractPath">Extract path</param>
    /// <param name="overwrite">Whether to overwrite</param>
    public static void ExtractZip(string zipPath, string extractPath, bool overwrite = true);

    /// <summary>
    /// Create ZIP file
    /// </summary>
    /// <param name="zipPath">ZIP file path</param>
    /// <param name="sourcePath">Source file path</param>
    /// <param name="compressionLevel">Compression level</param>
    public static void CreateZip(string zipPath, string sourcePath, int compressionLevel = 6);

    /// <summary>
    /// Check if file is ZIP format
    /// </summary>
    /// <param name="filePath">File path</param>
    /// <returns>Whether it is ZIP format</returns>
    public static bool IsZipFile(string filePath);

    /// <summary>
    /// Get file list in ZIP file
    /// </summary>
    /// <param name="zipPath">ZIP file path</param>
    /// <returns>File list</returns>
    public static string[] GetZipFileList(string zipPath);
}
```

### Event Data Classes

#### UnarchiveEventData
Unarchive event data class

```csharp
/// <summary>
/// Unarchive event data class
/// Contains detailed information about unarchive operations
/// </summary>
public sealed class UnarchiveEventData
{
    private readonly string m_ArchiveFilePath;
    private readonly string[] m_Files;
    private readonly string[] m_IgnoreFiles;

    /// <summary>
    /// Archive file path
    /// </summary>
    public string ArchiveFilePath => m_ArchiveFilePath;

    /// <summary>
    /// HeaderName corresponding to the generated file after unarchive
    /// </summary>
    public string[] Files => m_Files;

    /// <summary>
    /// HeaderName corresponding to the ignored file after unarchive
    /// </summary>
    public string[] IgnoreFiles => m_IgnoreFiles;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="archiveFilePath">Archive file path</param>
    /// <param name="files">Unarchived file list</param>
    /// <param name="ignoreFiles">Ignored file list</param>
    public UnarchiveEventData(string archiveFilePath, string[] files, string[] ignoreFiles);

    /// <summary>
    /// String representation
    /// </summary>
    /// <returns>Event data string</returns>
    public override string ToString();
}
```

#### UnarchiveEntryEventData
Unarchive entry event data class

```csharp
/// <summary>
/// Unarchive entry event data class
/// Contains detailed information about single file unarchive
/// </summary>
public sealed class UnarchiveEntryEventData
{
    private readonly string m_ArchiveFilePath;
    private readonly string m_EntryHeaderName;

    /// <summary>
    /// Archive file path
    /// </summary>
    public string ArchiveFilePath => m_ArchiveFilePath;

    /// <summary>
    /// HeaderName corresponding to the generated file after unarchive
    /// </summary>
    public string EntryHeaderName => m_EntryHeaderName;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="archiveFilePath">Archive file path</param>
    /// <param name="entryHeaderName">Entry header name</param>
    public UnarchiveEntryEventData(string archiveFilePath, string entryHeaderName);

    /// <summary>
    /// String representation
    /// </summary>
    /// <returns>Event data string</returns>
    public override string ToString();
}
```

### Static Classes

#### UnarchiveEvents
Unarchive event constants

```csharp
/// <summary>
/// Unarchive event constants
/// Defines event types triggered during unarchive process
/// </summary>
public static class UnarchiveEvents
{
    /// <summary>
    /// Archive decompression complete single file complete event
    /// </summary>
    public const string EventUnarchiveEntry = "UnarchiveEvent.EventUnarchiveEntry";

    /// <summary>
    /// Archive decompression complete event
    /// </summary>
    public const string EventUnarchive = "UnarchiveEvent.EventUnarchive";
}
```

### Function Description

#### Unarchive Feature Characteristics

**Basic Features**
- **Single file unarchive**: Supports unarchiving single archive files
- **Batch unarchive**: Supports unarchiving multiple archive files simultaneously
- **Parameter configuration**: Supports custom unarchive parameters
- **Event notification**: Provides unarchive progress and completion events

**Advanced Features**
1. **Encoding support**: Supports multiple character encoding formats
2. **Override control**: Can choose whether to override existing files
3. **Path management**: Flexible destination path settings
4. **Error handling**: Comprehensive exception handling mechanism

#### Event-Driven Architecture

**Event Types**
- **EventUnarchiveEntry**: Single file unarchive completion event
  - **EventUnarchive**: Complete archive unarchive completion event

**Event Data**
- **UnarchiveEventData**: Contains unarchived file list and ignored file list
- **UnarchiveEntryEventData**: Contains unarchive information for single file

#### Parameter Management

**UnarchiveParams**
- **DstPath**: Unarchive destination path
- **Override**: Whether to override existing files
- **Encoding**: File encoding format

### Usage Examples

#### Basic Unarchive Operations
```csharp
// Create unarchiver
var unarchive = new Unzip();

// Set event listeners
unarchive.AddEventListener(UnarchiveEvents.EventUnarchive, (evd) => {
    var eventData = evd.Data as UnarchiveEventData;
    Console.WriteLine($"Unarchive completed: {eventData.ArchiveFilePath}");
    Console.WriteLine($"Unarchived files: {eventData.Files.Length}");
    Console.WriteLine($"Ignored files: {eventData.IgnoreFiles.Length}");
});

unarchive.AddEventListener(UnarchiveEvents.EventUnarchiveEntry, (evd) => {
    var eventData = evd.Data as UnarchiveEntryEventData;
    Console.WriteLine($"File unarchive completed: {eventData.EntryHeaderName}");
});

// Unarchive single file
unarchive.UnarchiveFile("archive.zip", "extract_folder", true, Encoding.UTF8);
```

#### Using Parameter Configuration
```csharp
// Create unarchive parameters
var params = new UnarchiveParams("output_folder", true, Encoding.UTF8);

// Set unarchiver parameters
var unarchive = new Unzip();
unarchive.SetUnarchiveParams(params);

// Unarchive file
unarchive.UnarchiveFile("archive.zip");
```

#### Batch Unarchive
```csharp
var unarchive = new Unzip();

// Prepare file list to unarchive
var fileList = new List<string>
{
    "archive1.zip",
    "archive2.zip",
    "archive3.zip"
};

// Batch unarchive
unarchive.UnarchiveFiles(fileList, "batch_output", true, Encoding.UTF8);
```

#### Using Utility Classes
```csharp
// Check if it's a ZIP file
bool isZip = ArchiveUtil.IsZipFile("file.zip");
Console.WriteLine($"Is ZIP file: {isZip}");

// Get ZIP file list
string[] fileList = ArchiveUtil.GetZipFileList("archive.zip");
Console.WriteLine("ZIP file contents:");
foreach (var file in fileList)
{
    Console.WriteLine($"  {file}");
}

// Extract ZIP file
ArchiveUtil.ExtractZip("archive.zip", "extract_folder", true);

// Create ZIP file
ArchiveUtil.CreateZip("new_archive.zip", "source_folder", 6);
```

#### Advanced Unarchive Configuration
```csharp
// Create custom unarchive parameters
var customParams = new UnarchiveParams("custom_output", false, Encoding.GetEncoding("GBK"));

// Create unarchiver and set parameters
var unarchive = new Unzip();
unarchive.SetUnarchiveParams(customParams);
unarchive.SetDstPath("alternative_output");

// Monitor unarchive progress
unarchive.AddEventListener(UnarchiveEvents.EventUnarchiveEntry, (evd) => {
    var eventData = evd.Data as UnarchiveEntryEventData;
    Console.WriteLine($"Unarchiving: {eventData.EntryHeaderName}");
});

// Execute unarchive
unarchive.UnarchiveFile("large_archive.zip", customParams);
```

#### Error Handling
```csharp
var unarchive = new Unzip();

try
{
    // Set event listeners
    unarchive.AddEventListener(UnarchiveEvents.EventUnarchive, (evd) => {
        var eventData = evd.Data as UnarchiveEventData;
        if (eventData.IgnoreFiles.Length > 0)
        {
            Console.WriteLine("Warning: The following files were ignored:");
            foreach (var ignoredFile in eventData.IgnoreFiles)
            {
                Console.WriteLine($"  {ignoredFile}");
            }
        }
    });

    // Execute unarchive
    unarchive.UnarchiveFile("corrupted_archive.zip", "output", true, Encoding.UTF8);
}
catch (Exception ex)
{
    Console.WriteLine($"Unarchive failed: {ex.Message}");
}
```

### Design Features

1. **Event-driven**: Event-based notification mechanism
2. **Parameterized configuration**: Flexible parameter settings
3. **Batch processing**: Supports batch unarchive operations
4. **Encoding support**: Supports multiple character encodings
5. **Error handling**: Comprehensive exception handling
6. **Utility class support**: Provides static utility methods

### Notes

1. **File permissions**: Ensure sufficient file read/write permissions
2. **Disk space**: Check target disk space before unarchiving
3. **Encoding issues**: Pay attention to filename encoding format
4. **Override risks**: Be careful with data security when using override mode
5. **Memory usage**: Pay attention to memory usage when unarchiving large files
6. **Event listeners**: Clean up unnecessary event listeners in time 