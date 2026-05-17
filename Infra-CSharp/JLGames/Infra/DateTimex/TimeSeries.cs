using System.Collections.Generic;

namespace JLGames.Infra.DateTimex
{
    /// <summary>
    /// Time Series
    /// 时间序列
    /// </summary>
    public class TimeSeries
    {
        private struct SliceUnit
        {
            private readonly string m_Name;
            private TimeSlice m_Slice;

            public string Name => m_Name;
            public TimeSlice Slice => m_Slice;

            public SliceUnit(string name, TimeSlice slice)
            {
                m_Name = name;
                m_Slice = slice;
            }
        }

        private long m_Basestamp;
        private bool m_Loop;

        private readonly List<SliceUnit> m_Slices;

        /// <summary>
        /// Construct a time series object
        /// 构造一个时间序列对象
        /// </summary>
        /// <param name="basestamp">Base timestamp subtracted before locating slices. 定位前减去的基础时间戳。</param>
        /// <param name="loop">Whether to loop when locating timestamps. 定位时间戳时是否循环。</param>
        public TimeSeries(long basestamp = 0, bool loop = false)
        {
            m_Basestamp = basestamp;
            m_Loop = loop;
            m_Slices = new List<SliceUnit>();
        }

        /// <summary>
        /// Set base timestamp
        /// 设置基础时间戳
        /// </summary>
        /// <param name="basestamp">Base timestamp for locating slices. 用于定位时间片的基础时间戳。</param>
        public void SetBasestamp(long basestamp)
        {
            m_Basestamp = basestamp;
        }

        /// <summary>
        /// Checks whether a time slice with the specified name is contained
        /// 检查是否包含指定名称的时间片
        /// </summary>
        /// <param name="name">Slice name to look up. 要查找的时间片名称。</param>
        /// <returns>True if any slice uses this name. 存在同名时间片时返回 true。</returns>
        public bool Contains(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return -1 != m_Slices.FindIndex(unit => unit.Name == name);
        }

        /// <summary>
        /// Find the first time slice with the specified name
        /// 查找第一个指定名称的时间片
        /// </summary>
        /// <param name="name">Slice name to find. 要查找的时间片名称。</param>
        /// <returns>Index of the first match, or -1 if not found. 首个匹配的索引，未找到时为 -1。</returns>
        public int FindFirstSlice(string name)
        {
            if (string.IsNullOrEmpty(name)) return -1;
            return m_Slices.FindIndex(unit => unit.Name == name);
        }

        /// <summary>
        /// Find the last time slice with the specified name
        /// 查找最后一个指定名称的时间片
        /// </summary>
        /// <param name="name">Slice name to find. 要查找的时间片名称。</param>
        /// <returns>Index of the last match, or -1 if not found. 最后一个匹配的索引，未找到时为 -1。</returns>
        public int FindLastSlice(string name)
        {
            if (string.IsNullOrEmpty(name)) return -1;
            return m_Slices.FindLastIndex(unit => unit.Name == name);
        }

        /// <summary>
        /// Locating time slices based on timestamps
        /// 根据时间戳定位时间片
        /// </summary>
        /// <param name="timestamp">Absolute timestamp (relative to series baseline). 绝对时间戳（相对序列基准）。</param>
        /// <returns>Name of the slice containing the offset, or null if none. 包含该偏移的时间片名称，无匹配时为 null。</returns>
        public string LocateTo(long timestamp)
        {
            if (timestamp < m_Basestamp || m_Slices.Count <= 0) return null;
            if (m_Loop)
                return LoopLocateTo(timestamp - m_Basestamp);
            else
                return NonLoopLocateTo(timestamp - m_Basestamp);
        }

        /// <summary>
        /// Add time slice
        /// 追加时间片
        /// </summary>
        /// <param name="name">Slice name. 时间片名称。</param>
        /// <param name="duration">Duration in ticks (no leading gap). 时长（Tick），无前导间隔。</param>
        /// <returns>True if added; false when name is empty or duration is negative. 添加成功返回 true；名称为空或时长为负时返回 false。</returns>
        public bool AddSlice(string name, long duration)
        {
            return AddSlice(name, 0, duration);
        }

