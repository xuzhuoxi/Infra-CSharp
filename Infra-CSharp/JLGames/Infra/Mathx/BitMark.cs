namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// Precomputed single-bit and multi-bit masks for primitive integer types.
    /// 为基本整型预计算的单比特与多比特掩码。
    /// </summary>
    public static class BitMark
    {
        /// <summary>
        /// The number of bits corresponding to a byte data
        /// 一个byte数据对应的位数
        /// </summary>
        public const int BitsPerByte = 8;

        /// <summary>
        /// The number of bits corresponding to a ushort data
        /// 一个ushort数据对应的位数
        /// </summary>
        public const int BitsPerUshort = 16;

        /// <summary>
        /// The number of bits corresponding to a int data
        /// 一个int数据对应的位数
        /// </summary>
        public const int BitsPerInt = 32;

        /// <summary>
        /// The number of bits corresponding to a uint data
        /// 一个uint数据对应的位数
        /// </summary>
        public const int BitsPerUint = 32;

        /// <summary>
        /// The number of bits corresponding to a ulong data
        /// 一个ulong数据对应的位数
        /// </summary>
        public const int BitsPerUlong = 64;

        private static byte[] m_ByteMarks;

        /// <summary>
        /// 8 code bit data corresponding to byte
        /// byte对应的8个码位数据
        /// </summary>
        static byte[] ByteMarks
        {
            get
            {
                if (null == m_ByteMarks)
                {
                    m_ByteMarks = GenByteMarks(BitsPerByte);
                }

                return m_ByteMarks;
            }
        }

        private static ushort[] m_UshortMarks;

        /// <summary>
        /// 16 code bit data corresponding to ushort
        /// ushort对应的16个码位数据
        /// </summary>
        static ushort[] UshortMarks
        {
            get
            {
                if (null == m_UshortMarks)
                {
                    m_UshortMarks = GenUshortMarks(BitsPerUshort);
                }

                return m_UshortMarks;
            }
        }

        private static int[] m_IntMarks;

        /// <summary>
        /// 32 code bit data corresponding to int
        /// int对应的32个码位数据
        /// </summary>
        static int[] IntMarks
        {
            get
            {
                if (null == m_IntMarks)
                {
                    m_IntMarks = GenIntMarks(BitsPerInt);
                }

                return m_IntMarks;
            }
        }

        private static uint[] m_UintMarks;

        /// <summary>
        /// 32 code bit data corresponding to uint
        /// uint对应的32个码位数据
        /// </summary>
        static uint[] UintMarks
        {
            get
            {
                if (null == m_UintMarks)
                {
                    m_UintMarks = GenUintMarks(BitsPerUint);
                }

                return m_UintMarks;
            }
        }

        private static ulong[] m_UlongMarks;

        /// <summary>
        /// 64 code bit data corresponding to ulong
        /// ulong对应的64个码位数据
        /// </summary>
        static ulong[] UlongMarks
        {
            get
            {
                if (null == m_UlongMarks)
                {
                    m_UlongMarks = GenUlongMarks(BitsPerUlong);
                }

                return m_UlongMarks;
            }
        }

        /// <summary>
        /// Returns a <see cref="byte"/> mask covering <paramref name="markLen"/> bits starting at <paramref name="markIndex"/>.
        /// 返回从 <paramref name="markIndex"/> 起连续 <paramref name="markLen"/> 位的 <see cref="byte"/> 掩码。
        /// </summary>
        public static byte GetByteMark(int markIndex, int markLen)
        {
            return (byte) GetIntMark(markIndex, markLen);
        }

        /// <summary>
        /// Returns the single-bit <see cref="byte"/> mask at <paramref name="markIndex"/>.
        /// 返回 <paramref name="markIndex"/> 处的单比特 <see cref="byte"/> 掩码。
        /// </summary>
        public static byte GetByteMark(int markIndex)
        {
            return ByteMarks[markIndex];
        }

        /// <summary>
        /// Returns a <see cref="ushort"/> mask covering <paramref name="markLen"/> bits starting at <paramref name="markIndex"/>.
        /// 返回从 <paramref name="markIndex"/> 起连续 <paramref name="markLen"/> 位的 <see cref="ushort"/> 掩码。
        /// </summary>
        public static ushort GetUshortMark(int markIndex, int markLen)
        {
            return (ushort) GetIntMark(markIndex, markLen);
        }

        /// <summary>
        /// Returns the single-bit <see cref="ushort"/> mask at <paramref name="markIndex"/>.
        /// 返回 <paramref name="markIndex"/> 处的单比特 <see cref="ushort"/> 掩码。
        /// </summary>
        public static ushort GetUshortMark(int markIndex)
        {
            return UshortMarks[markIndex];
        }

        /// <summary>
        /// Returns an <see cref="int"/> mask covering <paramref name="markLen"/> bits starting at <paramref name="markIndex"/>.
        /// 返回从 <paramref name="markIndex"/> 起连续 <paramref name="markLen"/> 位的 <see cref="int"/> 掩码。
        /// </summary>
        public static int GetIntMark(int markIndex, int markLen)
        {
            var rs = 0;
            for (var i = 0; i < markLen; i++)
            {
                rs = rs & GetIntMark(markIndex + i);
            }

            return rs;
        }

        /// <summary>
        /// Returns the single-bit <see cref="int"/> mask at <paramref name="markIndex"/>.
        /// 返回 <paramref name="markIndex"/> 处的单比特 <see cref="int"/> 掩码。
        /// </summary>
        public static int GetIntMark(int markIndex)
        {
            return IntMarks[markIndex];
        }

        /// <summary>
        /// Returns a <see cref="uint"/> mask covering <paramref name="markLen"/> bits starting at <paramref name="markIndex"/>.
        /// 返回从 <paramref name="markIndex"/> 起连续 <paramref name="markLen"/> 位的 <see cref="uint"/> 掩码。
        /// </summary>
        public static uint GetUintMark(int markIndex, int markLen)
        {
            uint rs = 0;
            for (var i = 0; i < markLen; i++)
            {
                rs = rs & GetUintMark(markIndex + i);
            }

            return rs;
        }

        /// <summary>
        /// Returns the single-bit <see cref="uint"/> mask at <paramref name="markIndex"/>.
        /// 返回 <paramref name="markIndex"/> 处的单比特 <see cref="uint"/> 掩码。
        /// </summary>
        public static uint GetUintMark(int markIndex)
        {
            return UintMarks[markIndex];
        }

        /// <summary>
        /// Returns a <see cref="ulong"/> mask covering <paramref name="markLen"/> bits starting at <paramref name="markIndex"/>.
        /// 返回从 <paramref name="markIndex"/> 起连续 <paramref name="markLen"/> 位的 <see cref="ulong"/> 掩码。
        /// </summary>
        public static ulong GetUlongMark(int markIndex, int markLen)
        {
            ulong rs = 0;
            for (var i = 0; i < markLen; i++)
            {
                rs = rs & GetUlongMark(markIndex + i);
            }

            return rs;
        }

        /// <summary>
        /// Returns the single-bit <see cref="ulong"/> mask at <paramref name="markIndex"/>.
        /// 返回 <paramref name="markIndex"/> 处的单比特 <see cref="ulong"/> 掩码。
        /// </summary>
        public static ulong GetUlongMark(int markIndex)
        {
            return UlongMarks[markIndex];
        }

        //初始化方法--------------------

        private static byte[] GenByteMarks(int markLen)
        {
            var marks = new byte[markLen];
            for (var i = 0; i < markLen; i++)
            {
                marks[i] = (byte) (1 << i);
            }

            return marks;
        }

        private static ushort[] GenUshortMarks(int markLen)
        {
            var marks = new ushort[markLen];
            for (var i = 0; i < markLen; i++)
            {
                marks[i] = (ushort) (1 << i);
            }

            return marks;
        }

        private static int[] GenIntMarks(int markLen)
        {
            var marks = new int[markLen];
            for (var i = 0; i < markLen; i++)
            {
                marks[i] = 1 << i;
            }

            return marks;
        }

        private static uint[] GenUintMarks(int markLen)
        {
            var marks = new uint[markLen];
            for (var i = 0; i < markLen; i++)
            {
                marks[i] = (uint) 1 << i;
            }

            return marks;
        }

        private static ulong[] GenUlongMarks(int markLen)
        {
            var marks = new ulong[markLen];
            for (var i = 0; i < markLen; i++)
            {
                marks[i] = (ulong) 1 << i;
            }

            return marks;
        }
    }
}