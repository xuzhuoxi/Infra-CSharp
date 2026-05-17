namespace JLGames.Infra.Service
{
    /// <summary>
    /// Payload for per-service result events data structure (inject, awake, init start, load/save start, etc.).
    /// 单个服务结果类事件（注入、激活、初始化开始、加载/保存开始等）的数据结构。
    /// </summary>
    public struct ServiceResultData
    {
        /// <summary>
        /// Affected service name.
        /// 相关服务名称。
        /// </summary>
        public string ServiceName;

        /// <summary>
        /// Whether the service implements the capability required for that step (see event docs in <see cref="ServiceEvents"/>).
        /// 该服务是否实现了对应步骤所需的 capability（详见 <see cref="ServiceEvents"/> 中的事件说明）。
        /// </summary>
        public bool Succ;
    }
}
