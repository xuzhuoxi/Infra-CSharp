using System;
using System.Net.Sockets;

namespace JLGames.Infra.Net
{
    internal interface IConnectAdapter
    {
        /// <summary>
        /// 是否已经连接
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// 正在进行连接 或 正在断开连接
        /// </summary>
        bool ActionDoing { get; }

        /// <summary>
        /// Socket实例
        /// </summary>
        Socket Socket { get; }

        /// <summary>
        /// 开始连接
        /// </summary>
        /// <returns></returns>
        void Connect(SocketParams @params, AdapterDelegates.OnConnect onConnect);

        /// <summary>
        /// 开始连接
        /// </summary>
        /// <returns></returns>
        void Connect(AdapterDelegates.OnConnect onConnect);

        /// <summary>
        /// 关闭连接并释放资源
        /// </summary>
        /// <returns></returns>
        void Close(AdapterDelegates.OnDisconnect onDisconnect, bool release);
    }
}