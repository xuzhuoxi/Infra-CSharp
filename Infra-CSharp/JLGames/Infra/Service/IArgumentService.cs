namespace JLGames.Infra.Service
{
    /// <summary>
    /// Receives constructor-style arguments before initialization.
    /// 在初始化前接收构造/配置参数的接口。
    /// Invoked by <see cref="ServiceManager"/> for each entry in <see cref="ServiceConfig"/> during <see cref="ServiceManager.StartInitalization"/>.
    /// 在 <see cref="ServiceManager.StartInitalization"/> 期间由 <see cref="ServiceManager"/> 对每个 <see cref="ServiceConfig"/> 项调用。
    /// </summary>
    public interface IArgumentService
    {
        /// <summary>
        /// Inject data.
        /// 注入数据
        /// </summary>
        /// <param name="args">Arguments from <see cref="ServiceInfo.Args"/>; may be null or empty.<br/>来自 <see cref="ServiceInfo.Args"/> 的参数；可为 null 或空数组。</param>
        void InjectArgument(object[] args);
    }
}