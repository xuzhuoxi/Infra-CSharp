using System;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    internal interface IReceiveAdapter : IEventDispatcher
    {
        /// <summary>
        /// 是否正在接收
        /// </summary>
        bool IsReceiving { get; }

        /// <summary>
        /// 开始接收
        /// </summary>
        /// <returns></returns>
        void Start(AdapterDelegates.OnReceive onReceive);

        /// <summary>
        /// 停止接收并释放资源
        /// </summary>
        /// <returns></returns>
        void Stop();

        /// <summary>
        /// 继续接收下一条消息
        /// </summary>
        void Next();
    }
}