# DateTimex API 文档

## 命名空间: JLGames.Infra.DateTimex

### 静态类 (Static Classes)

#### DateTimeUtil
日期时间工具类

```csharp
/// <summary>
/// 日期时间工具类
/// 提供日期时间相关的工具方法和常量
/// 
/// 更多信息：
///   TimeSpan
/// </summary>
public static class DateTimeUtil
{
    /// <summary>
    /// 代理委托：获取当前时间戳。
    /// </summary>
    public delegate long FuncGetNowTicks();

    // 时间单位常量
    /// <summary>
    /// 1微秒 对应 Ticks数量
    /// </summary>
    public const long TicksPerMicrosecond = 10;

    /// <summary>
    /// 1毫秒 对应 Ticks数量
    /// </summary>
    public const long TicksPerMilliSecond = TicksPerMicrosecond * 1000;

    /// <summary>
    /// 1厘秒 对应 Ticks数量
    /// </summary>
    public const long TicksPerCentisecond = TicksPerMilliSecond * 10;

    /// <summary>
    /// 1秒 对应 Ticks数量
    /// </summary>
    public const long TicksPerSecond = TicksPerMilliSecond * 1000;

    /// <summary>
    /// 1分钟 对应 Ticks数量
    /// </summary>
    public const long TicksPerMinute = TicksPerSecond * 60;

    /// <summary>
    /// 1小时 对应 Ticks数量
    /// </summary>
    public const long TicksPerHour = TicksPerMinute * 60;

    /// <summary>
    /// 1天 对应 Ticks数量
    /// </summary>
    public const long TicksPerDay = TicksPerHour * 24;

    /// <summary>
    /// 一个28天的月份 对应 Ticks数量
    /// </summary>
    public const long TicksPerMonth28 = TicksPerDay * 28;

    /// <summary>
    /// 一个29天的月份 对应 Ticks数量
    /// </summary>
    public const long TicksPerMonth29 = TicksPerDay * 29;

    /// <summary>
    /// 一个30天的月份 对应 Ticks数量
    /// </summary>
    public const long TicksPerMonth30 = TicksPerDay * 30;

    /// <summary>
    /// 一个31天的月份 对应 Ticks数量
    /// </summary>
    public const long TicksPerMonth31 = TicksPerDay * 31;

    /// <summary>
    /// 1年 对应 Ticks数量
    /// </summary>
    public const long TicksPerYear = TicksPerDay * 365;

    /// <summary>
    /// 1闰年 对应 Ticks数量
    /// </summary>
    public const long TicksPerYear2 = TicksPerDay * 366;

    /// <summary>
    /// 1周 对应 Ticks数量
    /// </summary>
    public const long TicksPerWeak = TicksPerDay * 7;

    // 当前时间戳属性
    /// <summary>
    /// 1970.1.1的时间戳(Ticks)
    /// </summary>
    public static long Ticks1970 { get; }

    /// <summary>
    /// 距离0001.1.1的当前时间戳(Ticks)
    /// </summary>
    public static long NowTicks { get; }

    /// <summary>
    /// 距离1970.1.1的当前时间戳(Ticks)
    /// </summary>
    public static long NowTicks1970 { get; }

    /// <summary>
    /// 距离0001.1.1的当前时间戳(纳秒)
    /// </summary>
    public static long NowNanoseconds { get; }

    /// <summary>
    /// 距离1970.1.1的当前时间戳(纳秒)
    /// </summary>
    public static long NowNanoseconds1970 { get; }

    /// <summary>
    /// 距离0001.1.1的当前时间戳(毫秒)
    /// </summary>
    public static long NowMilliseconds { get; }

    /// <summary>
    /// 距离1970.1.1的当前时间戳(毫秒)
    /// </summary>
    public static long NowMilliseconds1970 { get; }

    /// <summary>
    /// 距离0001.1.1的当前时间戳(秒)
    /// </summary>
    public static long NowSeconds { get; }

    /// <summary>
    /// 距离1970.1.1的当前时间戳(秒)
    /// </summary>
    public static long NowSeconds1970 { get; }

    /// <summary>
    /// 距离0001.1.1的当前时间戳(分钟)
    /// </summary>
    public static long NowMinutes { get; }

    /// <summary>
    /// 距离1970.1.1的当前时间戳(分钟)
    /// </summary>
    public static long NowMinutes1970 { get; }

    /// <summary>
    /// 距离0001.1.1的当前时间戳(小时)
    /// </summary>
    public static long NowHours { get; }

    /// <summary>
    /// 距离1970.1.1的当前时间戳(小时)
    /// </summary>
    public static long NowHours1970 { get; }

    // 时间单位转换方法
    /// <summary>
    /// Tick数 转 纳秒数
    /// </summary>
    /// <param name="ticks">Tick数</param>
    /// <returns>纳秒数</returns>
    public static long Ticks2Nanos(long ticks);

    /// <summary>
    /// Ticks to milliseconds
    /// Tick数 转 毫秒数
    /// </summary>
    /// <param name="ticks">Tick数</param>
    /// <returns>毫秒数</returns>
    public static long Ticks2Millis(long ticks);

    /// <summary>
    /// Ticks to seconds
    /// Tick数 转 秒数
    /// </summary>
    /// <param name="ticks">Tick数</param>
    /// <returns>秒数</returns>
    public static long Ticks2Seconds(long ticks);

    /// <summary>
    /// Ticks to minutes
    /// Tick数 转 分钟数
    /// </summary>
    /// <param name="ticks">Tick数</param>
    /// <returns>分钟数</returns>
    public static long Ticks2Minutes(long ticks);

    /// <summary>
    /// Ticks to hours
    /// Tick数 转 小时数
    /// </summary>
    /// <param name="ticks">Tick数</param>
    /// <returns>小时数</returns>
    public static long Ticks2Hours(long ticks);

    /// <summary>
    /// Nanoseconds to ticks
    /// 纳秒数 转 Tick数
    /// </summary>
    /// <param name="nanos">纳秒数</param>
    /// <returns>Tick数</returns>
    public static long Nanos2Ticks(long nanos);

    /// <summary>
    /// Milliseconds to ticks
    /// 毫秒数 转 Tick数
    /// </summary>
    /// <param name="millisecond">毫秒数</param>
    /// <returns>Tick数</returns>
    public static long Millis2Ticks(long millisecond);

    /// <summary>
    /// Seconds to ticks
    /// 秒数 转 Tick数
    /// </summary>
    /// <param name="seconds">秒数</param>
    /// <returns>Tick数</returns>
    public static long Seconds2Ticks(long seconds);

    /// <summary>
    /// Minutes to ticks
    /// 分钟数 转 Tick数
    /// </summary>
    /// <param name="minutes">分钟数</param>
    /// <returns>Tick数</returns>
    public static long Minutes2Ticks(long minutes);

    /// <summary>
    /// Hours to ticks
    /// 小时数 转 Tick数
    /// </summary>
    /// <param name="hours">小时数</param>
    /// <returns>Tick数</returns>
    public static long Hours2Ticks(long hours);

    // 日期计算方法
    /// <summary>
    /// 获取指定月份的天数
    /// </summary>
    /// <param name="month">月份</param>
    /// <param name="year">年份</param>
    /// <returns>天数</returns>
    public static int GetMonthDay(int month, int year);

    /// <summary>
    /// 检查是否为闰年
    /// </summary>
    /// <param name="year">年份</param>
    /// <returns>是否为闰年</returns>
    public static bool CheckLeapYear(int year);

    /// <summary>
    /// 获取两个日期之间的月份差
    /// </summary>
    /// <param name="start">开始日期</param>
    /// <param name="end">结束日期</param>
    /// <returns>月份差</returns>
    public static int GetOffsetMonth(DateTime start, DateTime end);

    // 格式化方法
    /// <summary>
    /// 格式化日期时间
    /// </summary>
    /// <param name="dt">日期时间</param>
    /// <param name="format">格式</param>
    /// <param name="provider">格式提供者</param>
    /// <returns>格式化后的字符串</returns>
    public static string FormatDateTime(DateTime dt, string format, IFormatProvider provider = null);

    /// <summary>
    /// 格式化时间戳
    /// </summary>
    /// <param name="timestamp">时间戳</param>
    /// <param name="format">格式</param>
    /// <param name="provider">格式提供者</param>
    /// <returns>格式化后的字符串</returns>
    public static string FormatDateTime(long timestamp, string format, IFormatProvider provider = null);

    /// <summary>
    /// 格式化本地时间戳
    /// </summary>
    /// <param name="timestamp">时间戳</param>
    /// <param name="format">格式</param>
    /// <param name="provider">格式提供者</param>
    /// <returns>格式化后的字符串</returns>
    public static string FormatDateTimeLocal(long timestamp, string format, IFormatProvider provider = null);
}
```

