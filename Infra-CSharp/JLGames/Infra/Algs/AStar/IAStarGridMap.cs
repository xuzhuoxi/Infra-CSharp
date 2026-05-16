using System;

namespace JLGames.Infra.AStar
{
    /// <summary>
    /// Grid-based A* map facade: manages map data, cell size, and high-level path queries.
    /// 基于格子的 A* 地图门面：管理地图数据、格子尺寸及高层寻路查询
    /// </summary>
    public interface IAStarGridMap
    {
        /// <summary>
        /// Initialize map with logical data size; grid size defaults to 1×1×1.
        /// 以逻辑数据尺寸初始化地图，格子尺寸默认为 1×1×1
        /// </summary>
        /// <param name="dataSize">Cell count in each dimension; 各维格子数量</param>
        void InitGridMap(Size dataSize);

        /// <summary>
        /// Initialize map with logical data size and per-cell world size.
        /// 以逻辑数据尺寸与单格世界尺寸初始化地图
        /// </summary>
        /// <param name="dataSize">Cell count in each dimension; 各维格子数量</param>
        /// <param name="gridSize">World size of one cell; 单格世界尺寸</param>
        void InitGridMap(Size dataSize, Size gridSize);

        //----------------------------------------

        /// <summary>
        /// Set allowed expansion directions for pathfinding.
        /// 设置寻路时允许扩展的方向
        /// </summary>
        /// <param name="direction">Direction indices; 方向索引数组</param>
        void SetAllowedDirections(int[] direction);

        /// <summary>
        /// Set map data from a flat array.
        /// 以一维数组设置地图数据
        /// </summary>
        /// <param name="data">Length must equal width × height × depth; 长度须等于 width × height × depth</param>
        /// <returns>Null on success, or an exception describing the error; 成功返回 null，失败返回异常</returns>
        Exception SetMapData(int[] data);

        /// <summary>
        /// Set map data from a 2D array (depth = 1).
        /// 以二维数组设置地图数据（depth = 1）
        /// </summary>
        /// <param name="data">Length must equal width × height; 长度须等于 width × height</param>
        /// <returns>Null on success, or an exception describing the error; 成功返回 null，失败返回异常</returns>
        Exception SetMapData(int[][] data);

        /// <summary>
        /// Set map data from a 3D array [depth][height][width].
        /// 以三维数组 [depth][height][width] 设置地图数据
        /// </summary>
        /// <param name="data">Dimensions must match initialization; 尺寸须与初始化一致</param>
        /// <returns>Null on success, or an exception describing the error; 成功返回 null，失败返回异常</returns>
        Exception SetMapData(int[][][] data);

        /// <summary>
        /// Set custom step-cost and heuristic functions (either may be null to keep default).
        /// 设置自定义步进代价与启发函数（可传 null 保留默认）
        /// </summary>
        /// <param name="dn">Step-cost delegate; 步进代价委托</param>
        /// <param name="hn">Heuristic delegate; 启发函数委托</param>
        void SetCustomFunc(AStarDelegates.FuncDn dn, AStarDelegates.FuncHn hn);

        //----------------------------------------

        /// <summary>
        /// Get world size of one grid cell.
        /// 获取单格世界尺寸
        /// </summary>
        /// <returns>Grid size; 格子尺寸</returns>
        Size GetGridSize();

        /// <summary>
        /// Get logical map size in cells.
        /// 获取逻辑地图尺寸（格子数）
        /// </summary>
        /// <returns>Data size; 数据尺寸</returns>
        Size GetDataSize();

        /// <summary>
        /// Get total map size in world units (grid size × data size).
        /// 获取地图世界总尺寸（格子尺寸 × 数据尺寸）
        /// </summary>
        /// <returns>Pixel/world size; 像素/世界尺寸</returns>
        Size GetPixelSize();

        /// <summary>
        /// Get the underlying A* algorithm instance.
        /// 获取底层 A* 算法实例
        /// </summary>
        /// <returns>A* algorithm; A* 算法</returns>
        IAStarAlg GetAStartAlg();

        /// <summary>
        /// Get the cell value at the given position.
        /// 获取指定格子的数据值
        /// </summary>
        /// <param name="pos">Grid position; 格子坐标</param>
        /// <returns>Cell value, or <see cref="AstarConst.GridOut"/> if out of bounds; 格子值，越界时返回 GridOut</returns>
        int GetDataValue(Position pos);

        /// <summary>
        /// Check whether every point on the path is walkable.
        /// 检查路径上每一点是否均可通行
        /// </summary>
        /// <param name="path">Path to validate; 待校验路径</param>
        /// <returns>True if all cells are walkable; 全部可通行时返回 true</returns>
        bool CheckPath(Position[] path);

        /// <summary>
        /// Check whether start and end can be reached in a straight line without obstacles.
        /// 判断两点之间是否可直线通行（无遮挡）
        /// </summary>
        /// <param name="startPos">Start position; 起点</param>
        /// <param name="endPos">End position; 终点</param>
        /// <returns>True if a clear straight path exists; 存在无障碍直线时返回 true</returns>
        bool CanLineTo(Position startPos, Position endPos);

        /// <summary>
        /// Find a path between two positions; optionally removes collinear intermediate points.
        /// 在两点间寻路；可选是否保留共线中间点（拐点）
        /// </summary>
        /// <param name="startPos">Start position; 起点</param>
        /// <param name="endPos">End position; 终点</param>
        /// <param name="keepTurningPoint">If true, keep all turning points; if false, simplify collinear segments; 为 true 保留拐点，为 false 则简化共线段</param>
        /// <returns>Path array, or null if unreachable; 路径数组，不可达时返回 null</returns>
        Position[] SearchPath(Position startPos, Position endPos, bool keepTurningPoint = false);
    }
}