using System;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// Half-open integer range [Start, Start + Length).
    /// 半开整型区间 [Start, Start + Length)。
    /// </summary>
    [Serializable]
    public struct RangeInt
    {
        /// <summary>
        ///   <para>The starting index of the range, where 0 is the first position, 1 is the second, 2 is the third, and so on.</para>
        /// </summary>
        public int Start;

        /// <summary>
        ///   <para>The length of the range.</para>
        /// </summary>
        public int Length;

        /// <summary>
        ///   <para>The end index of the range (not inclusive).</para>
        /// </summary>
        public int End
        {
            get { return Start + Length; }
            set { Length = value - Start; }
        }

        /// <summary>
        ///   <para>Constructs a new RangeInt with given start, length values.</para>
        /// </summary>
        /// <param name="start">The starting index of the range.</param>
        /// <param name="length">The length of the range.</param>
        public RangeInt(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}