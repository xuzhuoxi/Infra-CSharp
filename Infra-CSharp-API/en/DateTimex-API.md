# DateTimex API Documentation

## Namespace: JLGames.Infra.DateTimex

### Static Classes

#### DateTimeUtil
Date/time utilities: tick constants, conversions, current timestamps, calendar helpers, and formatting.

```csharp
/// <summary>
/// Date/time utilities: tick constants, conversions, current timestamps, calendar helpers, and formatting.
/// </summary>
public static class DateTimeUtil
{
    /// <summary>
    /// Delegate: Get now timestamp.
    /// </summary>
    public delegate long FuncGetNowTicks();

    // Time unit constants
    /// <summary>
    /// Number of ticks in 1 microsecond
    /// </summary>
    public const long TicksPerMicrosecond = 10;

    /// <summary>
    /// Number of ticks in 1 millisecond
    /// </summary>
    public const long TicksPerMilliSecond = TicksPerMicrosecond * 1000;

    /// <summary>
    /// Number of ticks in 1 centisecond
    /// </summary>
    public const long TicksPerCentisecond = TicksPerMilliSecond * 10;

    /// <summary>
    /// Number of ticks in 1 second
    /// </summary>
    public const long TicksPerSecond = TicksPerMilliSecond * 1000;

    /// <summary>
    /// Number of ticks in 1 minute
    /// </summary>
    public const long TicksPerMinute = TicksPerSecond * 60;

    /// <summary>
    /// Number of ticks in 1 hour
    /// </summary>
    public const long TicksPerHour = TicksPerMinute * 60;

    /// <summary>
    /// Number of ticks in 1 day
    /// </summary>
    public const long TicksPerDay = TicksPerHour * 24;

    /// <summary>
    /// Number of ticks in a 28-day month
    /// </summary>
    public const long TicksPerMonth28 = TicksPerDay * 28;

    /// <summary>
    /// Number of ticks in a 29-day month
    /// </summary>
    public const long TicksPerMonth29 = TicksPerDay * 29;

    /// <summary>
    /// Number of ticks in a 30-day month
    /// </summary>
    public const long TicksPerMonth30 = TicksPerDay * 30;

    /// <summary>
    /// Number of ticks in a 31-day month
    /// </summary>
    public const long TicksPerMonth31 = TicksPerDay * 31;

    /// <summary>
    /// Number of ticks in a year
    /// </summary>
    public const long TicksPerYear = TicksPerDay * 365;

    /// <summary>
    /// Number of ticks in a leap year
    /// </summary>
    public const long TicksPerYear2 = TicksPerDay * 366;

    /// <summary>
    /// Number of ticks in a week
    /// </summary>
    public const long TicksPerWeak = TicksPerDay * 7;

    // Current timestamp properties (all based on UTC)
    /// <summary>
    /// The timestamp (tick) of 1970.1.1
    /// </summary>
    public static long Ticks1970 { get; }

    /// <summary>
    /// The current timestamp (tick) from 0001.1.1
    /// </summary>
    public static long NowTicks { get; }

    /// <summary>
    /// The current timestamp (tick) from 1970.1.1
    /// </summary>
    public static long NowTicks1970 { get; }

    /// <summary>
    /// The current timestamp (nano) from 0001.1.1
    /// </summary>
    public static long NowNanoseconds { get; }

    /// <summary>
    /// The current timestamp (nano) from 1970.1.1
    /// </summary>
    public static long NowNanoseconds1970 { get; }

    /// <summary>
    /// The current timestamp (millisecond) from 0001.1.1
    /// </summary>
    public static long NowMilliseconds { get; }

    /// <summary>
    /// The current timestamp (millisecond) from 1970.1.1
    /// </summary>
    public static long NowMilliseconds1970 { get; }

    /// <summary>
    /// The current timestamp (second) from 0001.1.1
    /// </summary>
    public static long NowSeconds { get; }

    /// <summary>
    /// The current timestamp (second) from 1970.1.1
    /// </summary>
    public static long NowSeconds1970 { get; }

    /// <summary>
    /// The current timestamp (minute) from 0001.1.1
    /// </summary>
    public static long NowMinutes { get; }

    /// <summary>
    /// The current timestamp (minute) from 1970.1.1
    /// </summary>
    public static long NowMinutes1970 { get; }

    /// <summary>
    /// The current timestamp (hour) from 0001.1.1
    /// </summary>
    public static long NowHours { get; }

    /// <summary>
    /// The current timestamp (hour) from 1970.1.1
    /// </summary>
    public static long NowHours1970 { get; }

    // Time unit conversion methods
    /// <summary>
    /// Ticks to nanoseconds
    /// </summary>
    /// <param name="ticks">Tick count to convert.</param>
    /// <returns>Nanoseconds.</returns>
    public static long Ticks2Nanos(long ticks);

    /// <summary>
    /// Ticks to milliseconds
    /// </summary>
    /// <param name="ticks">Tick count to convert.</param>
    /// <returns>Milliseconds.</returns>
    public static long Ticks2Millis(long ticks);

    /// <summary>
    /// Ticks to seconds
    /// </summary>
    /// <param name="ticks">Tick count to convert.</param>
    /// <returns>Seconds.</returns>
    public static long Ticks2Seconds(long ticks);

    /// <summary>
    /// Ticks to minutes
    /// </summary>
    /// <param name="ticks">Tick count to convert.</param>
    /// <returns>Minutes.</returns>
    public static long Ticks2Minutes(long ticks);

    /// <summary>
    /// Ticks to hours
    /// </summary>
    /// <param name="ticks">Tick count to convert.</param>
    /// <returns>Hours.</returns>
    public static long Ticks2Hours(long ticks);

    /// <summary>
    /// Nanoseconds to ticks
    /// </summary>
    /// <param name="nanos">Nanoseconds to convert.</param>
    /// <returns>Ticks.</returns>
    public static long Nanos2Ticks(long nanos);

    /// <summary>
    /// Milliseconds to ticks
    /// </summary>
    /// <param name="millisecond">Milliseconds</param>
    /// <returns>Ticks</returns>
    public static long Millis2Ticks(long millisecond);

    /// <summary>
    /// Seconds to ticks
    /// </summary>
    /// <param name="seconds">Seconds</param>
    /// <returns>Ticks</returns>
    public static long Seconds2Ticks(long seconds);

    /// <summary>
    /// Minutes to ticks
    /// </summary>
    /// <param name="minutes">Minutes</param>
    /// <returns>Ticks</returns>
    public static long Minutes2Ticks(long minutes);

    /// <summary>
    /// Hours to ticks
    /// </summary>
    /// <param name="hours">Hours</param>
    /// <returns>Ticks</returns>
    public static long Hours2Ticks(long hours);

    // Date calculation methods
    /// <summary>
    /// Get the number of days in each month
    /// </summary>
    /// <param name="month">Month</param>
    /// <param name="year">Year</param>
    /// <returns>Number of days</returns>
    public static int GetMonthDay(int month, int year);

    /// <summary>
    /// Check for leap year
    /// </summary>
    /// <param name="year">Year</param>
    /// <returns>Whether it is a leap year</returns>
    public static bool CheckLeapYear(int year);

    /// <summary>
    /// Calculate the month offsets between two time points
    /// </summary>
    /// <param name="start">Start time</param>
    /// <param name="end">End time</param>
    /// <returns>Month difference</returns>
    public static int GetOffsetMonth(DateTime start, DateTime end);

    // Formatting methods
    /// <summary>
    /// Format time
    /// </summary>
    /// <param name="dt">DateTime</param>
    /// <param name="format">Format string</param>
    /// <param name="provider">Format provider; invariant culture when null.</param>
    /// <returns>Formatted string</returns>
    public static string FormatDateTime(DateTime dt, string format, IFormatProvider provider = null);

    /// <summary>
    /// Format timestamp
    /// </summary>
    /// <param name="timestamp">Ticks from 0001.01.01</param>
    /// <param name="format">Format string</param>
    /// <param name="provider">Format provider; invariant culture when null.</param>
    /// <returns>UTC-formatted string</returns>
    public static string FormatDateTime(long timestamp, string format, IFormatProvider provider = null);

    /// <summary>
    /// Format timestamp
    /// </summary>
    /// <param name="timestamp">Ticks from 0001.01.01</param>
    /// <param name="format">Format string</param>
    /// <param name="provider">Format provider; invariant culture when null.</param>
    /// <returns>String formatted in local time</returns>
    public static string FormatDateTimeLocal(long timestamp, string format, IFormatProvider provider = null);
}
```

