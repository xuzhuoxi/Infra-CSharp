# DateTimex API 文档

## 命名空间: JLGames.Infra.DateTimex

### 静态类 (Static Classes)

#### DateTimeUtil
日期时间工具：Tick 常量、单位换算、当前时间戳、日历辅助与格式化。

```csharp
/// <summary>
/// 日期时间工具：Tick 常量、单位换算、当前时间戳、日历辅助与格式化。
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

    // 当前时间戳属性（均基于 UTC）
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
    /// <param name="ticks">待转换的 Tick 数。</param>
    /// <returns>纳秒数。</returns>
    public static long Ticks2Nanos(long ticks);

    /// <summary>
    /// Tick数 转 毫秒数
    /// </summary>
    /// <param name="ticks">待转换的 Tick 数。</param>
    /// <returns>毫秒数。</returns>
    public static long Ticks2Millis(long ticks);

    /// <summary>
    /// Tick数 转 秒数
    /// </summary>
    /// <param name="ticks">待转换的 Tick 数。</param>
    /// <returns>秒数。</returns>
    public static long Ticks2Seconds(long ticks);

    /// <summary>
    /// Tick数 转 分钟数
    /// </summary>
    /// <param name="ticks">待转换的 Tick 数。</param>
    /// <returns>分钟数。</returns>
    public static long Ticks2Minutes(long ticks);

    /// <summary>
    /// Tick数 转 小时数
    /// </summary>
    /// <param name="ticks">待转换的 Tick 数。</param>
    /// <returns>小时数。</returns>
    public static long Ticks2Hours(long ticks);

    /// <summary>
    /// 纳秒数 转 Tick数
    /// </summary>
    /// <param name="nanos">待转换的纳秒数。</param>
    /// <returns>Tick 数。</returns>
    public static long Nanos2Ticks(long nanos);

    /// <summary>
    /// 毫秒数 转 Tick数
    /// </summary>
    /// <param name="millisecond">毫秒数</param>
    /// <returns>Tick 数</returns>
    public static long Millis2Ticks(long millisecond);

    /// <summary>
    /// 秒数 转 Tick数
    /// </summary>
    /// <param name="seconds">秒数</param>
    /// <returns>Tick 数</returns>
    public static long Seconds2Ticks(long seconds);

    /// <summary>
    /// 分钟数 转 Tick数
    /// </summary>
    /// <param name="minutes">分钟数</param>
    /// <returns>Tick 数</returns>
    public static long Minutes2Ticks(long minutes);

    /// <summary>
    /// 小时数 转 Tick数
    /// </summary>
    /// <param name="hours">小时数</param>
    /// <returns>Tick 数</returns>
    public static long Hours2Ticks(long hours);

    // 日期计算方法
    /// <summary>
    /// 取每个月份的天数
    /// </summary>
    /// <param name="month">月份</param>
    /// <param name="year">年份</param>
    /// <returns>天数</returns>
    public static int GetMonthDay(int month, int year);

    /// <summary>
    /// 判断是否闰年
    /// </summary>
    /// <param name="year">年份</param>
    /// <returns>是否为闰年</returns>
    public static bool CheckLeapYear(int year);

    /// <summary>
    /// 计算两个时间点的月份差
    /// </summary>
    /// <param name="start">开始时间</param>
    /// <param name="end">结束时间</param>
    /// <returns>月份差</returns>
    public static int GetOffsetMonth(DateTime start, DateTime end);

    // 格式化方法
    /// <summary>
    /// 格式化时间
    /// </summary>
    /// <param name="dt">日期时间</param>
    /// <param name="format">格式字符串</param>
    /// <param name="provider">格式提供者；为 null 时使用不变区域性。</param>
    /// <returns>格式化后的字符串</returns>
    public static string FormatDateTime(DateTime dt, string format, IFormatProvider provider = null);

    /// <summary>
    /// 格式化时间戳
    /// </summary>
    /// <param name="timestamp">自 0001.01.01 起的 Ticks</param>
    /// <param name="format">格式字符串</param>
    /// <param name="provider">格式提供者；为 null 时使用不变区域性。</param>
    /// <returns>按 UTC 格式化后的字符串</returns>
    public static string FormatDateTime(long timestamp, string format, IFormatProvider provider = null);

    /// <summary>
    /// 格式化时间戳
    /// </summary>
    /// <param name="timestamp">自 0001.01.01 起的 Ticks</param>
    /// <param name="format">格式字符串</param>
    /// <param name="provider">格式提供者；为 null 时使用不变区域性。</param>
    /// <returns>转换为本地时间后的格式化字符串</returns>
    public static string FormatDateTimeLocal(long timestamp, string format, IFormatProvider provider = null);
}
```

