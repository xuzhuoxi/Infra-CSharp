using System.Collections.Generic;
using System.Text;
using JLGames.Infra.Event;

namespace JLGames.Infra.Archive
{
    public interface IUnarchive : IEventDispatcher
    {
        /// <summary>
        /// Set parameters
        /// 设置参数
        /// </summary>
        /// <param name="params"></param>
        void SetUnarchiveParams(UnarchiveParams @params);

        /// <summary>
        /// Set the unxip storage directory path
        /// 设置解压存储目录路径
        /// </summary>
        /// <param name="dstPath"></param>
        void SetDstPath(string dstPath);

        /// <summary>
        /// Extract a single archive
        /// 解压单个归档文件
        /// </summary>
        /// <param name="srcFilePath"></param>
        void UnarchiveFile(string srcFilePath);

        /// <summary>
        /// Extract a single archive
        /// 解压单个归档文件
        /// </summary>
        /// <param name="srcFilePath"></param>
        /// <param name="dstDir"></param>
        /// <param name="override"></param>
        /// <param name="encoding"></param>
        void UnarchiveFile(string srcFilePath, string dstDir, bool @override, Encoding encoding);

        /// <summary>
        /// Extract a single archive
        /// 解压单个归档文件
        /// </summary>
        /// <param name="srcFilePath"></param>
        /// <param name="params"></param>
        void UnarchiveFile(string srcFilePath, UnarchiveParams @params);

        /// <summary>
        /// Extract multiple archives
        /// 解压多个归档文件
        /// </summary>
        /// <param name="srcFilePaths"></param>
        void UnarchiveFiles(IEnumerable<string> srcFilePaths);

        /// <summary>
        /// Extract multiple archives
        /// 解压多个归档文件
        /// </summary>
        /// <param name="srcFilePaths"></param>
        /// <param name="dstDir"></param>
        /// <param name="override"></param>
        /// <param name="encoding"></param>
        void UnarchiveFiles(IEnumerable<string> srcFilePaths, string dstDir, bool @override, Encoding encoding);

        /// <summary>
        /// Extract multiple archives
        /// 解压多个归档文件
        /// </summary>
        /// <param name="srcFilePaths"></param>
        /// <param name="params"></param>
        void UnarchiveFiles(IEnumerable<string> srcFilePaths, UnarchiveParams @params);
    }
}