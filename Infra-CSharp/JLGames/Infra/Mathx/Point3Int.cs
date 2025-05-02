using System;
using System.Runtime.CompilerServices;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// 3D point（Integer）
    /// 三维点（整型）
    /// </summary>
    public struct Point3Int : IEquatable<Point3Int>
    {
        public int X;
        public int Y;
        public int Z;

        public override string ToString()
        {
            return $"{{X={X},Y={Y},Z={Z}}}";
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() << 4;
        }

        public override bool Equals(object obj)
        {
            return obj is Point3Int && Equals((Point3Int) obj);
        }

        public bool Equals(Point3Int other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
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
                    case 2:
                        return this.Z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Point3 index!");
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
                    case 2:
                        this.Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Point3 index!");
                }
            }
        }

        public Point3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Set(int newX, int newY, int newZ)
        {
            X = newX;
            Y = newY;
            Z = newZ;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static Point3Int operator +(Point3Int b, Point3Int c)
        {
            return new Point3Int {X = b.X + c.X, Y = b.Y + c.Y, Z = b.Z + c.Z};
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static Point3Int operator -(Point3Int b, Point3Int c)
        {
            return new Point3Int {X = b.X - c.X, Y = b.Y - c.Y, Z = b.Z - c.Z};
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Point3Int b, Point3Int c)
        {
            return !b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Point3Int b, Point3Int c)
        {
            return b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static implicit operator Point2Int(Point3Int v)
        {
            return new Point2Int {X = v.X, Y = v.Y};
        }
    }
}