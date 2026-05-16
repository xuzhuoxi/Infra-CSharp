namespace JLGames.Infra.Buffer
{
    /// <summary>
    /// Byte buffer reader.
    /// 字节缓冲区读取器
    /// </summary>
    public interface IByteBufferReader
    {
        /// <summary>
        /// Current unread length
        /// 当前未读取长度
        /// </summary>
        int Len { get; }

        /// <summary>
        /// Reading index position
        /// 读取索引位置
        /// </summary>
        int ReadPosition { get; }

        /// <summary>
        /// set reading index position
        /// 设置读取索引位置
        /// </summary>
        /// <param name="pos"></param>
        void SetReadPosition(int pos);

        /// <summary>
        /// Read a byte data
        /// 读取一个字节数据
        /// </summary>
        /// <returns></returns>
        byte ReadByte();

        /// <summary>
        /// Read all unread data
        /// 读取全部未读取的数据
        /// </summary>
        /// <returns></returns>
        byte[] ReadBytes();

        /// <summary>
        /// Read data of specified length
        /// 读取指定长度数据
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        byte[] ReadBytes(int size);

        /// <summary>
        /// Read data of specified length and write it into the target array
        /// 读取指定长度数据，并写入到目标数组中
        /// </summary>
        /// <param name="dst"></param>
        /// <returns></returns>
        int ReadBytesTo(ref byte[] dst);

        /// <summary>
        /// Read data of specified length and write it into the target array
        /// 读取指定长度数据，并写入到目标数组中
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="size"></param>
        int ReadBytesTo(ref byte[] dst, int size);

    }
}