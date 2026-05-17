using JLGames.Infra.Event;

namespace JLGames.Infra.Service
{
    /// <summary>
    /// Abstract base for services: lifecycle flags, progress reporting, and event dispatch.
    /// 服务抽象基类：生命周期状态、进度上报与事件派发。
    /// </summary>
    public abstract class ServiceBase : EventDispatcher, IService, IClearService
    {
        protected delegate void ProcessingCall();

        /// <inheritdoc />
        public string ServiceName { get; set; }

        protected bool m_Inited = false;
        protected bool m_DataInited = false;

        /// <summary>
        /// Whether <see cref="IInitService.Init"/> has completed for this service.
        /// 本服务是否已完成 <see cref="IInitService.Init"/> 初始化。
        /// </summary>
        public bool IsInited => m_Inited;

        /// <summary>
        /// Whether <see cref="IInitDataService.InitData"/> has completed for this service.
        /// 本服务是否已完成 <see cref="IInitDataService.InitData"/> 数据初始化。
        /// </summary>
        public bool IsDataInited => m_DataInited;

        protected uint m_ProgressingLen = 1;
        protected uint m_ProgressingCurrent = 0;

        /// <summary>
        /// Total progress steps when implementing <see cref="IProgressingService"/>.
        /// 实现 <see cref="IProgressingService"/> 时的进度总步数。
        /// </summary>
        public uint ProgressingLen => m_ProgressingLen;

        /// <summary>
        /// Completed progress steps; used with <see cref="ProgressingLen"/>.
        /// 已完成的进度步数，与 <see cref="ProgressingLen"/> 配合使用。
        /// </summary>
        public uint ProgressingCurrent => m_ProgressingCurrent;

        /// <inheritdoc />
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