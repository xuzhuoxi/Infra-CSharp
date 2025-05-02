using System.Text;

namespace JLGames.Infra.Archive
{
    public class UnarchiveParams
    {
        private string m_DstPath;
        private readonly bool m_Override;
        private readonly Encoding m_Encoding;

        public string DstPath => m_DstPath;
        public bool Override => m_Override;
        public Encoding Encoding => m_Encoding;

        public UnarchiveParams(string dstPath)
        {
            m_DstPath = dstPath;
            m_Override = true;
            m_Encoding = Encoding.UTF8;
        }

        public UnarchiveParams(string dstPath, bool @override, Encoding encoding)
        {
            m_DstPath = dstPath;
            m_Override = @override;
            m_Encoding = encoding;
        }

        public void SetDstPath(string path)
        {
            m_DstPath = path;
        }
    }
}