        /// <summary>
        /// Add time slice
        /// 追加时间片
        /// </summary>
        /// <param name="name">Slice name. 时间片名称。</param>
        /// <param name="space">Leading gap before this slice within the series layout. 本片在序列布局中的前导间隔。</param>
        /// <param name="duration">Duration in ticks. 时长（Tick）。</param>
        /// <returns>True if added; false when arguments are invalid. 参数有效且添加成功时返回 true。</returns>
        public bool AddSlice(string name, long space, long duration)
        {
            if (string.IsNullOrEmpty(name) || space < 0 || duration < 0) return false;
            m_Slices.Add(new SliceUnit(name, TimeSlice.NewSliceWitDur(space, duration)));
            return true;
        }

        /// <summary>
        /// Remove time slice
        /// 移除时间片
        /// </summary>
        /// <param name="index">Index of the slice to remove. 要移除的时间片索引。</param>
        /// <param name="keepBlank">If true, shift the next slice to absorb the removed span. 为 true 时，将下一片平移以保留空白时长。</param>
        /// <returns>Removed slice name, or null if index is out of range. 被移除片的名称；索引越界时为 null。</returns>
        public string RemoveAt(int index, bool keepBlank = false)
        {
            if (index < 0 || index >= m_Slices.Count) return null;
            var remove = m_Slices[index];
            if (keepBlank && index < m_Slices.Count - 1)
            {
                m_Slices[index + 1].Slice.Move(remove.Slice.Start + remove.Slice.Duration);
            }

            m_Slices.RemoveAt(index);
            return remove.Name;
        }

        /// <summary>
        /// Remove the first time slice
        /// 移除第一个时间片
        /// </summary>
        /// <param name="keepBlank">If true, shift the next slice after removal. 为 true 时，移除后平移下一片。</param>
        /// <returns>Removed slice name, or null if the series is empty. 被移除片的名称；序列为空时为 null。</returns>
        public string RemoveFirst(bool keepBlank = false)
        {
            return RemoveAt(0, keepBlank);
        }

        /// <summary>
        /// Remove last time slice
        /// 移除最后一个时间片
        /// </summary>
        /// <returns>Removed slice name, or null if the series is empty. 被移除片的名称；序列为空时为 null。</returns>
        public string RemoveLast()
        {
            if (m_Slices.Count == 0) return null;
            var index = m_Slices.Count - 1;
            var remove = m_Slices[index];
            m_Slices.RemoveAt(index);
            return remove.Name;
        }

        /// <summary>
        /// Remove all time slices
        /// 移除全部时间片
        /// </summary>
        public void RemoveAll()
        {
            m_Slices.Clear();
        }

        private string LoopLocateTo(long timestamp)
        {
            var seriesLen = GetSeriesDuration();
            var duration = timestamp;
            if (duration > seriesLen)
            {
                duration = duration % seriesLen;
            }

            return NonLoopLocateTo(duration);
        }

        private string NonLoopLocateTo(long timestamp)
        {
            var duration = timestamp;
            for (var index = 0; index < m_Slices.Count; index++)
            {
                if (m_Slices[index].Slice.Duration <= 0) continue;
                if (duration == 0) return m_Slices[index].Name;

                duration = duration - m_Slices[index].Slice.Start;
                if (duration < 0) return null;
                if (duration == 0) return m_Slices[index].Name;

                duration = duration - m_Slices[index].Slice.Duration;
                if (duration < 0) return m_Slices[index].Name;
            }

            return null;
        }

        private long GetSeriesDuration()
        {
            var rs = 0L;
            foreach (var unit in m_Slices)
            {
                rs += (unit.Slice.Start + unit.Slice.Duration);
            }

            return rs;
        }
    }
}