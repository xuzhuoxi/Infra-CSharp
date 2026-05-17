using System;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// Half-open float range [Start, Start + Length).
    /// 半开浮点区间 [Start, Start + Length)。
    /// </summary>
    [Serializable]
    public struct Range
    {
        /// <summary>
        ///   <para>The starting index of the range, where 0 is the first position, 1 is the second, 2 is the third, and so on.</para>
        /// </summary>
        public float Start;

        /// <summary>
        ///   <para>The length of the range.</para>
        /// </summary>
        public float Length;

        /// <summary>
        ///   <para>The end index of the range (not inclusive).</para>
        /// </summary>
        public float End
        {
            get { return Start + Length; }
            set { Length = value - Start; }
        }

        /// <summary>
        ///   <para>Constructs a new RangeInt with given start, length values.</para>
        /// </summary>
        /// <param name="start">The starting index of the range.</param>
        /// <param name="length">The length of the range.</param>
        public Range(float start, float length)
        {
            Start = start;
            Length = length;
        }
    }
}