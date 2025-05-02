using System.Collections.Generic;
using JLGames.Infra.Event;

namespace JLGames.Infra.Serial
{
    public sealed class SerialManager : EventDispatcher, ISerialManager
    {
        private readonly List<ISerialModule> m_Modules = new List<ISerialModule>();
        private SerialStatus m_Status = SerialStatus.Stopped;
        private int m_Index;
        private Callback m_EndCall;

        public void AppendModule(ISerialModule module)
        {
            if (null == module) return;
            m_Modules.Add(module);
        }

        public bool StartManager(Callback endCall = null)
        {
            if (m_Status != SerialStatus.Stopped) return false;

            m_EndCall = endCall;
            m_Status = SerialStatus.Starting;
            m_Index = 0;
            StartModule();
            return true;
        }

        public bool StopManager(Callback endCall = null)
        {
            if (m_Status != SerialStatus.Started) return false;

            m_EndCall = endCall;
            m_Status = SerialStatus.Stopping;
            m_Index = m_Modules.Count - 1;
            StopModule();
            return true;
        }

        private void StartModule()
        {
            if (m_Index >= m_Modules.Count)
            {
                m_Status = SerialStatus.Started;
                m_EndCall?.Invoke();
                DispatchEvent(SerialEvents.EventOnManagerStarted, null);
                return;
            }

            var o = m_Modules[m_Index];
            o.OnceEventListener(SerialEvents.EventOnModuleStarted, OnModuleStartup);
            o.Startup();
        }


        private void OnModuleStartup(EventData evd)
        {
            m_Index += 1;
            StartModule();
        }

        private void StopModule()
        {
            if (m_Index < 0)
            {
                m_Status = SerialStatus.Stopped;
                m_EndCall?.Invoke();
                DispatchEvent(SerialEvents.EventOnManagerStopped, null);
                return;
            }

            var o = m_Modules[m_Index];
            o.OnceEventListener(SerialEvents.EventOnModuleStopped, OnModuleShutdown);
            o.Shutdown();
        }

        private void OnModuleShutdown(EventData evd)
        {
            m_Index -= 1;
            StopModule();
        }
    }
}