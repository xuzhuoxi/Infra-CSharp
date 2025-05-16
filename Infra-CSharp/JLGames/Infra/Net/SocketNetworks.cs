namespace JLGames.Infra.Net
{
    public static class SocketNetworks
    {
        /// <summary>
        /// 未定义的网络通信类型
        /// </summary>
        public const string Undefined = "";

        /// <summary>
        /// TCP 协议
        /// </summary>
        public const string Tcp = "tcp";

        /// <summary>
        /// IPv4 的 TCP 协议
        /// </summary>
        public const string Tcp4 = "tcp4";

        /// <summary>
        /// IPv6 的 TCP 协议
        /// </summary>
        public const string Tcp6 = "tcp6";

        /// <summary>
        /// UDP 协议
        /// </summary>
        public const string Udp = "udp";

        /// <summary>
        /// IPv4 的 UDP 协议
        /// </summary>
        public const string Udp4 = "udp4";

        /// <summary>
        /// IPv6 的 UDP 协议
        /// </summary>
        public const string Udp6 = "udp6";

        /// <summary>
        /// WebSocket 协议
        /// </summary>
        public const string WebSocket = "ws";

        /// <summary>
        /// WebSocket Secure 协议
        /// </summary>
        public const string WebSocketSecure = "wss";

        /// <summary>
        /// QUIC 协议
        /// </summary>
        public const string Quic = "quic";
        
        public enum Network
        {
            /// <summary>
            /// 未定义的网络通信类型
            /// </summary>
            Undefined,
            /// <summary>
            /// TCP 协议
            /// </summary>
            Tcp,
            /// <summary>
            /// IPv4 的 TCP 协议
            /// </summary>
            Tcp4,
            /// <summary>
            /// IPv6 的 TCP 协议
            /// </summary>
            Tcp6,
            /// <summary>
            /// UDP 协议
            /// </summary>
            Udp,
            /// <summary>
            /// IPv4 的 UDP 协议
            /// </summary>
            Udp4,
            /// <summary>
            /// IPv6 的 UDP 协议
            /// </summary>
            Udp6,
            /// <summary>
            /// WebSocket 协议
            /// </summary>
            WebSocket,
            /// <summary>
            /// WebSocket Secure 协议
            /// </summary>
            WebSockets,
            /// <summary>
            /// QUIC 协议
            /// </summary>
            Quic,
        }
    }
}