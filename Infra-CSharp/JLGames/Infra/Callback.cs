namespace JLGames.Infra
{
    /// <summary>
    /// Wraps a delegate with optional bound arguments for deferred invocation (e.g. service completion callbacks).
    /// 封装委托及可选绑定参数，供延迟调用（如服务完成回调）。
    /// </summary>
    public class Callback
    {
        /// <summary>
        /// Callback delegate signature; receives invocation arguments.
        /// 回调委托签名；接收调用时传入的参数。
        /// </summary>
        /// <param name="args">Arguments passed to <see cref="Invoke"/> or <see cref="Apply"/>.<br/>传给 <see cref="Invoke"/> 或 <see cref="Apply"/> 的参数。</param>
        public delegate void Func(params object[] args);

        private Func m_Func;
        private object[] m_Args;

        /// <summary>
        /// True when no delegate is bound (<see cref="Clear"/> was called or constructor received null).
        /// 未绑定委托时为 true（已 <see cref="Clear"/> 或构造时传入 null）。
        /// </summary>
        public bool IsNone => m_Func == null;

        /// <summary>
        /// Creates a callback with a delegate and optional arguments for <see cref="Invoke"/>.
        /// 创建回调，绑定委托及供 <see cref="Invoke"/> 使用的可选参数。
        /// </summary>
        /// <param name="func">Delegate to invoke; may be null.<br/>要调用的委托；可为 null。</param>
        /// <param name="args">Bound arguments used by <see cref="Invoke"/>; ignored by <see cref="Apply"/>.<br/><see cref="Invoke"/> 使用的绑定参数；<see cref="Apply"/> 不使用。</param>
        public Callback(Func func, params object[] args)
        {
            m_Func = func;
            m_Args = args;
        }

        /// <summary>
        /// Replaces the bound delegate.
        /// 替换已绑定的委托。
        /// </summary>
        /// <param name="func">New delegate; may be null.<br/>新委托；可为 null。</param>
        public void SetFunc(Func func)
        {
            m_Func = func;
        }

        /// <summary>
        /// Replaces bound arguments used by <see cref="Invoke"/>.
        /// 替换 <see cref="Invoke"/> 使用的绑定参数。
        /// </summary>
        /// <param name="args">New argument array.<br/>新的参数数组。</param>
        public void SetArgs(params object[] args)
        {
            m_Args = args;
        }

        /// <summary>
        /// Invokes the delegate with the given arguments (does not use bound args from construction).
        /// 使用传入参数调用委托（不使用构造时绑定的参数）。
        /// </summary>
        /// <param name="args">Arguments passed to the delegate.<br/>传给委托的参数。</param>
        public void Apply(params object[] args)
        {
            m_Func(args);
        }

        /// <summary>
        /// Invokes the delegate with bound arguments from the constructor or <see cref="SetArgs"/>.
        /// 使用构造或 <see cref="SetArgs"/> 绑定的参数调用委托。
        /// </summary>
        public void Invoke()
        {
            m_Func(m_Args);
        }

        /// <summary>
        /// Clears the delegate and bound arguments; <see cref="IsNone"/> becomes true.
        /// 清除委托与绑定参数；此后 <see cref="IsNone"/> 为 true。
        /// </summary>
        public void Clear()
        {
            m_Func = null;
            m_Args = null;
        }
    }
}
