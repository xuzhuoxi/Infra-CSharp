namespace JLGames.Infra.Service
{
    /// <summary>
    /// Service Awaka interface
    /// 服务激活接口
    /// </summary>
    public interface IAwakableService
    {
        /// <summary>
        /// Awake service
        /// 激活Service
        /// Async is not allowed
        /// 不允许使用异步
        /// </summary>
        void Awake();
    }
}