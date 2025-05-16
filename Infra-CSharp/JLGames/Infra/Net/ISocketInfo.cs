namespace JLGames.Infra.Net
{
    public interface ISocketInfo
    {
        /// <summary>
        /// name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// is it connected?
        /// 判断是否连接中
        /// </summary>
        bool Connected { get; }
    }
}