### 类 (Classes)

#### TimeSeries
时间序列：按名称追加时间片，并根据时间戳定位所属片段。

```csharp
/// <summary>
/// 时间序列
/// </summary>
public class TimeSeries
{
    /// <summary>
    /// 构造一个时间序列对象
    /// </summary>
    /// <param name="basestamp">定位前减去的基础时间戳。</param>
    /// <param name="loop">定位时间戳时是否循环。</param>
    public TimeSeries(long basestamp = 0, bool loop = false);

    /// <summary>
    /// 设置基础时间戳
    /// </summary>
    /// <param name="basestamp">用于定位时间片的基础时间戳。</param>
    public void SetBasestamp(long basestamp);

    /// <summary>
    /// 检查是否包含指定名称的时间片
    /// </summary>
    /// <param name="name">要查找的时间片名称。</param>
    /// <returns>存在同名时间片时返回 true。</returns>
    public bool Contains(string name);

    /// <summary>
    /// 查找第一个指定名称的时间片
    /// </summary>
    /// <param name="name">要查找的时间片名称。</param>
    /// <returns>首个匹配的索引，未找到时为 -1。</returns>
    public int FindFirstSlice(string name);

    /// <summary>
    /// 查找最后一个指定名称的时间片
    /// </summary>
    /// <param name="name">要查找的时间片名称。</param>
    /// <returns>最后一个匹配的索引，未找到时为 -1。</returns>
    public int FindLastSlice(string name);

    /// <summary>
    /// 根据时间戳定位时间片
    /// </summary>
    /// <param name="timestamp">绝对时间戳（相对序列基准）。</param>
    /// <returns>包含该偏移的时间片名称，无匹配时为 null。</returns>
    public string LocateTo(long timestamp);

    /// <summary>
    /// 追加时间片
    /// </summary>
    /// <param name="name">时间片名称。</param>
    /// <param name="duration">时长（Tick），无前导间隔。</param>
    /// <returns>添加成功返回 true；名称为空或时长为负时返回 false。</returns>
    public bool AddSlice(string name, long duration);

    /// <summary>
    /// 追加时间片
    /// </summary>
    /// <param name="name">时间片名称。</param>
    /// <param name="space">本片在序列布局中的前导间隔。</param>
    /// <param name="duration">时长（Tick）。</param>
    /// <returns>参数有效且添加成功时返回 true。</returns>
    public bool AddSlice(string name, long space, long duration);

    /// <summary>
    /// 移除时间片
    /// </summary>
    /// <param name="index">要移除的时间片索引。</param>
    /// <param name="keepBlank">为 true 时，将下一片平移以保留空白时长。</param>
    /// <returns>被移除片的名称；索引越界时为 null。</returns>
    public string RemoveAt(int index, bool keepBlank = false);

    /// <summary>
    /// 移除第一个时间片
    /// </summary>
    /// <param name="keepBlank">为 true 时，移除后平移下一片。</param>
    /// <returns>被移除片的名称；序列为空时为 null。</returns>
    public string RemoveFirst(bool keepBlank = false);

    /// <summary>
    /// 移除最后一个时间片
    /// </summary>
    /// <returns>被移除片的名称；序列为空时为 null。</returns>
    public string RemoveLast();

    /// <summary>
    /// 移除全部时间片
    /// </summary>
    public void RemoveAll();
}
```

#### StampTimer
可暂停/恢复的流逝时间计时器，支持手动调整流失时间，并可注入自定义 Tick 来源。

默认时钟为 `DateTimeUtil.NowTicks1970`。

