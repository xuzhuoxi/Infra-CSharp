using JLGames.Infra.Mathx;
using JLGames.Infra.Extensions;

namespace JLGames.Infra.DateTimex
{
    /// <summary>
    /// A time interval on the timeline with start, duration, and move/zoom operations.
    /// 时间轴上的时间片，包含起点、时长，并支持平移与缩放。
    /// </summary>
    public struct TimeSlice
    {
        /// <summary>
        /// Anchor position when scaling a time slice.
        /// 缩放时间片时的锚点位置。
        /// </summary>
        public enum ZoomAnchor
        {
            /// <summary>Anchor at start. 锚定在起点。</summary>
            Start,

            /// <summary>Anchor at center. 锚定在中心。</summary>
            Center,

            /// <summary>Anchor at end. 锚定在终点。</summary>
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
        /// <param name="timestamp">Timestamp to test. 待检测的时间戳。</param>
        /// <param name="includeEnd">Whether the end boundary is inclusive. 是否包含结束边界。</param>
        /// <returns>True if <paramref name="timestamp"/> lies within the slice. 时间戳落在片内时返回 true。</returns>
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
        /// <param name="offset">Ticks to shift the start; duration is unchanged. 平移起点的 Tick 偏移量，时长不变。</param>
        public void Move(long offset)
        {
            m_Start += offset;
        }

        /// <summary>
        /// Extend the time length
        /// 延长时间长度
        /// </summary>
        /// <param name="duration">Ticks to add to duration. 追加到时长上的 Tick 数。</param>
        public void Extend(long duration)
        {
            m_Duration += duration;
        }

        /// <summary>
        /// Zoom Time Piece
        /// 缩放时间片
        /// </summary>
        /// <param name="scale">Scale factor; 1 means no change. 缩放系数，1 表示不变。</param>
        /// <param name="anchor">Anchor in [0,1]: 0 end, 1 start, otherwise interpolated. 锚点 [0,1]：0 为终点，1 为起点，其余为插值。</param>
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
        /// <param name="scale">Scale factor; 1 means no change. 缩放系数，1 表示不变。</param>
        /// <param name="anchor">Fixed anchor: start, center, or end. 固定锚点：起点、中心或终点。</param>
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
        /// <param name="start">Start timestamp. 开始时间戳。</param>
        /// <param name="duration">Duration in ticks. 时长（Tick）。</param>
        /// <returns>A new time slice. 新的时间片。</returns>
        public static TimeSlice NewSliceWitDur(long start, long duration)
        {
            return new TimeSlice(start, duration);
        }

        /// <summary>
        /// Create time slice from two time points
        /// 通过两个时间点创建时间片
        /// </summary>
        /// <param name="start">Start timestamp. 开始时间戳。</param>
        /// <param name="end">End timestamp (exclusive unless used with <see cref="Contains"/> including end). 结束时间戳。</param>
        /// <returns>A new time slice with duration <c>end - start</c>. 时长为 <c>end - start</c> 的新时间片。</returns>
        public static TimeSlice NewSliceWithEnd(long start, long end)
        {
            return new TimeSlice(start, end - start);
        }
    }
}