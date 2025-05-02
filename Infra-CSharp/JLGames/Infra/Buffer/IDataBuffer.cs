namespace JLGames.Infra.Buffer
{
    public interface IDataBuffer : IByteBuffer, IDataBufferWriter, IDataBufferReader, IDataBufferCopier
    {
        /// <summary>
        /// Endian Coverter
        /// 字节转换器
        /// </summary>
        EndianCoverter EndianCoverter { get; }
    }
}