namespace JLGames.Infra.Service
{
    /// <summary>
    /// Service base interface
    /// 服务的基础接口
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Service name.
        /// 服务名称
        /// </summary>
        string ServiceName { get; set; }
    }
}