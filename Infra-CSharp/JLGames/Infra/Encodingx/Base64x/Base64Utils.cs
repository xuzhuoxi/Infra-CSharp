using System;
using System.Text;

namespace JLGames.Infra.Encodingx.Base64x
{
    public static class Base64Utils
    {
        // Std ----------

        /// <summary>
        /// 按标准Base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeToStdString(byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        /// <summary>
        /// 按标准Base64编码
        /// 先把字符串按UTF8编码处理为字符数组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeToStdString(string input)
        {
            return EncodeToStdString(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// 按标准Base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] EncodeToStdBytes(byte[] input)
        {
            return Encoding.UTF8.GetBytes(Convert.ToBase64String(input));
        }

        /// <summary>
        /// 按标准Base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] EncodeToStdBytes(string input)
        {
            return EncodeToStdBytes(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] DecodeBytesFromStd(string input)
        {
            return Convert.FromBase64String(input);
        }

        /// <summary>
        /// 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] DecodeBytesFromStd(byte[] input)
        {
            return Convert.FromBase64String(Encoding.UTF8.GetString(input));
        }

        /// <summary>
        /// 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecodeStringFromStd(string input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromStd(input));
        }

        /// <summary>
        /// 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecodeStringFromStd(byte[] input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromStd(input));
        }

        // RawStd ----------

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 删除填充
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeToRawStdString(byte[] input)
        {
            return Convert.ToBase64String(input).TrimEnd('=');
        }

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 删除填充
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeToRawStdString(string input)
        {
            return EncodeToRawStdString(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 删除填充
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] EncodeStrToRawStdBytes(byte[] input)
        {
            return Encoding.UTF8.GetBytes(EncodeToRawStdString(input));
        }

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 删除填充
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] EncodeStrToRawStdBytes(string input)
        {
            return Encoding.UTF8.GetBytes(EncodeToRawStdString(input));
        }

        /// <summary>
        /// 1. 补充填充
        /// 2. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] DecodeBytesFromRawStd(string input)
        {
            var base64 = input;
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }

        /// <summary>
        /// 1. 补充填充
        /// 2. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] DecodeBytesFromRawStd(byte[] input)
        {
            return DecodeBytesFromRawStd(Encoding.UTF8.GetString(input));
        }

        /// <summary>
        /// 1. 补充填充
        /// 2. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecodeStringFromRawStd(string input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromRawStd(input));
        }

        /// <summary>
        /// 1. 补充填充
        /// 2. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecodeStringFromRawStd(byte[] input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromRawStd(input));
        }

        // Url ----------

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 替换为 URL 安全字符（保留填充）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeToUrlString(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_');
        }

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 替换为 URL 安全字符（保留填充）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeToUrlString(string input)
        {
            return EncodeToUrlString(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 替换为 URL 安全字符（保留填充）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] EncodeToUrlBytes(byte[] input)
        {
            return Encoding.UTF8.GetBytes(EncodeToUrlString(input));
        }

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 替换为 URL 安全字符（保留填充）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] EncodeToUrlBytes(string input)
        {
            return Encoding.UTF8.GetBytes(EncodeToUrlString(input));
        }

        /// <summary>
        /// 1. 把 URL安全字符 转换为 标准字符
        /// 2. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] DecodeBytesFromUrl(string input)
        {
            var base64 = input
                .Replace('-', '+')
                .Replace('_', '/');
            return Convert.FromBase64String(base64); // 自动处理填充
        }

        /// <summary>
        /// 1. 把 URL安全字符 转换为 标准字符
        /// 2. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] DecodeBytesFromUrl(byte[] input)
        {
            return DecodeBytesFromUrl(Encoding.UTF8.GetString(input));
        }

        /// <summary>
        /// 1. 把 URL安全字符 转换为 标准字符
        /// 2. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecodeStringFromUrl(string input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromUrl(input)); // 自动处理填充
        }

        /// <summary>
        /// 1. 把 URL安全字符 转换为 标准字符
        /// 2. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecodeStringFromUrl(byte[] input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromUrl(input)); // 自动处理填充
        }

        // RawUrl ----------

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 删除填充
        /// 3. 替换为 URL 安全字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeToRawUrlString(byte[] input)
        {
            return Convert.ToBase64String(input)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 删除填充
        /// 3. 替换为 URL 安全字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeToRawUrlString(string input)
        {
            return EncodeToRawUrlString(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 删除填充
        /// 3. 替换为 URL 安全字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] EncodeToRawUrlBytes(byte[] input)
        {
            return Encoding.UTF8.GetBytes(EncodeToRawUrlString(input));
        }

        /// <summary>
        /// 1. 按标准Base64编码
        /// 2. 删除填充
        /// 3. 替换为 URL 安全字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] EncodeToRawUrlBytes(string input)
        {
            return Encoding.UTF8.GetBytes(EncodeToRawUrlString(input));
        }

        /// <summary>
        /// 1. 把 URL安全字符 转换为 标准字符
        /// 2. 补充填充
        /// 3. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] DecodeBytesFromRawUrl(string input)
        {
            var base64 = input
                .Replace('-', '+')
                .Replace('_', '/');

            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }

        /// <summary>
        /// 1. 把 URL安全字符 转换为 标准字符
        /// 2. 补充填充
        /// 3. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] DecodeBytesFromRawUrl(byte[] input)
        {
            return DecodeBytesFromRawUrl(Encoding.UTF8.GetString(input));
        }

        /// <summary>
        /// 1. 把 URL安全字符 转换为 标准字符
        /// 2. 补充填充
        /// 3. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecodeStringFromRawUrl(string input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromRawUrl(input));
        }

        /// <summary>
        /// 1. 把 URL安全字符 转换为 标准字符
        /// 2. 补充填充
        /// 3. 按标准Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecodeStringFromRawUrl(byte[] input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromRawUrl(input));
        }
    }
}