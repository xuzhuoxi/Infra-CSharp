using System.Net.Sockets;
using System.Threading;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
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

        public string Name => m_Name;

        public SocketClient(string name, bool littleEndian, bool apmMode)
        {
            m_Name = name;
            m_LittleEndian = littleEndian;
            m_ApmMode = apmMode;
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

        public void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler)
        {
            m_Receiver?.SetMessageHandler(handler);
        }

        public void StartReceiving()
        {
            if (null == m_Receiver) return;
            m_Receiver.AddEventListener(SocketEvents.EventOnMessageReceived, OnReceived);
            m_Receiver.AddEventListener(SocketEvents.EventOnMessageReceivedEnd, OnReceivedEnd);
            m_Receiver.StartReceiving();
        }

        public void StopReceiving()
        {
            if (null == m_Receiver) return;
            m_Receiver.RemoveEventListener(SocketEvents.EventOnMessageReceivedEnd, OnReceivedEnd);
            m_Receiver.RemoveEventListener(SocketEvents.EventOnMessageReceived, OnReceived);
            m_Receiver.StopReceiving();
        }

        public bool IsReceiving => m_Receiver.IsReceiving;

        public bool Connected => m_ConnectAdapter.Connected;

        public void SetContext(SynchronizationContext context)
        {
            m_SyncContext = context;
        }

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
        /// 使用设置好的线程上下文重新发布事件
        /// 如果线程上下文为空，则直接发布事件
        /// </summary>
        /// <param name="evd"></param>
        private void RedispatchEvent(EventData evd)
        {
            if (null == m_SyncContext)
                DispatchEvent(evd.Type, evd.Data);
            else
                m_SyncContext.Send(_ => { DispatchEvent(evd.Type, evd.Data); }, null);
        }
    }
}