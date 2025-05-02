using System.Runtime.CompilerServices;

namespace JLGames.Infra.Utils
{
    public static class BitUtil
    {
        #region isValid

        /// <summary>
        /// Is it effective. 1 is valid, 0 is invalid
        /// 是否有效。1为效，0为无效
        /// </summary>
        /// <returns><c>true</c>, if valid was ised, <c>false</c> otherwise.</returns>
        /// <param name="value">value to be detected(待检测值)</param>
        /// <param name="bitIndex">Starting from the low order, the first bit index is 0(从低位开始，第1位索引为0)</param>
        public static bool IsValid(sbyte value, int bitIndex)
        {
            return 0 != ((1 << bitIndex) & value);
        }

        /// <summary>
        /// Is it effective. 1 is valid, 0 is invalid
        /// 是否有效。1为效，0为无效
        /// </summary>
        /// <returns><c>true</c>, if valid was ised, <c>false</c> otherwise.</returns>
        /// <param name="value">value to be detected(待检测值)</param>
        /// <param name="bitIndex">Starting from the low order, the first bit index is 0(从低位开始，第1位索引为0)</param>
        public static bool IsValid(ushort value, int bitIndex)
        {
            return 0 != ((1 << bitIndex) & value);
        }

        /// <summary>
        /// Is it effective. 1 is valid, 0 is invalid
        /// 是否有效。1为效，0为无效
        /// </summary>
        /// <returns><c>true</c>, if valid was ised, <c>false</c> otherwise.</returns>
        /// <param name="value">value to be detected(待检测值)</param>
        /// <param name="bitIndex">Starting from the low order, the first bit index is 0(从低位开始，第1位索引为0)</param>
        public static bool IsValid(int value, int bitIndex)
        {
            return 0 != ((1 << bitIndex) & value);
        }

        /// <summary>
        /// Is it effective. 1 is valid, 0 is invalid
        /// 是否有效。1为效，0为无效
        /// </summary>
        /// <returns><c>true</c>, if valid was ised, <c>false</c> otherwise.</returns>
        /// <param name="value">value to be detected(待检测值)</param>
        /// <param name="bitIndex">Starting from the low order, the first bit index is 0(从低位开始，第1位索引为0)</param>
        public static bool IsValid(uint value, int bitIndex)
        {
            return 0 != ((1u << bitIndex) & value);
        }

        /// <summary>
        /// Is it effective. 1 is valid, 0 is invalid
        /// 是否有效。1为效，0为无效
        /// </summary>
        /// <returns><c>true</c>, if valid was ised, <c>false</c> otherwise.</returns>
        /// <param name="value">value to be detected(待检测值)</param>
        /// <param name="bitIndex">Starting from the low order, the first bit index is 0(从低位开始，第1位索引为0)</param>
        public static bool IsValid(long value, int bitIndex)
        {
            return 0 != ((1L << bitIndex) & value);
        }

        /// <summary>
        /// Is it effective. 1 is valid, 0 is invalid
        /// 是否有效。1为效，0为无效
        /// </summary>
        /// <returns><c>true</c>, if valid was ised, <c>false</c> otherwise.</returns>
        /// <param name="value">value to be detected(待检测值)</param>
        /// <param name="bitIndex">Starting from the low order, the first bit index is 0(从低位开始，第1位索引为0)</param>
        public static bool IsValid(ulong value, int bitIndex)
        {
            return 0UL != ((1UL << bitIndex) & value);
        }

        #endregion

