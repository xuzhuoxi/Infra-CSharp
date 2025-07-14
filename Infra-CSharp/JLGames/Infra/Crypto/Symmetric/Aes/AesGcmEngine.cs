using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    public sealed class AesGcmEngine
    {
        public static AesGcmEngine Default { get; private set; } = new AesGcmEngine();

        private const int c_BlockSize = 16;
        private readonly int m_AuthenticationTagSize;
        public int TagSize => m_AuthenticationTagSize;

        public AesGcmEngine()
        {
            m_AuthenticationTagSize = 16;
        }

        public AesGcmEngine(int authenticationTagSize)
        {
            if (authenticationTagSize < 12 || authenticationTagSize > 16)
                throw new ArgumentException("Invalid authentication tag size");
            m_AuthenticationTagSize = authenticationTagSize;
        }

        public void Encrypt(byte[] plaintext, byte[] key, byte[] nonce, out byte[] ciphertext, out byte[] tag)
        {
            ciphertext = new byte[plaintext.Length];

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] zeroBlock = new byte[c_BlockSize];
                    byte[] h = encryptor.TransformFinalBlock(zeroBlock, 0, c_BlockSize);

                    byte[] j0 = BuildJ0(nonce);

                    byte[] ctr = (byte[])j0.Clone();
                    IncrementCounter(ctr); // move to J₁

                    using (ICryptoTransform ctrEncryptor = aes.CreateEncryptor())
                    {
                        byte[] counterBlock = new byte[c_BlockSize];
                        for (int i = 0; i < plaintext.Length; i += c_BlockSize)
                        {
                            int remainBlockSize = Math.Min(c_BlockSize, plaintext.Length - i);
                            counterBlock = ctrEncryptor.TransformFinalBlock(ctr, 0, c_BlockSize);
                            for (int j = 0; j < remainBlockSize; j++)
                                ciphertext[i + j] = (byte)(plaintext[i + j] ^ counterBlock[j]);
                            IncrementCounter(ctr);
                        }
                    }

                    // GHASH input: ciphertext + length block
                    byte[] ghashInput = BuildGHashInput(ciphertext);
                    byte[] tagInput = GHash(h, ghashInput);

                    byte[] tagMask = encryptor.TransformFinalBlock(j0, 0, c_BlockSize);

                    tag = new byte[16];
                    for (int i = 0; i < 16; i++)
                        tag[i] = (byte)(tagInput[i] ^ tagMask[i]);
                }
            }
        }


        public bool Decrypt(byte[] ciphertext, byte[] key, byte[] nonce, byte[] tag, out byte[] plaintext)
        {
            plaintext = new byte[ciphertext.Length];

            // Step 1: AES_K function
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    byte[] zeroBlock = new byte[c_BlockSize];
                    byte[] h = encryptor.TransformFinalBlock(zeroBlock, 0, c_BlockSize);

                    // Step 2: Build J₀
                    byte[] j0 = BuildJ0(nonce);

                    // Step 3: Encrypt J₀ to create tag mask
                    byte[] tagMask = encryptor.TransformFinalBlock(j0, 0, c_BlockSize);

                    // Step 4: GHASH over ciphertext + length block
                    byte[] ghashInput = BuildGHashInput(ciphertext);
                    byte[] tagInput = GHash(h, ghashInput);

                    // Step 5: Calculate authentication tag
                    byte[] calculatedTag = new byte[m_AuthenticationTagSize];
                    for (int i = 0; i < m_AuthenticationTagSize; i++)
                        calculatedTag[i] = (byte)(tagInput[i] ^ tagMask[i]);

                    // Step 6: Constant-time comparison
                    if (!AreEqual(calculatedTag, tag))
                        return false;

                    // Step 7: AES-CTR decrypt
                    byte[] ctr = (byte[])j0.Clone();
                    IncrementCounter(ctr); // move to J₁
                    using (ICryptoTransform ctrEncryptor = aes.CreateEncryptor())
                    {
                        byte[] counterBlock = new byte[c_BlockSize];
                        for (int i = 0; i < ciphertext.Length; i += c_BlockSize)
                        {
                            int blockSize = Math.Min(16, ciphertext.Length - i);
                            counterBlock = ctrEncryptor.TransformFinalBlock(ctr, 0, c_BlockSize);
                            for (int j = 0; j < blockSize; j++)
                                plaintext[i + j] = (byte)(ciphertext[i + j] ^ counterBlock[j]);
                            IncrementCounter(ctr);
                        }
                    }
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private byte[] BuildJ0(byte[] nonce)
        {
            byte[] j0 = new byte[16];
            System.Buffer.BlockCopy(nonce, 0, j0, 0, 12);
            j0[12] = 0;
            j0[13] = 0;
            j0[14] = 0;
            j0[15] = 1;
            return j0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private byte[] BuildGHashInput(byte[] ciphertext)
        {
            // 手动补齐密文到 16 字节块（AES-GCM 不自动 padding）
            int padding = 16 - (ciphertext.Length % 16);
            int paddedLength = ciphertext.Length + padding;
            int gHashInputLen = paddedLength + c_BlockSize;

            byte[] gHashInput = new byte[gHashInputLen];
            System.Buffer.BlockCopy(ciphertext, 0, gHashInput, 0, ciphertext.Length);
            // 剩余部分为 0，无需额外处理

            // 构造标准 16 字节长度块
            ulong cipherBitLength = (ulong)ciphertext.Length * 8;
            gHashInput[gHashInputLen - 8] = (byte)(cipherBitLength >> 56);
            gHashInput[gHashInputLen - 7] = (byte)(cipherBitLength >> 48);
            gHashInput[gHashInputLen - 6] = (byte)(cipherBitLength >> 40);
            gHashInput[gHashInputLen - 5] = (byte)(cipherBitLength >> 32);
            gHashInput[gHashInputLen - 4] = (byte)(cipherBitLength >> 24);
            gHashInput[gHashInputLen - 3] = (byte)(cipherBitLength >> 16);
            gHashInput[gHashInputLen - 2] = (byte)(cipherBitLength >> 8);
            gHashInput[gHashInputLen - 1] = (byte)(cipherBitLength);

            return gHashInput;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private byte[] GHash(byte[] h, byte[] data)
        {
            const int gHashSize = 16;
            byte[] y = new byte[gHashSize];

            for (int i = 0; i < data.Length; i += gHashSize)
            {
                byte[] block = new byte[gHashSize];
                int blockSize = Math.Min(gHashSize, data.Length - i);
                System.Buffer.BlockCopy(data, i, block, 0, blockSize);
                for (int j = 0; j < gHashSize; j++)
                    y[j] ^= block[j];
                y = MultiplyGf128(y, h);
            }

            return y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private byte[] MultiplyGf128(byte[] x, byte[] y)
        {
            byte[] z = new byte[16];
            byte[] v = (byte[])y.Clone();

            for (int i = 0; i < 128; i++)
            {
                int byteIndex = i / 8;
                int bitIndex = 7 - (i % 8);
                if ((x[byteIndex] & (1 << bitIndex)) != 0)
                {
                    for (int j = 0; j < 16; j++)
                        z[j] ^= v[j];
                }

                bool lsb = (v[15] & 1) != 0;
                for (int j = 15; j > 0; j--)
                    v[j] = (byte)((v[j] >> 1) | ((v[j - 1] & 1) << 7));
                v[0] >>= 1;
                if (lsb)
                    v[0] ^= 0xe1;
            }

            return z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncrementCounter(byte[] counter)
        {
            for (int i = 15; i >= 12; i--)
            {
                counter[i]++;
                if (counter[i] != 0)
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool AreEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}