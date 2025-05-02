using System.Security.Cryptography;
using System.Text;
using System;

namespace JLGames.Infra.Utils
{
    public static class ConfusedUtil
    {
        public static string Md5(byte[] dataByte)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var bytes_md5_out = md5.ComputeHash(dataByte);
            return InnerTo16String(bytes_md5_out).PadLeft(32, '0');
        }

        public static string Md5(string dataStr)
        {
            var bytes_md5_in = UTF8Encoding.Default.GetBytes(dataStr);
            return Md5(bytes_md5_in);
        }

        public static string Sha1(byte[] dataByte)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            var bytes_sha1_out = sha1.ComputeHash(dataByte);
            return InnerTo16String(bytes_sha1_out);
        }

        public static string Sha1(string dataStr)
        {
            var bytes_sha1_in = UTF8Encoding.Default.GetBytes(dataStr);
            return Sha1(bytes_sha1_in);
        }

        private static string InnerTo16String(byte[] data)
        {
            var outStr = "";
            foreach (var b in data)
            {
                outStr += Convert.ToString(b, 16).PadLeft(2, '0');
            }

            return outStr;
        }
    }
}