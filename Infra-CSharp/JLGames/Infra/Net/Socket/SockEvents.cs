namespace JLGames.Infra.Net
{
    public static class SockEvents
    {
        /// <summary>
        /// Enable connection result event
        /// 开启连接结果事件
        /// Event data(事件数据)：
        ///   Succ(成功): null
        ///   Fail(失败)：SocketError|Exception
        /// </summary>
        public static string EventOnConnectOpen = "SockEvents.EventOnConnectOpen";

        /// <summary>
        /// Close connection result event
        /// 关闭连接结果事件
        /// Event data(事件数据)：
        ///   Succ(成功): null
        ///   Fail(失败)：SocketError|Exception
        /// </summary>
        public static string EventOnConnectClose = "SockEvents.EventOnConnectClose";

        /// <summary>
        /// Data reception processing completed
        /// 数据接收处理结束
        /// Event data(事件数据)：null | bytesRead | Exception | SocketError
        /// </summary>
        public static string EventOnReceivingStopped = "SockEvents.EventOnReceivingStopped";
    }
}