        #region isValidAnd

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(sbyte value, int bitIndex, params int[] otherIndexs)
        {
            var bits = (sbyte) Get32Bits1(bitIndex, otherIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(sbyte value, int[] bitIndexs)
        {
            var bits = (sbyte) Get32Bits1(bitIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(ushort value, int bitIndex, params int[] otherIndexs)
        {
            var bits = (ushort) Get32Bits1(bitIndex, otherIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(ushort value, int[] bitIndexs)
        {
            var bits = (ushort) Get32Bits1(bitIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(int value, int bitIndex, params int[] otherIndexs)
        {
            var bits = (int) Get32Bits1(bitIndex, otherIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(int value, int[] bitIndexs)
        {
            var bits = (int) Get32Bits1(bitIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(uint value, int bitIndex, params int[] otherIndexs)
        {
            var bits = Get32Bits1(bitIndex, otherIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(uint value, int[] bitIndexs)
        {
            var bits = Get32Bits1(bitIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(long value, int bitIndex, params int[] otherIndexs)
        {
            var bits = (long) Get64Bits1(bitIndex, otherIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(long value, int[] bitIndexs)
        {
            var bits = (long) Get64Bits1(bitIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(ulong value, int bitIndex, params int[] otherIndexs)
        {
            var bits = Get64Bits1(bitIndex, otherIndexs);
            return (value & bits) == bits;
        }

        /// <summary>
        /// Check if all bits are valid
        /// 检查位是否全部为有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidAnd(ulong value, int[] bitIndexs)
        {
            var bits = Get64Bits1(bitIndexs);
            return (value & bits) == bits;
        }

        #endregion

        #region isValidOr

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(sbyte value, int bitIndex, params int[] otherIndexs)
        {
            var bits = Get32Bits1(bitIndex, otherIndexs);
            return (value & (sbyte) bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(sbyte value, int[] bitIndexs)
        {
            var bits = Get32Bits1(bitIndexs);
            return (value & (sbyte) bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(ushort value, int bitIndex, params int[] otherIndexs)
        {
            var bits = Get32Bits1(bitIndex, otherIndexs);
            return (value & (ushort) bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(ushort value, int[] bitIndexs)
        {
            var bits = Get32Bits1(bitIndexs);
            return (value & (ushort) bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(int value, int bitIndex, params int[] otherIndexs)
        {
            var bits = Get32Bits1(bitIndex, otherIndexs);
            return (value & (int) bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(int value, int[] bitIndexs)
        {
            var bits = Get32Bits1(bitIndexs);
            return (value & (int) bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(uint value, int bitIndex, params int[] otherIndexs)
        {
            var bits = Get32Bits1(bitIndex, otherIndexs);
            return (value & bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(uint value, int[] bitIndexs)
        {
            var bits = Get32Bits1(bitIndexs);
            return (value & bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(long value, int bitIndex, params int[] otherIndexs)
        {
            var bits = Get64Bits1(bitIndex, otherIndexs);
            return (value & (long) bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(long value, int[] bitIndexs)
        {
            var bits = Get64Bits1(bitIndexs);
            return (value & (long) bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="otherIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(ulong value, int bitIndex, params int[] otherIndexs)
        {
            var bits = Get64Bits1(bitIndex, otherIndexs);
            return (value & bits) > 0;
        }

        /// <summary>
        /// Check if one of the bits is valid
        /// 检查位是否其中一个有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <returns></returns>
        public static bool IsValidOr(ulong value, int[] bitIndexs)
        {
            var bits = Get64Bits1(bitIndexs);
            return (value & bits) > 0;
        }

        #endregion

        #region setValid

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static sbyte SetValid(sbyte value, int bitIndex, bool isValid)
        {
            if (isValid)
                return (sbyte) (value | (sbyte) Get32Bits1(bitIndex));
            else
                return (sbyte) (value & (sbyte) Get32Bits0(bitIndex));
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的多个指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static sbyte SetValid(sbyte value, int[] bitIndexs, bool isValid)
        {
            if (null == bitIndexs || bitIndexs.Length == 0) return value;
            if (isValid)
                return (sbyte) (value | (sbyte) Get32Bits1(bitIndexs));
            else
                return (sbyte) (value & (sbyte) Get32Bits0(bitIndexs));
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static ushort SetValid(ushort value, int bitIndex, bool isValid)
        {
            if (isValid)
                return (ushort) (value | (ushort) Get32Bits1(bitIndex));
            else
                return (ushort) (value & (ushort) Get32Bits0(bitIndex));
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的多个指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static ushort SetValid(ushort value, int[] bitIndexs, bool isValid)
        {
            if (isValid)
                return (ushort) (value | (ushort) Get32Bits1(bitIndexs));
            else
                return (ushort) (value & (ushort) Get32Bits0(bitIndexs));
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static int SetValid(int value, int bitIndex, bool isValid)
        {
            if (isValid)
                return value | (int) Get32Bits1(bitIndex);
            else
                return value & (int) Get32Bits0(bitIndex);
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的多个指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static int SetValid(int value, int[] bitIndexs, bool isValid)
        {
            if (isValid)
                return value | (int) Get32Bits1(bitIndexs);
            else
                return value & (int) Get32Bits0(bitIndexs);
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static uint SetValid(uint value, int bitIndex, bool isValid)
        {
            if (isValid)
                return value | Get32Bits1(bitIndex);
            else
                return value & Get32Bits0(bitIndex);
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的多个指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static uint SetValid(uint value, int[] bitIndexs, bool isValid)
        {
            if (isValid)
                return value | Get32Bits1(bitIndexs);
            else
                return value & Get32Bits0(bitIndexs);
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static long SetValid(long value, int bitIndex, bool isValid)
        {
            if (isValid)
                return value | (long) Get64Bits1(bitIndex);
            else
                return value & (long) Get64Bits0(bitIndex);
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的多个指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <param name="isValid"></param>
        /// <returns></returns> 
        public static long SetValid(long value, int[] bitIndexs, bool isValid)
        {
            if (isValid)
                return value | (long) Get64Bits1(bitIndexs);
            else
                return value & (long) Get64Bits0(bitIndexs);
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndex"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public static ulong SetValid(ulong value, int bitIndex, bool isValid)
        {
            if (isValid)
                return value | Get64Bits1(bitIndex);
            else
                return value & Get64Bits0(bitIndex);
        }

        /// <summary>
        /// Set a value to a specified bit of a value
        /// 针对值的多个指定位进行赋值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitIndexs"></param>
        /// <param name="isValid"></param>
        /// <returns></returns> 
        public static ulong SetValid(ulong value, int[] bitIndexs, bool isValid)
        {
            if (isValid)
                return value | Get64Bits1(bitIndexs);
            else
                return value & Get64Bits0(bitIndexs);
        }

        #endregion

        #region Bit Value Gen

        /// <summary>
        /// Generate 32-bit data, specify the subscript value of 1
        /// 生成32位数据，指定下标值为1
        /// </summary>
        /// <param name="index"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static uint Gen32BitValue1(int index, params int[] other)
        {
            return Get32Bits1(index, other);
        }

        /// <summary>
        /// Generate 32-bit data, specify the subscript value of 1
        /// 生成32位数据，指定下标值为1
        /// </summary>
        /// <param name="indexs"></param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static uint Gen32BitValue1(int[] indexs)
        {
            return Get32Bits1(indexs);
        }

        /// <summary>
        /// Generate 64-bit data, specify the subscript value of 1
        /// 生成64位数据，指定下标值为1
        /// </summary>
        /// <param name="index"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static ulong Gen64BitValue1(int index, params int[] other)
        {
            return Get64Bits1(index, other);
        }

        /// <summary>
        /// Generate 64-bit data, specify the subscript value of 1
        /// 生成64位数据，指定下标值为1
        /// </summary>
        /// <param name="indexs"></param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static ulong Gen64BitValue1(int[] indexs)
        {
            return Get64Bits1(indexs);
        }

        /// <summary>
        /// Generate 32-bit data, specify the subscript value of 0
        /// 生成32位数据，指定下标值为0
        /// </summary>
        /// <param name="index"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static uint Gen32BitValue0(int index, params int[] other)
        {
            return Get32Bits0(index, other);
        }

        /// <summary>
        /// Generate 32-bit data, specify the subscript value of 0
        /// 生成32位数据，指定下标值为0
        /// </summary>
        /// <param name="indexs"></param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static uint Gen32BitValue0(int[] indexs)
        {
            return Get32Bits0(indexs);
        }

        /// <summary>
        /// Generate 64-bit data, specify the subscript value of 0
        /// 生成64位数据，指定下标值为0
        /// </summary>
        /// <param name="index"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static ulong Gen64BitValue0(int index, params int[] other)
        {
            return Get64Bits0(index, other);
        }

        /// <summary>
        /// Generate 64-bit data, specify the subscript value of 0
        /// 生成64位数据，指定下标值为0
        /// </summary>
        /// <param name="indexs"></param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions) 256)]
        public static ulong Gen64BitValue0(int[] indexs)
        {
            return Get64Bits0(indexs);
        }

        #endregion

        private static uint Get32Bits1(int index, params int[] other)
        {
            var rs = 1U << index;
            if (0 == other.Length) return rs;
            for (var idx = 0; idx < other.Length; idx++)
            {
                rs = rs | (1U << other[idx]);
            }

            return rs;
        }

        private static uint Get32Bits1(int[] indexs)
        {
            if (null == indexs || indexs.Length == 0) return 0;
            var rs = 0U;
            for (var idx = 0; idx < indexs.Length; idx++)
            {
                rs = rs | (1U << indexs[idx]);
            }

            return rs;
        }

        private static ulong Get64Bits1(int index, params int[] other)
        {
            var rs = 1UL << index;
            if (0 == other.Length) return rs;
            for (var idx = 0; idx < other.Length; idx++)
            {
                rs = rs | (1UL << other[idx]);
            }

            return rs;
        }

        private static ulong Get64Bits1(int[] indexs)
        {
            if (null == indexs || indexs.Length == 0) return 0;
            var rs = 0UL;
            for (var idx = 0; idx < indexs.Length; idx++)
            {
                rs = rs | (1UL << indexs[idx]);
            }

            return rs;
        }

        private static uint Get32Bits0(int index, params int[] other)
        {
            return ~Get32Bits1(index, other);
        }

        private static uint Get32Bits0(int[] indexs)
        {
            return ~Get32Bits1(indexs);
        }

        private static ulong Get64Bits0(int index, params int[] other)
        {
            return ~Get64Bits1(index, other);
        }

        private static ulong Get64Bits0(int[] indexs)
        {
            return ~Get64Bits1(indexs);
        }
    }
}