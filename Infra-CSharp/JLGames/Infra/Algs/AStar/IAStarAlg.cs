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
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        void InitMapSize(int width, int height, int depth);

        /// <summary>
        /// Initialize map size.
        /// 初始化地图尺寸
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void InitMapSize(int width, int height);

        //----------------------------------------

        /// <summary>
        /// Directions that allows retrieval
        /// 取允许检索的方向
        /// </summary>
        /// <returns></returns>
        int[] AllowdDirections { get; }

        /// <summary>
        /// Set the allowed directions to retrieve, this can be used for special maps.
        /// 设置允许检索的方向(可用于特殊地图) 
        /// </summary>
        /// <param name="direction"></param>
        void SetAllowedDirections(int[] direction);

        /// <summary>
        /// Set custom distance estimation function
        /// 设置自定义的距离估值函数
        /// </summary>
        /// <param name="hn"></param>
        void SetCustomFuncHn(AStarDelegates.FuncHn hn);

        /// <summary>
        /// Set custom direction evaluation function
        /// 设置自定义的方向估值函数
        /// </summary>
        /// <param name="dn"></param>
        void SetCustomFuncDn(AStarDelegates.FuncDn dn);

        /// <summary>\
        /// Set map data.
        /// 设置地图数据
        /// </summary>
        /// <param name="data">长度=地图的width*height*depth</param>
        /// <returns></returns>
        int[][][] SetData(int[] data);

        /// <summary>
        /// Set map data.
        /// 设置地图数据
        /// Note: depth=1
        /// 注意: depth=1
        /// </summary>
        /// <param name="data">长度=地图的width*height</param>
        /// <returns></returns>
        int[][][] SetData(int[][] data);

        /// <summary>
        /// Set map data.
        /// 设置地图数据
        /// </summary>
        /// <param name="data">int[depth][height][width]对应长度与初始化时一致</param>
        /// <returns></returns>
        int[][][] SetData(int[][][] data);

        //----------------------------------------

        /// <summary>
        /// 2D pathfinding.
        /// 二维寻路
        /// </summary>
        /// <param name="sx">Start Ppint X</param>
        /// <param name="sy">Start Point Y</param>
        /// <param name="ex">End Point X</param>
        /// <param name="ey">End Point Y</param>
        /// <returns></returns>
        Position[] Search(int sx, int sy, int ex, int ey);

        /// <summary>
        /// 3D pathfinding.
        /// 三维寻路
        /// sx:StartX; sy:StartY; sz:StartZ
        /// ex:EndX; ey:EndY; ez:EndZ
        /// </summary>
        /// <param name="sx">Start Ppint X</param>
        /// <param name="sy">Start Point Y</param>
        /// <param name="sz">Start Point Z</param>
        /// <param name="ex">End Point X</param>
        /// <param name="ey">End Point Y</param>
        /// <param name="ez">End Point Z</param>
        /// <returns></returns>
        Position[] Search(int sx, int sy, int sz, int ex, int ey, int ez);

        /// <summary>
        /// 寻路
        /// </summary>
        /// <param name="startPos">Start Point; 开始点</param>
        /// <param name="endPos">End Point: 结束点</param>
        /// <returns></returns>
        Position[] SearchPosition(Position startPos, Position endPos);
    }
}