```csharp
/// <summary>
/// 可暂停/恢复的流逝时间计时器，支持手动调整流失时间，并可注入自定义 Tick 来源。
/// </summary>
public sealed class StampTimer
{
    /// <summary>
    /// 获取当前Ticks函数代理
    /// </summary>
    public delegate long NowTicksGetter();

    /// <summary>
    /// 自 Start 起流逝的时间（不含暂停时段）。
    /// </summary>
    public long LostTicks { get; }

    /// <summary>
    /// 流失的时间(毫秒)
    /// </summary>
    public long LostMilliseconds { get; }

    /// <summary>
    /// 全局流失的时间(Ticks)
    /// </summary>
    public long GlobalLostTicks { get; }

    /// <summary>
    /// 全局流失的时间(毫秒)
    /// </summary>
    public long GlobalLostMilliseconds { get; }

    /// <summary>
    /// 暂停累计占用的时间(Ticks)
    /// </summary>
    public long PauseTicks { get; }

    /// <summary>
    /// 暂停累计占用的时间(毫秒)
    /// </summary>
    public long PauseMilliseconds { get; }

    /// <summary>
    /// 是否暂停中
    /// </summary>
    public bool IsPause { get; }

    /// <summary>
    /// 创建基准流失时间为 0 的计时器。
    /// </summary>
    public StampTimer();

    /// <summary>
    /// 以指定的基准流失 Tick 数创建计时器。
    /// </summary>
    /// <param name="baseLostTicks">在 Start 之前计入的基准流失 Tick。</param>
    public StampTimer(long baseLostTicks);

    /// <summary>
    /// 根据 DateTime 推导基准流失时间并创建计时器。
    /// </summary>
    /// <param name="baseLostDateTime">作为基准锚点的日期时间。</param>
    public StampTimer(DateTime baseLostDateTime);

    /// <summary>
    /// 设置获取当前时间戳的函数
    /// </summary>
    /// <param name="getter">自定义 Tick 提供器；为 null 时使用 DateTimeUtil.NowTicks1970。</param>
    public void SetNowTicksGetter(NowTicksGetter getter);

    /// <summary>
    /// 开始计时
    /// </summary>
    public void Start();

    /// <summary>
    /// 计时器结束
    /// </summary>
    public void Stop();

    /// <summary>
    /// 时间暂停
    /// </summary>
    public void Pause();

    /// <summary>
    /// 时间继续流动
    /// </summary>
    public void Continue();

    /// <summary>
    /// 增加流失时间
    /// </summary>
    /// <param name="lostTicks">根据当前状态追加到运行或暂停流失时间的 Tick 数。</param>
    public void AddLost(long lostTicks);

    /// <summary>
    /// 增加运行流失时间
    /// </summary>
    /// <param name="lostTicks">计时器运行期间追加的流失 Tick 数。</param>
    public void AddRunningLost(long lostTicks);

    /// <summary>
    /// 增加暂停运行时间
    /// </summary>
    /// <param name="lostTicks">计时器暂停期间追加的流失 Tick 数。</param>
    public void AddPausingLost(long lostTicks);

    /// <summary>
    /// 生成一个时间戳对象
    /// </summary>
    /// <param name="initTicks">初始基准流失 Tick 数。</param>
    /// <returns>新的 StampTimer 实例。</returns>
    public static StampTimer GenTimer(long initTicks);

    /// <summary>
    /// 生成一个时间戳对象
    /// </summary>
    /// <returns>基准为 0 的新 StampTimer 实例。</returns>
    public static StampTimer GenTimer();
}
```

### 结构体 (Structs)

#### TimeSlice
时间轴上的时间片，包含起点、时长，并支持平移与缩放。起点与时长均为 `long` 时间戳（Tick），不是 `DateTime`。

```csharp
/// <summary>
/// 时间轴上的时间片，包含起点、时长，并支持平移与缩放。
/// </summary>
public struct TimeSlice
{
    /// <summary>
    /// 缩放时间片时的锚点位置。
    /// </summary>
    public enum ZoomAnchor
    {
        /// <summary>锚定在起点。</summary>
        Start,

        /// <summary>锚定在中心。</summary>
        Center,

        /// <summary>锚定在终点。</summary>
        End
    }

    /// <summary>
    /// 开始时间戳
    /// </summary>
    public long Start { get; }

    /// <summary>
    /// 结束时间戳
    /// </summary>
    public long End { get; }

    /// <summary>
    /// 时间长度
    /// </summary>
    public long Duration { get; }

    /// <summary>
    /// 检查时间戳是否在时间片内
    /// </summary>
    /// <param name="timestamp">待检测的时间戳。</param>
    /// <param name="includeEnd">是否包含结束边界。</param>
    /// <returns>时间戳落在片内时返回 true。</returns>
    public bool Contains(long timestamp, bool includeEnd = false);

    /// <summary>
    /// 移动时间片
    /// </summary>
    /// <param name="offset">平移起点的 Tick 偏移量，时长不变。</param>
    public void Move(long offset);

    /// <summary>
    /// 延长时间长度
    /// </summary>
    /// <param name="duration">追加到时长上的 Tick 数。</param>
    public void Extend(long duration);

    /// <summary>
    /// 缩放时间片
    /// </summary>
    /// <param name="scale">缩放系数，1 表示不变。</param>
    /// <param name="anchor">锚点 [0,1]：0 为终点，1 为起点，其余为插值。</param>
    public void Zoom(float scale, float anchor);

    /// <summary>
    /// 缩放时间片
    /// </summary>
    /// <param name="scale">缩放系数，1 表示不变。</param>
    /// <param name="anchor">固定锚点：起点、中心或终点。</param>
    public void Zoom(float scale, ZoomAnchor anchor);

    /// <summary>
    /// 通过时间长度创建时间片
    /// </summary>
    /// <param name="start">开始时间戳。</param>
    /// <param name="duration">时长（Tick）。</param>
    /// <returns>新的时间片。</returns>
    public static TimeSlice NewSliceWitDur(long start, long duration);

    /// <summary>
    /// 通过两个时间点创建时间片
    /// </summary>
    /// <param name="start">开始时间戳。</param>
    /// <param name="end">结束时间戳。</param>
    /// <returns>时长为 end - start 的新时间片。</returns>
    public static TimeSlice NewSliceWithEnd(long start, long end);
}
```

