namespace JLGames.Infra.Service
{
    /// <summary>
    /// Early activation hook before <see cref="IInitService.Init"/>.
    /// 在 <see cref="IInitService.Init"/> 之前执行的早期激活钩子。
    /// Invoked synchronously by <see cref="ServiceManager"/> during startup; must not use async.
    /// 由 <see cref="ServiceManager"/> 在启动时同步调用；不允许使用异步。
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