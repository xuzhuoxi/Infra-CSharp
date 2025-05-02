using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    internal sealed class ServiceInitDataHandler : ServiceHandler
    {
        public ServiceInitDataHandler(ServiceInfo[] services, IEventDispatcher eventDispatcher) : base(services, eventDispatcher)
        {
        }

        public void Start(Callback endCall = null)
        {
            m_EndCall = endCall;
            m_ServiceIndex = 0;
            TryNextService();
        }

        private void TryNextService()
        {
            if (m_ServiceIndex == m_Services.Length)
            {
                m_EndCall?.Invoke();
                m_EndCall = null;
                m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceDataAllInited, null);
                return;
            }

            var serviceInfo = CurrentServiceInfo;
            var checkIs = serviceInfo.IsInitDataService;
            m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceDataInitStart,
                new ServiceResultData {ServiceName = serviceInfo.ServiceName, Succ = checkIs});
            if (checkIs)
            {
                if (serviceInfo.IsProgressingService)
                {
                    var serviceProcessing = serviceInfo.GetServiceImpl<IProgressingService>();
                    AddProgressingEvents(serviceProcessing);
                }

                DoInitServiceData(serviceInfo);
            }
            else
            {
                m_ServiceIndex++;
                TryNextService();
            }
        }

        private void DoInitServiceData(ServiceInfo serviceInfo)
        {
//            DebugUtil.Log($"准备数据初始化Service[{m_ServiceIndex}]:{serviceInfo.ServiceName}");
            var service = serviceInfo.GetServiceImpl<IInitDataService>();
            service.OnceEventListener(ServiceEvents.OnServiceDataInited, OnServiceDataInited);
            service.InitData();
        }

        private void OnServiceDataInited(EventData evd)
        {
            var serviceName = evd.Data as string;
            if (CurrentServiceInfo.IsProgressingService)
            {
                var serviceProcessing = CurrentServiceInfo.GetServiceImpl<IProgressingService>();
                RemoveProgressingEvents(serviceProcessing);
            }

            m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceDataInited, serviceName);
//            DebugUtil.Log($"完成数据初始化Service[{m_ServiceIndex}]:{serviceName}");

            m_ServiceIndex++;
            TryNextService();
        }
    }
}