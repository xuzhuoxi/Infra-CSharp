using System;
using JLGames.Infra.Event;

namespace JLGames.Infra.Net
{
    /// <summary>
    /// Internal adapter for asynchronous socket receive loop.
    /// Socket 异步接收循环适配器（内部）。
    /// </summary>
    internal interface IReceiveAdapter : IEventDispatcher
    {
        /// <summary>
        /// Whether receive loop is active.
        /// 是否正在接收。
        /// </summary>
        bool IsReceiving { get; }

        /// <summary>
        /// Start the receive loop.
        /// 开始接收。
        /// </summary>
        /// <param name="onReceive">Callback invoked after each receive.<br/>每次接收完成后的回调。</param>
        void Start(AdapterDelegates.OnReceive onReceive);

        /// <summary>
        /// Stop receiving and release receive resources.
        /// 停止接收并释放接收相关资源。
        /// </summary>
        void Stop();

        /// <summary>
        /// Schedule the next receive operation (after processing current data).
        /// 在处理完当前数据后继续下一次接收。
        /// </summary>
        void Next();
    }
}
