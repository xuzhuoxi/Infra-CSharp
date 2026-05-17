using System;
using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Internal adapter for asynchronous socket connect/disconnect.
    /// Socket 异步连接/断开适配器（内部）。
    /// </summary>
    internal interface IConnectAdapter
    {
        /// <summary>
        /// Whether the socket is connected.
        /// 是否已连接。
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Whether a connect or disconnect operation is in progress.
        /// 是否正在进行连接或断开。
        /// </summary>
        bool ActionDoing { get; }

        /// <summary>
        /// Underlying <see cref="Socket"/> instance.
        /// 底层 <see cref="Socket"/> 实例。
        /// </summary>
        Socket Socket { get; }

        /// <summary>
        /// Start connecting with new parameters.
        /// 使用新参数开始连接。
        /// </summary>
        /// <param name="params">Connection parameters.<br/>连接参数。</param>
        /// <param name="onConnect">Completion callback.<br/>完成回调。</param>
        void Connect(SocketParams @params, AdapterDelegates.OnConnect onConnect);

        /// <summary>
        /// Start connecting with parameters already set on the adapter.
        /// 使用适配器上已设置的参数开始连接。
        /// </summary>
        /// <param name="onConnect">Completion callback.<br/>完成回调。</param>
        void Connect(AdapterDelegates.OnConnect onConnect);

        /// <summary>
        /// Close the connection and optionally release the socket.
        /// 关闭连接并可选择释放 Socket。
        /// </summary>
        /// <param name="onDisconnect">Completion callback.<br/>完成回调。</param>
        /// <param name="release">Release socket resources when true.<br/>为 true 时释放 Socket 资源。</param>
        void Close(AdapterDelegates.OnDisconnect onDisconnect, bool release);
    }
}
