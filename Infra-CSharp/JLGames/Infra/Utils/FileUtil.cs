using System.IO;

namespace JLGames.Infra.Utils
{
    public static class FileUtil
    {
        /// <summary>
        /// Check if file exists
        /// 检测文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Exists(string path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }

        /// <summary>
        /// Get file extension
        /// 取文件扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetExtension(string path)
        {
            if (string.IsNullOrEmpty(path)) return "";
            var index = path.LastIndexOf('.');
            return -1 == index ? "" : path.Substring(index + 1);
        }

        /// <summary>
        /// Move the file
        /// 移动文件
        /// Note：Self-assurance path existence
        /// 注意：自行保证路径的存在性
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static void MoveFile(string oldPath, string newPath)
        {
            if (!Exists(oldPath)) return;
            var info = new FileInfo(oldPath);
            info.MoveTo(newPath);
        }

        /// <summary>
        /// Copy the file
        /// 复制文件
        /// Note：Self-assurance path existence
        /// 注意：自行保证路径的存在性
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        /// <param name="overwrite"></param>
        public static void CopyFile(string srcPath, string destPath, bool overwrite = false)
        {
            if (!Exists(srcPath)) return;
            File.Copy(srcPath, destPath, overwrite);
        }

        /// <summary>
        /// Rename the file
        /// 文件重命名
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newName"></param>
        public static void RenameFile(string filePath, string newName)
        {
            if (!Exists(filePath)) return;
            var info = new FileInfo(filePath);
            var newPath = $"{filePath.Substring(0, filePath.Length - info.Name.Length)}{newName}";
            info.MoveTo(newPath);
        }

        /// <summary>
        /// Delete the file
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="force">是否强制</param>
        public static void DeleteFile(string filePath, bool force)
        {
            if (!Exists(filePath))
            {
                return;
            }

            var fileInfo = new FileInfo(filePath);

            if (force)
            {
                if (IsReadOnly(fileInfo))
                {
                    fileInfo.Attributes = FileAttributes.Normal;
                }
            }

            File.Delete(filePath);
        }

        /// <summary>
        /// Delete all files.
        /// 删除全部文件
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="recursive">是否递归</param>
        /// <param name="force">强制</param>
        public static void DeleteAllFile(string rootPath, bool recursive, bool force)
        {
            if (Exists(rootPath))
            {
                DeleteFile(rootPath, force);
                return;
            }

            if (!Directory.Exists(rootPath))
            {
                return;
            }

            var files = Directory.GetFiles(rootPath);
            if (files.Length > 0)
            {
                foreach (var filePath in files)
                {
                    DeleteFile(filePath, force);
                }
            }

            if (!recursive) return;

            var dirs = Directory.GetDirectories(rootPath);
            if (dirs.Length == 0) return;

            foreach (var path in dirs)
            {
                DeleteAllFile(path, true, force);
            }
        }

        public static bool IsReadOnly(FileInfo fileInfo)
        {
            return (fileInfo.Attributes & FileAttributes.ReadOnly) > 0;
        }
    }
}