### 功能说明

#### 时间单位转换

**支持的时间单位**
- **Tick**：.NET 基本时间单位，1 Tick = 100 纳秒
- **纳秒**：`Ticks2Nanos` / `Nanos2Ticks`（1 Tick = 100 纳秒）
- **微秒**：`TicksPerMicrosecond = 10`
- **毫秒**：`TicksPerMilliSecond = 10_000`
- **厘秒**：`TicksPerCentisecond = 100_000`
- **秒**：`TicksPerSecond = 10_000_000`
- **分钟 / 小时 / 天 / 周**：对应 `TicksPerMinute`、`TicksPerHour`、`TicksPerDay`、`TicksPerWeak`
- **月**：28/29/30/31 天对应的 Tick 常量
- **年**：365 天（闰年 366 天，`TicksPerYear2`）

**转换方法**
1. **Ticks 转其他单位**：`Ticks2Nanos`、`Ticks2Millis`、`Ticks2Seconds`、`Ticks2Minutes`、`Ticks2Hours`
2. **其他单位转 Ticks**：`Nanos2Ticks`、`Millis2Ticks`、`Seconds2Ticks`、`Minutes2Ticks`、`Hours2Ticks`

#### 时间戳系统

**时间戳基准**
- **0001 基准**：自公元 0001-01-01 起的 Ticks / 纳秒 / 毫秒 / 秒 / 分钟 / 小时（`NowTicks`、`NowNanoseconds`、`NowMilliseconds`、`NowSeconds`、`NowMinutes`、`NowHours`）
- **1970 基准（Unix）**：自 1970-01-01 UTC 起（`Ticks1970`、`NowTicks1970` 及对应的纳秒/毫秒/秒/分钟/小时属性）

当前时间均取 `DateTime.UtcNow`。

**格式化**
- `FormatDateTime(DateTime, ...)`：按给定格式格式化 `DateTime`
- `FormatDateTime(long, ...)`：将自 0001-01-01 起的 Ticks 视为 UTC `DateTime` 后格式化
- `FormatDateTimeLocal(long, ...)`：同样以自 0001-01-01 的 Ticks 为输入，转换到本地时区后格式化
- `format` 使用 .NET 标准/自定义日期时间格式字符串；`provider` 为 null 时使用不变区域性

#### 日期计算

- **GetMonthDay**：按年月取该月天数（二月结合闰年）
- **CheckLeapYear**：判断是否闰年
- **GetOffsetMonth**：计算两个时间点的月份差

#### 时间序列与时间片

- **TimeSeries**：由命名时间片组成的序列。`AddSlice` 追加片段（可选前导间隔 `space`），`LocateTo` 用时间戳定位所属片段名称；`loop` 为 true 时按序列总时长取模循环定位
- **TimeSlice**：以 `long` 起点与时长表示区间，可通过 `NewSliceWitDur` / `NewSliceWithEnd` 创建，并支持 `Contains`、`Move`、`Extend`、`Zoom`
- **StampTimer**：可暂停/继续的流逝计时器；`LostTicks` 不含暂停时段，`GlobalLostTicks` 含构造时的基准流失时间；可用 `SetNowTicksGetter` 注入自定义时钟

### 使用示例

