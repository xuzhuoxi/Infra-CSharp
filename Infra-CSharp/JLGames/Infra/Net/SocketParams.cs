using System;
using System.Net;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// IP address and port pair.
    /// IP 地址与端口。
    /// </summary>
    public struct AddressInfo
    {
        /// <summary>
        /// IP address string.
        /// IP 地址字符串。
        /// </summary>
        public string IPAddress;

        /// <summary>
        /// Port number.
        /// 端口号。
        /// </summary>
        public int Port;
    }

    /// <summary>
    /// Socket connection parameters (addresses, protocol, WebSocket options).
    /// Socket 连接参数（地址、协议、WebSocket 选项等）。
    /// </summary>
    [Serializable]
    public struct SocketParams
    {
        /// <summary>
        /// Network protocol type.
        /// 网络协议类型。
        /// </summary>
        public SocketNetworks.Network Network { get; set; }

        /// <summary>
        /// Local endpoint in "host:port" format.
        /// 本地端点，格式为 "host:port"。
        /// </summary>
        public string LocalAddress { get; set; }

        /// <summary>
        /// Remote endpoint in "host:port" format.
        /// 远端端点，格式为 "host:port"。
        /// </summary>
        public string RemoteAddress { get; set; }

        /// <summary>
        /// WebSocket path pattern (e.g. "/", "/echo").
        /// WebSocket 路径模式（例如 "/"、"/echo"）。
        /// </summary>
        public string WSPattern { get; set; }

        /// <summary>
        /// WebSocket Origin header (e.g. "http://127.0.0.1/", must end with "/").
        /// WebSocket Origin 头（例如 "http://127.0.0.1/"，须以 "/" 结尾）。
        /// </summary>
        public string WSOrigin { get; set; }

        /// <summary>
        /// WebSocket sub-protocol string (may be empty).
        /// WebSocket 子协议字符串（可为空）。
        /// </summary>
        public string WSProtocol { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Network={Network}, Local={LocalAddress}, Remote={RemoteAddress}, WS={{{WSPattern}, {WSOrigin}, {WSProtocol}}}";
        }

        /// <summary>
        /// Parse <see cref="LocalAddress"/> into an <see cref="EndPoint"/>.
        /// 将 <see cref="LocalAddress"/> 解析为 <see cref="EndPoint"/>。
        /// </summary>
        /// <returns>Local endpoint.<br/>本地端点。</returns>
        public EndPoint LocalEndPoint()
        {
            return InnerToAddressInfo(LocalAddress);
        }

        /// <summary>
        /// Parse <see cref="RemoteAddress"/> into an <see cref="EndPoint"/>.
        /// 将 <see cref="RemoteAddress"/> 解析为 <see cref="EndPoint"/>。
        /// </summary>
        /// <returns>Remote endpoint.<br/>远端端点。</returns>
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
