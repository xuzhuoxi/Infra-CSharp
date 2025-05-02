using System.IO;
using System.Text;

namespace JLGames.Infra.Utils
{
    public static class TextUtil
    {
        /// <summary>
        /// Read character file content
        /// 读取字符文件内容
        /// </summary>
        /// <returns>The text.</returns>
        /// <param name="filePath">File path.</param>
        public static string ReadText(string filePath)
        {
            return File.ReadAllText(filePath, Encoding.UTF8);
        }

        /// <summary>
        /// Read character file content
        /// 读取字符文件内容
        /// </summary>
        /// <returns>The text.</returns>
        /// <param name="filePath">File path.</param>
        /// <param name="encoding"></param>
        public static string ReadText(string filePath, Encoding encoding)
        {
            return File.ReadAllText(filePath, encoding);
        }

        /// <summary>
        /// Create or open a text file and overwrite the contents
        /// 创建或打开一个文本文件，并覆盖内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="text">文件内容</param>
        public static void CreateFileWithText(string filePath, string text)
        {
            File.WriteAllText(filePath, text, Encoding.UTF8);
        }

        /// <summary>
        /// Create or open a text file and overwrite the contents
        /// 创建或打开一个文本文件，并覆盖内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="text">文件内容</param>
        /// <param name="encoding"></param>
        public static void CreateFileWithText(string filePath, string text, Encoding encoding)
        {
            File.WriteAllText(filePath, text, encoding);
        }

        /// <summary>
        /// Create or open a text file and append the contents
        /// 创建或打开一个文本文件，并追加内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="text">文件内容</param>
        public static void AppendTextToFile(string filePath, string text)
        {
            File.AppendAllText(filePath, text, Encoding.UTF8);
        }

        /// <summary>
        /// Create or open a text file and append the contents
        /// 创建或打开一个文本文件，并追加内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="text">文件内容</param>
        /// <param name="encoding"></param>
        public static void AppendTextToFile(string filePath, string text, Encoding encoding)
        {
            File.AppendAllText(filePath, text, encoding);
        }
    }
}