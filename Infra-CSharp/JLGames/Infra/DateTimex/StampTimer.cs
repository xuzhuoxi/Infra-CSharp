using System;

namespace JLGames.Infra.DateTimex
{
    public sealed class StampTimer
    {
        /// <summary>
        /// Get the current Ticks function proxy
        /// 获取当前Ticks函数代理
        /// </summary>
        public delegate long NowTicksGetter();

        /// <summary>
        /// The default elapsed time when the timer is initialized
        /// 初始化计时器时默认已经流失的时间
        /// Used to define the start time
        /// 用于锁定起始时间
        /// </summary>
        private readonly long m_BaseLostTicks;

        /// <summary>
        /// Increased lost time in time running
        /// 运行时增加的流失时间
        /// </summary>
        private long m_RunningLostTicks;

        /// <summary>
        /// Start time of timer
        /// 时间器开始时间
        /// </summary>
        private long m_StartStamp;

        /// <summary>
        /// current timer pause timestamp
        /// 当前计时器暂停时间戳
        /// </summary>
        private long m_PauseStamp = -1;

        /// <summary>
        /// length of pause
        /// 暂停时间长度
        /// </summary>
        private long m_PauseLostTicks;

        private NowTicksGetter m_TicksGetter = null;

        /// <summary>
        /// Get the current ticks
        /// 取当前时间
        /// </summary>
        private long NowTicks => m_TicksGetter?.Invoke() ?? DateTimeUtil.NowTicks1970;

        /// <summary>
        /// Lost time
        /// 流失的时间
        /// </summary>
        /// <returns></returns>
        public long LostTicks
        {
            get
            {
                if (IsPause)
                    return m_PauseStamp - m_StartStamp + m_RunningLostTicks - m_PauseLostTicks;

                return NowTicks - m_StartStamp + m_RunningLostTicks - m_PauseLostTicks;
            }
        }

        /// <summary>
        /// Lost time(millisecond)
        /// 流失的时间(毫秒)
        /// </summary>
        public long LostMilliseconds => DateTimeUtil.Ticks2Millis(LostTicks);

        /// <summary>
        /// Global lost time(ticks)
        /// 全局流失的时间(Ticks)
        /// </summary>
        public long GlobalLostTicks => LostTicks + m_BaseLostTicks;

        /// <summary>
        /// Global lost time(millisecond)
        /// 全局流失的时间(毫秒)
        /// </summary>
        public long GlobalLostMilliseconds => DateTimeUtil.Ticks2Millis(GlobalLostTicks);

        /// <summary>
        /// Pause lost time(ticks)
        /// 暂停用掉的时间(Ticks)
        /// </summary>
        /// <returns></returns>
        public long PauseTicks =>
            IsPause ? NowTicks - m_PauseStamp + m_PauseLostTicks : m_PauseLostTicks;

        /// <summary>
        /// Pause lost time(millisecond)
        /// 暂停用掉的时间(毫秒)
        /// </summary>
        /// <returns></returns>
        public long PauseMilliseconds => DateTimeUtil.Ticks2Millis(PauseTicks);

        /// <summary>
        /// Is it pausing
        /// 是否暂时中
        /// </summary>
        public bool IsPause => m_PauseStamp > 0;

        public StampTimer()
        {
            m_BaseLostTicks = 0;
        }

        public StampTimer(long baseLostTicks)
        {
            m_BaseLostTicks = baseLostTicks;
        }

        public StampTimer(DateTime baseLostDateTime)
        {
            m_BaseLostTicks = baseLostDateTime.Ticks;
        }

        /// <summary>
        /// Set the function to get the current timestamp
        /// 设置获取当前时间戳的函数
        /// </summary>
        /// <param name="getter"></param>
        public void SetNowTicksGetter(NowTicksGetter getter)
        {
            m_TicksGetter = getter;
        }

        /// <summary>
        /// Start the timer
        /// 开始计时
        /// </summary>
        public void Start()
        {
            m_StartStamp = NowTicks;
        }

        /// <summary>
        /// Stop the timer
        /// 计时器结束
        /// </summary>
        public void Stop()
        {
            Pause();
        }

        /// <summary>
        /// Pause the timer
        /// 时间暂停
        /// </summary>
        public void Pause()
        {
            if (IsPause) return;

            m_PauseStamp = NowTicks;
        }

        /// <summary>
        /// Time goes on
        /// 时间继续流动
        /// </summary>
        public void Continue()
        {
            if (!IsPause) return;

            m_PauseLostTicks += (NowTicks - m_PauseStamp);
            m_PauseStamp = -1;
        }

        /// <summary>
        /// Increase lost time
        /// 增加流失时间
        /// </summary>
        /// <param name="lostTicks"></param>
        public void AddLost(long lostTicks)
        {
            if (IsPause)
                AddPausingLost(lostTicks);
            else
                AddRunningLost(lostTicks);
        }

        /// <summary>
        /// Increase running lost time
        /// 增加运行流失时间
        /// </summary>
        /// <param name="lostTicks"></param>
        public void AddRunningLost(long lostTicks)
        {
            m_RunningLostTicks += lostTicks;
        }

        /// <summary>
        /// Increase pausing lost time
        /// 增加暂停运行时间
        /// </summary>
        /// <param name="lostTicks"></param>
        public void AddPausingLost(long lostTicks)
        {
            m_PauseLostTicks += lostTicks;
        }

        //-------------------------------

        /// <summary>
        /// Generate a timestamp object
        /// 生成一个时间戳对象
        /// </summary>
        /// <param name="initTicks"></param>
        /// <returns></returns>
        public static StampTimer GenTimer(long initTicks)
        {
            var rs = new StampTimer(initTicks);
            return rs;
        }

        /// <summary>
        /// Generate a timestamp object
        /// 生成一个时间戳对象
        /// </summary>
        /// <returns></returns>
        public static StampTimer GenTimer()
        {
            var rs = new StampTimer(0);
            return rs;
        }
    }
}