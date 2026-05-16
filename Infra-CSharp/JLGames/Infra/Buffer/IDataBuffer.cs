namespace JLGames.Infra.Buffer
{
    /// <summary>
    /// Typed data buffer supporting endian-aware serialization of primitive types.
    /// 支持按字节序序列化基础类型的数据缓冲区
    /// </summary>
    public interface IDataBuffer : IByteBuffer, IDataBufferWriter, IDataBufferReader, IDataBufferCopier
    {
        /// <summary>
        /// Endian Coverter
        /// 字节转换器
        /// </summary>
        EndianCoverter EndianCoverter { get; }
    }
}