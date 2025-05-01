using System;

namespace JLGames.Infra.Crypto.Asymmetric
{
    public sealed class RsaGroup : IDisposable
    {
        private byte[] m_Buff;
        private int m_GroupSize;
        private int m_Position;

        public RsaGroup()
        {
        }

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
        /// 重装装载数据
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="groupSize"></param>
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
        /// <returns></returns>
        public byte[] ReadNext()
        {
            var len = Math.Min(m_GroupSize, m_Buff.Length - m_Position);
            var next = new byte[len];
            Buffer.BlockCopy(m_Buff, m_Position, next, 0, len);
            m_Position += len;
            return next;
        }

        public void Dispose()
        {
            m_Buff = null;
            m_GroupSize = 0;
            m_Position = 0;
        }
    }
}