using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// Low-level AES-CTR engine (nonce + counter form a 16-byte IV).
    /// AES-CTR 模式底层引擎（nonce + counter 组成 16 字节 IV）。
    /// </summary>
    public sealed class AesCtrEngine
    {
        private static readonly byte[] s_DefaultCounter = { 0, 0, 0, 1 };
        /// <summary>Default singleton instance. / 默认单例实例。</summary>
        public static AesCtrEngine Default { get; private set; } = new AesCtrEngine();

        private const int m_BlockSize = 16;

        /// <summary>
        /// Encrypt with a random 12-byte nonce; output is IV + ciphertext.
        /// 使用随机 nonce 加密，返回 IV + 密文。
        /// </summary>
        /// <param name="data">Plaintext or ciphertext (CTR is symmetric) / 明文或密文</param>
        /// <param name="key">AES key, 16/24/32 bytes / AES 密钥</param>
        /// <returns>16-byte IV followed by processed data / IV + 处理后的数据</returns>
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
        /// Decrypt input prefixed with a 16-byte IV (from <see cref="EncryptRandom"/>).
        /// 从 IV + 密文格式中解析 IV 并解密/还原。
        /// </summary>
        /// <param name="data">IV + ciphertext / IV + 密文</param>
        /// <param name="key">AES key / AES 密钥</param>
        /// <returns>Plaintext / 明文</returns>
        /// <exception cref="CryptoException">Input shorter than one block / 长度不足一块</exception>
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
        /// CTR encrypt or decrypt with 12-byte nonce and default 4-byte counter (0,0,0,1).
        /// 使用 12 字节 nonce 与默认 counter 进行 CTR 加/解密。
        /// </summary>
        /// <param name="data">Input buffer / 输入数据</param>
        /// <param name="key">16/24/32-byte AES key / AES 密钥</param>
        /// <param name="nonce">12-byte nonce / 12 字节 nonce</param>
        /// <returns>Output (same length as input) / 与输入等长的输出</returns>
        public byte[] ProcessWithNonce(byte[] data, byte[] key, byte[] nonce)
        {
            var iv = CryptoUtils.Combine(nonce, s_DefaultCounter);
            return ProcessWithIv(data, key, iv);
        }

        /// <summary>
        /// CTR encrypt or decrypt; nonce and counter are concatenated to form the 16-byte IV.
        /// CTR 加/解密；nonce 与 counter 拼接为 16 字节 IV。
        /// </summary>
        /// <param name="data">Input buffer / 输入数据</param>
        /// <param name="key">16/24/32-byte AES key / AES 密钥</param>
        /// <param name="nonce">Nonce bytes (recommended 12) / nonce 字节</param>
        /// <param name="counter">Counter bytes (recommended 4); len(nonce)+len(counter)=16 / counter 字节</param>
        /// <returns>Output (same length as input) / 与输入等长的输出</returns>
        public byte[] ProcessWithNonce(byte[] data, byte[] key, byte[] nonce, byte[] counter)
        {
            var iv = CryptoUtils.Combine(nonce, counter);
            return ProcessWithIv(data, key, iv);
        }

        /// <summary>
        /// CTR encrypt or decrypt with a full 16-byte IV (nonce || counter).
        /// 使用完整 16 字节 IV 进行 CTR 加/解密。
        /// </summary>
        /// <param name="data">Input buffer / 输入数据</param>
        /// <param name="key">AES key / AES 密钥</param>
        /// <param name="iv">Exactly 16 bytes / 必须为 16 字节</param>
        /// <returns>Output (same length as input) / 与输入等长的输出</returns>
        /// <exception cref="CryptoException"><paramref name="iv"/> length is not 16 / IV 长度不为 16</exception>
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