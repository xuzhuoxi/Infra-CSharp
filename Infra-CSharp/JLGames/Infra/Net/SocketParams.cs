using System;
using System.Net;

namespace JLGames.Infra.Net
{
    public struct AddressInfo
    {
        public string IPAddress;
        public int Port;
    }

    [Serializable]
    public struct SocketParams
    {
        public SocketNetworks.Network Network { get; set; }

        public string LocalAddress { get; set; }
        public string RemoteAddress { get; set; }

        // E.g: /,/echo
        public string WSPattern { get; set; }

        // E.g: http://127.0.0.1/，must end with "/"
        public string WSOrigin { get; set; }

        // E.g: ""
        public string WSProtocol { get; set; }

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