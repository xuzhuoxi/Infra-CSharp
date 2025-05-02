using System;
using System.Linq;
using System.Text;
using JLGames.Infra.Crypto;
using JLGames.Infra.Crypto.Symmetric;

namespace JLGames.InfraTests.Crypto
{
    [TestFixture]
    public class TestAes
    {
        private class AesCase
        {
            public byte[] Key { get; set; }
            public byte[] Iv { get; set; }
            public byte[] Plaintext { get; set; }
            public byte[] Ciphertext { get; set; }
            public byte[] Plaintext2 { get; set; }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Key:{{bs=[{string.Join(",", Key)}]}};");
                if (null != Iv) sb.AppendLine($"IV:{{bs=[{string.Join(",", Iv)}]}};");
                sb.AppendLine($"Plaintext:{{bs=[{string.Join(",", Plaintext)}]}};");
                sb.AppendLine($"Ciphertext:{{bs=[{string.Join(",", Ciphertext)}]}};");
                sb.AppendLine($"Plaintext2:{{bs=[{string.Join(",", Plaintext2)}]}}");
                return sb.ToString();
            }
        }


        private static class AesIvTests
        {
            // iv长度为16
            public static AesCase[] IvCases =
            {
                new()
                {
                    Key = new byte[]
                    {
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                    },
                    Iv = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!Hello World!Hello World!"),
                },
                new()
                {
                    Key = new byte[]
                    {
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                    },
                    Iv = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!Hello World!Hello World!666"),
                }
            };

            // iv长度为12
            public static AesCase[] IvGcmCases =
            {
                new()
                {
                    Key = new byte[]
                    {
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                    },
                    Iv = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67 },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!Hello World!Hello World!"),
                },
                new()
                {
                    Key = new byte[]
                    {
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                    },
                    Iv = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67 },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!Hello World!Hello World!666"),
                }
            };

            public static AesCase[] Cases =
            {
                new()
                {
                    Key = new byte[]
                    {
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                    },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!Hello World!Hello World!"),
                },
                new()
                {
                    Key = new byte[]
                    {
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                        0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
                    },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!Hello World!Hello World!666"),
                },
            };

            public static BlockMode[] Modes = { BlockMode.CBC, BlockMode.CTR, BlockMode.GCM };
        }


        [Test, Order(1)]
        public void TestAesIvGcmCases()
        {
            foreach (var @case in AesIvTests.IvGcmCases)
            {
                TestIvGcmCase(@case);
            }
        }

        [Test, Order(2)]
        public void TestAesIvCases()
        {
            foreach (var @case in AesIvTests.IvCases)
            {
                TestIvCase(@case, BlockMode.CBC);
                TestIvCase(@case, BlockMode.CTR);
            }
        }

        [Test, Order(3)]
        public void TestAesCases()
        {
            foreach (var blockMode in AesIvTests.Modes)
            {
                foreach (var @case in AesIvTests.Cases)
                {
                    TestAesCase(@case, blockMode);
                }
            }
        }

        private void TestIvGcmCase(AesCase value)
        {
            var cipher = new AesCipher(value.Key);
            var ciphertext = cipher.EncryptGcm(value.Plaintext, value.Iv);
            var plaintext2 = cipher.DecryptGcm(ciphertext, value.Iv);
            value.Ciphertext = ciphertext;
            value.Plaintext2 = plaintext2;
            Console.WriteLine(value);
            Assert.IsTrue(value.Plaintext.SequenceEqual(plaintext2),
                $"Mode GCM, Expected {ArrayToString(value.Plaintext)} \nbut got {ArrayToString(plaintext2)}");
            Console.WriteLine("---------- ---------- ---------- ---------- ----------");
        }

        private void TestIvCase(AesCase value, BlockMode mode)
        {
            var cipher = new AesCipher(value.Key);
            var ciphertext = cipher.EncryptMode(value.Plaintext, value.Iv, mode);
            var plaintext2 = cipher.DecryptMode(ciphertext, value.Iv, mode);
            value.Ciphertext = ciphertext;
            value.Plaintext2 = plaintext2;
            Console.WriteLine($"{mode}:");
            Console.WriteLine(value);
            Assert.IsTrue(value.Plaintext.SequenceEqual(plaintext2),
                $"Mode {mode}, Expected {ArrayToString(value.Plaintext)} \nbut got {ArrayToString(plaintext2)}");
            Console.WriteLine("---------- ---------- ---------- ---------- ----------");
        }

        private void TestAesCase(AesCase value, BlockMode mode)
        {
            var cipher = new AesCipher(value.Key);
            var ciphertext = cipher.EncryptMode(value.Plaintext, mode);
            var plaintext2 = cipher.DecryptMode(ciphertext, mode);
            value.Ciphertext = ciphertext;
            value.Plaintext2 = plaintext2;
            Console.WriteLine($"{mode}:");
            Console.WriteLine(value);
            Assert.IsTrue(value.Plaintext.SequenceEqual(plaintext2),
                $"Mode {mode}, Expected {ArrayToString(value.Plaintext)} \nbut got {ArrayToString(plaintext2)}");
        }

        private string ArrayToString<T>(T[] array)
        {
            return "[" + string.Join(",", array) + "]";
        }
    }
}