using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// DES/3DES cipher interface (DEA algorithm).
    /// DES 对称加密接口（数据加密标准，算法 DEA）
    /// </summary>
    public interface IDesCipher : ICipher
    {
        /// <summary>Key length in bytes. / 密钥长度（字节）</summary>
        int KeySize { get; }

        /// <summary>Block size in bytes (8 for DES). / 块大小（字节）</summary>
        int BlockSize { get; }

        /// <summary>
        /// 设置填充模式
        /// </summary>
        /// <param name="paddingMode">填充模式</param>
        void SetPaddingMode(PaddingMode paddingMode);

        /// <summary>
        /// 指定 BlockMode 加密
        /// </summary>
        /// <param name="plaintext">明文数据</param>
        /// <param name="blockMode">块模式</param>
        /// <returns>加密后的数据</returns>
        byte[] EncryptMode(byte[] plaintext, BlockMode blockMode);

        /// <summary>
        /// 指定 BlockMode 加密
        /// </summary>
        /// <param name="plaintext">明文数据</param>
        /// <param name="iv"></param>
        /// <param name="blockMode">块模式</param>
        /// <returns>加密后的数据</returns>
        byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode);

        /// <summary>
        /// 指定 BlockMode 解密
        /// </summary>
        /// <param name="ciphertext">密文数据</param>
        /// <param name="blockMode">块模式</param>
        /// <returns>解密后的数据</returns>
        byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode);

        /// <summary>
        /// 指定 BlockMode 解密
        /// </summary>
        /// <param name="ciphertext">密文数据</param>
        /// <param name="iv"></param>
        /// <param name="blockMode">块模式</param>
        /// <returns>解密后的数据</returns>
        byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode);

        /// <summary>
        /// 使用 ECB 模式加密
        /// </summary>
        /// <param name="plaintext">明文数据</param>
        /// <returns>加密后的数据</returns>
        byte[] EncryptEcb(byte[] plaintext);

        /// <summary>
        /// 使用 ECB 模式解密
        /// </summary>
        /// <param name="ciphertext">密文数据</param>
        /// <returns>解密后的数据</returns>
        byte[] DecryptEcb(byte[] ciphertext);

        /// <summary>
        /// 使用 CBC 模式加密
        /// </summary>
        /// <param name="plaintext">明文数据</param>
        /// <returns>加密后的数据</returns>
        byte[] EncryptCbc(byte[] plaintext);

        /// <summary>
        /// 使用 CBC 模式加密
        /// </summary>
        /// <param name="plaintext">明文数据</param>
        /// <param name="iv"></param>
        /// <returns>加密后的数据</returns>
        byte[] EncryptCbc(byte[] plaintext, byte[] iv);

        /// <summary>
        /// 使用 CBC 模式解密
        /// </summary>
        /// <param name="ciphertext">密文数据</param>
        /// <returns>解密后的数据</returns>
        byte[] DecryptCbc(byte[] ciphertext);

        /// <summary>
        /// 使用 CBC 模式解密
        /// </summary>
        /// <param name="ciphertext">密文数据</param>
        /// <param name="iv"></param>
        /// <returns>解密后的数据</returns>
        byte[] DecryptCbc(byte[] ciphertext, byte[] iv);

        /// <summary>
        /// 使用 CTR 模式加密
        /// </summary>
        /// <param name="plaintext">明文数据</param>
        /// <returns>加密后的数据</returns>
        byte[] EncryptCtr(byte[] plaintext);

        /// <summary>
        /// 使用 CTR 模式加密
        /// </summary>
        /// <param name="plaintext">明文数据</param>
        /// <param name="iv"></param>
        /// <returns>加密后的数据</returns>
        byte[] EncryptCtr(byte[] plaintext, byte[] iv);

        /// <summary>
        /// 使用 CTR 模式解密
        /// </summary>
        /// <param name="ciphertext">密文数据</param>
        /// <returns>解密后的数据</returns>
        byte[] DecryptCtr(byte[] ciphertext);

        /// <summary>
        /// 使用 CTR 模式解密
        /// </summary>
        /// <param name="ciphertext">密文数据</param>
        /// <param name="iv"></param>
        /// <returns>解密后的数据</returns>
        byte[] DecryptCtr(byte[] ciphertext, byte[] iv);
    }
}