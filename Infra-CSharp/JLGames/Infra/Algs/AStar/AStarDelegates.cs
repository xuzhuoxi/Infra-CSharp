namespace JLGames.Infra.AStar
{
    /// <summary>
    /// Delegate definition
    /// 代理定义
    /// </summary>
    public static class AStarDelegates
    {
        /// <summary>
        /// Calculation of movement cost between two points
        /// 两点间的移动代价计算
        /// </summary>
        /// <param name="cx">Current X; 当前点 X</param>
        /// <param name="cy">Current Y; 当前点 Y</param>
        /// <param name="cz">Current Z; 当前点 Z</param>
        /// <param name="ex">Goal X; 终点 X</param>
        /// <param name="ey">Goal Y; 终点 Y</param>
        /// <param name="ez">Goal Z; 终点 Z</param>
        /// <returns>Estimated cost to goal (h); 到终点的估计代价（h）</returns>
        public delegate int FuncHn(int cx, int cy, int cz, int ex, int ey, int ez);

        /// <summary>
        /// Calculation of movement cost with direction
        /// 方向移动代价计算
        /// </summary>
        /// <param name="dirX">Direction offset on X (-1, 0, or 1); X 方向偏移（-1、0 或 1）</param>
        /// <param name="dirY">Direction offset on Y; Y 方向偏移</param>
        /// <param name="dirZ">Direction offset on Z; Z 方向偏移</param>
        /// <returns>Step cost for this direction; 该方向的步进代价</returns>
        public delegate int FuncDn(int dirX, int dirY, int dirZ);
    }
}