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
        /// <summary>
        /// X coordinate.
        /// X 坐标。
        /// </summary>
        public int X;

        /// <summary>
        /// Y coordinate.
        /// Y 坐标。
        /// </summary>
        public int Y;

        /// <summary>
        /// Z coordinate.
        /// Z 坐标。
        /// </summary>
        public int Z;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{{X={X},Y={Y},Z={Z}}}";
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() << 4;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Point3Int && Equals((Point3Int) obj);
        }

        /// <inheritdoc />
        public bool Equals(Point3Int other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <summary>
        /// Component access by index: 0 = X, 1 = Y, 2 = Z.
        /// 按索引访问分量：0 为 X，1 为 Y，2 为 Z。
        /// </summary>
        /// <param name="index">Component index (0, 1, or 2).<br/>分量索引（0、1 或 2）。</param>
        /// <exception cref="IndexOutOfRangeException">Index is not 0, 1, or 2.<br/>索引不是 0、1 或 2。</exception>
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

        /// <summary>
        /// Creates a 3D integer point.
        /// 创建三维整型点。
        /// </summary>
        public Point3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Sets all three coordinates.
        /// 设置 X、Y、Z 坐标。
        /// </summary>
        public void Set(int newX, int newY, int newZ)
        {
            X = newX;
            Y = newY;
            Z = newZ;
        }

        /// <summary>
        /// Component-wise addition.
        /// 分量相加。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static Point3Int operator +(Point3Int b, Point3Int c)
        {
            return new Point3Int {X = b.X + c.X, Y = b.Y + c.Y, Z = b.Z + c.Z};
        }

        /// <summary>
        /// Component-wise subtraction.
        /// 分量相减。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static Point3Int operator -(Point3Int b, Point3Int c)
        {
            return new Point3Int {X = b.X - c.X, Y = b.Y - c.Y, Z = b.Z - c.Z};
        }

        /// <summary>
        /// Inequality comparison.
        /// 不等比较。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Point3Int b, Point3Int c)
        {
            return !b.Equals(c);
        }

        /// <summary>
        /// Equality comparison.
        /// 相等比较。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Point3Int b, Point3Int c)
        {
            return b.Equals(c);
        }

        /// <summary>
        /// Drops Z and returns the XY components as <see cref="Point2Int"/>.
        /// 丢弃 Z，将 XY 分量转为 <see cref="Point2Int"/>。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static implicit operator Point2Int(Point3Int v)
        {
            return new Point2Int {X = v.X, Y = v.Y};
        }
    }
}
