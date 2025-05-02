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
        /// <param name="basestamp">Base Stamp（基础时间戳）</param>
        /// <param name="loop">Weather to loop（是否循环）</param>
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
        /// <param name="basestamp"></param>
        public void SetBasestamp(long basestamp)
        {
            m_Basestamp = basestamp;
        }

        /// <summary>
        /// Checks whether a time slice with the specified name is contained
        /// 检查是否包含指定名称的时间片
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Contains(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return -1 != m_Slices.FindIndex(unit => unit.Name == name);
        }

        /// <summary>
        /// Find the first time slice with the specified name
        /// 查找第一个指定名称的时间片
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int FindFirstSlice(string name)
        {
            if (string.IsNullOrEmpty(name)) return -1;
            return m_Slices.FindIndex(unit => unit.Name == name);
        }

        /// <summary>
        /// Find the last time slice with the specified name
        /// 查找最后一个指定名称的时间片
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int FindLastSlice(string name)
        {
            if (string.IsNullOrEmpty(name)) return -1;
            return m_Slices.FindLastIndex(unit => unit.Name == name);
        }

        /// <summary>
        /// Locating time slices based on timestamps
        /// 根据时间戳定位时间片
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
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
        /// <param name="name"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public bool AddSlice(string name, long duration)
        {
            return AddSlice(name, 0, duration);
        }

        /// <summary>
        /// Add time slice
        /// 追加时间片
        /// </summary>
        /// <param name="name"></param>
        /// <param name="space"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
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
        /// <param name="index"></param>
        /// <param name="keepBlank">是否把移除后的时间片转为空白</param>
        /// <returns></returns>
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
        /// <param name="keepBlank"></param>
        /// <returns></returns>
        public string RemoveFirst(bool keepBlank = false)
        {
            return RemoveAt(0, keepBlank);
        }

        /// <summary>
        /// Remove last time slice
        /// 移除最后一个时间片
        /// </summary>
        /// <returns></returns>
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