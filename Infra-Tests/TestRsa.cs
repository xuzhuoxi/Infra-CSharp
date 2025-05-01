using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using JLGames.Infra.Crypto.Asymmetric;

namespace Infra_Tests
{
    public static class TestRsaValues
    {
        public static readonly string ContentShort = @"abcdabcdabcd";

        public static readonly string ContentLong =
            @"abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,
abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,
abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,
abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,
abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,
abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,abcdabcdabcd,";
    }

    [TestFixture]
    public class TestRsa
    {
        private string m_PrivateKeyPath;
        private string m_PublicKeyPath;

        private byte[] m_ContentShort;
        private byte[] m_ContentLong;

        private IRsaPrivateCipher m_PrivateCipher;
        private IRsaPublicCipher m_PublicCipher;

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            var basePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent!.Parent!.Parent!.FullName;
            m_PrivateKeyPath = Path.Combine(basePath!, "TestResources/Keys", "rsa_private.pem");
            m_PublicKeyPath = Path.Combine(basePath!, "TestResources/Keys", "rsa_public.pem");

            m_ContentShort = Encoding.UTF8.GetBytes(TestRsaValues.ContentShort);
            m_ContentLong = Encoding.UTF8.GetBytes(TestRsaValues.ContentLong);
        }

        [OneTimeTearDown]
        public void GlobalCleanup()
        {
        }

        [SetUp]
        public void Setup()
        {
            m_PrivateCipher = RsaUtils.LoadPrivateCipherPkcs1V15(m_PrivateKeyPath);
            m_PublicCipher = RsaUtils.LoadPublicCipherPkcs1V15(m_PublicKeyPath);
        }

        [TearDown]
        public void TearDown()
        {
            m_PrivateCipher?.Dispose();
            m_PrivateCipher = null;
            m_PublicCipher?.Dispose();
            m_PublicCipher = null;
        }

        [Test, Order(1)]
        public void TestNullable()
        {
            Assert.NotNull(m_PrivateCipher);
            Assert.NotNull(m_PublicCipher);
        }

        [Test, Order(2)]
        public void TestEncryptShort()
        {
            byte[] ciphertext;
            byte[] plaintext;

            ciphertext = m_PublicCipher.Encrypt(m_ContentShort);
            plaintext = m_PrivateCipher.Decrypt(ciphertext);
            Assert.IsTrue(m_ContentShort.SequenceEqual(plaintext),
                $"Encrypt Short: [{string.Join(" ", m_ContentShort)}],[{string.Join(" ", plaintext)}]");

            ciphertext = m_PublicCipher.EncryptHybrid(m_ContentShort);
            plaintext = m_PrivateCipher.DecryptHybrid(ciphertext);
            Assert.IsTrue(m_ContentShort.SequenceEqual(plaintext),
                $"Hybrid Long: [{string.Join(" ", m_ContentShort)}],[{string.Join(" ", plaintext)}]");
        }

        [Test, Order(2)]
        public void TestEncryptLong()
        {
            byte[] ciphertext;
            byte[] plaintext;

            ciphertext = m_PublicCipher.Encrypt(m_ContentLong);
            plaintext = m_PrivateCipher.Decrypt(ciphertext);
            Assert.IsTrue(m_ContentLong.SequenceEqual(plaintext),
                $"Encrypt Long: [{string.Join(" ", m_ContentLong)}],[{string.Join(" ", plaintext)}]");

            ciphertext = m_PublicCipher.EncryptHybrid(m_ContentLong);
            plaintext = m_PrivateCipher.DecryptHybrid(ciphertext);
            Assert.IsTrue(m_ContentLong.SequenceEqual(plaintext),
                $"Hybrid Long: [{string.Join(" ", m_ContentLong)}],[{string.Join(" ", plaintext)}]");
        }

        [Test, Order(3)]
        public void TestKeySign()
        {
            byte[] signed;
            bool isValid;

            signed = m_PrivateCipher.Sign(m_ContentShort);
            isValid = m_PublicCipher.VerifySign(m_ContentShort, signed);
            Assert.IsTrue(isValid);

            signed = m_PrivateCipher.Sign(m_ContentLong);
            isValid = m_PublicCipher.VerifySign(m_ContentLong, signed);
            Assert.IsTrue(isValid);
        }

        [Test, Order(5)]
        public void TestKeySignHash()
        {
            var hashs = new[]
                { HashAlgorithmName.MD5, HashAlgorithmName.SHA1, HashAlgorithmName.SHA256, HashAlgorithmName.SHA384, HashAlgorithmName.SHA512 };
            foreach (var hashAlgorithmName in hashs)
            {
                var signed = m_PrivateCipher.SignHash(m_ContentShort, hashAlgorithmName);
                var isValid = m_PublicCipher.VerifySignHash(m_ContentShort, signed, hashAlgorithmName);
                Assert.IsTrue(isValid);
                signed = m_PrivateCipher.SignHash(m_ContentLong, hashAlgorithmName);
                isValid = m_PublicCipher.VerifySignHash(m_ContentLong, signed, hashAlgorithmName);
                Assert.IsTrue(isValid);
            }
        }
    }
}