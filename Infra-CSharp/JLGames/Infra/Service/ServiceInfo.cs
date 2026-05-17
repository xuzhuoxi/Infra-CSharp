namespace JLGames.Infra.Service
{
    /// <summary>
    /// Describes one registered service: name, implementation instance, and optional constructor arguments.
    /// 描述单个已注册服务：名称、实现实例及可选的构造/注入参数。
    /// </summary>
    public class ServiceInfo : ICloneable<ServiceInfo>
    {
        private readonly string m_ServiceName;
        private readonly object m_ServiceImpl;
        private readonly object[] m_Args;

        /// <summary>
        /// Unique service name.
        /// 服务唯一名称。
        /// </summary>
        public string ServiceName => m_ServiceName;

        /// <summary>
        /// Service implementation as <see cref="IService"/>.
        /// 以 <see cref="IService"/> 形式暴露的实现实例。
        /// </summary>
        public IService ServiceImpl => GetServiceImpl<IService>();

        /// <summary>
        /// Arguments passed to <see cref="IArgumentService.InjectArgument"/> when applicable.
        /// 在适用时传给 <see cref="IArgumentService.InjectArgument"/> 的参数。
        /// </summary>
        public object[] Args => m_Args;

        /// <summary>
        /// Whether the implementation implements <see cref="IAwakableService"/>.
        /// 判断当前实例是否实现了 <see cref="IAwakableService"/>。true: 实现了; false: 未实现。
        /// </summary>
        public bool IsAwakableService => CheckServiceImpl<IAwakableService>();

        /// <summary>
        /// Whether the implementation implements <see cref="IInitService"/>.
        /// 判断当前实例是否实现了 <see cref="IInitService"/>。true: 实现了; false: 未实现。
        /// </summary>
        public bool IsInitService => CheckServiceImpl<IInitService>();

        /// <summary>
        /// Whether the implementation implements <see cref="IArgumentService"/>.
        /// 判断当前实例是否实现了 <see cref="IArgumentService"/>。true: 实现了; false: 未实现。
        /// </summary>
        public bool IsArgumentService => CheckServiceImpl<IArgumentService>();

        /// <summary>
        /// Whether the implementation implements <see cref="IProgressingService"/>.
        /// 判断当前实例是否实现了 <see cref="IProgressingService"/>。true: 实现了; false: 未实现。
        /// </summary>
        public bool IsProgressingService => CheckServiceImpl<IProgressingService>();

        /// <summary>
        /// Whether the implementation implements <see cref="IInitDataService"/>.
        /// 判断当前实例是否实现了 <see cref="IInitDataService"/>。true: 实现了; false: 未实现。
        /// </summary>
        public bool IsInitDataService => CheckServiceImpl<IInitDataService>();

        /// <summary>
        /// Whether the implementation implements <see cref="ILoadDataService"/>.
        /// 判断当前实例是否实现了 <see cref="ILoadDataService"/>。true: 实现了; false: 未实现。
        /// </summary>
        public bool IsLoadDataService => CheckServiceImpl<ILoadDataService>();

        /// <summary>
        /// Whether the implementation implements <see cref="ISaveDataService"/>.
        /// 判断当前实例是否实现了 <see cref="ISaveDataService"/>。true: 实现了; false: 未实现。
        /// </summary>
        public bool IsSaveDataService => CheckServiceImpl<ISaveDataService>();

        /// <summary>
        /// Check the interface state of the implementing object
        /// 检查实现对象的接口状态
        /// </summary>
        /// <typeparam name="T">Interface or base type to test.<br/>要检测的接口或基类型。</typeparam>
        /// <returns>True if the implementation instance is assignable to <typeparamref name="T"/>.<br/>实现实例可赋给 <typeparamref name="T"/> 时返回 true。</returns>
        public bool CheckServiceImpl<T>() where T : class
        {
            return m_ServiceImpl is T;
        }

        /// <summary>
        /// Get the implementation object of the service
        /// 取服务的实现对象
        /// </summary>
        /// <typeparam name="T">Expected implementation type.<br/>期望的实现类型。</typeparam>
        /// <returns>Cast instance, or null if incompatible.<br/>转换后的实例；类型不兼容时为 null。</returns>
        public T GetServiceImpl<T>() where T : class
        {
            return m_ServiceImpl as T;
        }

        /// <summary>
        /// Constructor
        /// 构造方法
        /// </summary>
        /// <param name="serviceName">Unique service name.<br/>服务唯一名称。</param>
        /// <param name="serviceImpl">Service implementation instance.<br/>服务实现实例。</param>
        /// <param name="args">Optional arguments for <see cref="IArgumentService"/>.<br/>供 <see cref="IArgumentService"/> 使用的可选参数。</param>
        public ServiceInfo(string serviceName, IService serviceImpl, params object[] args)
        {
            m_ServiceName = serviceName;
            m_ServiceImpl = serviceImpl;
            m_Args = args;
        }

        /// <summary>
        /// Clone
        /// 克隆
        /// </summary>
        /// <returns>A shallow copy sharing the same implementation reference.<br/>共享同一实现引用的浅拷贝。</returns>
        public ServiceInfo Clone()
        {
            return new ServiceInfo(m_ServiceName, (IService) m_ServiceImpl, m_Args);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{{Name={m_ServiceName},Impl={m_ServiceImpl},Args={m_Args}";
        }
    }
}