### Classes

#### TimeSeries
Time series: append named time slices and locate which slice a timestamp falls into.

```csharp
/// <summary>
/// Time Series
/// </summary>
public class TimeSeries
{
    /// <summary>
    /// Construct a time series object
    /// </summary>
    /// <param name="basestamp">Base timestamp subtracted before locating slices.</param>
    /// <param name="loop">Whether to loop when locating timestamps.</param>
    public TimeSeries(long basestamp = 0, bool loop = false);

    /// <summary>
    /// Set base timestamp
    /// </summary>
    /// <param name="basestamp">Base timestamp for locating slices.</param>
    public void SetBasestamp(long basestamp);

    /// <summary>
    /// Checks whether a time slice with the specified name is contained
    /// </summary>
    /// <param name="name">Slice name to look up.</param>
    /// <returns>True if any slice uses this name.</returns>
    public bool Contains(string name);

    /// <summary>
    /// Find the first time slice with the specified name
    /// </summary>
    /// <param name="name">Slice name to find.</param>
    /// <returns>Index of the first match, or -1 if not found.</returns>
    public int FindFirstSlice(string name);

    /// <summary>
    /// Find the last time slice with the specified name
    /// </summary>
    /// <param name="name">Slice name to find.</param>
    /// <returns>Index of the last match, or -1 if not found.</returns>
    public int FindLastSlice(string name);

    /// <summary>
    /// Locating time slices based on timestamps
    /// </summary>
    /// <param name="timestamp">Absolute timestamp (relative to series baseline).</param>
    /// <returns>Name of the slice containing the offset, or null if none.</returns>
    public string LocateTo(long timestamp);

    /// <summary>
    /// Add time slice
    /// </summary>
    /// <param name="name">Slice name.</param>
    /// <param name="duration">Duration in ticks (no leading gap).</param>
    /// <returns>True if added; false when name is empty or duration is negative.</returns>
    public bool AddSlice(string name, long duration);

    /// <summary>
    /// Add time slice
    /// </summary>
    /// <param name="name">Slice name.</param>
    /// <param name="space">Leading gap before this slice within the series layout.</param>
    /// <param name="duration">Duration in ticks.</param>
    /// <returns>True if added; false when arguments are invalid.</returns>
    public bool AddSlice(string name, long space, long duration);

    /// <summary>
    /// Remove time slice
    /// </summary>
    /// <param name="index">Index of the slice to remove.</param>
    /// <param name="keepBlank">If true, shift the next slice to absorb the removed span.</param>
    /// <returns>Removed slice name, or null if index is out of range.</returns>
    public string RemoveAt(int index, bool keepBlank = false);

    /// <summary>
    /// Remove the first time slice
    /// </summary>
    /// <param name="keepBlank">If true, shift the next slice after removal.</param>
    /// <returns>Removed slice name, or null if the series is empty.</returns>
    public string RemoveFirst(bool keepBlank = false);

    /// <summary>
    /// Remove last time slice
    /// </summary>
    /// <returns>Removed slice name, or null if the series is empty.</returns>
    public string RemoveLast();

    /// <summary>
    /// Remove all time slices
    /// </summary>
    public void RemoveAll();
}
```

