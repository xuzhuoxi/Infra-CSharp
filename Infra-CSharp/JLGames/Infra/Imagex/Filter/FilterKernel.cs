using System;

namespace JLGames.Infra.Imagex
{
    /// <summary>
    /// Sparse convolution kernel as offset/weight vectors.
    /// 以偏移/权重向量表示的稀疏卷积核。
    /// </summary>
    public class FilterKernel : ICloneable<FilterKernel>
    {
        internal KernelVector[] m_Vectors;

        /// <summary>
        /// Number of kernel vectors, or 0 if unset.
        /// 卷积核向量数量；未初始化时为 0。
        /// </summary>
        public int Len => m_Vectors?.Length ?? 0;

        /// <summary>
        /// Whether vector at index <paramref name="i"/> sorts before index <paramref name="j"/>.
        /// 下标 <paramref name="i"/> 的向量是否排在下标 <paramref name="j"/> 之前。
        /// </summary>
        /// <param name="i">First index; 第一个下标</param>
        /// <param name="j">Second index; 第二个下标</param>
        /// <returns><c>true</c> if <c>Vectors[i]</c> precedes <c>Vectors[j]</c>; 前者排序靠前则为 <c>true</c></returns>
        public bool Less(int i, int j)
        {
            return m_Vectors[i].Less(m_Vectors[j]);
        }

        /// <summary>
        /// Whether <paramref name="i"/> sorts before <paramref name="j"/> (row-major: Y then X).
        /// <paramref name="i"/> 是否排在 <paramref name="j"/> 之前（先行后列）。
        /// </summary>
        /// <param name="i">First vector; 第一个向量</param>
        /// <param name="j">Second vector; 第二个向量</param>
        /// <returns><c>true</c> if <paramref name="i"/> precedes <paramref name="j"/>; 前者排序靠前则为 <c>true</c></returns>
        public bool Less(KernelVector i, KernelVector j)
        {
            return i.Less(j);
        }

        /// <summary>
        /// Swap two vectors by index.
        /// 按下标交换两个向量。
        /// </summary>
        /// <param name="i">First index; 第一个下标</param>
        /// <param name="j">Second index; 第二个下标</param>
        public void Swap(int i, int j)
        {
            (m_Vectors[i], m_Vectors[j]) = (m_Vectors[j], m_Vectors[i]);
        }

        /// <summary>
        /// Find the first index whose weight equals <paramref name="value"/>.
        /// 查找权重等于 <paramref name="value"/> 的第一个下标。
        /// </summary>
        /// <param name="value">Weight to search for; 要查找的权重</param>
        /// <returns>Index, or -1 if not found; 下标，未找到返回 -1</returns>
        public int IndexOfValue(int value)
        {
            for (var index = 0; index < m_Vectors.Length; index++)
            {
                if (m_Vectors[index].Value == value) return index;
            }

            return -1;
        }

        /// <summary>
        /// Shallow-copy the vector array.
        /// 浅拷贝向量数组。
        /// </summary>
        /// <returns>A new kernel with copied vectors; 含拷贝向量的新卷积核</returns>
        public FilterKernel Clone()
        {
            var units = m_Vectors?.Clone() as KernelVector[];
            return new FilterKernel {m_Vectors = units};
        }

        /// <summary>
        /// Flip itself upside down.
        /// 上下翻转自身。
        /// </summary>
        public void FlipUpDownSelf()
        {
            for (var index = 0; index < m_Vectors.Length; index++)
            {
                if (0 == m_Vectors[index].Y)
                {
                    continue;
                }

                m_Vectors[index].Y = -m_Vectors[index].Y;
            }
        }

        /// <summary>
        /// Flip upside down and return a new kernel.
        /// 上下翻转并返回新卷积核。
        /// </summary>
        /// <returns>Flipped copy; 翻转后的副本</returns>
        public FilterKernel FlipUuDown()
        {
            var rs = Clone();
            rs.FlipUpDownSelf();
            return rs;
        }

        /// <summary>
        /// Flip itself left and right.
        /// 左右翻转自身。
        /// </summary>
        public void FlipLeftRightSelf()
        {
            for (var index = 0; index < m_Vectors.Length; index++)
            {
                if (0 == m_Vectors[index].X)
                {
                    continue;
                }

                m_Vectors[index].X = -m_Vectors[index].X;
            }
        }

        /// <summary>
        /// Flip left and right and return a new kernel.
        /// 左右翻转并返回新卷积核。
        /// </summary>
        /// <returns>Flipped copy; 翻转后的副本</returns>
        public FilterKernel FlipLeftRight()
        {
            var rs = Clone();
            rs.FlipLeftRightSelf();
            return rs;
        }

        /// <summary>
        /// Rotate 90 degrees in place.
        /// 原地旋转 90 度。
        /// </summary>
        /// <param name="clockwise"><c>true</c> for clockwise, <c>false</c> for counter-clockwise; <c>true</c> 顺时针，<c>false</c> 逆时针</param>
        public void Rotate90Self(bool clockwise)
        {
            int x;
            int y;
            if (clockwise)
            {
                for (var index = 0; index < m_Vectors.Length; index++)
                {
                    x = -m_Vectors[index].Y;
                    y = m_Vectors[index].X;
                    m_Vectors[index].X = x;
                    m_Vectors[index].Y = y;
                }
            }
            else
            {
                for (var index = 0; index < m_Vectors.Length; index++)
                {
                    x = m_Vectors[index].Y;
                    y = -m_Vectors[index].X;
                    m_Vectors[index].X = x;
                    m_Vectors[index].Y = y;
                }
            }
        }

        /// <summary>
        /// Rotate 90 degrees and return a new kernel.
        /// 旋转 90 度并返回新卷积核。
        /// </summary>
        /// <param name="clockwise"><c>true</c> for clockwise, <c>false</c> for counter-clockwise; <c>true</c> 顺时针，<c>false</c> 逆时针</param>
        /// <returns>Rotated copy; 旋转后的副本</returns>
        public FilterKernel Rotate90(bool clockwise)
        {
            var rs = Clone();
            rs.Rotate90Self(clockwise);
            return rs;
        }

        /// <summary>
        /// Rotate in place by multiples of 90 degrees.
        /// 原地按 90 度倍数旋转。
        /// </summary>
        /// <param name="clockwise"><c>true</c> for clockwise per step; 每步 <c>true</c> 为顺时针</param>
        /// <param name="count90">Number of 90° steps (negative values wrap); 90° 步数（负值会归一化）</param>
        public void RotateSelf(bool clockwise, int count90)
        {
            var c = count90;
            while (c < 0)
            {
                c += 4;
            }

            c = c % 4;
            while (c > 0)
            {
                Rotate90Self(clockwise);
                c--;
            }
        }

        /// <summary>
        /// Rotate by multiples of 90 degrees and return a new kernel.
        /// 按 90 度倍数旋转并返回新卷积核。
        /// </summary>
        /// <param name="clockwise"><c>true</c> for clockwise per step; 每步 <c>true</c> 为顺时针</param>
        /// <param name="count90">Number of 90° steps (negative values wrap); 90° 步数（负值会归一化）</param>
        /// <returns>Rotated copy; 旋转后的副本</returns>
        public FilterKernel Rotate(bool clockwise, int count90)
        {
            var rs = Clone();
            rs.RotateSelf(clockwise, count90);
            return rs;
        }

        /// <summary>
        /// Sort vectors in row-major order (Y then X).
        /// 按先行后列（Y 再 X）对向量排序。
        /// </summary>
        public void Sort()
        {
            Array.Sort(m_Vectors);
        }
    }
}
