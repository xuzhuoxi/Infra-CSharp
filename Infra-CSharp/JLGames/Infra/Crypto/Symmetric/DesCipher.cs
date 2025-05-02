using System;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// DES 加密类
    /// </summary>
    public class DesCipher : IDesCipher
    {
        protected byte[] m_Key;
        protected PaddingMode m_PaddingMode;
        protected SymmetricAlgorithm m_Des;
        public int KeySize => DesDefines.KeySize;
        public virtual int BlockSize => DesDefines.BlockSize;

        protected DesCipher()
        {
        }

        public DesCipher(byte[] key)
        {
            if (null == key) throw new ArgumentException("Key must not be Null.");
            var keyLen = key.Length;
            switch (keyLen)
            {
                case DesDefines.KeySize:
                    m_Key = key;
                    m_Des = DES.Create();
                    m_Des.Key = key;
                    break;
                case DesDefines.KeySize * 2:
                    m_Key = new byte[24];
                    System.Buffer.BlockCopy(key, 0, m_Key, 0, 16);
                    System.Buffer.BlockCopy(key, 0, m_Key, 16, 8);
                    m_Des = TripleDES.Create();
                    m_Des.Key = m_Key;
                    break;
                case DesDefines.TripleKeySize:
                    m_Key = key;
                    m_Des = TripleDES.Create();
                    m_Des.Key = m_Key;
                    break;
                default:
                    throw new ArgumentException("Key must be 8 (DES) or 16|24 (3DES) bytes");
            }

            m_PaddingMode = PaddingMode.PKCS7;
        }


        /// <summary>
        /// 设置填充方式
        /// </summary>
        public void SetPaddingMode(PaddingMode paddingMode)
        {
            m_PaddingMode = paddingMode;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文数据</param>
        /// <returns>加密后的数据</returns>
        public byte[] Encrypt(byte[] plaintext) => EncryptCbc(plaintext);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">密文数据</param>
        /// <returns>解密后的数据</returns>
        public byte[] Decrypt(byte[] ciphertext) => DecryptCbc(ciphertext);

        public byte[] EncryptMode(byte[] plaintext, BlockMode blockMode)
        {
            switch (blockMode)
            {
                case BlockMode.ECB: return EncryptEcb(plaintext);
                case BlockMode.CBC: return EncryptCbc(plaintext);
                case BlockMode.CTR: return EncryptCtr(plaintext);
                default: throw new Exception("Unsupported DES block mode!");
            }
        }

        public byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode)
        {
            switch (blockMode)
            {
                case BlockMode.ECB: return EncryptEcb(plaintext);
                case BlockMode.CBC: return EncryptCbc(plaintext, iv);
                case BlockMode.CTR: return EncryptCtr(plaintext, iv);
                default: throw new Exception("Unsupported DES block mode!");
            }
        }

        public byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode)
        {
            switch (blockMode)
            {
                case BlockMode.ECB: return DecryptEcb(ciphertext);
                case BlockMode.CBC: return DecryptCbc(ciphertext);
                case BlockMode.CTR: return DecryptCtr(ciphertext);
                default: throw new Exception("Unsupported DES block mode!");
            }
        }

        public byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode)
        {
            switch (blockMode)
            {
                case BlockMode.ECB: return DecryptEcb(ciphertext);
                case BlockMode.CBC: return DecryptCbc(ciphertext, iv);
                case BlockMode.CTR: return DecryptCtr(ciphertext, iv);
                default: throw new Exception("Unsupported DES block mode!");
            }
        }

        // ECB ---------- ---------- ---------- ---------- ----------
        
        /// <summary>
        /// ECB模式加密
        /// </summary>
        public byte[] EncryptEcb(byte[] plaintext)
        {
            m_Des.Mode = CipherMode.ECB;
            m_Des.Padding = m_PaddingMode;
            var encryptor = m_Des.CreateEncryptor();
            try
            {
                return encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
            }
            finally
            {
                encryptor.Dispose();
            }
        }

        /// <summary>
        /// ECB模式解密
        /// </summary>
        public byte[] DecryptEcb(byte[] ciphertext)
        {
            m_Des.Mode = CipherMode.ECB;
            m_Des.Padding = m_PaddingMode;
            var decryptor = m_Des.CreateDecryptor();
            try
            {
                return decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
            }
            finally
            {
                decryptor.Dispose();
            }
        }

        // CBC ---------- ---------- ---------- ---------- ----------
        
        /// <summary>
        /// CBC模式加密
        /// </summary>
        public byte[] EncryptCbc(byte[] plaintext)
        {
            m_Des.Mode = CipherMode.CBC;
            m_Des.GenerateIV();
            using (var encryptor = m_Des.CreateEncryptor())
            {
                var encrypted = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
                return CryptoUtils.Combine(m_Des.IV, encrypted);
            }
        }

        public byte[] EncryptCbc(byte[] plaintext, byte[] iv)
        {
            m_Des.Mode = CipherMode.CBC;
            m_Des.IV = iv;
            using (var encryptor = m_Des.CreateEncryptor())
            {
                var encrypted = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
                return CryptoUtils.Combine(iv, encrypted);
            }
        }

        /// <summary>
        /// CBC模式解密
        /// </summary>
        public byte[] DecryptCbc(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, DesDefines.BlockSize, out var iv, out var data);
            return DecryptCbc(data, iv);
        }

        public byte[] DecryptCbc(byte[] ciphertext, byte[] iv)
        {
            m_Des.Mode = CipherMode.CBC;
            m_Des.IV = iv;
            using (var decryptor = m_Des.CreateDecryptor())
            {
                return decryptor.TransformFinalBlock(ciphertext, iv.Length, ciphertext.Length - iv.Length);
            }
        }

        // CTR ---------- ---------- ---------- ---------- ----------
        
        public byte[] EncryptCtr(byte[] plaintext)
        {
            m_Des.GenerateIV();
            var encrypted = EncryptCtr(plaintext, m_Des.IV);
            return CryptoUtils.Combine(m_Des.IV, encrypted);
        }

        public byte[] EncryptCtr(byte[] plaintext, byte[] iv)
        {
            return CtrWithEcb(plaintext, iv);
        }

        public byte[] DecryptCtr(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, DesDefines.BlockSize, out var iv, out var data);
            return DecryptCtr(data, iv);
        }

        public byte[] DecryptCtr(byte[] ciphertext, byte[] iv)
        {
            return CtrWithEcb(ciphertext, iv);
        }

        private byte[] CtrWithEcb(byte[] data, byte[] iv)
        {
            m_Des.Key = m_Key;
            m_Des.Mode = CipherMode.ECB; // 使用 ECB 模式加密计数器
            m_Des.Padding = PaddingMode.None; // CTR不使用Padding

            using (var encryptor = m_Des.CreateEncryptor()) // 加解密都应该使用CreateEncryptor
            {
                var counter = (byte[])iv.Clone();
                var output = new byte[data.Length];

                for (var i = 0; i < data.Length; i += DesDefines.BlockSize)
                {
                    var encryptedCounter = encryptor.TransformFinalBlock(counter, 0, 8);

                    for (var j = 0; j < Math.Min(DesDefines.BlockSize, data.Length - i); j++)
                    {
                        output[i + j] = (byte)(data[i + j] ^ encryptedCounter[j]);
                    }

                    IncrementCounter(counter); // 计数器递增
                }

                return output;
            }
        }

        private void IncrementCounter(byte[] counter)
        {
            for (var i = counter.Length - 1; i >= 0; i--)
            {
                if (++counter[i] != 0) break;
            }
        }
    }
}