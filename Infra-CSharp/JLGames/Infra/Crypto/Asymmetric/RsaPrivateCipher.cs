using System;
using System.Security.Cryptography;
using JLGames.Infra.Crypto.Symmetric;

namespace JLGames.Infra.Crypto.Asymmetric
{
    /// <summary>
    /// RSA 私钥解密与签名实现（PKCS#1 v1.5 填充）。
    /// </summary>
    public class RsaPrivateCipher : IRsaPrivateCipher
    {
        private readonly RSAEncryptionPadding m_PaddingMode;
        /// <summary>单段 RSA 密文字节数（密钥长度/8）。</summary>
        public int DecryptPartLen { get; private set; }
        /// <inheritdoc/>
        public RSA PrivateKey { get; private set; }

        /// <summary>
        /// 使用 .NET <see cref="RSA"/> 私钥实例创建处理器。
        /// </summary>
        /// <param name="privateKey">RSA 私钥</param>
        public RsaPrivateCipher(RSA privateKey)
        {
            PrivateKey = privateKey;
            DecryptPartLen = privateKey.KeySize / 8;
            m_PaddingMode = RSAEncryptionPadding.Pkcs1;
        }

        /// <inheritdoc/>
        public byte[] Decrypt(byte[] ciphertext)
        {
            if (ciphertext.Length == DecryptPartLen)
            {
                return PrivateKey.Decrypt(ciphertext, m_PaddingMode);
            }

            using (var group = new RsaGroup(ciphertext, DecryptPartLen))
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    while (group.HasNext)
                    {
                        var bytes = PrivateKey.Decrypt(group.ReadNext(), RSAEncryptionPadding.Pkcs1);
                        memoryStream.Write(bytes, 0, bytes.Length);
                    }

                    return memoryStream.ToArray();
                }
            }
        }

        /// <inheritdoc/>
        public byte[] DecryptHybrid(byte[] ciphertext)
        {
            if (ciphertext.Length == DecryptPartLen)
                return PrivateKey.Decrypt(ciphertext, m_PaddingMode);

            // 拆分成RSA密文与AES密文
            CryptoUtils.Extract(ciphertext, DecryptPartLen, out var encryptedKey, out var aesCiphertext);

            // 解密RSA密文，得到AES-Key与AES-IV
            DecryptKeyIv(encryptedKey, out var aesKey, out var aesIv);

            // 使用AES-Key与AES-IV对AES密文进行AES-CTR解密
            var aesCipher = new AesCipher(aesKey);
            var plaintext = aesCipher.DecryptCtr(aesCiphertext, aesIv);

            return plaintext;
        }

        /// <summary>
        /// RSA解密
        /// 拆分Key与IV
        /// </summary>
        /// <param name="encryptedKey"></param>
        /// <param name="aesKey"></param>
        /// <param name="aesIv"></param>
        /// <returns></returns>
        private void DecryptKeyIv(byte[] encryptedKey, out byte[] aesKey, out byte[] aesIv)
        {
            var plaintext = PrivateKey.Decrypt(encryptedKey, m_PaddingMode);
            CryptoUtils.Extract(plaintext, AesDefines.DefaultKeyLength, out aesKey, out aesIv);
        }

        /// <summary>
        /// 使用SHA256进行签名
        /// </summary>
        public byte[] Sign(byte[] origData)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashed = sha256.ComputeHash(origData);
                return PrivateKey.SignHash(hashed, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        /// <summary>
        /// 使用SHA256进行签名，并将结果转化为Base64编码字符串
        /// </summary>
        public string SignBase64(byte[] origData)
        {
            var signature = Sign(origData);
            return Convert.ToBase64String(signature);
        }

        /// <summary>
        /// 指定Hash算法进行签名
        /// </summary>
        public byte[] SignHash(byte[] origData, HashAlgorithmName hashAlgorithm)
        {
            using (var o = HashAlgorithm.Create(hashAlgorithm.Name))
            {
                if (o != null)
                {
                    var hashed = o.ComputeHash(origData);
                    return PrivateKey.SignHash(hashed, hashAlgorithm, RSASignaturePadding.Pkcs1);
                }
            }

            return null;
        }

        /// <summary>
        /// 指定Hash算法进行签名，并将结果转化为Base64编码字符串
        /// </summary>
        public string SignHashBase64(byte[] origData, HashAlgorithmName hashAlgorithm)
        {
            var signature = SignHash(origData, hashAlgorithm);
            return Convert.ToBase64String(signature);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            PrivateKey?.Dispose();
        }
    }
}