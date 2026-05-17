using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Asymmetric
{
    /// <summary>
    /// 组合 <see cref="RsaPrivateCipher"/> 与 <see cref="RsaPublicCipher"/> 的 RSA 完整实现。
    /// </summary>
    public class RsaCipher : IRsaCipher
    {
        private readonly RsaPrivateCipher m_RsaPrivateCipher;
        private readonly RsaPublicCipher m_RsaPublicCipher;

        /// <summary>
        /// 使用已有私钥与公钥封装创建实例。
        /// </summary>
        /// <param name="privateCipher">私钥处理器</param>
        /// <param name="publicCipher">公钥处理器</param>
        public RsaCipher(RsaPrivateCipher privateCipher, RsaPublicCipher publicCipher)
        {
            m_RsaPrivateCipher = privateCipher;
            m_RsaPublicCipher = publicCipher;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            m_RsaPrivateCipher?.Dispose();
            m_RsaPublicCipher?.Dispose();
        }

        // IRsaPrivateCipher ---------- ---------- ---------- ---------- ----------

        /// <inheritdoc/>
        public RSA PrivateKey => m_RsaPrivateCipher.PrivateKey;

        /// <inheritdoc/>
        public byte[] Decrypt(byte[] ciphertext)
        {
            return m_RsaPrivateCipher.Decrypt(ciphertext);
        }

        /// <inheritdoc/>
        public byte[] DecryptHybrid(byte[] ciphertext)
        {
            return m_RsaPrivateCipher.DecryptHybrid(ciphertext);
        }

        /// <inheritdoc/>
        public byte[] Sign(byte[] origData)
        {
            return m_RsaPrivateCipher.Sign(origData);
        }

        /// <inheritdoc/>
        public string SignBase64(byte[] origData)
        {
            return m_RsaPrivateCipher.SignBase64(origData);
        }

        /// <inheritdoc/>
        public byte[] SignHash(byte[] origData, HashAlgorithmName hashAlgorithm)
        {
            return m_RsaPrivateCipher.SignHash(origData, hashAlgorithm);
        }

        /// <inheritdoc/>
        public string SignHashBase64(byte[] origData, HashAlgorithmName hashAlgorithm)
        {
            return m_RsaPrivateCipher.SignHashBase64(origData, hashAlgorithm);
        }

        // IRsaPublicCipher ---------- ---------- ---------- ---------- ----------

        /// <inheritdoc/>
        public RSA PublicKey => m_RsaPublicCipher.PublicKey;

        /// <inheritdoc/>
        public byte[] Encrypt(byte[] plaintext)
        {
            return m_RsaPublicCipher.Encrypt(plaintext);
        }

        /// <inheritdoc/>
        public byte[] EncryptHybrid(byte[] plaintext)
        {
            return m_RsaPublicCipher.EncryptHybrid(plaintext);
        }

        /// <inheritdoc/>
        public bool VerifySign(byte[] origData, byte[] signature)
        {
            return m_RsaPublicCipher.VerifySign(origData, signature);
        }

        /// <inheritdoc/>
        public bool VerifySignBase64(byte[] origData, string base64Signature)
        {
            return m_RsaPublicCipher.VerifySignBase64(origData, base64Signature);
        }

        /// <inheritdoc/>
        public bool VerifySignHash(byte[] origData, byte[] signature, HashAlgorithmName hashAlgorithm)
        {
            return m_RsaPublicCipher.VerifySignHash(origData, signature, hashAlgorithm);
        }

        /// <inheritdoc/>
        public bool VerifySignHashBase64(byte[] origData, string base64Signature, HashAlgorithmName hashAlgorithm)
        {
            return m_RsaPublicCipher.VerifySignHashBase64(origData, base64Signature, hashAlgorithm);
        }
    }
}