#### TimeSeries
时间序列类

```csharp
/// <summary>
/// 时间序列类
/// 用于处理时间序列数据
/// </summary>
public class TimeSeries
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <param name="interval">时间间隔</param>
    public TimeSeries(DateTime startTime, DateTime endTime, TimeSpan interval);

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime StartTime { get; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime EndTime { get; }

    /// <summary>
    /// 时间间隔
    /// </summary>
    public TimeSpan Interval { get; }

    /// <summary>
    /// 时间点数量
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// 获取指定索引的时间点
    /// </summary>
    /// <param name="index">索引</param>
    /// <returns>时间点</returns>
    public DateTime this[int index] { get; }

    /// <summary>
    /// 获取时间序列迭代器
    /// </summary>
    /// <returns>时间序列迭代器</returns>
    public IEnumerator<DateTime> GetEnumerator();
}
```

#### TimeSlice
时间切片类

```csharp
/// <summary>
/// 时间切片类
/// 表示一个时间区间
/// </summary>
public struct TimeSlice
{
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="start">开始时间</param>
    /// <param name="end">结束时间</param>
    public TimeSlice(DateTime start, DateTime end);

    /// <summary>
    /// 持续时间
    /// </summary>
    public TimeSpan Duration { get; }

    /// <summary>
    /// 是否包含指定时间
    /// </summary>
    /// <param name="time">时间</param>
    /// <returns>是否包含</returns>
    public bool Contains(DateTime time);

    /// <summary>
    /// 是否与另一个时间切片重叠
    /// </summary>
    /// <param name="other">另一个时间切片</param>
    /// <returns>是否重叠</returns>
    public bool Overlaps(TimeSlice other);

    /// <summary>
    /// 获取交集
    /// </summary>
    /// <param name="other">另一个时间切片</param>
    /// <returns>交集时间切片</returns>
    public TimeSlice? Intersect(TimeSlice other);

    /// <summary>
    /// 获取并集
    /// </summary>
    /// <param name="other">另一个时间切片</param>
    /// <returns>并集时间切片</returns>
    public TimeSlice Union(TimeSlice other);
}
```

