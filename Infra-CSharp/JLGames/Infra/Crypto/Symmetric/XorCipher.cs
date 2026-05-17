namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// 循环密钥异或实现，加解密为同一运算。
    /// </summary>
    public class XorCipher : IXorCipher
    {
        /// <summary>异或密钥字节</summary>
        public byte[] Key { get; private set; }
        /// <summary>密钥长度；为 0 时加解密原样返回输入</summary>
        public int KeyLen { get; private set; }

        /// <summary>
        /// 使用指定密钥创建实例。
        /// </summary>
        /// <param name="key">异或密钥，可为 <c>null</c>（等价于空密钥）</param>
        public XorCipher(byte[] key)
        {
            Key = key;
            KeyLen = key?.Length ?? 0;
        }

        /// <inheritdoc/>
        public byte[] Encrypt(byte[] plaintext)
        {
            if (KeyLen == 0) return plaintext;
            return InnerXor(plaintext);
        }

        /// <inheritdoc/>
        public byte[] Decrypt(byte[] ciphertext)
        {
            if (KeyLen == 0) return ciphertext;
            return InnerXor(ciphertext);
        }

        private byte[] InnerXor(byte[] text)
        {
            var result = new byte[text.Length];
            var keyIndex = 0;
            for (var index = 0; index < text.Length; index++)
            {
                result[index] = (byte)(text[index] ^ Key[keyIndex]);
                keyIndex++;
                if (keyIndex == KeyLen) keyIndex = 0;
            }

            return result;
        }
    }
}