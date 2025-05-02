namespace JLGames.Infra.Buffer
{
    public interface IByteBufferWriter
    {
        /// <summary>
        /// Writing index position
        /// 写入索引位置
        /// </summary>
        int WritePosition { get; }

        /// <summary>
        /// Set writing index position
        /// 设置写入索引位置
        /// </summary>
        /// <param name="pos"></param>
        void SetWritePosition(int pos);
        
        /// <summary>
        /// Write binary 0 value
        /// 写入二进制0值
        /// </summary>
        /// <param name="size"></param>
        void WriteZero(int size);

        /// <summary>
        /// Write a byte
        /// 写入单个字节
        /// </summary>
        /// <param name="b"></param>
        void Write(byte b);

        /// <summary>
        /// Write byte array
        /// 写入字节数组
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex">the start index of bytes</param>
        /// <param name="size"></param>
        void Write(byte[] bytes, int startIndex, int size);

        /// <summary>
        /// Write byte array
        /// 写入字节数组
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="startIndex">the start index of bytes</param>
        void Write(byte[] bytes, int startIndex);

        /// <summary>
        /// Write byte array
        /// 写入字节数组
        /// </summary>
        /// <param name="bytes"></param>
        void Write(byte[] bytes);
    }
}