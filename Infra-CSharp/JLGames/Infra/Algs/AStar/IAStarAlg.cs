namespace JLGames.Infra.AStar
{
    /// <summary>
    /// A* core algorithm interface
    /// A*核心算法接口
    /// </summary>
    public interface IAStarAlg
    {
        /// <summary>
        /// Initialize map size.
        /// 初始化地图尺寸
        /// </summary>
        /// <param name="width">Map width; 地图宽度</param>
        /// <param name="height">Map height; 地图高度</param>
        /// <param name="depth">Map depth; 地图深度</param>
        void InitMapSize(int width, int height, int depth);

        /// <summary>
        /// Initialize map size (2D, depth defaults to 1).
        /// 初始化地图尺寸（二维，深度默认为 1）
        /// </summary>
        /// <param name="width">Map width; 地图宽度</param>
        /// <param name="height">Map height; 地图高度</param>
        void InitMapSize(int width, int height);

        //----------------------------------------

        /// <summary>
        /// Directions that allows retrieval
        /// 取允许检索的方向
        /// </summary>
        /// <returns>Allowed direction indices; 允许的方向索引数组</returns>
        int[] AllowdDirections { get; }

        /// <summary>
        /// Set the allowed directions to retrieve, this can be used for special maps.
        /// 设置允许检索的方向(可用于特殊地图) 
        /// </summary>
        /// <param name="direction">Direction indices; 方向索引数组</param>
        void SetAllowedDirections(int[] direction);

        /// <summary>
        /// Set custom heuristic function (distance estimate from node to goal).
        /// 设置自定义启发函数（节点到终点的距离估值）
        /// </summary>
        /// <param name="hn">Heuristic delegate; 启发函数委托</param>
        void SetCustomFuncHn(AStarDelegates.FuncHn hn);

        /// <summary>
        /// Set custom step-cost function (cost along a direction).
        /// 设置自定义步进代价函数（沿某方向的移动代价）
        /// </summary>
        /// <param name="dn">Step-cost delegate; 步进代价委托</param>
        void SetCustomFuncDn(AStarDelegates.FuncDn dn);

        /// <summary>
        /// Set map data from a flat array.
        /// 以一维数组设置地图数据
        /// </summary>
        /// <param name="data">Length = width × height × depth; 长度 = width × height × depth</param>
        /// <returns>Copied map [depth][height][width], or null if invalid; 复制后的地图，无效时返回 null</returns>
        int[][][] SetData(int[] data);

        /// <summary>
        /// Set map data.
        /// 设置地图数据
        /// Note: depth=1
        /// 注意: depth=1
        /// </summary>
        /// <param name="data">Length = width × height (depth = 1); 长度 = width × height（depth = 1）</param>
        /// <returns>Copied map [depth][height][width], or null if invalid; 复制后的地图，无效时返回 null</returns>
        int[][][] SetData(int[][] data);

        /// <summary>
        /// Set map data from a 3D array.
        /// 以三维数组设置地图数据
        /// </summary>
        /// <param name="data">Layout [depth][height][width], dimensions must match init; 布局 [depth][height][width]，尺寸须与初始化一致</param>
        /// <returns>Copied map, or null if invalid; 复制后的地图，无效时返回 null</returns>
        int[][][] SetData(int[][][] data);

        //----------------------------------------

        /// <summary>
        /// 2D pathfinding.
        /// 二维寻路
        /// </summary>
        /// <param name="sx">Start point X; 起点 X</param>
        /// <param name="sy">Start point Y; 起点 Y</param>
        /// <param name="ex">End point X; 终点 X</param>
        /// <param name="ey">End point Y; 终点 Y</param>
        /// <returns>Path from start to end, or null if no path; 路径，无解时返回 null</returns>
        Position[] Search(int sx, int sy, int ex, int ey);

        /// <summary>
        /// 3D pathfinding.
        /// 三维寻路
        /// </summary>
        /// <param name="sx">Start point X; 起点 X</param>
        /// <param name="sy">Start point Y; 起点 Y</param>
        /// <param name="sz">Start point Z; 起点 Z</param>
        /// <param name="ex">End point X; 终点 X</param>
        /// <param name="ey">End point Y; 终点 Y</param>
        /// <param name="ez">End point Z; 终点 Z</param>
        /// <returns>Path from start to end, or null if no path; 路径，无解时返回 null</returns>
        Position[] Search(int sx, int sy, int sz, int ex, int ey, int ez);

        /// <summary>
        /// Pathfinding between two positions.
        /// 在两点之间寻路
        /// </summary>
        /// <param name="startPos">Start point; 起点</param>
        /// <param name="endPos">End point; 终点</param>
        /// <returns>Path from start to end, or null if no path; 路径，无解时返回 null</returns>
        Position[] SearchPosition(Position startPos, Position endPos);
    }
}