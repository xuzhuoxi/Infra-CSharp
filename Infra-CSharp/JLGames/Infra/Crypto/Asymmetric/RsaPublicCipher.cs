using System;
using System.Security.Cryptography;
using JLGames.Infra.Crypto.Symmetric;

namespace JLGames.Infra.Crypto.Asymmetric
{
    /// <summary>
    /// RSA 公钥加解密与签名验证实现（PKCS#1 v1.5 填充）。
    /// </summary>
    public class RsaPublicCipher : IRsaPublicCipher
    {
        private readonly RSAEncryptionPadding m_PaddingMode;
        /// <summary>单次 RSA 加密可容纳的最大明文字节数（密钥长度/8 - 11）。</summary>
        public int EncryptPartLen { get; private set; }
        /// <inheritdoc/>
        public RSA PublicKey { get; private set; }

        /// <summary>
        /// 使用 .NET <see cref="RSA"/> 公钥实例创建处理器。
        /// </summary>
        /// <param name="publicKey">RSA 公钥</param>
        public RsaPublicCipher(RSA publicKey)
        {
            PublicKey = publicKey;
            // RSA 使用 Pkcs1v15 padding，所以有 11 个字节用于 padding 信息
            EncryptPartLen = publicKey.KeySize / 8 - 11;
            m_PaddingMode = RSAEncryptionPadding.Pkcs1;
        }

        /// <inheritdoc/>
        public byte[] Encrypt(byte[] plaintext)
        {
            if (plaintext.Length <= EncryptPartLen)
            {
                return PublicKey.Encrypt(plaintext, m_PaddingMode);
            }

            using (var group = new RsaGroup(plaintext, EncryptPartLen))
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    while (group.HasNext)
                    {
                        var bytes = PublicKey.Encrypt(group.ReadNext(), RSAEncryptionPadding.Pkcs1);
                        memoryStream.Write(bytes, 0, bytes.Length);
                    }

                    return memoryStream.ToArray();
                }
            }
        }

        /// <inheritdoc/>
        public byte[] EncryptHybrid(byte[] plaintext)
        {
            if (plaintext.Length <= EncryptPartLen)
                return PublicKey.Encrypt(plaintext, m_PaddingMode);

            // 生成随机AES-Key和AES-IV, 合并后RSA加密
            var aesKey = new byte[AesDefines.DefaultKeyLength]; // AES-256
            var aesIv = new byte[AesDefines.BlockSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(aesKey);
                rng.GetBytes(aesIv);
            }

            var keyCiphertext = EncryptKeyIv(aesKey, aesIv);

            // 使用Key与IV，对明文进行AES-CTR加密
            var aesCipher = new AesCipher(aesKey);
            var ciphertext = aesCipher.EncryptCtr(plaintext, aesIv);

            // 合并两段密文            
            var rs = new byte[keyCiphertext.Length + ciphertext.Length];
            System.Buffer.BlockCopy(keyCiphertext, 0, rs, 0, keyCiphertext.Length);
            System.Buffer.BlockCopy(ciphertext, 0, rs, keyCiphertext.Length, ciphertext.Length);
            return rs;
        }


        /// <summary>
        /// 合并Key与IV, 并进行RSA加密
        /// </summary>
        /// <param name="aesKey"></param>
        /// <param name="aesIv"></param>
        /// <returns></returns>
        private byte[] EncryptKeyIv(byte[] aesKey, byte[] aesIv)
        {
            var aesKeyIv = CryptoUtils.Combine(aesKey, aesIv);
            return PublicKey.Encrypt(aesKeyIv, m_PaddingMode);
        }

        /// <summary>
        /// 使用SHA256进行签名验证
        /// </summary>
        public bool VerifySign(byte[] origData, byte[] signature)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashed = sha256.ComputeHash(origData);
                try
                {
                    return PublicKey.VerifyHash(hashed, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
                catch (CryptographicException)
                {
                    return false; // 验证失败
                }
            }
        }


        /// <summary>
        /// 使用SHA256进行签名验证（Base64）
        /// </summary>
        public bool VerifySignBase64(byte[] origData, string base64Signature)
        {
            var signature = Convert.FromBase64String(base64Signature);
            return VerifySign(origData, signature);
        }


        /// <summary>
        /// 指定Hash算法进行签名验证
        /// </summary>
        public bool VerifySignHash(byte[] origData, byte[] signature, HashAlgorithmName hashAlgorithm)
        {
            using (var hash = HashAlgorithm.Create(hashAlgorithm.Name))
            {
                if (null == hash)
                {
                    return false;
                }

                var hashed = hash.ComputeHash(origData);
                try
                {
                    return PublicKey.VerifyHash(hashed, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
                }
                catch (CryptographicException)
                {
                    return false; // 验证失败
                }
            }
        }

        /// <summary>
        /// 指定Hash算法进行签名验证（Base64）
        /// </summary>
        public bool VerifySignHashBase64(byte[] origData, string base64Signature, HashAlgorithmName hashAlgorithm)
        {
            var signature = Convert.FromBase64String(base64Signature);
            return VerifySignHash(origData, signature, hashAlgorithm);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            PublicKey?.Dispose();
        }
    }
}