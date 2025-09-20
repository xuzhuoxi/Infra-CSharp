using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Http客户端代理
    /// 注意：Unity上支持有限制，建议在Unity上使用UnityWebRequest
    /// Url合并规则：
    ///   - "http://127.0.0.1:9000/api/sub"  + "test"  = "http://127.0.0.1:9000/api/test"
    ///   - "http://127.0.0.1:9000/api/sub/" + "test"  = "http://127.0.0.1:9000/api/sub/test"
    ///   - "http://127.0.0.1:9000/api/sub"  + "/test" = "http://127.0.0.1:9000/test"
    ///   - "http://127.0.0.1:9000/api/sub/" + "/test" = "http://127.0.0.1:9000/test"
    /// </summary>
    public sealed class HttpClientProxy : IHttpClientProxy
    {
        private const string c_KeepAlive = "keep-alive";

        // 默认超时时间100秒，实际上.net不设置timeout时，超时限制也是100秒
        private readonly TimeSpan m_Timeout = TimeSpan.FromSeconds(100);
        private readonly string m_BaseAddress;
        private readonly Uri m_BaseUri;

        private readonly HttpClient m_Client;

        /// <summary>
        /// 创建Http客户端代理<br/>
        /// 使用默认超时时间100秒<br/>
        /// </summary>
        public HttpClientProxy()
        {
            m_BaseAddress = "";
            m_Client = new HttpClient { Timeout = m_Timeout };
        }

        /// <summary>
        /// 创建Http客户端代理<br/>
        /// 设置基础URL<br/>
        /// 使用默认超时时间100秒<br/>
        /// 会进行预热，避免第一次请求时性能低<br/>
        /// </summary>
        /// <param name="baseUrl"></param>
        public HttpClientProxy(string baseUrl)
        {
            m_BaseAddress = baseUrl;
            m_BaseUri = new Uri(baseUrl);
            m_Client = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = m_Timeout };
            m_Client.DefaultRequestHeaders.Connection.Add(c_KeepAlive);
            Preheat();
        }

        /// <summary>
        /// 创建Http客户端代理<br/>
        /// 设置基础URL<br/>
        /// 使用自定义超时<br/>
        /// 会进行预热，避免第一次请求时性能低<br/>
        /// </summary>
        /// <param name="baseUrl">应该以"/"结尾，否则会使用相对路径</param>
        /// <param name="timeout"></param>
        public HttpClientProxy(string baseUrl, TimeSpan timeout)
        {
            m_BaseAddress = baseUrl;
            m_BaseUri = new Uri(baseUrl);
            m_Timeout = timeout;
            m_Client = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = timeout };
            m_Client.DefaultRequestHeaders.Connection.Add(c_KeepAlive);
            Preheat();
        }

        /// <summary>
        /// 设置超时时间<br/>
        /// 不影响创建时设置的超时设置<br/>
        /// </summary>
        /// <param name="timeout"></param>
        public void SetTimeout(TimeSpan timeout)
        {
            m_Client.Timeout = timeout;
        }

        /// <summary>
        /// 重置超时时间<br/>
        /// 使用创建时设置的超时设置
        /// </summary>
        public void ResetTimeout()
        {
            m_Client.Timeout = m_Timeout;
        }

        /// <summary>
        /// 设置是不否使用长连接
        /// </summary>
        /// <param name="enable"></param>
        public void SetKeepAlive(bool enable)
        {
            var contains = m_Client.DefaultRequestHeaders.Connection.Contains(c_KeepAlive);
            if (contains == enable) return;
            if (enable)
                m_Client.DefaultRequestHeaders.Connection.Add(c_KeepAlive);
            else
                m_Client.DefaultRequestHeaders.Connection.Remove(c_KeepAlive);
        }

        //  Get ----------

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        public Task<HttpResult<byte[]>> GetBytesAsync(string pattern)
        {
            return GetBytesAsync(m_BaseUri, pattern);
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="timeout">超时时间</param>
        public Task<HttpResult<byte[]>> GetBytesAsync(string pattern, TimeSpan timeout)
        {
            return GetBytesAsync(m_BaseUri, pattern, timeout);
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        public async Task<HttpResult<byte[]>> GetBytesAsync(Uri baseUri, string pattern)
        {
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.GetAsync(fullUrl);
                var result = await msg.Content.ReadAsByteArrayAsync();
                return new HttpResult<byte[]> { StatusCode = msg.StatusCode, Content = result };
            }
            catch (UriFormatException e)
            {
                return new HttpResult<byte[]> { StatusCode = HttpStatusCode.NotFound, Exception = e };
            }
            catch (TaskCanceledException e1)
            {
                return new HttpResult<byte[]> { Timeout = true, StatusCode = HttpStatusCode.RequestTimeout, Exception = e1 };
            }
            catch (Exception e2)
            {
                return new HttpResult<byte[]> { StatusCode = HttpStatusCode.NotFound, Exception = e2 };
            }
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="timeout">超时时间</param>
        public async Task<HttpResult<byte[]>> GetBytesAsync(Uri baseUri, string pattern, TimeSpan timeout)
        {
            var cts = new CancellationTokenSource(timeout);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.GetAsync(fullUrl, cts.Token);
                var result = await msg.Content.ReadAsByteArrayAsync();
                return new HttpResult<byte[]> { StatusCode = msg.StatusCode, Content = result };
            }
            catch (UriFormatException e)
            {
                return new HttpResult<byte[]> { StatusCode = HttpStatusCode.NotFound, Exception = e };
            }
            catch (TaskCanceledException e1)
            {
                return new HttpResult<byte[]> { Timeout = true, StatusCode = HttpStatusCode.RequestTimeout, Exception = e1 };
            }
            catch (Exception e2)
            {
                return new HttpResult<byte[]> { StatusCode = HttpStatusCode.NotFound, Exception = e2 };
            }
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        public Task<HttpResult<string>> GetStringAsync(string pattern)
        {
            return GetStringAsync(m_BaseUri, pattern);
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="timeout">超时时间</param>
        public Task<HttpResult<string>> GetStringAsync(string pattern, TimeSpan timeout)
        {
            return GetStringAsync(m_BaseUri, pattern, timeout);
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        public async Task<HttpResult<string>> GetStringAsync(Uri baseUri, string pattern)
        {
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.GetAsync(fullUrl);
                var result = await msg.Content.ReadAsStringAsync();
                return new HttpResult<string> { StatusCode = msg.StatusCode, Content = result };
            }
            catch (UriFormatException e)
            {
                return new HttpResult<string> { StatusCode = HttpStatusCode.NotFound, Exception = e };
            }
            catch (TaskCanceledException e1)
            {
                return new HttpResult<string> { Timeout = true, StatusCode = HttpStatusCode.RequestTimeout, Exception = e1 };
            }
            catch (Exception e2)
            {
                return new HttpResult<string> { StatusCode = HttpStatusCode.NotFound, Exception = e2 };
            }
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="timeout">超时时间</param>
        public async Task<HttpResult<string>> GetStringAsync(Uri baseUri, string pattern, TimeSpan timeout)
        {
            var cts = new CancellationTokenSource(timeout);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.GetAsync(fullUrl, cts.Token);
                var result = await msg.Content.ReadAsStringAsync();
                return new HttpResult<string> { StatusCode = msg.StatusCode, Content = result };
            }
            catch (UriFormatException e)
            {
                return new HttpResult<string> { StatusCode = HttpStatusCode.NotFound, Exception = e };
            }
            catch (TaskCanceledException e1)
            {
                return new HttpResult<string> { Timeout = true, StatusCode = HttpStatusCode.RequestTimeout, Exception = e1 };
            }
            catch (Exception e2)
            {
                return new HttpResult<string> { StatusCode = HttpStatusCode.NotFound, Exception = e2 };
            }
        }

        //  Post ----------

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        public Task<HttpResult<byte[]>> PostBytesAsync(string pattern, Dictionary<string, string> value)
        {
            return PostBytesAsync(m_BaseUri, pattern, value);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        /// <param name="timeout">超时时间</param>
        public Task<HttpResult<byte[]>> PostBytesAsync(string pattern, Dictionary<string, string> value, TimeSpan timeout)
        {
            return PostBytesAsync(m_BaseUri, pattern, value, timeout);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        public async Task<HttpResult<byte[]>> PostBytesAsync(Uri baseUri, string pattern, Dictionary<string, string> value)
        {
            var content = null == value ? null : new FormUrlEncodedContent(value);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.PostAsync(fullUrl, content);
                var result = await msg.Content.ReadAsByteArrayAsync();
                return new HttpResult<byte[]> { StatusCode = msg.StatusCode, Content = result };
            }
            catch (UriFormatException e)
            {
                return new HttpResult<byte[]> { StatusCode = HttpStatusCode.NotFound, Exception = e };
            }
            catch (TaskCanceledException e1)
            {
                return new HttpResult<byte[]> { Timeout = true, StatusCode = HttpStatusCode.RequestTimeout, Exception = e1 };
            }
            catch (Exception e2)
            {
                return new HttpResult<byte[]> { StatusCode = HttpStatusCode.NotFound, Exception = e2 };
            }
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        /// <param name="timeout">超时时间</param>
        public async Task<HttpResult<byte[]>> PostBytesAsync(Uri baseUri, string pattern, Dictionary<string, string> value, TimeSpan timeout)
        {
            var content = null == value ? null : new FormUrlEncodedContent(value);
            var cts = new CancellationTokenSource(timeout);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.PostAsync(fullUrl, content, cts.Token);
                var result = await msg.Content.ReadAsByteArrayAsync();
                return new HttpResult<byte[]> { StatusCode = msg.StatusCode, Content = result };
            }
            catch (UriFormatException e)
            {
                return new HttpResult<byte[]> { StatusCode = HttpStatusCode.NotFound, Exception = e };
            }
            catch (TaskCanceledException e1)
            {
                return new HttpResult<byte[]> { Timeout = true, StatusCode = HttpStatusCode.RequestTimeout, Exception = e1 };
            }
            catch (Exception e2)
            {
                return new HttpResult<byte[]> { StatusCode = HttpStatusCode.NotFound, Exception = e2 };
            }
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        public Task<HttpResult<string>> PostStringAsync(string pattern, Dictionary<string, string> value)
        {
            return PostStringAsync(m_BaseUri, pattern, value);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        /// <param name="timeout">超时时间</param>
        public Task<HttpResult<string>> PostStringAsync(string pattern, Dictionary<string, string> value, TimeSpan timeout)
        {
            return PostStringAsync(m_BaseUri, pattern, value, timeout);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="values">参数集</param>
        public async Task<HttpResult<string>> PostStringAsync(Uri baseUri, string pattern, Dictionary<string, string> values)
        {
            var content = null == values ? null : new FormUrlEncodedContent(values);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.PostAsync(fullUrl, content);
                var result = await msg.Content.ReadAsStringAsync();
                return new HttpResult<string> { StatusCode = msg.StatusCode, Content = result };
            }
            catch (UriFormatException e)
            {
                return new HttpResult<string> { StatusCode = HttpStatusCode.NotFound, Exception = e };
            }
            catch (TaskCanceledException e1)
            {
                return new HttpResult<string> { Timeout = true, StatusCode = HttpStatusCode.RequestTimeout, Exception = e1 };
            }
            catch (Exception e2)
            {
                return new HttpResult<string> { StatusCode = HttpStatusCode.NotFound, Exception = e2 };
            }
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="values">参数集</param>
        /// <param name="timeout">超时时间</param>
        public async Task<HttpResult<string>> PostStringAsync(Uri baseUri, string pattern, Dictionary<string, string> values, TimeSpan timeout)
        {
            var content = null == values ? null : new FormUrlEncodedContent(values);
            var cts = new CancellationTokenSource(timeout);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.PostAsync(fullUrl, content, cts.Token);
                var result = await msg.Content.ReadAsStringAsync();
                return new HttpResult<string> { StatusCode = msg.StatusCode, Content = result };
            }
            catch (UriFormatException e)
            {
                return new HttpResult<string> { StatusCode = HttpStatusCode.NotFound, Exception = e };
            }
            catch (TaskCanceledException e1)
            {
                return new HttpResult<string> { Timeout = true, StatusCode = HttpStatusCode.RequestTimeout, Exception = e1 };
            }
            catch (Exception e2)
            {
                return new HttpResult<string> { StatusCode = HttpStatusCode.NotFound, Exception = e2 };
            }
        }

        private void Preheat()
        {
            m_Client.SendAsync(new HttpRequestMessage
            {
                Method = new HttpMethod("HEAD"),
                RequestUri = new Uri($"{m_BaseAddress}/")
            });
        }

        public void Dispose()
        {
            m_Client?.Dispose();
        }
    }
}