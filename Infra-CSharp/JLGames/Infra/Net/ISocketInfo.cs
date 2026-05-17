namespace JLGames.Infra.Net
{
    /// <summary>
    /// Basic socket identity and connection state.
    /// Socket 基本信息与连接状态。
    /// </summary>
    public interface ISocketInfo
    {
        /// <summary>
        /// Socket instance name.
        /// Socket 实例名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Whether the socket is connected.
        /// 是否已连接。
        /// </summary>
        bool Connected { get; }
    }
}