using System;
using System.Numerics;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Key
{
    public static class DiffieHellman
    {
        // RFC 3526 Group 14 (2048-bit MODP) prime number
        private static readonly string primeHex = @"
FFFFFFFFFFFFFFFFC90FDAA22168C234C4C6628B80DC1CD129024E08
8A67CC74020BBEA63B139B22514A08798E3404DDEF9519B3CD3A431B
302B0A6DF25F14374FE1356D6D51C245E485B576625E7EC6F44C42E9
A637ED6B0BFF5CB6F406B7EDEE386BFB5A899FA5AE9F24117C4B1FE6
49286651ECE65381FFFFFFFFFFFFFFFF
";

        private static BigInteger p;
        private static BigInteger g = new BigInteger(2); // g = 2

        static DiffieHellman()
        {
            p = BigInteger.Parse(RemoveSpaces(primeHex), System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// GenerateDHKeyPair generates a DH key pair (private, public)
        /// </summary>
        /// <returns></returns>
        public static DhKeyPair GenerateDhKeyPair()
        {
            // Generate a random private key in range [0, p)
            BigInteger privateKey;
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[p.ToByteArray().Length];
                rng.GetBytes(buffer);
                privateKey = new BigInteger(buffer) % p;
            }

            // Calculate public key (public = g^private mod p)
            var publicKey = BigInteger.ModPow(g, privateKey, p);
            return new DhKeyPair { Private = privateKey, Public = publicKey };
        }

        /// <summary>
        /// ComputeDHSharedK computes the shared key (K = public^private mod p)
        /// </summary>
        /// <param name="theirPublic"></param>
        /// <param name="myPrivate"></param>
        /// <returns></returns>
        public static BigInteger ComputeDhSharedK(BigInteger theirPublic, BigInteger myPrivate)
        {
            return BigInteger.ModPow(theirPublic, myPrivate, p);
        }

        /// <summary>
        /// Removes spaces, tabs, and newlines from the primeHex string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string RemoveSpaces(string s)
        {
            return string.Concat(s.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}