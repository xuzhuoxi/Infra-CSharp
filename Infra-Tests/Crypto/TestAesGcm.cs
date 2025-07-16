using System;
using System.Security.Cryptography;
using System.Text;
using JLGames.Infra.Crypto;
using JLGames.Infra.Crypto.Symmetric;

namespace JLGames.InfraTests.Crypto;

[TestFixture]
public class TestAesGcm
{
    public struct GcmCaseItem
    {
        public byte[] Plaintext { get; internal set; }
        public byte[] Key { get; internal set; }
        public byte[] Nonce { get; internal set; }
        public byte[] Ciphertext { get; internal set; }
        public byte[] Tag { get; internal set; }
        public byte[] FullCiphertext => CryptoUtils.Combine(Ciphertext, Tag);
        public byte[] Decrypted { get; internal set; }

        public override string ToString()
        {
            string pStr = $"Plaintext: [{string.Join(", ", Plaintext)}] | {Encoding.UTF8.GetString(Plaintext)}";
            string kStr = $"Key: [{string.Join(", ", Key)}]";
            string nStr = $"Nonce: [{string.Join(", ", Nonce)}]";
            string cStr = $"Ciphertext: [{string.Join(", ", Ciphertext)}] | {Encoding.UTF8.GetString(Ciphertext)}";
            string dStr = $"Decrypted: [{string.Join(", ", Decrypted)}] | {Encoding.UTF8.GetString(Decrypted)}";
            return $"{pStr}\n{kStr}\n{nStr}\n{cStr}\n{dStr}";
        }
    }

    private static readonly GcmCaseItem s_DecryptGcmCases = new GcmCaseItem
    {
        Plaintext = Encoding.UTF8.GetBytes("Hello AES-GCM from Go!"),
        Key = new byte[]
        {
            135, 212, 233, 21, 202, 155, 146, 54, 253, 253, 255, 180, 3, 2, 226, 187,
            123, 139, 127, 21, 83, 161, 178, 118, 16, 70, 125, 52, 104, 162, 126, 104
        },
        Nonce = new byte[]
        {
            33, 238, 28, 226, 226, 77, 34, 1, 98, 75, 255, 170
        },
        Ciphertext = new byte[]
        {
            129, 242, 236, 122, 16, 8, 45, 209, 164, 108, 53, 143, 171, 128, 142, 28,
            136, 198, 17, 189, 167, 54
        },
        Tag = new byte[]
        {
            65, 98, 176, 212, 202, 78, 100, 229, 235, 167, 62, 197, 83, 193, 222, 148
        },
    };

