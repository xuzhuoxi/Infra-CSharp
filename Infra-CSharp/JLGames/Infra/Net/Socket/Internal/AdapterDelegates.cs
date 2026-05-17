using System;
using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Delegate types and result structs for internal socket adapters.
    /// 内部 Socket 适配器使用的委托与结果结构。
    /// </summary>
    internal static class AdapterDelegates
    {
        /// <summary>
        /// Result of a single async receive operation.
        /// 单次异步接收的结果。
        /// </summary>
        public struct ReceiveResultInfo
        {
            /// <summary>
            /// Number of bytes received.
            /// 接收到的字节数。
            /// </summary>
            public int BytesRead { get; set; }

            /// <summary>
            /// Socket error code.
            /// Socket 错误码。
            /// </summary>
            public SocketError Error { get; set; }

            /// <summary>
            /// Exception if any (may be null).
            /// 异常（可为 null）。
            /// </summary>
            public Exception Exception { get; set; }

            /// <summary>
            /// Reset fields to default success state.
            /// 重置为默认成功状态。
            /// </summary>
            public void Reset()
            {
                BytesRead = 0;
                Error = SocketError.Success;
                Exception = null;
            }
        }

        /// <summary>
        /// Callback after an async receive completes.
        /// 异步接收完成后的回调。
        /// </summary>
        /// <param name="info">Receive result.<br/>接收结果。</param>
        public delegate void OnReceive(ReceiveResultInfo info);

        /// <summary>
        /// Result of a connect or disconnect operation.
        /// 连接或断开操作的结果。
        /// </summary>
        public struct ConnectResultInfo
        {
            /// <summary>
            /// Whether the operation succeeded.
            /// 操作是否成功。
            /// </summary>
            public bool Suc { get; set; }

            /// <summary>
            /// Whether the operation was cancelled.
            /// 操作是否被取消。
            /// </summary>
            public bool Cancel { get; set; }

            /// <summary>
            /// Socket error code.
            /// Socket 错误码。
            /// </summary>
            public SocketError Error { get; set; }

            /// <summary>
            /// Exception if any (may be null).
            /// 异常（可为 null）。
            /// </summary>
            public Exception Exception { get; set; }

            /// <summary>
            /// Reset fields to default state.
            /// 重置为默认状态。
            /// </summary>
            public void Reset()
            {
                Suc = false;
                Cancel = false;
                Error = SocketError.Success;
                Exception = null;
            }
        }

        /// <summary>
        /// Callback when connect completes.
        /// 连接完成回调。
        /// </summary>
        /// <param name="info">Connect result.<br/>连接结果。</param>
        public delegate void OnConnect(ConnectResultInfo info);

        /// <summary>
        /// Callback when disconnect completes.
        /// 断开连接完成回调。
        /// </summary>
        /// <param name="info">Disconnect result.<br/>断开结果。</param>
        public delegate void OnDisconnect(ConnectResultInfo info);
    }
}
