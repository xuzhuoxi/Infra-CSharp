using System;
using System.Text;

namespace JLGames.Infra.Encodingx.Base64x
{
    /// <summary>
    /// Static helpers for standard, raw, URL-safe, and raw URL-safe Base64 variants.
    /// 标准、无填充、URL 安全及无填充 URL 安全等多种 Base64 变体的静态工具方法。
    /// </summary>
    public static class Base64Utils
    {
        // Std ----------

        /// <summary>
        /// Encode bytes with standard Base64 (RFC 4648).
        /// 按标准 Base64（RFC 4648）编码字节数组。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>Base64 string. Base64 字符串。</returns>
        public static string EncodeToStdString(byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        /// <summary>
        /// Encode UTF-8 text with standard Base64 (RFC 4648).
        /// 将 UTF-8 文本按标准 Base64（RFC 4648）编码。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>Base64 string. Base64 字符串。</returns>
        public static string EncodeToStdString(string input)
        {
            return EncodeToStdString(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Encode bytes with standard Base64 and return UTF-8 bytes of the result string.
        /// 按标准 Base64 编码字节数组，并返回结果字符串的 UTF-8 字节。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        public static byte[] EncodeToStdBytes(byte[] input)
        {
            return Encoding.UTF8.GetBytes(Convert.ToBase64String(input));
        }

        /// <summary>
        /// Encode UTF-8 text with standard Base64 and return UTF-8 bytes of the result string.
        /// 将 UTF-8 文本按标准 Base64 编码，并返回结果字符串的 UTF-8 字节。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        public static byte[] EncodeToStdBytes(string input)
        {
            return EncodeToStdBytes(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Decode a standard Base64 string to bytes.
        /// 将标准 Base64 字符串解码为字节数组。
        /// </summary>
        /// <param name="input">Base64 string. Base64 字符串。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
        public static byte[] DecodeBytesFromStd(string input)
        {
            return Convert.FromBase64String(input);
        }

        /// <summary>
        /// Decode UTF-8 bytes of a standard Base64 string to bytes.
        /// 将标准 Base64 字符串的 UTF-8 字节解码为字节数组。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
        public static byte[] DecodeBytesFromStd(byte[] input)
        {
            return Convert.FromBase64String(Encoding.UTF8.GetString(input));
        }

        /// <summary>
        /// Decode a standard Base64 string to UTF-8 text.
        /// 将标准 Base64 字符串解码为 UTF-8 文本。
        /// </summary>
        /// <param name="input">Base64 string. Base64 字符串。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        public static string DecodeStringFromStd(string input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromStd(input));
        }

        /// <summary>
        /// Decode UTF-8 bytes of a standard Base64 string to UTF-8 text.
        /// 将标准 Base64 字符串的 UTF-8 字节解码为 UTF-8 文本。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        public static string DecodeStringFromStd(byte[] input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromStd(input));
        }

        // RawStd ----------

        /// <summary>
        /// Encode with standard Base64 and strip padding <c>=</c>.
        /// 按标准 Base64 编码并去除填充符 <c>=</c>。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>Base64 string without padding. 无填充的 Base64 字符串。</returns>
        public static string EncodeToRawStdString(byte[] input)
        {
            return Convert.ToBase64String(input).TrimEnd('=');
        }

        /// <summary>
        /// Encode UTF-8 text with standard Base64 and strip padding <c>=</c>.
        /// 将 UTF-8 文本按标准 Base64 编码并去除填充符 <c>=</c>。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>Base64 string without padding. 无填充的 Base64 字符串。</returns>
        public static string EncodeToRawStdString(string input)
        {
            return EncodeToRawStdString(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Encode with standard Base64 (no padding) and return UTF-8 bytes of the result string.
        /// 按无填充标准 Base64 编码，并返回结果字符串的 UTF-8 字节。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        public static byte[] EncodeStrToRawStdBytes(byte[] input)
        {
            return Encoding.UTF8.GetBytes(EncodeToRawStdString(input));
        }

        /// <summary>
        /// Encode UTF-8 text with standard Base64 (no padding) and return UTF-8 bytes of the result string.
        /// 将 UTF-8 文本按无填充标准 Base64 编码，并返回结果字符串的 UTF-8 字节。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        public static byte[] EncodeStrToRawStdBytes(string input)
        {
            return Encoding.UTF8.GetBytes(EncodeToRawStdString(input));
        }

        /// <summary>
        /// Restore padding and decode raw standard Base64 to bytes.
        /// 补全填充符后，将无填充标准 Base64 解码为字节数组。
        /// </summary>
        /// <param name="input">Base64 string without padding. 无填充的 Base64 字符串。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
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
        /// Restore padding and decode UTF-8 bytes of raw standard Base64 to bytes.
        /// 补全填充符后，将无填充标准 Base64 的 UTF-8 字节解码为字节数组。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
        public static byte[] DecodeBytesFromRawStd(byte[] input)
        {
            return DecodeBytesFromRawStd(Encoding.UTF8.GetString(input));
        }

        /// <summary>
        /// Restore padding and decode raw standard Base64 to UTF-8 text.
        /// 补全填充符后，将无填充标准 Base64 解码为 UTF-8 文本。
        /// </summary>
        /// <param name="input">Base64 string without padding. 无填充的 Base64 字符串。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        public static string DecodeStringFromRawStd(string input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromRawStd(input));
        }

        /// <summary>
        /// Restore padding and decode UTF-8 bytes of raw standard Base64 to UTF-8 text.
        /// 补全填充符后，将无填充标准 Base64 的 UTF-8 字节解码为 UTF-8 文本。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        public static string DecodeStringFromRawStd(byte[] input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromRawStd(input));
        }

        // Url ----------

        /// <summary>
        /// Encode with standard Base64, then map to URL-safe characters (padding retained).
        /// 按标准 Base64 编码后映射为 URL 安全字符（保留填充符）。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>URL-safe Base64 string. URL 安全 Base64 字符串。</returns>
        public static string EncodeToUrlString(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_');
        }

        /// <summary>
        /// Encode UTF-8 text with URL-safe Base64 (padding retained).
        /// 将 UTF-8 文本按 URL 安全 Base64 编码（保留填充符）。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>URL-safe Base64 string. URL 安全 Base64 字符串。</returns>
        public static string EncodeToUrlString(string input)
        {
            return EncodeToUrlString(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Encode with URL-safe Base64 and return UTF-8 bytes of the result string.
        /// 按 URL 安全 Base64 编码，并返回结果字符串的 UTF-8 字节。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        public static byte[] EncodeToUrlBytes(byte[] input)
        {
            return Encoding.UTF8.GetBytes(EncodeToUrlString(input));
        }

        /// <summary>
        /// Encode UTF-8 text with URL-safe Base64 and return UTF-8 bytes of the result string.
        /// 将 UTF-8 文本按 URL 安全 Base64 编码，并返回结果字符串的 UTF-8 字节。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        public static byte[] EncodeToUrlBytes(string input)
        {
            return Encoding.UTF8.GetBytes(EncodeToUrlString(input));
        }

        /// <summary>
        /// Map URL-safe characters to standard alphabet, then decode Base64 to bytes.
        /// 将 URL 安全字符映射回标准字母表后，解码 Base64 为字节数组。
        /// </summary>
        /// <param name="input">URL-safe Base64 string. URL 安全 Base64 字符串。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
        public static byte[] DecodeBytesFromUrl(string input)
        {
            var base64 = input
                .Replace('-', '+')
                .Replace('_', '/');
            return Convert.FromBase64String(base64); // 自动处理填充
        }

        /// <summary>
        /// Map URL-safe characters to standard alphabet, then decode UTF-8 bytes of Base64 to bytes.
        /// 将 URL 安全字符映射回标准字母表后，将 Base64 的 UTF-8 字节解码为字节数组。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the URL-safe Base64 string. URL 安全 Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
        public static byte[] DecodeBytesFromUrl(byte[] input)
        {
            return DecodeBytesFromUrl(Encoding.UTF8.GetString(input));
        }

        /// <summary>
        /// Map URL-safe characters to standard alphabet, then decode Base64 to UTF-8 text.
        /// 将 URL 安全字符映射回标准字母表后，解码 Base64 为 UTF-8 文本。
        /// </summary>
        /// <param name="input">URL-safe Base64 string. URL 安全 Base64 字符串。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        public static string DecodeStringFromUrl(string input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromUrl(input)); // 自动处理填充
        }

        /// <summary>
        /// Map URL-safe characters to standard alphabet, then decode UTF-8 bytes of Base64 to UTF-8 text.
        /// 将 URL 安全字符映射回标准字母表后，将 Base64 的 UTF-8 字节解码为 UTF-8 文本。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the URL-safe Base64 string. URL 安全 Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        public static string DecodeStringFromUrl(byte[] input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromUrl(input)); // 自动处理填充
        }

        // RawUrl ----------

        /// <summary>
        /// Encode with standard Base64, strip padding, then map to URL-safe characters.
        /// 按标准 Base64 编码、去除填充符后映射为 URL 安全字符。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>URL-safe Base64 string without padding. 无填充的 URL 安全 Base64 字符串。</returns>
        public static string EncodeToRawUrlString(byte[] input)
        {
            return Convert.ToBase64String(input)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        /// <summary>
        /// Encode UTF-8 text with raw URL-safe Base64 (no padding).
        /// 将 UTF-8 文本按无填充 URL 安全 Base64 编码。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>URL-safe Base64 string without padding. 无填充的 URL 安全 Base64 字符串。</returns>
        public static string EncodeToRawUrlString(string input)
        {
            return EncodeToRawUrlString(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Encode with raw URL-safe Base64 and return UTF-8 bytes of the result string.
        /// 按无填充 URL 安全 Base64 编码，并返回结果字符串的 UTF-8 字节。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        public static byte[] EncodeToRawUrlBytes(byte[] input)
        {
            return Encoding.UTF8.GetBytes(EncodeToRawUrlString(input));
        }

        /// <summary>
        /// Encode UTF-8 text with raw URL-safe Base64 and return UTF-8 bytes of the result string.
        /// 将 UTF-8 文本按无填充 URL 安全 Base64 编码，并返回结果字符串的 UTF-8 字节。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        public static byte[] EncodeToRawUrlBytes(string input)
        {
            return Encoding.UTF8.GetBytes(EncodeToRawUrlString(input));
        }

        /// <summary>
        /// Map URL-safe characters to standard alphabet, restore padding, then decode to bytes.
        /// 将 URL 安全字符映射回标准字母表、补全填充符后解码为字节数组。
        /// </summary>
        /// <param name="input">URL-safe Base64 string without padding. 无填充的 URL 安全 Base64 字符串。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
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
        /// Map URL-safe characters to standard alphabet, restore padding, then decode UTF-8 bytes to bytes.
        /// 将 URL 安全字符映射回标准字母表、补全填充符后，将 Base64 的 UTF-8 字节解码为字节数组。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the URL-safe Base64 string. URL 安全 Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
        public static byte[] DecodeBytesFromRawUrl(byte[] input)
        {
            return DecodeBytesFromRawUrl(Encoding.UTF8.GetString(input));
        }

        /// <summary>
        /// Map URL-safe characters to standard alphabet, restore padding, then decode to UTF-8 text.
        /// 将 URL 安全字符映射回标准字母表、补全填充符后解码为 UTF-8 文本。
        /// </summary>
        /// <param name="input">URL-safe Base64 string without padding. 无填充的 URL 安全 Base64 字符串。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        public static string DecodeStringFromRawUrl(string input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromRawUrl(input));
        }

        /// <summary>
        /// Map URL-safe characters to standard alphabet, restore padding, then decode UTF-8 bytes to UTF-8 text.
        /// 将 URL 安全字符映射回标准字母表、补全填充符后，将 Base64 的 UTF-8 字节解码为 UTF-8 文本。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the URL-safe Base64 string. URL 安全 Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        public static string DecodeStringFromRawUrl(byte[] input)
        {
            return Encoding.UTF8.GetString(DecodeBytesFromRawUrl(input));
        }
    }
}
