using System;

namespace JLGames.Infra.AStar
{
    public interface IAStarGridMap
    {
        /// <summary>
        /// Initialize map size
        /// 初始化地图尺寸 
        /// </summary>
        /// <param name="dataSize"></param>
        void InitGridMap(Size dataSize);

        /// <summary>
        /// Initialize map size
        /// 初始化地图尺寸
        /// </summary>
        /// <param name="dataSize"></param>
        /// <param name="gridSize"></param>
        void InitGridMap(Size dataSize, Size gridSize);

        //----------------------------------------

        // 设置允许寻路的方向
        void SetAllowedDirections(int[] direction);

        // 设置地图数据
        Exception SetMapData(int[] data);

        // 设置地图数据
        Exception SetMapData(int[][] data);

        // 设置地图数据
        Exception SetMapData(int[][][] data);

        /// <summary>
        /// 设置自定义估值函数
        /// </summary>
        /// <param name="dn"></param>
        /// <param name="hn"></param>
        void SetCustomFunc(AStarDelegates.FuncDn dn, AStarDelegates.FuncHn hn);

        //----------------------------------------

        // 格式大小
        Size GetGridSize();

        // 地图数据大小
        Size GetDataSize();

        // 地图像素大小
        Size GetPixelSize();

        // AStart算法
        IAStarAlg GetAStartAlg();

        // 格式数据值
        int GetDataValue(Position pos);

        // 判断路径是否通路
        bool CheckPath(Position[] path);

        // 判断是否两点直通
        bool CanLineTo(Position startPos, Position endPos);

        // 检索路径
        // 默认清除拐点
        Position[] SearchPath(Position startPos, Position endPos, bool keepTurningPoint = false);
    }
}