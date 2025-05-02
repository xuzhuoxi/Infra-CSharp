using System;
using System.Collections.Generic;
using System.Linq;

namespace JLGames.Infra.Utils
{
    public static class ArrayUtil
    {
        /// <summary>
        /// Clone array
        /// 克隆数组
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <returns></returns>
        public static T[] CloneArray<T, TK>(TK[] source) where T : class where TK : class
        {
            if (null == source) return null;
            var lenSource = source.Length;
            if (lenSource == 0) return new T[0];
            var rs = new T[lenSource];
            for (var index = 0; index < lenSource; index++)
            {
                rs[index] = source[index] as T;
            }

            return rs;
        }

        #region 合并拆分追加创建数组

        /// <summary>
        /// Create a 1D array and set the values
        /// 创建一维数组并赋值
        /// </summary>
        /// <param name="len"></param>
        /// <param name="default"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] NewArray<T>(int len, T @default)
        {
            var rs = new T[len];
            for (var index = 0; index < len; index++)
            {
                rs[index] = @default;
            }

            return rs;
        }

        /// <summary>
        /// Create a 2D array
        /// 创建二维数组
        /// </summary>
        /// <returns>The 2D array.</returns>
        /// <param name="yLen">行数</param>
        /// <param name="xLen">列数</param>
        public static T[][] NewArray<T>(int yLen, int xLen)
        {
            var rs = new T[yLen][];
            for (var i = 0; i < yLen; i++)
            {
                rs[i] = new T[xLen];
            }

            return rs;
        }

        /// <summary>
        /// Create a 2D array and set the values
        /// 创建二维数组并赋值
        /// </summary>
        /// <returns>The 2D array.</returns>
        /// <param name="yLen">行数</param>
        /// <param name="xLen">列数</param>
        /// <param name="default"></param>
        public static T[][] NewArray<T>(int yLen, int xLen, T @default)
        {
            var rs = new T[yLen][];
            for (var i = 0; i < yLen; i++)
            {
                rs[i] = NewArray(xLen, @default);
            }

            return rs;
        }

        /// <summary>
        /// Merge array
        /// 合并数组
        /// </summary>
        /// <param name="first">第一个元素</param>
        /// <param name="second">第二个数组</param>
        /// <returns>合并后的数组(第一个元素+第二个数组，长度为两者长度和)</returns>
        public static T[] MergeArray<T>(T first, T[] second)
        {
            if (null == first && null == second) return null;
            if (null == first) return second;
            if (null == second) return new[] {first};
            var result = new T[second.Length + 1];
            result[0] = first;
            second.CopyTo(result, 1);
            return result;
        }

        /// <summary>
        /// Merge array
        /// 合并数组
        /// </summary>
        /// <param name="first">第一个数组</param>
        /// <param name="second">第二个元素</param>
        /// <returns>合并后的数组(第一个数组+第二个元素，长度为两者长度和)</returns>
        public static T[] MergeArray<T>(T[] first, T second)
        {
            if (null == first && null == second) return null;
            if (null == first) return new T[] {second};
            if (null == second) return first;
            var result = new T[first.Length + 1];
            first.CopyTo(result, 0);
            result[result.Length - 1] = second;
            return result;
        }

        /// <summary>
        /// Merge array
        /// 合并数组
        /// </summary>
        /// <param name="first">第一个数组</param>
        /// <param name="second">第二个数组</param>
        /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
        public static T[] MergeArray<T>(T[] first, T[] second)
        {
            if (null == first && null == second) return null;
            if (null == first) return second;
            if (null == second) return first;
            var result = new T[first.Length + second.Length];
            first.CopyTo(result, 0);
            second.CopyTo(result, first.Length);
            return result;
        }

        /// <summary>
        /// Merge array
        /// 合并数组
        /// </summary>
        /// <param name="first">第一个数组</param>
        /// <param name="second">第二个数组</param>
        /// <param name="other">不定个数组</param>
        /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
        public static T[] MergeArray<T>(T[] first, T[] second, params T[][] other)
        {
            var fLen = first?.Length ?? 0;
            var sLen = second?.Length ?? 0;
            var len = fLen + sLen;
            for (var i = 0; i < other.Length; i++)
            {
                var o1 = other[i];
                len += o1.Length;
            }

            var result = new T[len];
            var index = 0;
            if (null != first && first.Length > 0)
            {
                first.CopyTo(result, 0);
                index = first.Length;
            }

            if (null != second && second.Length > 0)
            {
                second.CopyTo(result, index);
                index += second.Length;
            }

            if (other.Length > 0)
            {
                foreach (var o2 in other)
                {
                    o2.CopyTo(result, index);
                    index += o2.Length;
                }
            }

            return result;
        }

