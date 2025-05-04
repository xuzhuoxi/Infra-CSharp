using System.Net;

namespace JLGames.Infra.Net
{
    public static class HttpDelegate
    {
        /// <summary>
        /// Http响应回调，内容以byte[]返回
        /// </summary>
        public delegate void OnByteArrayResponse(bool timeout, HttpStatusCode statusCode, byte[] content);

        /// <summary>
        /// Http响应回调，内容以string返回
        /// </summary>
        public delegate void OnStringResponse(bool timeout, HttpStatusCode statusCode, string content);
    }
}