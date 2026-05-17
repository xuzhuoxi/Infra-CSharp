using System.Net.Sockets;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// <see cref="IReceiveAdapter"/> using <see cref="Socket.ReceiveAsync"/> (TAP).
    /// 基于 <see cref="Socket.ReceiveAsync"/> 的接收适配器（TAP）。
    /// </summary>
    internal class ReceiveAsyncAdapter : EventDispatcher, IReceiveAdapter
    {
        private readonly Socket m_Socket;
        private readonly byte[] m_Buffer;
        private readonly INetMessageReader m_Reader;

        private SocketAsyncEventArgs m_Args;
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
        public ReceiveAsyncAdapter(Socket socket, INetMessageReader reader, int buffSize)
        {
            m_Socket = socket;
            m_Reader = reader;
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
            DoReceive();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (!m_Receiving) return;
            m_Receiving = false;
            m_OnReceiveHandler = null;
            if (null != m_Args)
            {
                m_Args.Completed -= OnReceive;
                m_Args = null;
            }
        }

        /// <inheritdoc/>
        public void Next()
        {
            if (null == m_Args) return;
            // 继续接收
            ContinueReceive(m_Args);
        }

        private void DoReceive()
        {
            m_Args = new SocketAsyncEventArgs();
            m_Args.SetBuffer(m_Buffer, 0, m_Buffer.Length);
            m_Args.Completed += OnReceive;
            m_Socket.ReceiveAsync(m_Args);
        }

        private void OnReceive(object sender, SocketAsyncEventArgs args)
        {
            m_ResultInfo.BytesRead = args.BytesTransferred;
            m_ResultInfo.Error = args.SocketError;

            if (args.BytesTransferred > 0)
            {
                m_Reader.WriteMessageBytes(m_Buffer, 0, args.BytesTransferred);
            }

            m_OnReceiveHandler?.Invoke(m_ResultInfo);
        }

        private void ContinueReceive(SocketAsyncEventArgs args)
        {
            if (!m_Receiving) return;
            m_Socket.ReceiveAsync(args);
        }
    }
}
