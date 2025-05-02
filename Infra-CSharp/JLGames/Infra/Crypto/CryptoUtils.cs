namespace JLGames.Infra.Crypto
{
    public static class CryptoUtils
    {
        /// <summary>
        /// 合并两个字节数组
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="bs1"></param>
        /// <returns></returns>
        public static byte[] Combine(byte[] bs, byte[] bs1)
        {
            var result = new byte[bs.Length + bs1.Length];
            System.Buffer.BlockCopy(bs, 0, result, 0, bs.Length);
            System.Buffer.BlockCopy(bs1, 0, result, bs.Length, bs1.Length);
            return result;
        }

        /// <summary>
        /// 拆分数组为两个
        /// </summary>
        /// <param name="data"></param>
        /// <param name="firstSize"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public static void Extract(byte[] data, int firstSize, out byte[] first, out byte[] second)
        {
            first = new byte[firstSize];
            second = new byte[data.Length - first.Length];
            System.Buffer.BlockCopy(data, 0, first, 0, firstSize);
            System.Buffer.BlockCopy(data, firstSize, second, 0, second.Length);
        }

        /// <summary>
        /// 合并多个字节数组
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="bs1"></param>
        /// <param name="bs2"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static byte[] CombineMulti(byte[] bs, byte[] bs1, byte[] bs2, params byte[][] others)
        {
            var len = bs.Length + bs1.Length + bs2.Length;
            if (others.Length > 0)
            {
                for (var i = 0; i < others.Length; i++)
                {
                    len += others[i].Length;
                }
            }

            var result = new byte[len];
            var index = 0;
            System.Buffer.BlockCopy(bs, 0, result, index, bs.Length);
            index += bs.Length;
            System.Buffer.BlockCopy(bs1, 0, result, index, bs1.Length);
            index += bs1.Length;
            System.Buffer.BlockCopy(bs2, 0, result, index, bs2.Length);
            index += bs2.Length;
            if (others.Length > 0)
            {
                for (var i = 0; i < others.Length; i++)
                {
                    System.Buffer.BlockCopy(others[i], 0, result, index, others[i].Length);
                    index += others[i].Length;
                }
            }

            return result;
        }
    }
}