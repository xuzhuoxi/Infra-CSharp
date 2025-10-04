using System;

namespace JLGames.Infra.Mathx
{
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

        public static bool Between(double val, double a, double b)
        {
            if (a > b)
            {
                (a, b) = (b, a);
            }

            return val > a && val < b;
        }

        public static bool Between(float val, float a, float b)
        {
            if (a > b)
            {
                (a, b) = (b, a);
            }

            return val > a && val < b;
        }

        public static bool Between(int val, int a, int b)
        {
            if (a > b)
            {
                (a, b) = (b, a);
            }

            return val > a && val < b;
        }

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

        public static bool IsSimilar(float a, float b, float epsilon = float.Epsilon)
        {
            return Math.Abs(a - b) < Math.Abs(float.Epsilon);
        }

        public static bool IsSimilar(double a, double b, double epsilon = double.Epsilon)
        {
            return Math.Abs(a - b) < Math.Abs(double.Epsilon);
        }

        public static int FloorToInt(this float a, float epsilon = float.Epsilon)
        {
            return (int)Math.Floor(a + Math.Abs(epsilon));
        }

        public static int FloorToInt(this double a, double epsilon = double.Epsilon)
        {
            return (int)Math.Floor(a + Math.Abs(epsilon));
        }

        public static int CeilToInt(this float a, float epsilon = float.Epsilon)
        {
            return (int)Math.Ceiling(a - Math.Abs(epsilon));
        }

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

        public static float Distance(float ax, float ay, float bx, float by)
        {
            var num1 = ax - bx;
            var num2 = ay - by;
            return (float)Math.Sqrt((double)num1 * num1 + (double)num2 * num2);
        }

        public static float DistanceSquare(float ax, float ay, float bx, float by)
        {
            var num1 = ax - bx;
            var num2 = ay - by;
            return num1 * num1 + num2 * num2;
        }
    }
}