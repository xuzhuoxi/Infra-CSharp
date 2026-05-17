using System;
using System.Collections.Generic;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Registry of configured services; used by <see cref="ServiceManager"/> during startup.
    /// 已注册服务的配置表，供 <see cref="ServiceManager"/> 在启动流程中使用。
    /// </summary>
    public class ServiceConfig
    {
        private readonly List<ServiceInfo> m_Config = new List<ServiceInfo>();

        /// <summary>
        /// Total number of configured services
        /// 已配置的服务的总个数
        /// </summary>
        public int ServiceSize => m_Config.Count;

        /// <summary>
        /// Get all service configuration list
        /// 取全部的服务配置列表
        /// </summary>
        public ServiceInfo[] ServiceInfos => m_Config.ToArray();

        /// <summary>
        /// Add a service to the end of the configuration list
        /// 添加服务到配置列表尾部
        /// </summary>
        /// <param name="sc">Service entry to append.<br/>要追加的服务配置项。</param>
        /// <param name="ignoreSame">When false, throws if <see cref="ServiceInfo.ServiceName"/> already exists.<br/>为 false 时，若 <see cref="ServiceInfo.ServiceName"/> 已存在则抛出异常。</param>
        /// <exception cref="Exception">Thrown when <paramref name="ignoreSame"/> is false and the service name is duplicate.<br/>当 <paramref name="ignoreSame"/> 为 false 且服务名重复时抛出。</exception>
        public void AddConfig(ServiceInfo sc, bool ignoreSame = true)
        {
            if (!ignoreSame && ContainsService(sc.ServiceName))
            {
                throw new Exception($"重复的ServiceName:{sc.ServiceName}");
            }

            m_Config.Add(sc);
            sc.ServiceImpl.ServiceName = sc.ServiceName;
        }

        /// <summary>
        /// Get the specified server implementation object
        /// 取指定服务器实现对象
        /// </summary>
        /// <param name="serviceName">Unique service name.<br/>服务唯一名称。</param>
        /// <returns>Implementation instance, or null if not found.<br/>实现实例；未找到时为 null。</returns>
        public IService GetServiceImpl(string serviceName)
        {
            return GetServiceImpl<IService>(serviceName);
        }

        /// <summary>
        /// Take the specified server implementation object and use generics to reduce the amount of code writing
        /// 取指定服务器实现对象，使用泛型为了减少代码编写量
        /// </summary>
        /// <param name="serviceName">Unique service name.<br/>服务唯一名称。</param>
        /// <typeparam name="T">Expected implementation type.<br/>期望的实现类型。</typeparam>
        /// <returns>Cast implementation, or null if missing or incompatible.<br/>转换后的实现；不存在或类型不匹配时为 null。</returns>
        public T GetServiceImpl<T>(string serviceName) where T : class
        {
            return GetServiceInfo(serviceName)?.GetServiceImpl<T>();
        }

        /// <summary>
        /// Check service existence
        /// 检查服务存在性
        /// </summary>
        /// <param name="serviceName">Unique service name.<br/>服务唯一名称。</param>
        /// <returns>True if a service with this name is registered.<br/>已注册同名服务时返回 true。</returns>
        public bool ContainsService(string serviceName)
        {
            return GetServiceInfo(serviceName) != null;
        }

        /// <summary>
        /// Get a specified service configuration information
        /// 取指定一个的服务配置信息
        /// </summary>
        /// <param name="serviceName">Unique service name.<br/>服务唯一名称。</param>
        /// <returns>Configuration entry, or null if not found.<br/>配置项；未找到时为 null。</returns>
        public ServiceInfo GetServiceInfo(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName)) return null;
            return m_Config.Find((info => info.ServiceName == serviceName));
        }

        //-----------------------------------------

        /// <summary>
        /// Global service configuration singleton.
        /// 全局服务配置单例。
        /// </summary>
        public static ServiceConfig Shared { get; } = new ServiceConfig();

        private ServiceConfig()
        {
        }
    }
}