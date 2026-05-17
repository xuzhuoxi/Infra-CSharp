using System;
using System.Net.Sockets;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// <see cref="IReceiveAdapter"/> using <see cref="Socket.BeginReceive"/> (APM).
    /// 基于 <see cref="Socket.BeginReceive"/> 的接收适配器（APM）。
    /// </summary>
    internal class BeginReceiveAdapter : EventDispatcher, IReceiveAdapter
    {
        private readonly Socket m_Socket;
        private readonly byte[] m_Buffer;
        private readonly int m_BuffSize;
        private readonly INetMessageReader m_Reader;

        private AdapterDelegates.ReceiveResultInfo m_ResultInfo;
        private AdapterDelegates.OnReceive m_OnReceiveHandler;
        private bool m_Receiving = false;

        /// <inheritdoc/>
        public bool IsReceiving => m_Receiving;

        /// <summary>
        /// Create receive adapter for the given socket and message reader.
        /// 为指定 Socket 与消息读取器创建接收适配器。
        /// </summary>
        /// <param name="socket">Socket to receive from.<br/>用于接收的 Socket。</param>
        /// <param name="reader">Buffer for incoming message bytes.<br/>入站消息字节缓冲区。</param>
        /// <param name="buffSize">Receive buffer size in bytes.<br/>接收缓冲区大小（字节）。</param>
        public BeginReceiveAdapter(Socket socket, INetMessageReader reader, int buffSize)
        {
            m_Socket = socket;
            m_Reader = reader;
            m_BuffSize = buffSize;
            m_Buffer = new byte[buffSize];
            m_ResultInfo = new AdapterDelegates.ReceiveResultInfo();
        }

        /// <inheritdoc/>
        public void Start(AdapterDelegates.OnReceive onReceive)
        {
            if (m_Receiving) return;
            if (!m_Socket.Connected) return;
            m_Receiving = true;
            m_OnReceiveHandler = onReceive;
            BeginReceive();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (!m_Receiving) return;
            m_Receiving = false;
            m_OnReceiveHandler = null;
        }

        /// <inheritdoc/>
        public void Next()
        {
            BeginReceive();
        }

        private void BeginReceive()
        {
            if (!m_Receiving)
                return;

            DoReceive();
        }

        private void DoReceive()
        {
            m_Socket.BeginReceive(m_Buffer, 0, m_BuffSize, SocketFlags.None, OnReceiving, m_Socket);
        }

        private void OnReceiving(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            try
            {
                // 结束异步接收，并获取读取到的字节数
                var bytesRead = socket.EndReceive(ar, out var err);
                m_ResultInfo.BytesRead = bytesRead;
                m_ResultInfo.Error = err;

                // 如果读取到的字节数小于等于0，则表示连接已断开
                if (bytesRead > 0)
                {
                    // 处理数据
                    m_Reader.WriteMessageBytes(m_Buffer, 0, bytesRead);
                }

                m_OnReceiveHandler?.Invoke(m_ResultInfo);
            }
            catch (Exception ex)
            {
                m_ResultInfo.Exception = ex;
                m_OnReceiveHandler?.Invoke(m_ResultInfo);
            }
        }
    }
}
