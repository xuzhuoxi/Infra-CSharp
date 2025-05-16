namespace JLGames.Infra.Net
{
    public static class SocketDelegates
    {
        /// <summary>
        /// 二进制消息处理
        /// </summary>
        public delegate void OnBinaryMessageHandler(byte[] msg, string remoteAddress, object other);

        /// <summary>
        /// 字符消息处理
        /// </summary>
        public delegate void OnStringMessageHandler(string msg, string remoteAddress, object other);
    }
}