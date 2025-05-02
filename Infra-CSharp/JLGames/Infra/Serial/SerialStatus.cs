namespace JLGames.Infra.Serial
{
    public enum SerialStatus
    {
        /// <summary>
        /// 停止完成
        /// </summary>
        Stopped,

        /// <summary>
        /// 启动进行中
        /// </summary>
        Starting,

        /// <summary>
        /// 启动完成
        /// </summary>
        Started,

        /// <summary>
        /// 停止进行中
        /// </summary>
        Stopping
    }
}