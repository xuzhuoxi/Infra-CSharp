namespace JLGames.Infra.Archive
{
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

        public override string ToString()
        {
            return $"{{{m_ArchiveFilePath}, FileSize={m_Files?.Length}, IgnoreSize={m_IgnoreFiles?.Length}}}";
        }

        public UnarchiveEventData(string archiveFilePath, string[] files, string[] ignoreFiles)
        {
            m_ArchiveFilePath = archiveFilePath;
            m_Files = files;
            m_IgnoreFiles = ignoreFiles;
        }
    }

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

        public override string ToString()
        {
            return $"{{{m_ArchiveFilePath}, {m_EntryHeaderName}}}";
        }

        public UnarchiveEntryEventData(string archiveFilePath, string entryHeaderName)
        {
            m_ArchiveFilePath = archiveFilePath;
            m_EntryHeaderName = entryHeaderName;
        }
    }
}