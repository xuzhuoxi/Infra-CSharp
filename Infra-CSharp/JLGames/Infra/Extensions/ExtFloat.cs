using System;

namespace JLGames.Infra.Extensions
{
    /// <summary>
    /// Float extension methods for approximate equality comparison.
    /// float 扩展方法：近似相等比较。
    /// </summary>
    public static class ExtFloat
    {
        private static double FLOAT_DELTA = 1E-6;

        /// <summary>
        /// Determine whether two float data are similar (equal)
        /// 判断两个float数据是否相近(相等)
        /// </summary>
        /// <param name="value">First value. 第一个值。</param>
        /// <param name="value2">Second value. 第二个值。</param>
        /// <returns>True if values are equal or within epsilon (1E-6). 相等或差值在 1E-6 以内时为 true。</returns>
        public static bool FloatEquals(this float value, float value2)
        {
            return value.Equals(value2) || Math.Abs(value - value2) < FLOAT_DELTA;
        }
    }
}
