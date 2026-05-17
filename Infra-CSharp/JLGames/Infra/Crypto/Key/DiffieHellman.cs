using System;
using System.Numerics;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Key
{
    /// <summary>
    /// RFC 3526 Group 14（2048 位 MODP）Diffie-Hellman 密钥交换。
    /// </summary>
    public static class DiffieHellman
    {
        // RFC 3526 Group 14 (2048-bit MODP) prime number
        private const string m_PrimeHex = @"
FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD129024E08
8A67CC74020BBEA63B139B22514A08798E3404DDEF9519B3CD3A431B
302B0A6DF25F14374FE1356D6D51C245E485B576625E7EC6F44C42E9
A637ED6B0BFF5CB6F406B7EDEE386BFB5A899FA5AE9F24117C4B1FE6
49286651ECE65381FFFFFFFFFFFFFFFF
";

        private static BigInteger s_P;
        private static readonly BigInteger s_G = new BigInteger(2); // g = 2

        static DiffieHellman()
        {
            s_P = BigInteger.Parse(RemoveSpaces(m_PrimeHex), System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// 生成 DH 密钥对（私钥 x 与公钥 g^x mod p）。
        /// </summary>
        /// <returns>新生成的密钥对</returns>
        public static DhKeyPair GenerateDhKeyPair()
        {
            // Generate a random private key in range [0, p)
            BigInteger privateKey;
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[s_P.ToByteArray().Length];
                rng.GetBytes(buffer);
                privateKey = new BigInteger(buffer) % s_P;
            }

            // Calculate public key (public = g^private mod p)
            var publicKey = BigInteger.ModPow(s_G, privateKey, s_P);
            return new DhKeyPair { Private = privateKey, Public = publicKey };
        }

        /// <summary>
        /// 计算共享密钥 K = theirPublic^myPrivate mod p。
        /// </summary>
        /// <param name="theirPublic">对方公钥</param>
        /// <param name="myPrivate">本方私钥</param>
        /// <returns>共享大整数（通常需再经 KDF 派生为对称密钥）</returns>
        public static BigInteger ComputeDhSharedK(BigInteger theirPublic, BigInteger myPrivate)
        {
            return BigInteger.ModPow(theirPublic, myPrivate, s_P);
        }

        /// <summary>
        /// Removes spaces, tabs, and newlines from the primeHex string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string RemoveSpaces(string s)
        {
            return string.Concat(s.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}