namespace JLGames.Infra.Service
{
    public class ServiceInfo : ICloneable<ServiceInfo>
    {
        /// <summary>
        /// service name, unique identifier
        /// 服务名称，唯一标识
        /// </summary>
        private readonly string m_ServiceName;

        /// <summary>
        /// Service implementation object
        /// 服务的实现对象
        /// </summary>
        private readonly object m_ServiceImpl;

        /// <summary>
        /// initialization is the parameter to inject
        /// 初始化是要注入的参数
        /// </summary>
        private readonly object[] m_Args;

        public string ServiceName => m_ServiceName;
        public IService ServiceImpl => GetServiceImpl<IService>();
        public object[] Args => m_Args;
        public bool IsAwakableService => CheckServiceImpl<IAwakableService>();
        public bool IsInitService => CheckServiceImpl<IInitService>();
        public bool IsArgumentService => CheckServiceImpl<IArgumentService>();
        public bool IsProgressingService => CheckServiceImpl<IProgressingService>();
        public bool IsInitDataService => CheckServiceImpl<IInitDataService>();
        public bool IsLoadDataService => CheckServiceImpl<ILoadDataService>();
        public bool IsSaveDataService => CheckServiceImpl<ISaveDataService>();

        /// <summary>
        /// Check the interface state of the implementing object
        /// 检查实现对象的接口状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool CheckServiceImpl<T>() where T : class
        {
            return m_ServiceImpl is T;
        }

        /// <summary>
        /// Get the implementation object of the service
        /// 取服务的实现对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetServiceImpl<T>() where T : class
        {
            return m_ServiceImpl as T;
        }

        /// <summary>
        /// Constructor
        /// 构造方法
        /// </summary>
        /// <param name="serviceName">服务标识</param>
        /// <param name="serviceImpl">实现类的实例</param>
        /// <param name="args">参数</param>
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
        /// <returns></returns>
        public ServiceInfo Clone()
        {
            return new ServiceInfo(m_ServiceName, (IService) m_ServiceImpl, m_Args);
        }

        public override string ToString()
        {
            return $"{{Name={m_ServiceName},Impl={m_ServiceImpl},Args={m_Args}";
        }
    }
}