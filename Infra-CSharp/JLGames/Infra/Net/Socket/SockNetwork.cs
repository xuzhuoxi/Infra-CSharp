namespace JLGames.Infra.Net
{
    public enum SockNetwork
    {
        Undefined,

        Quic,
        WebSocket,
        WebSockets,

        Tcp,
        Tcp4,
        Tcp6,

        Udp,
        Udp4,
        Udp6,
    }
}