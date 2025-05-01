using System;
using System.Linq;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Engines;

namespace JLGames.Infra.Crypto.Symmetric
{
    public class AesCipher : IAesCipher
    {
        private const int GcmNonceSize = 12;
        private readonly byte[] m_Key;
        private PaddingMode m_PaddingMode;
        private readonly KeyParameter m_KeyParam;

        public AesCipher(byte[] key)
        {
            m_Key = key;
            m_KeyParam = new KeyParameter(m_Key);
            m_PaddingMode = PaddingMode.PKCS7;
        }

        public byte[] Key => (byte[])m_Key.Clone();
        public int BlockSize => AesDefines.BlockSize;

        public void SetPadding(PaddingMode paddingMode)
        {
            m_PaddingMode = paddingMode;
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            return EncryptGcm(plaintext); // 默认使用 GCM 加密
        }

        public byte[] Decrypt(byte[] ciphertext)
        {
            return DecryptGcm(ciphertext); // 默认使用 GCM 解密
        }

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

        public byte[] EncryptCbc(byte[] plaintext)
        {
            var iv = new byte[AesDefines.BlockSize];
            new Random().NextBytes(iv);
            var output = EncryptCbc(plaintext, iv);
            return CryptoUtils.Combine(iv, output);
        }

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

        public byte[] DecryptCbc(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, AesDefines.BlockSize, out var iv, out var data);
            return DecryptCbc(data, iv);
        }

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

        public byte[] EncryptCtr(byte[] plaintext)
        {
            var iv = new byte[AesDefines.BlockSize];
            new Random().NextBytes(iv);
            var output = EncryptCtr(plaintext, iv);
            return CryptoUtils.Combine(iv, output);
        }

        public byte[] EncryptCtr(byte[] plaintext, byte[] iv)
        {
            // var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding"); // CTR算法不需要Padding
            // cipher.Init(true, new ParametersWithIV(new KeyParameter(m_Key), iv));
            // return cipher.DoFinal(plaintext);

            var cipher = new BufferedBlockCipher(new SicBlockCipher(new AesEngine()));
            cipher.Init(true, new ParametersWithIV(new KeyParameter(m_Key), iv));
            var output = new byte[cipher.GetOutputSize(plaintext.Length)];
            var processedBytes = cipher.ProcessBytes(plaintext, 0, plaintext.Length, output, 0);
            var finalBytes = cipher.DoFinal(output, processedBytes); // 处理最后一块数据
            return output.Take(processedBytes + finalBytes).ToArray();
        }

        public byte[] DecryptCtr(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, AesDefines.BlockSize, out var iv, out var data);
            return DecryptCtr(data, iv);
        }

        public byte[] DecryptCtr(byte[] ciphertext, byte[] iv)
        {
            // var cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding"); // CTR算法不需要Padding
            // cipher.Init(false, new ParametersWithIV(new KeyParameter(m_Key), iv));
            // return cipher.DoFinal(ciphertext);

            var cipher = new BufferedBlockCipher(new SicBlockCipher(new AesEngine()));
            cipher.Init(false, new ParametersWithIV(new KeyParameter(m_Key), iv));
            var output = new byte[cipher.GetOutputSize(ciphertext.Length)];
            var processedBytes = cipher.ProcessBytes(ciphertext, 0, ciphertext.Length, output, 0);
            var finalBytes = cipher.DoFinal(output, processedBytes); // 处理最后一块数据
            return output.Take(processedBytes + finalBytes).ToArray();
        }

        // GCM ---------- ---------- ---------- ---------- ----------

        public byte[] EncryptGcm(byte[] plaintext)
        {
            var nonce = new byte[GcmNonceSize]; // GCM nonce size is 12 bytes
            new Random().NextBytes(nonce);
            var output = EncryptGcm(plaintext, nonce);
            return CryptoUtils.Combine(nonce, output);
        }

        public byte[] EncryptGcm(byte[] plaintext, byte[] nonce)
        {
            var cipher = new AesLightEngine();
            var blockCipher = new GcmBlockCipher(cipher); // 使用 GCMBlockCipher

            var cipherParams = new ParametersWithIV(m_KeyParam, nonce);
            blockCipher.Init(true, cipherParams); // 初始化加密

            var output = new byte[blockCipher.GetOutputSize(plaintext.Length)];
            var length = blockCipher.ProcessBytes(plaintext, 0, plaintext.Length, output, 0);
            blockCipher.DoFinal(output, length); // 结束加密过程

            return output;
        }

        public byte[] DecryptGcm(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, GcmNonceSize, out var nonce, out var data); // GCM nonce size is 12 bytes
            return DecryptGcm(data, nonce);
        }

        public byte[] DecryptGcm(byte[] ciphertext, byte[] nonce)
        {
            var cipher = new AesLightEngine(); // 使用 AesLightEngine
            var blockCipher = new GcmBlockCipher(cipher); // 使用 GCMBlockCipher

            var cipherParams = new ParametersWithIV(m_KeyParam, nonce);
            blockCipher.Init(false, cipherParams); // 初始化解密

            var output = new byte[blockCipher.GetOutputSize(ciphertext.Length)];
            var length = blockCipher.ProcessBytes(ciphertext, 0, ciphertext.Length, output, 0);
            blockCipher.DoFinal(output, length); // 结束解密过程

            return output;
        }
    }
}