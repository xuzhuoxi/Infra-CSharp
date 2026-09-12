# Archive API Documentation

## Namespace: JLGames.Infra.Archive

### Interfaces

#### IUnarchive
Archive extraction contract; supports parameter configuration, single/batch unzip, and progress events. Extends `IEventDispatcher`.

```csharp
/// <summary>
/// Archive extraction contract; supports parameter configuration, single/batch unzip, and progress events.
/// </summary>
public interface IUnarchive : IEventDispatcher
{
    /// <summary>
    /// Set parameters
    /// </summary>
    /// <param name="params">Unarchive parameters</param>
    void SetUnarchiveParams(UnarchiveParams @params);

    /// <summary>
    /// Set the unxip storage directory path
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
Parameters for archive extraction (destination path, overwrite policy, entry name encoding).

```csharp
/// <summary>
/// Parameters for archive extraction (destination path, overwrite policy, entry name encoding).
/// </summary>
public class UnarchiveParams
{
    private string m_DstPath;
    private readonly bool m_Override;
    private readonly Encoding m_Encoding;

    /// <summary>
    /// Destination directory for extracted files.
    /// </summary>
    public string DstPath { get; }

    /// <summary>
    /// Whether to overwrite existing files at the destination.
    /// </summary>
    public bool Override { get; }

    /// <summary>
    /// Encoding used to read entry names from the archive.
    /// </summary>
    public Encoding Encoding { get; }

    /// <summary>
    /// Create parameters with default overwrite (true) and UTF-8 encoding.
    /// </summary>
    /// <param name="dstPath">Destination path</param>
    public UnarchiveParams(string dstPath);

    /// <summary>
    /// Create parameters with the specified destination, overwrite policy, and encoding.
    /// </summary>
    /// <param name="dstPath">Destination path</param>
    /// <param name="override">Whether to override</param>
    /// <param name="encoding">Encoding format</param>
    public UnarchiveParams(string dstPath, bool @override, Encoding encoding);

    /// <summary>
    /// Update the destination directory path.
    /// </summary>
    /// <param name="path">New path</param>
    public void SetDstPath(string path);
}
```

#### Unzip
ZIP archive extractor; dispatches `UnarchiveEvents` per entry and per archive. Extends `EventDispatcher` and implements `IUnarchive`.

```csharp
/// <summary>
/// ZIP archive extractor; dispatches <see cref="UnarchiveEvents"/> per entry and per archive.
/// </summary>
public class Unzip : EventDispatcher, IUnarchive
{
    /// <summary>
    /// Set parameters
    /// </summary>
    /// <param name="params">Unarchive parameters</param>
    public void SetUnarchiveParams(UnarchiveParams @params);

    /// <summary>
    /// Set the unzip storage directory path
    /// </summary>
    /// <param name="dstPath">Destination path</param>
    public void SetDstPath(string dstPath);

    /// <summary>
    /// Extract a single archive using previously set parameters.
    /// </summary>
    /// <param name="srcFilePath">Source file path</param>
    public void UnarchiveFile(string srcFilePath);

