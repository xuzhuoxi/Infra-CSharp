namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// 1D point
    /// 一维点
    /// </summary>
    public struct Point1
    {
        /// <summary>
        /// Area point
        /// 区间点
        /// </summary>
        public struct AreaPoint1
        {
            /// <summary>
            /// Area index
            /// 区间索引
            /// </summary>
            public long AreaIndex;

            /// <summary>
            /// Area remainder
            /// 区间余量
            /// </summary>
            public double Remainder;
        }

        public double Value;

        public Point1(double value)
        {
            Value = value;
        }

        public static implicit operator Point1(double point)
        {
            return new Point1(point);
        }

        public static implicit operator double(Point1 point)
        {
            return point.Value;
        }

        /// <summary>
        /// Convert to area point
        /// 转换为区间点
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static AreaPoint1 ToAreaPoint1(double point1, ulong size)
        {
            var itg = (long) (point1 / size);
            var re = point1 % size;
            if (re < 0 && itg <= 0)
            {
                itg -= 1;
            }

            re = re < 0 ? re + size : re;
            return new AreaPoint1 {AreaIndex = itg, Remainder = re};
        }

        /// <summary>
        /// Convert to 1D point
        /// 转换为一维点
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Point1 FromAreaPoint1(AreaPoint1 point1, ulong size)
        {
            return point1.AreaIndex * (long) size + point1.Remainder;
        }
    }
}