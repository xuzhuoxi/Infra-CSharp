namespace JLGames.Infra.Imagex
{
    /// <summary>
    /// Image convolution filter matrix (sparse kernel + scale/offset metadata).
    /// 图像卷积滤波矩阵（稀疏卷积核及倍率/偏移元数据）。
    /// </summary>
    public class FilterMatrix : ICloneable<FilterMatrix>
    {
        private FilterKernel m_Kernel; // 滤波器卷积核
        private int m_KernelRadius; // 滤波器半径
        private int m_KernelSize; // 滤波器边长
        private int m_KernelScale; // 滤波器卷积核倍率
        private int m_ResultOffset; // 运算结果偏移量

        /// <summary>
        /// Sparse convolution kernel.
        /// 稀疏卷积核。
        /// </summary>
        public FilterKernel Kernel => m_Kernel;

        /// <summary>
        /// Kernel radius (half side length in pixels).
        /// 卷积核半径（像素半边长）。
        /// </summary>
        public int KernelRadius => m_KernelRadius;

        /// <summary>
        /// Kernel side length (<c>2 * KernelRadius + 1</c>).
        /// 卷积核边长（<c>2 * KernelRadius + 1</c>）。
        /// </summary>
        public int KernelSize => m_KernelSize;

        /// <summary>
        /// Divisor applied after convolution (sum of weights should equal this).
        /// 卷积后的除数（权重之和应等于此值）。
        /// </summary>
        public int KernelScale => m_KernelScale;

        /// <summary>
        /// Constant offset added to the filtered result.
        /// 滤波结果上加的常数偏移。
        /// </summary>
        public int ResultOffset => m_ResultOffset;

        /// <summary>
        /// Deep-copy kernel and metadata.
        /// 深拷贝卷积核及元数据。
        /// </summary>
        /// <returns>A new independent copy; 新的独立副本</returns>
        public FilterMatrix Clone()
        {
            var kernel = m_Kernel.Clone();
            return new FilterMatrix
            {
                m_Kernel = kernel,
                m_KernelRadius = m_KernelRadius,
                m_KernelSize = m_KernelSize,
                m_KernelScale = m_KernelScale,
                m_ResultOffset = m_ResultOffset,
            };
        }

        /// <summary>
        /// Whether it is a magnification filter.
        /// 是否为倍率滤波器。
        /// </summary>
        public bool IsScaleMatrix => m_KernelScale != 0 && m_KernelScale != 1;

        /// <summary>
        /// Whether the operation result may exceed the pixel range (unsafe pixel value).
        /// 运算结果是否可能超出像素范围（非安全像素值）。
        /// </summary>
        public bool IsPixelUnsafe
        {
            get
            {
                if (m_ResultOffset != 0)
                {
                    return true;
                }

                for (var index = 0; index < m_Kernel.m_Vectors.Length; index++)
                {
                    if (m_Kernel.m_Vectors[index].Value < 0) return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Flip upside down.
        /// 上下翻转。
        /// </summary>
        /// <returns>Flipped copy with sorted kernel; 翻转并已排序卷积核的副本</returns>
        public FilterMatrix FlipUpDown()
        {
            var rs = Clone();
            rs.m_Kernel.FlipUpDownSelf();
            rs.m_Kernel.Sort();
            return rs;
        }

        /// <summary>
        /// Flip left and right.
        /// 左右翻转。
        /// </summary>
        /// <returns>Flipped copy with sorted kernel; 翻转并已排序卷积核的副本</returns>
        public FilterMatrix FlipLeftRight()
        {
            var rs = Clone();
            rs.m_Kernel.FlipLeftRightSelf();
            rs.m_Kernel.Sort();
            return rs;
        }

        /// <summary>
        /// Rotate by multiples of 90 degrees.
        /// 按 90 度倍数旋转。
        /// </summary>
        /// <param name="clockwise"><c>true</c> for clockwise per step; 每步 <c>true</c> 为顺时针</param>
        /// <param name="count90">Number of 90° steps; 90° 步数</param>
        /// <returns>Rotated copy with sorted kernel; 旋转并已排序卷积核的副本</returns>
        public FilterMatrix Rotate(bool clockwise, int count90)
        {
            var rs = Clone();
            rs.m_Kernel.RotateSelf(clockwise, count90);
            rs.m_Kernel.Sort();
            return rs;
        }

        /// <summary>
        /// Check filter template validity (radius, scale, and weight sum).
        /// 检查滤波模板有效性（半径、倍率及权重和）。
        /// </summary>
        /// <returns><c>true</c> if radius ≥ 1, scale ≥ 0, and weights sum to <see cref="KernelScale"/>; 半径 ≥ 1、倍率 ≥ 0 且权重和等于 <see cref="KernelScale"/> 时为 <c>true</c></returns>
        public bool CheckValidity()
        {
            if (m_KernelRadius < 1) return false;
            if (m_KernelScale < 0) return false;
            var sum = 0;
            foreach (var unit in m_Kernel.m_Vectors)
            {
                sum += unit.Value;
            }

            return sum == m_KernelScale;
        }
    }
}
