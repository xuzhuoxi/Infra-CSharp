# DateTimex API Documentation

## Namespace: JLGames.Infra.DateTimex

### Static Classes

#### DateTimeUtil
DateTime utility class

```csharp
/// <summary>
/// DateTime utility class
/// Provides utility methods and constants related to date and time
/// 
/// More information:
///   TimeSpan
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

    // Current timestamp properties
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
    /// <param name="ticks">Number of ticks</param>
    /// <returns>Number of nanoseconds</returns>
    public static long Ticks2Nanos(long ticks);

    /// <summary>
    /// Ticks to milliseconds
    /// </summary>
    /// <param name="ticks">Number of ticks</param>
    /// <returns>Number of milliseconds</returns>
    public static long Ticks2Millis(long ticks);

    /// <summary>
    /// Ticks to seconds
    /// </summary>
    /// <param name="ticks">Number of ticks</param>
    /// <returns>Number of seconds</returns>
    public static long Ticks2Seconds(long ticks);

    /// <summary>
    /// Ticks to minutes
    /// </summary>
    /// <param name="ticks">Number of ticks</param>
    /// <returns>Number of minutes</returns>
    public static long Ticks2Minutes(long ticks);

    /// <summary>
    /// Ticks to hours
    /// </summary>
    /// <param name="ticks">Number of ticks</param>
    /// <returns>Number of hours</returns>
    public static long Ticks2Hours(long ticks);

    /// <summary>
    /// Nanoseconds to ticks
    /// </summary>
    /// <param name="nanos">Number of nanoseconds</param>
    /// <returns>Number of ticks</returns>
    public static long Nanos2Ticks(long nanos);

    /// <summary>
    /// Milliseconds to ticks
    /// </summary>
    /// <param name="millisecond">Number of milliseconds</param>
    /// <returns>Number of ticks</returns>
    public static long Millis2Ticks(long millisecond);

    /// <summary>
    /// Seconds to ticks
    /// </summary>
    /// <param name="seconds">Number of seconds</param>
    /// <returns>Number of ticks</returns>
    public static long Seconds2Ticks(long seconds);

    /// <summary>
    /// Minutes to ticks
    /// </summary>
    /// <param name="minutes">Number of minutes</param>
    /// <returns>Number of ticks</returns>
    public static long Minutes2Ticks(long minutes);

    /// <summary>
    /// Hours to ticks
    /// </summary>
    /// <param name="hours">Number of hours</param>
    /// <returns>Number of ticks</returns>
    public static long Hours2Ticks(long hours);

    // Date calculation methods
    /// <summary>
    /// Get the number of days in the specified month
    /// </summary>
    /// <param name="month">Month</param>
    /// <param name="year">Year</param>
    /// <returns>Number of days</returns>
    public static int GetMonthDay(int month, int year);

    /// <summary>
    /// Check if it is a leap year
    /// </summary>
    /// <param name="year">Year</param>
    /// <returns>Whether it is a leap year</returns>
    public static bool CheckLeapYear(int year);

    /// <summary>
    /// Get the month difference between two dates
    /// </summary>
    /// <param name="start">Start date</param>
    /// <param name="end">End date</param>
    /// <returns>Month difference</returns>
    public static int GetOffsetMonth(DateTime start, DateTime end);

    // Formatting methods
    /// <summary>
    /// Format date time
    /// </summary>
    /// <param name="dt">DateTime</param>
    /// <param name="format">Format</param>
    /// <param name="provider">Format provider</param>
    /// <returns>Formatted string</returns>
    public static string FormatDateTime(DateTime dt, string format, IFormatProvider provider = null);

    /// <summary>
    /// Format timestamp
    /// </summary>
    /// <param name="timestamp">Timestamp</param>
    /// <param name="format">Format</param>
    /// <param name="provider">Format provider</param>
    /// <returns>Formatted string</returns>
    public static string FormatDateTime(long timestamp, string format, IFormatProvider provider = null);

    /// <summary>
    /// Format local timestamp
    /// </summary>
    /// <param name="timestamp">Timestamp</param>
    /// <param name="format">Format</param>
    /// <param name="provider">Format provider</param>
    /// <returns>Formatted string</returns>
    public static string FormatDateTimeLocal(long timestamp, string format, IFormatProvider provider = null);
}
```

#### TimeSeries
Time series class

