using JLGames.Infra.Buffer;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Message packer with buffer
    /// 带缓存的消息封包器
    /// </summary>
    public interface IMessageWriter : IDataBufferWriter, IByteBufferWriter
    {
        /// <summary>
        /// Pack message into self buffer, Contains byte length information
        /// 把消息内容封包进缓存, 包含字节长度信息
        /// </summary>
        /// <param name="msg"></param>
        void WriteMessage(INetMessage msg);

        /// <summary>
        /// Pack message array into self buffer, Contains array length information
        /// 把消息数组封包进缓存, 包含数组长篇信息
        /// </summary>
        /// <param name="msg"></param>
        /// <typeparam name="T"></typeparam>
        void WriteMessage<T>(T[] msg) where T : INetMessage;

        /// <summary>
        /// Read out all bytes.
        /// 读出缓存中全部字节数据
        /// </summary>
        /// <returns></returns>
        byte[] ReadMessageBytes();

        /// <summary>
        /// Clear all bytes
        /// 清除全部字节
        /// </summary>
        void Clear();
    }
}