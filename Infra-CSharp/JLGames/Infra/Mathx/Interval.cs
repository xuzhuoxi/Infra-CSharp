using System;

namespace JLGames.Infra.Mathx
{
    [Serializable]
    public struct Interval
    {
        /// <summary>
        ///   <para>The min value of the interval.</para>
        /// </summary>
        public float Min;

        /// <summary>
        ///   <para>The max value of the interval.</para>
        /// </summary>
        public float Max;

        /// <summary>
        ///   <para>The length of the interval (inclusive).</para>
        /// </summary>
        public float Length
        {
            get { return Max - Min; }
            set { Max = Min + value; }
        }

        /// <summary>
        ///   <para>Constructs a new Interval with given min, max values.</para>
        /// </summary>
        public Interval(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}