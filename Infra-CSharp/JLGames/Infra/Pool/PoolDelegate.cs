namespace JLGames.Infra.Pool
{
    public static class PoolDelegate
    {
        /// <summary>
        /// Clone object
        /// 克隆对象
        /// </summary>
        /// <param name="origin"></param>
        /// <typeparam name="T"></typeparam>
        public delegate T CloneObject<T>(T origin) where T : class;
    }
}