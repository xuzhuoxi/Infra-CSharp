using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    public sealed class AesCtrEngine
    {
        private static readonly byte[] s_Counter = { 0, 0, 0, 1 };
        public static AesCtrEngine Default { get; private set; } = new AesCtrEngine();

        private const int c_BlockSize = 16;

        /// <summary>
        /// 加密 或 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">长度有三种，可以是16(AES-128),24(AES-192)或32(AES-256)</param>
        /// <param name="nonce">长度不固定，推荐长度为12字节(96位)</param>
        /// <returns></returns>
        public byte[] Process(byte[] data, byte[] key, byte[] nonce)
        {
            byte[] output = new byte[data.Length];
            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                aes.Key = key;

                using (var encryptor = aes.CreateEncryptor())
                {
                    // 初始化计数器块（nonce + counter）
                    byte[] counterBlock = new byte[c_BlockSize];
                    // nonce 推荐情况下占前12字节
                    System.Buffer.BlockCopy(nonce, 0, counterBlock, 0, nonce.Length);
                    // 最后4字节作为计数器，初始为 1
                    System.Buffer.BlockCopy(s_Counter, 0, counterBlock, s_Counter.Length - 4, s_Counter.Length);

                    for (int i = 0; i < data.Length; i += c_BlockSize)
                    {
                        byte[] encryptedCounter = encryptor.TransformFinalBlock(counterBlock, 0, c_BlockSize);
                        int blockSize = Math.Min(c_BlockSize, data.Length - i);

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