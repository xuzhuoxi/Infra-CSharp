using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    public interface ISockName
    {
        /// <summary>
        /// name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// set name
        /// </summary>
        /// <param name="name"></param>
        void SetName(string name);
    }

    public interface ISockSender
    {
        /// <summary>
        /// Send a message. no packed
        /// 发送消息,不作封包处理
        /// </summary>
        /// <param name="bytes"></param>
        void SendBytes(byte[] bytes);

        /// <summary>
        /// Send a message. packed
        /// 发送消息,封包
        /// </summary>
        /// <param name="message"></param>
        void SendMessage(byte[] message);

        /// <summary>
        /// Send one or more messages.
        /// 发送一个或多个消息
        /// </summary>
        /// <param name="messages"></param>
        void SendMessage(string[] messages);

        /// <summary>
        /// Send one or more messages.
        /// 发送一个或多个消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        void SendMessage(string message, params string[] messages);
    }

    public interface ISockReceiver : IEventDispatcher
    {
        /// <summary>
        /// Whether to handle the message receiving state
        /// 是否处理消息接收状态
        /// </summary>
        bool IsReceiving { get; }

        /// <summary>
        /// Start receiving message.
        /// 开始接收数据
        /// </summary>
        /// <returns></returns>
        void StartReceiving();

        /// <summary>
        /// Stop receiving message
        /// 停止接收数据
        /// </summary>
        /// <returns></returns>
        void StopReceiving();

        /// <summary>
        /// set the message handler function
        /// 设置消息处理函数
        /// </summary>
        /// <param name="handler"></param>
        void SetMessageHandler(SockDelegate.FuncMessageHandler handler);
    }

    public interface IConnection : ISockSender, ISockReceiver
    {
        /// <summary>
        /// is it connected?
        /// 判断是否连接中
        /// </summary>
        bool Connected { get; }
    }

    public interface ISockClient : ISockName, IConnection, IEventDispatcher
    {
        /// <summary>
        /// Open client
        /// 开启客户端
        /// </summary>
        /// <param name="params"></param>
        void OpenClient(SockParams @params);

        /// <summary>
        /// Close client
        /// 关闭客户端
        /// </summary>
        void CloseClient();
    }
}