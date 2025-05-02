using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace JLGames.Infra.Utils
{
    /// <summary>
    /// DES util.
    /// Good efficiency
    /// 效率不错
    /// </summary>
    public static class DESUtil
    {
        //要求为8位
        private static readonly byte[] m_DefaultKey = Encoding.UTF8.GetBytes("abcdefgh".Substring(0, 8));
        private static readonly byte[] m_DefaultIv = {0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF};

        public static string Encrypted(string encryptString)
        {
            return Encrypted(encryptString, m_DefaultKey, m_DefaultIv);
        }

        public static string Encrypted(string encryptString, string encryptKey, string encryptIV)
        {
            var key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            var iv = Encoding.UTF8.GetBytes(encryptIV);
            return Encrypted(encryptString, key, iv);
        }

        /// <summary>
        /// DES encrypted string
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <param name="encryptIV"></param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string Encrypted(string encryptString, byte[] encryptKey, byte[] encryptIV)
        {
            try
            {
                var rgbKey = encryptKey;
                var rgbIV = encryptIV;
                var inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var dCSP = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream =
                    new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        public static string Decrypted(string decryptString)
        {
            return Decrypted(decryptString, m_DefaultKey, m_DefaultIv);
        }

        public static string Decrypted(string decryptString, string encryptKey, string encryptIV)
        {
            var key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            var iv = Encoding.UTF8.GetBytes(encryptIV);
            return Decrypted(decryptString, key, iv);
        }

        /// <summary>
        /// DES decrypted string
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <param name="decryptIV"></param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string Decrypted(string decryptString, byte[] decryptKey, byte[] decryptIV)
        {
            try
            {
                var rgbKey = decryptKey;
                var rgbIV = decryptIV;
                var inputByteArray = Convert.FromBase64String(decryptString);
                var dcsp = new DESCryptoServiceProvider();
                var mStream = new MemoryStream();
                var cStream =
                    new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
    }
}