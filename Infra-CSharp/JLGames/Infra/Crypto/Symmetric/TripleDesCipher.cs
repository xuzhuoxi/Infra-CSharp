using System;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// 三重 DES（3DES）对称加密，密钥 8/16/24 字节自动展开为 24 字节。
    /// </summary>
    public class TripleDesCipher : DesCipher
    {
        /// <summary>报告 3DES 密钥长度（24 字节），与 DES 块大小（8 字节）不同。</summary>
        public override int BlockSize => DesDefines.TripleKeySize;

        /// <summary>
        /// 使用 8、16 或 24 字节密钥创建 3DES 实例。
        /// </summary>
        /// <param name="key">原始密钥</param>
        public TripleDesCipher(byte[] key)
        {
            if (null == key) throw new ArgumentException("Key must not be Null.");
            var keyLen = key.Length;
            switch (keyLen)
            {
                case DesDefines.KeySize:
                    m_Key = new byte[24];
                    System.Buffer.BlockCopy(key, 0, m_Key, 0, 8);
                    System.Buffer.BlockCopy(key, 0, m_Key, 8, 8);
                    System.Buffer.BlockCopy(key, 0, m_Key, 16, 8);
                    break;
                case DesDefines.KeySize * 2:
                    m_Key = new byte[24];
                    System.Buffer.BlockCopy(key, 0, m_Key, 0, 16);
                    System.Buffer.BlockCopy(key, 0, m_Key, 16, 8);
                    break;
                case DesDefines.TripleKeySize:
                    m_Key = key;
                    break;
                default:
                    throw new ArgumentException("Key must be 8 (DES) or 16|24 (3DES) bytes");
            }

            m_Des = TripleDES.Create();
            m_Des.Key = m_Key;
            m_PaddingMode = PaddingMode.PKCS7;
        }
    }
}