```csharp
/// <summary>
/// Time series class
/// Used for processing time series data
/// </summary>
public class TimeSeries
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="startTime">Start time</param>
    /// <param name="endTime">End time</param>
    /// <param name="interval">Time interval</param>
    public TimeSeries(DateTime startTime, DateTime endTime, TimeSpan interval);

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime StartTime { get; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime EndTime { get; }

    /// <summary>
    /// Time interval
    /// </summary>
    public TimeSpan Interval { get; }

    /// <summary>
    /// Number of time points
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Get the time point at the specified index
    /// </summary>
    /// <param name="index">Index</param>
    /// <returns>Time point</returns>
    public DateTime this[int index] { get; }

    /// <summary>
    /// Get time series enumerator
    /// </summary>
    /// <returns>Time series enumerator</returns>
    public IEnumerator<DateTime> GetEnumerator();
}
```

#### TimeSlice
Time slice class

```csharp
/// <summary>
/// Time slice class
/// Represents a time interval
/// </summary>
public struct TimeSlice
{
    /// <summary>
    /// Start time
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="start">Start time</param>
    /// <param name="end">End time</param>
    public TimeSlice(DateTime start, DateTime end);

    /// <summary>
    /// Duration
    /// </summary>
    public TimeSpan Duration { get; }

    /// <summary>
    /// Whether it contains the specified time
    /// </summary>
    /// <param name="time">Time</param>
    /// <returns>Whether it contains</returns>
    public bool Contains(DateTime time);

    /// <summary>
    /// Whether it overlaps with another time slice
    /// </summary>
    /// <param name="other">Another time slice</param>
    /// <returns>Whether it overlaps</returns>
    public bool Overlaps(TimeSlice other);

    /// <summary>
    /// Get intersection
    /// </summary>
    /// <param name="other">Another time slice</param>
    /// <returns>Intersection time slice</returns>
    public TimeSlice? Intersect(TimeSlice other);

    /// <summary>
    /// Get union
    /// </summary>
    /// <param name="other">Another time slice</param>
    /// <returns>Union time slice</returns>
    public TimeSlice Union(TimeSlice other);
}
```

#### StampTimer
Timestamp timer class

```csharp
/// <summary>
/// Timestamp timer class
/// Provides high-precision timestamp timing functionality
/// </summary>
    public class StampTimer
    {
        private long m_StartTicks;
        private long m_StopTicks;
        private bool m_IsRunning;

            /// <summary>
    /// Whether it is running
    /// </summary>
        public bool IsRunning => m_IsRunning;

            /// <summary>
    /// Start timing
    /// </summary>
        public void Start();

            /// <summary>
    /// Stop timing
    /// </summary>
        public void Stop();

            /// <summary>
    /// Reset timer
    /// </summary>
        public void Reset();

            /// <summary>
    /// Restart timer
    /// </summary>
        public void Restart();

            /// <summary>
    /// Get elapsed milliseconds
    /// </summary>
    /// <returns>Number of milliseconds</returns>
        public long GetElapsedMilliseconds();

            /// <summary>
    /// Get elapsed microseconds
    /// </summary>
    /// <returns>Number of microseconds</returns>
        public long GetElapsedMicroseconds();

            /// <summary>
    /// Get elapsed nanoseconds
    /// </summary>
    /// <returns>Number of nanoseconds</returns>
        public long GetElapsedNanoseconds();

            /// <summary>
    /// Get elapsed time span
    /// </summary>
    /// <returns>Time span</returns>
        public TimeSpan GetElapsedTime();
    }
```

### Feature Description

#### Time Unit Conversion

**Supported Time Units**
- **Nanoseconds**: Smallest time unit, 1 nanosecond = 10 Ticks
- **Microseconds**: 1 microsecond = 10 nanoseconds
- **Milliseconds**: 1 millisecond = 1000 microseconds
- **Seconds**: 1 second = 1000 milliseconds
- **Minutes**: 1 minute = 60 seconds
- **Hours**: 1 hour = 60 minutes
- **Days**: 1 day = 24 hours
- **Weeks**: 1 week = 7 days
- **Months**: 28/29/30/31 days
- **Years**: 365 days (366 days in leap year)

**Conversion Methods**
1. **Ticks to other units**: Ticks2Nanos, Ticks2Millis, Ticks2Seconds, etc.
2. **Other units to Ticks**: Nanos2Ticks, Millis2Ticks, Seconds2Ticks, etc.
3. **Bidirectional conversion**: Supports bidirectional conversion of all time units