#### StampTimer
Elapsed-time timer with pause/resume, manual lost-time adjustment, and optional custom tick source.

The default clock is `DateTimeUtil.NowTicks1970`.

```csharp
/// <summary>
/// Elapsed-time timer with pause/resume, manual lost-time adjustment, and optional custom tick source.
/// </summary>
public sealed class StampTimer
{
    /// <summary>
    /// Get the current Ticks function proxy
    /// </summary>
    public delegate long NowTicksGetter();

    /// <summary>
    /// Lost time since Start, excluding pause intervals.
    /// </summary>
    public long LostTicks { get; }

    /// <summary>
    /// Lost time(millisecond)
    /// </summary>
    public long LostMilliseconds { get; }

    /// <summary>
    /// Global lost time(ticks)
    /// </summary>
    public long GlobalLostTicks { get; }

    /// <summary>
    /// Global lost time(millisecond)
    /// </summary>
    public long GlobalLostMilliseconds { get; }

    /// <summary>
    /// Pause lost time(ticks)
    /// </summary>
    public long PauseTicks { get; }

    /// <summary>
    /// Pause lost time(millisecond)
    /// </summary>
    public long PauseMilliseconds { get; }

    /// <summary>
    /// Is it pausing
    /// </summary>
    public bool IsPause { get; }

    /// <summary>
    /// Create a timer with zero baseline lost time.
    /// </summary>
    public StampTimer();

    /// <summary>
    /// Create a timer with the specified baseline lost ticks.
    /// </summary>
    /// <param name="baseLostTicks">Baseline lost ticks counted before Start.</param>
    public StampTimer(long baseLostTicks);

    /// <summary>
    /// Create a timer with baseline lost time derived from a DateTime.
    /// </summary>
    /// <param name="baseLostDateTime">Date/time used as the baseline anchor.</param>
    public StampTimer(DateTime baseLostDateTime);

    /// <summary>
    /// Set the function to get the current timestamp
    /// </summary>
    /// <param name="getter">Custom tick provider; uses DateTimeUtil.NowTicks1970 when null.</param>
    public void SetNowTicksGetter(NowTicksGetter getter);

    /// <summary>
    /// Start the timer
    /// </summary>
    public void Start();

    /// <summary>
    /// Stop the timer
    /// </summary>
    public void Stop();

    /// <summary>
    /// Pause the timer
    /// </summary>
    public void Pause();

    /// <summary>
    /// Time goes on
    /// </summary>
    public void Continue();

    /// <summary>
    /// Increase lost time
    /// </summary>
    /// <param name="lostTicks">Ticks to add to running or pausing lost time depending on state.</param>
    public void AddLost(long lostTicks);

    /// <summary>
    /// Increase running lost time
    /// </summary>
    /// <param name="lostTicks">Ticks to add while the timer is running.</param>
    public void AddRunningLost(long lostTicks);

    /// <summary>
    /// Increase pausing lost time
    /// </summary>
    /// <param name="lostTicks">Ticks to add while the timer is paused.</param>
    public void AddPausingLost(long lostTicks);

    /// <summary>
    /// Generate a timestamp object
    /// </summary>
    /// <param name="initTicks">Initial baseline lost ticks.</param>
    /// <returns>A new StampTimer instance.</returns>
    public static StampTimer GenTimer(long initTicks);

    /// <summary>
    /// Generate a timestamp object
    /// </summary>
    /// <returns>A new StampTimer with zero baseline.</returns>
    public static StampTimer GenTimer();
}
```

