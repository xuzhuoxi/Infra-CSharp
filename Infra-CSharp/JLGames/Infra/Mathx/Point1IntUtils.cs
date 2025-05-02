namespace JLGames.Infra.Mathx
{
    public static class Point1IntUtils
    {
        /// <summary>
        /// Convert to area point
        /// 转换为区间点
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Point1Int.AreaPoint1Int ToAreaPoint1(this Point1Int point1, uint size)
        {
            return Point1Int.ToAreaPoint1(point1.Value, size);
        }

        /// <summary>
        /// Convert to 1D point
        /// 转换为一维点
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Point1Int FromAreaPoint1(this Point1Int.AreaPoint1Int point1, uint size)
        {
            return Point1Int.FromAreaPoint1(point1, size);
        }
    }
}