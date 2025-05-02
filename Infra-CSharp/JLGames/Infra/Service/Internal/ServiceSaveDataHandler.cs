using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    internal sealed class ServiceSaveDataHandler : ServiceHandler
    {
        public ServiceSaveDataHandler(ServiceInfo[] services, IEventDispatcher eventDispatcher) : base(services, eventDispatcher)
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
                m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceDataAllSaved, null);
                return;
            }

            var serviceInfo = CurrentServiceInfo;
            var checkIs = serviceInfo.IsSaveDataService;
            m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceDataSaveStart,
                new ServiceResultData {ServiceName = serviceInfo.ServiceName, Succ = checkIs});
            if (checkIs)
            {
                DoSaveServiceData(serviceInfo);
            }
            else
            {
                m_ServiceIndex++;
                TryNextService();
            }
        }

        private void DoSaveServiceData(ServiceInfo serviceInfo)
        {
            var service = serviceInfo.GetServiceImpl<ISaveDataService>();
            service.OnceEventListener(ServiceEvents.OnServiceDataSaved, OnServiceDataLoaded);
            service.SaveData();
        }

        private void OnServiceDataLoaded(EventData evd)
        {
            var serviceName = evd.Data as string;

            m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceDataSaved, serviceName);
            m_ServiceIndex++;
            TryNextService();
        }
    }
}