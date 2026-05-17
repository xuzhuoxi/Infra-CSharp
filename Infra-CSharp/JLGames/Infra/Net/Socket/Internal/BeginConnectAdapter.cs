using System;
using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// <see cref="IConnectAdapter"/> using <see cref="Socket.BeginConnect"/> / <see cref="Socket.BeginDisconnect"/> (APM).
    /// 基于 <see cref="Socket.BeginConnect"/> / <see cref="Socket.BeginDisconnect"/> 的连接适配器（APM）。
    /// </summary>
    internal class BeginConnectAdapter : IConnectAdapter
    {
        private SocketParams m_Params;
        private Socket m_Client;
        private bool m_Release;

        private AdapterDelegates.OnConnect m_OnConnectHandler;
        private AdapterDelegates.OnDisconnect m_OnDisconnectHandler;

        private bool m_Connecting;
        private bool m_Disconnecting;

        /// <inheritdoc/>
        public Socket Socket => m_Client;

        /// <inheritdoc/>
        public bool Connected => m_Client?.Connected ?? false;

        /// <inheritdoc/>
        public bool ActionDoing => m_Connecting || m_Disconnecting;

        /// <summary>
        /// Create adapter; call <see cref="Connect"/> with parameters before use.
        /// 创建适配器；使用前须通过 <see cref="Connect"/> 传入参数。
        /// </summary>
        public BeginConnectAdapter()
        {
        }

        /// <summary>
        /// Create adapter with initial connection parameters.
        /// 使用初始连接参数创建适配器。
        /// </summary>
        /// <param name="params">Connection parameters.<br/>连接参数。</param>
        public BeginConnectAdapter(SocketParams @params)
        {
            m_Params = @params;
        }

        /// <inheritdoc/>
        public void Connect(SocketParams @params, AdapterDelegates.OnConnect onConnect)
        {
            m_Params = @params;
            Connect(onConnect);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