#### 时间单位转换
```csharp
long ticks = DateTimeUtil.TicksPerSecond; // 1 秒对应的 Ticks

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

long backToTicks = DateTimeUtil.Nanos2Ticks(nanoseconds);
Console.WriteLine($"转换回Ticks: {backToTicks}");

long fromHours = DateTimeUtil.Hours2Ticks(2);
long fromSeconds = DateTimeUtil.Seconds2Ticks(90);
```

#### 时间戳操作
```csharp
long currentTicks = DateTimeUtil.NowTicks;
long currentTicks1970 = DateTimeUtil.NowTicks1970;
long currentMilliseconds = DateTimeUtil.NowMilliseconds1970;
long currentSeconds = DateTimeUtil.NowSeconds1970;

Console.WriteLine($"当前Ticks (0001基准): {currentTicks}");
Console.WriteLine($"当前Ticks (1970基准): {currentTicks1970}");
Console.WriteLine($"当前毫秒 (1970基准): {currentMilliseconds}");
Console.WriteLine($"当前秒 (1970基准): {currentSeconds}");
Console.WriteLine($"Unix epoch 的 Ticks: {DateTimeUtil.Ticks1970}");

// timestamp 为自 0001.01.01 起的 Ticks
string formattedTime = DateTimeUtil.FormatDateTime(currentTicks, "yyyy-MM-dd HH:mm:ss");
Console.WriteLine($"格式化时间: {formattedTime}");
```

#### 日期计算
```csharp
int daysInFebruary2024 = DateTimeUtil.GetMonthDay(2, 2024);
int daysInFebruary2023 = DateTimeUtil.GetMonthDay(2, 2023);

Console.WriteLine($"2024年2月天数: {daysInFebruary2024}"); // 29 (闰年)
Console.WriteLine($"2023年2月天数: {daysInFebruary2023}"); // 28

bool isLeap2024 = DateTimeUtil.CheckLeapYear(2024);
bool isLeap2023 = DateTimeUtil.CheckLeapYear(2023);

Console.WriteLine($"2024是闰年: {isLeap2024}"); // True
Console.WriteLine($"2023是闰年: {isLeap2023}"); // False

DateTime startDate = new DateTime(2023, 1, 1);
DateTime endDate = new DateTime(2024, 6, 15);
int monthOffset = DateTimeUtil.GetOffsetMonth(startDate, endDate);

Console.WriteLine($"从 {startDate:yyyy-MM-dd} 到 {endDate:yyyy-MM-dd} 的月份差: {monthOffset}");
```

#### 时间序列处理
```csharp
var series = new TimeSeries(basestamp: 0, loop: true);

series.AddSlice("morning", DateTimeUtil.Hours2Ticks(8));
series.AddSlice("work", DateTimeUtil.Hours2Ticks(8));
series.AddSlice("evening", DateTimeUtil.Hours2Ticks(8));

Console.WriteLine(series.Contains("work"));          // True
Console.WriteLine(series.FindFirstSlice("work"));    // 1

string name = series.LocateTo(DateTimeUtil.Hours2Ticks(10));
Console.WriteLine($"10 小时处所属片段: {name}");     // work

// 循环定位：超过序列总时长后取模
string looped = series.LocateTo(DateTimeUtil.Hours2Ticks(26));
Console.WriteLine($"26 小时处所属片段: {looped}");   // work

series.SetBasestamp(DateTimeUtil.NowTicks1970);
series.RemoveAt(0);
series.RemoveLast();
series.RemoveAll();
```

#### 时间切片操作
```csharp
var slice = TimeSlice.NewSliceWitDur(0, DateTimeUtil.Hours2Ticks(8));
var slice2 = TimeSlice.NewSliceWithEnd(
    DateTimeUtil.Hours2Ticks(4),
    DateTimeUtil.Hours2Ticks(12)
);

Console.WriteLine($"起点: {slice.Start}, 终点: {slice.End}, 时长: {slice.Duration}");
Console.WriteLine(slice.Contains(DateTimeUtil.Hours2Ticks(3)));       // True
Console.WriteLine(slice.Contains(slice.End));                        // False（默认不含结束边界）
Console.WriteLine(slice.Contains(slice.End, includeEnd: true));      // True

slice.Move(DateTimeUtil.Hours2Ticks(1));
slice.Extend(DateTimeUtil.Hours2Ticks(2));
slice.Zoom(2f, TimeSlice.ZoomAnchor.Center);
slice.Zoom(0.5f, 1f); // 锚点 1 = 起点
```

