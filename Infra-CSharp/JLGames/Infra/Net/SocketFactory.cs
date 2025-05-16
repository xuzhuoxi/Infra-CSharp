using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    public static class SocketFactory
    {
        /// <summary>
        /// 创建一个SocketClient
        /// </summary>
        /// <param name="name"></param>
        /// <param name="littleEndian">是否小端</param>
        /// <param name="apmMode">是否使用异步编程模型</param>
        /// <returns></returns>
        public static ISocketClient CreateSocketClient(string name, bool littleEndian, bool apmMode)
        {
            return new SocketClient(name, littleEndian, apmMode);
        }

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