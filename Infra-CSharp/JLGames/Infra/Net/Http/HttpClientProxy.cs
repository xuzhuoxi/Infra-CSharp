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
    public sealed class HttpClientProxy : IDisposable
    {
        private const string KeepAlive = "keep-alive";

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
            m_Client.DefaultRequestHeaders.Connection.Add(KeepAlive);
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
            m_Client.DefaultRequestHeaders.Connection.Add(KeepAlive);
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
            var contains = m_Client.DefaultRequestHeaders.Connection.Contains(KeepAlive);
            if (contains == enable) return;
            if (enable)
                m_Client.DefaultRequestHeaders.Connection.Add(KeepAlive);
            else
                m_Client.DefaultRequestHeaders.Connection.Remove(KeepAlive);
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        public async Task Get(string pattern, HttpDelegate.OnByteArrayResponse onResponse)
        {
            await Get(m_BaseUri, pattern, onResponse);
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以UTF-8字符串为内容的响应函数</param>
        public async Task Get(string pattern, HttpDelegate.OnStringResponse onResponse)
        {
            await Get(m_BaseUri, pattern, onResponse);
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Get(string pattern, HttpDelegate.OnByteArrayResponse onResponse, TimeSpan timeout)
        {
            await Get(m_BaseUri, pattern, onResponse, timeout);
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以字符串为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Get(string pattern, HttpDelegate.OnStringResponse onResponse, TimeSpan timeout)
        {
            await Get(m_BaseUri, pattern, onResponse, timeout);
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        public async Task Get(Uri baseUri, string pattern, HttpDelegate.OnByteArrayResponse onResponse)
        {
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.GetAsync(fullUrl);
                var result = await msg.Content.ReadAsByteArrayAsync();
                onResponse?.Invoke(false, msg.StatusCode, result);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine($"Get UriFormatException: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                onResponse?.Invoke(true, HttpStatusCode.RequestTimeout, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Exception({e.GetType()}): {e.Message}");
            }
        }

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以字符串为内容的响应函数</param>
        public async Task Get(Uri baseUri, string pattern, HttpDelegate.OnStringResponse onResponse)
        {
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.GetAsync(fullUrl);
                var result = await msg.Content.ReadAsStringAsync();
                onResponse?.Invoke(false, msg.StatusCode, result);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine($"Get UriFormatException: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                onResponse?.Invoke(true, HttpStatusCode.RequestTimeout, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Exception({e.GetType()}): {e.Message}");
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
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Get(Uri baseUri, string pattern, HttpDelegate.OnByteArrayResponse onResponse, TimeSpan timeout)
        {
            var cts = new CancellationTokenSource(timeout);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.GetAsync(fullUrl, cts.Token);
                var result = await msg.Content.ReadAsByteArrayAsync();
                onResponse?.Invoke(false, msg.StatusCode, result);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine($"Get UriFormatException: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                onResponse?.Invoke(true, HttpStatusCode.RequestTimeout, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Exception({e.GetType()}): {e.Message}");
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
        /// <param name="onResponse">以字符串为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Get(Uri baseUri, string pattern, HttpDelegate.OnStringResponse onResponse, TimeSpan timeout)
        {
            var cts = new CancellationTokenSource(timeout);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.GetAsync(fullUrl, cts.Token);
                var result = await msg.Content.ReadAsStringAsync();
                onResponse?.Invoke(false, msg.StatusCode, result);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine($"Get UriFormatException: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                onResponse?.Invoke(true, HttpStatusCode.RequestTimeout, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Get Exception({e.GetType()}): {e.Message}");
            }
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 不带参数<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        public async Task Post(string pattern, HttpDelegate.OnByteArrayResponse onResponse)
        {
            await Post(m_BaseUri, pattern, null, onResponse);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// 不带参数<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以UTF-8字符串为内容的响应函数</param>
        public async Task Post(string pattern, HttpDelegate.OnStringResponse onResponse)
        {
            await Post(m_BaseUri, pattern, null, onResponse);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        public async Task Post(string pattern, Dictionary<string, string> value, HttpDelegate.OnByteArrayResponse onResponse)
        {
            await Post(m_BaseUri, pattern, value, onResponse);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        /// <param name="onResponse">以UTF-8字符串为内容的响应函数</param>
        public async Task Post(string pattern, Dictionary<string, string> value, HttpDelegate.OnStringResponse onResponse)
        {
            await Post(m_BaseUri, pattern, value, onResponse);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 不带参数<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Post(string pattern, HttpDelegate.OnByteArrayResponse onResponse, TimeSpan timeout)
        {
            await Post(m_BaseUri, pattern, null, onResponse, timeout);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 不带参数<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="onResponse">以字符串为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Post(string pattern, HttpDelegate.OnStringResponse onResponse, TimeSpan timeout)
        {
            await Post(m_BaseUri, pattern, null, onResponse, timeout);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Post(string pattern, Dictionary<string, string> value, HttpDelegate.OnByteArrayResponse onResponse, TimeSpan timeout)
        {
            await Post(m_BaseUri, pattern, value, onResponse, timeout);
        }

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        /// <param name="onResponse">以字符串为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Post(string pattern, Dictionary<string, string> value, HttpDelegate.OnStringResponse onResponse, TimeSpan timeout)
        {
            await Post(m_BaseUri, pattern, value, onResponse, timeout);
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
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        public async Task Post(Uri baseUri, string pattern, Dictionary<string, string> value, HttpDelegate.OnByteArrayResponse onResponse)
        {
            var content = null == value ? null : new FormUrlEncodedContent(value);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.PostAsync(fullUrl, content);
                var result = await msg.Content.ReadAsByteArrayAsync();
                onResponse?.Invoke(false, msg.StatusCode, result);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine($"Post UriFormatException: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                onResponse?.Invoke(true, HttpStatusCode.RequestTimeout, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Post Exception({e.GetType()}): {e.Message}");
            }
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
        /// <param name="onResponse">以字符串为内容的响应函数</param>
        public async Task Post(Uri baseUri, string pattern, Dictionary<string, string> value, HttpDelegate.OnStringResponse onResponse)
        {
            var content = null == value ? null : new FormUrlEncodedContent(value);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.PostAsync(fullUrl, content);
                var result = await msg.Content.ReadAsStringAsync();
                onResponse?.Invoke(false, msg.StatusCode, result);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine($"Post UriFormatException: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                onResponse?.Invoke(true, HttpStatusCode.RequestTimeout, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Post Exception({e.GetType()}): {e.Message}");
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
        /// <param name="onResponse">以字节数组为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Post(Uri baseUri, string pattern, Dictionary<string, string> value, HttpDelegate.OnByteArrayResponse onResponse,
            TimeSpan timeout)
        {
            var content = null == value ? null : new FormUrlEncodedContent(value);
            var cts = new CancellationTokenSource(timeout);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.PostAsync(fullUrl, content, cts.Token);
                var result = await msg.Content.ReadAsByteArrayAsync();
                onResponse?.Invoke(false, msg.StatusCode, result);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine($"Post UriFormatException: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                onResponse?.Invoke(true, HttpStatusCode.RequestTimeout, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Post Exception({e.GetType()}): {e.Message}");
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
        /// <param name="onResponse">以字符串为内容的响应函数</param>
        /// <param name="timeout">超时时间</param>
        public async Task Post(Uri baseUri, string pattern, Dictionary<string, string> value, HttpDelegate.OnStringResponse onResponse,
            TimeSpan timeout)
        {
            var content = null == value ? null : new FormUrlEncodedContent(value);
            var cts = new CancellationTokenSource(timeout);
            try
            {
                var fullUrl = null == baseUri ? new Uri(pattern) : new Uri(baseUri, pattern);
                var msg = await m_Client.PostAsync(fullUrl, content, cts.Token);
                var result = await msg.Content.ReadAsStringAsync();
                onResponse?.Invoke(false, msg.StatusCode, result);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine($"Post UriFormatException: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                onResponse?.Invoke(true, HttpStatusCode.RequestTimeout, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Post Exception({e.GetType()}): {e.Message}}}");
            }
        }

        // /// <summary>
        // /// Send a get request.
        // /// 发送Get请求
        // /// </summary>
        // /// <param name="url"></param>
        // /// <param name="call"></param>
        // public void GetAsync(string url, Callback call)
        // {
        //     InnerHttpGetAsync(m_Client, url, call);
        // }
        //
        // /// <summary>
        // /// Send a post request.
        // /// 发送Post请求
        // /// </summary>
        // /// <param name="url"></param>
        // /// <param name="jsonData"></param>
        // /// <param name="call"></param>
        // public void PostAsync(string url, string jsonData, Callback call)
        // {
        //     InnerHttpPostAsync(m_Client, url, jsonData, call);
        // }
        //
        // private async void InnerHttpGetAsync(HttpClient client, string url, Callback call)
        // {
        //     var msg = await client.GetAsync(url);
        //     if (!msg.IsSuccessStatusCode)
        //     {
        //         call?.Apply(msg.StatusCode);
        //         return;
        //     }
        //
        //     var result = msg.Content.ReadAsStringAsync().Result;
        //     call?.Apply(msg.StatusCode, result);
        // }
        //
        // private async void InnerHttpPostAsync(HttpClient client, string url, string jsonData, Callback call)
        // {
        //     var content = new StringContent(jsonData);
        //     content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //     var msg = await client.PostAsync(url, content);
        //     if (!msg.IsSuccessStatusCode)
        //     {
        //         call.Apply(msg.StatusCode);
        //         return;
        //     }
        //
        //     var result = msg.Content.ReadAsStringAsync().Result;
        //     call.Apply(msg.StatusCode, result);
        // }

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