#### 高精度计时器
```csharp
var timer = new StampTimer();
timer.Start();

Thread.Sleep(100);

timer.Pause();
Console.WriteLine($"暂停中: {timer.IsPause}");
Console.WriteLine($"已流失毫秒: {timer.LostMilliseconds}");
Console.WriteLine($"暂停累计毫秒: {timer.PauseMilliseconds}");

timer.Continue();
Thread.Sleep(50);
timer.Stop();

Console.WriteLine($"流失 Ticks: {timer.LostTicks}");
Console.WriteLine($"流失毫秒: {timer.LostMilliseconds}");
Console.WriteLine($"全局流失毫秒: {timer.GlobalLostMilliseconds}");

// 手动补偿流失时间
timer.AddLost(DateTimeUtil.TicksPerMilliSecond * 10);
timer.AddRunningLost(DateTimeUtil.TicksPerMilliSecond);
timer.AddPausingLost(DateTimeUtil.TicksPerMilliSecond);

// 带基准流失时间 / 工厂方法 / 自定义时钟
var withBase = new StampTimer(DateTimeUtil.TicksPerSecond);
var fromFactory = StampTimer.GenTimer();
fromFactory.SetNowTicksGetter(() => DateTimeUtil.NowTicks1970);
fromFactory.Start();
```

#### 时间格式化
```csharp
DateTime now = DateTime.UtcNow;
string formatted1 = DateTimeUtil.FormatDateTime(now, "yyyy年MM月dd日 HH:mm:ss");
string formatted2 = DateTimeUtil.FormatDateTime(now, "yyyy-MM-dd HH:mm:ss.fff");

Console.WriteLine($"格式化1: {formatted1}");
Console.WriteLine($"格式化2: {formatted2}");

long timestamp = DateTimeUtil.NowTicks; // 自 0001.01.01 的 Ticks
string formattedTimestamp = DateTimeUtil.FormatDateTime(timestamp, "yyyy-MM-dd HH:mm:ss");
string formattedLocal = DateTimeUtil.FormatDateTimeLocal(timestamp, "yyyy-MM-dd HH:mm:ss");

Console.WriteLine($"时间戳格式化(UTC): {formattedTimestamp}");
Console.WriteLine($"本地时间格式化: {formattedLocal}");
```

#### 性能测试
```csharp
var performanceTimer = new StampTimer();

performanceTimer.Start();
for (int i = 0; i < 1000000; i++)
{
    long ticks = i * 1000L;
    long ms = DateTimeUtil.Ticks2Millis(ticks);
    long backToTicks = DateTimeUtil.Nanos2Ticks(DateTimeUtil.Ticks2Nanos(ticks));
}
performanceTimer.Stop();

Console.WriteLine($"100万次时间转换耗时: {performanceTimer.LostMilliseconds}ms");

performanceTimer = StampTimer.GenTimer();
performanceTimer.Start();
for (int i = 0; i < 100000; i++)
{
    long ticks = DateTimeUtil.NowTicks;
    long ms = DateTimeUtil.NowMilliseconds1970;
}
performanceTimer.Stop();

Console.WriteLine($"10万次时间戳获取耗时: {performanceTimer.LostMilliseconds}ms");
```

### 设计特点

1. **高精度**：以 Tick 为基本单位，并可换算到纳秒
2. **双基准**：同时提供 0001-01-01 与 1970-01-01 UTC 两套当前时间戳
3. **单位换算**：常用时间单位与 Ticks 的双向转换及对应常量
4. **命名时间序列**：`TimeSeries` 按名称管理时间片，并支持循环定位
5. **可变时间片**：`TimeSlice` 支持平移、延长与按锚点缩放
6. **可暂停计时器**：`StampTimer` 区分运行流失与暂停占用，可注入自定义时钟

### 注意事项

1. **精度**：1 Tick = 100 纳秒；纳秒级读数受系统时钟精度限制
2. **时区**：当前时间戳基于 UTC；`FormatDateTime(long)` 按 UTC 格式化，`FormatDateTimeLocal` 转为本地时区
3. **时间戳单位**：`FormatDateTime(long)` / `FormatDateTimeLocal` 的参数是自 0001-01-01 起的 **Ticks**，不是毫秒 Unix 时间戳
4. **TimeSlice 类型**：起点、终点、时长均为 `long`，不是 `DateTime`；它是可变结构体，注意拷贝语义
5. **StampTimer 时钟**：默认使用 `DateTimeUtil.NowTicks1970`；`Stop` 内部调用 `Pause`
6. **命名**：`TicksPerWeak` 表示一周的 Ticks；`NewSliceWitDur` 为源码中的工厂方法名
