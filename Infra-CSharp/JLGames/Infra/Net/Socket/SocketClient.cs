using System.Net.Sockets;
using System.Threading;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Default implementation of <see cref="ISocketClient"/> (connect, send, receive, events).
    /// <see cref="ISocketClient"/> 的默认实现（连接、收发、事件）。
    /// </summary>
    public class SocketClient : EventDispatcher, ISocketClient
    {
        private string m_Name;
        private SynchronizationContext m_SyncContext;
        private readonly bool m_LittleEndian;
        private readonly bool m_ApmMode;
        private SocketParams m_Params;

        private IConnectAdapter m_ConnectAdapter;
        private ISocketSender m_Sender;
        private ISocketReceiver m_Receiver;

        /// <inheritdoc/>
        public string Name => m_Name;

        /// <summary>
        /// Create a socket client.
        /// 创建 Socket 客户端。
        /// </summary>
        /// <param name="name">Client name.<br/>客户端名称。</param>
        /// <param name="littleEndian">Use little-endian for messages.<br/>消息是否小端。</param>
        /// <param name="apmMode">Use APM async model when true.<br/>为 true 时使用 APM 异步模型。</param>
        public SocketClient(string name, bool littleEndian, bool apmMode)
        {
            m_Name = name;
            m_LittleEndian = littleEndian;
            m_ApmMode = apmMode;
        }

        /// <summary>
        /// Update the client display name.
        /// 更新客户端名称。
        /// </summary>
        /// <param name="name">New name.<br/>新名称。</param>
        public void SetName(string name)
        {
            m_Name = name;
        }

        /// <inheritdoc/>
        public void SendBytes(byte[] bytes)
        {
            m_Sender?.SendBytes(bytes);
        }

        /// <inheritdoc/>
        public void SendMessage(byte[] message)
        {
            m_Sender?.SendMessage(message);
        }

        /// <inheritdoc/>
        public void SendMessage(string[] messages)
        {
            m_Sender?.SendMessage(messages);
        }

        /// <inheritdoc/>
        public void SendMessage(string message, params string[] messages)
        {
            m_Sender?.SendMessage(message, messages);
        }

        /// <inheritdoc/>
        public void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler)
        {
            m_Receiver?.SetMessageHandler(handler);
        }

        /// <inheritdoc/>
        public void StartReceiving()
        {
            if (null == m_Receiver) return;
            m_Receiver.AddEventListener(SocketEvents.EventOnMessageReceived, OnReceived);
            m_Receiver.AddEventListener(SocketEvents.EventOnMessageReceivedEnd, OnReceivedEnd);
            m_Receiver.StartReceiving();
        }

        /// <inheritdoc/>
        public void StopReceiving()
        {
            if (null == m_Receiver) return;
            m_Receiver.RemoveEventListener(SocketEvents.EventOnMessageReceivedEnd, OnReceivedEnd);
            m_Receiver.RemoveEventListener(SocketEvents.EventOnMessageReceived, OnReceived);
            m_Receiver.StopReceiving();
        }

        /// <inheritdoc/>
        public bool IsReceiving => m_Receiver.IsReceiving;

        /// <inheritdoc/>
        public bool Connected => m_ConnectAdapter.Connected;

        /// <inheritdoc/>
        public void SetContext(SynchronizationContext context)
        {
            m_SyncContext = context;
        }

        /// <inheritdoc/>
        public void ConnectServer(SocketParams @params)
        {
            if (null != m_ConnectAdapter)
            {
                if (null == m_SyncContext)
                    DispatchEvent(SocketEvents.EventOnConnectionOpen, new SocketEvents.SocketConnEventInfo(false));
                else
                    m_SyncContext.Send(_ => { DispatchEvent(SocketEvents.EventOnConnectionOpen, new SocketEvents.SocketConnEventInfo(false)); },
                        null);

                return;
            }

            m_Params = @params;
            if (m_ApmMode)
                m_ConnectAdapter = new BeginConnectAdapter(@params);
            else
                m_ConnectAdapter = new ConnectAsyncAdapter(@params);

            m_ConnectAdapter.Connect(onConnect: OnConnected);
        }

        private void OnConnected(AdapterDelegates.ConnectResultInfo info)
        {
            if (null == m_SyncContext)
                HandleConnectResult(info);
            else
                m_SyncContext.Send(_ => { HandleConnectResult(info); }, null);
        }

        private void HandleConnectResult(AdapterDelegates.ConnectResultInfo info)
        {
            // 连接成功
            if (info.Suc && info.Error == SocketError.Success)
            {
                CreateSendReceiver();
                DispatchEvent(SocketEvents.EventOnConnectionOpen, new SocketEvents.SocketConnEventInfo(true));
                return;
            }

            // 连接失败
            DispatchEvent(SocketEvents.EventOnConnectionOpen,
                new SocketEvents.SocketConnEventInfo(false, info.Error, info.Exception));
        }

        /// <inheritdoc/>
        public void DisconnectServer()
        {
            if (null == m_ConnectAdapter)
            {
                if (null == m_SyncContext)
                    DispatchEvent(SocketEvents.EventOnConnectionClose, new SocketEvents.SocketConnEventInfo(false));
                else
                    m_SyncContext.Send(_ => {  DispatchEvent(SocketEvents.EventOnConnectionClose, new SocketEvents.SocketConnEventInfo(false)); }, null);

                return;
            }

            m_Receiver.StopReceiving();
            m_ConnectAdapter.Close(OnDisconnect, true);
        }

        private void OnDisconnect(AdapterDelegates.ConnectResultInfo info)
        {
            if (null == m_SyncContext)
                HandleDisconnectResult(info);
            else
                m_SyncContext.Send(_ => { HandleDisconnectResult(info); }, null);
        }

        private void HandleDisconnectResult(AdapterDelegates.ConnectResultInfo info)
        {
            // 断开成功
            if (info.Suc && info.Error == SocketError.Success)
            {
                DispatchEvent(SocketEvents.EventOnConnectionClose, new SocketEvents.SocketConnEventInfo(true));
                return;
            }

            // 断开失败
            DispatchEvent(SocketEvents.EventOnConnectionClose,
                new SocketEvents.SocketConnEventInfo(false, info.Error, info.Exception));
        }

        private void CreateSendReceiver()
        {
            m_Sender = new SocketSender(m_Name, m_ConnectAdapter.Socket, new NetMessageWriter(m_LittleEndian));
            m_Receiver = new SocketReceiver(m_Name, m_ConnectAdapter.Socket, new NetMessageReader(m_LittleEndian), m_ApmMode);
        }

        private void OnReceived(EventData evd)
        {
            RedispatchEvent(evd);
        }

        private void OnReceivedEnd(EventData evd)
        {
            StopReceiving();
            RedispatchEvent(evd);
        }

        /// <summary>
        /// Redispatch event on the configured synchronization context, or directly if none.
        /// 在已配置的同步上下文上重新派发事件；未配置则直接派发。
        /// </summary>
        /// <param name="evd">Event data.<br/>事件数据。</param>
        private void RedispatchEvent(EventData evd)
        {
            if (null == m_SyncContext)
                DispatchEvent(evd.Type, evd.Data);
            else
                m_SyncContext.Send(_ => { DispatchEvent(evd.Type, evd.Data); }, null);
        }
    }
}
