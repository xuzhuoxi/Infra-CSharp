using System;

namespace JLGames.Infra.Mathx
{
    [Serializable]
    public struct IntervalInt
    {
        /// <summary>
        ///   <para>The min value of the interval. where 0 is the first position, 1 is the second, 2 is the third, and so on.</para>
        /// </summary>
        public int Min;

        /// <summary>
        ///   <para>The max value of the interval.</para>
        /// </summary>
        public int Max;

        /// <summary>
        ///   <para>where include max value</para>
        /// </summary>
        public bool MaxIncluded;

        /// <summary>
        ///   <para>The length of the interval.</para>
        /// </summary>
        public int Length
        {
            get { return MaxIncluded ? Max - Min + 1 : Max - Min; }
            set { Max = MaxIncluded ? Min + value - 1 : Min + value; }
        }

        /// <summary>
        ///   <para>Constructs a new RangeInt with given min, max, maxIncluded values.</para>
        /// </summary>
        public IntervalInt(int min, int max, bool maxIncluded)
        {
            Min = min;
            Max = max;
            MaxIncluded = maxIncluded;
        }
    }
}