using System.Text;

namespace JLGames.Infra.Crypto.Asymmetric
{
    public class RsaCipher
    {
        private RsaPrivateCipher m_RsaPrivateCipher;
        private RsaPublicCipher m_RsaPublicCipher;

        public RsaCipher(RsaPrivateCipher privateCipher, RsaPublicCipher publicCipher)
        {
            m_RsaPrivateCipher = privateCipher;
            m_RsaPublicCipher = publicCipher;
        }
    }
}