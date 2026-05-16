using System.Text;

namespace JLGames.Infra.Archive
{
    /// <summary>
    /// Parameters for archive extraction (destination path, overwrite policy, entry name encoding).
    /// 归档解压参数（目标目录、是否覆盖、条目名编码）。
    /// </summary>
    public class UnarchiveParams
    {
        private string m_DstPath;
        private readonly bool m_Override;
        private readonly Encoding m_Encoding;

        /// <summary>
        /// Destination directory for extracted files.
        /// 解压输出目录。
        /// </summary>
        public string DstPath => m_DstPath;

        /// <summary>
        /// Whether to overwrite existing files at the destination.
        /// 是否覆盖目标路径下已存在的文件。
        /// </summary>
        public bool Override => m_Override;

        /// <summary>
        /// Encoding used to read entry names from the archive.
        /// 读取归档内条目名称时使用的编码。
        /// </summary>
        public Encoding Encoding => m_Encoding;

        /// <summary>
        /// Create parameters with default overwrite (true) and UTF-8 encoding.
        /// 使用默认覆盖策略（true）与 UTF-8 编码创建参数。
        /// </summary>
        /// <param name="dstPath"></param>
        public UnarchiveParams(string dstPath)
        {
            m_DstPath = dstPath;
            m_Override = true;
            m_Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Create parameters with the specified destination, overwrite policy, and encoding.
        /// 使用指定的目标目录、覆盖策略与编码创建参数。
        /// </summary>
        /// <param name="dstPath"></param>
        /// <param name="override"></param>
        /// <param name="encoding"></param>
        public UnarchiveParams(string dstPath, bool @override, Encoding encoding)
        {
            m_DstPath = dstPath;
            m_Override = @override;
            m_Encoding = encoding;
        }

        /// <summary>
        /// Update the destination directory path.
        /// 更新解压输出目录路径。
        /// </summary>
        /// <param name="path"></param>
        public void SetDstPath(string path)
        {
            m_DstPath = path;
        }
    }
}