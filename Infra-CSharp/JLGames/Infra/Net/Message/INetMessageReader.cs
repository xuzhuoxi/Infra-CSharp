using JLGames.Infra.Buffer;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Message unpacker with buffer
    /// 带缓存的消息解包器
    /// </summary>
    public interface INetMessageReader : IDataBufferReader, IDataBufferCopier, IByteBufferReader, IByteBufferCopier
    {
        /// <summary>
        /// Check message exist
        /// 检查解包器中是否有消息
        /// </summary>
        /// <returns></returns>
        bool CheckMessage();

        /// <summary>
        /// Read message
        /// 计取整条消息
        /// </summary>
        /// <returns></returns>
        byte[] ReadMessage();

        /// <summary>
        /// Unpack message to object
        /// 解包消息到指定对象
        /// </summary>
        /// <param name="o"></param>
        /// <typeparam name="T"></typeparam>
        void ReadMessageTo<T>(ref T o) where T : INetMessage;

        /// <summary>
        /// Unpack message to object
        /// 解包消息到指定对象
        /// </summary>
        /// <param name="o"></param>
        /// <typeparam name="T"></typeparam>
        void ReadMessageTo<T>(ref T[] o) where T : INetMessage;

        /// <summary>
        /// Read message, not move reader index
        /// 计取整条消息, 不移动读下标
        /// </summary>
        /// <returns></returns>
        byte[] CopyMessage();

        /// <summary>
        /// Unpack message to object, not move reader index
        /// 解包消息到指定对象, 不移动读下标
        /// </summary>
        /// <param name="o"></param>
        /// <typeparam name="T"></typeparam>
        void CopyMessageTo<T>(ref T o) where T : INetMessage;

        /// <summary>
        /// Unpack message to object, not move reader index
        /// 解包消息到指定对象, 不移动读下标
        /// </summary>
        /// <param name="o"></param>
        /// <typeparam name="T"></typeparam>
        void CopyMessageTo<T>(ref T[] o) where T : INetMessage;

        /// <summary>
        /// Write bytes data.
        /// 写入字节数据
        /// </summary>
        /// <param name="src">要写入的数据源</param>
        void WriteMessageBytes(byte[] src);

        /// <summary>
        /// Write bytes data.
        /// 写入字节数据
        /// </summary>
        /// <param name="src">要写入的数据源</param>
        /// <param name="srcIndex">数据源的索引</param>
        /// <param name="size">数据写入长度</param>
        void WriteMessageBytes(byte[] src, int srcIndex, int size);
    }
}