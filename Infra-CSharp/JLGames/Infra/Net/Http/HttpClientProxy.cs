using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JLGames.Infra.Net
{
    public sealed class HttpClientProxy
    {
        private readonly bool m_KeepAlive;
        private readonly HttpClient m_AliveClient;
        private readonly Uri m_BaseAddress;

        /// <summary>
        /// Create a http client proxy.
        /// 创建一个Http客户端代理
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="keepAlive"></param>
        public HttpClientProxy(string baseAddress, bool keepAlive = true)
        {
            m_BaseAddress = new Uri(baseAddress);
            m_KeepAlive = keepAlive;
            if (!keepAlive)
            {
                return;
            }

            m_AliveClient = new HttpClient {BaseAddress = m_BaseAddress};
            m_AliveClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            //预热
            m_AliveClient.SendAsync(new HttpRequestMessage
            {
                Method = new HttpMethod("HEAD"),
                RequestUri = new Uri(baseAddress + "/")
            });
        }

        /// <summary>
        /// Send a get request.
        /// 发送Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="call"></param>
        public void GetAsync(string url, Callback call)
        {
            var client = m_KeepAlive ? m_AliveClient : new HttpClient {BaseAddress = m_BaseAddress};
            InnerHttpGetAsync(client, url, call);
        }

        /// <summary>
        /// Send a post request.
        /// 发送Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonData"></param>
        /// <param name="call"></param>
        public void PostAsync(string url, string jsonData, Callback call)
        {
            var client = m_KeepAlive ? m_AliveClient : new HttpClient {BaseAddress = m_BaseAddress};
            InnerHttpPostAsync(client, url, jsonData, call);
        }

        private async void InnerHttpGetAsync(HttpClient client, string url, Callback call)
        {
            var msg = await client.GetAsync(url);
            if (!msg.IsSuccessStatusCode)
            {
                call?.Apply(msg.StatusCode);
                return;
            }

            var result = msg.Content.ReadAsStringAsync().Result;
            call?.Apply(msg.StatusCode, result);
        }

        private async void InnerHttpPostAsync(HttpClient client, string url, string jsonData, Callback call)
        {
            var content = new StringContent(jsonData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var msg = await client.PostAsync(url, content);
            if (!msg.IsSuccessStatusCode)
            {
                call.Apply(msg.StatusCode);
                return;
            }

            var result = msg.Content.ReadAsStringAsync().Result;
            call.Apply(msg.StatusCode, result);
        }

//        private async Task<string> getUrl(string url)
//        {
//            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
//            request.Method = WebRequestMethods.Http.Get;
//            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
//            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
//            return reader.ReadToEnd();
//            ;
//        }
    }
}