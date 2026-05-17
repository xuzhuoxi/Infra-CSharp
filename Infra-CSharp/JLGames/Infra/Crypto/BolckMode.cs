namespace JLGames.Infra.Crypto
{
    /// <summary>
    /// Block cipher modes of operation.
    /// 分组密码工作模式枚举
    /// </summary>
    public enum BlockMode
    {
        /// <summary>
        /// Electronic Codebook (insecure; identical blocks yield identical ciphertext).
        /// 电子密码本模式（不安全，不推荐实际使用）
        /// </summary>
        ECB,

        /// <summary>
        /// Cipher Block Chaining; requires a random IV for the first block.
        /// 加密分组链接模式，首块需随机 IV
        /// </summary>
        CBC,

        /// <summary>
        /// Cipher Feedback (stream-like); bit errors propagate locally.
        /// 加密反馈模式（流式）
        /// </summary>
        CFB,

        /// <summary>
        /// Output Feedback; keystream from cipher output, sensitive to IV.
        /// 输出反馈模式
        /// </summary>
        OFB,

        /// <summary>
        /// Counter mode; parallelizable, high performance.
        /// 计数器模式，支持并行
        /// </summary>
        CTR,

        /// <summary>
        /// Galois/Counter Mode; authenticated encryption (e.g. TLS).
        /// Galois/Counter 认证加密模式
        /// </summary>
        GCM
    }

    /// <summary>
    /// Helpers for <see cref="BlockMode"/>.
    /// 分组模式辅助工具
    /// </summary>
    public static class BlockModeHelper
    {
        /// <summary>
        /// Returns a human-readable description of the mode.
        /// 获取加密模式的中文描述
        /// </summary>
        /// <param name="mode">加密模式枚举值</param>
        /// <returns>描述信息</returns>
        public static string GetDescription(BlockMode mode)
        {
            switch (mode)
            {
                case BlockMode.ECB:
                    return "电子密码本模式：不安全，每个分组独立加密，容易泄漏结构，不推荐使用";
                case BlockMode.CBC:
                    return "加密分组链接模式：常用，安全性较好，解密可并行，但加密不可并行";
                case BlockMode.CFB:
                    return "加密反馈模式：流式加密，适合字节数据传输，一位错误会影响当前和下一个块";
                case BlockMode.OFB:
                    return "输出反馈模式：预计算密钥流，错误不会扩散，对 IV 非常敏感";
                case BlockMode.CTR:
                    return "计数器模式：高性能，支持并行加解密，常用于高性能通信流加密";
                case BlockMode.GCM:
                    return "Galois/Counter 模式：高安全性，支持认证加密，常用于 TLS、VPN、HTTPS 等高安全需求场景";
                default:
                    return "未知加密模式";
            }
        }
    }
}