#### StampTimer
时间戳计时器类

```csharp
/// <summary>
    /// 时间戳计时器类
    /// 提供高精度的时间戳计时功能
    /// </summary>
    public class StampTimer
    {
        private long m_StartTicks;
        private long m_StopTicks;
        private bool m_IsRunning;

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning => m_IsRunning;

        /// <summary>
        /// 开始计时
        /// </summary>
        public void Start();

        /// <summary>
        /// 停止计时
        /// </summary>
        public void Stop();

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void Reset();

        /// <summary>
        /// 重启计时器
        /// </summary>
        public void Restart();

        /// <summary>
        /// 获取经过的毫秒数
        /// </summary>
        /// <returns>毫秒数</returns>
        public long GetElapsedMilliseconds();

        /// <summary>
        /// 获取经过的微秒数
        /// </summary>
        /// <returns>微秒数</returns>
        public long GetElapsedMicroseconds();

        /// <summary>
        /// 获取经过的纳秒数
        /// </summary>
        /// <returns>纳秒数</returns>
        public long GetElapsedNanoseconds();

        /// <summary>
        /// 获取经过的时间间隔
        /// </summary>
        /// <returns>时间间隔</returns>
        public TimeSpan GetElapsedTime();
    }
```

### 功能说明

#### 时间单位转换

**支持的时间单位**
- **纳秒**：最小时间单位，1纳秒 = 10 Ticks
- **微秒**：1微秒 = 10纳秒
- **毫秒**：1毫秒 = 1000微秒
- **秒**：1秒 = 1000毫秒
- **分钟**：1分钟 = 60秒
- **小时**：1小时 = 60分钟
- **天**：1天 = 24小时
- **周**：1周 = 7天
- **月**：28/29/30/31天
- **年**：365天（闰年366天）

**转换方法**
1. **Ticks转其他单位**：Ticks2Nanos, Ticks2Millis, Ticks2Seconds等
2. **其他单位转Ticks**：Nanos2Ticks, Millis2Ticks, Seconds2Ticks等
3. **双向转换**：支持所有时间单位的双向转换

#### 时间戳系统

**时间戳基准**
- **Unix时间戳**：从1970年1月1日开始的秒数
- **Ticks时间戳**：从0001年1月1日开始的Ticks数
- **毫秒时间戳**：从基准时间开始的毫秒数
- **纳秒时间戳**：从基准时间开始的纳秒数

