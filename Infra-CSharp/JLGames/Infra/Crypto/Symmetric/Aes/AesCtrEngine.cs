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
                    System.Buffer.BlockCopy(nonce, 0, counterBlock, 0, nonce.Length); // nonce 占前12字节
                    // 最后4字节作为计数器，初始为 1
                    System.Buffer.BlockCopy(s_Counter, 0, counterBlock, nonce.Length, s_Counter.Length);

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