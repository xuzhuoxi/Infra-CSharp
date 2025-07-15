using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    public sealed class AesCtrEngine
    {
        private static readonly byte[] s_DefaultCounter = { 0, 0, 0, 1 };
        public static AesCtrEngine Default { get; private set; } = new AesCtrEngine();

        private const int m_BlockSize = 16;

        /// <summary>
        /// 使用 随机nonce 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns>iv+ciphertext</returns>
        public byte[] EncryptRandom(byte[] data, byte[] key)
        {
            byte[] nonce = new byte[12]; // 推荐12字节，与 GCM/Go 保持一致
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(nonce);
            }

            var iv = CryptoUtils.Combine(nonce, s_DefaultCounter);
            var ciphertext = ProcessWithIv(data, key, iv);
            return CryptoUtils.Combine(iv, ciphertext);
        }

        /// <summary>
        /// 使用 密文中iv 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="CryptoException"></exception>
        public byte[] DecryptRandom(byte[] data, byte[] key)
        {
            if (data.Length < m_BlockSize)
            {
                throw new CryptoException("data.Length < BlockSize !");
            }

            CryptoUtils.Extract(data, m_BlockSize, out var iv, out var ciphertext);
            return ProcessWithIv(ciphertext, key, iv);
        }

        /// <summary>
        /// 加密 或 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">长度有三种，可以是16(AES-128),24(AES-192)或32(AES-256)</param>
        /// <param name="nonce">长度固定，为12字节(96位)</param>
        /// <returns></returns>
        public byte[] ProcessWithNonce(byte[] data, byte[] key, byte[] nonce)
        {
            var iv = CryptoUtils.Combine(nonce, s_DefaultCounter);
            return ProcessWithIv(data, key, iv);
        }

        /// <summary>
        /// 加密 或 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">长度有三种，可以是16(AES-128),24(AES-192)或32(AES-256)</param>
        /// <param name="nonce">长度不固定，推荐长度为12字节(96位), nonce与counter长度之和必须为16</param>
        /// <param name="counter">长度不固定，推荐长度为4字节(32位), nonce与counter长度之和必须为16</param>
        /// <returns></returns>
        public byte[] ProcessWithNonce(byte[] data, byte[] key, byte[] nonce, byte[] counter)
        {
            var iv = CryptoUtils.Combine(nonce, counter);
            return ProcessWithIv(data, key, iv);
        }

        /// <summary>
        /// 加密 或 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        /// <exception cref="CryptoException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte[] ProcessWithIv(byte[] data, byte[] key, byte[] iv)
        {
            // 初始化计数器块（nonce + counter）
            if (iv.Length != m_BlockSize)
            {
                throw new CryptoException("nonce.Length+counter.Length != BlockSize !");
            }

            byte[] counterBlock = new byte[m_BlockSize];
            System.Buffer.BlockCopy(iv, 0, counterBlock, 0, iv.Length);
            byte[] output = new byte[data.Length];
            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                aes.Key = key;

                using (var encryptor = aes.CreateEncryptor())
                {
                    for (int i = 0; i < data.Length; i += m_BlockSize)
                    {
                        byte[] encryptedCounter = encryptor.TransformFinalBlock(counterBlock, 0, m_BlockSize);
                        int blockSize = Math.Min(m_BlockSize, data.Length - i);

                        for (int j = 0; j < blockSize; j++)
                            output[i + j] = (byte)(data[i + j] ^ encryptedCounter[j]);

                        IncrementCounter(counterBlock);
                    }
                }
            }

            return output;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncrementCounter(byte[] counter)
        {
            for (int i = counter.Length - 1; i >= 12; i--) // 只递增最后4字节
            {
                if (++counter[i] != 0)
                    break;
            }
        }
    }
}