**当前时间戳**
- **NowTicks**：距离0001.1.1的当前Ticks
- **NowTicks1970**：距离1970.1.1的当前Ticks
- **NowMilliseconds1970**：距离1970.1.1的当前毫秒数
- **NowSeconds1970**：距离1970.1.1的当前秒数

#### 日期计算

**月份天数计算**
- **GetMonthDay**：根据年月计算天数
- **CheckLeapYear**：检查是否为闰年
- **GetOffsetMonth**：计算两个日期间的月份差

**时间序列处理**
- **TimeSeries**：处理时间序列数据
- **TimeSlice**：表示时间区间
- **StampTimer**：高精度计时器

### 使用示例

#### 时间单位转换
```csharp
// 基本时间单位转换
long ticks = 1000000; // 1秒的Ticks数

long nanoseconds = DateTimeUtil.Ticks2Nanos(ticks);
long milliseconds = DateTimeUtil.Ticks2Millis(ticks);
long seconds = DateTimeUtil.Ticks2Seconds(ticks);
long minutes = DateTimeUtil.Ticks2Minutes(ticks);
long hours = DateTimeUtil.Ticks2Hours(ticks);

Console.WriteLine($"Ticks: {ticks}");
Console.WriteLine($"纳秒: {nanoseconds}");
Console.WriteLine($"毫秒: {milliseconds}");
Console.WriteLine($"秒: {seconds}");
Console.WriteLine($"分钟: {minutes}");
Console.WriteLine($"小时: {hours}");

// 反向转换
long backToTicks = DateTimeUtil.Nanos2Ticks(nanoseconds);
Console.WriteLine($"转换回Ticks: {backToTicks}");
```

#### 时间戳操作
```csharp
// 获取当前时间戳
long currentTicks = DateTimeUtil.NowTicks;
long currentTicks1970 = DateTimeUtil.NowTicks1970;
long currentMilliseconds = DateTimeUtil.NowMilliseconds1970;
long currentSeconds = DateTimeUtil.NowSeconds1970;

Console.WriteLine($"当前Ticks (0001基准): {currentTicks}");
Console.WriteLine($"当前Ticks (1970基准): {currentTicks1970}");
Console.WriteLine($"当前毫秒 (1970基准): {currentMilliseconds}");
Console.WriteLine($"当前秒 (1970基准): {currentSeconds}");

// 时间戳格式化
string formattedTime = DateTimeUtil.FormatDateTime(currentMilliseconds, "yyyy-MM-dd HH:mm:ss");
Console.WriteLine($"格式化时间: {formattedTime}");
```

#### 日期计算
```csharp
// 月份天数计算
int daysInFebruary2024 = DateTimeUtil.GetMonthDay(2, 2024);
int daysInFebruary2023 = DateTimeUtil.GetMonthDay(2, 2023);

Console.WriteLine($"2024年2月天数: {daysInFebruary2024}"); // 29 (闰年)
Console.WriteLine($"2023年2月天数: {daysInFebruary2023}"); // 28

// 闰年检查
bool isLeap2024 = DateTimeUtil.CheckLeapYear(2024);
bool isLeap2023 = DateTimeUtil.CheckLeapYear(2023);

Console.WriteLine($"2024是闰年: {isLeap2024}"); // True
Console.WriteLine($"2023是闰年: {isLeap2023}"); // False

// 月份差计算
DateTime startDate = new DateTime(2023, 1, 1);
DateTime endDate = new DateTime(2024, 6, 15);
int monthOffset = DateTimeUtil.GetOffsetMonth(startDate, endDate);

Console.WriteLine($"从 {startDate:yyyy-MM-dd} 到 {endDate:yyyy-MM-dd} 的月份差: {monthOffset}");
```

#### 时间序列处理
```csharp
// 创建时间序列
DateTime startTime = new DateTime(2024, 1, 1);
DateTime endTime = new DateTime(2024, 1, 31);
TimeSpan interval = TimeSpan.FromDays(1);

var timeSeries = new TimeSeries(startTime, endTime, interval);

Console.WriteLine($"时间序列长度: {timeSeries.Count}");
Console.WriteLine($"时间间隔: {interval}");

// 遍历时间序列
foreach (DateTime time in timeSeries)
{
    Console.WriteLine($"时间点: {time:yyyy-MM-dd}");
}
```

