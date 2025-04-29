using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// IAESCipher 接口
    /// AES：Advanced Encryption Standard（高级加密标准），对应算法 Rijndael
    /// 特点：
    /// 1. 对称加密
    /// 2. 一个密钥扩展成多个子密钥，多轮加密
    /// </summary>
    public interface IAesCipher : ICipher
    {
        /// <summary>
        /// 获取密钥（只读）
        /// </summary>
        byte[] Key { get; }

        /// <summary>
        /// BlockSize
        /// </summary>
        int BlockSize { get; }

        /// <summary>
        /// 设置填充模式
        /// </summary>
        /// <param name="paddingMode">填充模式</param>
        void SetPadding(PaddingMode paddingMode);

        /// <summary>
        /// 指定 BlockMode 加密
        /// </summary>
        byte[] EncryptMode(byte[] plaintext, BlockMode blockMode);

        /// <summary>
        /// 指定 BlockMode 加密
        /// </summary>
        byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode);

        /// <summary>
        /// 指定 BlockMode 解密
        /// </summary>
        byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode);

        /// <summary>
        /// 指定 BlockMode 解密
        /// </summary>
        byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode);

        /// <summary>
        /// CBC 模式加密
        /// </summary>
        byte[] EncryptCbc(byte[] plaintext);

        /// <summary>
        /// CBC 模式加密
        /// </summary>
        byte[] EncryptCbc(byte[] plaintext, byte[] iv);

        /// <summary>
        /// CBC 模式解密
        /// </summary>
        byte[] DecryptCbc(byte[] ciphertext);

        /// <summary>
        /// CBC 模式解密
        /// </summary>
        byte[] DecryptCbc(byte[] ciphertext, byte[] iv);

        /// <summary>
        /// CTR 模式加密
        /// </summary>
        byte[] EncryptCtr(byte[] plaintext);

        /// <summary>
        /// CTR 模式加密
        /// </summary>
        byte[] EncryptCtr(byte[] plaintext, byte[] iv);

        /// <summary>
        /// CTR 模式解密
        /// </summary>
        byte[] DecryptCtr(byte[] ciphertext);

        /// <summary>
        /// CTR 模式解密
        /// </summary>
        byte[] DecryptCtr(byte[] ciphertext, byte[] iv);

        /// <summary>
        /// GCM 模式加密
        /// </summary>
        byte[] EncryptGcm(byte[] plaintext);

        /// <summary>
        /// GCM 模式加密
        /// </summary>
        byte[] EncryptGcm(byte[] plaintext, byte[] nonce);

        /// <summary>
        /// GCM 模式解密
        /// </summary>
        byte[] DecryptGcm(byte[] ciphertext);

        /// <summary>
        /// GCM 模式解密
        /// </summary>
        byte[] DecryptGcm(byte[] ciphertext, byte[] nonce);
    }
}