### Structs

#### TimeSlice
A time interval on the timeline with start, duration, and move/zoom operations. Start and duration are `long` timestamps (ticks), not `DateTime`.

```csharp
/// <summary>
/// A time interval on the timeline with start, duration, and move/zoom operations.
/// </summary>
public struct TimeSlice
{
    /// <summary>
    /// Anchor position when scaling a time slice.
    /// </summary>
    public enum ZoomAnchor
    {
        /// <summary>Anchor at start.</summary>
        Start,

        /// <summary>Anchor at center.</summary>
        Center,

        /// <summary>Anchor at end.</summary>
        End
    }

    /// <summary>
    /// Start Timestamp
    /// </summary>
    public long Start { get; }

    /// <summary>
    /// End Timestamp
    /// </summary>
    public long End { get; }

    /// <summary>
    /// Time Length
    /// </summary>
    public long Duration { get; }

    /// <summary>
    /// Check if the timestamp is within the time slice
    /// </summary>
    /// <param name="timestamp">Timestamp to test.</param>
    /// <param name="includeEnd">Whether the end boundary is inclusive.</param>
    /// <returns>True if timestamp lies within the slice.</returns>
    public bool Contains(long timestamp, bool includeEnd = false);

    /// <summary>
    /// Move Time Piece
    /// </summary>
    /// <param name="offset">Ticks to shift the start; duration is unchanged.</param>
    public void Move(long offset);

    /// <summary>
    /// Extend the time length
    /// </summary>
    /// <param name="duration">Ticks to add to duration.</param>
    public void Extend(long duration);

    /// <summary>
    /// Zoom Time Piece
    /// </summary>
    /// <param name="scale">Scale factor; 1 means no change.</param>
    /// <param name="anchor">Anchor in [0,1]: 0 end, 1 start, otherwise interpolated.</param>
    public void Zoom(float scale, float anchor);

    /// <summary>
    /// Zoom Time Piece
    /// </summary>
    /// <param name="scale">Scale factor; 1 means no change.</param>
    /// <param name="anchor">Fixed anchor: start, center, or end.</param>
    public void Zoom(float scale, ZoomAnchor anchor);

    /// <summary>
    /// Create time slice by length of time
    /// </summary>
    /// <param name="start">Start timestamp.</param>
    /// <param name="duration">Duration in ticks.</param>
    /// <returns>A new time slice.</returns>
    public static TimeSlice NewSliceWitDur(long start, long duration);

    /// <summary>
    /// Create time slice from two time points
    /// </summary>
    /// <param name="start">Start timestamp.</param>
    /// <param name="end">End timestamp.</param>
    /// <returns>A new time slice with duration end - start.</returns>
    public static TimeSlice NewSliceWithEnd(long start, long end);
}
```

