using System;

namespace JLGames.Infra.Imagex
{
    /// <summary>
    /// Filter vector unit.
    /// 滤波器向量单元。
    /// </summary>
    public struct KernelVector : IComparable<KernelVector>
    {
        /// <summary>
        /// Offset along X from kernel center.
        /// 相对卷积核中心的 X 偏移。
        /// </summary>
        public int X;

        /// <summary>
        /// Offset along Y from kernel center.
        /// 相对卷积核中心的 Y 偏移。
        /// </summary>
        public int Y;

        /// <summary>
        /// Convolution weight at this offset.
        /// 该偏移处的卷积权重。
        /// </summary>
        public int Value;

        /// <summary>
        /// Compare by sort order (Y then X); never returns 0 for equal keys.
        /// 按排序规则比较（先 Y 后 X）；相等时也不返回 0。
        /// </summary>
        /// <param name="j">Other vector; 另一向量</param>
        /// <returns>-1 if this precedes <paramref name="j"/>, otherwise 1; 若本项排在前面返回 -1，否则 1</returns>
        public int CompareTo(KernelVector j)
        {
            return Less(j) ? -1 : 1;
        }

        /// <summary>
        /// Whether this vector sorts before <paramref name="j"/> (row-major: Y then X).
        /// 本向量是否排在 <paramref name="j"/> 之前（先行后列：先 Y 后 X）。
        /// </summary>
        /// <param name="j">Other vector; 另一向量</param>
        /// <returns><c>true</c> if this is less in sort order; 排序意义下小于对方则为 <c>true</c></returns>
        public bool Less(KernelVector j)
        {
            if (Y == j.Y)
            {
                return X < j.X;
            }

            return Y < j.Y;
        }
    }
}