#### 时间切片操作
```csharp
// 创建时间切片
var slice1 = new TimeSlice(
    new DateTime(2024, 1, 1, 9, 0, 0),
    new DateTime(2024, 1, 1, 17, 0, 0)
);

var slice2 = new TimeSlice(
    new DateTime(2024, 1, 1, 14, 0, 0),
    new DateTime(2024, 1, 1, 18, 0, 0)
);

Console.WriteLine($"切片1: {slice1.Start:HH:mm} - {slice1.End:HH:mm}");
Console.WriteLine($"切片2: {slice2.Start:HH:mm} - {slice2.End:HH:mm}");
Console.WriteLine($"切片1持续时间: {slice1.Duration}");

// 检查重叠
bool overlaps = slice1.Overlaps(slice2);
Console.WriteLine($"是否重叠: {overlaps}");

// 获取交集
var intersection = slice1.Intersect(slice2);
if (intersection.HasValue)
{
    Console.WriteLine($"交集: {intersection.Value.Start:HH:mm} - {intersection.Value.End:HH:mm}");
}
```

#### 高精度计时器
```csharp
// 创建计时器
var timer = new StampTimer();

// 开始计时
timer.Start();

// 执行一些操作
Thread.Sleep(100); // 模拟耗时操作

// 停止计时
timer.Stop();

// 获取计时结果
long elapsedMs = timer.GetElapsedMilliseconds();
long elapsedMicros = timer.GetElapsedMicroseconds();
long elapsedNanos = timer.GetElapsedNanoseconds();
TimeSpan elapsedTime = timer.GetElapsedTime();

Console.WriteLine($"经过时间:");
Console.WriteLine($"  毫秒: {elapsedMs}");
Console.WriteLine($"  微秒: {elapsedMicros}");
Console.WriteLine($"  纳秒: {elapsedNanos}");
Console.WriteLine($"  时间间隔: {elapsedTime}");

// 重启计时器
timer.Restart();
// ... 执行其他操作
timer.Stop();
Console.WriteLine($"重启后经过时间: {timer.GetElapsedMilliseconds()}ms");
```

#### 时间格式化
```csharp
// 格式化当前时间
DateTime now = DateTime.Now;
string formatted1 = DateTimeUtil.FormatDateTime(now, "yyyy年MM月dd日 HH:mm:ss");
string formatted2 = DateTimeUtil.FormatDateTime(now, "yyyy-MM-dd HH:mm:ss.fff");

Console.WriteLine($"格式化1: {formatted1}");
Console.WriteLine($"格式化2: {formatted2}");

// 格式化时间戳
long timestamp = DateTimeUtil.NowMilliseconds1970;
string formattedTimestamp = DateTimeUtil.FormatDateTime(timestamp, "yyyy-MM-dd HH:mm:ss");
string formattedLocal = DateTimeUtil.FormatDateTimeLocal(timestamp, "yyyy-MM-dd HH:mm:ss");

Console.WriteLine($"时间戳格式化: {formattedTimestamp}");
Console.WriteLine($"本地时间格式化: {formattedLocal}");
```

#### 性能测试
```csharp
// 使用计时器进行性能测试
var performanceTimer = new StampTimer();

// 测试时间单位转换性能
performanceTimer.Start();
for (int i = 0; i < 1000000; i++)
{
    long ticks = i * 1000;
    long ms = DateTimeUtil.Ticks2Millis(ticks);
    long backToTicks = DateTimeUtil.Millis2Ticks(ms);
}
performanceTimer.Stop();

Console.WriteLine($"100万次时间转换耗时: {performanceTimer.GetElapsedMilliseconds()}ms");

// 测试时间戳获取性能
performanceTimer.Restart();
for (int i = 0; i < 100000; i++)
{
    long ticks = DateTimeUtil.NowTicks;
    long ms = DateTimeUtil.NowMilliseconds1970;
}
performanceTimer.Stop();

Console.WriteLine($"10万次时间戳获取耗时: {performanceTimer.GetElapsedMilliseconds()}ms");
```

### 设计特点

1. **高精度**：支持纳秒级精度
2. **多基准**：支持Unix和Ticks两种时间基准
3. **完整转换**：支持所有时间单位的双向转换
4. **实用工具**：提供日期计算和格式化功能
5. **性能优化**：使用常量避免重复计算
6. **易于使用**：提供简洁的API接口

### 注意事项

1. **精度考虑**：纳秒级操作可能受系统精度限制
2. **时区处理**：注意UTC和本地时间的区别
3. **性能影响**：频繁的时间戳获取可能影响性能
4. **内存使用**：大量时间序列数据注意内存使用
5. **线程安全**：多线程环境下的时间操作
6. **基准时间**：注意不同时间基准的差异 