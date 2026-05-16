namespace JLGames.Infra.Buffer
{
    /// <summary>
    /// Typed data buffer built on <see cref="ByteBuffer"/> with endian-aware read/write.
    /// 基于 <see cref="ByteBuffer"/> 构建、支持按字节序读写基础类型的数据缓冲区
    /// </summary>
    public partial class DataBuffer : IDataBuffer
    {
        protected const int LenSize = sizeof(ushort);
        protected readonly ByteBuffer m_Buff;
        protected readonly EndianCoverter m_Coverter;

        /// <summary>
        /// Create a buffer with the specified byte capacity and endianness.
        /// 使用指定字节容量与字节序创建缓冲区
        /// </summary>
        /// <param name="byteSize">Initial byte capacity; 初始字节容量</param>
        /// <param name="littleEndian">True for little-endian; true 表示小端</param>
        public DataBuffer(int byteSize, bool littleEndian)
        {
            m_Buff = new ByteBuffer(byteSize);
            m_Coverter = EndianCoverter.GetEndianCoverter(littleEndian);
        }

        /// <summary>
        /// Create a buffer with default byte capacity and the specified endianness.
        /// 使用默认字节容量与指定字节序创建缓冲区
        /// </summary>
        /// <param name="littleEndian">True for little-endian; true 表示小端</param>
        public DataBuffer(bool littleEndian)
        {
            m_Buff = new ByteBuffer();
            m_Coverter = EndianCoverter.GetEndianCoverter(littleEndian);
        }

        /// <summary>
        /// Create a buffer with the specified endian converter.
        /// 使用指定字节序转换器创建缓冲区
        /// </summary>
        /// <param name="coverter">Endian converter; 字节序转换器</param>
        public DataBuffer(EndianCoverter coverter)
        {
            m_Buff = new ByteBuffer();
            m_Coverter = coverter;
        }

        /// <inheritdoc/>
        public EndianCoverter EndianCoverter => m_Coverter;
    }
}
