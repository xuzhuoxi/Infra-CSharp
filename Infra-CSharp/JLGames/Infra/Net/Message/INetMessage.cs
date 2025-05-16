using JLGames.Infra.Buffer;

namespace JLGames.Infra.Net
{
    public interface INetMessage
    {
        /// <summary>
        /// Serialize to byte array
        /// 序列化为字节数组
        /// </summary>
        /// <returns></returns>
        byte[] EncodeToBytes();

        /// <summary>
        /// Construct message from byte array
        /// 由字节数组构造消息
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        void DecodeFromBytes(byte[] bytes);
        
        /// <summary>
        /// Serialized as a byte array and written to the IDataBuffer buffer
        /// 序列化为字节数组并写入到IDataBuffer缓存区
        /// </summary>
        /// <param name="buff"></param>
        void EncodeToBuff(IDataBufferWriter buff);

        /// <summary>
        /// Reads the byte array from the IDataBuffer buffer and updates to the current properties
        /// 从IDataBuffer缓存区读取字节数组并更新到当前属性
        /// </summary>
        /// <param name="buff"></param>
        void DecodeFromBuff(IDataBufferReader buff);
    }
}