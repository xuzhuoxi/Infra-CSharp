namespace JLGames.Infra.Net
{
    /// <summary>
    /// Socket connection: send, receive, and connection state.
    /// Socket 连接：发送、接收与连接状态。
    /// </summary>
    public interface ISocketConn : ISocketSender, ISocketReceiver, ISocketInfo
    {
    }
}