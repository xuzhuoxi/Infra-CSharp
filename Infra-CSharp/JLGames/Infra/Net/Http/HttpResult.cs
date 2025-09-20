using System;
using System.Net;

namespace JLGames.Infra.Net
{
    public struct HttpResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 是否超时
        /// </summary>
        public bool Timeout { get; set; }

        /// <summary>
        /// 结果数据
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; }
    }
}