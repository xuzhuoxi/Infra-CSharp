using System;

namespace JLGames.Infra.Crypto
{
    /// <summary>
    /// 加密处理器接口
    /// </summary>
    public interface IEncryptCipher
    {
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="plaintext">待加密的明文数据</param>
        /// <returns>加密后的密文数据</returns>
        /// <exception cref="Exception">加密过程中发生错误</exception>
        byte[] Encrypt(byte[] plaintext);
    }

    /// <summary>
    /// 解密处理器接口
    /// </summary>
    public interface IDecryptCipher
    {
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="ciphertext">待解密的密文数据</param>
        /// <returns>解密后的明文数据</returns>
        /// <exception cref="Exception">解密过程中发生错误</exception>
        byte[] Decrypt(byte[] ciphertext);
    }

    /// <summary>
    /// 加密解密处理器接口，继承了加密和解密接口
    /// </summary>
    public interface ICipher : IEncryptCipher, IDecryptCipher
    {
    }
}