using System.IO;

namespace JLGames.Infra.Utils
{
    public static class DirectoryUtil
    {
        /// <summary>
        /// Check if the directory exists
        /// 检测目录是否存在
        /// This parameter path is allowed to specify relative path or absolute path information.
        /// Relative path information will be interpreted relative to the current working directory.
        /// 允许此参数 path 指定相对路径或绝对路径信息。 相对路径信息将解释为相对于当前工作目录。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Exists(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        /// <summary>
        /// Check if the directory is empty
        /// 检测目录是否为空目录
        /// The current directory does not exist, return true
        /// 当前目录不存在，返回为true
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsEmpty(string path)
        {
            if (!Exists(path)) return true;
            var info = new DirectoryInfo(path);
            return info.GetFileSystemInfos().Length == 0;
        }

        /// <summary>
        /// Move directory
        /// 移动文件夹
        /// Note：Self-assurance path existence
        /// 注意：自行保证路径的存在性
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static void MoveDir(string oldPath, string newPath)
        {
            if (!Exists(oldPath)) return;

            var info = new DirectoryInfo(oldPath);
            info.MoveTo(newPath);
        }

        /// <summary>
        /// Copy directory
        /// 复制文件夹
        /// Note：Self-assurance path existence
        /// 注意：自行保证路径的存在性
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static void CopyDir(string oldPath, string newPath)
        {
            if (!Exists(oldPath)) return;
            if (!Exists(newPath))
            {
                MakeDirAll(newPath, false);
            }

            var filepaths = Directory.GetFiles(oldPath);
            foreach (var filepath in filepaths)
            {
                var fileName = Path.GetFileName(filepath);
                if (string.IsNullOrEmpty(fileName)) continue;
                var destpath = Path.Combine(newPath, fileName);
                File.Copy(filepath, destpath);
            }

            var folderpaths = Directory.GetDirectories(oldPath);
            foreach (var folderpath in folderpaths)
            {
                var folderName = Path.GetFileName(folderpath);
                if (string.IsNullOrEmpty(folderName)) continue;
                var destpath = Path.Combine(newPath, folderName);
                CopyDir(folderpath, destpath);
            }
        }

        /// <summary>
        /// Rename directory
        /// 文件夹重命名
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="newName"></param>
        public static void RenameDir(string dirPath, string newName)
        {
            if (!Exists(dirPath)) return;

            var info = new DirectoryInfo(dirPath);
            var newPath = $"{dirPath.Substring(0, dirPath.Length - info.Name.Length)}{newName}";
            info.MoveTo(newPath);
        }

        /// <summary>
        /// Delete directory
        /// 删除文件夹
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="force">强制</param>
        public static void DeleteDir(string dir, bool force)
        {
            if (!Exists(dir))
            {
                return;
            }

            FileUtil.DeleteAllFile(dir, true, force);

            var di = new FileInfo(dir);

            if (force)
            {
                if (FileUtil.IsReadOnly(di))
                {
                    di.Attributes = FileAttributes.Normal;
                }
            }

            Directory.Delete(dir, true);
        }

        /// <summary>
        /// Clear directory
        /// 清空文件夹
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="force"></param>
        public static void ClearDir(string dir, bool force)
        {
            if (!Directory.Exists(dir))
            {
                return;
            }

            var subList = Directory.GetFileSystemEntries(dir);
            if (subList.Length == 0) return;
            foreach (var path in subList)
            {
                if (File.Exists(path))
                {
                    FileUtil.DeleteFile(path, force);
                    continue;
                }

                if (Directory.Exists(path))
                {
                    DeleteDir(path, force);
                }
            }
        }

        /// <summary>
        /// Create a directory.
        /// 创建一个文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <param name="keepExist"></param>
        public static void MakeDir(string path, bool keepExist = true)
        {
            if (string.IsNullOrEmpty(path)) return;

            //文件夹存在则返回
            if (keepExist && Directory.Exists(path)) return;

            DeleteDir(path, true);
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Create a directory.
        /// 创建一个文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <param name="keepExist"></param>
        public static void MakeDirAll(string path, bool keepExist = true)
        {
            //文件夹存在则返回
            if (keepExist && Directory.Exists(path)) return;

            var formatedPath = PathUtil.Format2LinuxPath(path);
            var paths = formatedPath.Split('/');
            if (paths.Length == 0) return;

            var start = false;
            var dir = paths[0];
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                start = true;
                Directory.CreateDirectory(path);
            }

            for (var index = 1; index < paths.Length - 1; index++)
            {
                dir = $"{dir}/{paths[index]}";
                if (start || !string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                    start = true;
                }
            }

            dir = $"{dir}/{paths[paths.Length - 1]}";
            MakeDir(dir, keepExist);
        }

        /// <summary>
        /// Get a list of files in a directory, current directory only
        /// 取目录下文件列表, 仅限当前目录
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static string[] GetFiles(string folderPath, string searchPattern = "*")
        {
            if (string.IsNullOrEmpty(folderPath) || !Exists(folderPath)) return null;
            return Directory.GetFiles(folderPath, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Get a list of files in a directory, recursively
        /// 取目录下文件列表, 递归
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static string[] GetAllFiles(string folderPath, string searchPattern = "*")
        {
            if (string.IsNullOrEmpty(folderPath) || !Exists(folderPath)) return null;
            return Directory.GetFiles(folderPath, searchPattern, SearchOption.AllDirectories);
        }
    }
}