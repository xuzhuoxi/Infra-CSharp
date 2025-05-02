namespace JLGames.Infra.AStar
{
    public class AstarConst
    {
        public static readonly int DEFAULT_MASK_ADD = 1048576;

        public static readonly int MAX_MASK = 2000000000;

        /// <summary>
        /// Out of map
        /// 地图外
        /// </summary>
        public static readonly int GridOut = -1;

        /// <summary>
        /// None
        /// 无
        /// </summary>
        public static readonly int GridNone = 0;

        /// <summary>
        /// Path
        /// 通路
        /// </summary>
        public static readonly int GridPath = 1;

        /// <summary>
        /// Obstacle
        /// 障碍
        /// </summary>
        public static readonly int GridObstacle = 2;
    }

    public struct Size
    {
        public int Width, Height, Depth;

        public int Area => Width * Height * Depth;

        public bool Empty => (Width == 0 || Width == 0 || Depth == 0);
    }
}