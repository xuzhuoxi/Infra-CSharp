using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Factory for creating socket clients and <see cref="Socket"/> instances.
    /// 创建 Socket 客户端与 <see cref="Socket"/> 实例的工厂。
    /// </summary>
    public static class SocketFactory
    {
        /// <summary>
        /// Create a socket client.
        /// 创建 Socket 客户端。
        /// </summary>
        /// <param name="name">Client name.<br/>客户端名称。</param>
        /// <param name="littleEndian">Use little-endian byte order for messages.<br/>消息是否使用小端字节序。</param>
        /// <param name="apmMode">Use APM (Begin/End) async model when true; otherwise TAP.<br/>为 true 时使用 APM（Begin/End）异步模型，否则使用 TAP。</param>
        /// <returns>Socket client instance.<br/>Socket 客户端实例。</returns>
        public static ISocketClient CreateSocketClient(string name, bool littleEndian, bool apmMode)
        {
            return new SocketClient(name, littleEndian, apmMode);
        }

        /// <summary>
        /// Create a <see cref="Socket"/> for the given parameters.
        /// 根据连接参数创建 <see cref="Socket"/>。
        /// </summary>
        /// <param name="params">Connection parameters.<br/>连接参数。</param>
        /// <returns>Configured socket, or null for unsupported protocols (WebSocket, QUIC).<br/>已配置的 Socket；不支持的协议（WebSocket、QUIC）返回 null。</returns>
        public static Socket CreateSocket(SocketParams @params)
        {
            switch (@params.Network)
            {
                case SocketNetworks.Network.Quic:
                    return null;
                case SocketNetworks.Network.WebSocket:
                    return null;
                case SocketNetworks.Network.WebSockets:
                    return null;
                case SocketNetworks.Network.Tcp:
                    var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    tcpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, false);
                    tcpSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                    return tcpSocket;
                case SocketNetworks.Network.Tcp4:
                    var tcp4Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    tcp4Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, false);
                    tcp4Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                    return tcp4Socket;
                case SocketNetworks.Network.Tcp6:
                    var tcp6Socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                    tcp6Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, false);
                    tcp6Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                    return tcp6Socket;
                case SocketNetworks.Network.Udp:
                    var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    return udpSocket;
                case SocketNetworks.Network.Udp4:
                    var udp4Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    return udp4Socket;
                case SocketNetworks.Network.Udp6:
                    var upd6Socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
                    return upd6Socket;
            }

            return null;
        }
    }
}