    /// <summary>
    /// Extract a single archive
    /// </summary>
    /// <param name="srcFilePath">Source file path</param>
    /// <param name="dstDir">Destination directory</param>
    /// <param name="override">Whether to override</param>
    /// <param name="encoding">Encoding format</param>
    public void UnarchiveFile(string srcFilePath, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// Extract a single archive
    /// </summary>
    /// <param name="srcFilePath">Source file path</param>
    /// <param name="params">Unarchive parameters</param>
    public void UnarchiveFile(string srcFilePath, UnarchiveParams @params);

    /// <summary>
    /// Extract multiple archives using previously set parameters.
    /// </summary>
    /// <param name="srcFilePaths">Source file path collection</param>
    public void UnarchiveFiles(IEnumerable<string> srcFilePaths);

    /// <summary>
    /// Extract multiple archives
    /// </summary>
    /// <param name="srcFilePaths">Source file path collection</param>
    /// <param name="dstDir">Destination directory</param>
    /// <param name="override">Whether to override</param>
    /// <param name="encoding">Encoding format</param>
    public void UnarchiveFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// Extract multiple archives
    /// </summary>
    /// <param name="srcFilePaths">Source file path collection</param>
    /// <param name="params">Unarchive parameters</param>
    public void UnarchiveFiles(IEnumerable<string> srcFilePaths, UnarchiveParams @params);
}
```

`SetDstPath`: if no parameters are set, creates `UnarchiveParams` for that path (default overwrite, UTF-8); if parameters already exist, only updates the destination directory.

`UnarchiveFile`: returns immediately if the source file does not exist; creates the destination directory if it is missing. Existing entries are skipped and recorded in the ignore list when `Override` is `false`. Dispatches `EventUnarchiveEntry` after each extracted entry and `EventUnarchive` when the archive is finished.

#### ArchiveUtil
Static helpers for ZIP archive extraction without subscribing to events. Internally creates `Unzip`, sets parameters, and runs extraction.

```csharp
/// <summary>
/// Static helpers for ZIP archive extraction without subscribing to events.
/// </summary>
public static class ArchiveUtil
{
    /// <summary>
    /// Unpack a single archive
    /// </summary>
    /// <param name="srcFilePath">Source file path</param>
    /// <param name="dstDir">Destination directory</param>
    /// <param name="override">Whether to override</param>
    /// <param name="encoding">Encoding format</param>
    public static void UnzipFile(string srcFilePath, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// Unpack a single archive
    /// </summary>
    /// <param name="srcFilePath">Source file path</param>
    /// <param name="dstDir">Destination directory</param>
    public static void UnzipFile(string srcFilePath, string dstDir);

    /// <summary>
    /// Unzip multiple archives
    /// </summary>
    /// <param name="srcFilePaths">Source file path collection</param>
    /// <param name="dstDir">Destination directory</param>
    /// <param name="override">Whether to override</param>
    /// <param name="encoding">Encoding format</param>
    public static void UnzipFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override, Encoding encoding);

    /// <summary>
    /// Unzip multiple archives
    /// </summary>
    /// <param name="srcFilePaths">Source file path collection</param>
    /// <param name="dstDir">Destination directory</param>
    public static void UnzipFiles(IEnumerable<string> srcFilePaths, string dstDir);
}
```

The two-argument overloads are equivalent to `@override = true` and `encoding = Encoding.UTF8`.

### Event Data Classes

#### UnarchiveEventData
Event payload when an entire archive has been processed.

```csharp
/// <summary>
/// Event payload when an entire archive has been processed.
/// </summary>
public sealed class UnarchiveEventData
{
    private readonly string m_ArchiveFilePath;
    private readonly string[] m_Files;
    private readonly string[] m_IgnoreFiles;

    /// <summary>
    /// Archive file path
    /// </summary>
    public string ArchiveFilePath { get; }

    /// <summary>
    /// Unarchive the HeaderName corresponding to the generated file
    /// </summary>
    public string[] Files { get; }

    /// <summary>
    /// Unarchive the HeaderName corresponding to the ignored file
    /// </summary>
    public string[] IgnoreFiles { get; }

    /// <summary>
    /// Create completion event data for one archive.
    /// </summary>
    /// <param name="archiveFilePath">Archive file path</param>
    /// <param name="files">Unarchived file list</param>
    /// <param name="ignoreFiles">Ignored file list</param>
    public UnarchiveEventData(string archiveFilePath, string[] files, string[] ignoreFiles);

    /// <summary>
    /// Returns a short diagnostic string (path and entry counts).
    /// </summary>
    public override string ToString();
}
```

#### UnarchiveEntryEventData
Event payload when a single entry inside an archive has been extracted or skipped. The current implementation dispatches this event only after a successful extract.

```csharp
/// <summary>
/// Event payload when a single entry inside an archive has been extracted or skipped.
/// </summary>
public sealed class UnarchiveEntryEventData
{
    private readonly string m_ArchiveFilePath;
    private readonly string m_EntryHeaderName;

    /// <summary>
    /// Archive file path
    /// </summary>
    public string ArchiveFilePath { get; }

