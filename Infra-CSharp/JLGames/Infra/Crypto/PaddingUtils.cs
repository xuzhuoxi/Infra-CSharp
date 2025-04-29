using System;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto
{
    public static class PaddingDelegate
    {
        /// <summary>
        /// 填充函数类型
        /// </summary>
        public delegate byte[] FuncPadding(byte[] data, int blockSize);

        /// <summary>
        /// 去填充函数类型
        /// </summary>
        public delegate byte[] FuncUnPadding(byte[] data);
    }

    public class PaddingUtils
    {
        
        /// <summary>
        /// PKCS7 Padding
        /// </summary>
        /// <param name="data"></param>
        /// <param name="blockSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] Pkcs7Padding(byte[] data, int blockSize)
        {
            if (blockSize <= 0)
            {
                throw new ArgumentException("blockSize must be positive", nameof(blockSize));
            }

            var paddingLen = blockSize - data.Length % blockSize;
            if (paddingLen == 0)
            {
                paddingLen = blockSize;
            }

            var padding = new byte[paddingLen];
            for (var i = 0; i < paddingLen; i++)
            {
                padding[i] = (byte)paddingLen;
            }

            var result = new byte[data.Length + paddingLen];
            Array.Copy(data, result, data.Length);
            Array.Copy(padding, 0, result, data.Length, paddingLen);

            return result;
        }

        public static byte[] Pkcs7UnPadding(byte[] data)
        {
            if (data.Length == 0)
            {
                throw new ArgumentException("data is empty", nameof(data));
            }

            int paddingLen = data[data.Length - 1];
            if (paddingLen > data.Length)
            {
                throw new ArgumentException("invalid padding", nameof(data));
            }

            var result = new byte[data.Length - paddingLen];
            Array.Copy(data, result, data.Length - paddingLen);
            return result;
        }

        /// <summary>
        /// Zero Padding
        /// </summary>
        /// <param name="data"></param>
        /// <param name="blockSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] ZeroPadding(byte[] data, int blockSize)
        {
            if (blockSize <= 0)
            {
                throw new ArgumentException("blockSize must be positive", nameof(blockSize));
            }

            var paddingLen = blockSize - data.Length % blockSize;
            if (paddingLen == 0)
            {
                paddingLen = blockSize;
            }

            var result = new byte[data.Length + paddingLen];
            Array.Copy(data, result, data.Length);
            return result;
        }

        public static byte[] ZeroUnPadding(byte[] data)
        {
            var i = data.Length - 1;
            while (i >= 0 && data[i] == 0)
            {
                i--;
            }

            var result = new byte[i + 1];
            Array.Copy(data, result, i + 1);
            return result;
        }

        /// <summary>
        /// ISO10126 Padding
        /// </summary>
        /// <param name="data"></param>
        /// <param name="blockSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] Iso10126Padding(byte[] data, int blockSize)
        {
            if (blockSize <= 0)
            {
                throw new ArgumentException("blockSize must be positive", nameof(blockSize));
            }

            var paddingLen = blockSize - data.Length % blockSize;
            if (paddingLen == 0)
            {
                paddingLen = blockSize;
            }

            var padding = new byte[paddingLen - 1];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(padding);
            }

            var result = new byte[data.Length + paddingLen];
            Array.Copy(data, result, data.Length);
            Array.Copy(padding, 0, result, data.Length, paddingLen - 1);
            result[result.Length - 1] = (byte)paddingLen;

            return result;
        }

        public static byte[] Iso10126UnPadding(byte[] data)
        {
            if (data.Length == 0)
            {
                throw new ArgumentException("data is empty", nameof(data));
            }

            var paddingLen = data[data.Length - 1];
            if (paddingLen > data.Length)
            {
                throw new ArgumentException("invalid padding", nameof(data));
            }

            var result = new byte[data.Length - paddingLen];
            Array.Copy(data, result, data.Length - paddingLen);
            return result;
        }

        /// <summary>
        /// ANSI X9.23 Padding
        /// </summary>
        /// <param name="data"></param>
        /// <param name="blockSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] AnsiX923Padding(byte[] data, int blockSize)
        {
            if (blockSize <= 0)
            {
                throw new ArgumentException("blockSize must be positive", nameof(blockSize));
            }

            var paddingLen = blockSize - data.Length % blockSize;
            if (paddingLen == 0)
            {
                paddingLen = blockSize;
            }

            var padding = new byte[paddingLen - 1];
            var result = new byte[data.Length + paddingLen];
            Array.Copy(data, result, data.Length);
            Array.Copy(padding, 0, result, data.Length, paddingLen - 1);
            result[result.Length - 1] = (byte)paddingLen;

            return result;
        }

        public static byte[] AnsiX923UnPadding(byte[] data)
        {
            if (data.Length == 0)
            {
                throw new ArgumentException("data is empty", nameof(data));
            }

            int paddingLen = data[data.Length - 1];
            if (paddingLen > data.Length)
            {
                throw new ArgumentException("invalid padding", nameof(data));
            }

            var result = new byte[data.Length - paddingLen];
            Array.Copy(data, result, data.Length - paddingLen);
            return result;
        }
    }
}