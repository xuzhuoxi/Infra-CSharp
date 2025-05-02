using System;
using System.Net.Sockets;
using JLGames.Infra.Buffer;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    public class SockReceiver : EventDispatcher, ISockReceiver
    {
        private const int BuffSize = 4096;
        private readonly Socket m_Socket;
        private readonly IByteBuffer m_ByteBuff;
        private readonly IMessageReader m_Reader;

//        private Thread m_ThRecv;
        private SockDelegate.FuncMessageHandler m_Handler;
        private readonly byte[] m_Buffer = new byte[BuffSize];

        private bool m_Receiving = false;
        public bool IsReceiving => m_Receiving;

        public SockReceiver(Socket socket, IByteBuffer byteBuff, IMessageReader reader)
        {
            m_Socket = socket;
            m_ByteBuff = byteBuff;
            m_Reader = reader;
        }

        public void SetMessageHandler(SockDelegate.FuncMessageHandler handler)
        {
            m_Handler = handler;
        }

        public void StartReceiving()
        {
            if (m_Receiving) return;
            m_Receiving = true;
            BeginReceive();
//            m_ThRecv = new Thread(DoReceiveMessage);
//            m_ThRecv.Start();
        }

        public void StopReceiving()
        {
            if (!m_Receiving) return;
            m_Receiving = false;

//            m_ThRecv.Abort();
//            m_ThRecv = null;
        }

        private void BeginReceive()
        {
            if (!m_Receiving)
            {
                DispatchEvent(SockEvents.EventOnReceivingStopped, null);
                return;
            }

//            DebugUtil.Log("BeginReceive");
            m_Socket.BeginReceive(m_Buffer, 0, BuffSize, SocketFlags.None, OnReceiving, m_Socket);
        }

        private void OnReceiving(IAsyncResult ar)
        {
//            DebugUtil.Log("OnReceiving");
            var socket = (Socket) ar.AsyncState;
            try
            {
                // 结束异步接收，并获取读取到的字节数
                var bytesRead = socket.EndReceive(ar, out var err);
                if (bytesRead <= 0)
                {
                    DispatchEvent(SockEvents.EventOnReceivingStopped, bytesRead);
                    return;
                }

                m_ByteBuff.Write(m_Buffer, 0, bytesRead);
                InnerCheckAndHandle();

//                var canContinue = err == SocketError.Success || err == SocketError.WouldBlock || err == SocketError.InProgress
//                                  || err == SocketError.Interrupted || err == SocketError.TimedOut || err == SocketError.MessageSize;
                var canContinue = err == SocketError.Success;
                if (!canContinue)
                {
                    DispatchEvent(SockEvents.EventOnReceivingStopped, err);
                    return;
                }

                BeginReceive();
            }
            catch (Exception ex)
            {
                DispatchEvent(SockEvents.EventOnReceivingStopped, ex);
            }
        }

        private void DoReceiveMessage()
        {
            while (IsReceiving && m_Socket.Connected)
            {
                var ln = m_Socket.Receive(m_Buffer);
                if (ln < 0)
                    continue;

                m_ByteBuff.Write(m_Buffer, 0, ln);
                InnerCheckAndHandle();
            }
        }

        private void InnerCheckAndHandle()
        {
//            DebugUtil.Log("InnerCheckAndHandle.Start");
            if (null == m_Handler)
                return;

            while (m_Reader.CheckMessage())
            {
//                DebugUtil.Log("InnerCheckAndHandle.NewMessage");
                var remoteAddress = m_Socket.RemoteEndPoint.Serialize().ToString();
                m_Handler.Invoke(m_Reader.ReadMessage(), remoteAddress, null);
            }
        }
    }
}