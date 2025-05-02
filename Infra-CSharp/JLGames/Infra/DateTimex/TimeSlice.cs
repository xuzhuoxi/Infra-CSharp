using JLGames.Infra.Mathx;
using JLGames.Infra.Extensions;

namespace JLGames.Infra.DateTimex
{
    public struct TimeSlice
    {
        public enum ZoomAnchor
        {
            Start,
            Center,
            End
        }

        private long m_Start;
        private long m_Duration;

        /// <summary>
        /// Start Timestamp
        /// 开始时间戳
        /// </summary>
        public long Start => m_Start;

        /// <summary>
        /// 结束时间戳
        /// End Timestamp
        /// </summary>
        public long End => m_Start + m_Duration;

        /// <summary>
        /// Time Length
        /// 时间长度
        /// </summary>
        public long Duration => m_Duration;

        private TimeSlice(long start, long duration)
        {
            m_Start = start;
            m_Duration = duration;
        }

        /// <summary>
        /// Check if the timestamp is within the time slice
        /// 检查时间戳是否在时间片内
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="includeEnd"></param>
        /// <returns></returns>
        public bool Contains(long timestamp, bool includeEnd = false)
        {
            if (m_Start == timestamp) return true;
            var end = End;
            if (includeEnd && end == timestamp) return true;
            return MathUtil.Between(timestamp, m_Start, end);
        }

        /// <summary>
        /// Move Time Piece
        /// 移动时间片
        /// </summary>
        /// <param name="offset"></param>
        public void Move(long offset)
        {
            m_Start += offset;
        }

        /// <summary>
        /// Extend the time length
        /// 延长时间长度
        /// </summary>
        /// <param name="duration"></param>
        public void Extend(long duration)
        {
            m_Duration += duration;
        }

        /// <summary>
        /// Zoom Time Piece
        /// 缩放时间片
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="anchor"></param>
        public void Zoom(float scale, float anchor)
        {
            if (anchor.FloatEquals(1f))
            {
                ZoomStart(scale);
                return;
            }

            if (anchor.FloatEquals(0f))
            {
                ZoomEnd(scale);
                return;
            }

            ZoomByAnchor(scale, anchor);
        }

        /// <summary>
        /// Zoom Time Piece
        /// 缩放时间片
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="anchor"></param>
        public void Zoom(float scale, ZoomAnchor anchor)
        {
            switch (anchor)
            {
                case ZoomAnchor.Start:
                    ZoomStart(scale);
                    return;
                case ZoomAnchor.Center:
                    ZoomByAnchor(scale, 0.5f);
                    return;
                case ZoomAnchor.End:
                    ZoomEnd(scale);
                    return;
            }
        }

        private void ZoomByAnchor(float scale, float anchor)
        {
            anchor = MathUtil.Clamp01(anchor);
            m_Start = m_Start - (long) (m_Duration * anchor * (scale - 1));
            m_Duration = (long) (m_Duration * scale);
        }

        private void ZoomStart(float scale)
        {
            m_Duration = (long) (m_Duration * scale);
        }

        private void ZoomEnd(float scale)
        {
            m_Start = m_Start - (long) (m_Duration * (scale - 1));
            m_Duration = (long) (m_Duration * scale);
        }

        // Static

        /// <summary>
        /// Create time slice by length of time
        /// 通过时间长度创建时间片
        /// </summary>
        /// <param name="start"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static TimeSlice NewSliceWitDur(long start, long duration)
        {
            return new TimeSlice(start, duration);
        }

        /// <summary>
        /// Create time slice from two time points
        /// 通过两个时间点创建时间片
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static TimeSlice NewSliceWithEnd(long start, long end)
        {
            return new TimeSlice(start, end - start);
        }
    }
}