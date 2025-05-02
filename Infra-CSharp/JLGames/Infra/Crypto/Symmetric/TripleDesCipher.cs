using System;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    public class TripleDesCipher : DesCipher
    {
        public override int BlockSize => DesDefines.TripleKeySize;

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