        /// <summary>
        /// Merge 2D arrays in X direction
        /// 以X方向合并二维数组
        /// </summary>
        /// <param name="first"></param>
        /// <param name="other"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[][] MergeArrayX<T>(T[][] first, params T[][][] other)
        {
            if (null == first)
            {
                return null;
            }

            if (other.Length == 0)
            {
                return first;
            }

            var xlen = first[0].Length;
            for (var i = other.Length - 1; i >= 0; i--)
            {
                if (other[i][0].Length != first.Length)
                {
                    return null;
                }

                xlen += other[i][0].Length;
            }

            var rs = NewArray<T>(first.Length, xlen);
            for (var yIndex = rs.Length - 1; yIndex >= 0; yIndex--)
            {
                Array.Copy(first[yIndex], 0, rs[yIndex], 0, first[yIndex].Length);
                var startIndex = first[yIndex].Length;
                for (var oIndex = 0; oIndex < other.Length; oIndex++)
                {
                    Array.Copy(other[oIndex][yIndex], 0, rs[yIndex], startIndex, other[oIndex][yIndex].Length);
                    startIndex += other[oIndex][yIndex].Length;
                }
            }

            return rs;
        }

        /// <summary>
        /// Merge 2D arrays in Y direction
        /// 以Y方向合并二维数组
        /// </summary>
        /// <param name="first"></param>
        /// <param name="other"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[][] MergeArrayY<T>(T[][] first, params T[][][] other)
        {
            if (null == first)
            {
                return null;
            }

            if (other.Length == 0)
            {
                return first;
            }

            var yLen = first.Length;
            for (var i = other.Length - 1; i >= 0; i--)
            {
                if (other[i][0].Length != first[0].Length)
                {
                    return null;
                }

                yLen += other[i].Length;
            }

            var rs = NewArray<T>(yLen, first[0].Length);
            Array.Copy(first, 0, rs, 0, first.Length);
            var startY = first.Length;
            for (var i = 0; i < other.Length; i++)
            {
                Array.Copy(other[i], 0, rs, startY, other[i].Length);
                startY += other[i].Length;
            }

            return rs;
        }

        /// <summary>
        /// Copy data
        /// 复制数据
        /// </summary>
        /// <param name="target">目标数组</param>
        /// <param name="source">源数组</param>
        /// <param name="startTargetX">目标数组开始X</param>
        /// <param name="startTargetY">目标数组开始Y</param>
        /// <typeparam name="T"></typeparam>
        public static void CopyDataTo<T>(T[][] target, T[][] source, uint startTargetX, uint startTargetY)
        {
            if (source == null || target == null || source.Length == 0 || target.Length == 0 || source[0] == null ||
                source[0].Length == 0 || target[0] == null || target[0].Length == 0)
            {
                return;
            }

            var yLen = Math.Min(target.Length - startTargetY, source.Length);
            var xLen = Math.Min(target[0].Length - startTargetX, source[0].Length);
            var sIndex = 0;
            var tIndex = startTargetY;
            for (; sIndex < yLen; sIndex++, tIndex++)
            {
                Array.Copy(source[sIndex], 0, target[tIndex], startTargetX, xLen);
            }
        }

        /// <summary>
        /// Concat element to array
        /// 数组追加
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="element">元素</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        public static T[] ConcatArray<T>(T[] source, T element)
        {
            return MergeArray(source, element);
        }

        /// <summary>
        /// Cut a part from an array into a new array
        /// 从数组中截取一部分成新的数组
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="startIndex">原数组的起始位置</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static T[] SubArray<T>(T[] source, int startIndex, int len)
        {
            var result = new T[len];
            Array.Copy(source, startIndex, result, 0, len);
            return result;
        }

