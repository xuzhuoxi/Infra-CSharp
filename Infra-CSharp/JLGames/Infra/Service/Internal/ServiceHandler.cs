using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    internal abstract class ServiceHandler
    {
        protected readonly ServiceInfo[] m_Services;
        protected readonly IEventDispatcher m_EventDispatcher;
        protected int m_ServiceIndex = 0;
        protected Callback m_EndCall;

        protected ServiceInfo CurrentServiceInfo => m_Services[m_ServiceIndex];

        public ServiceHandler(ServiceInfo[] services, IEventDispatcher eventDispatcher)
        {
            m_Services = services;
            m_EventDispatcher = eventDispatcher;
        }

        protected void AddProgressingEvents(IProgressingService service)
        {
            service.AddEventListener(ServiceEvents.OnServiceProcessing, OnServiceProcessing);
        }

        protected void RemoveProgressingEvents(IProgressingService service)
        {
            service.RemoveEventListener(ServiceEvents.OnServiceProcessing, OnServiceProcessing);
        }

        private void OnServiceProcessing(EventData evd)
        {
            var serviceName = evd.Data as string;
            m_EventDispatcher.DispatchEvent(ServiceEvents.OnServiceProcessing, serviceName);
        }
    }
}