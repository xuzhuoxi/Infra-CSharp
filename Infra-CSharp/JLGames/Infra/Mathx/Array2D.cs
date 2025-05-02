using System;
using System.Text;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// Two-dimensional array wrapper class
    /// 二维数组包装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Array2D<T>
    {
        private T[][] m_Data;
        private Point2Int m_Size;
        private Bounds2Int m_Bound;

        public Array2D()
        {
        }

        public Array2D(T[][] data)
        {
            SetData(data);
        }

        public Array2D(T[] data, int width)
        {
            SetData(data, width);
        }

        /// <summary>
        /// Set data
        /// 设置数据 
        /// </summary>
        /// <param name="data"></param>
        public void SetData(T[][] data)
        {
            m_Data = data;
            m_Size = new Point2Int {X = data[0].Length, Y = data.Length};
            m_Bound = new Bounds2Int(Point2Int.Zero, m_Size);
        }

        /// <summary>
        /// Set data
        /// 设置数据 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="width"></param>
        public void SetData(T[] data, int width)
        {
            var height = data.Length / width;
            var data2 = new T[height][];
            var heightIndex = 0;
            for (var index = 0; index < data.Length; index += width)
            {
                var sub = new T[width];
                Array.Copy(data, index, sub, 0, width);
                data2[heightIndex] = sub;
                heightIndex += 1;
            }

            m_Data = data2;
            m_Size = new Point2Int {X = width, Y = height};
            m_Bound = new Bounds2Int(Point2Int.Zero, m_Size);
        }

        /// <summary>
        /// Size
        /// </summary>
        public Point2Int Size => m_Size;

        /// <summary>
        /// Bound
        /// 边界
        /// </summary>
        public Bounds2Int Bound => m_Bound;

        /// <summary>
        /// Get value
        /// 取值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public T GetValue(int x, int y)
        {
            if (!m_Bound.ContainsX(x) || !m_Bound.ContainsY(y))
            {
                return default(T);
            }

            return m_Data[y][x];
        }

        /// <summary>
        /// Take the coordinates of the point whose value is nearby
        /// 取附近值为value的点坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <param name="max">最大搜索范围</param>
        /// <returns></returns>
        public Point2Int? GetNearValue(int x, int y, T value, int max)
        {
            for (var addY = 0; addY < max; addY++)
            {
                var y1 = y + addY;
                var y2 = y - addY;
                for (var addX = 0; addX <= addY; addX++)
                {
                    var x1 = x + addX;
                    var x2 = x - addX;

                    if (CheckValue(x1, y1, value))
                    {
                        return new Point2Int(x1, y1);
                    }

                    if (CheckValue(x2, y1, value))
                    {
                        return new Point2Int(x2, y1);
                    }

                    if (CheckValue(x1, y2, value))
                    {
                        return new Point2Int(x1, y2);
                    }

                    if (CheckValue(x2, y2, value))
                    {
                        return new Point2Int(x2, y2);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Check value by position
        /// 值检查
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckValue(int x, int y, T value)
        {
            if (!m_Bound.ContainsX(x) || !m_Bound.ContainsY(y))
            {
                return false;
            }

            if (value is IEquatable<T>)
            {
                return value.Equals(m_Data[y][x]);
            }

            return (object) (m_Data[y][x]) == (object) value;
        }

        /// <summary>
        /// Get all data.
        /// 取全部数据
        /// </summary>
        /// <returns></returns>
        public T[][] GetData()
        {
            return m_Data;
        }

        /// <summary>
        /// Get data within the boundaries
        /// 取区域数据
        /// </summary>
        /// <param name="bound"></param>
        /// <returns></returns>
        public T[][] GetDataAtBound(Bounds2Int bound)
        {
            return bound.Equals(m_Bound) ? m_Data : GetDataAtBound(bound.XMin, bound.YMin, bound.XMax, bound.YMax);
        }

        /// <summary>
        /// Get data within the boundaries
        /// 取区域数据
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="yMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMax"></param>
        /// <returns></returns>
        public T[][] GetDataAtBound(int xMin, int yMin, int xMax, int yMax)
        {
            var value = m_Bound.Crop2(xMin, yMin, xMax, yMax);
            if (value.IsNone)
            {
                return null;
            }

            var ySize = value.YSize;
            var xSize = value.XSize;
            var rs = new T[ySize][];
            var y = 0;
            for (var yIndex = value.YMin; yIndex < value.YMax; yIndex++)
            {
                rs[y] = new T[xSize];
                Array.Copy(m_Data[yIndex], value.XMin, rs[y], 0, xSize);
                y++;
            }

            return rs;
        }

        public override string ToString()
        {
            return ToPrintString(true);
        }

        public string ToPrintString(bool reverse)
        {
            var sb = new StringBuilder();
            if (!reverse)
            {
                foreach (var ds in m_Data)
                {
                    sb.Append(string.Join(",", ds));
                    sb.Append("\n");
                }
            }
            else
            {
                for (var i = m_Data.Length - 1; i >= 0; i--)
                {
                    sb.Append(string.Join(",", m_Data[i]));
                    sb.Append("\n");
                }
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}