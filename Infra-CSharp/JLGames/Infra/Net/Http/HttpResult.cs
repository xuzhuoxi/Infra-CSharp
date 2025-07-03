using System;
using System.Net;

namespace JLGames.Infra.Net
{
    public struct HttpResult<T>
    {
        /// <summary>
        /// 是否超时
        /// </summary>
        public bool Timeout { get; internal set; }
        
        /// <summary>
        /// 状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }
        
        /// <summary>
        /// 结果数据
        /// </summary>
        public T Content { get; internal set; }
        
        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; internal set; }
    }
}