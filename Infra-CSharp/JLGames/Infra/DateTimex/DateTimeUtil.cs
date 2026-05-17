using System;
using System.Globalization;

namespace JLGames.Infra.DateTimex
{
    /// <summary>
    /// Date/time utilities: tick constants, conversions, current timestamps, calendar helpers, and formatting.
    /// 日期时间工具：Tick 常量、单位换算、当前时间戳、日历辅助与格式化。
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// Delegate: Get now timestamp.
        /// 代理委托：获取当前时间戳。
        /// </summary>
        public delegate long FuncGetNowTicks();

        /// <summary>
        /// Number of ticks in 1 microsecond
        /// 1微秒 对应 Ticks数量
        /// </summary>
        public const long TicksPerMicrosecond = 10;

        /// <summary>
        /// Number of ticks in 1 millisecond
        /// 1毫秒 对应 Ticks数量
        /// </summary>
        public const long TicksPerMilliSecond = TicksPerMicrosecond * 1000;

        /// <summary>
        /// Number of ticks in 1 centisecond
        /// 1厘秒 对应 Ticks数量
        /// </summary>
        public const long TicksPerCentisecond = TicksPerMilliSecond * 10;

        /// <summary>
        /// Number of ticks in 1 second
        /// 1秒 对应 Ticks数量
        /// </summary>
        public const long TicksPerSecond = TicksPerMilliSecond * 1000;

        /// <summary>
        /// Number of ticks in 1 minute
        /// 1分钟 对应 Ticks数量
        /// </summary>
        public const long TicksPerMinute = TicksPerSecond * 60;

        /// <summary>
        /// Number of ticks in 1 hour
        /// 1小时 对应 Ticks数量
        /// </summary>
        public const long TicksPerHour = TicksPerMinute * 60;

        /// <summary>
        /// Number of ticks in 1 day
        /// 1天 对应 Ticks数量
        /// </summary>
        public const long TicksPerDay = TicksPerHour * 24;

        /// <summary>
        /// Number of ticks in a 28-day month
        /// 一个28天的月份 对应 Ticks数量
        /// </summary>
        public const long TicksPerMonth28 = TicksPerDay * 28;

        /// <summary>
        /// Number of ticks in a 29-day month
        /// 一个29天的月份 对应 Ticks数量
        /// </summary>
        public const long TicksPerMonth29 = TicksPerDay * 29;

        /// <summary>
        /// Number of ticks in a 30-day month
        /// 一个30天的月份 对应 Ticks数量
        /// </summary>
        public const long TicksPerMonth30 = TicksPerDay * 30;

        /// <summary>
        /// Number of ticks in a 31-day month
        /// 一个31天的月份 对应 Ticks数量
        /// </summary>
        public const long TicksPerMonth31 = TicksPerDay * 31;

        /// <summary>
        /// Number of ticks in a year
        /// 1年 对应 Ticks数量
        /// </summary>
        public const long TicksPerYear = TicksPerDay * 365;

        /// <summary>
        /// Number of ticks in a leap year
        /// 1闰年 对应 Ticks数量
        /// </summary>
        public const long TicksPerYear2 = TicksPerDay * 366;

        /// <summary>
        /// Number of ticks in a week
        /// 1周 对应 Ticks数量
        /// </summary>
        public const long TicksPerWeak = TicksPerDay * 7;

        //------------------------------------------

        private static readonly DateTime s_DateTimeSimpleZero = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private static readonly DateTime s_DateTimeZero = new DateTime(0, DateTimeKind.Utc);

        /// <summary>
        /// The timestamp (tick) of 1970.1.1
        /// 1970.1.1的时间戳(Ticks)
        /// </summary>
        public static long Ticks1970 => s_DateTimeSimpleZero.Ticks;

        /// <summary>
        /// The current timestamp (tick) from 0001.1.1
        /// 距离0001.1.1的当前时间戳(Ticks)
        /// </summary>
        public static long NowTicks => DateTime.UtcNow.Ticks - s_DateTimeZero.Ticks;

