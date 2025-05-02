using System;
using System.Net.Sockets;
using JLGames.Infra.Event;
using JLGames.Infra.Buffer;

namespace JLGames.Infra.Net
{
    public class SockClient : EventDispatcher, ISockClient
    {
        private readonly bool m_LittleEndian;
        private string m_Name;

        private ISockSender m_Sender;
        private ISockReceiver m_Receiver;
        private Socket m_Client;

        public string Name => m_Name;

        public SockClient(string name, bool littleEndian)
        {
            m_Name = name;
            m_LittleEndian = littleEndian;
        }

        public void SetName(string name)
        {
            m_Name = name;
        }

        public void SendBytes(byte[] bytes)
        {
            m_Sender?.SendBytes(bytes);
        }

        public void SendMessage(byte[] message)
        {
            m_Sender?.SendMessage(message);
        }

        public void SendMessage(string[] messages)
        {
            m_Sender?.SendMessage(messages);
        }

        public void SendMessage(string message, params string[] messages)
        {
            m_Sender?.SendMessage(message, messages);
        }

        public void SetMessageHandler(SockDelegate.FuncMessageHandler handler)
        {
            m_Receiver?.SetMessageHandler(handler);
        }

        public void StartReceiving()
        {
            if (null == m_Receiver) return;
            m_Receiver.AddEventListener(SockEvents.EventOnReceivingStopped, OnReceiving);
            m_Receiver.StartReceiving();
        }

        public void StopReceiving()
        {
            if (null == m_Receiver) return;
            m_Receiver.RemoveEventListener(SockEvents.EventOnReceivingStopped, OnReceiving);
            m_Receiver.StopReceiving();
        }

        public bool IsReceiving => m_Receiver.IsReceiving;

        public bool Connected => m_Client != null && m_Client.Connected;

        public void OpenClient(SockParams @params)
        {
            if (null != m_Client && m_Client.Connected)
            {
                DispatchEvent(SockEvents.EventOnConnectOpen, null);
                return;
            }

            m_Client = @params.GenSocket();
            OpenClientUseConnectAsync(@params);
//            OpenClientUseBeginConnect(@params);
        }

        public void CloseClient()
        {
            if (null == m_Client || !m_Client.Connected)
            {
                DispatchEvent(SockEvents.EventOnConnectClose, null);
                return;
            }

            m_Client.Shutdown(SocketShutdown.Both);
            CloseClientUseDisconnectAsync();
//            CloseClientUseBeginDisconnect();
        }

        #region ConnectAsync

        private void OpenClientUseConnectAsync(SockParams @params)
        {
            try
            {
                var args = new SocketAsyncEventArgs {RemoteEndPoint = @params.RemoteEndPoint()};
                args.Completed += OnConnectCompleted;
                var pending = m_Client.ConnectAsync(args);
                if (!pending)
                {
                    OnConnectCompleted(m_Client, args);
                }
            }
            catch (Exception e)
            {
                DispatchEvent(SockEvents.EventOnConnectOpen, e);
            }
        }

        private void OnConnectCompleted(object sender, SocketAsyncEventArgs e)
        {
            e.Completed -= OnConnectCompleted;
            // 检查是否发生错误
            if (e.SocketError != SocketError.Success)
            {
                DispatchEvent(SockEvents.EventOnConnectOpen, e.SocketError);
                return;
            }

            CreateScenderReceiver();
            DispatchEvent(SockEvents.EventOnConnectOpen, null);
        }

        #endregion

        #region DisconnectAsync

        private void CloseClientUseDisconnectAsync()
        {
            try
            {
                var args = new SocketAsyncEventArgs
                {
                    DisconnectReuseSocket = false
                };
                args.Completed += OnDisconnectAsyncCompleted;
                var pending = m_Client.DisconnectAsync(args);
                if (!pending)
                {
                    OnDisconnectAsyncCompleted(m_Client, args);
                }
            }
            catch (Exception e)
            {
                DispatchEvent(SockEvents.EventOnConnectClose, e);
            }
        }

        private void OnDisconnectAsyncCompleted(object sender, SocketAsyncEventArgs e)
        {
            e.Completed -= OnDisconnectAsyncCompleted;
            m_Client.Dispose();
            m_Receiver = null;
            m_Sender = null;
            m_Client = null;
            // 检查是否发生错误
            if (e.SocketError != SocketError.Success && e.SocketError != SocketError.OperationAborted)
            {
                DispatchEvent(SockEvents.EventOnConnectClose, e.SocketError);
                return;
            }

            DispatchEvent(SockEvents.EventOnConnectClose, null);
        }

        #endregion

        #region BeginConnect

        private void OpenClientUseBeginConnect(SockParams @params)
        {
            try
            {
                m_Client.BeginConnect(@params.RemoteEndPoint(), InnerOnConnected, m_Client);
            }
            catch (Exception e)
            {
                DispatchEvent(SockEvents.EventOnConnectOpen, e);
            }
        }

        private void InnerOnConnected(IAsyncResult iar)
        {
            var client = (Socket) iar.AsyncState;
            try
            {
                client.EndConnect(iar);
            }
            catch (Exception e)
            {
                DispatchEvent(SockEvents.EventOnConnectOpen, e);
                return;
            }

            CreateScenderReceiver();
            DispatchEvent(SockEvents.EventOnConnectOpen, null);
        }

        #endregion

        #region BeginDisconnect

        public void CloseClientUseBeginDisconnect()
        {
            try
            {
                m_Client.BeginDisconnect(false, InnerOnDisconnected, m_Client);
            }
            catch (Exception e)
            {
                DispatchEvent(SockEvents.EventOnConnectClose, e);
            }
        }

        private void InnerOnDisconnected(IAsyncResult iar)
        {
            var client = (Socket) iar.AsyncState;
            try
            {
                client.EndDisconnect(iar);
                client.Dispose();
            }
            catch (Exception e)
            {
                m_Receiver = null;
                m_Sender = null;
                m_Client = null;
                DispatchEvent(SockEvents.EventOnConnectClose, e);
                return;
            }

            m_Receiver = null;
            m_Sender = null;
            m_Client = null;
            DispatchEvent(SockEvents.EventOnConnectClose, null);
        }

        #endregion

        private void CreateScenderReceiver()
        {
            m_Sender = new SockSender(m_Client, new MessageWriter(m_LittleEndian));
            var msgReader = new MessageReader(m_LittleEndian);
            m_Receiver = new SockReceiver(m_Client, msgReader, msgReader);
        }

        private void OnReceiving(EventData evd)
        {
            DispatchEvent(evd.Type, evd.Data);
        }
    }
}