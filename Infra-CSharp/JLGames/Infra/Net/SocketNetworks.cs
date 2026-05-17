namespace JLGames.Infra.Net
{
    /// <summary>
    /// Network protocol type constants and conversions.
    /// 网络协议类型常量与转换工具。
    /// </summary>
    public static class SocketNetworks
    {
        /// <summary>
        /// Supported network protocol kinds.
        /// 支持的网络协议类型。
        /// </summary>
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

        /// <summary>
        /// Get the string identifier for a network enum value.
        /// 将网络类型枚举转换为字符串标识。
        /// </summary>
        /// <param name="network">Network type.<br/>网络类型。</param>
        /// <returns>Protocol string (e.g. "tcp", "udp").<br/>协议字符串（如 "tcp"、"udp"）。</returns>
        public static string GetNetworkValue(Network network)
        {
            switch (network)
            {
                case Network.Tcp:
                    return Tcp;
                case Network.Tcp4:
                    return Tcp4;
                case Network.Tcp6:
                    return Tcp6;
                case Network.Udp:
                    return Udp;
                case Network.Udp4:
                    return Udp4;
                case Network.Udp6:
                    return Udp6;
                case Network.WebSocket:
                    return WebSocket;
                case Network.WebSockets:
                    return WebSocketSecure;
                case Network.Quic:
                    return Quic;
                default:
                    return Undefined;
            }
        }

        /// <summary>
        /// Parse a protocol string into a network enum value (case-insensitive).
        /// 将协议字符串解析为网络类型枚举（不区分大小写）。
        /// </summary>
        /// <param name="network">Protocol string.<br/>协议字符串。</param>
        /// <returns>Matching <see cref="Network"/>; <see cref="Network.Undefined"/> if unknown.<br/>匹配的枚举值；未知时返回 <see cref="Network.Undefined"/>。</returns>
        public static Network GetNetwork(string network)
        {
            switch (network.ToLower())
            {
                case Tcp:
                    return Network.Tcp;
                case Tcp4:
                    return Network.Tcp4;
                case Tcp6:
                    return Network.Tcp6;
                case Udp:
                    return Network.Udp;
                case Udp4:
                    return Network.Udp4;
                case Udp6:
                    return Network.Udp6;
                case WebSocket:
                    return Network.WebSocket;
                case WebSocketSecure:
                    return Network.WebSockets;
                case Quic:
                    return Network.Quic;
                default:
                    return Network.Undefined;
            }
        }
    }
}