        /// <summary>
        /// The current timestamp (tick) from 1970.1.1
        /// 距离1970.1.1的当前时间戳(Ticks)
        /// </summary>
        public static long NowTicks1970 => DateTime.UtcNow.Ticks - s_DateTimeSimpleZero.Ticks;

        /// <summary>
        /// The current timestamp (nano) from 0001.1.1
        /// 距离0001.1.1的当前时间戳(纳秒)
        /// </summary>
        public static long NowNanoseconds => Ticks2Nanos(NowTicks);

        /// <summary>
        /// The current timestamp (nano) from 1970.1.1
        /// 距离1970.1.1的当前时间戳(纳秒)
        /// </summary>
        public static long NowNanoseconds1970 => Ticks2Nanos(NowTicks1970);

        /// <summary>
        /// The current timestamp (millisecond) from 0001.1.1
        /// 距离0001.1.1的当前时间戳(毫秒)
        /// </summary>
        public static long NowMilliseconds => Ticks2Millis(NowTicks);

        /// <summary>
        /// The current timestamp (millisecond) from 1970.1.1
        /// 距离1970.1.1的当前时间戳(毫秒)
        /// </summary>
        public static long NowMilliseconds1970 => Ticks2Millis(NowTicks1970);

        /// <summary>
        /// The current timestamp (second) from 0001.1.1
        /// 距离0001.1.1的当前时间戳(秒)
        /// </summary>
        public static long NowSeconds => Ticks2Seconds(NowTicks);

        /// <summary>
        /// The current timestamp (second) from 1970.1.1
        /// 距离1970.1.1的当前时间戳(秒)
        /// </summary>
        public static long NowSeconds1970 => Ticks2Seconds(NowTicks1970);

        /// <summary>
        /// The current timestamp (minute) from 0001.1.1
        /// 距离0001.1.1的当前时间戳(分钟)
        /// </summary>
        public static long NowMinutes => Ticks2Minutes(NowTicks);

        /// <summary>
        /// The current timestamp (minute) from 1970.1.1
        /// 距离1970.1.1的当前时间戳(分钟)
        /// </summary>
        public static long NowMinutes1970 => Ticks2Minutes(NowTicks1970);

        /// <summary>
        /// The current timestamp (hour) from 0001.1.1
        /// 距离0001.1.1的当前时间戳(小时)
        /// </summary>
        public static long NowHours => Ticks2Hours(NowTicks);

        /// <summary>
        /// The current timestamp (hour) from 1970.1.1
        /// 距离1970.1.1的当前时间戳(小时)
        /// </summary>
        public static long NowHours1970 => Ticks2Hours(NowTicks1970);

        /// <summary>
        /// Ticks to nanoseconds
        /// Tick数 转 纳秒数
        /// </summary>
        /// <param name="ticks">Tick count to convert. 待转换的 Tick 数。</param>
        /// <returns>Nanoseconds. 纳秒数。</returns>
        public static long Ticks2Nanos(long ticks)
        {
            return ticks * 100;
        }

        /// <summary>
        /// Ticks to milliseconds
        /// Tick数 转 毫秒数
        /// </summary>
        /// <param name="ticks">Tick count to convert. 待转换的 Tick 数。</param>
        /// <returns>Milliseconds. 毫秒数。</returns>
        public static long Ticks2Millis(long ticks)
        {
            return ticks / TicksPerMilliSecond;
        }

        /// <summary>
        /// Ticks to seconds
        /// Tick数 转 秒数
        /// </summary>
        /// <param name="ticks">Tick count to convert. 待转换的 Tick 数。</param>
        /// <returns>Seconds. 秒数。</returns>
        public static long Ticks2Seconds(long ticks)
        {
            return ticks / TicksPerSecond;
        }

