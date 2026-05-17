using System;

namespace JLGames.Infra.Crypto.Asymmetric
{
    /// <summary>
    /// 将长字节流按 RSA 块大小切分为多个分组，用于分段加解密。
    /// </summary>
    public sealed class RsaGroup : IDisposable
    {
        private byte[] m_Buff;
        private int m_GroupSize;
        private int m_Position;

        /// <summary>创建空分组读取器，稍后通过 <see cref="ResetData"/> 装载数据。</summary>
        public RsaGroup()
        {
        }

        /// <summary>
        /// 创建分组读取器并绑定缓冲区与分组大小。
        /// </summary>
        /// <param name="buff">待切分数据</param>
        /// <param name="groupSize">每组最大字节数</param>
        public RsaGroup(byte[] buff, int groupSize)
        {
            m_Buff = buff;
            m_GroupSize = groupSize;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            m_Position = 0;
        }

        /// <summary>
        /// 重新装载数据并重置读取位置。
        /// </summary>
        /// <param name="buff">待切分数据</param>
        /// <param name="groupSize">每组最大字节数</param>
        public void ResetData(byte[] buff, int groupSize)
        {
            m_Buff = buff;
            m_GroupSize = groupSize;
            m_Position = 0;
        }

        /// <summary>
        /// 是否还有下一分组
        /// </summary>
        public bool HasNext => m_Position < m_Buff.Length;

        /// <summary>
        /// 读取下一分组
        /// </summary>
        /// <returns>下一分组字节；最后一组可能短于分组大小</returns>
        public byte[] ReadNext()
        {
            var len = Math.Min(m_GroupSize, m_Buff.Length - m_Position);
            var next = new byte[len];
            System.Buffer.BlockCopy(m_Buff, m_Position, next, 0, len);
            m_Position += len;
            return next;
        }

        /// <summary>释放内部缓冲区引用。</summary>
        public void Dispose()
        {
            m_Buff = null;
            m_GroupSize = 0;
            m_Position = 0;
        }
    }
}