#### Timestamp System

**Timestamp Baselines**
- **Unix Timestamp**: Number of seconds since January 1, 1970
- **Ticks Timestamp**: Number of ticks since January 1, 0001
- **Millisecond Timestamp**: Number of milliseconds since baseline time
- **Nanosecond Timestamp**: Number of nanoseconds since baseline time

**Current Timestamps**
- **NowTicks**: Current ticks since 0001.1.1
- **NowTicks1970**: Current ticks since 1970.1.1
- **NowMilliseconds1970**: Current milliseconds since 1970.1.1
- **NowSeconds1970**: Current seconds since 1970.1.1

#### Date Calculation

**Month Day Calculation**
- **GetMonthDay**: Calculate number of days based on year and month
- **CheckLeapYear**: Check if it is a leap year
- **GetOffsetMonth**: Calculate month difference between two dates

**Time Series Processing**
- **TimeSeries**: Process time series data
- **TimeSlice**: Represent time intervals
- **StampTimer**: High-precision timer

### Usage Examples

#### Time Unit Conversion
```csharp
// Basic time unit conversion
long ticks = 1000000; // Number of ticks for 1 second

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

// Reverse conversion
long backToTicks = DateTimeUtil.Nanos2Ticks(nanoseconds);
Console.WriteLine($"Convert back to Ticks: {backToTicks}");
```

#### Timestamp Operations
```csharp
// Get current timestamps
long currentTicks = DateTimeUtil.NowTicks;
long currentTicks1970 = DateTimeUtil.NowTicks1970;
long currentMilliseconds = DateTimeUtil.NowMilliseconds1970;
long currentSeconds = DateTimeUtil.NowSeconds1970;

Console.WriteLine($"Current Ticks (0001 baseline): {currentTicks}");
Console.WriteLine($"Current Ticks (1970 baseline): {currentTicks1970}");
Console.WriteLine($"Current Milliseconds (1970 baseline): {currentMilliseconds}");
Console.WriteLine($"Current Seconds (1970 baseline): {currentSeconds}");

// Timestamp formatting
string formattedTime = DateTimeUtil.FormatDateTime(currentMilliseconds, "yyyy-MM-dd HH:mm:ss");
Console.WriteLine($"Formatted time: {formattedTime}");
```

#### Date Calculation
```csharp
// Month day calculation
int daysInFebruary2024 = DateTimeUtil.GetMonthDay(2, 2024);
int daysInFebruary2023 = DateTimeUtil.GetMonthDay(2, 2023);

Console.WriteLine($"Days in February 2024: {daysInFebruary2024}"); // 29 (leap year)
Console.WriteLine($"Days in February 2023: {daysInFebruary2023}"); // 28

// Leap year check
bool isLeap2024 = DateTimeUtil.CheckLeapYear(2024);
bool isLeap2023 = DateTimeUtil.CheckLeapYear(2023);

Console.WriteLine($"2024 is leap year: {isLeap2024}"); // True
Console.WriteLine($"2023 is leap year: {isLeap2023}"); // False

// Month difference calculation
DateTime startDate = new DateTime(2023, 1, 1);
DateTime endDate = new DateTime(2024, 6, 15);
int monthOffset = DateTimeUtil.GetOffsetMonth(startDate, endDate);

Console.WriteLine($"Month difference from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}: {monthOffset}");
```

#### Time Series Processing
```csharp
// Create time series
DateTime startTime = new DateTime(2024, 1, 1);
DateTime endTime = new DateTime(2024, 1, 31);
TimeSpan interval = TimeSpan.FromDays(1);

var timeSeries = new TimeSeries(startTime, endTime, interval);

Console.WriteLine($"Time series length: {timeSeries.Count}");
Console.WriteLine($"Time interval: {interval}");

// Iterate through time series
foreach (DateTime time in timeSeries)
{
    Console.WriteLine($"Time point: {time:yyyy-MM-dd}");
}
```

