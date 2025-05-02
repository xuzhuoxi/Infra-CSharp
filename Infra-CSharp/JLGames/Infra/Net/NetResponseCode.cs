namespace JLGames.Infra.Net
{
    public static class NetResponseCode
    {   
        /// <summary>
        /// 成功
        /// </summary>
        public const int Suc = 0;
        /// <summary>
        /// 协议错误-协议不存在
        /// </summary>
        public const int ProtoFail = 1;
        /// <summary>
        /// 参数错误
        /// </summary>
        public const int Args = 2;
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        public const int Internal = 3;
        /// <summary>
        /// 数据库执行错误
        /// </summary>
        public const int DbQuery = 4;
        /// <summary>
        /// 请求超时
        /// </summary>
        public const int Timeout = 5;
        /// <summary>
        /// 权限不足
        /// </summary>
        public const int Right = 6;
        /// <summary>
        /// 状态不匹配
        /// </summary>
        public const int Status = 7;
        /// <summary>
        /// 请求重复
        /// </summary>
        public const int Repeat = 8;
        /// <summary>
        /// 请求过于频繁
        /// </summary>
        public const int Frequent = 9;
        /// <summary>
        /// 其它错误
        /// </summary>
        public const int Other = 10;
    }
}