using System.Threading.Tasks;

namespace JLGames.Infra.Event
{
    public static class AsyncEventDelegates
    {
        /// <summary>
        /// Delegate function
        /// 委托函数
        /// </summary>
        /// <param name="evd"></param>
        public delegate Task AsyncEventHandler(EventData evd);
    }
}