### Feature Description

#### Time Unit Conversion

**Supported Time Units**
- **Tick**: .NET base unit; 1 tick = 100 nanoseconds
- **Nanoseconds**: `Ticks2Nanos` / `Nanos2Ticks` (1 tick = 100 nanoseconds)
- **Microseconds**: `TicksPerMicrosecond = 10`
- **Milliseconds**: `TicksPerMilliSecond = 10_000`
- **Centiseconds**: `TicksPerCentisecond = 100_000`
- **Seconds**: `TicksPerSecond = 10_000_000`
- **Minutes / hours / days / weeks**: `TicksPerMinute`, `TicksPerHour`, `TicksPerDay`, `TicksPerWeak`
- **Months**: tick constants for 28/29/30/31-day months
- **Years**: 365 days (366 in a leap year, `TicksPerYear2`)

**Conversion Methods**
1. **Ticks to other units**: `Ticks2Nanos`, `Ticks2Millis`, `Ticks2Seconds`, `Ticks2Minutes`, `Ticks2Hours`
2. **Other units to ticks**: `Nanos2Ticks`, `Millis2Ticks`, `Seconds2Ticks`, `Minutes2Ticks`, `Hours2Ticks`

#### Timestamp System

**Timestamp Baselines**
- **Year 0001 baseline**: ticks / nanoseconds / milliseconds / seconds / minutes / hours since 0001-01-01 (`NowTicks`, `NowNanoseconds`, `NowMilliseconds`, `NowSeconds`, `NowMinutes`, `NowHours`)
- **Unix 1970 baseline**: since 1970-01-01 UTC (`Ticks1970`, `NowTicks1970`, and the matching nano/milli/second/minute/hour properties)

Current timestamps are taken from `DateTime.UtcNow`.

**Formatting**
- `FormatDateTime(DateTime, ...)`: format a `DateTime` with the given pattern
- `FormatDateTime(long, ...)`: treat ticks from 0001-01-01 as a UTC `DateTime` and format it
- `FormatDateTimeLocal(long, ...)`: same tick input, converted to local time before formatting
- `format` is a .NET standard/custom DateTime format string; a null `provider` uses the invariant culture

#### Date Calculation

- **GetMonthDay**: days in the given month (February accounts for leap years)
- **CheckLeapYear**: whether the year is a leap year
- **GetOffsetMonth**: month difference between two time points

#### Time Series and Time Slices

- **TimeSeries**: a sequence of named slices. `AddSlice` appends a fragment (optional leading `space`); `LocateTo` returns the slice name for a timestamp; when `loop` is true, location wraps by total series duration
- **TimeSlice**: an interval of `long` start + duration, created with `NewSliceWitDur` / `NewSliceWithEnd`, with `Contains`, `Move`, `Extend`, and `Zoom`
- **StampTimer**: pause/resume elapsed timer; `LostTicks` excludes pause time, `GlobalLostTicks` includes the constructor baseline; inject a custom clock with `SetNowTicksGetter`

### Usage Examples

