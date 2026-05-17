using System;
using System.Net;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// HTTP request result wrapper.
    /// HTTP 请求结果封装。
    /// </summary>
    /// <typeparam name="T">Response body type.<br/>响应体类型。</typeparam>
    public struct HttpResult<T>
    {
        /// <summary>
        /// HTTP status code.
        /// HTTP 状态码。
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Whether the request timed out.
        /// 是否请求超时。
        /// </summary>
        public bool Timeout { get; set; }

        /// <summary>
        /// Response body content.
        /// 响应内容。
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// Exception if the request failed (may be null).
        /// 请求失败时的异常（可为 null）。
        /// </summary>
        public Exception Exception { get; set; }
    }
}