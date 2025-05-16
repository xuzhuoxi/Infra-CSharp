using System;
using System.Net.Sockets;
using System.Threading;

namespace JLGames.Infra.Net
{
    internal class ConnectAsyncAdapter : IConnectAdapter
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

        public ConnectAsyncAdapter()
        {
        }

        public ConnectAsyncAdapter(SocketParams @params)
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
                var args = new SocketAsyncEventArgs { RemoteEndPoint = m_Params.RemoteEndPoint() };
                args.Completed += OnConnect;
                var pending = m_Client.ConnectAsync(args);
                if (!pending)
                {
                    OnConnect(m_Client, args);
                }
            }
            catch (Exception e)
            {
                m_Connecting = false;
                m_OnConnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Exception = e });
            }
        }

        private void OnConnect(object sender, SocketAsyncEventArgs e)
        {
            e.Completed -= OnConnect;
            if (m_Connecting)
            {
                m_Connecting = false;
                var suc = e.SocketError == SocketError.Success;
                m_OnConnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Suc = suc, Error = e.SocketError });
            }
            else
            {
                m_OnConnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Cancel = true });
            }
        }

        private void StartDisconnect(bool release)
        {
            m_Release = release;
            m_Disconnecting = true;
            try
            {
                m_Client.Shutdown(SocketShutdown.Both);
                var args = new SocketAsyncEventArgs { DisconnectReuseSocket = false };
                args.Completed += OnDisconnect;
                var pending = m_Client.DisconnectAsync(args);
                if (!pending)
                {
                    OnDisconnect(m_Client, args);
                }
            }
            catch (Exception e)
            {
                m_Disconnecting = false;
                m_OnDisconnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Exception = e });
            }
        }

        private void OnDisconnect(object sender, SocketAsyncEventArgs e)
        {
            e.Completed -= OnDisconnect;
            m_Disconnecting = false;
            if (m_Disconnecting)
            {
                m_Disconnecting = false;
                var suc = e.SocketError == SocketError.Success;
                if (m_Release) DoRelease();
                m_OnDisconnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Suc = suc, Error = e.SocketError });
            }
            else
            {
                m_OnDisconnectHandler?.Invoke(new AdapterDelegates.ConnectResultInfo { Cancel = true });
            }
        }

        private void DoRelease()
        {
            m_Client?.Close();
            m_Client = null;
            m_Release = false;
        }
    }
}