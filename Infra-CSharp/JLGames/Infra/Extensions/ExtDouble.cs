using System;

namespace JLGames.Infra.Extensions
{
    public static class ExtDouble
    {
        private static double DOUBLE_DELTA = 1E-6;

        /// <summary>
        /// Determine whether two double data are similar (equal)
        /// 判断两个double数据是否相近(相等)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool DoubleEquals(this double value, double value2)
        {
            return value.Equals(value2) || Math.Abs(value - value2) < DOUBLE_DELTA;
        }
    }
}