using JLGames.Infra.Buffer;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Network message that can be encoded to / decoded from bytes or buffers.
    /// 可序列化/反序列化的网络消息。
    /// </summary>
    public interface INetMessage
    {
        /// <summary>
        /// Serialize to byte array
        /// 序列化为字节数组
        /// </summary>
        /// <returns>Serialized bytes.<br/>序列化后的字节数组。</returns>
        byte[] EncodeToBytes();

        /// <summary>
        /// Construct message from byte array.
        /// 由字节数组构造消息。
        /// </summary>
        /// <param name="bytes">Source bytes.<br/>源字节数组。</param>
        void DecodeFromBytes(byte[] bytes);
        
        /// <summary>
        /// Serialized as a byte array and written to the IDataBuffer buffer
        /// 序列化为字节数组并写入到IDataBuffer缓存区
        /// </summary>
        /// <param name="buff">Target buffer writer.<br/>目标缓冲区写入器。</param>
        void EncodeToBuff(IDataBufferWriter buff);

        /// <summary>
        /// Read from buffer and update current properties.
        /// 从缓冲区读取并更新当前属性。
        /// </summary>
        /// <param name="buff">Source buffer reader.<br/>源缓冲区读取器。</param>
        void DecodeFromBuff(IDataBufferReader buff);
    }
}