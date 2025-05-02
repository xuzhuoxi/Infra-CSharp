using System;

namespace JLGames.Infra.Imagex
{
    /// <summary>
    /// Vector core
    /// 向量核心
    /// </summary>
    public class FilterKernel : ICloneable<FilterKernel>
    {
        internal KernelVector[] Vectors;

        public int Len => Vectors?.Length ?? 0;

        public bool Less(int i, int j)
        {
            return Vectors[i].Less(Vectors[j]);
        }

        public bool Less(KernelVector i, KernelVector j)
        {
            return i.Less(j);
        }

        /// <summary>
        /// Swap data
        /// 交换数据
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void Swap(int i, int j)
        {
            var temp = Vectors[i];
            Vectors[i] = Vectors[j];
            Vectors[j] = temp;
        }

        public int IndexOfValue(int value)
        {
            for (var index = 0; index < Vectors.Length; index++)
            {
                if (Vectors[index].Value == value) return index;
            }

            return -1;
        }

        public FilterKernel Clone()
        {
            var units = Vectors?.Clone() as KernelVector[];
            return new FilterKernel {Vectors = units};
        }

        /// <summary>
        /// Flip itself upside down
        /// 上下翻转自身
        /// </summary>
        public void FlipUpDownSelf()
        {
            for (var index = 0; index < Vectors.Length; index++)
            {
                if (0 == Vectors[index].Y)
                {
                    continue;
                }

                Vectors[index].Y = -Vectors[index].Y;
            }
        }

        /// <summary>
        /// Flip Upside down
        /// 上下翻转
        /// </summary>
        /// <returns></returns>
        public FilterKernel FlipUuDown()
        {
            var rs = Clone();
            rs.FlipUpDownSelf();
            return rs;
        }

        /// <summary>
        /// Flips itself left and right
        /// 左右翻转自身
        /// </summary>
        public void FlipLeftRightSelf()
        {
            for (var index = 0; index < Vectors.Length; index++)
            {
                if (0 == Vectors[index].X)
                {
                    continue;
                }

                Vectors[index].X = -Vectors[index].X;
            }
        }

        /// <summary>
        /// Flip left and right
        /// 左右翻转
        /// </summary>
        /// <returns></returns>
        public FilterKernel FlipLeftRight()
        {
            var rs = Clone();
            rs.FlipLeftRightSelf();
            return rs;
        }

        /// <summary>
        /// Rotate 90 degrees
        /// 旋转90度
        /// </summary>
        /// <param name="clockwise">是否为顺时针</param>
        public void Rotate90Self(bool clockwise)
        {
            int x;
            int y;
            if (clockwise)
            {
                for (var index = 0; index < Vectors.Length; index++)
                {
                    x = -Vectors[index].Y;
                    y = Vectors[index].X;
                    Vectors[index].X = x;
                    Vectors[index].Y = y;
                }
            }
            else
            {
                for (var index = 0; index < Vectors.Length; index++)
                {
                    x = Vectors[index].Y;
                    y = -Vectors[index].X;
                    Vectors[index].X = x;
                    Vectors[index].Y = y;
                }
            }
        }

        /// <summary>
        /// Rotate 90 degrees
        /// 旋转90度
        /// </summary>
        /// <param name="clockwise">是否为顺时针</param>
        /// <returns></returns>
        public FilterKernel Rotate90(bool clockwise)
        {
            var rs = Clone();
            rs.Rotate90Self(clockwise);
            return rs;
        }

        /// <summary>
        /// Rotate
        /// 旋转
        /// </summary>
        /// <param name="clockwise"></param>
        /// <param name="count90"></param>
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
        /// Rotate
        /// 旋转
        /// </summary>
        /// <param name="clockwise"></param>
        /// <param name="count90"></param>
        /// <returns></returns>
        public FilterKernel Rotate(bool clockwise, int count90)
        {
            var rs = Clone();
            rs.RotateSelf(clockwise, count90);
            return rs;
        }

        /// <summary>
        /// Sort
        /// 排序
        /// </summary>
        public void Sort()
        {
            Array.Sort(Vectors);
        }
    }
}