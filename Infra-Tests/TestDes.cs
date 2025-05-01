using System;
using System.Linq;
using System.Text;
using JLGames.Infra.Crypto;
using JLGames.Infra.Crypto.Symmetric;

namespace Infra_Tests
{
    [TestFixture]
    public class TestDes
    {
        private class DesCase
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
                if (null != Iv) sb.AppendLine($"IV:{{bs=[{string.Join(" ", Iv)}]}};");
                sb.AppendLine($"Plaintext:{{bs=[{string.Join(" ", Plaintext)}]}};");
                sb.AppendLine($"Ciphertext:{{bs=[{string.Join(" ", Ciphertext)}]}};");
                sb.AppendLine($"Plaintext2:{{bs=[{string.Join(" ", Plaintext2)}]}}");
                return sb.ToString();
            }
        }


        private static class DesIvTests
        {
            // key长度为8 iv长度为8
            public static DesCase[] IvCases =
            {
                new()
                {
                    Key = new byte[]
                    {
                        243, 214, 215, 220, 74, 207, 231, 243
                    },
                    Iv = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!"),
                },
                new()
                {
                    Key = new byte[]
                    {
                        89, 173, 213, 90, 95, 254, 113, 73
                    },
                    Iv = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!6666"),
                }
            };

            // key长度为23 iv长度为8
            public static DesCase[] Iv3Cases =
            {
                new()
                {
                    Key = new byte[]
                    {
                        138, 9, 184, 208, 86, 158, 16, 32, 156, 239, 117, 48, 143, 226, 239, 85, 92, 114, 191, 33, 68, 182, 38, 116
                    },
                    Iv = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!"),
                },
                new()
                {
                    Key = new byte[]
                    {
                        49, 222, 2, 183, 220, 23, 102, 190, 204, 254, 151, 76, 32, 243, 157, 244, 86, 108, 112, 48, 25, 62, 29, 14
                    },
                    Iv = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef },
                    Plaintext = Encoding.UTF8.GetBytes("Hello World!6666"),
                }
            };

            public static BlockMode[] Modes = { BlockMode.ECB, BlockMode.CBC, BlockMode.CTR };
        }

        [Test, Order(1)]
        public void TestDesIvCases()
        {
            foreach (var @case in DesIvTests.IvCases)
            {
                foreach (var blockMode in DesIvTests.Modes)
                {
                    TestIvCase(@case, blockMode);
                }

                Console.WriteLine("---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------");
            }
        }

        [Test, Order(1)]
        public void TestDesIvCases3()
        {
            foreach (var @case in DesIvTests.Iv3Cases)
            {
                foreach (var blockMode in DesIvTests.Modes)
                {
                    TestIvCase3(@case, blockMode);
                }

                Console.WriteLine("---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------");
            }
        }

        private void TestIvCase(DesCase value, BlockMode mode)
        {
            var cipher = new DesCipher(value.Key);
            var ciphertext = cipher.EncryptMode(value.Plaintext, value.Iv, mode);
            var plaintext2 = cipher.DecryptMode(ciphertext, value.Iv, mode);
            value.Ciphertext = ciphertext;
            value.Plaintext2 = plaintext2;
            Console.WriteLine($"{mode}:");
            Console.WriteLine(value);
            Assert.IsTrue(value.Plaintext.SequenceEqual(plaintext2),
                $"Mode {mode}, Expected {ArrayToString(value.Plaintext)} \nbut got {ArrayToString(plaintext2)}");
        }

        private void TestIvCase3(DesCase value, BlockMode mode)
        {
            var cipher = new TripleDesCipher(value.Key);
            var ciphertext = cipher.EncryptMode(value.Plaintext, value.Iv, mode);
            var plaintext2 = cipher.DecryptMode(ciphertext, value.Iv, mode);
            value.Ciphertext = ciphertext;
            value.Plaintext2 = plaintext2;
            Console.WriteLine($"{mode}:");
            Console.WriteLine(value);
            Assert.IsTrue(value.Plaintext.SequenceEqual(plaintext2),
                $"Mode {mode}, Expected {ArrayToString(value.Plaintext)} \nbut got {ArrayToString(plaintext2)}");
        }

        private string ArrayToString<T>(T[] array)
        {
            return "[" + string.Join(" ", array) + "]";
        }
    }
}