#### Time Unit Conversion
```csharp
long ticks = DateTimeUtil.TicksPerSecond; // ticks in 1 second

long nanoseconds = DateTimeUtil.Ticks2Nanos(ticks);
long milliseconds = DateTimeUtil.Ticks2Millis(ticks);
long seconds = DateTimeUtil.Ticks2Seconds(ticks);
long minutes = DateTimeUtil.Ticks2Minutes(ticks);
long hours = DateTimeUtil.Ticks2Hours(ticks);

Console.WriteLine($"Ticks: {ticks}");
Console.WriteLine($"Nanoseconds: {nanoseconds}");
Console.WriteLine($"Milliseconds: {milliseconds}");
Console.WriteLine($"Seconds: {seconds}");
Console.WriteLine($"Minutes: {minutes}");
Console.WriteLine($"Hours: {hours}");

long backToTicks = DateTimeUtil.Nanos2Ticks(nanoseconds);
Console.WriteLine($"Convert back to Ticks: {backToTicks}");

long fromHours = DateTimeUtil.Hours2Ticks(2);
long fromSeconds = DateTimeUtil.Seconds2Ticks(90);
```

#### Timestamp Operations
```csharp
long currentTicks = DateTimeUtil.NowTicks;
long currentTicks1970 = DateTimeUtil.NowTicks1970;
long currentMilliseconds = DateTimeUtil.NowMilliseconds1970;
long currentSeconds = DateTimeUtil.NowSeconds1970;

Console.WriteLine($"Current Ticks (0001 baseline): {currentTicks}");
Console.WriteLine($"Current Ticks (1970 baseline): {currentTicks1970}");
Console.WriteLine($"Current Milliseconds (1970 baseline): {currentMilliseconds}");
Console.WriteLine($"Current Seconds (1970 baseline): {currentSeconds}");
Console.WriteLine($"Unix epoch ticks: {DateTimeUtil.Ticks1970}");

// timestamp is ticks from 0001.01.01
string formattedTime = DateTimeUtil.FormatDateTime(currentTicks, "yyyy-MM-dd HH:mm:ss");
Console.WriteLine($"Formatted time: {formattedTime}");
```

#### Date Calculation
```csharp
int daysInFebruary2024 = DateTimeUtil.GetMonthDay(2, 2024);
int daysInFebruary2023 = DateTimeUtil.GetMonthDay(2, 2023);

Console.WriteLine($"Days in February 2024: {daysInFebruary2024}"); // 29 (leap year)
Console.WriteLine($"Days in February 2023: {daysInFebruary2023}"); // 28

bool isLeap2024 = DateTimeUtil.CheckLeapYear(2024);
bool isLeap2023 = DateTimeUtil.CheckLeapYear(2023);

Console.WriteLine($"2024 is leap year: {isLeap2024}"); // True
Console.WriteLine($"2023 is leap year: {isLeap2023}"); // False

DateTime startDate = new DateTime(2023, 1, 1);
DateTime endDate = new DateTime(2024, 6, 15);
int monthOffset = DateTimeUtil.GetOffsetMonth(startDate, endDate);

Console.WriteLine($"Month difference from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}: {monthOffset}");
```

#### Time Series Processing
```csharp
var series = new TimeSeries(basestamp: 0, loop: true);

series.AddSlice("morning", DateTimeUtil.Hours2Ticks(8));
series.AddSlice("work", DateTimeUtil.Hours2Ticks(8));
series.AddSlice("evening", DateTimeUtil.Hours2Ticks(8));

Console.WriteLine(series.Contains("work"));          // True
Console.WriteLine(series.FindFirstSlice("work"));    // 1

string name = series.LocateTo(DateTimeUtil.Hours2Ticks(10));
Console.WriteLine($"Slice at 10 hours: {name}");     // work

// Loop locate: wraps after the total series duration
string looped = series.LocateTo(DateTimeUtil.Hours2Ticks(26));
Console.WriteLine($"Slice at 26 hours: {looped}");   // work

series.SetBasestamp(DateTimeUtil.NowTicks1970);
series.RemoveAt(0);
series.RemoveLast();
series.RemoveAll();
```

#### Time Slice Operations
```csharp
var slice = TimeSlice.NewSliceWitDur(0, DateTimeUtil.Hours2Ticks(8));
var slice2 = TimeSlice.NewSliceWithEnd(
    DateTimeUtil.Hours2Ticks(4),
    DateTimeUtil.Hours2Ticks(12)
);

Console.WriteLine($"Start: {slice.Start}, End: {slice.End}, Duration: {slice.Duration}");
Console.WriteLine(slice.Contains(DateTimeUtil.Hours2Ticks(3)));       // True
Console.WriteLine(slice.Contains(slice.End));                        // False (end exclusive by default)
Console.WriteLine(slice.Contains(slice.End, includeEnd: true));      // True

slice.Move(DateTimeUtil.Hours2Ticks(1));
slice.Extend(DateTimeUtil.Hours2Ticks(2));
slice.Zoom(2f, TimeSlice.ZoomAnchor.Center);
slice.Zoom(0.5f, 1f); // anchor 1 = start
```

