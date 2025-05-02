using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    internal sealed class ServiceLoadDataHandler : ServiceHandler
    {
        public ServiceLoadDataHandler(ServiceInfo[] services, IEventDispatcher eventDispatcher) : base(services, eventDispatcher)
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
                m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceDataAllLoaded, null);
                return;
            }

            var serviceInfo = CurrentServiceInfo;
            var checkIs = serviceInfo.IsLoadDataService;
            m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceDataLoadStart,
                new ServiceResultData {ServiceName = serviceInfo.ServiceName, Succ = checkIs});
            if (checkIs)
            {
                DoLoadServiceData(serviceInfo);
            }
            else
            {
                m_ServiceIndex++;
                TryNextService();
            }
        }

        private void DoLoadServiceData(ServiceInfo serviceInfo)
        {
            var service = serviceInfo.GetServiceImpl<ILoadDataService>();
            service.OnceEventListener(ServiceEvents.OnServiceDataLoaded, OnServiceDataLoaded);
            service.LoadData();
        }

        private void OnServiceDataLoaded(EventData evd)
        {
            var serviceName = evd.Data as string;

            m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceDataLoaded, serviceName);
            m_ServiceIndex++;
            TryNextService();
        }
    }
}