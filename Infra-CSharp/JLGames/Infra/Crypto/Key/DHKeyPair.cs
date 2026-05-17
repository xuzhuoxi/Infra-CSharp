using System.Numerics;

namespace JLGames.Infra.Crypto.Key
{
    /// <summary>
    /// Diffie-Hellman 密钥对（私钥与公钥大整数）。
    /// </summary>
    public class DhKeyPair
    {
        /// <summary>私钥 x</summary>
        public BigInteger Private { get; set; }
        /// <summary>公钥 g^x mod p</summary>
        public BigInteger Public { get; set; }
    }
}