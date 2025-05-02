using System;

namespace JLGames.Infra.Extensions
{
    public static class ExtFloat
    {
        private static double FLOAT_DELTA = 1E-6;

        /// <summary>
        /// Determine whether two float data are similar (equal)
        /// 判断两个float数据是否相近(相等)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool FloatEquals(this float value, float value2)
        {
            return value.Equals(value2) || Math.Abs(value - value2) < FLOAT_DELTA;
        }
    }
}