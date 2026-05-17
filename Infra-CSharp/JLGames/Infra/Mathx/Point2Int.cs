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

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{{X={X},Y={Y}}}";
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Point2Int && Equals((Point2Int) obj);
        }

        /// <inheritdoc />
        public bool Equals(Point2Int other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Component access by index: 0 = X, 1 = Y.
        /// 按索引访问分量：0 为 X，1 为 Y。
        /// </summary>
        /// <param name="index">Component index (0 or 1).<br/>分量索引（0 或 1）。</param>
        /// <exception cref="IndexOutOfRangeException">Index is not 0 or 1.<br/>索引不是 0 或 1。</exception>
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

        /// <summary>
        /// Creates a 2D integer point.
        /// 创建二维整型点。
        /// </summary>
        /// <param name="x">X coordinate.<br/>X 坐标。</param>
        /// <param name="y">Y coordinate.<br/>Y 坐标。</param>
        public Point2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Sets both coordinates.
        /// 设置 X、Y 坐标。
        /// </summary>
        public void Set(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        /// <summary>
        /// Origin (0, 0).
        /// 原点 (0, 0)。
        /// </summary>
        public static readonly Point2Int Zero = new Point2Int {X = 0, Y = 0};

        /// <summary>
        /// Component-wise addition.
        /// 分量相加。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static Point2Int operator +(Point2Int b, Point2Int c)
        {
            return new Point2Int {X = b.X + c.X, Y = b.Y + c.Y};
        }

        /// <summary>
        /// Component-wise subtraction.
        /// 分量相减。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static Point2Int operator -(Point2Int b, Point2Int c)
        {
            return new Point2Int {X = b.X - c.X, Y = b.Y - c.Y};
        }

        /// <summary>
        /// Inequality comparison.
        /// 不等比较。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Point2Int b, Point2Int c)
        {
            return !b.Equals(c);
        }

        /// <summary>
        /// Equality comparison.
        /// 相等比较。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Point2Int b, Point2Int c)
        {
            return b.Equals(c);
        }

        /// <summary>
        /// Promotes to <see cref="Point3Int"/> with Z = 0.
        /// 提升为 <see cref="Point3Int"/>（Z 为 0）。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static implicit operator Point3Int(Point2Int v)
        {
            return new Point3Int {X = v.X, Y = v.Y};
        }
    }
}
