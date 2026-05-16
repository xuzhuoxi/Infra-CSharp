namespace JLGames.Infra.Buffer
{
    /// <summary>
    /// Byte buffer with read, write, and peek (copy) capabilities.
    /// 具备读取、写入与窥视（复制）能力的字节缓冲区
    /// </summary>
    public interface IByteBuffer : IByteBufferReader, IByteBufferWriter, IByteBufferCopier
    {
        /// <summary>
        /// Total capacity
        /// 总容量
        /// </summary>
        int Cap { get; }

        /// <summary>
        /// Clean up records
        /// 清理记录
        /// </summary>
        void Clear();
    }
}