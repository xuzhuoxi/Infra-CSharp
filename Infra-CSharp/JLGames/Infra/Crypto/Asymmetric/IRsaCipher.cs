using System;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Asymmetric
{
    /// <summary>
    /// RSA public-key operations: encrypt, hybrid encrypt, verify (PKCS#1 v1.5).
    /// RSA 公钥操作：加密、混合加密与签名验证
    /// </summary>
    public interface IRsaPublicCipher : IDisposable
    {
        /// <summary>RSA public key instance. / 获取公钥</summary>
        RSA PublicKey { get; }

        /// <summary>
        /// 加密（支持分组）
        /// </summary>
        /// <param name="plaintext">原始数据</param>
        /// <returns>加密后的数据</returns>
        byte[] Encrypt(byte[] plaintext);

        /// <summary>
        /// 混合加密
        /// 步骤：
        /// 1. 不足分组长度，不生成随机AES密钥，直接使用RSA加密
        /// 2. 长于分组长度的执行以下步骤:
        ///   2.1 生成32位随机AES密钥与16位随机IV，
        ///   2.2 使用RSA加密AES密钥与IV，得到AES密钥密文
        ///   2.3 使用AES密钥与IV，使用AES-CTR算法加密明文
        ///   2.4 返回(AES密钥密文, 密文)
        /// </summary>
        /// <param name="plaintext">原始数据</param>
        /// <returns>密文数据，包含RSA加密的AES密钥与IV，以及AES加密后的密文</returns>
        byte[] EncryptHybrid(byte[] plaintext);

        /// <summary>
        /// 使用SHA256进行签名验证
        /// </summary>
        /// <param name="origData">原始数据</param>
        /// <param name="signature">签名数据</param>
        /// <returns>是否验证成功</returns>
        bool VerifySign(byte[] origData, byte[] signature);

        /// <summary>
        /// 使用SHA256进行签名验证
        /// 签名数据为Base64编码字符串
        /// </summary>
        /// <param name="origData">原始数据</param>
        /// <param name="base64Signature">Base64编码的签名数据</param>
        /// <returns>是否验证成功</returns>
        bool VerifySignBase64(byte[] origData, string base64Signature);

        /// <summary>
        /// 指定Hash算法进行签名验证
        /// 注意：MD5和SHA1不可用
        /// </summary>
        /// <param name="origData">原始数据</param>
        /// <param name="signature">签名数据</param>
        /// <param name="hashAlgorithm">Hash算法</param>
        /// <returns>是否验证成功</returns>
        bool VerifySignHash(byte[] origData, byte[] signature, HashAlgorithmName hashAlgorithm);

        /// <summary>
        /// 使用指定的Hash算法进行签名验证
        /// 签名数据为Base64编码字符串
        /// </summary>
        /// <param name="origData">原始数据</param>
        /// <param name="base64Signature">Base64编码的签名数据</param>
        /// <param name="hashAlgorithm">Hash算法</param>
        /// <returns>是否验证成功</returns>
        bool VerifySignHashBase64(byte[] origData, string base64Signature, HashAlgorithmName hashAlgorithm);
    }

    /// <summary>
    /// RSA private-key operations: decrypt, hybrid decrypt, sign (PKCS#1 v1.5).
    /// RSA 私钥操作：解密、混合解密与签名
    /// </summary>
    public interface IRsaPrivateCipher : IDisposable
    {
        /// <summary>RSA private key instance. / 获取私钥</summary>
        RSA PrivateKey { get; }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext">加密数据</param>
        /// <returns>解密后的数据</returns>
        byte[] Decrypt(byte[] ciphertext);

        /// <summary>
        /// 混合解密
        /// </summary>
        /// <param name="ciphertext">密文数据，包含RSA加密的AES密钥与IV，以及AES加密后的密文</param>
        /// <returns>解密后的数据</returns>
        byte[] DecryptHybrid(byte[] ciphertext);

        /// <summary>
        /// 使用SHA256进行签名
        /// </summary>
        /// <param name="origData">原始数据</param>
        /// <returns>签名后的数据</returns>
        byte[] Sign(byte[] origData);

        /// <summary>
        /// 使用SHA256进行签名，并将结果转化为Base64编码字符串
        /// </summary>
        /// <param name="origData">原始数据</param>
        /// <returns>签名后的Base64编码字符串</returns>
        string SignBase64(byte[] origData);

        /// <summary>
        /// 指定Hash算法进行签名
        /// 注意：MD5和SHA1不可用
        /// </summary>
        /// <param name="origData">原始数据</param>
        /// <param name="hashAlgorithm">Hash算法</param>
        /// <returns>签名后的数据</returns>
        byte[] SignHash(byte[] origData, HashAlgorithmName hashAlgorithm);

        /// <summary>
        /// 指定Hash算法进行签名，并将结果转化为Base64编码字符串
        /// 注意：MD5和SHA1不可用
        /// </summary>
        /// <param name="origData">原始数据</param>
        /// <param name="hashAlgorithm">Hash算法</param>
        /// <returns>签名后的Base64编码字符串</returns>
        string SignHashBase64(byte[] origData, HashAlgorithmName hashAlgorithm);
    }

    /// <summary>
    /// Full RSA cipher with both public and private key operations.
    /// 同时具备 RSA 公钥与私钥能力的完整接口
    /// </summary>
    public interface IRsaCipher : IRsaPrivateCipher, IRsaPublicCipher
    {
    }
}