#### High-Precision Timer
```csharp
var timer = new StampTimer();
timer.Start();

Thread.Sleep(100);

timer.Pause();
Console.WriteLine($"Paused: {timer.IsPause}");
Console.WriteLine($"Lost milliseconds: {timer.LostMilliseconds}");
Console.WriteLine($"Pause milliseconds: {timer.PauseMilliseconds}");

timer.Continue();
Thread.Sleep(50);
timer.Stop();

Console.WriteLine($"Lost ticks: {timer.LostTicks}");
Console.WriteLine($"Lost milliseconds: {timer.LostMilliseconds}");
Console.WriteLine($"Global lost milliseconds: {timer.GlobalLostMilliseconds}");

// Manually adjust lost time
timer.AddLost(DateTimeUtil.TicksPerMilliSecond * 10);
timer.AddRunningLost(DateTimeUtil.TicksPerMilliSecond);
timer.AddPausingLost(DateTimeUtil.TicksPerMilliSecond);

// Baseline lost time / factory / custom clock
var withBase = new StampTimer(DateTimeUtil.TicksPerSecond);
var fromFactory = StampTimer.GenTimer();
fromFactory.SetNowTicksGetter(() => DateTimeUtil.NowTicks1970);
fromFactory.Start();
```

#### Time Formatting
```csharp
DateTime now = DateTime.UtcNow;
string formatted1 = DateTimeUtil.FormatDateTime(now, "yyyy-MM-dd HH:mm:ss");
string formatted2 = DateTimeUtil.FormatDateTime(now, "yyyy-MM-dd HH:mm:ss.fff");

Console.WriteLine($"Format 1: {formatted1}");
Console.WriteLine($"Format 2: {formatted2}");

long timestamp = DateTimeUtil.NowTicks; // ticks from 0001.01.01
string formattedTimestamp = DateTimeUtil.FormatDateTime(timestamp, "yyyy-MM-dd HH:mm:ss");
string formattedLocal = DateTimeUtil.FormatDateTimeLocal(timestamp, "yyyy-MM-dd HH:mm:ss");

Console.WriteLine($"Timestamp formatting (UTC): {formattedTimestamp}");
Console.WriteLine($"Local time formatting: {formattedLocal}");
```

#### Performance Testing
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

Console.WriteLine($"1 million time conversions took: {performanceTimer.LostMilliseconds}ms");

performanceTimer = StampTimer.GenTimer();
performanceTimer.Start();
for (int i = 0; i < 100000; i++)
{
    long ticks = DateTimeUtil.NowTicks;
    long ms = DateTimeUtil.NowMilliseconds1970;
}
performanceTimer.Stop();

Console.WriteLine($"100k timestamp retrievals took: {performanceTimer.LostMilliseconds}ms");
```

### Design Features

1. **High precision**: tick-based units, convertible to nanoseconds
2. **Two baselines**: current timestamps from both 0001-01-01 and 1970-01-01 UTC
3. **Unit conversion**: bidirectional conversion and constants for common units
4. **Named time series**: `TimeSeries` manages named slices and supports looped location
5. **Mutable slices**: `TimeSlice` supports move, extend, and anchored zoom
6. **Pausable timer**: `StampTimer` separates running lost time from pause time and accepts a custom clock

### Considerations

1. **Precision**: 1 tick = 100 nanoseconds; nanosecond readings are limited by system clock resolution
2. **Time zone**: current timestamps use UTC; `FormatDateTime(long)` formats as UTC, `FormatDateTimeLocal` converts to local time
3. **Timestamp unit**: `FormatDateTime(long)` / `FormatDateTimeLocal` take **ticks from 0001-01-01**, not Unix milliseconds
4. **TimeSlice type**: start, end, and duration are `long`, not `DateTime`; it is a mutable struct (copy semantics apply)
5. **StampTimer clock**: defaults to `DateTimeUtil.NowTicks1970`; `Stop` internally calls `Pause`
6. **Naming**: `TicksPerWeak` is ticks per week; `NewSliceWitDur` is the factory method name in source
