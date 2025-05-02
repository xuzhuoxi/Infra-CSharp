namespace JLGames.Infra.Mathx
{
    public static class Point1Utils
    {
        /// <summary>
        /// Convert to area point
        /// 转换为区间点
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Point1.AreaPoint1 ToAreaPoint1(this Point1 point1, ulong size)
        {
            return Point1.ToAreaPoint1(point1.Value, size);
        }

        /// <summary>
        /// Convert to 1D point
        /// 转换为一维点
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Point1 FromAreaPoint1(this Point1.AreaPoint1 point1, ulong size)
        {
            return Point1.FromAreaPoint1(point1, size);
        }
    }
}