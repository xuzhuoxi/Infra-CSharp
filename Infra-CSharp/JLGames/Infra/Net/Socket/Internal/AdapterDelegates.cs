using System;
using System.Net.Sockets;
using System.Threading;

namespace JLGames.Infra.Net
{
    internal static class AdapterDelegates
    {
        public struct ReceiveResultInfo
        {
            /// <summary>
            /// 接收的字节数
            /// </summary>
            public int BytesRead { get; set; }

            /// <summary>
            /// 错误
            /// </summary>
            public SocketError Error { get; set; }

            /// <summary>
            /// 异常
            /// </summary>
            public Exception Exception { get; set; }

            public void Reset()
            {
                BytesRead = 0;
                Error = SocketError.Success;
                Exception = null;
            }
        }

        /// <summary>
        /// 接收到数据后回调
        /// </summary>
        public delegate void OnReceive(ReceiveResultInfo info);

        public struct ConnectResultInfo
        {
            /// <summary>
            /// 连接是否成功
            /// </summary>
            public bool Suc { get; set; }

            /// <summary>
            /// 连接被取消
            /// </summary>
            public bool Cancel { get; set; }

            /// <summary>
            /// 错误
            /// </summary>
            public SocketError Error { get; set; }

            /// <summary>
            /// 异常
            /// </summary>
            public Exception Exception { get; set; }

            public void Reset()
            {
                Suc = false;
                Cancel = false;
                Error = SocketError.Success;
                Exception = null;
            }
        }

        /// <summary>
        /// 连接回调
        /// </summary>
        public delegate void OnConnect(ConnectResultInfo info);

        /// <summary>
        /// 关闭连接回调
        /// </summary>
        public delegate void OnDisconnect(ConnectResultInfo info);
    }
}