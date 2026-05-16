using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using JLGames.Infra.Event;
using JLGames.Infra.Utils;

namespace JLGames.Infra.Archive
{
    /// <summary>
    /// ZIP archive extractor; dispatches <see cref="UnarchiveEvents"/> per entry and per archive.
    /// ZIP 归档解压实现；按条目与整包分发 <see cref="UnarchiveEvents"/> 事件。
    /// </summary>
    public class Unzip : EventDispatcher, IUnarchive
    {
        private UnarchiveParams m_Params;
        private List<string> m_TempEntryList;
        private List<string> m_TempIgnoreList;

        /// <summary>
        /// Set parameters
        /// 设置参数
        /// </summary>
        /// <param name="params"></param>
        public void SetUnarchiveParams(UnarchiveParams @params)
        {
            m_Params = @params;
        }

        /// <summary>
        /// Set the unzip storage directory path
        /// 设置解压存储目录路径
        /// </summary>
        /// <param name="dstPath"></param>
        public void SetDstPath(string dstPath)
        {
            if (null == m_Params)
            {
                m_Params = new UnarchiveParams(dstPath);
                return;
            }

            m_Params.SetDstPath(dstPath);
        }

        /// <summary>
        /// Extract a single archive using previously set parameters.
        /// 使用已设置的参数解压单个归档文件。
        /// </summary>
        /// <param name="srcFilePath"></param>
        public void UnarchiveFile(string srcFilePath)
        {
            UnarchiveFile(srcFilePath, m_Params);
        }

        /// <summary>
        /// Extract a single archive
        /// 解压单个归档文件
        /// </summary>
        /// <param name="srcFilePath"></param>
        /// <param name="dstDir"></param>
        /// <param name="override"></param>
        /// <param name="encoding"></param>
        public void UnarchiveFile(string srcFilePath, string dstDir, bool @override, Encoding encoding)
        {
            var @params = new UnarchiveParams(dstDir, @override, encoding);
            UnarchiveFile(srcFilePath, @params);
        }

        /// <summary>
        /// Extract a single archive
        /// 解压单个归档文件
        /// </summary>
        /// <param name="srcFilePath"></param>
        /// <param name="params"></param>
        public void UnarchiveFile(string srcFilePath, UnarchiveParams @params)
        {
            if (!FileUtil.Exists(srcFilePath))
            {
                return;
            }

            if (!DirectoryUtil.Exists(@params.DstPath))
            {
                DirectoryUtil.MakeDirAll(@params.DstPath);
            }

            using (var source = ZipFile.Open(srcFilePath, ZipArchiveMode.Read, @params.Encoding))
            {
                var entries = source.Entries;
                if (null == entries) return;
                ResetTemp();
                foreach (var entry in entries)
                {
                    var filePath = PathUtil.CombinePath(@params.DstPath, entry.FullName);
                    if (FileUtil.Exists(filePath) && !@params.Override)
                    {
                        m_TempIgnoreList.Add(entry.FullName);
                        continue;
                    }

                    var fileDir = PathUtil.GetParentDirectory(filePath);
                    if (!DirectoryUtil.Exists(fileDir))
                    {
                        DirectoryUtil.MakeDirAll(fileDir);
                    }

                    entry.ExtractToFile(filePath, @params.Override);
                    DispatchEvent(UnarchiveEvents.EventUnarchiveEntry,
                        new UnarchiveEntryEventData(srcFilePath, entry.FullName));
                    m_TempEntryList.Add(entry.FullName);
                }

                DispatchEvent(UnarchiveEvents.EventUnarchive,
                    new UnarchiveEventData(srcFilePath, m_TempEntryList.ToArray(), m_TempIgnoreList.ToArray()));
            }
        }

        private void ResetTemp()
        {
            if (null == m_TempEntryList)
            {
                m_TempEntryList = new List<string>();
            }
            else
            {
                m_TempEntryList.Clear();
            }

            if (null == m_TempIgnoreList)
            {
                m_TempIgnoreList = new List<string>();
            }
            else
            {
                m_TempIgnoreList.Clear();
            }
        }

        /// <summary>
        /// Extract multiple archives using previously set parameters.
        /// 使用已设置的参数解压多个归档文件。
        /// </summary>
        /// <param name="srcFilePaths"></param>
        public void UnarchiveFiles(IEnumerable<string> srcFilePaths)
        {
            UnarchiveFiles(srcFilePaths, m_Params);
        }

        /// <summary>
        /// Extract multiple archives
        /// 解压多个归档文件
        /// </summary>
        /// <param name="srcFilePaths"></param>
        /// <param name="dstDir"></param>
        /// <param name="override"></param>
        /// <param name="encoding"></param>
        public void UnarchiveFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override,
            Encoding encoding)
        {
            var @params = new UnarchiveParams(dstDir, @override, encoding);
            UnarchiveFiles(srcFilePaths, @params);
        }

        /// <summary>
        /// Extract multiple archives
        /// 解压多个归档文件
        /// </summary>
        /// <param name="srcFilePaths"></param>
        /// <param name="params"></param>
        public void UnarchiveFiles(IEnumerable<string> srcFilePaths, UnarchiveParams @params)
        {
            if (null == srcFilePaths) return;
            foreach (var archiveFilePath in srcFilePaths)
            {
                UnarchiveFile(archiveFilePath, @params);
            }
        }
    }
}