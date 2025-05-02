using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    internal sealed class ServiceInitHandler : ServiceHandler
    {
        public ServiceInitHandler(ServiceInfo[] services, IEventDispatcher eventDispatcher) : base(services, eventDispatcher)
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
                m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceAllInited, null);
                return;
            }

            var serviceInfo = CurrentServiceInfo;
            var checkIs = serviceInfo.IsInitService;
            m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceInitStart,
                new ServiceResultData {ServiceName = serviceInfo.ServiceName, Succ = checkIs});
            if (checkIs)
            {
                if (serviceInfo.IsProgressingService)
                {
                    var serviceProcessing = serviceInfo.GetServiceImpl<IProgressingService>();
                    AddProgressingEvents(serviceProcessing);
                }

                DoInitService(serviceInfo);
            }
            else
            {
                m_ServiceIndex++;
                TryNextService();
            }
        }

        private void DoInitService(ServiceInfo serviceInfo)
        {
//            DebugUtil.Log($"准备初始化Service[{m_ServiceIndex}]:{serviceInfo.ServiceName}");
            var service = serviceInfo.GetServiceImpl<IInitService>();
            service.OnceEventListener(ServiceEvents.OnServiceInited, OnServiceInited);
            service.Init();
        }

        private void OnServiceInited(EventData evd)
        {
            var serviceName = evd.Data as string;
            if (CurrentServiceInfo.IsProgressingService)
            {
                var serviceProcessing = CurrentServiceInfo.GetServiceImpl<IProgressingService>();
                RemoveProgressingEvents(serviceProcessing);
            }

            m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceInited, serviceName);
//            DebugUtil.Log($"完成初始化Service[{m_ServiceIndex}]:{serviceName}");

            m_ServiceIndex++;
            TryNextService();
        }
    }
}