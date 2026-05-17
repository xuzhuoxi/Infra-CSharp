using System;
using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Socket event type constants and event payload types.
    /// Socket 事件类型常量与事件数据结构。
    /// </summary>
    public static class SocketEvents
    {
        /// <summary>
        /// Event payload for received messages (user/connection mapping).
        /// 消息接收事件数据（用户与连接映射信息）。
        /// </summary>
        public readonly struct SocketMessageEventInfo
        {
            /// <summary>
            /// User ID.
            /// 用户 ID。
            /// </summary>
            public string UserId { get; }

            /// <summary>
            /// Connection ID.
            /// 连接 ID。
            /// </summary>
            public string ConnId { get; }

            /// <summary>
            /// Binary payload.
            /// 二进制数据。
            /// </summary>
            public byte[] BinaryMessage { get; }

            /// <summary>
            /// String payload.
            /// 字符串数据。
            /// </summary>
            public string StringMessage { get; }

            /// <summary>
            /// Socket error code.
            /// Socket 错误码。
            /// </summary>
            public SocketError Error { get; }

            /// <summary>
            /// Exception if any (may be null).
            /// 异常信息（可为 null）。
            /// </summary>
            public Exception Exception { get; }

            /// <summary>
            /// Create message event info.
            /// 构造消息事件数据。
            /// </summary>
            /// <param name="connId">Connection ID.<br/>连接 ID。</param>
            /// <param name="userId">User ID.<br/>用户 ID。</param>
            /// <param name="binaryMessage">Binary payload.<br/>二进制数据。</param>
            /// <param name="stringMessage">String payload.<br/>字符串数据。</param>
            /// <param name="error">Socket error code.<br/>Socket 错误码。</param>
            /// <param name="e">Exception (may be null).<br/>异常（可为 null）。</param>
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

        /// <summary>
        /// Event payload for connection open/close results.
        /// 连接建立/关闭结果事件数据。
        /// </summary>
        public struct SocketConnEventInfo
        {
            /// <summary>
            /// Whether the operation succeeded.
            /// 是否成功。
            /// </summary>
            public bool Suc { get; private set; }

            /// <summary>
            /// Socket error code.
            /// Socket 错误码。
            /// </summary>
            public SocketError Error { get; private set; }

            /// <summary>
            /// Exception if any (may be null).
            /// 异常信息（可为 null）。
            /// </summary>
            public Exception Exception { get; private set; }

            /// <summary>
            /// Create connection event info with success flag only.
            /// 仅根据成功标志构造连接事件数据。
            /// </summary>
            /// <param name="suc">Success flag.<br/>是否成功。</param>
            public SocketConnEventInfo(bool suc)
            {
                Suc = suc;
                Error = suc ? SocketError.Success : SocketError.TypeNotFound;
                Exception = null;
            }

            /// <summary>
            /// Create connection event info with success flag and socket error.
            /// 根据成功标志与 Socket 错误码构造连接事件数据。
            /// </summary>
            /// <param name="suc">Success flag.<br/>是否成功。</param>
            /// <param name="error">Socket error code.<br/>Socket 错误码。</param>
            public SocketConnEventInfo(bool suc, SocketError error)
            {
                Suc = suc;
                Error = error;
                Exception = null;
            }

            /// <summary>
            /// Create connection event info with success flag and exception.
            /// 根据成功标志与异常构造连接事件数据。
            /// </summary>
            /// <param name="suc">Success flag.<br/>是否成功。</param>
            /// <param name="exception">Exception (may be null).<br/>异常（可为 null）。</param>
            public SocketConnEventInfo(bool suc, Exception exception)
            {
                Suc = suc;
                Error = suc ? SocketError.Success : SocketError.TypeNotFound;
                Exception = exception;
            }

            /// <summary>
            /// Create connection event info with success flag, socket error, and exception.
            /// 根据成功标志、Socket 错误码与异常构造连接事件数据。
            /// </summary>
            /// <param name="suc">Success flag.<br/>是否成功。</param>
            /// <param name="error">Socket error code.<br/>Socket 错误码。</param>
            /// <param name="exception">Exception (may be null).<br/>异常（可为 null）。</param>
            public SocketConnEventInfo(bool suc, SocketError error, Exception exception)
            {
                Suc = suc;
                Error = error;
                Exception = exception;
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return $"{{Suc={Suc}, Error={Error}, Exception={Exception}}}";
            }
        }

        /// <summary>
        /// Event payload when message receiving ends.
        /// 消息接收结束事件数据。
        /// </summary>
        public struct SocketReceivedEndInfo
        {
            /// <summary>
            /// Whether the connection was disconnected.
            /// 连接是否已断开。
            /// </summary>
            public bool Disconnect { get; private set; }

            /// <summary>
            /// Socket error during receive.
            /// 接收时得到的 Socket 错误码。
            /// </summary>
            public SocketError Error { get; private set; }

            /// <summary>
            /// Exception during receive (may be null).
            /// 接收数据时的异常（可为 null）。
            /// </summary>
            public Exception Exception { get; private set; }

            /// <summary>
            /// Create receive-end event info with disconnect flag only.
            /// 仅根据断开标志构造接收结束事件数据。
            /// </summary>
            /// <param name="disconnect">Whether disconnected.<br/>是否已断开连接。</param>
            public SocketReceivedEndInfo(bool disconnect)
            {
                Disconnect = disconnect;
                Error = SocketError.TypeNotFound;
                Exception = null;
            }

            /// <summary>
            /// Create receive-end event info with disconnect flag and socket error.
            /// 根据断开标志与 Socket 错误码构造接收结束事件数据。
            /// </summary>
            /// <param name="disconnect">Whether disconnected.<br/>是否已断开连接。</param>
            /// <param name="error">Socket error code.<br/>Socket 错误码。</param>
            public SocketReceivedEndInfo(bool disconnect, SocketError error)
            {
                Disconnect = disconnect;
                Error = error;
                Exception = null;
            }

            /// <summary>
            /// Create receive-end event info with disconnect flag and exception.
            /// 根据断开标志与异常构造接收结束事件数据。
            /// </summary>
            /// <param name="disconnect">Whether disconnected.<br/>是否已断开连接。</param>
            /// <param name="exception">Exception (may be null).<br/>异常（可为 null）。</param>
            public SocketReceivedEndInfo(bool disconnect, Exception exception)
            {
                Disconnect = disconnect;
                Error = SocketError.TypeNotFound;
                Exception = exception;
            }

            /// <summary>
            /// Create receive-end event info with disconnect flag, socket error, and exception.
            /// 根据断开标志、Socket 错误码与异常构造接收结束事件数据。
            /// </summary>
            /// <param name="disconnect">Whether disconnected.<br/>是否已断开连接。</param>
            /// <param name="error">Socket error code.<br/>Socket 错误码。</param>
            /// <param name="exception">Exception (may be null).<br/>异常（可为 null）。</param>
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