namespace JLGames.Infra
{
    /// <summary>
    /// Common callback context
    /// 通用回调上下文
    /// </summary>
    public class Callback
    {
        public delegate void Func(params object[] args);

        private Func m_Func;
        private object[] m_Args;

        public bool IsNone => m_Func == null;

        public Callback(Func func, params object[] args)
        {
            m_Func = func;
            m_Args = args;
        }

        public void SetFunc(Func func)
        {
            m_Func = func;
        }

        public void SetArgs(params object[] args)
        {
            m_Args = args;
        }

        public void Apply(params object[] args)
        {
            m_Func(args);
        }

        public void Invoke()
        {
            m_Func(m_Args);
        }

        public void Clear()
        {
            m_Func = null;
            m_Args = null;
        }
    }
}