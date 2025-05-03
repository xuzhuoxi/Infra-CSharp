using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using JLGames.Infra.Crypto.Asymmetric;

namespace JLGames.InfraTests.Crypto
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
        private string m_Pkcs1PrivateKeyPath;
        private string m_Pkcs1PublicKeyPath;
        private string m_Pkcs8PrivateKeyPath;
        private string m_X509PublicKeyPath;

        private byte[] m_ContentShort;
        private byte[] m_ContentLong;

        private IRsaPrivateCipher m_PrivateCipherPkcs1;
        private IRsaPublicCipher m_PublicCipherPkcs1;
        private IRsaPrivateCipher m_PrivateCipherPkcs8;
        private IRsaPublicCipher m_PublicCipherX509;

        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            var basePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent!.Parent!.Parent!.FullName;

            m_Pkcs1PrivateKeyPath = Path.Combine(basePath!, "TestResources/Keys", "rsa_private.pem");
            m_Pkcs1PublicKeyPath = Path.Combine(basePath!, "TestResources/Keys", "rsa_public.pem");

            m_Pkcs8PrivateKeyPath = Path.Combine(basePath!, "TestResources/Keys", "pkcs8_private.pem");
            m_X509PublicKeyPath = Path.Combine(basePath!, "TestResources/Keys", "x509_public.pem");

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
            m_PrivateCipherPkcs1 = RsaUtils.LoadPrivateCipherPkcs1V15(m_Pkcs1PrivateKeyPath);
            m_PublicCipherPkcs1 = RsaUtils.LoadPublicCipherPkcs1V15(m_Pkcs1PublicKeyPath);

            m_PrivateCipherPkcs8 = RsaUtils.LoadPrivateCipherPkcs8(m_Pkcs8PrivateKeyPath);
            m_PublicCipherX509 = RsaUtils.LoadPublicCipherX509(m_X509PublicKeyPath);
        }

        [TearDown]
        public void TearDown()
        {
            m_PublicCipherX509?.Dispose();
            m_PublicCipherX509 = null;
            m_PrivateCipherPkcs8?.Dispose();
            m_PrivateCipherPkcs8 = null;

            m_PublicCipherPkcs1?.Dispose();
            m_PublicCipherPkcs1 = null;
            m_PrivateCipherPkcs1?.Dispose();
            m_PrivateCipherPkcs1 = null;
        }

        [Test, Order(1)]
        public void TestNullable()
        {
            Assert.NotNull(m_PrivateCipherPkcs1);
            Assert.NotNull(m_PublicCipherPkcs1);
            Assert.NotNull(m_PrivateCipherPkcs8);
            Assert.NotNull(m_PublicCipherX509);
        }

        [Test]
        public void TestPemKeys()
        {
            byte[] ciphertext;
            byte[] plaintext;

            ciphertext = m_PublicCipherX509.Encrypt(m_ContentShort);
            plaintext = m_PrivateCipherPkcs8.Decrypt(ciphertext);
            Assert.IsTrue(m_ContentShort.SequenceEqual(plaintext),
                $"Encrypt Short: [{string.Join(" ", m_ContentShort)}],[{string.Join(" ", plaintext)}]");

            ciphertext = m_PublicCipherX509.EncryptHybrid(m_ContentShort);
            plaintext = m_PrivateCipherPkcs8.DecryptHybrid(ciphertext);
            Assert.IsTrue(m_ContentShort.SequenceEqual(plaintext),
                $"Hybrid Long: [{string.Join(" ", m_ContentShort)}],[{string.Join(" ", plaintext)}]");

            ciphertext = m_PublicCipherX509.Encrypt(m_ContentLong);
            plaintext = m_PrivateCipherPkcs8.Decrypt(ciphertext);
            Assert.IsTrue(m_ContentLong.SequenceEqual(plaintext),
                $"Encrypt Short: [{string.Join(" ", m_ContentLong)}],[{string.Join(" ", plaintext)}]");

            ciphertext = m_PublicCipherX509.EncryptHybrid(m_ContentLong);
            plaintext = m_PrivateCipherPkcs8.DecryptHybrid(ciphertext);
            Assert.IsTrue(m_ContentLong.SequenceEqual(plaintext),
                $"Hybrid Long: [{string.Join(" ", m_ContentLong)}],[{string.Join(" ", plaintext)}]");
        }

        [Test, Order(2)]
        public void TestEncryptShort()
        {
            byte[] ciphertext;
            byte[] plaintext;

            ciphertext = m_PublicCipherPkcs1.Encrypt(m_ContentShort);
            plaintext = m_PrivateCipherPkcs1.Decrypt(ciphertext);
            Assert.IsTrue(m_ContentShort.SequenceEqual(plaintext),
                $"Encrypt Short: [{string.Join(" ", m_ContentShort)}],[{string.Join(" ", plaintext)}]");

            ciphertext = m_PublicCipherPkcs1.EncryptHybrid(m_ContentShort);
            plaintext = m_PrivateCipherPkcs1.DecryptHybrid(ciphertext);
            Assert.IsTrue(m_ContentShort.SequenceEqual(plaintext),
                $"Hybrid Long: [{string.Join(" ", m_ContentShort)}],[{string.Join(" ", plaintext)}]");
        }

        [Test, Order(3)]
        public void TestEncryptLong()
        {
            byte[] ciphertext;
            byte[] plaintext;

            ciphertext = m_PublicCipherPkcs1.Encrypt(m_ContentLong);
            plaintext = m_PrivateCipherPkcs1.Decrypt(ciphertext);
            Assert.IsTrue(m_ContentLong.SequenceEqual(plaintext),
                $"Encrypt Long: [{string.Join(" ", m_ContentLong)}],[{string.Join(" ", plaintext)}]");

            ciphertext = m_PublicCipherPkcs1.EncryptHybrid(m_ContentLong);
            plaintext = m_PrivateCipherPkcs1.DecryptHybrid(ciphertext);
            Assert.IsTrue(m_ContentLong.SequenceEqual(plaintext),
                $"Hybrid Long: [{string.Join(" ", m_ContentLong)}],[{string.Join(" ", plaintext)}]");
        }

        [Test, Order(4)]
        public void TestKeySign()
        {
            byte[] signed;
            bool isValid;

            signed = m_PrivateCipherPkcs1.Sign(m_ContentShort);
            isValid = m_PublicCipherPkcs1.VerifySign(m_ContentShort, signed);
            Assert.IsTrue(isValid);

            signed = m_PrivateCipherPkcs1.Sign(m_ContentLong);
            isValid = m_PublicCipherPkcs1.VerifySign(m_ContentLong, signed);
            Assert.IsTrue(isValid);
        }

        [Test, Order(5)]
        public void TestKeySignHash()
        {
            var hashs = new[]
                { HashAlgorithmName.MD5, HashAlgorithmName.SHA1, HashAlgorithmName.SHA256, HashAlgorithmName.SHA384, HashAlgorithmName.SHA512 };
            foreach (var hashAlgorithmName in hashs)
            {
                var signed = m_PrivateCipherPkcs1.SignHash(m_ContentShort, hashAlgorithmName);
                var isValid = m_PublicCipherPkcs1.VerifySignHash(m_ContentShort, signed, hashAlgorithmName);
                Assert.IsTrue(isValid);
                signed = m_PrivateCipherPkcs1.SignHash(m_ContentLong, hashAlgorithmName);
                isValid = m_PublicCipherPkcs1.VerifySignHash(m_ContentLong, signed, hashAlgorithmName);
                Assert.IsTrue(isValid);
            }
        }
    }
}