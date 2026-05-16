using System;
using System.Collections.Generic;

namespace JLGames.Infra.AStar
{
    /// <summary>
    /// Pathfinding History
    /// 寻路历史记录
    /// </summary>
    internal struct History
    {
        internal Position m_StartPos, m_EndPos;
        internal Position[] m_Path;
        internal bool m_PathOk;

        /// <summary>
        /// Extract a sub-path by index range (inclusive).
        /// 按索引范围截取子路径（含首尾）
        /// </summary>
        /// <param name="sIndex">Start index; 起始索引</param>
        /// <param name="eIndex">End index; 结束索引</param>
        /// <returns>Sub-path array, null if no path, or empty if path is empty; 子路径，无路径时为 null，空路径时为空数组</returns>
        public Position[] GetSubPath(int sIndex, int eIndex)
        {
            if (null == m_Path)
            {
                return null;
            }

            if (0 == m_Path.Length)
            {
                return Array.Empty<Position>();
            }

            var ln = eIndex - sIndex + 1;

            var rs = new Position[ln];
            Array.Copy(m_Path, sIndex, rs, 0, ln);
            return rs;
        }

        /// <summary>
        /// Check whether start and end both lie on the cached path and start precedes end.
        /// 检查起终点是否均在缓存路径上，且起点索引不大于终点索引
        /// </summary>
        /// <param name="startPos">Start position; 起点</param>
        /// <param name="endPos">End position; 终点</param>
        /// <returns>True if a valid sub-path exists; 存在有效子路径时返回 true</returns>
        public bool CheckSubPath(Position startPos, Position endPos)
        {
            if (!m_PathOk)
            {
                return false;
            }

            var sIndex = GetFirstPositionIndex(startPos);
            var eIndex = GetLastPositionIndex(endPos);
            if (-1 == sIndex || -1 == eIndex)
            {
                return false;
            }

            if (sIndex > eIndex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the last index of the given position in the cached path.
        /// 获取指定坐标在缓存路径中最后一次出现的索引
        /// </summary>
        /// <param name="pos">Position to find; 待查找坐标</param>
        /// <returns>Index, or -1 if not found; 索引，未找到返回 -1</returns>
        public int GetLastPositionIndex(Position pos)
        {
            for (var i = m_Path.Length - 1; i >= 0; i--)
            {
                if (m_Path[i].Equals(pos))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get the first index of the given position in the cached path.
        /// 获取指定坐标在缓存路径中第一次出现的索引
        /// </summary>
        /// <param name="pos">Position to find; 待查找坐标</param>
        /// <returns>Index, or -1 if not found; 索引，未找到返回 -1</returns>
        public int GetFirstPositionIndex(Position pos)
        {
            for (var i = 0; i < m_Path.Length; i++)
            {
                if (m_Path[i].Equals(pos))
                {
                    return i;
                }
            }

            return -1;
        }
    }

    /// <summary>
    /// A* pathfinding algorithm implementation.
    /// A* 寻路算法实现
    /// </summary>
    public class AStarAlg : IAStarAlg
    {
        private Size m_Size; // 地图的大小
        private int[] m_NextDirs; //可行方向
        private AStarDelegates.FuncHn m_CustomFuncHn; //自定义距离估值函数
        private AStarDelegates.FuncDn m_CustomFuncDn; //自定义方向估值函数
        private int[][][] m_SourceMap; //初始地图数据
        private int[][][] m_SearchMap; //检索地图数据

        private int m_BaseMask;
        private int m_MaxMask;
        private Position m_StartPos, m_EndPos;
        private History m_History;
        private readonly PriorityPositionQueue m_OpenQueue = new PriorityPositionQueue();

        /// <summary>
        /// Initialize map size.
        /// 初始化地图尺寸
        /// </summary>
        /// <param name="width">Map width; 地图宽度</param>
        /// <param name="height">Map height; 地图高度</param>
        /// <param name="depth">Map depth; 地图深度</param>
        public void InitMapSize(int width, int height, int depth)
        {
            m_Size = new Size {Width = width, Height = height, Depth = depth};
        }

        /// <summary>
        /// Initialize map size (2D, depth defaults to 1).
        /// 初始化地图尺寸（二维，深度默认为 1）
        /// </summary>
        /// <param name="width">Map width; 地图宽度</param>
        /// <param name="height">Map height; 地图高度</param>
        public void InitMapSize(int width, int height)
        {
            m_Size = new Size {Width = width, Height = height, Depth = 1};
        }

        //----------------------------------------

        /// <summary>
        /// Set map data from a flat array.
        /// 以一维数组设置地图数据
        /// </summary>
        /// <param name="data">Length must equal width * height * depth; 长度须等于 width * height * depth</param>
        /// <returns>Copied map data [depth][height][width], or null if invalid; 复制后的地图数据，无效时返回 null</returns>
        public int[][][] SetData(int[] data)
        {
            if (null == data || data.Length != m_Size.Area)
            {
                return null;
            }

            m_SourceMap = InnerCopyData(data);
            InnerResetSearchMap();
            return m_SourceMap;
        }

        /// <summary>
        /// Set map data from a 2D array (depth = 1).
        /// 以二维数组设置地图数据（depth = 1）
        /// </summary>
        /// <param name="data">Length must equal width * height; 长度须等于 width * height</param>
        /// <returns>Copied map data [depth][height][width], or null if invalid; 复制后的地图数据，无效时返回 null</returns>
        public int[][][] SetData(int[][] data)
        {
            if (null == data || data.Length == 0 || null == data[0] || data[0].Length == 0 ||
                data.Length * data[0].Length != m_Size.Area)
            {
                return null;
            }

            m_SourceMap = InnerCopyData(data);
            InnerResetSearchMap();
            return m_SourceMap;
        }

        /// <summary>
        /// Set map data from a 3D array.
        /// 以三维数组设置地图数据
        /// </summary>
        /// <param name="data">Layout [depth][height][width], dimensions must match initialization; 布局为 [depth][height][width]，尺寸须与初始化一致</param>
        /// <returns>Copied map data, or null if invalid; 复制后的地图数据，无效时返回 null</returns>
        public int[][][] SetData(int[][][] data)
        {
            if (null == data || data.Length == 0 ||
                null == data[0] || data[0].Length == 0 ||
                null == data[0][0] || data[0][0].Length == 0 ||
                data.Length * data[0].Length * data[0][0].Length != m_Size.Area)
            {
                return null;
            }

            m_SourceMap = InnerCopyData(data);
            InnerResetSearchMap();
            return m_SourceMap;
        }

        /// <summary>
        /// Directions allowed for path expansion.
        /// 允许检索/扩展的方向集合
        /// </summary>
        public int[] AllowdDirections => m_NextDirs;

        /// <summary>
        /// Set the allowed directions for path expansion (e.g. for special maps).
        /// 设置允许检索的方向（可用于特殊地图）
        /// </summary>
        /// <param name="direction">Direction indices; 方向索引数组</param>
        public void SetAllowedDirections(int[] direction)
        {
            m_NextDirs = new int[direction.Length];
            Array.Copy(direction, m_NextDirs, direction.Length);
        }

        /// <summary>
        /// Set custom heuristic function (distance estimate from node to goal).
        /// 设置自定义启发函数（节点到终点的距离估值）
        /// </summary>
        /// <param name="hn">Heuristic delegate; 启发函数委托</param>
        public void SetCustomFuncHn(AStarDelegates.FuncHn hn)
        {
            m_CustomFuncHn = hn;
        }

        /// <summary>
        /// Set custom step-cost function (cost from current node along a direction).
        /// 设置自定义步进代价函数（沿某方向从当前节点移动的代价）
        /// </summary>
        /// <param name="dn">Step-cost delegate; 步进代价委托</param>
        public void SetCustomFuncDn(AStarDelegates.FuncDn dn)
        {
            m_CustomFuncDn = dn;
        }

        //----------------------------------------

        /// <summary>
        /// 2D pathfinding.
        /// 二维寻路
        /// </summary>
        /// <param name="sx">Start point X; 起点 X</param>
        /// <param name="sy">Start point Y; 起点 Y</param>
        /// <param name="ex">End point X; 终点 X</param>
        /// <param name="ey">End point Y; 终点 Y</param>
        /// <returns>Path from start to end, or null if no path; 起点到终点的路径，无解时返回 null</returns>
        public Position[] Search(int sx, int sy, int ex, int ey)
        {
            return SearchPosition(Positions.NewPosition(sx, sy), Positions.NewPosition(ex, ey));
        }

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
        /// <returns>Path from start to end, or null if no path; 起点到终点的路径，无解时返回 null</returns>
        public Position[] Search(int sx, int sy, int sz, int ex, int ey, int ez)
        {
            return SearchPosition(Positions.NewPosition(sx, sy, sz), Positions.NewPosition(ex, ey, ez));
        }

        /// <summary>
        /// Pathfinding between two positions. Reuses a cached sub-path when the request lies on the last successful path.
        /// 在两点之间寻路；若起终点均落在上次成功路径上，则直接返回对应子路径
        /// </summary>
        /// <param name="startPos">Start point; 起点</param>
        /// <param name="endPos">End point; 终点</param>
        /// <returns>Path from start to end, or null if no path; 起点到终点的路径，无解时返回 null</returns>
        public Position[] SearchPosition(Position startPos, Position endPos)
        {
            if (m_History.CheckSubPath(startPos, endPos))
            {
                var sIndex = m_History.GetFirstPositionIndex(startPos);
                var eIndex = m_History.GetLastPositionIndex(endPos);
                return m_History.GetSubPath(sIndex, eIndex);
            }

            m_StartPos = startPos;
            m_EndPos = endPos;
            InnerInitPath();

            if (!InnerSearchPath())
            {
                return null;
            }

            var path = InnerGenPath();
            m_History.m_StartPos = startPos;
            m_History.m_EndPos = endPos;
            m_History.m_Path = path;
            m_History.m_PathOk = true;
            return path;
        }

        //-------------------------------------------------------

        private void InnerInitPath()
        {
            // By increasing the Mask value, the effect of searching the map without resetting is achieved.
            // 通过幅度提高Mask值，达到不用重置检索地图的效果
            m_BaseMask += AstarConst.DEFAULT_MASK_ADD;
            // The highest mask value currently available
            // 当前可使用的最高Mask值
            m_MaxMask = m_BaseMask + AstarConst.DEFAULT_MASK_ADD;
            // Empty the open queue
            // 清空open队列
            m_OpenQueue.Clear();
            if (m_BaseMask > AstarConst.MAX_MASK)
            {
                m_BaseMask = AstarConst.DEFAULT_MASK_ADD;
                m_MaxMask = m_BaseMask + AstarConst.DEFAULT_MASK_ADD;
                InnerResetSearchMap();
            }
        }

        /// <summary>
        /// Search path.
        /// 寻路
        /// 1. Execute process modification to retrieve map data
        /// 1. 执行过程修改检索地图数据
        /// 2. Map paths are marked by data values
        /// 2. 通过数据值来标记地图路径
        /// 3. Retrieve from start point to target point
        /// 3. 从开始点向目标点检索
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private bool InnerSearchPath()
        {
            if (InnerOutOfMap(m_StartPos) || InnerOutOfMap(m_EndPos))
            {
                return false;
            }

            // 以开始点为初始点搜索
            var start = m_StartPos;
            var end = m_EndPos;

            m_SearchMap[start.Z][start.Y][start.X] = m_BaseMask;
            m_OpenQueue.Push(start.X, start.Y, start.Z, m_BaseMask);

            while (m_OpenQueue.Len > 0)
            {
                var info = m_OpenQueue.Shift();
                var pos = info.Position;
                foreach (var n in m_NextDirs)
                {
                    var nextVector = DirectionsStatic.GetVector(n);
                    var nextPos = pos.AddVector(nextVector);
                    // 如果下一个超出地图范围
                    if (InnerOutOfMap(nextPos))
                    {
                        continue;
                    }

                    var adjX = nextPos.X;
                    var adjY = nextPos.Y;
                    var adjZ = nextPos.Z;
                    // 如果下一个是 不可走区域 或 内部地图边界
                    if (m_SearchMap[adjZ][adjY][adjX] == AstarConst.GridObstacle ||
                        m_SearchMap[adjZ][adjY][adjX] == AstarConst.GridOut)
                    {
                        continue;
                    }

                    int gn; //当前点到下一点的实际代价
                    if (null != m_CustomFuncDn)
                    {
                        gn = m_SearchMap[pos.Z][pos.Y][pos.X] +
                             m_CustomFuncDn(nextVector.OffsetX, nextVector.OffsetY,
                                 nextVector.OffsetZ); //当前代价 + 方向代价 + 地形代价
                    }
                    else
                    {
                        gn = m_SearchMap[pos.Z][pos.Y][pos.X] + nextVector.OffsetV; //当前代价 + 方向代价 + 地形代价
                    }

                    //fmt.Println("AStarAlg.searchPath:下一个路径点：", adjX, adjY, gn, nextDir)
                    // 已走过的区域
                    if (m_SearchMap[adjZ][adjY][adjX] >= m_BaseMask)
                    {
                        if (gn < m_SearchMap[adjZ][adjY][adjX])
                        {
                            //实际代价更小，路径更优
                            m_SearchMap[adjZ][adjY][adjX] = gn;
                        }

                        continue;
                    }

                    if (gn >= m_MaxMask)
                    {
                        throw new Exception("Mask Error: gn >= maxMask");
                    }

                    m_SearchMap[adjZ][adjY][adjX] = gn;
                    if (adjX == end.X && adjY == end.Y)
                    {
                        //找到目标点
                        //fmt.Println("AStarAlg.searchPath:检索结束！成功", adjX, adjY, gn)
                        return true;
                    }

                    int hn; //下一点到终点的估算代价
                    if (null != m_CustomFuncHn)
                    {
                        hn = m_CustomFuncHn(adjX, adjY, adjZ, end.X, end.Y, end.Z);
                    }
                    else
                    {
                        hn = DefaultFuncHn(adjX, adjY, adjZ, end.X, end.Y, end.Z);
                    }

                    var fn = gn + hn + m_SourceMap[adjZ][adjY][adjX]; //总的启发函数：fn = gn + hn + nextValue
                    m_OpenQueue.Push(adjX, adjY, adjZ, fn);
                    //fmt.Println("AStarAlg.searchPath:\t加入路径点：", adjX, adjY, fn)
                }

                //fmt.Println("AStarAlg.searchPath:余下长度：", openQueue.Len())
            }

            //fmt.Println("AStarAlg.searchPath:检索结束！失败")
            return false;
        }

        /// <summary>
        /// Generate paths for successful search
        /// 生成搜索成功的路径
        /// Take the path from the target point to the start point in a small value manner
        /// 从目标点向开始点以取小值方式取出路径
        /// </summary>
        /// <returns></returns>
        private Position[] InnerGenPath()
        {
            var walk = new List<Position> {m_EndPos};
            var currPos = m_EndPos;
            var minGnVal = m_SearchMap[currPos.Z][currPos.Y][currPos.X];
            while (!currPos.Equals(m_StartPos))
            {
                //fmt.Println("for 0", pos, minGnVal)
                Position? nextDirPos = null;
                foreach (var n in m_NextDirs)
                {
                    var nextDirVector = DirectionsStatic.GetVector(n);
                    var adjPos = currPos.AddVector(nextDirVector);
                    // 如果下一个超出地图范围
                    if (InnerOutOfMap(adjPos))
                    {
                        continue;
                    }

                    //到达目标点
                    if (m_StartPos.Equals(adjPos))
                    {
                        nextDirPos = adjPos;
                        break;
                    }

                    var adjX = adjPos.X;
                    var adjY = adjPos.Y;
                    var adjZ = adjPos.Z;
                    //fmt.Println("for 1", nextPos, nextDir, alg.searchMap[adjY][adjX], alg.baseMask)
                    // 如果不是检索过的地方
                    if (m_SearchMap[adjZ][adjY][adjX] < m_BaseMask)
                    {
                        continue;
                    }

                    //fmt.Println("比较：", alg.searchMap[adjY][adjX], minGnVal)
                    if (m_SearchMap[adjZ][adjY][adjX] < minGnVal)
                    {
                        minGnVal = m_SearchMap[adjZ][adjY][adjX];
                        nextDirPos = adjPos;
                    }
                }

                if (null == nextDirPos)
                {
                    continue;
                }

                walk.Add(nextDirPos.Value);
                currPos = nextDirPos.Value;
            }

            var sIndex = 0;
            var eIndex = walk.Count - 1;
            Position temp;
            for (; sIndex < eIndex;)
            {
                //反序
                temp = walk[sIndex];
                walk[sIndex] = walk[eIndex];
                walk[eIndex] = temp;
                sIndex++;
                eIndex--;
            }

            //fmt.Println("AStarAlg.genPath:路径生成！", walk)
            return walk.ToArray();
        }

        private int DefaultFuncHn(int cx, int cy, int cz, int ex, int ey, int ez)
        {
            return Math.Abs(cx - ex) + Math.Abs(cy - ey) + Math.Abs(cz - ez);
        }

        private bool InnerOutOfMap(Position pos)
        {
            return InnerOutOfMap(pos.X, pos.Y, pos.Z);
        }

        private bool InnerOutOfMap(int x, int y, int z)
        {
            return x < 0 || y < 0 || z < 0 || x >= m_Size.Width || y >= m_Size.Height || z >= m_Size.Depth;
        }

        /// <summary>
        /// Copy data.
        /// 克隆数据
        /// 要求Area相等
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private int[][][] InnerCopyData(int[] data)
        {
            var rsz = new int[m_Size.Depth][][];
            for (var z = 0; z < m_Size.Depth; z++)
            {
                var rsy = new int[m_Size.Height][];
                var startIndexZ = z * m_Size.Height * m_Size.Width;
                for (var y = 0; y < m_Size.Height; y++)
                {
                    var rsx = new int[m_Size.Width];
                    var startIndexY = startIndexZ + y * m_Size.Width;
                    Array.Copy(data, startIndexY, rsx, 0, m_Size.Width);
                    rsy[y] = rsx;
                }

                rsz[z] = rsy;
            }

            return rsz;
        }

        /// <summary>
        /// Copy data.
        /// 克隆数据
        /// 要求Depth=1,Width,Height一一对应
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private int[][][] InnerCopyData(int[][] data)
        {
            return InnerCopyData(new[] {data});
        }

        /// <summary>
        /// Copy data.
        /// 克隆数据
        /// 要求Width,Height,Depth一一对应
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private int[][][] InnerCopyData(int[][][] data)
        {
            var rsz = new int[m_Size.Depth][][];
            for (var z = 0; z < m_Size.Depth; z++)
            {
                var rsy = new int[m_Size.Height][];
//			    var startIndexZ = z * size.Depth;
                for (var y = 0; y < m_Size.Height; y++)
                {
                    var rsx = new int[m_Size.Width];
                    Array.Copy(data[z][y], rsx, m_Size.Width);
                    rsy[y] = rsx;
                }

                rsz[z] = rsy;
            }

            return rsz;
        }

        /// <summary>
        /// reset search map
        /// 重置检索地图
        /// </summary>
        private void InnerResetSearchMap()
        {
            m_SearchMap = InnerCopyData(m_SourceMap);
        }

        private bool InnerContainsDirection(int[] directions, int dir)
        {
            foreach (var d in directions)
            {
                if (d == dir)
                {
                    return true;
                }
            }

            return false;
        }
    }
}