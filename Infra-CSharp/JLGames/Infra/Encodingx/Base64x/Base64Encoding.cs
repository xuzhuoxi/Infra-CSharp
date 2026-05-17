namespace JLGames.Infra.Encodingx.Base64x
{
    /// <summary>
    /// Standard Base64 (RFC 4648): uses <c>+</c>/<c>/</c> and padding <c>=</c>.
    /// 标准 Base64（RFC 4648）：使用 <c>+</c>/<c>/</c> 及填充符 <c>=</c>。
    /// </summary>
    public sealed class Base64StdEncoding : IBase64Encoding
    {
        /// <inheritdoc/>
        public string EncodeToString(byte[] input)
        {
            return Base64Utils.EncodeToStdString(input);
        }

        /// <inheritdoc/>
        public string EncodeToString(string input)
        {
            return Base64Utils.EncodeToStdString(input);
        }

        /// <inheritdoc/>
        public byte[] EncodeToBytes(byte[] input)
        {
            return Base64Utils.EncodeToStdBytes(input);
        }

        /// <inheritdoc/>
        public byte[] EncodeToBytes(string input)
        {
            return Base64Utils.EncodeToStdBytes(input);
        }

        /// <inheritdoc/>
        public byte[] DecodeBytesFrom(byte[] input)
        {
            return Base64Utils.DecodeBytesFromStd(input);
        }

        /// <inheritdoc/>
        public byte[] DecodeBytesFrom(string input)
        {
            return Base64Utils.DecodeBytesFromStd(input);
        }

        /// <inheritdoc/>
        public string DecodeStringFrom(byte[] input)
        {
            return Base64Utils.DecodeStringFromStd(input);
        }

        /// <inheritdoc/>
        public string DecodeStringFrom(string input)
        {
            return Base64Utils.DecodeStringFromStd(input);
        }
    }

    /// <summary>
    /// Raw standard Base64: same alphabet as standard Base64, without padding <c>=</c>.
    /// 无填充标准 Base64：字母表与标准 Base64 相同，省略填充符 <c>=</c>。
    /// </summary>
    public sealed class Base64RawStdEncoding : IBase64Encoding
    {
        /// <inheritdoc/>
        public string EncodeToString(byte[] input)
        {
            return Base64Utils.EncodeToRawStdString(input);
        }

        /// <inheritdoc/>
        public string EncodeToString(string input)
        {
            return Base64Utils.EncodeToRawStdString(input);
        }

        /// <inheritdoc/>
        public byte[] EncodeToBytes(byte[] input)
        {
            return Base64Utils.EncodeStrToRawStdBytes(input);
        }

        /// <inheritdoc/>
        public byte[] EncodeToBytes(string input)
        {
            return Base64Utils.EncodeStrToRawStdBytes(input);
        }

        /// <inheritdoc/>
        public byte[] DecodeBytesFrom(byte[] input)
        {
            return Base64Utils.DecodeBytesFromRawStd(input);
        }

        /// <inheritdoc/>
        public byte[] DecodeBytesFrom(string input)
        {
            return Base64Utils.DecodeBytesFromRawStd(input);
        }

        /// <inheritdoc/>
        public string DecodeStringFrom(byte[] input)
        {
            return Base64Utils.DecodeStringFromRawStd(input);
        }

        /// <inheritdoc/>
        public string DecodeStringFrom(string input)
        {
            return Base64Utils.DecodeStringFromRawStd(input);
        }
    }

    /// <summary>
    /// URL-safe Base64: uses <c>-</c>/<c>_</c> instead of <c>+</c>/<c>/</c>, with padding <c>=</c>.
    /// URL 安全 Base64：以 <c>-</c>/<c>_</c> 替代 <c>+</c>/<c>/</c>，保留填充符 <c>=</c>。
    /// </summary>
    public sealed class Base64UrlEncoding : IBase64Encoding
    {
        /// <inheritdoc/>
        public string EncodeToString(byte[] input)
        {
            return Base64Utils.EncodeToUrlString(input);
        }

        /// <inheritdoc/>
        public string EncodeToString(string input)
        {
            return Base64Utils.EncodeToUrlString(input);
        }

        /// <inheritdoc/>
        public byte[] EncodeToBytes(byte[] input)
        {
            return Base64Utils.EncodeToUrlBytes(input);
        }

        /// <inheritdoc/>
        public byte[] EncodeToBytes(string input)
        {
            return Base64Utils.EncodeToUrlBytes(input);
        }

        /// <inheritdoc/>
        public byte[] DecodeBytesFrom(byte[] input)
        {
            return Base64Utils.DecodeBytesFromUrl(input);
        }

        /// <inheritdoc/>
        public byte[] DecodeBytesFrom(string input)
        {
            return Base64Utils.DecodeBytesFromUrl(input);
        }

        /// <inheritdoc/>
        public string DecodeStringFrom(byte[] input)
        {
            return Base64Utils.DecodeStringFromUrl(input);
        }

        /// <inheritdoc/>
        public string DecodeStringFrom(string input)
        {
            return Base64Utils.DecodeStringFromUrl(input);
        }
    }

    /// <summary>
    /// Raw URL-safe Base64: URL-safe alphabet without padding <c>=</c>.
    /// 无填充 URL 安全 Base64：使用 URL 安全字母表，省略填充符 <c>=</c>。
    /// </summary>
    public sealed class Base64RawUrlEncoding : IBase64Encoding
    {
        /// <inheritdoc/>
        public string EncodeToString(byte[] input)
        {
            return Base64Utils.EncodeToRawUrlString(input);
        }

        /// <inheritdoc/>
        public string EncodeToString(string input)
        {
            return Base64Utils.EncodeToRawUrlString(input);
        }

        /// <inheritdoc/>
        public byte[] EncodeToBytes(byte[] input)
        {
            return Base64Utils.EncodeToRawUrlBytes(input);
        }

        /// <inheritdoc/>
        public byte[] EncodeToBytes(string input)
        {
            return Base64Utils.EncodeToRawUrlBytes(input);
        }

        /// <inheritdoc/>
        public byte[] DecodeBytesFrom(byte[] input)
        {
            return Base64Utils.DecodeBytesFromRawUrl(input);
        }

        /// <inheritdoc/>
        public byte[] DecodeBytesFrom(string input)
        {
            return Base64Utils.DecodeBytesFromRawUrl(input);
        }

        /// <inheritdoc/>
        public string DecodeStringFrom(byte[] input)
        {
            return Base64Utils.DecodeStringFromRawUrl(input);
        }

        /// <inheritdoc/>
        public string DecodeStringFrom(string input)
        {
            return Base64Utils.DecodeStringFromRawUrl(input);
        }
    }
}
