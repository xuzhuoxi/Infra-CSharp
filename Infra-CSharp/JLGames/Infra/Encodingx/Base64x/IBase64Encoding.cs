namespace JLGames.Infra.Encodingx.Base64x
{
    /// <summary>
    /// Base64 encoding/decoding abstraction; variant behavior is defined by the implementation.
    /// Base64 编解码抽象；具体变体规则由实现类决定。
    /// </summary>
    public interface IBase64Encoding
    {
        /// <summary>
        /// Encode binary data to a Base64 string.
        /// 将二进制数据编码为 Base64 字符串。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>Base64 string. Base64 字符串。</returns>
        string EncodeToString(byte[] input);

        /// <summary>
        /// Encode UTF-8 text to a Base64 string.
        /// 将 UTF-8 文本编码为 Base64 字符串。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>Base64 string. Base64 字符串。</returns>
        string EncodeToString(string input);

        /// <summary>
        /// Encode binary data to UTF-8 bytes of the Base64 string.
        /// 将二进制数据编码为 Base64 字符串的 UTF-8 字节形式。
        /// </summary>
        /// <param name="input">Bytes to encode. 待编码的字节数组。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        byte[] EncodeToBytes(byte[] input);

        /// <summary>
        /// Encode UTF-8 text to UTF-8 bytes of the Base64 string.
        /// 将 UTF-8 文本编码为 Base64 字符串的 UTF-8 字节形式。
        /// </summary>
        /// <param name="input">Text to encode. 待编码的文本。</param>
        /// <returns>UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</returns>
        byte[] EncodeToBytes(string input);

        /// <summary>
        /// Decode UTF-8 bytes of a Base64 string to binary data.
        /// 将 Base64 字符串的 UTF-8 字节解码为二进制数据。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
        byte[] DecodeBytesFrom(byte[] input);

        /// <summary>
        /// Decode a Base64 string to binary data.
        /// 将 Base64 字符串解码为二进制数据。
        /// </summary>
        /// <param name="input">Base64 string. Base64 字符串。</param>
        /// <returns>Decoded bytes. 解码后的字节数组。</returns>
        byte[] DecodeBytesFrom(string input);

        /// <summary>
        /// Decode UTF-8 bytes of a Base64 string to UTF-8 text.
        /// 将 Base64 字符串的 UTF-8 字节解码为 UTF-8 文本。
        /// </summary>
        /// <param name="input">UTF-8 bytes of the Base64 string. Base64 字符串的 UTF-8 字节。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        string DecodeStringFrom(byte[] input);

        /// <summary>
        /// Decode a Base64 string to UTF-8 text.
        /// 将 Base64 字符串解码为 UTF-8 文本。
        /// </summary>
        /// <param name="input">Base64 string. Base64 字符串。</param>
        /// <returns>Decoded UTF-8 text. 解码后的 UTF-8 文本。</returns>
        string DecodeStringFrom(string input);
    }
}
