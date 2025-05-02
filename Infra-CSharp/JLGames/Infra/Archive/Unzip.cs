using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using JLGames.Infra.Event;
using JLGames.Infra.Utils;

namespace JLGames.Infra.Archive
{
    public class Unzip : EventDispatcher, IUnarchive
    {
        private UnarchiveParams m_Params;
        private List<string> m_TempEntryList;
        private List<string> m_TempIgnoreList;

        public void SetUnarchiveParams(UnarchiveParams @params)
        {
            m_Params = @params;
        }

        public void SetDstPath(string dstPath)
        {
            if (null == m_Params)
            {
                m_Params = new UnarchiveParams(dstPath);
                return;
            }

            m_Params.SetDstPath(dstPath);
        }

        public void UnarchiveFile(string srcFilePath)
        {
            UnarchiveFile(srcFilePath, m_Params);
        }

        public void UnarchiveFile(string srcFilePath, string dstDir, bool @override, Encoding encoding)
        {
            var @params = new UnarchiveParams(dstDir, @override, encoding);
            UnarchiveFile(srcFilePath, @params);
        }

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

        public void UnarchiveFiles(IEnumerable<string> srcFilePaths)
        {
            UnarchiveFiles(srcFilePaths, m_Params);
        }

        public void UnarchiveFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override,
            Encoding encoding)
        {
            var @params = new UnarchiveParams(dstDir, @override, encoding);
            UnarchiveFiles(srcFilePaths, @params);
        }

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