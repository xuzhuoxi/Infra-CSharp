using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using JLGames.Infra.Crypto.Symmetric;

namespace JLGames.InfraTests.Crypto;

[TestFixture]
public class TestAesCtr
{
    public struct CtrCaseItem
    {
        public byte[] Plaintext { get; internal set; }
        public byte[] Key { get; internal set; }
        public byte[] Nonce { get; internal set; }
        public byte[] Counter { get; internal set; }
        public byte[] Ciphertext { get; internal set; }
        public byte[] Decrypted { get; internal set; }

        public override string ToString()
        {
            string pStr = $"Plaintext: [{string.Join(", ", Plaintext)}] | {Encoding.UTF8.GetString(Plaintext)}";
            string kStr = $"Key: [{string.Join(", ", Key)}]";
            // string kStr = BitConverter.ToString(Key).Replace("-", ""); // hex形式可选
            string nStr = $"Nonce: [{string.Join(", ", Nonce)}]";
            // string nStr = BitConverter.ToString(Nonce).Replace("-", ""); // hex形式可选
            string cStr = $"Ciphertext: [{string.Join(", ", Ciphertext)}] | {Encoding.UTF8.GetString(Ciphertext)}";
            string dStr = $"Decrypted: [{string.Join(", ", Decrypted)}] | {Encoding.UTF8.GetString(Decrypted)}";

            return $"{pStr}\n{kStr}\n{nStr}\n{cStr}\n{dStr}";
        }
    }

    private static readonly CtrCaseItem s_DemoCase = new()
    {
        Plaintext = Encoding.UTF8.GetBytes("Hello AES-CTR from Go!"),
        Key = new byte[]
        {
            171, 145, 30, 202, 108, 201, 19, 35, 72, 50, 210, 129, 250, 80, 25, 96,
            135, 61, 24, 141, 48, 27, 200, 107, 95, 4, 240, 93, 74, 127, 22, 208
        },
        Nonce = new byte[] { 188, 93, 26, 190, 198, 36, 25, 25, 115, 49, 208, 117 },
        Ciphertext = new byte[]
        {
            25, 166, 2, 181, 129, 251, 167, 211, 152, 185, 167, 224,
            84, 86, 161, 225, 65, 217, 34, 211, 44, 89
        }
    };

    private static readonly CtrCaseItem s_NoNonceCase = new()
    {
        Plaintext = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 },
        Key = new byte[]
        {
            0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
            0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
            0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,
            0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef
        }
    };

    // 固定密钥（128位）和 Nonce（12字节）
    private static readonly byte[] s_FixedKey = new byte[16]
    {
        0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
        0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
    };

    private static readonly byte[] s_FixedNonce = new byte[12] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66 };

    private static readonly List<CtrCaseItem> s_UseCases = new()
    {
        // 1️⃣ 标准短文本
        new CtrCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Hello AES-CTR"),
            Key = s_FixedKey,
            Nonce = s_FixedNonce,
            Ciphertext = new byte[] { 223, 169, 30, 237, 220, 68, 46, 212, 201, 210, 102, 198, 157 }
        },

        // 2️⃣ 空明文
        new CtrCaseItem
        {
            Plaintext = Array.Empty<byte>(),
            Key = s_FixedKey,
            Nonce = s_FixedNonce,
            Ciphertext = Array.Empty<byte>()
        },

        // 3️⃣ 多块数据（128字节）
        new CtrCaseItem
        {
            Plaintext = Enumerable.Repeat((byte)'A', 128).ToArray(),
            Key = s_FixedKey,
            Nonce = s_FixedNonce,
            Ciphertext = new byte[]
            {
                214, 141, 51, 192, 242, 37, 46, 208, 219, 190, 100, 211, 142, 124, 7, 3,
                173, 1, 69, 212, 78, 129, 39, 155, 237, 103, 37, 144, 20, 130, 69, 241,
                224, 246, 73, 10, 118, 202, 39, 129, 220, 151, 16, 95, 199, 78, 129, 101,
                100, 189, 48, 254, 16, 103, 92, 60, 221, 246, 214, 72, 131, 175, 233, 255,
                18, 91, 159, 182, 63, 55, 125, 247, 140, 249, 129, 160, 35, 34, 132, 198,
                51, 208, 131, 12, 39, 162, 214, 78, 235, 30, 201, 172, 223, 237, 156, 138,
                141, 90, 68, 179, 147, 93, 80, 57, 149, 65, 204, 40, 90, 45, 211, 83,
                247, 81, 188, 73, 31, 201, 124, 237, 202, 251, 13, 191, 191, 65, 191, 4,
            }
        },

        // 4️⃣ 边界长度：31字节非对齐长度
        new CtrCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("This message is 31 bytes long!!!"),
            Key = s_FixedKey,
            Nonce = s_FixedNonce,
            Ciphertext = new byte[]
            {
                195, 164, 27, 242, 147, 9, 10, 226, 233, 158, 66, 247, 239, 84, 53, 98, 223, 113, 36, 247, 118, 180, 3, 169, 140, 74, 11, 191, 50,
                226, 37, 145
            }
        }
    };

    [Test]
    public void TestAesCtrDemo()
    {
        byte[] key = new byte[32]; // AES-256
        byte[] nonce = new byte[12]; // 推荐12字节，与 GCM/Go 保持一致
        RandomNumberGenerator.Fill(key);
        RandomNumberGenerator.Fill(nonce);

        string message = "Hello AES-CTR from C#!";
        byte[] plaintext = System.Text.Encoding.UTF8.GetBytes(message);

        // 加密
        byte[] ciphertext = AesCtrEngine.Default.ProcessWithNonce(plaintext, key, nonce);

        // 解密（CTR 模式对称）
        byte[] decrypted = AesCtrEngine.Default.ProcessWithNonce(ciphertext, key, nonce);
        string result = System.Text.Encoding.UTF8.GetString(decrypted);

        Console.WriteLine($"Decrypted: {result}");
    }

    [Test]
    public void TestAesCtrDecrypt()
    {
        byte[] decrypted = AesCtrEngine.Default.ProcessWithNonce(s_DemoCase.Ciphertext, s_DemoCase.Key, s_DemoCase.Nonce);
        string result = System.Text.Encoding.UTF8.GetString(decrypted);

        Console.WriteLine($"Decrypted: [{string.Join(" ", decrypted)}] | {result}");
    }

    [Test]
    public void TestAesCtrRandom()
    {
        byte[] ciphertext = AesCtrEngine.Default.EncryptRandom(s_NoNonceCase.Plaintext, s_NoNonceCase.Key);
        byte[] decrypted = AesCtrEngine.Default.DecryptRandom(ciphertext, s_NoNonceCase.Key);
        Assert.That(decrypted, Is.EqualTo(s_NoNonceCase.Plaintext));
    }

    [Test]
    public void TestAesCtrUseCases()
    {
        for (var index = 0; index < s_UseCases.Count; index++)
        {
            var useCase = s_UseCases[index];
            var plaintext = AesCtrEngine.Default.ProcessWithNonce(useCase.Ciphertext, useCase.Key, useCase.Nonce);
            // TestContext.Progress.WriteLine($"\nUseCase index: {index}");
            // TestContext.Progress.WriteLine($"Original: [{string.Join(" ", useCase.Plaintext)}]");
            // TestContext.Progress.WriteLine($"Compute: [{string.Join(" ", plaintext)}]");
            Assert.That(plaintext, Is.EqualTo(useCase.Plaintext));
        }
    }
}