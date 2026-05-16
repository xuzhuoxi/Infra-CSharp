using System;

namespace JLGames.Infra.AStar
{
    /// <summary>
    /// Grid-based A* map implementation.
    /// 基于格子的 A* 地图实现
    /// </summary>
    public class AStarGridMap : IAStarGridMap
    {
        private Size m_DataSize;
        private Size m_GridSize;

        private int[][][] m_MadData;
        private IAStarAlg m_AStarAlg;

        /// <inheritdoc />
        public void InitGridMap(Size dataSize)
        {
            if (dataSize.Empty)
            {
                throw new Exception("GridMap Empty! ");
            }

            InitGridMap(dataSize, new Size {Width = 1, Height = 1, Depth = 1});
        }

        /// <inheritdoc />
        public void InitGridMap(Size dataSize, Size gridSize)
        {
            if (dataSize.Empty || gridSize.Empty)
            {
                throw new Exception("GridMap Empty! ");
            }

            m_DataSize = dataSize;
            m_GridSize = gridSize;
            m_AStarAlg = new AStarAlg();
            m_AStarAlg.InitMapSize(dataSize.Width, dataSize.Height, dataSize.Depth);
        }

        /// <inheritdoc />
        public Exception SetMapData(int[] data)
        {
            var sourece = m_AStarAlg.SetData(data);
            if (null == sourece)
            {
                return new Exception("Data Error!");
            }

            m_MadData = sourece;
            return null;
        }

        /// <inheritdoc />
        public Exception SetMapData(int[][] data)
        {
            var sourece = m_AStarAlg.SetData(data);
            if (null == sourece)
            {
                return new Exception("Data Error!");
            }

            m_MadData = sourece;
            return null;
        }

        /// <inheritdoc />
        public Exception SetMapData(int[][][] data)
        {
            var sourece = m_AStarAlg.SetData(data);
            if (null == sourece)
            {
                return new Exception("Data Error!");
            }

            m_MadData = sourece;
            return null;
        }

        /// <inheritdoc />
        public void SetAllowedDirections(int[] direction)
        {
            m_AStarAlg.SetAllowedDirections(direction);
        }

        /// <inheritdoc />
        public void SetCustomFunc(AStarDelegates.FuncDn dn, AStarDelegates.FuncHn hn)
        {
            if (null != dn)
            {
                m_AStarAlg.SetCustomFuncDn(dn);
            }

            if (null != hn)
            {
                m_AStarAlg.SetCustomFuncHn(hn);
            }
        }

        //------------------------

        /// <inheritdoc />
        public Size GetGridSize()
        {
            return m_GridSize;
        }

        /// <inheritdoc />
        public Size GetDataSize()
        {
            return m_DataSize;
        }

        /// <inheritdoc />
        public Size GetPixelSize()
        {
            return new Size
            {
                Width = m_GridSize.Width * m_DataSize.Width,
                Height = m_GridSize.Height * m_DataSize.Height,
                Depth = 1
            };
        }

        /// <inheritdoc />
        public IAStarAlg GetAStartAlg()
        {
            return m_AStarAlg;
        }

        /// <inheritdoc />
        public int GetDataValue(Position pos)
        {
            return InnerGetDataValue(pos);
        }

        /// <inheritdoc />
        public bool CheckPath(Position[] path)
        {
            if (null == path || path.Length == 0)
            {
                return false;
            }

            foreach (var pos in path)
            {
                if (!InnerIsPathGrid(pos))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public bool CanLineTo(Position startPos, Position endPos)
        {
            return InnerCanLineTo(startPos, endPos);
        }


        /// <inheritdoc />
        public Position[] SearchPath(Position startPos, Position endPos, bool keepTurningPoint = false)
        {
            return InnerSearchPath(startPos, endPos, keepTurningPoint);
        }

        //------------------------

        private Position[] InnerSearchPath(Position startPos, Position endPos, bool keepInflection)
        {
            // in a non-walkable area
            // 处于不可行走区域
            if (!InnerIsPathGrid(startPos) || !InnerIsPathGrid(endPos)) //
            {
                return null;
            }

            // The start point is the same as the end point
            // 起始点与终点相同
            if (startPos.Equals(endPos))
            {
                return new[] {startPos};
            }

            // One-way straight line possible
            // 单向直线可行
            if (InnerCanLineTo(startPos, endPos))
            {
                return new[] {startPos, endPos};
            }

            var path = m_AStarAlg.SearchPosition(startPos, endPos);
            // There is an inflection point and it needs to be cleared.
            // Clearing the inflection point does not require additional restrictions on the direction.
            // 有拐点并要求清除，这里清除拐点不用额外限制方向
            if (null != path && path.Length > 3 && !keepInflection)
            {
                return AStarUtil.ClearRedundancies(path);
            }

            return path;
        }

        /// <summary>
        /// Can you walk in a straight line
        /// 是否可以直线行走
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        private bool InnerCanLineTo(Position startPos, Position endPos)
        {
            if (!AStarUtil.IsInStandardLine(startPos, endPos, true))
            {
                return false;
            }

            var addX = InnerGetAddDiff(startPos.X, endPos.X);
            var addY = InnerGetAddDiff(startPos.Y, endPos.Y);
            var addZ = InnerGetAddDiff(startPos.Z, endPos.Z);

            var temp = startPos;

            for (; !temp.Equals(endPos);)
            {
                if (!InnerIsPathGrid(temp))
                {
                    return false;
                }

                temp.X += addX;
                temp.Y += addY;
                temp.Z += addZ;
            }

            return true;
        }

        private int InnerGetAddDiff(int start, int end)
        {
            var diff = end - start;
            if (0 == diff)
            {
                return 0;
            }

            return diff > 0 ? 1 : -1;
        }

        /// <summary>
        /// Whether the grid is walkable
        /// 格子是否为可走
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool InnerIsPathGrid(Position pos)
        {
            var val = InnerGetDataValue(pos);
            return val != AstarConst.GridOut && val != AstarConst.GridObstacle;
        }

        /// <summary>
        /// Get data value
        /// 取格式数据值
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private int InnerGetDataValue(Position pos)
        {
            if (pos.X < 0 || pos.X >= m_DataSize.Width || pos.Y < 0 || pos.Y >= m_DataSize.Height || pos.Z < 0 ||
                pos.Z >= m_DataSize.Height)
            {
                return AstarConst.GridOut;
            }

            return m_MadData[pos.Z][pos.Y][pos.X];
        }
    }
}