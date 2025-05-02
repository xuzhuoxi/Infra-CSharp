namespace JLGames.Infra.Serial
{
    public interface ISerialManager
    {
        /// <summary>
        /// Append module
        /// 添加模块
        /// </summary>
        /// <param name="module"></param>
        void AppendModule(ISerialModule module);

        /// <summary>
        /// Manager start
        /// 管理器启动
        /// </summary>
        /// <param name="endCall"></param>
        /// <returns></returns>
        bool StartManager(Callback endCall = null);

        /// <summary>
        /// Manager stop
        /// 管理器停止
        /// </summary>
        /// <param name="endCall"></param>
        /// <returns></returns>
        bool StopManager(Callback endCall = null);
    }
}