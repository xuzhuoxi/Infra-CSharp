namespace JLGames.Infra.Crypto.Symmetric
{
    public class XorCipher : IXorCipher
    {
        public byte[] Key { get; private set; }
        public int KeyLen { get; private set; }

        public XorCipher(byte[] key)
        {
            Key = key;
            KeyLen = key?.Length ?? 0;
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            if (KeyLen == 0) return plaintext;
            return InnerXor(plaintext);
        }

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