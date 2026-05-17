using System;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto
{
    /// <summary>
    /// 块密码填充/去填充的委托类型定义。
    /// </summary>
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

    /// <summary>
    /// Block padding and unpadding for symmetric ciphers.
    /// 对称加密常用的块填充与去填充实现
    /// </summary>
    public class PaddingUtils
    {
        /// <summary>
        /// PKCS#7 填充：不足块大小时在末尾追加填充字节，每个填充字节的值等于填充长度。
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="blockSize">块大小（字节），须为正数</param>
        /// <returns>填充后的数据</returns>
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

        /// <summary>
        /// 移除 PKCS#7 填充。
        /// </summary>
        /// <param name="data">含填充的数据</param>
        /// <returns>去填充后的数据</returns>
        /// <exception cref="ArgumentException">数据为空或填充非法</exception>
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
        /// 零填充：在末尾补 0 至块边界；若已对齐则补一整块。
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="blockSize">块大小（字节），须为正数</param>
        /// <returns>填充后的数据</returns>
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

        /// <summary>
        /// 移除末尾的零字节填充。
        /// </summary>
        /// <param name="data">含填充的数据</param>
        /// <returns>去填充后的数据</returns>
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
        /// ISO 10126 填充：随机字节 + 最后一字节为填充长度。
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="blockSize">块大小（字节），须为正数</param>
        /// <returns>填充后的数据</returns>
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

        /// <summary>
        /// 移除 ISO 10126 填充。
        /// </summary>
        /// <param name="data">含填充的数据</param>
        /// <returns>去填充后的数据</returns>
        /// <exception cref="ArgumentException">数据为空或填充非法</exception>
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
        /// ANSI X9.23 填充：前若干字节为 0，最后一字节为填充长度。
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="blockSize">块大小（字节），须为正数</param>
        /// <returns>填充后的数据</returns>
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

        /// <summary>
        /// 移除 ANSI X9.23 填充。
        /// </summary>
        /// <param name="data">含填充的数据</param>
        /// <returns>去填充后的数据</returns>
        /// <exception cref="ArgumentException">数据为空或填充非法</exception>
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