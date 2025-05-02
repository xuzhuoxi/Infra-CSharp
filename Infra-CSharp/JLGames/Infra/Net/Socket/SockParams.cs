using System;
using System.Net;
using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    public struct AddressInfo
    {
        public string IPAddress;
        public int Port;
    }

    [Serializable]
    public struct SockParams
    {
        public SockNetwork Network;

        public string LocalAddress;
        public string RemoteAddress;

        // E.g: /,/echo
        public string WSPattern;

        // E.g: http://127.0.0.1/，must end with "/"
        public string WSOrigin;

        // E.g: ""
        public string WSProtocol;

        public Socket GenSocket()
        {
            switch (Network)
            {
                case SockNetwork.Quic:
                    return null;
                case SockNetwork.WebSocket:
                    return null;
                case SockNetwork.WebSockets:
                    return null;
                case SockNetwork.Tcp:
                    return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                case SockNetwork.Tcp4:
                    return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                case SockNetwork.Tcp6:
                    return new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                case SockNetwork.Udp:
                    return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
                case SockNetwork.Udp4:
                    return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
                case SockNetwork.Udp6:
                    return new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Udp);
            }

            return null;
        }

        public override string ToString()
        {
            return $"Network={Network}, Local={LocalAddress}, Remote={RemoteAddress}, WS={{{WSPattern}, {WSOrigin}, {WSProtocol}}}";
        }

        public EndPoint LocalEndPoint()
        {
            return InnerToAddressInfo(LocalAddress);
        }

        public EndPoint RemoteEndPoint()
        {
            return InnerToAddressInfo(RemoteAddress);
        }

        private EndPoint InnerToAddressInfo(string address)
        {
            var info = address.Split(':');
            return new IPEndPoint(IPAddress.Parse(info[0]), int.Parse(info[1]));
        }
    }
}