        /// <summary>
        /// Cut a part from an array into a new array
        /// 从数组中截取一部分成新的数组
        /// </summary>
        /// <param name="source">原数组</param>
        /// <param name="startIndex">原数组的起始位置</param>
        /// <param name="endIndex">原数组的截止位置(不包含)</param>
        /// <returns></returns>
        public static T[] SubAry<T>(T[] source, int startIndex, int endIndex)
        {
            var len = endIndex - startIndex;
            return SubArray(source, startIndex, len);
        }

        /// <summary>
        /// remove some elements from an array
        /// 从数组中移除部分元素
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="len">支持负数</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] RemoveElement<T>(T[] source, int index, int len)
        {
            if (null == source || source.Length == 0) return source;
            if (0 == len) return source;
            var sLen = source.Length;
//            if (index < 0 || index >= sLen) return source;
            var endIndex = index + len;

            if (len > 0)
            {
                if (endIndex > sLen) endIndex = sLen;
            }
            else
            {
                var temp = index;
                index = endIndex + 1;
                endIndex = temp + 1;
                if (index < 0) index = 0;
                if (endIndex > sLen) endIndex = sLen;
            }

            var rLen = endIndex - index;
            var newLen = sLen - rLen;
            var rs = new T[newLen];
            Array.Copy(source, 0, rs, 0, index);
            Array.Copy(source, index + rLen, rs, index, newLen - index);
            return rs;
        }

        /// <summary>
        /// Insert element into array
        /// 数组中插入元素
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] InsertElement<T>(T[] source, T e, int index)
        {
            if (null == source || source.Length == 0) return new[] {e};

            var sLen = source.Length;
//            if (index < 0 || index >= sLen) return source;

            var rs = new T[sLen + 1];
            Array.Copy(source, 0, rs, 0, index);
            rs[index] = e;
            Array.Copy(source, index, rs, index + 1, sLen - index);
            return rs;
        }

        /// <summary>
        /// Insert some elements into array
        /// 数组中插入一些元素
        /// </summary>
        /// <param name="source"></param>
        /// <param name="es"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] InsertElement<T>(T[] source, IEnumerable<T> es, int index)
        {
            if (null == es) return source;
            var enumerable = es as T[] ?? es.ToArray();

            if (null == source || source.Length == 0) return enumerable;

            var sLen = source.Length;
//            if (index < 0 || index >= sLen) return source;

            var eLen = enumerable.Count();

            var rs = new T[sLen + eLen];
            Array.Copy(source, 0, rs, 0, index);
            Array.Copy(enumerable, 0, rs, index + 1, eLen);
            Array.Copy(source, index, rs, index + eLen + 1, sLen - index);
            return rs;
        }

        #endregion

        #region 旋转数组

        public static T[][] Rotated<T>(T[][] array)
        {
            if (null == array || array.Length == 0)
            {
                return null;
            }

            var width = array[0].Length;
            var newData = new T[array.Length][];
            for (var i = 0; i < array.Length; i++)
            {
                newData[i] = new T[width];
            }

            for (var row = 0; row < array.Length; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    newData[col][row] = array[row][width - 1 - col];
                }
            }

            return newData;
        }

        public static T[] Rotated<T>(T[] array, int width, int height)
        {
            var newData = new T[array.Length];
            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    copy2(width, array, row, width - 1 - col, newData, col, row);
                }
            }

            return newData;
        }

        private static void copy2<T>(int width, T[] sourceArray, int oCol, int oRow, T[] targetArray, int nCol,
            int nRow)
        {
            var oIndex = oRow * width + oCol;
            var nIndex = nRow * width + nCol;
            targetArray[nIndex] = sourceArray[oIndex];
        }
        /*
        private void rotate2() {
            int n = data.Length;
            int limit = (n - 1) / 2;
            for (int i = 0; i <= limit; i++) {
                for (int j = i; j < n - 1 - i; j++) {
                    int temp = data[getIndex(j,i)];
                    data[getIndex(j, i)] = data[getIndex(i, n - 1 - j)];
                    data[getIndex(i, n - 1 - j)] = data[getIndex(n - 1 - j, n - 1 - i)];
                    data[getIndex(n - 1 - j, n - 1 - i)] = data[getIndex(n - 1 - i, j)];
                    data[getIndex(n - 1 - i, j)] = temp;
                }
            }
        }
        */

        #endregion
    }
}