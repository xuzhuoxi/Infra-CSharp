namespace JLGames.Infra.Imagex
{
    /// <summary>
    /// Image filter
    /// 图像滤波器
    /// </summary>
    public class FilterMatrix : ICloneable<FilterMatrix>
    {
        private FilterKernel m_Kernel; // 滤波器卷积核
        private int m_KernelRadius; // 滤波器半径
        private int m_KernelSize; // 滤波器边长
        private int m_KernelScale; // 滤波器卷积核倍率
        private int m_ResultOffset; // 运算结果偏移量

        public FilterKernel Kernel => m_Kernel;
        public int KernelRadius => m_KernelRadius;
        public int KernelSize => m_KernelSize;
        public int KernelScale => m_KernelScale;
        public int ResultOffset => m_ResultOffset;

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
        /// Whether it is a magnification filter
        /// 是否为倍率滤波器
        /// </summary>
        public bool IsScaleMatrix => m_KernelScale != 0 && m_KernelScale != 1;

        /// <summary>
        /// Whether the operation result may exceed the pixel range (unsafe pixel value)
        /// 运算结果是否可能超出像素范围(非安全像素值)
        /// </summary>
        public bool IsPixelUnsafe
        {
            get
            {
                if (m_ResultOffset != 0)
                {
                    return true;
                }

                for (var index = 0; index < m_Kernel.Vectors.Length; index++)
                {
                    if (m_Kernel.Vectors[index].Value < 0) return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Flip Upside down
        /// 上下翻转
        /// </summary>
        /// <returns></returns>
        public FilterMatrix FlipUpDown()
        {
            var rs = Clone();
            rs.m_Kernel.FlipUpDownSelf();
            rs.m_Kernel.Sort();
            return rs;
        }

        /// <summary>
        /// Flip left and right
        /// 左右翻转
        /// </summary>
        /// <returns></returns>
        public FilterMatrix FlipLeftRight()
        {
            var rs = Clone();
            rs.m_Kernel.FlipLeftRightSelf();
            rs.m_Kernel.Sort();
            return rs;
        }

        /// <summary>
        /// Rotate 90 degrees
        /// 旋转90度
        /// </summary>
        /// <param name="clockwise">是否顺时针</param>
        /// <param name="count90">旋转次数</param>
        /// <returns></returns>
        public FilterMatrix Rotate(bool clockwise, int count90)
        {
            var rs = Clone();
            rs.m_Kernel.RotateSelf(clockwise, count90);
            rs.m_Kernel.Sort();
            return rs;
        }

        /// <summary>
        /// Filter Template Effectiveness
        /// 滤波模板有效性
        /// </summary>
        /// <returns></returns>
        public bool CheckValidity()
        {
            if (m_KernelRadius < 1) return false;
            if (m_KernelScale < 0) return false;
            var sum = 0;
            foreach (var unit in m_Kernel.Vectors)
            {
                sum += unit.Value;
            }

            return sum == m_KernelScale;
        }
    }
}