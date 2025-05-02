using System.Text;

namespace JLGames.Infra.Utils
{
    public static class PrintUtil
    {
        /// <summary>
        /// stringify
        /// 字符串化
        /// </summary>
        /// <param name="o"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToStringText<T>(this T o)
        {
            if (o is int)
            {
                return $"{o}";
            }

            return o.ToString();
        }

        /// <summary>
        /// String representation of 2D array to string representation, mostly used for debugging or printing
        /// 二维数组转字符串表示，多用于调试或打印
        /// </summary>
        /// <param name="arr"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToStringText<T>(this T[] arr)
        {
            if (null == arr) return "null";
            if (0 == arr.Length) return "[]";
            return $"[{string.Join(",", arr)}]";
        }

        /// <summary>
        /// String representation of 3D array to string representation, mostly used for debugging or printing
        /// 三维数组转字符串表示，多用于调试或打印
        /// </summary>
        /// <param name="arr"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ToStringText<T>(this T[][] arr)
        {
            if (null == arr) return "null";
            if (0 == arr.Length) return "[]";
            var sb = new StringBuilder();
            foreach (var t in arr)
            {
                sb.Append($"\t{ToStringText(t)}\n");
            }

            return $"[\n{sb}]";
        }
    }
}