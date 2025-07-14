using System;
using System.Security.Cryptography;
using System.Text;
using JLGames.Infra.Crypto.Symmetric;

namespace JLGames.InfraTests.Crypto;

[TestFixture]
public class TestAes2
{
    private byte[] m_GcmPlaintext = Encoding.UTF8.GetBytes("Hello AES-GCM from Go!");

    private readonly byte[] m_GcmKey =
    {
        135, 212, 233, 21, 202, 155, 146, 54, 253, 253, 255, 180, 3, 2, 226, 187,
        123, 139, 127, 21, 83, 161, 178, 118, 16, 70, 125, 52, 104, 162, 126, 104
    };

    private readonly byte[] m_GcmNonce =
    {
        33, 238, 28, 226, 226, 77, 34, 1, 98, 75, 255, 170
    };

    private byte[] m_GcmSealCiphertext =
    {
        129, 242, 236, 122, 16, 8, 45, 209, 164, 108, 53, 143, 171, 128, 142, 28,
        136, 198, 17, 189, 167, 54, 65, 98, 176, 212, 202, 78, 100, 229, 235, 167,
        62, 197, 83, 193, 222, 148
    };

    private readonly byte[] m_GcmCiphertext =
    {
        129, 242, 236, 122, 16, 8, 45, 209, 164, 108, 53, 143, 171, 128, 142, 28,
        136, 198, 17, 189, 167, 54
    };

    private readonly byte[] m_GcmTag =
    {
        65, 98, 176, 212, 202, 78, 100, 229, 235, 167, 62, 197, 83, 193, 222, 148
    };


    [Test]
    public void TestAesGcm()
    {
        byte[] key = new byte[32]; // 256-bit key
        byte[] iv = new byte[12]; // GCM推荐长度
        RandomNumberGenerator.Fill(key);
        RandomNumberGenerator.Fill(iv);

        string plainText = "Hello AES-GCM from C#!";
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        AesGcmEngine.Default.Encrypt(plainBytes, key, iv, out byte[] cipherBytes, out byte[] tag);

        Console.WriteLine($"CipherText: {Convert.ToHexString(cipherBytes)}");
        Console.WriteLine($"Tag: {Convert.ToHexString(tag)}");


        bool valid = AesGcmEngine.Default.Decrypt(cipherBytes, key, iv, tag, out byte[] decryptedBytes);

        if (valid)
            Console.WriteLine($"Decrypted: {Encoding.UTF8.GetString(decryptedBytes)}");
        else
            Console.WriteLine("Tag verification failed. Decryption aborted.");
    }

    [Test]
    public void TestAesGcm2()
    {
        var ciphertext = m_GcmCiphertext;
        bool valid = AesGcmEngine.Default.Decrypt(ciphertext, m_GcmKey, m_GcmNonce, m_GcmTag, out byte[] decryptedBytes);
        if (valid)
            Console.WriteLine($"Decrypted: {Encoding.UTF8.GetString(decryptedBytes)}");
        else
            Console.WriteLine("Tag verification failed. Decryption aborted.");
    }

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
    public void TestAesCtr()
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
}