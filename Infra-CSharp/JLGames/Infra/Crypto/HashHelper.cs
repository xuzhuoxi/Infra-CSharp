using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JLGames.Infra.Crypto
{
    public class HashHelper
    {
        /// <summary>
        /// MD5 哈希计算
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Md5(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public static string Md5String(string data)
        {
            return Md5(Encoding.UTF8.GetBytes(data));
        }

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
        /// SHA1 哈希计算
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Sha1(byte[] data)
        {
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public static string Sha1String(string data)
        {
            return Sha1(Encoding.UTF8.GetBytes(data));
        }

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
        /// 统一哈希方法
        /// </summary>
        /// <param name="hashAlgorithm"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Hash(HashAlgorithm hashAlgorithm, byte[] data)
        {
            return hashAlgorithm?.ComputeHash(data);
        }

        public static string Hash2Hex(HashAlgorithm hashAlgorithm, byte[] data)
        {
            var hash = Hash(hashAlgorithm, data);
            return hash != null ? BitConverter.ToString(hash).Replace("-", "").ToLower() : "";
        }

        public static byte[] HashString(HashAlgorithm hashAlgorithm, string data)
        {
            return Hash(hashAlgorithm, Encoding.UTF8.GetBytes(data));
        }

        public static string HashString2Hex(HashAlgorithm hashAlgorithm, string data)
        {
            return Hash2Hex(hashAlgorithm, Encoding.UTF8.GetBytes(data));
        }

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