using System;
using System.Collections.Generic;

namespace JLGames.Infra.Service
{
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
        /// <param name="sc"></param>
        /// <param name="ignoreSame"></param>
        /// <exception cref="Exception"></exception>
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
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public IService GetServiceImpl(string serviceName)
        {
            return GetServiceImpl<IService>(serviceName);
        }

        /// <summary>
        /// Take the specified server implementation object and use generics to reduce the amount of code writing
        /// 取指定服务器实现对象，使用泛型为了减少代码编写量
        /// </summary>
        /// <param name="serviceName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetServiceImpl<T>(string serviceName) where T : class
        {
            return GetServiceInfo(serviceName)?.GetServiceImpl<T>();
        }

        /// <summary>
        /// Check service existence
        /// 检查服务存在性
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public bool ContainsService(string serviceName)
        {
            return GetServiceInfo(serviceName) != null;
        }

        /// <summary>
        /// Get a specified service configuration information
        /// 取指定一个的服务配置信息
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public ServiceInfo GetServiceInfo(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName)) return null;
            return m_Config.Find((info => info.ServiceName == serviceName));
        }

        //-----------------------------------------

        public static ServiceConfig Shared { get; } = new ServiceConfig();

        private ServiceConfig()
        {
        }
    }
}