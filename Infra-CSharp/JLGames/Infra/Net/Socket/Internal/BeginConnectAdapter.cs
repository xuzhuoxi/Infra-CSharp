using System;
using System.Net.Sockets;
using System.Threading;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    internal class BeginConnectAdapter : IConnectAdapter
    {
        private SocketParams m_Params;
        private Socket m_Client;
        private bool m_Release;

        private AdapterDelegates.OnConnect m_OnConnectHandler;
        private AdapterDelegates.OnDisconnect m_OnDisconnectHandler;

        private bool m_Connecting;
        private bool m_Disconnecting;

        public Socket Socket => m_Client;
        public bool Connected => m_Client?.Connected ?? false;
        public bool ActionDoing => m_Connecting || m_Disconnecting;

        public BeginConnectAdapter()
        {
        }

        public BeginConnectAdapter(SocketParams @params)
        {
            m_Params = @params;
        }

        public void Connect(SocketParams @params, AdapterDelegates.OnConnect onConnect)
        {
            m_Params = @params;
            Connect(onConnect);
        }

        public void Connect(AdapterDelegates.OnConnect onConnect)
        {
            if (m_Connecting || m_Disconnecting || (null != m_Client && m_Client.Connected))
            {
                onConnect?.Invoke(new AdapterDelegates.ConnectResultInfo { Suc = false });
                return;
            }

            if (null == m_Client)
            {
                m_Client = SocketFactory.CreateSocket(m_Params);
            }

            m_OnConnectHandler = onConnect;
            StartConnect();
        }

        public void Close(AdapterDelegates.OnDisconnect onDisconnect, bool release)
        {
            if (null == m_Client || !m_Client.Connected || m_Disconnecting || m_Connecting)
            {
                onDisconnect?.Invoke(new AdapterDelegates.ConnectResultInfo { Suc = false });
                return;
            }

            if (m_Connecting)
            {
                m_Connecting = false;
                m_Client.Close();
                m_Client = null;
                onDisconnect?.Invoke(new AdapterDelegates.ConnectResultInfo { Suc = true });
                return;
            }

            m_OnDisconnectHandler = onDisconnect;
            m_Release = release;
            StartDisconnect(release);
        }

        private void StartConnect()
        {
            m_Connecting = true;
            try
            {
                m_Client.BeginConnect(m_Params.RemoteEndPoint(), OnConnect, m_Client);
            }
            catch (Exception e)
            {
                m_Connecting = false;
                m_OnConnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Exception = e });
            }
        }

        private void OnConnect(IAsyncResult iar)
        {
            var client = (Socket)iar.AsyncState;
            try
            {
                client.EndConnect(iar);
            }
            catch (ObjectDisposedException e)
            {
                m_OnConnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Cancel = true, Exception = e });
                return;
            }
            catch (Exception e)
            {
                m_OnConnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Exception = e });
                return;
            }

            m_OnConnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Suc = true });
        }


        private void StartDisconnect(bool release)
        {
            m_Release = release;
            m_Disconnecting = true;
            try
            {
                m_Client.BeginDisconnect(false, OnDisconnect, m_Client);
            }
            catch (Exception e)
            {
                m_Disconnecting = false;
                m_OnDisconnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Exception = e });
            }
        }

        private void OnDisconnect(IAsyncResult iar)
        {
            m_Disconnecting = false;
            var client = (Socket)iar.AsyncState;
            try
            {
                client.EndDisconnect(iar);
            }
            catch (Exception e)
            {
                m_OnDisconnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Exception = e });
                return;
            }

            m_OnDisconnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Suc = true });
        }
    }
}