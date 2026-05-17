namespace JLGames.Infra.Pool
{
    /// <summary>
    /// Pool-related delegates.
    /// 对象池相关委托。
    /// </summary>
    public static class PoolDelegate
    {
        /// <summary>
        /// Clone an object instance.
        /// 克隆对象实例。
        /// </summary>
        /// <param name="origin">Source instance to clone.<br/>待克隆的源实例。</param>
        /// <typeparam name="T">Reference type to clone.<br/>待克隆的引用类型。</typeparam>
        /// <returns>Cloned instance.<br/>克隆后的实例。</returns>
        public delegate T CloneObject<T>(T origin) where T : class;
    }
}
