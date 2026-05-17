namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// 1D point(Integer)
    /// 一维点(整型)
    /// </summary>
    public struct Point1Int
    {
        /// <summary>
        /// Area point
        /// 区间点(整型)
        /// </summary>
        public struct AreaPoint1Int
        {
            /// <summary>
            /// Area index
            /// 区间索引
            /// </summary>
            public int AreaIndex;

            /// <summary>
            /// Area remainder
            /// 区间余量
            /// </summary>
            public int Remainder;
        }

        /// <summary>
        /// Coordinate value on the 1D axis.
        /// 一维坐标值（整型）。
        /// </summary>
        public int Value;

        /// <summary>
        /// Creates a 1D integer point with the given value.
        /// 使用指定值创建整型一维点。
        /// </summary>
        /// <param name="value">Coordinate value.<br/>坐标值。</param>
        public Point1Int(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Implicit conversion from <see cref="int"/> to <see cref="Point1Int"/>.
        /// 从 <see cref="int"/> 到 <see cref="Point1Int"/> 的隐式转换。
        /// </summary>
        public static implicit operator Point1Int(int point)
        {
            return new Point1Int(point);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Point1Int"/> to <see cref="int"/>.
        /// 从 <see cref="Point1Int"/> 到 <see cref="int"/> 的隐式转换。
        /// </summary>
        public static implicit operator int(Point1Int point)
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
        public static AreaPoint1Int ToAreaPoint1(int point1, uint size)
        {
            var itg = point1 / (int) size;
            var re = point1 % (int) size;
            if (re < 0 && itg <= 0)
            {
                itg -= 1;
            }

            re = re < 0 ? re + (int) size : re;
            return new AreaPoint1Int {AreaIndex = itg, Remainder = re};
        }

        /// <summary>
        /// Convert to 1D point
        /// 转换为一维点
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Point1Int FromAreaPoint1(AreaPoint1Int point1, uint size)
        {
            return point1.AreaIndex * (int) size + point1.Remainder;
        }
    }
}