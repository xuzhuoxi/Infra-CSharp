namespace JLGames.Infra.Net
{
    /// <summary>
    /// Socket message handler delegate definitions.
    /// Socket 消息处理委托定义。
    /// </summary>
    public static class SocketDelegates
    {
        /// <summary>
        /// Binary message handler.
        /// 二进制消息处理。
        /// </summary>
        /// <param name="msg">Message bytes.<br/>消息字节。</param>
        /// <param name="remoteAddress">Remote endpoint address.<br/>远端地址。</param>
        /// <param name="other">User-defined context (may be null).<br/>用户自定义上下文（可为 null）。</param>
        public delegate void OnBinaryMessageHandler(byte[] msg, string remoteAddress, object other);

        /// <summary>
        /// String message handler.
        /// 字符串消息处理。
        /// </summary>
        /// <param name="msg">Message text.<br/>消息文本。</param>
        /// <param name="remoteAddress">Remote endpoint address.<br/>远端地址。</param>
        /// <param name="other">User-defined context (may be null).<br/>用户自定义上下文（可为 null）。</param>
        public delegate void OnStringMessageHandler(string msg, string remoteAddress, object other);
    }
}