#### Time Slice Operations
```csharp
// Create time slices
var slice1 = new TimeSlice(
    new DateTime(2024, 1, 1, 9, 0, 0),
    new DateTime(2024, 1, 1, 17, 0, 0)
);

var slice2 = new TimeSlice(
    new DateTime(2024, 1, 1, 14, 0, 0),
    new DateTime(2024, 1, 1, 18, 0, 0)
);

Console.WriteLine($"Slice 1: {slice1.Start:HH:mm} - {slice1.End:HH:mm}");
Console.WriteLine($"Slice 2: {slice2.Start:HH:mm} - {slice2.End:HH:mm}");
Console.WriteLine($"Slice 1 duration: {slice1.Duration}");

// Check overlap
bool overlaps = slice1.Overlaps(slice2);
Console.WriteLine($"Overlaps: {overlaps}");

// Get intersection
var intersection = slice1.Intersect(slice2);
if (intersection.HasValue)
{
    Console.WriteLine($"Intersection: {intersection.Value.Start:HH:mm} - {intersection.Value.End:HH:mm}");
}
```

#### High-Precision Timer
```csharp
// Create timer
var timer = new StampTimer();

// Start timing
timer.Start();

// Perform some operations
Thread.Sleep(100); // Simulate time-consuming operation

// Stop timing
timer.Stop();

// Get timing results
long elapsedMs = timer.GetElapsedMilliseconds();
long elapsedMicros = timer.GetElapsedMicroseconds();
long elapsedNanos = timer.GetElapsedNanoseconds();
TimeSpan elapsedTime = timer.GetElapsedTime();

Console.WriteLine($"Elapsed time:");
Console.WriteLine($"  Milliseconds: {elapsedMs}");
Console.WriteLine($"  Microseconds: {elapsedMicros}");
Console.WriteLine($"  Nanoseconds: {elapsedNanos}");
Console.WriteLine($"  Time span: {elapsedTime}");

// Restart timer
timer.Restart();
// ... perform other operations
timer.Stop();
Console.WriteLine($"Elapsed time after restart: {timer.GetElapsedMilliseconds()}ms");
```

#### Time Formatting
```csharp
// Format current time
DateTime now = DateTime.Now;
string formatted1 = DateTimeUtil.FormatDateTime(now, "yyyy-MM-dd HH:mm:ss");
string formatted2 = DateTimeUtil.FormatDateTime(now, "yyyy-MM-dd HH:mm:ss.fff");

Console.WriteLine($"Format 1: {formatted1}");
Console.WriteLine($"Format 2: {formatted2}");

// Format timestamp
long timestamp = DateTimeUtil.NowMilliseconds1970;
string formattedTimestamp = DateTimeUtil.FormatDateTime(timestamp, "yyyy-MM-dd HH:mm:ss");
string formattedLocal = DateTimeUtil.FormatDateTimeLocal(timestamp, "yyyy-MM-dd HH:mm:ss");

Console.WriteLine($"Timestamp formatting: {formattedTimestamp}");
Console.WriteLine($"Local time formatting: {formattedLocal}");
```

#### Performance Testing
```csharp
// Use timer for performance testing
var performanceTimer = new StampTimer();

// Test time unit conversion performance
performanceTimer.Start();
for (int i = 0; i < 1000000; i++)
{
    long ticks = i * 1000;
    long ms = DateTimeUtil.Ticks2Millis(ticks);
    long backToTicks = DateTimeUtil.Millis2Ticks(ms);
}
performanceTimer.Stop();

Console.WriteLine($"1 million time conversions took: {performanceTimer.GetElapsedMilliseconds()}ms");

// Test timestamp retrieval performance
performanceTimer.Restart();
for (int i = 0; i < 100000; i++)
{
    long ticks = DateTimeUtil.NowTicks;
    long ms = DateTimeUtil.NowMilliseconds1970;
}
performanceTimer.Stop();

Console.WriteLine($"100k timestamp retrievals took: {performanceTimer.GetElapsedMilliseconds()}ms");
```

### Design Features

1. **High Precision**: Supports nanosecond-level precision
2. **Multiple Baselines**: Supports both Unix and Ticks time baselines
3. **Complete Conversion**: Supports bidirectional conversion of all time units
4. **Practical Tools**: Provides date calculation and formatting functions
5. **Performance Optimization**: Uses constants to avoid repeated calculations
6. **Easy to Use**: Provides concise API interfaces

### Considerations

1. **Precision Considerations**: Nanosecond-level operations may be limited by system precision
2. **Timezone Handling**: Pay attention to the difference between UTC and local time
3. **Performance Impact**: Frequent timestamp retrieval may affect performance
4. **Memory Usage**: Pay attention to memory usage for large time series data
5. **Thread Safety**: Time operations in multi-threaded environments
6. **Baseline Time**: Pay attention to differences between different time baselines 