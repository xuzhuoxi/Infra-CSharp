using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Archive
{
    public static class ArchiveUtil
    {
        /// <summary>
        /// Unpack a single archive
        /// 解压单个归档
        /// </summary>
        /// <param name="srcFilePath"></param>
        /// <param name="dstDir"></param>
        /// <param name="override"></param>
        /// <param name="encoding"></param>
        public static void UnzipFile(string srcFilePath, string dstDir, bool @override, Encoding encoding)
        {
            var @params = new UnarchiveParams(dstDir, @override, encoding);
            var unzip = new Unzip();
            unzip.SetUnarchiveParams(@params);
            unzip.UnarchiveFile(srcFilePath);
        }

        /// <summary>
        /// Unpack a single archive
        /// 解压单个归档
        /// </summary>
        /// <param name="srcFilePath"></param>
        /// <param name="dstDir"></param>
        public static void UnzipFile(string srcFilePath, string dstDir)
        {
            UnzipFile(srcFilePath, dstDir, true, Encoding.UTF8);
        }

        /// <summary>
        /// Unzip multiple archives
        /// 解压多个归档
        /// </summary>
        /// <param name="srcFilePaths"></param>
        /// <param name="dstDir"></param>
        /// <param name="override"></param>
        /// <param name="encoding"></param>
        public static void UnzipFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override,
            Encoding encoding)
        {
            var @params = new UnarchiveParams(dstDir, @override, encoding);
            var unzip = new Unzip();
            unzip.SetUnarchiveParams(@params);
            unzip.UnarchiveFiles(srcFilePaths);
        }

        /// <summary>
        /// Unzip multiple archives
        /// 解压多个归档
        /// </summary>
        /// <param name="srcFilePaths"></param>
        /// <param name="dstDir"></param>
        public static void UnzipFiles(IEnumerable<string> srcFilePaths, string dstDir)
        {
            UnzipFiles(srcFilePaths, dstDir, true, Encoding.UTF8);
        }
    }
}