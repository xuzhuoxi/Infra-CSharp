using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    public abstract class ServiceBase : EventDispatcher, IService, IClearService
    {
        protected delegate void ProcessingCall();

        public string ServiceName { get; set; }

        protected bool m_Inited = false;
        protected bool m_DataInited = false;

        public bool IsInited => m_Inited;
        public bool IsDataInited => m_DataInited;

        protected uint m_ProgressingLen = 1;
        protected uint m_ProgressingCurrent = 0;

        public uint ProgressingLen => m_ProgressingLen;
        public uint ProgressingCurrent => m_ProgressingCurrent;


        public virtual void Clear()
        {
            RemoveEventListener();
            m_Inited = false;
            m_DataInited = false;
        }

        //----------------------------

        protected virtual void InvokdProcessing(ProcessingCall call)
        {
            m_ProgressingCurrent++;
            DispatchEvent(ServiceEvents.OnServiceProcessing, ServiceName);
            call?.Invoke();
        }

        protected virtual void InvokeInited()
        {
//            DebugUtil.Log("InvokeInited", ServiceName);
            m_Inited = true;
            DispatchEvent(ServiceEvents.OnServiceInited, ServiceName);
        }

        protected virtual void InvokeDataInited()
        {
//            DebugUtil.Log("InvokeDataInited", ServiceName);
            m_DataInited = true;
            DispatchEvent(ServiceEvents.OnServiceDataInited, ServiceName);
        }

        protected virtual void InvokeDataLoaded()
        {
//            DebugUtil.Log("InvokeDataLoaded", ServiceName);
            DispatchEvent(ServiceEvents.OnServiceDataLoaded, ServiceName);
        }

        protected virtual void InvokeDataSaved()
        {
//            DebugUtil.Log("InvokeDataSaved", ServiceName);
            DispatchEvent(ServiceEvents.OnServiceDataSaved, ServiceName);
        }
    }
}