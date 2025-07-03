namespace JLGames.Infra.Net
{
    public static class SocketNetworks
    {
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