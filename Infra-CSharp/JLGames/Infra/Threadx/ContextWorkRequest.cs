using System.Threading;

namespace JLGames.Infra.Threadx
{
    public readonly struct ContextWorkRequest
    {
        private readonly SendOrPostCallback m_DelegateCallback;
        private readonly object m_DelegateState;
        private readonly ManualResetEvent m_WaitHandle;

        public ContextWorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
        {
            m_DelegateCallback = callback;
            m_DelegateState = state;
            m_WaitHandle = waitHandle;
        }

        public void Invoke()
        {
            try
            {
                m_DelegateCallback(m_DelegateState);
            }
            finally
            {
                m_WaitHandle?.Set();
            }
        }
    }
}