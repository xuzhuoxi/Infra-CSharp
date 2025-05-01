using System;
using System.Linq;
using System.Text;
using JLGames.Infra.Crypto.Symmetric;

namespace Infra_Tests
{
    [TestFixture]
    public class TestXor
    {
        private static readonly byte[] m_Key = Encoding.UTF8.GetBytes("MySecretKey123456");
        private static readonly byte[] m_Plaintext = Encoding.UTF8.GetBytes("Hello, World!");
        private static readonly byte[] m_BigData = new byte[10 * 1024 * 1024];

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var rand = new Random();
            rand.NextBytes(m_BigData);
        }

        [Test, Order(1)]
        public void TestXorData()
        {
            var cipher = new XorCipher(m_Key);
            var obfuscated = cipher.Encrypt(m_Plaintext);
            var deobfuscated = cipher.Decrypt(obfuscated);
            Assert.IsTrue(m_Plaintext.SequenceEqual(deobfuscated),
                $"Expected {BitConverter.ToString(m_Plaintext)} but got {BitConverter.ToString(deobfuscated)}");
        }

        [Test, Order(2)]
        public void TestXorBigData()
        {
            var cipher = new XorCipher(m_Key);
            var obfuscated = cipher.Encrypt(m_BigData);
            var deobfuscated = cipher.Decrypt(obfuscated);
            Assert.IsTrue(m_BigData.SequenceEqual(deobfuscated),
                $"Expected {BitConverter.ToString(m_BigData)} but got {BitConverter.ToString(deobfuscated)}");
        }
    }
}