    private static readonly GcmCaseItem[] s_Cases =
    {
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Hello"),
            Key = Encoding.UTF8.GetBytes("0123456789abcdef"),
            Nonce = Encoding.UTF8.GetBytes("abcdef012345"),
            Ciphertext = new byte[] { 20, 247, 177, 128, 136 }, // 示例: 前面部分为密文
            Tag = new byte[] { 215, 102, 217, 203, 145, 217, 135, 37, 244, 116, 156, 127, 196, 65, 102, 246 }, // 示例: 后16字节为Tag
            Decrypted = Encoding.UTF8.GetBytes("Hello")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("AES-GCM Test"),
            Key = Encoding.UTF8.GetBytes("00112233445566778899aabbccddeeff"),
            Nonce = Encoding.UTF8.GetBytes("112233445566"),
            Ciphertext = new byte[] { 133, 183, 139, 186, 149, 215, 119, 19, 127, 218, 133, 157 }, // 示例分割
            Tag = new byte[] { 22, 82, 34, 7, 166, 81, 133, 173, 193, 217, 48, 200, 126, 163, 136, 16 },
            Decrypted = Encoding.UTF8.GetBytes("AES-GCM Test")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Short"),
            Key = Encoding.UTF8.GetBytes("abcdefabcdefabcdefabcdefabcdefab"),
            Nonce = Encoding.UTF8.GetBytes("123456789012"),
            Ciphertext = new byte[] { 107, 226, 65, 100, 30 },
            Tag = new byte[] { 112, 222, 199, 171, 108, 214, 224, 123, 7, 157, 135, 165, 117, 98, 34, 208 },
            Decrypted = Encoding.UTF8.GetBytes("Short")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes(""),
            Key = Encoding.UTF8.GetBytes("000102030405060708090a0b0c0d0e0f"),
            Nonce = Encoding.UTF8.GetBytes("aabbccddeeff"),
            Ciphertext = new byte[] { },
            Tag = new byte[] { 235, 142, 20, 13, 64, 110, 194, 203, 102, 24, 66, 142, 254, 248, 90, 112 },
            Decrypted = Encoding.UTF8.GetBytes("")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Max Key Length"),
            Key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef"),
            Nonce = Encoding.UTF8.GetBytes("cafebabeface"),
            Ciphertext = new byte[] { 73, 43, 238, 134, 249, 35, 77, 217, 212, 129, 97, 40, 158, 66 },
            Tag = new byte[] { 90, 202, 10, 247, 85, 141, 215, 186, 13, 97, 238, 88, 5, 34, 136, 164 },
            Decrypted = Encoding.UTF8.GetBytes("Max Key Length")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Nonce Variation 1"),
            Key = Encoding.UTF8.GetBytes("00112233445566778899aabbccddeeff"),
            Nonce = Encoding.UTF8.GetBytes("111111111111"),
            Ciphertext = new byte[] { 174, 255, 118, 1, 231, 49, 47, 28, 11, 189, 149, 213, 225, 99, 77, 176, 105 },
            Tag = new byte[] { 52, 9, 42, 80, 142, 19, 128, 45, 52, 204, 161, 37, 253, 138, 83, 245 },
            Decrypted = Encoding.UTF8.GetBytes("Nonce Variation 1")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Nonce Variation 2"),
            Key = Encoding.UTF8.GetBytes("00112233445566778899aabbccddeeff"),
            Nonce = Encoding.UTF8.GetBytes("222222222222"),
            Ciphertext = new byte[] { 168, 181, 164, 216, 204, 48, 140, 185, 184, 201, 21, 106, 7, 22, 116, 219, 124 },
            Tag = new byte[] { 100, 121, 9, 234, 106, 90, 119, 32, 69, 156, 82, 94, 145, 100, 57, 1 },
            Decrypted = Encoding.UTF8.GetBytes("Nonce Variation 2")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Longer message to test block size handling and padding"),
            Key = Encoding.UTF8.GetBytes("abcdefabcdefabcdefabcdefabcdefab"),
            Nonce = Encoding.UTF8.GetBytes("0f1e2d3c4b5a"),
            Ciphertext = new byte[]
            {
                223, 62, 71, 180, 201, 51, 61, 201, 20, 238, 63, 150, 207, 117, 44, 62, 153, 83, 71,
                55, 31, 238, 165, 143, 138, 66, 159, 166, 84, 217, 137, 56, 89, 34, 172, 49, 200,
                69, 216, 211, 185, 239, 155, 57, 72, 235, 53, 237, 71, 200, 154, 192, 66, 188
            },
            Tag = new byte[] { 152, 41, 27, 239, 79, 103, 121, 81, 87, 92, 108, 111, 72, 45, 249, 174 },
            Decrypted = Encoding.UTF8.GetBytes("Longer message to test block size handling and padding")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Special chars !@#$%^&*()"),
            Key = Encoding.UTF8.GetBytes("9876543210abcdef9876543210abcdef"),
            Nonce = Encoding.UTF8.GetBytes("abcdef123456"),
            Ciphertext = new byte[]
            {
                64, 96, 89, 232, 119, 105, 104, 27, 10, 217, 23, 206, 92, 164, 111, 160, 134, 21,
                203, 81, 75, 195, 77, 197
            },
            Tag = new byte[] { 131, 198, 49, 126, 137, 3, 64, 21, 23, 225, 87, 120, 135, 220, 68, 66 },
            Decrypted = Encoding.UTF8.GetBytes("Special chars !@#$%^&*()")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("中文测试"),
            Key = Encoding.UTF8.GetBytes("1234567890abcdef1234567890abcdef"),
            Nonce = Encoding.UTF8.GetBytes("facefeedbeef"),
            Ciphertext = new byte[] { 31, 166, 71, 112, 79, 169, 43, 213, 92, 99, 228, 190 },
            Tag = new byte[] { 215, 148, 92, 195, 171, 132, 211, 80, 120, 85, 252, 180, 123, 8, 58, 63 },
            Decrypted = Encoding.UTF8.GetBytes("中文测试")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("123"),
            Key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef"),
            Nonce = Encoding.UTF8.GetBytes("777777777777"),
            Ciphertext = new byte[] { 226, 160, 44 },
            Tag = new byte[] { 202, 90, 113, 40, 91, 37, 180, 0, 159, 69, 93, 225, 160, 213, 50, 197 },
            Decrypted = Encoding.UTF8.GetBytes("123")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Long long long long long long long long text"),
            Key = Encoding.UTF8.GetBytes("99999999999999999999999999999999"),
            Nonce = Encoding.UTF8.GetBytes("888888888888"),
            Ciphertext = new byte[]
            {
                64, 73, 28, 44, 240, 184, 13, 225, 148, 39, 113, 196, 162, 204, 10, 114, 13, 245,
                103, 30, 188, 119, 154, 131, 130, 192, 205, 74, 191, 80, 3, 251, 84, 201, 226, 161,
                15, 197, 28, 104, 15, 194, 192, 64
            },
            Tag = new byte[] { 225, 213, 168, 163, 4, 41, 115, 53, 149, 161, 91, 0, 120, 79, 221, 86 },
            Decrypted = Encoding.UTF8.GetBytes("Long long long long long long long long text")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Same nonce, different keys"),
            Key = Encoding.UTF8.GetBytes("01010101010101010101010101010101"),
            Nonce = Encoding.UTF8.GetBytes("999999999999"),
            Ciphertext = new byte[]
                { 55, 225, 90, 79, 129, 172, 48, 35, 69, 169, 147, 186, 11, 66, 41, 175, 3, 171, 28, 195, 74, 5, 178, 78, 125, 151 },
            Tag = new byte[] { 99, 112, 179, 25, 132, 235, 138, 50, 81, 2, 126, 197, 174, 237, 66, 155 },
            Decrypted = Encoding.UTF8.GetBytes("Same nonce, different keys")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Same key, different nonce"),
            Key = Encoding.UTF8.GetBytes("01010101010101010101010101010101"),
            Nonce = Encoding.UTF8.GetBytes("aaaaaaaaaaaa"),
            Ciphertext = new byte[]
                { 181, 3, 37, 55, 32, 165, 67, 12, 199, 127, 193, 97, 191, 204, 234, 89, 6, 16, 34, 254, 225, 208, 1, 140, 123 },
            Tag = new byte[] { 130, 244, 17, 78, 218, 184, 181, 72, 32, 176, 21, 142, 28, 243, 59, 14 },
            Decrypted = Encoding.UTF8.GetBytes("Same key, different nonce")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Testing zero key"),
            Key = new byte[16],
            Nonce = Encoding.UTF8.GetBytes("bbbbbbbbbbbb"),
            Ciphertext = new byte[] { 184, 208, 207, 17, 234, 86, 131, 77, 190, 59, 216, 27, 250, 78, 168, 188 },
            Tag = new byte[] { 9, 195, 69, 156, 142, 73, 82, 109, 168, 90, 237, 141, 54, 238, 129, 94 },
            Decrypted = Encoding.UTF8.GetBytes("Testing zero key")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Testing zero nonce"),
            Key = Encoding.UTF8.GetBytes("cccccccccccccccccccccccccccccccc"),
            Nonce = new byte[12],
            Ciphertext = new byte[] { 96, 144, 134, 199, 14, 166, 87, 235, 95, 200, 190, 92, 28, 5, 78, 47, 144, 14 },
            Tag = new byte[] { 164, 131, 215, 167, 64, 8, 128, 160, 10, 142, 192, 181, 84, 242, 214, 39 },
            Decrypted = Encoding.UTF8.GetBytes("Testing zero nonce")
        },
        new GcmCaseItem
        {
            Plaintext = Encoding.UTF8.GetBytes("Edge case: single byte"),
            Key = Encoding.UTF8.GetBytes("aabbccddeeff00112233445566778899"),
            Nonce = Encoding.UTF8.GetBytes("555555555555"),
            Ciphertext = new byte[] { 184, 176, 247, 15, 127, 38, 235, 235, 144, 31, 145, 135, 119, 183, 246, 18, 144, 83, 157, 51, 192, 136 },
            Tag = new byte[] { 32, 3, 164, 50, 221, 200, 226, 29, 214, 126, 94, 175, 49, 150, 27, 247 },
            Decrypted = Encoding.UTF8.GetBytes("Edge case: single byte")
        },
    };

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
    public void TestCiphertext1()
    {
        bool valid = AesGcmEngine.Default.Decrypt(s_DecryptGcmCases.Ciphertext, s_DecryptGcmCases.Key, s_DecryptGcmCases.Nonce, s_DecryptGcmCases.Tag,
            out byte[] decryptedBytes);
        if (valid)
            Console.WriteLine($"Decrypted: {Encoding.UTF8.GetString(decryptedBytes)}");
        else
            Console.WriteLine("Tag verification failed. Decryption aborted.");
    }

    [Test]
    public void TestCiphertext2()
    {
        for (var i = 0; i < s_Cases.Length; i++)
        {
            var caseValue = s_Cases[i];
            bool valid = AesGcmEngine.Default.Decrypt(caseValue.Ciphertext, caseValue.Key, caseValue.Nonce, caseValue.Tag,
                out byte[] decryptedBytes);
            if (!valid)
                Console.WriteLine($"[{i}]: Tag verification failed. Decryption aborted.");
        }
    }

    [Test]
    public void TestDebugCiphertext()
    {
        for (var index = 0; index < s_Cases.Length; index++)
        {
            var caseValue = s_Cases[index];
            AesGcmEngine.Default.Encrypt(caseValue.Plaintext, caseValue.Key, caseValue.Nonce, out byte[] cipherBytes, out byte[] tag);
            bool valid = AesGcmEngine.Default.Decrypt(caseValue.Ciphertext, caseValue.Key, caseValue.Nonce, caseValue.Tag,
                out byte[] decryptedBytes);
            if (!valid)
            {
                Console.WriteLine($"[{index}]'{Encoding.UTF8.GetString(caseValue.Plaintext)}': Tag verification failed. Decryption aborted.");
                Console.WriteLine($"Nonce: \t\t[{string.Join(" ", caseValue.Nonce)}]");
                Console.WriteLine($"Plaintext: \t\t[{string.Join(" ", caseValue.Plaintext)}]");
                Console.WriteLine($"Decrypted: \t\t[{string.Join(" ", decryptedBytes)}]");
                Console.WriteLine($"Ciphertext: \t[{string.Join(" ", caseValue.Ciphertext)}]");
                Console.WriteLine($"CCiphertext: \t[{string.Join(" ", cipherBytes)}]");
                Console.WriteLine($"Tag: \t\t[{string.Join(" ", caseValue.Tag)}]");
                Console.WriteLine($"CTag: \t\t[{string.Join(" ", tag)}]");
                Console.WriteLine();
            }
        }
    }

    [Test]
    public void TestTemp()
    {
        var caseValue = s_Cases[14];
        AesGcmEngine.Default.Encrypt(caseValue.Plaintext, caseValue.Key, caseValue.Nonce, out byte[] cipherBytes, out byte[] tag);
        bool valid = AesGcmEngine.Default.Decrypt(caseValue.Ciphertext, caseValue.Key, caseValue.Nonce, caseValue.Tag,
            out byte[] decryptedBytes);
        if (!valid)
        {
            Console.WriteLine($"[{14}]'{Encoding.UTF8.GetString(caseValue.Plaintext)}': Tag verification failed. Decryption aborted.");
            Console.WriteLine($"Nonce: \t\t[{string.Join(" ", caseValue.Nonce)}]");
            Console.WriteLine($"Plaintext: \t\t[{string.Join(" ", caseValue.Plaintext)}]");
            Console.WriteLine($"Decrypted: \t\t[{string.Join(" ", decryptedBytes)}]");
            Console.WriteLine($"Ciphertext: \t[{string.Join(" ", caseValue.Ciphertext)}]");
            Console.WriteLine($"CCiphertext: \t[{string.Join(" ", cipherBytes)}]");
            Console.WriteLine($"Tag: \t\t[{string.Join(" ", caseValue.Tag)}]");
            Console.WriteLine($"CTag: \t\t[{string.Join(" ", tag)}]");
            Console.WriteLine();
        }
    }
}