namespace JLGames.Infra.AStar
{
    /// <summary>
    /// A* search constants and default cell values.
    /// A* 检索常量与默认格子取值
    /// </summary>
    public class AstarConst
    {
        /// <summary>
        /// Mask increment per search pass (avoids full map reset).
        /// 每次检索递增的 Mask 步长（避免整图重置）
        /// </summary>
        public static readonly int DEFAULT_MASK_ADD = 1048576;

        /// <summary>
        /// Upper bound for accumulated mask; triggers map reset when exceeded.
        /// Mask 累积上限，超出后触发地图重置
        /// </summary>
        public static readonly int MAX_MASK = 2000000000;

        /// <summary>
        /// Out of map.
        /// 地图外
        /// </summary>
        public static readonly int GridOut = -1;

        /// <summary>
        /// None.
        /// 无
        /// </summary>
        public static readonly int GridNone = 0;

        /// <summary>
        /// Walkable path cell.
        /// 可通行通路
        /// </summary>
        public static readonly int GridPath = 1;

        /// <summary>
        /// Obstacle.
        /// 障碍
        /// </summary>
        public static readonly int GridObstacle = 2;
    }

    /// <summary>
    /// Width, height, and depth of a grid or map region.
    /// 网格或地图区域的宽、高、深
    /// </summary>
    public struct Size
    {
        /// <summary>Width (X extent); 宽度（X 方向）</summary>
        public int Width, Height, Depth;

        /// <summary>Total cell count: Width × Height × Depth; 总格子数</summary>
        public int Area => Width * Height * Depth;

        /// <summary>True if any dimension is zero; 任一维为 0 时返回 true</summary>
        public bool Empty => (Width == 0 || Width == 0 || Depth == 0);
    }
}
