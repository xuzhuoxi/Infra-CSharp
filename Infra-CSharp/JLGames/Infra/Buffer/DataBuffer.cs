namespace JLGames.Infra.Buffer
{
    public partial class DataBuffer : IDataBuffer
    {
        protected const int LenSize = sizeof(ushort);
        protected readonly ByteBuffer m_Buff;
        protected readonly EndianCoverter m_Coverter;

        public DataBuffer(int byteSize, bool littleEndian)
        {
            m_Buff = new ByteBuffer(byteSize);
            m_Coverter = EndianCoverter.GetEndianCoverter(littleEndian);
        }

        public DataBuffer(bool littleEndian)
        {
            m_Buff = new ByteBuffer();
            m_Coverter = EndianCoverter.GetEndianCoverter(littleEndian);
        }

        public DataBuffer(EndianCoverter coverter)
        {
            m_Buff = new ByteBuffer();
            m_Coverter = coverter;
        }

        // IDataBuffer

        public EndianCoverter EndianCoverter => m_Coverter;
    }
}