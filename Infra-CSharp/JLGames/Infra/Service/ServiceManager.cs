using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    public sealed class ServiceManager : EventDispatcher
    {
        /// <summary>
        /// Callback after all initialization is complete, including Init function and InitData function
        /// 全部初始化完成后回调，包括Init函数与InitData函数
        /// </summary>
        private Callback m_FinishCall;

        private ServiceInfo[] m_Services;
        private uint m_ProcessingLen;
        private uint m_ProcessingFinished;

        /// <summary>
        /// Processing percentage
        /// 处理百分比
        /// </summary>
        public float ProcessingPercentage
        {
            get
            {
                var count = ProcessingFinished;
                if (count >= ProcessingLen) return 1;
                return (float) count / ProcessingLen;
            }
        }

        /// <summary>
        /// Current progress total length
        /// 当前进度总长
        /// </summary>
        public uint ProcessingLen => m_ProcessingLen;

        /// <summary>
        /// Progress completed
        /// 已完成进度
        /// </summary>
        public uint ProcessingFinished => m_ProcessingFinished;

        /// <summary>
        /// cleanup event listener
        /// 清理事件监听
        /// </summary>
        public void ClearEvents()
        {
            RemoveEventListener();
        }

        /// <summary>
        /// Service cleanup
        /// 服务清理
        /// </summary>
        public void ClearServices()
        {
            RemoveEventListener();
            for (var i = m_Services.Length - 1; i >= 0; i--)
            {
                (m_Services[i].ServiceImpl as IClearService)?.Clear();
            }
        }

        /// <summary>
        /// service data loading
        /// 服务数据加载
        /// </summary>
        /// <param name="endCall"></param>
        public void LoadServicesData(Callback endCall)
        {
            var handler = new ServiceLoadDataHandler(m_Services, this);
            handler.Start(endCall);
        }

        /// <summary>
        /// Service data storage
        /// 服务数据保存
        /// </summary>
        /// <param name="endCall"></param>
        public void SaveServicesData(Callback endCall)
        {
            var handler = new ServiceSaveDataHandler(m_Services, this);
            handler.Start(endCall);
        }

        /// <summary>
        /// Initialize the configured service
        /// 初始化配置好的服务
        /// If it has been initialized once, try to execute the callback directly
        /// 如果已经初始化一次，就尝试直接执行回调
        /// </summary>
        /// <param name="endCall"></param>
        public void StartInitalization(Callback endCall)
        {
            m_Services = ServiceConfig.Shared.ServiceInfos;
            m_FinishCall = endCall;

            InjectToServices();
            ActiveServices();
            m_ProcessingLen = GetServiceTotalLen();
            AddProcessingEvents();

            StartInitServices();
        }

        // Progress processing(进度处理)

        private void AddProcessingEvents()
        {
            AddEventListener(ServiceEvents.OnServiceProcessing, OnServiceProcessingUpdate);
            AddEventListener(ServiceEvents.OnServiceInited, OnServiceProcessingUpdate);
            AddEventListener(ServiceEvents.OnServiceDataInited, OnServiceProcessingUpdate);
        }

        private void RemoveProcessingEvents()
        {
            AddEventListener(ServiceEvents.OnServiceDataInited, OnServiceProcessingUpdate);
            AddEventListener(ServiceEvents.OnServiceInited, OnServiceProcessingUpdate);
            AddEventListener(ServiceEvents.OnServiceProcessing, OnServiceProcessingUpdate);
        }

        private void OnServiceProcessingUpdate(EventData evd)
        {
            m_ProcessingFinished = GetServiceCurrentLen();
            DispatchEvent(ServiceEvents.OnInitializationProcessing, null);
        }

        /// <summary>
        /// Inject arguments to the service
        /// 全部服务注入参数
        /// Handle IArgumentService behavior
        /// 处理 IArgumentService 行为
        /// </summary>
        private void InjectToServices()
        {
            foreach (var info in m_Services)
            {
                var succ = info.IsArgumentService;
                if (succ)
                {
                    info.GetServiceImpl<IArgumentService>().InjectArgument(info.Args);
                }

                DispatchEvent(ServiceEvents.OnServiceInjected,
                    new ServiceResultData {ServiceName = info.ServiceName, Succ = succ});
            }

            DispatchEvent(ServiceEvents.OnServiceAllInjected, null);
        }

        /// <summary>
        /// Activate the service
        /// 激活服务
        /// Handle IAWakableService behavior
        /// 处理 IAwakableService 行为
        /// </summary>
        private void ActiveServices()
        {
            foreach (var info in m_Services)
            {
                var succ = info.IsAwakableService;
                if (info.IsAwakableService)
                {
                    info.GetServiceImpl<IAwakableService>().Awake();
                }

                DispatchEvent(ServiceEvents.OnServiceAwaked,
                    new ServiceResultData {ServiceName = info.ServiceName, Succ = succ});
            }

            DispatchEvent(ServiceEvents.OnServiceAllAwaked, null);
        }

        // IInitService

        private void StartInitServices()
        {
            OnceEventListener(ServiceEvents.OnServiceAllInited, OnInitAll);
            var handler = new ServiceInitHandler(m_Services, this);
            handler.Start();
        }

        private void OnInitAll(EventData evd)
        {
            StartInitDataServices();
        }

        // IDataService

        private void StartInitDataServices()
        {
            OnceEventListener(ServiceEvents.OnServiceDataAllInited, OnDataInitAll);
            var handler = new ServiceInitDataHandler(m_Services, this);
            handler.Start();
        }

        private void OnDataInitAll(EventData evd)
        {
            m_FinishCall?.Invoke();
            RemoveProcessingEvents();
            DispatchEvent(ServiceEvents.OnInitializationFinish, null);
        }

        /// <summary>
        /// Statistical total length
        /// 统计总长度
        /// </summary>
        /// <returns></returns>
        private uint GetServiceTotalLen()
        {
            uint len = 0;
            foreach (var service in m_Services)
            {
                if (service.IsProgressingService)
                {
                    var progressService = service.GetServiceImpl<IProgressingService>();
                    len += progressService.ProgressingLen;
                }
                else
                {
                    if (service.IsInitService) len += 1;
                    if (service.IsInitDataService) len += 1;
                }
            }

            return len;
        }

        /// <summary>
        /// Statistical current length
        /// 统计当前长度
        /// </summary>
        /// <returns></returns>
        private uint GetServiceCurrentLen()
        {
            uint len = 0;
            foreach (var service in m_Services)
            {
                if (service.IsProgressingService)
                {
                    var progressService = service.GetServiceImpl<IProgressingService>();
                    len += progressService.ProgressingCurrent;
                }
                else
                {
                    if (service.IsInitService && service.GetServiceImpl<IInitService>().IsInited)
                        len += 1;
                    if (service.IsInitDataService && service.GetServiceImpl<IInitDataService>().IsDataInited)
                        len += 1;
                }
            }

            return len;
        }

        //----------------------------------

        public static ServiceManager Shared { get; } = new ServiceManager();

        public static ServiceConfig Config => ServiceConfig.Shared;
    }
}