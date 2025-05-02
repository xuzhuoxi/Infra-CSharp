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
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="cz"></param>
        /// <param name="ex"></param>
        /// <param name="ey"></param>
        /// <param name="ez"></param>
        public delegate int FuncHn(int cx, int cy, int cz, int ex, int ey, int ez);

        /// <summary>
        /// Calculation of movement cost with direction
        /// 方向移动代价计算
        /// </summary>
        /// <param name="dirX">0/1</param>
        /// <param name="dirY">0/1</param>
        /// <param name="dirZ">0/1</param>
        public delegate int FuncDn(int dirX, int dirY, int dirZ);
    }
}