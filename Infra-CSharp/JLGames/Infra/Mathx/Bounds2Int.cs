using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// Axis-aligned 2D integer bounds [XMin, XMax) × [YMin, YMax) (half-open).
    /// 轴对齐二维整型边界 [XMin, XMax) × [YMin, YMax)（半开区间）。
    /// </summary>
    public struct Bounds2Int : IEquatable<Bounds2Int>
    {
        private Point2Int m_Min;
        private Point2Int m_Max;

        /// <summary>
        /// Minimum X (inclusive).
        /// 最小 X（含）。
        /// </summary>
        public int XMin
        {
            get { return m_Min.X; }
            set { m_Min.X = value; }
        }

        /// <summary>
        /// Minimum Y (inclusive).
        /// 最小 Y（含）。
        /// </summary>
        public int YMin
        {
            get { return m_Min.Y; }
            set { m_Min.Y = value; }
        }

        /// <summary>
        /// Maximum X (exclusive).
        /// 最大 X（不含）。
        /// </summary>
        public int XMax
        {
            get { return m_Max.X; }
            set { m_Max.X = value; }
        }

        /// <summary>
        /// Maximum Y (exclusive).
        /// 最大 Y（不含）。
        /// </summary>
        public int YMax
        {
            get { return m_Max.Y; }
            set { m_Max.Y = value; }
        }

        /// <summary>
        /// Width (XMax − XMin).
        /// 宽度（XMax − XMin）。
        /// </summary>
        public int XSize
        {
            get { return XMax - XMin; }
            set { XMax = XMin + value; }
        }

        /// <summary>
        /// Height (YMax − YMin).
        /// 高度（YMax − YMin）。
        /// </summary>
        public int YSize
        {
            get { return YMax - YMin; }
            set { YMax = YMin + value; }
        }

        /// <summary>
        /// Minimum corner (inclusive).
        /// 最小角点（含）。
        /// </summary>
        public Point2Int Min
        {
            get { return m_Min; }
            set { m_Min = value; }
        }

        /// <summary>
        /// Maximum corner (exclusive).
        /// 最大角点（不含）。
        /// </summary>
        public Point2Int Max
        {
            get { return m_Max; }
            set { m_Max = value; }
        }

        /// <summary>
        /// Center X coordinate.
        /// 中心 X 坐标。
        /// </summary>
        public int XCenter => (XMax + XMin) / 2;

        /// <summary>
        /// Center Y coordinate.
        /// 中心 Y 坐标。
        /// </summary>
        public int YCenter => (YMax + YMin) / 2;

        /// <summary>
        /// Center point.
        /// 中心点。
        /// </summary>
        public Point2Int Center => new Point2Int {X = XCenter, Y = YCenter};

        /// <summary>
        /// Size as (XSize, YSize).
        /// 尺寸 (XSize, YSize)。
        /// </summary>
        public Point2Int Size => new Point2Int {X = XSize, Y = YSize};

        /// <summary>
        /// Number of unit cells (XSize × YSize).
        /// 单元格数量（XSize × YSize）。
        /// </summary>
        public int Area => XSize * YSize;

        /// <summary>
        /// Whether the bounds have zero area.
        /// 面积是否为 0。
        /// </summary>
        public bool IsNone => Area == 0;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{{Min={m_Min},Max={m_Max},Center={Center},Size={Size},Area={Area}}}";
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return m_Min.GetHashCode() ^ m_Max.GetHashCode() << 2;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Bounds2Int && Equals((Bounds2Int) obj);
        }

        /// <inheritdoc />
        public bool Equals(Bounds2Int other)
        {
            return m_Min == other.m_Min && m_Max == other.m_Max;
        }

        /// <summary>
        /// Creates bounds from min and max corners.
        /// 由最小、最大角点创建边界。
        /// </summary>
        public Bounds2Int(Point2Int min, Point2Int max)
        {
            m_Min = min;
            m_Max = max;
        }

        /// <summary>
        /// Creates bounds from axis limits.
        /// 由各轴上下界创建边界。
        /// </summary>
        public Bounds2Int(int xMin, int yMin, int xMax, int yMax)
        {
            m_Min = new Point2Int {X = xMin, Y = yMin};
            m_Max = new Point2Int {X = xMax, Y = yMax};
        }

        /// <summary>
        /// Replaces min and max corners.
        /// 重新设置最小、最大角点。
        /// </summary>
        public void Set(Point2Int min, Point2Int max)
        {
            m_Min = min;
            m_Max = max;
        }

        //---------------------

        /// <summary>
        /// Whether the point lies inside this bounds (half-open).
        /// 点是否在本边界内（半开）。
        /// </summary>
        public bool Contains(Point2Int point)
        {
            return Contains(point.X, point.Y);
        }

        /// <summary>
        /// Whether (x, y) lies inside this bounds (half-open).
        /// 坐标 (x, y) 是否在本边界内（半开）。
        /// </summary>
        public bool Contains(int x, int y)
        {
            return ContainsX(x) && ContainsY(y);
        }

        /// <summary>
        /// Whether x is in [XMin, XMax).
        /// x 是否在 [XMin, XMax) 内。
        /// </summary>
        public bool ContainsX(int x)
        {
            return x >= XMin && x < XMax;
        }

        /// <summary>
        /// Whether y is in [YMin, YMax).
        /// y 是否在 [YMin, YMax) 内。
        /// </summary>
        public bool ContainsY(int y)
        {
            return y >= YMin && y < YMax;
        }

        /// <summary>
        /// Check if two bounds intersect
        /// 判断两个范围是否相交
        /// </summary>
        /// <param name="bounds2Int">Other bounds.<br/>另一边界。</param>
        /// <returns>True if any corner of the other bounds lies inside this bounds.<br/>若对方任一角点在本边界内则为 true。</returns>
        public bool Intersect(Bounds2Int bounds2Int)
        {
            return Contains(bounds2Int.XMin, bounds2Int.YMin) || Contains(bounds2Int.XMax, bounds2Int.YMax) ||
                   Contains(bounds2Int.XMax, bounds2Int.YMin) || Contains(bounds2Int.XMin, bounds2Int.YMax);
        }

        /// <summary>
        /// Convert to point array
        /// 转为点数组
        /// </summary>
        /// <returns>All integer points in the bounds, or null if empty.<br/>边界内全部整型点；空边界返回 null。</returns>
        public Point2Int[] ToArray()
        {
            var ln = Area;
            if (ln == 0) return null;
            var rs = new Point2Int[ln];
            var index = 0;
            for (var y = YMin; y < YMax; y++)
            {
                for (var x = XMin; x < YMax; x++)
                {
                    rs[index] = new Point2Int(x, y);
                    index++;
                }
            }

            return rs;
        }

        //---------------------

        /// <summary>
        /// Returns a copy translated by <paramref name="offset"/>.
        /// 返回平移 <paramref name="offset"/> 后的副本。
        /// </summary>
        public Bounds2Int Move(Point2Int offset)
        {
            return new Bounds2Int
            {
                XMin = XMin + offset.X,
                YMin = YMin + offset.Y,
                XMax = XMax + offset.X,
                YMax = YMax + offset.Y,
            };
        }

        /// <summary>
        /// Intersection with another bounds (as corners).
        /// 与另一边界的交集（角点形式）。
        /// </summary>
        public Bounds2Int Crop(Bounds2Int subCrop)
        {
            return Crop2(subCrop.Min, subCrop.Max);
        }

        /// <summary>
        /// Intersection with the axis-aligned box [min, max).
        /// 与轴对齐矩形 [min, max) 的交集。
        /// </summary>
        public Bounds2Int Crop2(Point2Int min, Point2Int max)
        {
            return Crop2(min.X, min.Y, max.X, max.Y);
        }

        /// <summary>
        /// Intersection with the axis-aligned box [minX, maxX) × [minY, maxY).
        /// 与轴对齐矩形 [minX, maxX) × [minY, maxY) 的交集。
        /// </summary>
        public Bounds2Int Crop2(int minX, int minY, int maxX, int maxY)
        {
            if (minX >= maxX || minY >= maxY) return Empty;

            var min = Min;
            var max = Max;

            var newMinX = Math.Max(min.X, minX);
            var newMinY = Math.Max(min.Y, minY);
            var newMaxX = Math.Min(max.X, maxX);
            var newMaxY = Math.Min(max.Y, maxY);

            if (newMinX >= newMaxX || newMinY >= newMaxY) return Empty;
            return new Bounds2Int(newMinX, newMinY, newMaxX, newMaxY);
        }

        /// <summary>
        /// Intersection cropped along X only.
        /// 仅沿 X 轴裁剪的交集。
        /// </summary>
        public Bounds2Int CropX(int minX, int maxX)
        {
            var min = Min;
            var max = Max;
            minX = Math.Max(minX, min.X);
            maxX = Math.Min(maxX, max.X);
            if (minX >= maxX) return Empty;
            return new Bounds2Int(minX, min.Y, maxX, max.Y);
        }

        /// <summary>
        /// Intersection cropped along Y only.
        /// 仅沿 Y 轴裁剪的交集。
        /// </summary>
        public Bounds2Int CropY(int minY, int maxY)
        {
            var min = Min;
            var max = Max;
            minY = Math.Max(minY, min.Y);
            maxY = Math.Min(maxY, max.Y);
            if (minY >= maxY) return Empty;
            return new Bounds2Int(min.X, minY, max.X, maxY);
        }

        /// <summary>
        /// Split with x
        /// X分割
        /// </summary>
        /// <param name="x">Split line (must lie inside X range for a real split).<br/>分割线（须落在 X 范围内才会切分）。</param>
        /// <returns>One or two sub-bounds.<br/>一个或两个子边界。</returns>
        public Bounds2Int[] SplitX(int x)
        {
            if (!ContainsX(x))
            {
                return new[] {this};
            }

            return new[] {new Bounds2Int(XMin, YMin, x, YMax), new Bounds2Int(x, YMin, XMax, YMax)};
        }

        /// <summary>
        /// Split with y
        /// Y分割
        /// </summary>
        /// <param name="y">Split line (must lie inside Y range for a real split).<br/>分割线（须落在 Y 范围内才会切分）。</param>
        /// <returns>One or two sub-bounds.<br/>一个或两个子边界。</returns>
        public Bounds2Int[] SplitY(int y)
        {
            if (!ContainsY(y))
            {
                return new[] {this};
            }

            return new[] {new Bounds2Int(XMin, YMin, XMax, y), new Bounds2Int(XMin, y, XMax, YMax)};
        }

        /// <summary>
        /// Union of grid points from this bounds and others (deduplicated).
        /// 合并本边界与其它边界的格点（去重）。
        /// </summary>
        public Point2Int[] Add(Bounds2Int add, params Bounds2Int[] other)
        {
            var list = new List<Point2Int>();
            list.AddRange(add.ToArray());
            if (other.Length > 0)
            {
                foreach (var o in other)
                {
                    list.AddRange(o.ToArray());
                }
            }

            var adds = list.ToArray();
            list.Clear();
            list.AddRange(ToArray());
            foreach (var o in adds)
            {
                if (Contains(o) || list.Contains(o)) continue;
                list.Add(o);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Grid points in this bounds minus those in sub and others.
        /// 本边界格点减去 sub 及其它边界中的格点。
        /// </summary>
        public Point2Int[] Sub(Bounds2Int sub, params Bounds2Int[] other)
        {
            if (IsNone) return null;
            if (!Intersect(sub)) return ToArray();

            var subList = new List<Point2Int>();
            subList.AddRange(sub.ToArray());
            if (other.Length > 0)
            {
                foreach (var o in other)
                {
                    subList.AddRange(o.ToArray());
                }
            }

            var rsList = new List<Point2Int>();
            var olds = ToArray();
            for (var index = 0; index < olds.Length; index++)
            {
                if (subList.Contains(olds[index])) continue;
                rsList.Add(olds[index]);
            }

            return rsList.ToArray();
        }

        //--------------------------------------

        /// <summary>
        /// Inequality comparison.
        /// 不等比较。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Bounds2Int b, Bounds2Int c)
        {
            return !b.Equals(c);
        }

        /// <summary>
        /// Equality comparison.
        /// 相等比较。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Bounds2Int b, Bounds2Int c)
        {
            return b.Equals(c);
        }

        /// <summary>
        /// Scales bounds about center (ceil per axis).
        /// 以中心为基准缩放边界（各轴向上取整）。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static Bounds2Int operator /(Bounds2Int b, float c)
        {
            return NewCenterBound(b.XCenter, b.YCenter, (int) Math.Ceiling(b.XSize / c),
                (int) Math.Ceiling(b.YSize / c));
        }

        /// <summary>
        /// Scales bounds about center (ceil per axis).
        /// 以中心为基准缩放边界（各轴向上取整）。
        /// </summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static Bounds2Int operator *(Bounds2Int b, float c)
        {
            return NewCenterBound(b.XCenter, b.YCenter, (int) Math.Ceiling(b.XSize * c),
                (int) Math.Ceiling(b.YSize * c));
        }

        /// <summary>
        /// Builds half-open bounds centered at <paramref name="center"/> with given size.
        /// 以 <paramref name="center"/> 为中心、给定尺寸构造半开边界。
        /// </summary>
        public static Bounds2Int NewCenterBound(Point2Int center, Point2Int size)
        {
            return NewCenterBound(center.X, center.Y, size.X, size.Y);
        }

        /// <summary>
        /// Builds half-open bounds centered at (centerX, centerY) with given size.
        /// 以 (centerX, centerY) 为中心、给定尺寸构造半开边界。
        /// </summary>
        public static Bounds2Int NewCenterBound(int centerX, int centerY, int sizeX, int sizeY)
        {
            var minX = centerX - sizeX / 2;
            var minY = centerY - sizeY / 2;
            var maxX = minX + sizeX;
            var maxY = minY + sizeY;
            return new Bounds2Int(minX, minY, maxX, maxY);
        }

        /// <summary>
        /// Empty bounds (zero area).
        /// 空边界（面积为 0）。
        /// </summary>
        public static readonly Bounds2Int Empty = new Bounds2Int(0, 0, 0, 0);
    }
}
