using System;
using System.Net.Sockets;
using System.Threading;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    public class SocketReceiver : EventDispatcher, ISocketReceiver
    {
        private const int BuffSize = 8192;
        private readonly Socket m_Socket;
        private readonly INetMessageReader m_Reader;

        private readonly IReceiveAdapter m_ReceiveAdapter;
        private SocketDelegates.OnBinaryMessageHandler m_Handler;

        public bool IsReceiving => m_Socket.Connected && m_ReceiveAdapter.IsReceiving;

        public string Name { get; private set; }
        public bool Connected => m_Socket.Connected;

        public SocketReceiver(string name, Socket socket, INetMessageReader reader, bool oldApi)
        {
            Name = name;
            m_Socket = socket;
            m_Reader = reader;
            if (oldApi)
                m_ReceiveAdapter = new BeginReceiveAdapter(socket, m_Reader, BuffSize);
            else
                m_ReceiveAdapter = new ReceiveAsyncAdapter(socket, m_Reader, BuffSize);
        }

        public void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler)
        {
            m_Handler = handler;
        }

        public void StartReceiving()
        {
            m_ReceiveAdapter.Start(OnReceive);
        }

        public void StopReceiving()
        {
            m_ReceiveAdapter.Stop();
        }

        // ---------- ---------- ---------- ---------- ----------

        private void OnReceive(AdapterDelegates.ReceiveResultInfo info)
        {
            // if (context == m_SyncContext)
            HandleReceiveInfoCurrent(info);
            // else
            //     m_SyncContext.Post(_ => { HandleReceiveInfoCurrent(info); }, null); // 回到当前线程执行
        }

        private void HandleReceiveInfoCurrent(AdapterDelegates.ReceiveResultInfo info)
        {
            // 如果读取到的字节数小于等于0，则表示连接已断开
            if (info.BytesRead <= 0)
            {
                m_Socket.Close();
                DispatchEvent(SocketEvents.EventOnMessageReceivedEnd, new SocketEvents.SocketReceivedEndInfo(true));
                return;
            }

            // 检查并处理消息
            while (m_Reader.CheckMessage())
            {
                var remoteAddress = m_Socket.RemoteEndPoint.Serialize().ToString(); // 可能每次不同，如UDP下
                var msg = m_Reader.ReadMessage();
                m_Handler?.Invoke(msg, remoteAddress, null);
                DispatchEvent(SocketEvents.EventOnMessageReceived, msg);
            }

            // 处理错误 与 异常
            if (CatchSocketError(info.Error) || CatchException(info.Exception))
            {
                DispatchEvent(SocketEvents.EventOnMessageReceivedEnd,
                    new SocketEvents.SocketReceivedEndInfo(false, info.Error, info.Exception));
                return;
            }

            m_ReceiveAdapter.Next();
        }

        private bool CatchSocketError(SocketError err)
        {
            switch (err)
            {
                case SocketError.Success:
                    return false;
                case SocketError.WouldBlock:
                case SocketError.InProgress:
                case SocketError.Interrupted:
                case SocketError.TimedOut:
                case SocketError.MessageSize:
                    return true;
                default:
                    return true;
            }
        }

        private bool CatchException(Exception ex)
        {
            if (null == ex) return false;
            return true;
        }
    }
}