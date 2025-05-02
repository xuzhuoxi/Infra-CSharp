using System;
using System.IO;

namespace JLGames.Infra.Utils
{
    public static class PathUtil
    {
        private const string WinPathSep = "\\";
        private const char WinPathSepChar = '\\';
        private const string LinuxPathSep = "/";
        private const char LinuxPathSepChar = '/';

        /// <summary>
        /// Is it an absolute path
        /// 是否为绝对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsAbsPath(string path)
        {
            return Path.IsPathRooted(path);
        }

        /// <summary>
        /// Format to Linux path format
        /// 格式化为Linux路径格式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Format2LinuxPath(string path)
        {
            return path.Replace(WinPathSep, LinuxPathSep);
        }

        /// <summary>
        /// Format to Windows path format
        /// 格式化为Windows路径格式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Format2WindowsPath(string path)
        {
            return path.Replace(LinuxPathSep, WinPathSep);
        }

        /// <summary>
        /// Combine paths and convert to Linux path format
        /// 合并路径并转为Linux路径格式
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="path"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string CombineLinuxPath(string basePath, string path, params string[] paths)
        {
            var rs = CombinePath(basePath, path, paths);
            return Format2LinuxPath(rs);
        }

        /// <summary>
        /// Combine paths and convert to Windows path format
        /// 合并路径并转为Windows路径格式
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="path"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string CombineWindowsPath(string basePath, string path, params string[] paths)
        {
            var rs = CombinePath(basePath, path, paths);
            return Format2WindowsPath(rs);
        }

        /// <summary>
        /// Combine paths
        /// 合并路径
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="path"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string CombinePath(string basePath, string path, params string[] paths)
        {
            path = ClearAbsPattern(path);
            var rs = Path.Combine(basePath, path);
            if (paths == null || paths.Length == 0)
            {
                return rs;
            }

            foreach (var p in paths)
            {
                rs = Path.Combine(rs, ClearAbsPattern(p));
            }

            return rs;
        }

        /// <summary>
        /// Take the parent directory
        /// 取上一级目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Linux格式的路径</returns>
        public static string GetParentDirectory(string path)
        {
            path = Format2LinuxPath(path);
            path = path.Substring(0, path.LastIndexOf(LinuxPathSepChar));
            return path;
        }

        /// <summary>
        /// Take the current file name or current directory name under the path
        /// 取路径下的 当前文件名 或 当前目录名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            var formated = Format2LinuxPath(path);
            var index = formated.LastIndexOf(LinuxPathSepChar);
            return index < 0 ? formated : formated.Substring(index);
        }

        /// <summary>
        /// Clear file path extension
        /// 清除文件路径的扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ClearExtension(string path)
        {
            return Path.ChangeExtension(path, null);
        }

        /// <summary>
        /// Compare two paths, often used for path ordering
        /// 比较两条路径，常用于路径排序
        /// Comparison condition: levels > characters
        /// 比较条件：层级数 > 字符
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static int ComparePath(string path1, string path2)
        {
            var count1 = GetPathLevel(path1);
            var count2 = GetPathLevel(path2);
            if (count1 != count2)
                return count1 < count2 ? -1 : 1;

            return string.Compare(path1, path2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Get the number of levels of the path
        /// 获取路径的层级数量
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int GetPathLevel(string path)
        {
            var count = 0;
            for (var index = 0; index < path.Length; index++)
            {
                if (path[index] == '/') count += 1;
            }

            return count;
        }

        /// <summary>
        /// Note: If the second parameter of the Path.Combine function or later is an absolute path, the first part of the path will be discarded.
        /// 注意：Path.Combine函数第二参数或以后如果是绝对路径，则会把前部分路径丢弃。
        /// </summary>
        /// <param name="pathPattern"></param>
        /// <returns></returns>
        private static string ClearAbsPattern(string pathPattern)
        {
            while (pathPattern.StartsWith(LinuxPathSep) || pathPattern.StartsWith(WinPathSep))
            {
                pathPattern = pathPattern.Substring(1);
            }

            return pathPattern;
        }
    }
}