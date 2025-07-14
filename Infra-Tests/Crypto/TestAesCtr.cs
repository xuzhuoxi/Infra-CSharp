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
    public struct UseCaseItem
    {
        public byte[] Plaintext { get; set; }
        public byte[] Key { get; set; }
        public byte[] Nonce { get; set; }
        public byte[] Ciphertext { get; set; }
    }

    // 固定密钥（128位）和 Nonce（12字节）
    private static byte[] fixedKey = new byte[16]
    {
        0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
        0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
    };

    private static byte[] fixedNonce = new byte[12] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66 };

    private List<UseCaseItem> useCases = new()
    {
        // 1️⃣ 标准短文本
        new UseCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Hello AES-CTR"),
            Key = fixedKey,
            Nonce = fixedNonce,
            Ciphertext = new byte[]
            {
                0x8e, 0xc1, 0x41, 0x6b, 0xfa, 0x2f, 0x38, 0x16,
                0x6f, 0xbd, 0x59, 0x54, 0x8c
            }
        },

        // 2️⃣ 空明文
        new UseCaseItem
        {
            Plaintext = Array.Empty<byte>(),
            Key = fixedKey,
            Nonce = fixedNonce,
            Ciphertext = Array.Empty<byte>()
        },
        //
        // // 3️⃣ 多块数据（128字节）
        // new UseCaseItem
        // {
        //     Plaintext = Enumerable.Repeat((byte)'A', 128).ToArray(),
        //     Key = fixedKey,
        //     Nonce = fixedNonce,
        //     Ciphertext = new byte[]
        //     {
        //         // 示例首行（为简洁展示）
        //         0x48, 0x7a, 0xa3, 0x42, 0x86, 0x2a, 0xa2, 0x7c, 0x93, 0x1f, 0x1b, 0x36, 0x90, 0x52, 0x4f, 0xe5,
        //         // …其余数据略，可根据原始加密函数填充
        //     }
        // },
        //
        // 4️⃣ 边界长度：31字节非对齐长度
        new UseCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("This message is 31 bytes long!!!"),
            Key = fixedKey,
            Nonce = fixedNonce,
            Ciphertext = new byte[]
            {
                0x63, 0x6c, 0xe2, 0x89, 0x12, 0x7f, 0x88, 0xe1, 0xb1, 0x45, 0x5d,
                0x8a, 0x64, 0xa3, 0x79, 0xff, 0x11, 0x5b, 0x1c, 0x2a, 0xa4, 0x07,
                0xdc, 0x03, 0x6f, 0x4e, 0x3f, 0x92, 0xd2, 0x56, 0xe1
            }
        }
    };

    // --------------------

    private byte[] m_CtrPlaintext = Encoding.UTF8.GetBytes("Hello AES-CTR from Go!");

    private readonly byte[] m_CtrKey =
    {
        171, 145, 30, 202, 108, 201, 19, 35, 72, 50, 210, 129, 250, 80, 25, 96,
        135, 61, 24, 141, 48, 27, 200, 107, 95, 4, 240, 93, 74, 127, 22, 208
    };

    private readonly byte[] m_CtrNonce = { 188, 93, 26, 190, 198, 36, 25, 25, 115, 49, 208, 117 };

    private readonly byte[] m_CtrCiphertext =
    {
        25, 166, 2, 181, 129, 251, 167, 211, 152, 185, 167, 224,
        84, 86, 161, 225, 65, 217, 34, 211, 44, 89
    };

    [Test]
    public void TestAesCtrCommon()
    {
        byte[] key = new byte[32]; // AES-256
        byte[] nonce = new byte[12]; // 推荐12字节，与 GCM/Go 保持一致
        RandomNumberGenerator.Fill(key);
        RandomNumberGenerator.Fill(nonce);

        string message = "Hello AES-CTR from C#!";
        byte[] plaintext = System.Text.Encoding.UTF8.GetBytes(message);

        // 加密
        byte[] ciphertext = AesCtrEngine.Default.Process(plaintext, key, nonce);

        // 解密（CTR 模式对称）
        byte[] decrypted = AesCtrEngine.Default.Process(ciphertext, key, nonce);
        string result = System.Text.Encoding.UTF8.GetString(decrypted);

        Console.WriteLine($"Decrypted: {result}");
    }

    [Test]
    public void TestAesCtr2()
    {
        var ciphertext = m_CtrCiphertext;
        byte[] decrypted = AesCtrEngine.Default.Process(ciphertext, m_CtrKey, m_CtrNonce);
        string result = System.Text.Encoding.UTF8.GetString(decrypted);

        Console.WriteLine($"Decrypted: {result}");
    }

    [Test]
    public void TestAesCtrUseCases()
    {
        for (var index = 0; index < useCases.Count; index++)
        {
            var useCase = useCases[index];
            var plaintext = AesCtrEngine.Default.Process(useCase.Ciphertext, useCase.Key, useCase.Nonce);
            TestContext.Progress.WriteLine($"\nUseCase index: {index}");
            TestContext.Progress.WriteLine($"Original: [{string.Join(" ", useCase.Plaintext)}]");
            TestContext.Progress.WriteLine($"Compute: [{string.Join(" ", plaintext)}]");
            // Assert.That(plaintext, Is.EqualTo(useCase.Plaintext));
        }
    }
}