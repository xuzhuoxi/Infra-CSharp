namespace JLGames.Infra.Archive
{
    /// <summary>
    /// Event name constants dispatched during archive extraction.
    /// 归档解压过程中分发的事件名常量。
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

    /// <summary>
    /// Event payload when an entire archive has been processed.
    /// 单个归档文件全部处理完成时的事件数据。
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
        /// Returns a short diagnostic string (path and entry counts).
        /// 返回包含路径与条目数量的简要诊断字符串。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{m_ArchiveFilePath}, FileSize={m_Files?.Length}, IgnoreSize={m_IgnoreFiles?.Length}}}";
        }

        /// <summary>
        /// Create completion event data for one archive.
        /// 为单个归档创建解压完成事件数据。
        /// </summary>
        /// <param name="archiveFilePath"></param>
        /// <param name="files"></param>
        /// <param name="ignoreFiles"></param>
        public UnarchiveEventData(string archiveFilePath, string[] files, string[] ignoreFiles)
        {
            m_ArchiveFilePath = archiveFilePath;
            m_Files = files;
            m_IgnoreFiles = ignoreFiles;
        }
    }

    /// <summary>
    /// Event payload when a single entry inside an archive has been extracted or skipped.
    /// 归档内单个条目解压（或跳过）时的事件数据。
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
        /// Returns a short diagnostic string (archive path and entry name).
        /// 返回包含归档路径与条目名的简要诊断字符串。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{m_ArchiveFilePath}, {m_EntryHeaderName}}}";
        }

        /// <summary>
        /// Create per-entry event data.
        /// 创建单条目解压事件数据。
        /// </summary>
        /// <param name="archiveFilePath"></param>
        /// <param name="entryHeaderName"></param>
        public UnarchiveEntryEventData(string archiveFilePath, string entryHeaderName)
        {
            m_ArchiveFilePath = archiveFilePath;
            m_EntryHeaderName = entryHeaderName;
        }
    }
}