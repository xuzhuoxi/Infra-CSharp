using System;
using System.Runtime.CompilerServices;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// 2D point（Integer）
    /// 二维点（整型）
    /// </summary>
    public struct Point2Int : IEquatable<Point2Int>
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return $"{{X={X},Y={Y}}}";
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2;
        }

        public override bool Equals(object obj)
        {
            return obj is Point2Int && Equals((Point2Int) obj);
        }

        public bool Equals(Point2Int other)
        {
            return X == other.X && Y == other.Y;
        }

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Point2 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Point2 index!");
                }
            }
        }

        public Point2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Set(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        public static readonly Point2Int Zero = new Point2Int {X = 0, Y = 0};

        [MethodImpl((MethodImplOptions) 256)]
        public static Point2Int operator +(Point2Int b, Point2Int c)
        {
            return new Point2Int {X = b.X + c.X, Y = b.Y + c.Y};
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static Point2Int operator -(Point2Int b, Point2Int c)
        {
            return new Point2Int {X = b.X - c.X, Y = b.Y - c.Y};
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Point2Int b, Point2Int c)
        {
            return !b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Point2Int b, Point2Int c)
        {
            return b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static implicit operator Point3Int(Point2Int v)
        {
            return new Point3Int {X = v.X, Y = v.Y};
        }
    }
}