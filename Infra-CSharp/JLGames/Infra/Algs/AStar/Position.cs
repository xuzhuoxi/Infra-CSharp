using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace JLGames.Infra.AStar
{
    /// <summary>
    /// point location information
    /// 点位置信息
    /// </summary>
    public struct Position : IEquatable<Position>
    {
        public int X, Y, Z;

        public Position AddVector(DirectionVector vector)
        {
            return new Position {X = X + vector.OffsetX, Y = Y + vector.OffsetY, Z = Z + vector.OffsetZ};
        }

        public override string ToString()
        {
            return $"[{X},{Y},{Z}]";
        }

        public override bool Equals(object obj)
        {
            return obj is Position && Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() << 4;
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Position b, Position c)
        {
            return !b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Position b, Position c)
        {
            return b.Equals(c);
        }


        public static readonly Position Empty = new Position();
    }

    /// <summary>
    /// Point locations with weights
    /// 带权值的点位置
    /// </summary>
    public struct PriorityPosition
    {
        public int X, Y, Z;
        public int Priority;

        public bool EqualTo(PriorityPosition pos)
        {
            return X == pos.X && Y == pos.Y && Z == pos.Z && Priority == pos.Priority;
        }

        public Position AddVector(DirectionVector vector)
        {
            return new Position {X = X + vector.OffsetX, Y = Y + vector.OffsetY, Z = Z + vector.OffsetZ};
        }

        public override string ToString()
        {
            return $"[{X},{Y},{Z},{Priority}]";
        }

        public static readonly PriorityPosition Empty = new PriorityPosition();
    }

    public static class Positions
    {
        public static Position NewPosition(int x, int y)
        {
            return new Position {X = x, Y = y, Z = 0};
        }

        public static Position NewPosition(int x, int y, int z)
        {
            return new Position {X = x, Y = y, Z = z};
        }

        public static PriorityPosition NewPriorityPosition(int x, int y, int p)
        {
            return new PriorityPosition {X = x, Y = y, Z = 0, Priority = p};
        }

        public static PriorityPosition NewPriorityPosition(int x, int y, int z, int p)
        {
            return new PriorityPosition {X = x, Y = y, Z = z, Priority = p};
        }

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