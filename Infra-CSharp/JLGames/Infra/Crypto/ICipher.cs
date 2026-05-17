using System;

namespace JLGames.Infra.Crypto
{
    /// <summary>
    /// Encryption cipher interface.
    /// 加密处理器接口
    /// </summary>
    public interface IEncryptCipher
    {
        /// <summary>
        /// Encrypt data.
        /// 加密数据
        /// </summary>
        /// <param name="plaintext">Plaintext to encrypt / 待加密的明文数据</param>
        /// <returns>Ciphertext / 加密后的密文数据</returns>
        /// <exception cref="Exception">Thrown when encryption fails / 加密过程中发生错误</exception>
        byte[] Encrypt(byte[] plaintext);
    }

    /// <summary>
    /// Decryption cipher interface.
    /// 解密处理器接口
    /// </summary>
    public interface IDecryptCipher
    {
        /// <summary>
        /// Decrypt data.
        /// 解密数据
        /// </summary>
        /// <param name="ciphertext">Ciphertext to decrypt / 待解密的密文数据</param>
        /// <returns>Plaintext / 解密后的明文数据</returns>
        /// <exception cref="Exception">Thrown when decryption fails / 解密过程中发生错误</exception>
        byte[] Decrypt(byte[] ciphertext);
    }

    /// <summary>
    /// Symmetric or asymmetric cipher with both encrypt and decrypt.
    /// 加密解密处理器接口，继承了加密和解密接口
    /// </summary>
    public interface ICipher : IEncryptCipher, IDecryptCipher
    {
    }
}
