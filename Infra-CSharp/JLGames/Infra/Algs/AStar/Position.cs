using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace JLGames.Infra.AStar
{
    /// <summary>
    /// Grid position (X, Y, Z).
    /// 格子坐标（X, Y, Z）
    /// </summary>
    public struct Position : IEquatable<Position>
    {
        /// <summary>Grid X coordinate; 格子 X 坐标</summary>
        public int X, Y, Z;

        /// <summary>
        /// Add a direction offset to this position.
        /// 将方向偏移加到当前坐标
        /// </summary>
        /// <param name="vector">Direction offset; 方向偏移向量</param>
        /// <returns>New position; 新坐标</returns>
        public Position AddVector(DirectionVector vector)
        {
            return new Position {X = X + vector.OffsetX, Y = Y + vector.OffsetY, Z = Z + vector.OffsetZ};
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{X},{Y},{Z}]";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Position && Equals((Position) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() << 4;
        }

        /// <inheritdoc />
        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <summary>Equality operator; 相等比较</summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Position b, Position c)
        {
            return !b.Equals(c);
        }

        /// <summary>Equality operator; 相等比较</summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Position b, Position c)
        {
            return b.Equals(c);
        }

        /// <summary>Default empty position (0, 0, 0); 默认空坐标</summary>
        public static readonly Position Empty = new Position();
    }

    /// <summary>
    /// Grid position with A* priority (f-score).
    /// 带 A* 优先级（f 值）的格子坐标
    /// </summary>
    public struct PriorityPosition
    {
        /// <summary>Grid X coordinate; 格子 X 坐标</summary>
        public int X, Y, Z;

        /// <summary>Open-list priority (typically f = g + h); Open 表优先级（通常为 f = g + h）</summary>
        public int Priority;

        /// <summary>
        /// Compare coordinates and priority with another entry.
        /// 与另一项比较坐标与优先级是否完全相同
        /// </summary>
        /// <param name="pos">Other entry; 另一项</param>
        /// <returns>True if equal; 相等时返回 true</returns>
        public bool EqualTo(PriorityPosition pos)
        {
            return X == pos.X && Y == pos.Y && Z == pos.Z && Priority == pos.Priority;
        }

        /// <summary>
        /// Add a direction offset and return a plain position.
        /// 加上方向偏移并返回普通坐标
        /// </summary>
        /// <param name="vector">Direction offset; 方向偏移向量</param>
        /// <returns>New position; 新坐标</returns>
        public Position AddVector(DirectionVector vector)
        {
            return new Position {X = X + vector.OffsetX, Y = Y + vector.OffsetY, Z = Z + vector.OffsetZ};
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{X},{Y},{Z},{Priority}]";
        }

        /// <summary>Default empty entry; 默认空项</summary>
        public static readonly PriorityPosition Empty = new PriorityPosition();
    }

    /// <summary>
    /// Factory and formatting helpers for positions.
    /// 坐标构造与格式化辅助
    /// </summary>
    public static class Positions
    {
        /// <summary>
        /// Create a 2D position (Z = 0).
        /// 创建二维坐标（Z = 0）
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <returns>Position; 坐标</returns>
        public static Position NewPosition(int x, int y)
        {
            return new Position {X = x, Y = y, Z = 0};
        }

        /// <summary>
        /// Create a 3D position.
        /// 创建三维坐标
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <param name="z">Z coordinate; Z 坐标</param>
        /// <returns>Position; 坐标</returns>
        public static Position NewPosition(int x, int y, int z)
        {
            return new Position {X = x, Y = y, Z = z};
        }

        /// <summary>
        /// Create a 2D priority position (Z = 0).
        /// 创建二维优先级坐标（Z = 0）
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <param name="p">Priority value; 优先级</param>
        /// <returns>Priority position; 优先级坐标</returns>
        public static PriorityPosition NewPriorityPosition(int x, int y, int p)
        {
            return new PriorityPosition {X = x, Y = y, Z = 0, Priority = p};
        }

        /// <summary>
        /// Create a 3D priority position.
        /// 创建三维优先级坐标
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <param name="z">Z coordinate; Z 坐标</param>
        /// <param name="p">Priority value; 优先级</param>
        /// <returns>Priority position; 优先级坐标</returns>
        public static PriorityPosition NewPriorityPosition(int x, int y, int z, int p)
        {
            return new PriorityPosition {X = x, Y = y, Z = z, Priority = p};
        }

        /// <summary>
        /// Format an array of positions as a path string.
        /// 将坐标数组格式化为路径字符串
        /// </summary>
        /// <typeparam name="T">Position-like type with ToString; 具备 ToString 的坐标类型</typeparam>
        /// <param name="positions">Path points; 路径点数组</param>
        /// <returns>Formatted string; 格式化字符串</returns>
        public static string ToString<T>(T[] positions)
        {
            var sb = new StringBuilder();
            sb.Append("Path={");
            foreach (var pos in positions)
            {
                sb.Append($"{pos},");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }
    }
}
