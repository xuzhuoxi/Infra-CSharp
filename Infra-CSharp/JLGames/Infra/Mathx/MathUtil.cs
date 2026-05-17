using System;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// Common math helpers: clamp, parity, remainder/modulo, distance, and float comparisons.
    /// 常用数学工具：钳制、奇偶、余数/取模、距离与浮点比较等。
    /// </summary>
    public static class MathUtil
    {
        /// <summary>
        ///   <para>Clamps the given value between the given minimum float and maximum float values.  Returns the given value if it is within the min and max range.</para>
        /// </summary>
        /// <param name="value">The floating point value to restrict inside the range defined by the min and max values.</param>
        /// <param name="min">The minimum floating point value to compare against.</param>
        /// <param name="max">The maximum floating point value to compare against.</param>
        /// <returns>
        ///   <para>The float result between the min and max values.</para>
        /// </returns>
        public static float Clamp(float value, float min, float max)
        {
            if ((double)value < (double)min)
                value = min;
            else if ((double)value > (double)max)
                value = max;
            return value;
        }

        /// <summary>
        ///   <para>Clamps the given value between a range defined by the given minimum integer and maximum integer values. Returns the given value if it is within min and max.</para>
        /// </summary>
        /// <param name="value">The integer point value to restrict inside the min-to-max range</param>
        /// <param name="min">The minimum integer point value to compare against.</param>
        /// <param name="max">The maximum  integer point value to compare against.</param>
        /// <returns>
        ///   <para>The int result between min and max values.</para>
        /// </returns>
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        /// <summary>
        /// Returns whether <paramref name="val"/> is strictly between <paramref name="a"/> and <paramref name="b"/> (endpoints excluded; order of a/b does not matter).
        /// 判断 <paramref name="val"/> 是否严格位于 <paramref name="a"/> 与 <paramref name="b"/> 之间（不含端点；a、b 大小无关）。
        /// </summary>
        public static bool Between(double val, double a, double b)
        {
            if (a > b)
            {
                (a, b) = (b, a);
            }

            return val > a && val < b;
        }

        /// <summary>
        /// Returns whether <paramref name="val"/> is strictly between <paramref name="a"/> and <paramref name="b"/> (endpoints excluded; order of a/b does not matter).
        /// 判断 <paramref name="val"/> 是否严格位于 <paramref name="a"/> 与 <paramref name="b"/> 之间（不含端点；a、b 大小无关）。
        /// </summary>
        public static bool Between(float val, float a, float b)
        {
            if (a > b)
            {
                (a, b) = (b, a);
            }

            return val > a && val < b;
        }

        /// <summary>
        /// Returns whether <paramref name="val"/> is strictly between <paramref name="a"/> and <paramref name="b"/> (endpoints excluded; order of a/b does not matter).
        /// 判断 <paramref name="val"/> 是否严格位于 <paramref name="a"/> 与 <paramref name="b"/> 之间（不含端点；a、b 大小无关）。
        /// </summary>
        public static bool Between(int val, int a, int b)
        {
            if (a > b)
            {
                (a, b) = (b, a);
            }

            return val > a && val < b;
        }

        /// <summary>
        /// Returns whether <paramref name="val"/> is strictly between <paramref name="a"/> and <paramref name="b"/> (endpoints excluded; order of a/b does not matter).
        /// 判断 <paramref name="val"/> 是否严格位于 <paramref name="a"/> 与 <paramref name="b"/> 之间（不含端点；a、b 大小无关）。
        /// </summary>
        public static bool Between(long val, long a, long b)
        {
            if (a > b)
            {
                (a, b) = (b, a);
            }

            return val > a && val < b;
        }

        /// <summary>
        ///   <para>Clamps value between 0 and 1 and returns value.</para>
        /// </summary>
        /// <param name="value"></param>
        public static float Clamp01(float value)
        {
            if ((double)value < 0.0)
                return 0.0f;
            if ((double)value > 1.0)
                return 1f;
            return value;
        }

        /// <summary>
        /// Is it an odd number
        /// 是否为奇数
        /// </summary>
        /// <returns><c>true</c>, if odd was ised, <c>false</c> otherwise.</returns>
        /// <param name="num">Number.</param>
        public static bool IsOdd(int num)
        {
            return Convert.ToBoolean(num & 1);
        }

        /// <summary>
        /// Floor to even
        /// 向下偶数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int FloorToEven(float number)
        {
            var floorNum = (int)Math.Floor(number);
            return IsOdd(floorNum) ? floorNum - 1 : floorNum;
        }

        /// <summary>
        /// Floor to even
        /// 向下奇数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int FloorToOdd(float number)
        {
            var floorNum = (int)Math.Floor(number);
            return IsOdd(floorNum) ? floorNum : floorNum - 1;
        }

        /// <summary>
        /// Ceil to even
        /// 向上偶数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int CeilToEven(float number)
        {
            var ceilNum = (int)Math.Ceiling(number);
            return IsOdd(ceilNum) ? ceilNum + 1 : ceilNum;
        }

        /// <summary>
        /// Ceil to odd
        /// 向上奇数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int CeilToOdd(float number)
        {
            var ceilNum = (int)Math.Ceiling(number);
            return IsOdd(ceilNum) ? ceilNum : ceilNum + 1;
        }

        //---------------------------------------------

        /// <summary>
        /// Returns whether two floats are approximately equal (difference less than machine epsilon).
        /// 判断两个浮点数是否近似相等（差值小于机器精度）。
        /// </summary>
        /// <param name="a">First value.<br/>第一个值。</param>
        /// <param name="b">Second value.<br/>第二个值。</param>
        /// <param name="epsilon">Unused; kept for API compatibility.<br/>未使用，保留以兼容 API。</param>
        public static bool IsSimilar(float a, float b, float epsilon = float.Epsilon)
        {
            return Math.Abs(a - b) < Math.Abs(float.Epsilon);
        }

        /// <summary>
        /// Returns whether two doubles are approximately equal (difference less than machine epsilon).
        /// 判断两个双精度浮点数是否近似相等（差值小于机器精度）。
        /// </summary>
        /// <param name="a">First value.<br/>第一个值。</param>
        /// <param name="b">Second value.<br/>第二个值。</param>
        /// <param name="epsilon">Unused; kept for API compatibility.<br/>未使用，保留以兼容 API。</param>
        public static bool IsSimilar(double a, double b, double epsilon = double.Epsilon)
        {
            return Math.Abs(a - b) < Math.Abs(double.Epsilon);
        }

        /// <summary>
        /// Floors to int with a small positive bias from <paramref name="epsilon"/> to reduce boundary errors.
        /// 向下取整为 int，通过 <paramref name="epsilon"/> 施加微小正偏置以降低边界误差。
        /// </summary>
        public static int FloorToInt(this float a, float epsilon = float.Epsilon)
        {
            return (int)Math.Floor(a + Math.Abs(epsilon));
        }

        /// <summary>
        /// Floors to int with a small positive bias from <paramref name="epsilon"/> to reduce boundary errors.
        /// 向下取整为 int，通过 <paramref name="epsilon"/> 施加微小正偏置以降低边界误差。
        /// </summary>
        public static int FloorToInt(this double a, double epsilon = double.Epsilon)
        {
            return (int)Math.Floor(a + Math.Abs(epsilon));
        }

        /// <summary>
        /// Ceils to int with a small negative bias from <paramref name="epsilon"/> to reduce boundary errors.
        /// 向上取整为 int，通过 <paramref name="epsilon"/> 施加微小负偏置以降低边界误差。
        /// </summary>
        public static int CeilToInt(this float a, float epsilon = float.Epsilon)
        {
            return (int)Math.Ceiling(a - Math.Abs(epsilon));
        }

        /// <summary>
        /// Ceils to int with a small negative bias from <paramref name="epsilon"/> to reduce boundary errors.
        /// 向上取整为 int，通过 <paramref name="epsilon"/> 施加微小负偏置以降低边界误差。
        /// </summary>
        public static int CeilToInt(this double a, double epsilon = double.Epsilon)
        {
            return (int)Math.Ceiling(a - Math.Abs(epsilon));
        }

        /// <summary>
        /// Rem, the result sign is the same as a
        /// 求余, 结果符号与a一致
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Rem(this int a, int b)
        {
            // C#中 %代表求余
            // 并不是全部编程语言中%都是求余的，有部分是求模，例如python
            return a % b;
        }

        /// <summary>
        /// Mod, the result sign is the same as b
        /// 求模, 结果符号与b一致
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Mod(this int a, int b)
        {
            var c = (int)Math.Floor((double)a / b);
            return a - c * b;
        }

        /// <summary>
        /// Rem, the result sign is the same as a
        /// 求余, 结果符号与a一致
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Rem(this double a, double b)
        {
            // C#中 %代表求余
            // 并不是全部编程语言中%都是求余的，有部分是求模，例如python
            return a - (int)(a / b) * b;
        }

        /// <summary>
        /// Mod, the result sign is the same as b
        /// 求模, 结果符号与b一致
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Mod(this double a, double b)
        {
            return a - (int)Math.Floor(a / b) * b;
        }

        //距离相关

        /// <summary>
        /// Euclidean distance between two 2D points.
        /// 二维点之间的欧几里得距离。
        /// </summary>
        public static float Distance(float ax, float ay, float bx, float by)
        {
            var num1 = ax - bx;
            var num2 = ay - by;
            return (float)Math.Sqrt((double)num1 * num1 + (double)num2 * num2);
        }

        /// <summary>
        /// Squared Euclidean distance between two 2D points (avoids sqrt).
        /// 二维点之间欧几里得距离的平方（免开方）。
        /// </summary>
        public static float DistanceSquare(float ax, float ay, float bx, float by)
        {
            var num1 = ax - bx;
            var num2 = ay - by;
            return num1 * num1 + num2 * num2;
        }
    }
}