        /// <summary>
        /// Ticks to minutes
        /// Tick数 转 分钟数
        /// </summary>
        /// <param name="ticks">Tick count to convert. 待转换的 Tick 数。</param>
        /// <returns>Minutes. 分钟数。</returns>
        public static long Ticks2Minutes(long ticks)
        {
            return ticks / TicksPerMinute;
        }

        /// <summary>
        /// Ticks to hours
        /// Tick数 转 小时数
        /// </summary>
        /// <param name="ticks">Tick count to convert. 待转换的 Tick 数。</param>
        /// <returns>Hours. 小时数。</returns>
        public static long Ticks2Hours(long ticks)
        {
            return ticks / TicksPerHour;
        }

        /// <summary>
        /// Nanoseconds to ticks
        /// 纳秒数 转 Tick数
        /// </summary>
        /// <param name="nanos">Nanoseconds to convert. 待转换的纳秒数。</param>
        /// <returns>Ticks. Tick 数。</returns>
        public static long Nanos2Ticks(long nanos)
        {
            return nanos / 100;
        }

        /// <summary>
        /// Nanoseconds to ticks
        /// 毫秒数 转 Tick数
        /// </summary>
        /// <param name="millisecond"></param>
        /// <returns></returns>
        public static long Millis2Ticks(long millisecond)
        {
            return millisecond * TicksPerMinute;
        }

        /// <summary>
        /// Seconds to ticks
        /// 秒数 转 Tick数
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static long Seconds2Ticks(long seconds)
        {
            return seconds * TicksPerSecond;
        }

        /// <summary>
        /// Minutes to ticks
        /// 分钟数 转 Tick数
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static long Minutes2Ticks(long minutes)
        {
            return minutes * TicksPerMinute;
        }

        /// <summary>
        /// Hours to ticks
        /// 小时数 转 Tick数
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static long Hours2Ticks(long hours)
        {
            return hours * TicksPerHour;
        }

        //--------------------

        private static readonly int[] s_MonthDay = {31, 0, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}; // 0 means not sure; 0代表不确定

        /// <summary>
        /// Get the number of days in each month
        /// 取每个月份的天数
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetMonthDay(int month, int year)
        {
            if (month != 2)
            {
                return s_MonthDay[month - 1];
            }

            return CheckLeapYear(year) ? 29 : 28;
        }