    /// <summary>
    /// Unarchive the HeaderName corresponding to the generated file
    /// </summary>
    public string EntryHeaderName { get; }

    /// <summary>
    /// Create per-entry event data.
    /// </summary>
    /// <param name="archiveFilePath">Archive file path</param>
    /// <param name="entryHeaderName">Entry header name</param>
    public UnarchiveEntryEventData(string archiveFilePath, string entryHeaderName);

    /// <summary>
    /// Returns a short diagnostic string (archive path and entry name).
    /// </summary>
    public override string ToString();
}
```

### Static Classes

#### UnarchiveEvents
Event name constants dispatched during archive extraction.

```csharp
/// <summary>
/// Event name constants dispatched during archive extraction.
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
- **Single file unarchive**: Supports extracting a single ZIP archive
- **Batch unarchive**: Supports extracting multiple archives sequentially
- **Parameter configuration**: Destination, overwrite policy, and encoding via `UnarchiveParams` or method arguments
- **Event notification**: `Unzip` dispatches per-entry and per-archive events (`ArchiveUtil` does not subscribe to events)

**Advanced Features**
1. **Encoding support**: `Encoding` is used to read entry names from the archive
2. **Override control**: When `Override` is `false`, existing files are skipped and recorded in `IgnoreFiles`
3. **Path management**: `SetDstPath` updates the destination on existing parameters, or creates default parameters if none are set
4. **Silent skip**: `UnarchiveFile` returns without throwing if the source file does not exist

#### Event-Driven Architecture

**Event Types**
- **EventUnarchiveEntry**: Single entry extraction completion event
- **EventUnarchive**: Complete archive processing event

**Event Data**
- **UnarchiveEventData**: Contains extracted file list and ignored file list
- **UnarchiveEntryEventData**: Contains archive path and entry name for a single entry

#### Parameter Management

**UnarchiveParams**
- **DstPath**: Destination directory for extracted files
- **Override**: Whether to overwrite existing files at the destination (defaults to `true` in the single-argument constructor)
- **Encoding**: Encoding used to read entry names from the archive (defaults to `Encoding.UTF8` in the single-argument constructor)

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
// Create unarchive parameters (overwrite + UTF-8)
var unarchiveParams = new UnarchiveParams("output_folder", true, Encoding.UTF8);

// Set unarchiver parameters
var unarchive = new Unzip();
unarchive.SetUnarchiveParams(unarchiveParams);

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
// Unpack a single ZIP (default overwrite, UTF-8)
ArchiveUtil.UnzipFile("archive.zip", "extract_folder");

// Unpack a single ZIP (explicit overwrite and encoding)
ArchiveUtil.UnzipFile("archive.zip", "extract_folder", true, Encoding.UTF8);

// Unzip multiple archives (default overwrite, UTF-8)
var fileList = new List<string>
{
    "archive1.zip",
    "archive2.zip"
};
ArchiveUtil.UnzipFiles(fileList, "batch_output");

// Unzip multiple archives (explicit overwrite and encoding)
ArchiveUtil.UnzipFiles(fileList, "batch_output", false, Encoding.GetEncoding("GBK"));
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

1. **Event-driven**: `Unzip` dispatches per-entry and per-archive events via `EventDispatcher`
2. **Parameterized configuration**: Destination, overwrite policy, and entry-name encoding via `UnarchiveParams`
3. **Batch processing**: Sequential extraction from a path collection
4. **Encoding support**: `Encoding` is used to read ZIP entry names
5. **Override and ignore**: Existing entries are recorded in `IgnoreFiles` when overwrite is disabled
6. **Utility class support**: `ArchiveUtil` provides static unzip helpers without event subscription

### Notes

1. **File permissions**: Ensure sufficient file read/write permissions
2. **Disk space**: Check target disk space before unarchiving
3. **Encoding issues**: `Encoding` applies to ZIP entry names, not file contents
4. **Override risks**: Be careful with data security when using override mode
5. **Missing source file**: Returns silently if the source path does not exist; no exception is thrown
6. **Event listeners**: `ArchiveUtil` does not expose events; clean up unused listeners when using `Unzip`
