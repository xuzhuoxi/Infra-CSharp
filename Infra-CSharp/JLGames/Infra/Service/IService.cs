namespace JLGames.Infra.Service
{
    /// <summary>
    /// Service base interface
    /// 服务的基础接口
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Unique service name; set from <see cref="ServiceConfig.AddConfig"/>.
        /// 服务唯一名称；由 <see cref="ServiceConfig.AddConfig"/> 写入。
        /// </summary>
        string ServiceName { get; set; }
    }
}