        /// <summary>
        /// Check for leap year
        /// 判断是否闰年
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool CheckLeapYear(int year)
        {
            if (year % 4 == 0 && year % 100 != 0)
                return true;

            if (year % 400 == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Calculate the month offsets between two time points
        /// 计算两个时间点的月份差
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int GetOffsetMonth(DateTime start, DateTime end)
        {
            if (start > end)
            {
                (start, end) = (end, start);
            }

            var sy = start.Year;
            var ey = start.Year;

            if (sy == ey)
            {
                return end.Month - start.Month;
            }

            var sm = start.Month;
            var em = end.Month;

            if (em >= sm)
            {
                return (ey - sy) * 12 + em - sm;
            }

            return (ey - sy - 1) * 12 + (end.Month + 12) - start.Month;
        }

        //--------------------

        /// <summary>
        /// Format time
        /// 格式化时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime dt, string format, IFormatProvider provider = null)
        {
            return dt.ToString(format, provider ?? DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Format timestamp
        /// 格式化时间戳
        /// </summary>
        /// <param name="timestamp">from 0001.01.01</param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string FormatDateTime(long timestamp, string format, IFormatProvider provider = null)
        {
            return new DateTime(timestamp, DateTimeKind.Utc).ToString(format, provider ?? DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Format timestamp
        /// 格式化时间戳
        /// </summary>
        /// <param name="timestamp">from 0001.01.01</param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string FormatDateTimeLocal(long timestamp, string format, IFormatProvider provider = null)
        {
            var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds((timestamp - Ticks1970) / TicksPerMilliSecond);
            var localTime = dateTimeOffset.ToLocalTime();
            return localTime.ToString(format, provider ?? DateTimeFormatInfo.InvariantInfo);
        }

        // 参数format格式详细用法：
        //    格式字符 关联属性/说明 
        //        d ShortDatePattern 
        //        D LongDatePattern 
        //        f 完整日期和时间（长日期和短时间） 
        //        F FullDateTimePattern（长日期和长时间） 
        //        g 常规（短日期和短时间） 
        //        G 常规（短日期和长时间） 
        //        m、M MonthDayPattern 
        //        r、R RFC1123Pattern 
        //        s 使用当地时间的 SortableDateTimePattern（基于 ISO 8601） 
        //        t ShortTimePattern 
        //        T LongTimePattern 
        //        u UniversalSortableDateTimePattern 用于显示通用时间的格式 
        //        U 使用通用时间的完整日期和时间（长日期和长时间） 
        //        y、Y YearMonthPattern 
        //    格式模式 说明 
        //        d 月中的某一天。一位数的日期没有前导零。 
        //        dd 月中的某一天。一位数的日期有一个前导零。 
        //        ddd 周中某天的缩写名称，在 AbbreviatedDayNames 中定义。 
        //        dddd 周中某天的完整名称，在 DayNames 中定义。 
        //        M 月份数字。一位数的月份没有前导零。 
        //        MM 月份数字。一位数的月份有一个前导零。 
        //        MMM 月份的缩写名称，在 AbbreviatedMonthNames 中定义。 
        //        MMMM 月份的完整名称，在 MonthNames 中定义。 
        //        y 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示不具有前导零的年份。 
        //        yy 不包含纪元的年份。如果不包含纪元的年份小于 10，则显示具有前导零的年份。 
        //        yyyy 包括纪元的四位数的年份。 
        //        gg 时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。 
        //        h 12 小时制的小时。一位数的小时数没有前导零。 
        //        hh 12 小时制的小时。一位数的小时数有前导零。 
        //        H 24 小时制的小时。一位数的小时数没有前导零。 
        //        HH 24 小时制的小时。一位数的小时数有前导零。 
        //        m 分钟。一位数的分钟数没有前导零。 
        //        mm 分钟。一位数的分钟数有一个前导零。 
        //        s 秒。一位数的秒数没有前导零。 
        //        ss 秒。一位数的秒数有一个前导零。 
        //        f 秒的小数精度为一位。其余数字被截断。 
        //        ff 秒的小数精度为两位。其余数字被截断。 
        //        fff 秒的小数精度为三位。其余数字被截断。 
        //        ffff 秒的小数精度为四位。其余数字被截断。 
        //        fffff 秒的小数精度为五位。其余数字被截断。 
        //        ffffff 秒的小数精度为六位。其余数字被截断。 
        //        fffffff 秒的小数精度为七位。其余数字被截断。 
        //        t 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项的第一个字符（如果存在）。 
        //        tt 在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项（如果存在）。 
        //        z 时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数没有前导零。例如，太平洋标准时间是“-8”。 为本地时区信息，与输入的时间无关
        //        zz 时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数有前导零。例如，太平洋标准时间是“-08”。 为本地时区信息，与输入的时间无关
        //        zzz 完整时区偏移量（“+”或“-”后面跟有小时和分钟）。一位数的小时数和分钟数有前导零。例如，太平洋标准时间是“-08:00”。 为本地时区信息，与输入的时间无关
        //        : 在 TimeSeparator 中定义的默认时间分隔符。 
        //        / 在 DateSeparator 中定义的默认日期分隔符。 
        //        % c 其中 c 是格式模式（如果单独使用）。如果格式模式与原义字符或其他格式模式合并，则可以省略“%”字符。 
        //        c 其中 c 是任意字符。照原义显示字符。若要显示反斜杠字符，请使用“\”。 
    }
}