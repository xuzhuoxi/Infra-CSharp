namespace JLGames.Infra.Crypto
{
    /// <summary>
    /// 加密模式枚举
    /// </summary>
    public enum BlockMode
    {
        /// <summary>
        /// 电子密码本模式（不安全）
        /// 每个分组独立加密，没有上下文。同一明文块总是会生成相同密文块，容易泄漏结构。
        /// 不推荐用于实际应用。
        /// </summary>
        ECB,

        /// <summary>
        /// 加密分组链接模式
        /// 每个块与上一个密文块异或后再加密，首个块需要一个随机 IV。
        /// 常用于文件加密，安全性较好，但加密不可并行。
        /// </summary>
        CBC,

        /// <summary>
        /// 加密反馈模式（流式加密）
        /// 用前一密文作为输入流来生成密钥流，适合字节数据传输场景。
        /// 一位错误会影响当前和下一个块。
        /// </summary>
        CFB,

        /// <summary>
        /// 输出反馈模式（预计算密钥流）
        /// 类似 CFB，但只用加密输出而非密文。对 IV 敏感。
        /// 错误不会扩散到其他块。
        /// </summary>
        OFB,

        /// <summary>
        /// 计数器模式（高性能）
        /// 通过递增的“计数器”加密生成密钥流，支持并行加解密。
        /// 适用于高性能通信流加密。
        /// </summary>
        CTR,

        /// <summary>
        /// Galois/Counter 模式（高安全性）
        /// 支持认证加密，结合加密和验证，适用于高安全需求场景。
        /// 常用于 TLS、VPN、HTTPS 等。
        /// </summary>
        GCM
    }

    /// <summary>
    /// 加密模式的辅助工具类
    /// </summary>
    public static class BlockModeHelper
    {
        /// <summary>
        /// 获取加密模式的描述
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