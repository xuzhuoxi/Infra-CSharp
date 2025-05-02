using System;

namespace JLGames.Infra.Imagex
{
    /// <summary>
    /// Filter vector unit
    /// 滤波器向量单元
    /// </summary>
    public struct KernelVector : IComparable<KernelVector>
    {
        public int X, Y;
        public int Value;

        public int CompareTo(KernelVector j)
        {
            return Less(j) ? -1 : 1;
        }

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