using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JLGames.Infra.Crypto
{
    /// <summary>
    /// Convenience wrappers for common hash algorithms (hex or raw bytes).
    /// 常用哈希算法的便捷封装
    /// </summary>
    public class HashHelper
    {
        /// <summary>
        /// 对字节数组计算 MD5，返回小写十六进制字符串（无分隔符）。
        /// </summary>
        /// <param name="data">待哈希数据</param>
        /// <returns>32 位十六进制 MD5 摘要</returns>
        public static string Md5(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 对 UTF-8 字符串计算 MD5，返回小写十六进制字符串。
        /// </summary>
        /// <param name="data">待哈希文本</param>
        /// <returns>32 位十六进制 MD5 摘要</returns>
        public static string Md5String(string data)
        {
            return Md5(Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 读取文件全文并计算 MD5；读取失败时返回空字符串。
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>MD5 十六进制摘要，失败时为 <see cref="string.Empty"/></returns>
        public static string Md5File(string filePath)
        {
            try
            {
                var data = File.ReadAllBytes(filePath);
                return Md5(data);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 对字节数组计算 SHA-1，返回小写十六进制字符串（无分隔符）。
        /// </summary>
        /// <param name="data">待哈希数据</param>
        /// <returns>40 位十六进制 SHA-1 摘要</returns>
        public static string Sha1(byte[] data)
        {
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 对 UTF-8 字符串计算 SHA-1，返回小写十六进制字符串。
        /// </summary>
        /// <param name="data">待哈希文本</param>
        /// <returns>40 位十六进制 SHA-1 摘要</returns>
        public static string Sha1String(string data)
        {
            return Sha1(Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 读取文件全文并计算 SHA-1；读取失败时返回空字符串。
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>SHA-1 十六进制摘要，失败时为 <see cref="string.Empty"/></returns>
        public static string Sha1File(string filePath)
        {
            try
            {
                var data = File.ReadAllBytes(filePath);
                return Sha1(data);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 使用指定 <see cref="HashAlgorithm"/> 实例计算哈希。
        /// </summary>
        /// <param name="hashAlgorithm">哈希算法实例；为 <c>null</c> 时返回 <c>null</c></param>
        /// <param name="data">待哈希数据</param>
        /// <returns>原始哈希字节；算法为 <c>null</c> 时返回 <c>null</c></returns>
        public static byte[] Hash(HashAlgorithm hashAlgorithm, byte[] data)
        {
            return hashAlgorithm?.ComputeHash(data);
        }

        /// <summary>
        /// 计算哈希并格式化为小写十六进制字符串。
        /// </summary>
        /// <param name="hashAlgorithm">哈希算法实例</param>
        /// <param name="data">待哈希数据</param>
        /// <returns>十六进制摘要；失败或算法为 <c>null</c> 时返回 <see cref="string.Empty"/></returns>
        public static string Hash2Hex(HashAlgorithm hashAlgorithm, byte[] data)
        {
            var hash = Hash(hashAlgorithm, data);
            return hash != null ? BitConverter.ToString(hash).Replace("-", "").ToLower() : "";
        }

        /// <summary>
        /// 对 UTF-8 字符串计算哈希。
        /// </summary>
        /// <param name="hashAlgorithm">哈希算法实例</param>
        /// <param name="data">待哈希文本</param>
        /// <returns>原始哈希字节</returns>
        public static byte[] HashString(HashAlgorithm hashAlgorithm, string data)
        {
            return Hash(hashAlgorithm, Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 对 UTF-8 字符串计算哈希并格式化为小写十六进制字符串。
        /// </summary>
        /// <param name="hashAlgorithm">哈希算法实例</param>
        /// <param name="data">待哈希文本</param>
        /// <returns>十六进制摘要</returns>
        public static string HashString2Hex(HashAlgorithm hashAlgorithm, string data)
        {
            return Hash2Hex(hashAlgorithm, Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 读取文件全文并计算哈希；读取失败时返回 <c>null</c>。
        /// </summary>
        /// <param name="hashAlgorithm">哈希算法实例</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>原始哈希字节；失败时为 <c>null</c></returns>
        public static byte[] HashFile(HashAlgorithm hashAlgorithm, string filePath)
        {
            try
            {
                var data = File.ReadAllBytes(filePath);
                return Hash(hashAlgorithm, data);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 读取文件全文并计算哈希，格式化为小写十六进制字符串；读取失败时返回空字符串。
        /// </summary>
        /// <param name="hashAlgorithm">哈希算法实例</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>十六进制摘要；失败时为 <see cref="string.Empty"/></returns>
        public static string HashFile2Hex(HashAlgorithm hashAlgorithm, string filePath)
        {
            try
            {
                var data = File.ReadAllBytes(filePath);
                return Hash2Hex(hashAlgorithm, data);
            }
            catch
            {
                return "";
            }
        }
    }
}