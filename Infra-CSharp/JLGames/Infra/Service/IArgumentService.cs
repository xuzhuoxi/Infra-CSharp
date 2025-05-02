namespace JLGames.Infra.Service
{
    /// <summary>
    /// Data injectioin interface
    /// 注入数据接口
    /// </summary>
    public interface IArgumentService
    {
        /// <summary>
        /// Inject data.
        /// 注入数据
        /// </summary>
        /// <param name="args"></param>
        void InjectArgument(object[] args);
    }
}