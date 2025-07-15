using System;
using System.Security.Cryptography;
using System.Text;
using JLGames.Infra.Crypto;
using JLGames.Infra.Crypto.Symmetric;

namespace JLGames.InfraTests.Crypto;

[TestFixture]
public class TestAesGcm
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

    private readonly byte[] m_GcmCiphertext =
    {
        129, 242, 236, 122, 16, 8, 45, 209, 164, 108, 53, 143, 171, 128, 142, 28,
        136, 198, 17, 189, 167, 54
    };

    private readonly byte[] m_GcmTag =
    {
        65, 98, 176, 212, 202, 78, 100, 229, 235, 167, 62, 197, 83, 193, 222, 148
    };

    private byte[] m_GcmSealCiphertext => CryptoUtils.Combine(m_GcmCiphertext, m_GcmTag);


    [Test]
    public void TestAesGcmCommon()
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
}