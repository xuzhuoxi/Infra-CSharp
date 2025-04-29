using System.Numerics;

namespace JLGames.Infra.Crypto.Key
{
    public class DhKeyPair
    {
        public BigInteger Private { get; set; }
        public BigInteger Public { get; set; }
    }
}