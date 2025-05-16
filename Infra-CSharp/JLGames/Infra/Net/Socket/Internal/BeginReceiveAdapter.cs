using System;
using System.Net.Sockets;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    internal class BeginReceiveAdapter : EventDispatcher, IReceiveAdapter
    {
        private readonly Socket m_Socket;
        private readonly byte[] m_Buffer;
        private readonly int m_BuffSize;
        private readonly INetMessageReader m_Reader;

        private AdapterDelegates.ReceiveResultInfo m_ResultInfo;
        private AdapterDelegates.OnReceive m_OnReceiveHandler;
        private bool m_Receiving = false;

        public bool IsReceiving => m_Receiving;

        public BeginReceiveAdapter(Socket socket, INetMessageReader reader, int buffSize)
        {
            m_Socket = socket;
            m_Reader = reader;
            m_BuffSize = buffSize;
            m_Buffer = new byte[buffSize];
            m_ResultInfo = new AdapterDelegates.ReceiveResultInfo();
        }

        public void Start(AdapterDelegates.OnReceive onReceive)
        {
            if (m_Receiving) return;
            if (!m_Socket.Connected) return;
            m_Receiving = true;
            m_OnReceiveHandler = onReceive;
            BeginReceive();
        }

        public void Stop()
        {
            if (!m_Receiving) return;
            m_Receiving = false;
            m_OnReceiveHandler = null;
        }

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