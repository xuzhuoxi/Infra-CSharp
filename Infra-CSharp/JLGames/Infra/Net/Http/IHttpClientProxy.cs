using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JLGames.Infra.Net
{
    public interface IHttpClientProxy : IDisposable
    {
        /// <summary>
        /// 设置超时时间<br/>
        /// 不影响创建时设置的超时设置<br/>
        /// </summary>
        /// <param name="timeout"></param>
        void SetTimeout(TimeSpan timeout);

        /// <summary>
        /// 重置超时时间<br/>
        /// 使用创建时设置的超时设置
        /// </summary>
        void ResetTimeout();

        /// <summary>
        /// 设置是不否使用长连接
        /// </summary>
        /// <param name="enable"></param>
        void SetKeepAlive(bool enable);

        #region Get functions

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        Task<HttpResult<byte[]>> GetBytesAsync(string pattern);

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="timeout">超时时间</param>
        Task<HttpResult<byte[]>> GetBytesAsync(string pattern, TimeSpan timeout);

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        Task<HttpResult<byte[]>> GetBytesAsync(Uri baseUri, string pattern);

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="timeout">超时时间</param>
        Task<HttpResult<byte[]>> GetBytesAsync(Uri baseUri, string pattern, TimeSpan timeout);

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        Task<HttpResult<string>> GetStringAsync(string pattern);

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="timeout">超时时间</param>
        Task<HttpResult<string>> GetStringAsync(string pattern, TimeSpan timeout);

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        Task<HttpResult<string>> GetStringAsync(Uri baseUri, string pattern);

        /// <summary>
        /// 异步Get请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="timeout">超时时间</param>
        Task<HttpResult<string>> GetStringAsync(Uri baseUri, string pattern, TimeSpan timeout);

        #endregion


        #region Post functions.

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        Task<HttpResult<byte[]>> PostBytesAsync(string pattern, Dictionary<string, string> value);

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        /// <param name="timeout">超时时间</param>
        Task<HttpResult<byte[]>> PostBytesAsync(string pattern, Dictionary<string, string> value, TimeSpan timeout);

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        Task<HttpResult<byte[]>> PostBytesAsync(Uri baseUri, string pattern, Dictionary<string, string> value);

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
        Task<HttpResult<byte[]>> PostBytesAsync(Uri baseUri, string pattern, Dictionary<string, string> value, TimeSpan timeout);

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认的超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        Task<HttpResult<string>> PostStringAsync(string pattern, Dictionary<string, string> value);

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用指定超时设置<br/>
        /// </summary>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="value">参数集</param>
        /// <param name="timeout">超时时间</param>
        Task<HttpResult<string>> PostStringAsync(string pattern, Dictionary<string, string> value, TimeSpan timeout);

        /// <summary>
        /// 异步Post请求，建议调用时使用await关键字<br/>
        /// 使用指定的BaseUri, 忽略默认的BaseUri<br/>
        /// Pattern会与BaseUri拼接成完整的Url, 拼接逻辑请查看Uri类<br/>
        /// 使用默认超时设置<br/>
        /// </summary>
        /// <param name="baseUri">使用指定的BaseUri，忽略默认</param>
        /// <param name="pattern">URL 模式匹配</param>
        /// <param name="values">参数集</param>
        Task<HttpResult<string>> PostStringAsync(Uri baseUri, string pattern, Dictionary<string, string> values);

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
        Task<HttpResult<string>> PostStringAsync(Uri baseUri, string pattern, Dictionary<string, string> values, TimeSpan timeout);

        #endregion
    }
}