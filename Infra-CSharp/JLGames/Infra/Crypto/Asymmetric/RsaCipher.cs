using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Asymmetric
{
    public class RsaCipher : IRsaCipher
    {
        private readonly RsaPrivateCipher m_RsaPrivateCipher;
        private readonly RsaPublicCipher m_RsaPublicCipher;

        public RsaCipher(RsaPrivateCipher privateCipher, RsaPublicCipher publicCipher)
        {
            m_RsaPrivateCipher = privateCipher;
            m_RsaPublicCipher = publicCipher;
        }

        public void Dispose()
        {
            m_RsaPrivateCipher?.Dispose();
            m_RsaPublicCipher?.Dispose();
        }

        // IRsaPrivateCipher ---------- ---------- ---------- ---------- ----------

        public RSA PrivateKey => m_RsaPrivateCipher.PrivateKey;

        public byte[] Decrypt(byte[] ciphertext)
        {
            return m_RsaPrivateCipher.Decrypt(ciphertext);
        }

        public byte[] DecryptHybrid(byte[] ciphertext)
        {
            return m_RsaPrivateCipher.DecryptHybrid(ciphertext);
        }

        public byte[] Sign(byte[] origData)
        {
            return m_RsaPrivateCipher.Sign(origData);
        }

        public string SignBase64(byte[] origData)
        {
            return m_RsaPrivateCipher.SignBase64(origData);
        }

        public byte[] SignHash(byte[] origData, HashAlgorithmName hashAlgorithm)
        {
            return m_RsaPrivateCipher.SignHash(origData, hashAlgorithm);
        }

        public string SignHashBase64(byte[] origData, HashAlgorithmName hashAlgorithm)
        {
            return m_RsaPrivateCipher.SignHashBase64(origData, hashAlgorithm);
        }

        // IRsaPublicCipher ---------- ---------- ---------- ---------- ----------

        public RSA PublicKey => m_RsaPublicCipher.PublicKey;

        public byte[] Encrypt(byte[] plaintext)
        {
            return m_RsaPublicCipher.Encrypt(plaintext);
        }

        public byte[] EncryptHybrid(byte[] plaintext)
        {
            return m_RsaPublicCipher.EncryptHybrid(plaintext);
        }

        public bool VerifySign(byte[] origData, byte[] signature)
        {
            return m_RsaPublicCipher.VerifySign(origData, signature);
        }

        public bool VerifySignBase64(byte[] origData, string base64Signature)
        {
            return m_RsaPublicCipher.VerifySignBase64(origData, base64Signature);
        }

        public bool VerifySignHash(byte[] origData, byte[] signature, HashAlgorithmName hashAlgorithm)
        {
            return m_RsaPublicCipher.VerifySignHash(origData, signature, hashAlgorithm);
        }

        public bool VerifySignHashBase64(byte[] origData, string base64Signature, HashAlgorithmName hashAlgorithm)
        {
            return m_RsaPublicCipher.VerifySignHashBase64(origData, base64Signature, hashAlgorithm);
        }
    }
}