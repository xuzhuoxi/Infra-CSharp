namespace JLGames.Infra.Event
{
    /// <summary>
    /// Event module constants.
    /// 事件模块常量。
    /// </summary>
    public static class EventConst
    {
        /// <summary>
        /// Default listener weight when not specified (higher runs first).
        /// 未指定时的默认监听权重（数值越大越先执行）。
        /// </summary>
        public const int DefaultWeight = 50;
    }
}