namespace JLGames.Infra.Buffer
{
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