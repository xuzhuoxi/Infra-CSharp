using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// AES 对称加解密（默认 GCM）；支持 CBC、CTR、GCM 等分组模式。
    /// </summary>
    public class AesCipher : IAesCipher
    {
        private const int m_GcmNonceSize = 12;
        private readonly byte[] m_Key;
        private PaddingMode m_PaddingMode;

        /// <summary>
        /// 使用指定密钥创建 AES 实例（密钥长度 16/24/32 字节对应 AES-128/192/256）。
        /// </summary>
        /// <param name="key">对称密钥</param>
        public AesCipher(byte[] key)
        {
            m_Key = key;
            Trace.WriteLine($"AesCipher key[{key.Length}]: [{string.Join(" ", key)}]");
            m_PaddingMode = PaddingMode.PKCS7;
        }

        /// <inheritdoc/>
        public byte[] Key => (byte[])m_Key.Clone();
        /// <inheritdoc/>
        public int BlockSize => AesDefines.BlockSize;

        /// <inheritdoc/>
        public void SetPadding(PaddingMode paddingMode)
        {
            m_PaddingMode = paddingMode;
        }

        /// <inheritdoc/>
        public byte[] Encrypt(byte[] plaintext)
        {
            return EncryptGcm(plaintext); // 默认使用 GCM 加密
        }

        /// <inheritdoc/>
        public byte[] Decrypt(byte[] ciphertext)
        {
            return DecryptGcm(ciphertext); // 默认使用 GCM 解密
        }

        /// <inheritdoc/>
        public byte[] EncryptMode(byte[] plaintext, BlockMode blockMode)
        {
            switch (blockMode)
            {
                case BlockMode.CBC:
                    return EncryptCbc(plaintext);
                case BlockMode.CTR:
                    return EncryptCtr(plaintext);
                case BlockMode.GCM:
                    return EncryptGcm(plaintext);
                default:
                    throw new InvalidOperationException("Unsupported AES block mode!");
            }
        }

        /// <inheritdoc/>
        public byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode)
        {
            switch (blockMode)
            {
                case BlockMode.CBC:
                    return EncryptCbc(plaintext, iv);
                case BlockMode.CTR:
                    return EncryptCtr(plaintext, iv);
                case BlockMode.GCM:
                    return EncryptGcm(plaintext, iv);
                default:
                    throw new InvalidOperationException("Unsupported AES block mode!");
            }
        }

        /// <inheritdoc/>
        public byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode)
        {
            switch (blockMode)
            {
                case BlockMode.CBC:
                    return DecryptCbc(ciphertext);
                case BlockMode.CTR:
                    return DecryptCtr(ciphertext);
                case BlockMode.GCM:
                    return DecryptGcm(ciphertext);
                default:
                    throw new InvalidOperationException("Unsupported AES block mode!");
            }
        }

        /// <inheritdoc/>
        public byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode)
        {
            switch (blockMode)
            {
                case BlockMode.CBC:
                    return DecryptCbc(ciphertext, iv);
                case BlockMode.CTR:
                    return DecryptCtr(ciphertext, iv);
                case BlockMode.GCM:
                    return DecryptGcm(ciphertext, iv);
                default:
                    throw new InvalidOperationException("Unsupported AES block mode!");
            }
        }

        // CBC ---------- ---------- ---------- ---------- ----------

        /// <inheritdoc/>
        public byte[] EncryptCbc(byte[] plaintext)
        {
            var iv = new byte[AesDefines.BlockSize];
            new Random().NextBytes(iv);
            var output = EncryptCbc(plaintext, iv);
            return CryptoUtils.Combine(iv, output);
        }

        /// <inheritdoc/>
        public byte[] EncryptCbc(byte[] plaintext, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = m_Key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = m_PaddingMode;
                using (var encryptor = aes.CreateEncryptor())
                {
                    var encrypted = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
                    return encrypted;
                }
            }
        }

        /// <inheritdoc/>
        public byte[] DecryptCbc(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, AesDefines.BlockSize, out var iv, out var data);
            return DecryptCbc(data, iv);
        }

        /// <inheritdoc/>
        public byte[] DecryptCbc(byte[] ciphertext, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = m_Key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = m_PaddingMode;
                using (var decryptor = aes.CreateDecryptor())
                {
                    var encrypted = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                    return encrypted;
                }
            }
        }

        // CTR ---------- ---------- ---------- ---------- ----------

        /// <inheritdoc/>
        public byte[] EncryptCtr(byte[] plaintext)
        {
            var iv = new byte[AesDefines.BlockSize];
            new Random().NextBytes(iv);
            var output = EncryptCtr(plaintext, iv);
            return CryptoUtils.Combine(iv, output);
        }

        /// <inheritdoc/>
        public byte[] EncryptCtr(byte[] plaintext, byte[] iv)
        {
            // Org.BouncyCastle.Crypto
            // return BouncyCastleAesCtrEngine.Default.EncryptCtr(plaintext, m_Key, iv);

            return AesCtrEngine.Default.ProcessWithIv(plaintext, m_Key, iv);
        }

        /// <inheritdoc/>
        public byte[] DecryptCtr(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, AesDefines.BlockSize, out var iv, out var data);
            return DecryptCtr(data, iv);
        }

        /// <inheritdoc/>
        public byte[] DecryptCtr(byte[] ciphertext, byte[] iv)
        {
            // Org.BouncyCastle.Crypto
            // return BouncyCastleAesCtrEngine.Default.DecryptCtr(ciphertext, m_Key, iv);

            return AesCtrEngine.Default.ProcessWithIv(ciphertext, m_Key, iv);
        }

        // GCM ---------- ---------- ---------- ---------- ----------

        /// <inheritdoc/>
        public byte[] EncryptGcm(byte[] plaintext)
        {
            var nonce = new byte[m_GcmNonceSize]; // GCM nonce size is 12 bytes
            new Random().NextBytes(nonce);
            var output = EncryptGcm(plaintext, nonce);
            return CryptoUtils.Combine(nonce, output);
        }

        /// <inheritdoc/>
        public byte[] EncryptGcm(byte[] plaintext, byte[] nonce)
        {
            AesGcmEngine.Default.Encrypt(plaintext, m_Key, nonce, out var ciphertext, out var tag);
            return CryptoUtils.Combine(ciphertext, tag);

            // Org.BouncyCastle.Crypto
            // return BouncyCastleAesGcmEngine.Default.EncryptGcm(plaintext, m_Key, nonce);
        }

        /// <inheritdoc/>
        public byte[] DecryptGcm(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, m_GcmNonceSize, out var nonce, out var data); // GCM nonce size is 12 bytes
            return DecryptGcm(data, nonce);
        }

        /// <inheritdoc/>
        public byte[] DecryptGcm(byte[] ciphertext, byte[] nonce)
        {
            // Console.WriteLine($"DecryptGcm：{ciphertext?.Length}");
            var size = ciphertext.Length - AesGcmEngine.Default.TagSize;
            CryptoUtils.Extract(ciphertext, size, out var ciphertext2, out var tag);
            bool valid = AesGcmEngine.Default.Decrypt(ciphertext2, m_Key, nonce, tag, out var plaintext);
            if (valid)
            {
                return plaintext;
            }
            else
            {
                throw new Exception("Tag verification failed. Decryption aborted.");
            }

            // Org.BouncyCastle.Crypto
            // return BouncyCastleAesGcmEngine.Default.DecryptGcm(ciphertext, m_Key, nonce);
        }
    }
}