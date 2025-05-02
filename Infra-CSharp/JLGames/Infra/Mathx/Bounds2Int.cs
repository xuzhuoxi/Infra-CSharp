using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JLGames.Infra.Mathx
{
    public struct Bounds2Int : IEquatable<Bounds2Int>
    {
        private Point2Int m_Min;
        private Point2Int m_Max;

        public int XMin
        {
            get { return m_Min.X; }
            set { m_Min.X = value; }
        }

        public int YMin
        {
            get { return m_Min.Y; }
            set { m_Min.Y = value; }
        }

        public int XMax
        {
            get { return m_Max.X; }
            set { m_Max.X = value; }
        }

        public int YMax
        {
            get { return m_Max.Y; }
            set { m_Max.Y = value; }
        }

        public int XSize
        {
            get { return XMax - XMin; }
            set { XMax = XMin + value; }
        }

        public int YSize
        {
            get { return YMax - YMin; }
            set { YMax = YMin + value; }
        }

        public Point2Int Min
        {
            get { return m_Min; }
            set { m_Min = value; }
        }

        public Point2Int Max
        {
            get { return m_Max; }
            set { m_Max = value; }
        }

        public int XCenter => (XMax + XMin) / 2;
        public int YCenter => (YMax + YMin) / 2;
        public Point2Int Center => new Point2Int {X = XCenter, Y = YCenter};
        public Point2Int Size => new Point2Int {X = XSize, Y = YSize};
        public int Area => XSize * YSize;
        public bool IsNone => Area == 0;

        public override string ToString()
        {
            return $"{{Min={m_Min},Max={m_Max},Center={Center},Size={Size},Area={Area}}}";
        }

        public override int GetHashCode()
        {
            return m_Min.GetHashCode() ^ m_Max.GetHashCode() << 2;
        }

        public override bool Equals(object obj)
        {
            return obj is Bounds2Int && Equals((Bounds2Int) obj);
        }

        public bool Equals(Bounds2Int other)
        {
            return m_Min == other.m_Min && m_Max == other.m_Max;
        }

        public Bounds2Int(Point2Int min, Point2Int max)
        {
            m_Min = min;
            m_Max = max;
        }

        public Bounds2Int(int xMin, int yMin, int xMax, int yMax)
        {
            m_Min = new Point2Int {X = xMin, Y = yMin};
            m_Max = new Point2Int {X = xMax, Y = yMax};
        }

        public void Set(Point2Int min, Point2Int max)
        {
            m_Min = min;
            m_Max = max;
        }

        //---------------------

        public bool Contains(Point2Int point)
        {
            return Contains(point.X, point.Y);
        }

        public bool Contains(int x, int y)
        {
            return ContainsX(x) && ContainsY(y);
        }

        public bool ContainsX(int x)
        {
            return x >= XMin && x < XMax;
        }

        public bool ContainsY(int y)
        {
            return y >= YMin && y < YMax;
        }

        /// <summary>
        /// Check if two bounds intersect
        /// 判断两个范围是否相交
        /// </summary>
        /// <param name="bounds2Int"></param>
        /// <returns></returns>
        public bool Intersect(Bounds2Int bounds2Int)
        {
            return Contains(bounds2Int.XMin, bounds2Int.YMin) || Contains(bounds2Int.XMax, bounds2Int.YMax) ||
                   Contains(bounds2Int.XMax, bounds2Int.YMin) || Contains(bounds2Int.XMin, bounds2Int.YMax);
        }

        /// <summary>
        /// Convert to point array
        /// 转为点数组
        /// </summary>
        /// <returns></returns>
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

        public Bounds2Int Crop(Bounds2Int subCrop)
        {
            return Crop2(subCrop.Min, subCrop.Max);
        }

        public Bounds2Int Crop2(Point2Int min, Point2Int max)
        {
            return Crop2(min.X, min.Y, max.X, max.Y);
        }

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

        public Bounds2Int CropX(int minX, int maxX)
        {
            var min = Min;
            var max = Max;
            minX = Math.Max(minX, min.X);
            maxX = Math.Min(maxX, max.X);
            if (minX >= maxX) return Empty;
            return new Bounds2Int(minX, min.Y, maxX, max.Y);
        }

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
        /// <param name="x"></param>
        /// <returns></returns>
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
        /// <param name="y"></param>
        /// <returns></returns>
        public Bounds2Int[] SplitY(int y)
        {
            if (!ContainsY(y))
            {
                return new[] {this};
            }

            return new[] {new Bounds2Int(XMin, YMin, XMax, y), new Bounds2Int(XMin, y, XMax, YMax)};
        }

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

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(Bounds2Int b, Bounds2Int c)
        {
            return !b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(Bounds2Int b, Bounds2Int c)
        {
            return b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static Bounds2Int operator /(Bounds2Int b, float c)
        {
            return NewCenterBound(b.XCenter, b.YCenter, (int) Math.Ceiling(b.XSize / c),
                (int) Math.Ceiling(b.YSize / c));
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static Bounds2Int operator *(Bounds2Int b, float c)
        {
            return NewCenterBound(b.XCenter, b.YCenter, (int) Math.Ceiling(b.XSize * c),
                (int) Math.Ceiling(b.YSize * c));
        }

        public static Bounds2Int NewCenterBound(Point2Int center, Point2Int size)
        {
            return NewCenterBound(center.X, center.Y, size.X, size.Y);
        }

        public static Bounds2Int NewCenterBound(int centerX, int centerY, int sizeX, int sizeY)
        {
            var minX = centerX - sizeX / 2;
            var minY = centerY - sizeY / 2;
            var maxX = minX + sizeX;
            var maxY = minY + sizeY;
            return new Bounds2Int(minX, minY, maxX, maxY);
        }

        public static readonly Bounds2Int Empty = new Bounds2Int(0, 0, 0, 0);
    }
}