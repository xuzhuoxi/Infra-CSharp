using System;
using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    public static class SocketEvents
    {
        /// <summary>
        /// 事件数据结构: 用户连接映射信息
        /// </summary>
        public readonly struct SocketMessageEventInfo
        {
            /// <summary>
            /// 用户 ID
            /// </summary>
            public string UserId { get; }

            /// <summary>
            /// 连接 ID
            /// </summary>
            public string ConnId { get; }

            /// <summary>
            /// 二进制数据
            /// </summary>
            public byte[] BinaryMessage { get; }

            /// <summary>
            /// 字符串数据
            /// </summary>
            public string StringMessage { get; }

            /// <summary>
            /// 错误信息
            /// </summary>
            public SocketError Error { get; }

            /// <summary>
            /// 错误信息
            /// </summary>
            public Exception Exception { get; }

            public SocketMessageEventInfo(string connId, string userId, byte[] binaryMessage, string stringMessage, SocketError error, Exception e)
            {
                ConnId = connId;
                UserId = userId;
                BinaryMessage = binaryMessage;
                StringMessage = stringMessage;
                Error = error;
                Exception = e;
            }
        }

        public struct SocketConnEventInfo
        {
            /// <summary>
            /// 是否成功
            /// </summary>
            public bool Suc { get; private set; }

            /// <summary>
            /// 错误信息
            /// </summary>
            public SocketError Error { get; private set; }

            /// <summary>
            /// 错误信息
            /// </summary>
            public Exception Exception { get; private set; }

            public SocketConnEventInfo(bool suc)
            {
                Suc = suc;
                Error = suc ? SocketError.Success : SocketError.TypeNotFound;
                Exception = null;
            }

            public SocketConnEventInfo(bool suc, SocketError error)
            {
                Suc = suc;
                Error = error;
                Exception = null;
            }

            public SocketConnEventInfo(bool suc, Exception exception)
            {
                Suc = suc;
                Error = suc ? SocketError.Success : SocketError.TypeNotFound;
                Exception = exception;
            }

            public SocketConnEventInfo(bool suc, SocketError error, Exception exception)
            {
                Suc = suc;
                Error = error;
                Exception = exception;
            }

            public override string ToString()
            {
                return $"{{Suc={Suc}, Error={Error}, Exception={Exception}}}";
            }
        }

        public struct SocketReceivedEndInfo
        {
            /// <summary>
            /// 连接已断开
            /// </summary>
            public bool Disconnect { get; private set; }

            /// <summary>
            /// 接收时得到的错误信息
            /// </summary>
            public SocketError Error { get; private set; }

            /// <summary>
            /// 接收数据异常
            /// </summary>
            public Exception Exception { get; private set; }

            public SocketReceivedEndInfo(bool disconnect)
            {
                Disconnect = disconnect;
                Error = SocketError.TypeNotFound;
                Exception = null;
            }

            public SocketReceivedEndInfo(bool disconnect, SocketError error)
            {
                Disconnect = disconnect;
                Error = error;
                Exception = null;
            }

            public SocketReceivedEndInfo(bool disconnect, Exception exception)
            {
                Disconnect = disconnect;
                Error = SocketError.TypeNotFound;
                Exception = exception;
            }

            public SocketReceivedEndInfo(bool disconnect, SocketError error, Exception exception)
            {
                Disconnect = disconnect;
                Error = error;
                Exception = exception;
            }
        }

        /// <summary>
        /// Enable connection result event
        /// 开启连接结果事件
        /// Event data(事件数据)：SocketConnEventInfo
        /// </summary>
        public const string EventOnConnectionOpen = "SockEvents.EventOnConnectOpen";

        /// <summary>
        /// 连接超时事件
        /// Event data(事件数据)：SocketConnEventInfo
        /// </summary>
        public const string EventOnConnectionTimeout = "SockEvents.EventOnConnectionTimeout";

        /// <summary>
        /// Enable connection result event
        /// 连接取消
        /// Event data(事件数据)：SocketConnEventInfo
        /// </summary>
        public const string EventOnConnectionCancel = "SockEvents.EventOnConnectionCancel";

        /// <summary>
        /// Close connection result event
        /// 关闭连接结果事件
        /// Event data(事件数据)：SocketConnEventInfo
        /// </summary>
        public const string EventOnConnectionClose = "SockEvents.EventOnConnectClose";

        /// <summary>
        /// Data reception processing completed
        /// 数据接收处理结束
        /// Event data(事件数据)：byte[] message
        /// </summary>
        public const string EventOnMessageReceived = "SockEvents.EventOnMessageReceived";

        /// <summary>
        /// 接收结束
        /// 通常是接收到到的数据为空时作为依据
        /// Event data(事件数据)：SocketReceivedEndInfo
        /// </summary>
        public const string EventOnMessageReceivedEnd = "SockEvents.EventOnMessageReceivedEnd";
    }
}