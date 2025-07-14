using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace JLGames.Infra.Crypto.Symmetric
{
    public class AesCipher : IAesCipher
    {
        private const int c_GcmNonceSize = 12;
        private readonly byte[] m_Key;
        private PaddingMode m_PaddingMode;

        public AesCipher(byte[] key)
        {
            m_Key = key;
            Trace.WriteLine($"AesCipher key[{key.Length}]: [{string.Join(" ", key)}]");
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
            // return EncryptCtr(plaintext); // 默认使用 GCM 加密
        }

        public byte[] Decrypt(byte[] ciphertext)
        {
            return DecryptGcm(ciphertext); // 默认使用 GCM 解密
            // return DecryptCtr(ciphertext); // 默认使用 GCM 解密
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
            Console.WriteLine("EncryptCtr ---0");
            Console.WriteLine($"Plaintext: [{string.Join(" ", plaintext)}], iv: [{string.Join(" ", iv)}]");
            Console.WriteLine($"Ciphertext[BouncyCastle]: [{string.Join(" ", BouncyCastleAesCtrEngine.Default.EncryptCtr(plaintext, m_Key, iv))}]");
            Console.WriteLine($"Ciphertext[AesCtrEngine]: [{string.Join(" ", AesCtrEngine.Default.Process(plaintext, m_Key, iv))}]");
            Console.WriteLine("EncryptCtr ---1");
            Console.WriteLine();
            Console.Out.Flush();

            // Org.BouncyCastle.Crypto
            return BouncyCastleAesCtrEngine.Default.EncryptCtr(plaintext, m_Key, iv);

            // return AesCtrEngine.Default.Process(plaintext, m_Key, iv);
        }

        public byte[] DecryptCtr(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, AesDefines.BlockSize, out var iv, out var data);
            return DecryptCtr(data, iv);
        }

        public byte[] DecryptCtr(byte[] ciphertext, byte[] iv)
        {
            Console.WriteLine("DecryptCtr ---0");
            Console.WriteLine($"Ciphertext: [{string.Join(" ", ciphertext)}], iv: [{string.Join(" ", iv)}]");
            Console.WriteLine($"Plaintext[BouncyCastle]: [{string.Join(" ", BouncyCastleAesCtrEngine.Default.DecryptCtr(ciphertext, m_Key, iv))}]");
            Console.WriteLine($"Plaintext[AesCtrEngine]: [{string.Join(" ", AesCtrEngine.Default.Process(ciphertext, m_Key, iv))}]");
            Console.WriteLine("DecryptCtr ---1");
            Console.WriteLine();
            Console.Out.Flush();
            // Org.BouncyCastle.Crypto
            return BouncyCastleAesCtrEngine.Default.DecryptCtr(ciphertext, m_Key, iv);

            // return AesCtrEngine.Default.Process(ciphertext, m_Key, iv);
        }

        // GCM ---------- ---------- ---------- ---------- ----------

        public byte[] EncryptGcm(byte[] plaintext)
        {
            var nonce = new byte[c_GcmNonceSize]; // GCM nonce size is 12 bytes
            new Random().NextBytes(nonce);
            var output = EncryptGcm(plaintext, nonce);
            return CryptoUtils.Combine(nonce, output);
        }

        public byte[] EncryptGcm(byte[] plaintext, byte[] nonce)
        {
            AesGcmEngine.Default.Encrypt(plaintext, m_Key, nonce, out var ciphertext, out var tag);
            // return CryptoUtils.Combine(ciphertext, tag);

            Console.WriteLine("EncryptGcm ---0");
            Console.WriteLine($"Plaintext: [{string.Join(" ", plaintext)}], nonce: [{string.Join(" ", nonce)}]");
            Console.WriteLine(
                $"Ciphertext[BouncyCastle]: [{string.Join(" ", BouncyCastleAesGcmEngine.Default.EncryptGcm(plaintext, m_Key, nonce))}]");
            Console.WriteLine($"Ciphertext[AesGcmEngine]: [{string.Join(" ", CryptoUtils.Combine(ciphertext, tag))}]");
            Console.WriteLine("EncryptGcm ---1");
            Console.WriteLine();
            Console.Out.Flush();

            // Org.BouncyCastle.Crypto
            return BouncyCastleAesGcmEngine.Default.EncryptGcm(plaintext, m_Key, nonce);
        }

        /// <summary>
        /// merge ciphertext, include nonce, ciphertext and tag 
        /// </summary>
        /// <param name="ciphertext">nonce + ciphertext + tag</param>
        /// <returns></returns>
        public byte[] DecryptGcm(byte[] ciphertext)
        {
            CryptoUtils.Extract(ciphertext, c_GcmNonceSize, out var nonce, out var data); // GCM nonce size is 12 bytes
            return DecryptGcm(data, nonce);
        }

        public byte[] DecryptGcm(byte[] ciphertext, byte[] nonce)
        {
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


            Console.WriteLine("DecryptGcm ---0");
            Console.WriteLine($"Ciphertext: [{string.Join(" ", ciphertext)}], nonce: [{string.Join(" ", nonce)}]");
            var bcp = BouncyCastleAesGcmEngine.Default.DecryptGcm(ciphertext, m_Key, nonce);
            var bcpStr = Encoding.UTF8.GetString(bcp);
            Console.WriteLine($"Plaintext[BouncyCastle]: [{string.Join(" ", bcp)}], {bcpStr}");
            Console.WriteLine($"Plaintext[AesGcmEngine]: [{string.Join(" ", plaintext)}]");
            Console.WriteLine("DecryptGcm ---1");
            Console.WriteLine();
            Console.Out.Flush();

            // Org.BouncyCastle.Crypto
            return BouncyCastleAesGcmEngine.Default.DecryptGcm(ciphertext, m_Key, nonce);
        }
    }
}