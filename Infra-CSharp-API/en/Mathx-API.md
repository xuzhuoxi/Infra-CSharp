# Mathx API Documentation

## Namespace: JLGames.Infra.Mathx

Points, line segments, intervals, 2D bounds, bit masks, and common math helpers.

### Structs

#### Point1
1D point (`double`)

```csharp
/// <summary>
/// 1D point
/// </summary>
public struct Point1
{
    /// <summary>
    /// Area point
    /// </summary>
    public struct AreaPoint1
    {
        /// <summary>
        /// Area index
        /// </summary>
        public long AreaIndex;

        /// <summary>
        /// Area remainder
        /// </summary>
        public double Remainder;
    }

    /// <summary>
    /// Coordinate value on the 1D axis.
    /// </summary>
    public double Value;

    /// <summary>
    /// Creates a 1D point with the given value.
    /// </summary>
    /// <param name="value">Coordinate value.</param>
    public Point1(double value);

    /// <summary>
    /// Implicit conversion from double to Point1.
    /// </summary>
    public static implicit operator Point1(double point);

    /// <summary>
    /// Implicit conversion from Point1 to double.
    /// </summary>
    public static implicit operator double(Point1 point);

    /// <summary>
    /// Convert to area point
    /// </summary>
    public static AreaPoint1 ToAreaPoint1(double point1, ulong size);

    /// <summary>
    /// Convert to 1D point
    /// </summary>
    public static Point1 FromAreaPoint1(AreaPoint1 point1, ulong size);
}
```

#### Point1Int
1D point (integer)

```csharp
/// <summary>
/// 1D point(Integer)
/// </summary>
public struct Point1Int
{
    /// <summary>
    /// Area point
    /// </summary>
    public struct AreaPoint1Int
    {
        /// <summary>
        /// Area index
        /// </summary>
        public int AreaIndex;

        /// <summary>
        /// Area remainder
        /// </summary>
        public int Remainder;
    }

    /// <summary>
    /// Coordinate value on the 1D axis.
    /// </summary>
    public int Value;

    /// <summary>
    /// Creates a 1D integer point with the given value.
    /// </summary>
    /// <param name="value">Coordinate value.</param>
    public Point1Int(int value);

    /// <summary>
    /// Implicit conversion from int to Point1Int.
    /// </summary>
    public static implicit operator Point1Int(int point);

    /// <summary>
    /// Implicit conversion from Point1Int to int.
    /// </summary>
    public static implicit operator int(Point1Int point);

    /// <summary>
    /// Convert to area point
    /// </summary>
    public static AreaPoint1Int ToAreaPoint1(int point1, uint size);

    /// <summary>
    /// Convert to 1D point
    /// </summary>
    public static Point1Int FromAreaPoint1(AreaPoint1Int point1, uint size);
}
```

#### Point2Int
2D integer point

```csharp
/// <summary>
/// 2D point (Integer)
/// </summary>
public struct Point2Int : IEquatable<Point2Int>
{
    /// <summary>
    /// X coordinate.
    /// </summary>
    public int X;

    /// <summary>
    /// Y coordinate.
    /// </summary>
    public int Y;

    /// <summary>
    /// Component access by index: 0 = X, 1 = Y.
    /// </summary>
    /// <param name="index">Component index (0 or 1).</param>
    /// <exception cref="IndexOutOfRangeException">Index is not 0 or 1.</exception>
    public int this[int index] { get; set; }

    /// <summary>
    /// Creates a 2D integer point.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    public Point2Int(int x, int y);

    /// <summary>
    /// Sets both coordinates.
    /// </summary>
    public void Set(int newX, int newY);

    /// <summary>
    /// Origin (0, 0).
    /// </summary>
    public static readonly Point2Int Zero;

    /// <summary>
    /// Component-wise addition.
    /// </summary>
    public static Point2Int operator +(Point2Int b, Point2Int c);

    /// <summary>
    /// Component-wise subtraction.
    /// </summary>
    public static Point2Int operator -(Point2Int b, Point2Int c);

    /// <summary>
    /// Inequality comparison.
    /// </summary>
    public static bool operator !=(Point2Int b, Point2Int c);

    /// <summary>
    /// Equality comparison.
    /// </summary>
    public static bool operator ==(Point2Int b, Point2Int c);

    /// <summary>
    /// Promotes to Point3Int with Z = 0.
    /// </summary>
    public static implicit operator Point3Int(Point2Int v);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Point2Int other);
}
```

#### Point3Int
3D integer point

```csharp
/// <summary>
/// 3D point (Integer)
/// </summary>
public struct Point3Int : IEquatable<Point3Int>
{
    /// <summary>
    /// X coordinate.
    /// </summary>
    public int X;

    /// <summary>
    /// Y coordinate.
    /// </summary>
    public int Y;

    /// <summary>
    /// Z coordinate.
    /// </summary>
    public int Z;

    /// <summary>
    /// Component access by index: 0 = X, 1 = Y, 2 = Z.
    /// </summary>
    /// <param name="index">Component index (0, 1, or 2).</param>
    /// <exception cref="IndexOutOfRangeException">Index is not 0, 1, or 2.</exception>
    public int this[int index] { get; set; }

    /// <summary>
    /// Creates a 3D integer point.
    /// </summary>
    public Point3Int(int x, int y, int z);

    /// <summary>
    /// Sets all three coordinates.
    /// </summary>
    public void Set(int newX, int newY, int newZ);

    /// <summary>
    /// Component-wise addition.
    /// </summary>
    public static Point3Int operator +(Point3Int b, Point3Int c);

    /// <summary>
    /// Component-wise subtraction.
    /// </summary>
    public static Point3Int operator -(Point3Int b, Point3Int c);

    /// <summary>
    /// Inequality comparison.
    /// </summary>
    public static bool operator !=(Point3Int b, Point3Int c);

    /// <summary>
    /// Equality comparison.
    /// </summary>
    public static bool operator ==(Point3Int b, Point3Int c);

    /// <summary>
    /// Drops Z and returns the XY components as Point2Int.
    /// </summary>
    public static implicit operator Point2Int(Point3Int v);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Point3Int other);
}
```

#### Bounds2Int
Axis-aligned 2D integer bounds `[XMin, XMax) × [YMin, YMax)` (half-open)

```csharp
/// <summary>
/// Axis-aligned 2D integer bounds [XMin, XMax) × [YMin, YMax) (half-open).
/// </summary>
public struct Bounds2Int : IEquatable<Bounds2Int>
{
    /// <summary>
    /// Minimum X (inclusive).
    /// </summary>
    public int XMin { get; set; }

    /// <summary>
    /// Minimum Y (inclusive).
    /// </summary>
    public int YMin { get; set; }

    /// <summary>
    /// Maximum X (exclusive).
    /// </summary>
    public int XMax { get; set; }

    /// <summary>
    /// Maximum Y (exclusive).
    /// </summary>
    public int YMax { get; set; }

    /// <summary>
    /// Width (XMax − XMin).
    /// </summary>
    public int XSize { get; set; }

    /// <summary>
    /// Height (YMax − YMin).
    /// </summary>
    public int YSize { get; set; }

    /// <summary>
    /// Minimum corner (inclusive).
    /// </summary>
    public Point2Int Min { get; set; }

    /// <summary>
    /// Maximum corner (exclusive).
    /// </summary>
    public Point2Int Max { get; set; }

    /// <summary>
    /// Center X coordinate.
    /// </summary>
    public int XCenter { get; }

    /// <summary>
    /// Center Y coordinate.
    /// </summary>
    public int YCenter { get; }

    /// <summary>
    /// Center point.
    /// </summary>
    public Point2Int Center { get; }

    /// <summary>
    /// Size as (XSize, YSize).
    /// </summary>
    public Point2Int Size { get; }

    /// <summary>
    /// Number of unit cells (XSize × YSize).
    /// </summary>
    public int Area { get; }

    /// <summary>
    /// Whether the bounds have zero area.
    /// </summary>
    public bool IsNone { get; }

    /// <summary>
    /// Creates bounds from min and max corners.
    /// </summary>
    public Bounds2Int(Point2Int min, Point2Int max);

    /// <summary>
    /// Creates bounds from axis limits.
    /// </summary>
    public Bounds2Int(int xMin, int yMin, int xMax, int yMax);

    /// <summary>
    /// Replaces min and max corners.
    /// </summary>
    public void Set(Point2Int min, Point2Int max);

    /// <summary>
    /// Whether the point lies inside this bounds (half-open).
    /// </summary>
    public bool Contains(Point2Int point);

    /// <summary>
    /// Whether (x, y) lies inside this bounds (half-open).
    /// </summary>
    public bool Contains(int x, int y);

    /// <summary>
    /// Whether x is in [XMin, XMax).
    /// </summary>
    public bool ContainsX(int x);

    /// <summary>
    /// Whether y is in [YMin, YMax).
    /// </summary>
    public bool ContainsY(int y);

    /// <summary>
    /// Check if two bounds intersect
    /// </summary>
    /// <param name="bounds2Int">Other bounds.</param>
    /// <returns>True if any corner of the other bounds lies inside this bounds.</returns>
    public bool Intersect(Bounds2Int bounds2Int);

    /// <summary>
    /// Convert to point array
    /// </summary>
    /// <returns>All integer points in the bounds, or null if empty.</returns>
    public Point2Int[] ToArray();

    /// <summary>
    /// Returns a copy translated by offset.
    /// </summary>
    public Bounds2Int Move(Point2Int offset);

    /// <summary>
    /// Intersection with another bounds (as corners).
    /// </summary>
    public Bounds2Int Crop(Bounds2Int subCrop);

    /// <summary>
    /// Intersection with the axis-aligned box [min, max).
    /// </summary>
    public Bounds2Int Crop2(Point2Int min, Point2Int max);

    /// <summary>
    /// Intersection with the axis-aligned box [minX, maxX) × [minY, maxY).
    /// </summary>
    public Bounds2Int Crop2(int minX, int minY, int maxX, int maxY);

    /// <summary>
    /// Intersection cropped along X only.
    /// </summary>
    public Bounds2Int CropX(int minX, int maxX);

    /// <summary>
    /// Intersection cropped along Y only.
    /// </summary>
    public Bounds2Int CropY(int minY, int maxY);

    /// <summary>
    /// Split with x
    /// </summary>
    /// <param name="x">Split line (must lie inside X range for a real split).</param>
    /// <returns>One or two sub-bounds.</returns>
    public Bounds2Int[] SplitX(int x);

    /// <summary>
    /// Split with y
    /// </summary>
    /// <param name="y">Split line (must lie inside Y range for a real split).</param>
    /// <returns>One or two sub-bounds.</returns>
    public Bounds2Int[] SplitY(int y);

    /// <summary>
    /// Union of grid points from this bounds and others (deduplicated).
    /// </summary>
    public Point2Int[] Add(Bounds2Int add, params Bounds2Int[] other);

    /// <summary>
    /// Grid points in this bounds minus those in sub and others.
    /// </summary>
    public Point2Int[] Sub(Bounds2Int sub, params Bounds2Int[] other);

    /// <summary>
    /// Inequality comparison.
    /// </summary>
    public static bool operator !=(Bounds2Int b, Bounds2Int c);

    /// <summary>
    /// Equality comparison.
    /// </summary>
    public static bool operator ==(Bounds2Int b, Bounds2Int c);

    /// <summary>
    /// Scales bounds about center (ceil per axis).
    /// </summary>
    public static Bounds2Int operator /(Bounds2Int b, float c);

    /// <summary>
    /// Scales bounds about center (ceil per axis).
    /// </summary>
    public static Bounds2Int operator *(Bounds2Int b, float c);

    /// <summary>
    /// Builds half-open bounds centered at center with given size.
    /// </summary>
    public static Bounds2Int NewCenterBound(Point2Int center, Point2Int size);

    /// <summary>
    /// Builds half-open bounds centered at (centerX, centerY) with given size.
    /// </summary>
    public static Bounds2Int NewCenterBound(int centerX, int centerY, int sizeX, int sizeY);

    /// <summary>
    /// Empty bounds (zero area).
    /// </summary>
    public static readonly Bounds2Int Empty;

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Bounds2Int other);
}
```

#### Line1
1D line segment (floating-point endpoints)

```csharp
/// <summary>
/// 1D line segment (floating-point endpoints).
/// </summary>
public struct Line1 : IEquatable<Line1>
{
    /// <summary>
    /// Start endpoint.
    /// </summary>
    public double Start;

    /// <summary>
    /// End endpoint.
    /// </summary>
    public double End;

    /// <summary>
    /// Smaller of Start and End.
    /// </summary>
    public double Min { get; }

    /// <summary>
    /// Larger of Start and End.
    /// </summary>
    public double Max { get; }

    /// <summary>
    /// Signed length (End − Start).
    /// </summary>
    public double Size { get; }

    /// <summary>
    /// Absolute length.
    /// </summary>
    public double AbsSize { get; }

    /// <summary>
    /// Whether start and end coincide (within float tolerance).
    /// </summary>
    public bool IsPoint { get; }

    /// <summary>
    /// Creates a 1D segment.
    /// </summary>
    public Line1(double start, double end);

    /// <summary>
    /// Splits this segment into sub-segments of length size, aligned at coordinate 0.
    /// </summary>
    /// <param name="size">Segment length (must be non-zero).</param>
    /// <returns>Sub-segments, or null if size is zero.</returns>
    public Line1[] SliceAtZero(double size);

    /// <summary>
    /// Splits this segment aligned at Start.
    /// </summary>
    public Line1[] SliceAtStart(double size);

    /// <summary>
    /// Splits this segment aligned at End.
    /// </summary>
    public Line1[] SliceAtEnd(double size);

    /// <summary>
    /// Splits this segment aligned at Min.
    /// </summary>
    public Line1[] SliceAtMin(double size);

    /// <summary>
    /// Splits this segment aligned at Max.
    /// </summary>
    public Line1[] SliceAtMax(double size);

    /// <summary>
    /// Split into multiple Line1s based on basis Point
    /// </summary>
    /// <param name="basisPoint">Alignment reference on the axis.</param>
    /// <param name="size">Segment length (must be non-zero).</param>
    /// <returns>Sub-segments, or null if size is zero.</returns>
    public Line1[] SliceAt(double basisPoint, double size);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Line1 other);
}
```

#### Line1Int
1D line segment (integer endpoints)

```csharp
/// <summary>
/// 1D line segment (integer endpoints).
/// </summary>
public struct Line1Int : IEquatable<Line1Int>
{
    /// <summary>
    /// Start endpoint.
    /// </summary>
    public int Start;

    /// <summary>
    /// End endpoint.
    /// </summary>
    public int End;

    /// <summary>
    /// Smaller of Start and End.
    /// </summary>
    public int Min { get; }

    /// <summary>
    /// Larger of Start and End.
    /// </summary>
    public int Max { get; }

    /// <summary>
    /// Signed length (End − Start).
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Absolute length.
    /// </summary>
    public int AbsSize { get; }

    /// <summary>
    /// Whether start and end are equal.
    /// </summary>
    public bool IsPoint { get; }

    /// <summary>
    /// Creates a 1D integer segment.
    /// </summary>
    public Line1Int(int start, int end);

    /// <summary>
    /// Splits this segment into sub-segments of length size, aligned at coordinate 0.
    /// </summary>
    public Line1Int[] SliceAtZero(int size);

    /// <summary>
    /// Splits this segment aligned at Start.
    /// </summary>
    public Line1Int[] SliceAtStart(int size);

    /// <summary>
    /// Splits this segment aligned at End.
    /// </summary>
    public Line1Int[] SliceAtEnd(int size);

    /// <summary>
    /// Splits this segment aligned at Min.
    /// </summary>
    public Line1Int[] SliceAtMin(int size);

    /// <summary>
    /// Splits this segment aligned at Max.
    /// </summary>
    public Line1Int[] SliceAtMax(int size);

    /// <summary>
    /// Split into multiple Line1Int segments based on basis point
    /// </summary>
    /// <param name="basisPoint">Alignment reference on the axis.</param>
    /// <param name="size">Segment length (must be non-zero).</param>
    /// <returns>Sub-segments, or null if size is zero.</returns>
    public Line1Int[] SliceAt(int basisPoint, int size);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Line1Int other);
}
```

#### Line2Int
2D line segment with integer endpoints

```csharp
/// <summary>
/// 2D line segment with integer endpoints.
/// </summary>
public struct Line2Int : IEquatable<Line2Int>
{
    /// <summary>
    /// Start point.
    /// </summary>
    public Point2Int Start;

    /// <summary>
    /// End point.
    /// </summary>
    public Point2Int End;

    /// <summary>
    /// Squared length (avoids sqrt).
    /// </summary>
    public int SqrMagnitude { get; }

    /// <summary>
    /// Euclidean length.
    /// </summary>
    public double Magnitude { get; }

    /// <summary>
    /// Whether start and end coincide.
    /// </summary>
    public bool IsPoint { get; }

    /// <summary>
    /// Creates a 2D integer segment.
    /// </summary>
    public Line2Int(Point2Int start, Point2Int end);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Line2Int other);
}
```

#### Interval
Closed float interval `[Min, Max]`

```csharp
/// <summary>
/// Closed float interval [Min, Max].
/// </summary>
[Serializable]
public struct Interval
{
    /// <summary>
    /// The min value of the interval.
    /// </summary>
    public float Min;

    /// <summary>
    /// The max value of the interval.
    /// </summary>
    public float Max;

    /// <summary>
    /// The length of the interval (inclusive).
    /// </summary>
    public float Length { get; set; }

    /// <summary>
    /// Constructs a new Interval with given min, max values.
    /// </summary>
    public Interval(float min, float max);
}
```

#### IntervalInt
Integer interval `[Min, Max]` with optional inclusive maximum

```csharp
/// <summary>
/// Integer interval [Min, Max] with optional inclusive maximum.
/// </summary>
[Serializable]
public struct IntervalInt
{
    /// <summary>
    /// The min value of the interval. where 0 is the first position, 1 is the second, 2 is the third, and so on.
    /// </summary>
    public int Min;

    /// <summary>
    /// The max value of the interval.
    /// </summary>
    public int Max;

    /// <summary>
    /// Whether Max is included in the interval when computing Length.
    /// </summary>
    public bool MaxIncluded;

    /// <summary>
    /// The length of the interval.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Constructs a new IntervalInt with given min, max, maxIncluded values.
    /// </summary>
    public IntervalInt(int min, int max, bool maxIncluded);
}
```

#### Range
Half-open float range `[Start, Start + Length)`

```csharp
/// <summary>
/// Half-open float range [Start, Start + Length).
/// </summary>
[Serializable]
public struct Range
{
    /// <summary>
    /// The starting index of the range, where 0 is the first position, 1 is the second, 2 is the third, and so on.
    /// </summary>
    public float Start;

    /// <summary>
    /// The length of the range.
    /// </summary>
    public float Length;

    /// <summary>
    /// The end index of the range (not inclusive).
    /// </summary>
    public float End { get; set; }

    /// <summary>
    /// Constructs a new Range with given start, length values.
    /// </summary>
    /// <param name="start">The starting index of the range.</param>
    /// <param name="length">The length of the range.</param>
    public Range(float start, float length);
}
```

#### RangeInt
Half-open integer range `[Start, Start + Length)`

```csharp
/// <summary>
/// Half-open integer range [Start, Start + Length).
/// </summary>
[Serializable]
public struct RangeInt
{
    /// <summary>
    /// The starting index of the range, where 0 is the first position, 1 is the second, 2 is the third, and so on.
    /// </summary>
    public int Start;

    /// <summary>
    /// The length of the range.
    /// </summary>
    public int Length;

    /// <summary>
    /// The end index of the range (not inclusive).
    /// </summary>
    public int End { get; set; }

    /// <summary>
    /// Constructs a new RangeInt with given start, length values.
    /// </summary>
    /// <param name="start">The starting index of the range.</param>
    /// <param name="length">The length of the range.</param>
    public RangeInt(int start, int length);
}
```

### Classes

#### Array2D&lt;T&gt;
Two-dimensional array wrapper (jagged `T[][]`)

```csharp
/// <summary>
/// Two-dimensional array wrapper class
/// </summary>
public sealed class Array2D<T>
{
    /// <summary>
    /// Creates an empty wrapper (call SetData before use).
    /// </summary>
    public Array2D();

    /// <summary>
    /// Wraps a jagged 2D array; infers size from row 0 and row count.
    /// </summary>
    /// <param name="data">Row-major jagged array.</param>
    public Array2D(T[][] data);

    /// <summary>
    /// Builds a jagged 2D array from a flat buffer and row width.
    /// </summary>
    /// <param name="data">Flat row-major buffer.</param>
    /// <param name="width">Cells per row.</param>
    public Array2D(T[] data, int width);

    /// <summary>
    /// Set data
    /// </summary>
    public void SetData(T[][] data);

    /// <summary>
    /// Set data
    /// </summary>
    public void SetData(T[] data, int width);

    /// <summary>
    /// Size
    /// </summary>
    public Point2Int Size { get; }

    /// <summary>
    /// Bound
    /// </summary>
    public Bounds2Int Bound { get; }

    /// <summary>
    /// Get value
    /// </summary>
    public T GetValue(int x, int y);

    /// <summary>
    /// Take the coordinates of the point whose value is nearby
    /// </summary>
    /// <param name="max">Maximum search range</param>
    public Point2Int? GetNearValue(int x, int y, T value, int max);

    /// <summary>
    /// Check value by position
    /// </summary>
    public bool CheckValue(int x, int y, T value);

    /// <summary>
    /// Get all data.
    /// </summary>
    public T[][] GetData();

    /// <summary>
    /// Get data within the boundaries
    /// </summary>
    public T[][] GetDataAtBound(Bounds2Int bound);

    /// <summary>
    /// Get data within the boundaries
    /// </summary>
    public T[][] GetDataAtBound(int xMin, int yMin, int xMax, int yMax);

    /// <summary>
    /// Formats all cells as comma-separated rows (optionally reversed Y order).
    /// </summary>
    /// <param name="reverse">If true, print from top row index downward.</param>
    public string ToPrintString(bool reverse);

    public override string ToString();
}
```

#### BitFixedData
Fixed-length bit memory. Three concepts: raw data, bit data, and business data.

- Raw data (`uint`): The real data type of the stored data
- Bit data (`bit:bool`): The bitwise data value (0/1) of the original data
- Business data (`value:uint`): A data value composed of multiple bits spliced together

```csharp
/// <summary>
/// Bit memory (fixed length)
/// </summary>
public class BitFixedData
{
    /// <summary>
    /// Number of bits per data.
    /// </summary>
    public int BitsPerValue { get; }

    /// <summary>
    /// Value length.
    /// </summary>
    public int ValueLen { get; }

    /// <summary>
    /// Valid bit length of all data. BitLen = BitsPerValue * ValueLen
    /// </summary>
    public int BitLen { get; }

    /// <summary>
    /// Raw data length.
    /// </summary>
    public int RawDataLen { get; }

    /// <summary>
    /// Raw data.
    /// </summary>
    public uint[] RawData { get; }

    /// <summary>
    /// Construct a bit memory (1 bit per value).
    /// </summary>
    public BitFixedData(int valueLen);

    /// <summary>
    /// Construct a bit memory
    /// </summary>
    /// <param name="bitsPerValue">Must be less than 32</param>
    public BitFixedData(int bitsPerValue, int valueLen);

    /// <summary>
    /// Set raw data. Lack: set 0. Excess: truncate.
    /// </summary>
    public void SetRawData(uint[] data);

    /// <summary>
    /// Get all values.
    /// </summary>
    public uint[] GetValues();

    /// <summary>
    /// Get a range of values.
    /// </summary>
    public uint[] GetValues(int valueIndex, int valueCount);

    /// <summary>
    /// Get a single value
    /// </summary>
    public uint GetValue(int valueIndex);

    /// <summary>
    /// Set a single value
    /// </summary>
    public void SetValue(int valueIndex, uint value);

    /// <summary>
    /// Take the binary string representation of all bits
    /// </summary>
    public string GetStringBits();

    /// <summary>
    /// Get all bits of data
    /// </summary>
    public bool[] GetBits();

    /// <summary>
    /// Get bit data in a range.
    /// </summary>
    public bool[] GetBits(int bitIndex, int bitCount);

    /// <summary>
    /// Get a single bit of data.
    /// </summary>
    public bool GetBit(int bitIndex);

    /// <summary>
    /// Set a single bit of data.
    /// </summary>
    public void SetBit(int bitIndex, bool isTrue);

    /// <summary>
    /// Binary string for a bit range (optional separator between value groups).
    /// </summary>
    /// <param name="bitIndex">Start bit index.</param>
    /// <param name="bitLen">Number of bits.</param>
    /// <param name="valueSpace">Separator inserted between groups.</param>
    /// <returns>Binary representation.</returns>
    public string ToBitString(int bitIndex, int bitLen, string valueSpace = "");

    public override string ToString();
}
```

### Static Utility Classes

#### Point1Utils
Extension helpers for `Point1` area indexing

```csharp
/// <summary>
/// Extension helpers for Point1 area indexing.
/// </summary>
public static class Point1Utils
{
    /// <summary>
    /// Convert to area point
    /// </summary>
    public static Point1.AreaPoint1 ToAreaPoint1(this Point1 point1, ulong size);

    /// <summary>
    /// Convert to 1D point
    /// </summary>
    public static Point1 FromAreaPoint1(this Point1.AreaPoint1 point1, ulong size);
}
```

#### Point1IntUtils
Extension helpers for `Point1Int` area indexing

```csharp
/// <summary>
/// Extension helpers for Point1Int area indexing.
/// </summary>
public static class Point1IntUtils
{
    /// <summary>
    /// Convert to area point
    /// </summary>
    public static Point1Int.AreaPoint1Int ToAreaPoint1(this Point1Int point1, uint size);

    /// <summary>
    /// Convert to 1D point
    /// </summary>
    public static Point1Int FromAreaPoint1(this Point1Int.AreaPoint1Int point1, uint size);
}
```

#### BitMark
Precomputed single-bit and multi-bit masks for primitive integer types

```csharp
/// <summary>
/// Precomputed single-bit and multi-bit masks for primitive integer types.
/// </summary>
public static class BitMark
{
    /// <summary>
    /// The number of bits corresponding to a byte data
    /// </summary>
    public const int BitsPerByte = 8;

    /// <summary>
    /// The number of bits corresponding to a ushort data
    /// </summary>
    public const int BitsPerUshort = 16;

    /// <summary>
    /// The number of bits corresponding to a int data
    /// </summary>
    public const int BitsPerInt = 32;

    /// <summary>
    /// The number of bits corresponding to a uint data
    /// </summary>
    public const int BitsPerUint = 32;

    /// <summary>
    /// The number of bits corresponding to a ulong data
    /// </summary>
    public const int BitsPerUlong = 64;

    /// <summary>
    /// Returns a byte mask covering markLen bits starting at markIndex.
    /// </summary>
    public static byte GetByteMark(int markIndex, int markLen);

    /// <summary>
    /// Returns the single-bit byte mask at markIndex.
    /// </summary>
    public static byte GetByteMark(int markIndex);

    /// <summary>
    /// Returns a ushort mask covering markLen bits starting at markIndex.
    /// </summary>
    public static ushort GetUshortMark(int markIndex, int markLen);

    /// <summary>
    /// Returns the single-bit ushort mask at markIndex.
    /// </summary>
    public static ushort GetUshortMark(int markIndex);

    /// <summary>
    /// Returns an int mask covering markLen bits starting at markIndex.
    /// </summary>
    public static int GetIntMark(int markIndex, int markLen);

    /// <summary>
    /// Returns the single-bit int mask at markIndex.
    /// </summary>
    public static int GetIntMark(int markIndex);

    /// <summary>
    /// Returns a uint mask covering markLen bits starting at markIndex.
    /// </summary>
    public static uint GetUintMark(int markIndex, int markLen);

    /// <summary>
    /// Returns the single-bit uint mask at markIndex.
    /// </summary>
    public static uint GetUintMark(int markIndex);

    /// <summary>
    /// Returns a ulong mask covering markLen bits starting at markIndex.
    /// </summary>
    public static ulong GetUlongMark(int markIndex, int markLen);

    /// <summary>
    /// Returns the single-bit ulong mask at markIndex.
    /// </summary>
    public static ulong GetUlongMark(int markIndex);
}
```

#### MathUtil
Common math helpers: clamp, parity, remainder/modulo, distance, and float comparisons

```csharp
/// <summary>
/// Common math helpers: clamp, parity, remainder/modulo, distance, and float comparisons.
/// </summary>
public static class MathUtil
{
    /// <summary>
    /// Clamps the given value between the given minimum float and maximum float values. Returns the given value if it is within the min and max range.
    /// </summary>
    public static float Clamp(float value, float min, float max);

    /// <summary>
    /// Clamps the given value between a range defined by the given minimum integer and maximum integer values. Returns the given value if it is within min and max.
    /// </summary>
    public static int Clamp(int value, int min, int max);

    /// <summary>
    /// Returns whether val is strictly between a and b (endpoints excluded; order of a/b does not matter).
    /// </summary>
    public static bool Between(double val, double a, double b);
    public static bool Between(float val, float a, float b);
    public static bool Between(int val, int a, int b);
    public static bool Between(long val, long a, long b);

    /// <summary>
    /// Clamps value between 0 and 1 and returns value.
    /// </summary>
    public static float Clamp01(float value);

    /// <summary>
    /// Is it an odd number
    /// </summary>
    public static bool IsOdd(int num);

    /// <summary>
    /// Floor to even
    /// </summary>
    public static int FloorToEven(float number);

    /// <summary>
    /// Floor to odd
    /// </summary>
    public static int FloorToOdd(float number);

    /// <summary>
    /// Ceil to even
    /// </summary>
    public static int CeilToEven(float number);

    /// <summary>
    /// Ceil to odd
    /// </summary>
    public static int CeilToOdd(float number);

    /// <summary>
    /// Returns whether two floats are approximately equal (difference less than machine epsilon).
    /// </summary>
    /// <param name="epsilon">Unused; kept for API compatibility.</param>
    public static bool IsSimilar(float a, float b, float epsilon = float.Epsilon);
    public static bool IsSimilar(double a, double b, double epsilon = double.Epsilon);

    /// <summary>
    /// Floors to int with a small positive bias from epsilon to reduce boundary errors.
    /// </summary>
    public static int FloorToInt(this float a, float epsilon = float.Epsilon);
    public static int FloorToInt(this double a, double epsilon = double.Epsilon);

    /// <summary>
    /// Ceils to int with a small negative bias from epsilon to reduce boundary errors.
    /// </summary>
    public static int CeilToInt(this float a, float epsilon = float.Epsilon);
    public static int CeilToInt(this double a, double epsilon = double.Epsilon);

    /// <summary>
    /// Rem, the result sign is the same as a
    /// </summary>
    public static int Rem(this int a, int b);
    public static double Rem(this double a, double b);

    /// <summary>
    /// Mod, the result sign is the same as b
    /// </summary>
    public static int Mod(this int a, int b);
    public static double Mod(this double a, double b);

    /// <summary>
    /// Euclidean distance between two 2D points.
    /// </summary>
    public static float Distance(float ax, float ay, float bx, float by);

    /// <summary>
    /// Squared Euclidean distance between two 2D points (avoids sqrt).
    /// </summary>
    public static float DistanceSquare(float ax, float ay, float bx, float by);
}
```

### Function Description

#### Point Struct Features

**Point1 / Point1Int**
- **Implicit conversion**: Interchangeable with `double` / `int`
- **Area points**: Split a coordinate into `AreaIndex` and `Remainder` for grid/chunk indexing
- **Extensions**: `Point1Utils` / `Point1IntUtils` provide instance helpers

**Point2Int**
- **Coordinate access**: Direct X, Y fields and indexer (0=X, 1=Y)
- **Operator overloading**: Addition, subtraction, equality
- **Type conversion**: Implicit promotion to `Point3Int` (Z = 0)
- **Performance optimization**: Operators use `MethodImpl` inlining

**Point3Int**
- **3D coordinates**: X, Y, Z and indexer (0/1/2)
- **Operator overloading**: Addition, subtraction, equality
- **Type conversion**: Implicit demotion to `Point2Int` (drops Z)

#### Bounds and Lines

**Bounds2Int**
- **Half-open rectangle**: `[XMin, XMax) × [YMin, YMax)`
- **Derived properties**: Center, size, area, empty check
- **Containment and intersection**: Point/axis tests and corner-based intersection
- **Geometry ops**: Move, crop, split, grid union/difference, scale about center

**Line1 / Line1Int**
- **Directed segments**: Keep Start/End direction; also expose Min/Max, signed length, and absolute length
- **Aligned slicing**: Split at 0, start, end, min/max, or an arbitrary basis point

**Line2Int**
- **2D integer segment**: Squared length, Euclidean length, and degenerate-point check

#### Interval Types

**Interval / IntervalInt**
- **Closed interval**: Represented by Min and Max; `IntervalInt.MaxIncluded` controls whether Length includes Max

**Range / RangeInt**
- **Half-open range**: Represented by Start and Length; End is exclusive

#### Two-dimensional Array Features

**Array2D&lt;T&gt;**
- **Wraps existing data**: Jagged `T[][]` or a flat buffer plus row width
- **Bounded access**: Out-of-range `GetValue` returns `default(T)`
- **Neighborhood search**: `GetNearValue` finds a target within a radius
- **Region slice**: `GetDataAtBound` crops by `Bounds2Int`

#### Bit Features

**BitMark**
- **Precomputed single-bit masks**: `1 << index` for byte/ushort/int/uint/ulong
- **Multi-bit overloads**: `(index, len)` form for consecutive bits

**BitFixedData**
- **Fixed packing**: Packs business values into `uint[]` using `BitsPerValue`
- **Three access layers**: Raw data, bits, and business values
- **Binary text**: `ToBitString` / `GetStringBits` emit 0/1 strings

#### Mathematics Utility Features

**MathUtil**
- **Clamping**: `Clamp`, `Clamp01`, open-interval `Between`
- **Parity rounding**: Floor/ceil to even or odd
- **Remainder vs modulo**: `Rem` follows the dividend sign; `Mod` follows the divisor
- **Distance**: 2D Euclidean distance and its square
- **Float helpers**: Approximate equality and biased Floor/Ceil to int

### Usage Examples

#### Point Operations
```csharp
var point1 = new Point2Int(10, 20);
var point2 = new Point2Int(5, 15);

Console.WriteLine($"Point 1: {point1}"); // {X=10,Y=20}
Console.WriteLine($"Point 2: {point2}"); // {X=5,Y=15}

var sum = point1 + point2;   // {X=15,Y=35}
var diff = point1 - point2;  // {X=5,Y=5}

bool isEqual = point1 == point2;     // false
bool isNotEqual = point1 != point2;  // true

int x = point1[0]; // 10
int y = point1[1]; // 20

point1.Set(30, 40);
Console.WriteLine($"After modification: {point1}"); // {X=30,Y=40}

Point3Int p3 = point1;               // Z = 0
Point2Int back = new Point3Int(1, 2, 3); // {X=1,Y=2}
```

#### 1D Points and Area Indexing
```csharp
Point1 p = 5.5;
var area = p.ToAreaPoint1(2);                 // AreaIndex=2, Remainder=1.5
Point1 restored = area.FromAreaPoint1(2);     // 5.5

Point1Int pi = -3;
var areaInt = pi.ToAreaPoint1(4);             // Negative coordinates map to the correct chunk
```

#### Bounds Operations
```csharp
var bounds = new Bounds2Int(0, 0, 100, 100);
Console.WriteLine($"Bounds: {bounds}");
// {Min={X=0,Y=0},Max={X=100,Y=100},Center={X=50,Y=50},Size={X=100,Y=100},Area=10000}

Console.WriteLine($"Center point: {bounds.Center}"); // {X=50,Y=50}
Console.WriteLine($"Size: {bounds.Size}");           // {X=100,Y=100}
Console.WriteLine($"Area: {bounds.Area}");           // 10000

var point = new Point2Int(25, 25);
bool contains = bounds.Contains(point); // true
bool containsX = bounds.ContainsX(25);  // true
bool containsY = bounds.ContainsY(25);  // true
bool onMax = bounds.Contains(100, 50);  // false (half-open; XMax excluded)

var movedBounds = bounds.Move(new Point2Int(10, 10));
var croppedBounds = bounds.Crop(new Bounds2Int(25, 25, 75, 75));

var splitBounds = bounds.SplitX(50);
Console.WriteLine($"X split: {splitBounds.Length} bounds"); // 2

var centerBounds = Bounds2Int.NewCenterBound(new Point2Int(50, 50), new Point2Int(20, 20));
// Min={X=40,Y=40}, Max={X=60,Y=60}

var scaled = bounds * 0.5f; // Scale about center; each axis ceiled
```

#### Line Slicing
```csharp
var line = new Line1(1.5, 10.0);
Line1[] parts = line.SliceAtZero(4);
// Split into length-4 segments aligned at coordinate 0

var lineInt = new Line1Int(0, 10);
Line1Int[] chunks = lineInt.SliceAtStart(3);

var seg = new Line2Int(new Point2Int(0, 0), new Point2Int(3, 4));
Console.WriteLine(seg.SqrMagnitude); // 25
Console.WriteLine(seg.Magnitude);    // 5
```

#### Intervals and Ranges
```csharp
var interval = new Interval(0f, 10f);
Console.WriteLine(interval.Length); // 10

var intervalInt = new IntervalInt(0, 10, maxIncluded: true);
Console.WriteLine(intervalInt.Length); // 11

var range = new RangeInt(2, 5);
Console.WriteLine(range.End); // 7 (exclusive)
```

#### Two-dimensional Array Operations
```csharp
int[][] grid =
{
    new[] { 0, 1, 2 },
    new[] { 3, 4, 5 },
    new[] { 6, 7, 8 },
};
var array = new Array2D<int>(grid);

int value = array.GetValue(2, 1);     // 5
bool hit = array.CheckValue(1, 1, 4); // true
int missing = array.GetValue(9, 9);   // 0 (out of range returns default)

Point2Int? near = array.GetNearValue(0, 0, 8, 4); // {X=2,Y=2}

int[][] slice = array.GetDataAtBound(new Bounds2Int(1, 1, 3, 3));
Console.WriteLine(array.ToPrintString(reverse: false));
```

#### Bit Masks and Bit Memory
```csharp
byte bit3 = BitMark.GetByteMark(3); // 0b00001000

var pack = new BitFixedData(bitsPerValue: 4, valueLen: 8);
pack.SetValue(0, 13);
uint v = pack.GetValue(0);          // 13
pack.SetBit(0, true);
bool b0 = pack.GetBit(0);
string bits = pack.GetStringBits();
```

#### Mathematical Calculations
```csharp
float distance = MathUtil.Distance(0f, 0f, 3f, 4f);       // 5
float distanceSquare = MathUtil.DistanceSquare(0f, 0f, 3f, 4f); // 25

int clamped = MathUtil.Clamp(150, 0, 100);     // 100
float clamped01 = MathUtil.Clamp01(1.5f);      // 1
bool inside = MathUtil.Between(5, 0, 10);      // true

bool odd = MathUtil.IsOdd(7);                  // true
int even = MathUtil.FloorToEven(5.9f);         // 4

int rem = (-5).Rem(3);                         // -2 (sign follows dividend)
int mod = (-5).Mod(3);                         // 1 (sign follows divisor)

int floored = 3.9f.FloorToInt();
```

#### Complex Operation Combinations
```csharp
var mapBounds = new Bounds2Int(0, 0, 1000, 1000);
var playerPos = new Point2Int(500, 500);
var viewBounds = Bounds2Int.NewCenterBound(playerPos, new Point2Int(100, 100));
var visibleArea = mapBounds.Crop(viewBounds);
var visiblePoints = visibleArea.ToArray();

float distanceToCenter = MathUtil.Distance(
    playerPos.X, playerPos.Y,
    mapBounds.Center.X, mapBounds.Center.Y);

Console.WriteLine($"Points in view: {visiblePoints.Length}");
Console.WriteLine($"Distance to map center: {distanceToCenter}");
```

### Design Features

1. **Performance optimization**: Point and bounds operators use structs and `MethodImpl` inlining
2. **Type safety**: Integer/float and closed/half-open interval types are kept separate
3. **Operator overloading**: Intuitive point arithmetic and bounds scaling
4. **Memory efficiency**: Geometry types are value types; `Array2D` wraps existing jagged arrays
5. **Complete functionality**: Points, lines, bounds, intervals, bit packing, and common math
6. **Easy to use**: Implicit conversions, extension methods, and compact constructors

### Notes

1. **Value types**: Structs are copied on pass; mutating a copy does not affect the original
2. **Half-open bounds**: Upper bounds of `Bounds2Int` and `Range`/`RangeInt` are exclusive; `Contains(XMax, y)` is false
3. **Null results**: `ToArray()`, `SliceAt` (when size is 0), and `GetDataAtBound` may return `null` for empty input
4. **Array2D out of range**: `GetValue` returns `default(T)` instead of throwing
5. **BitFixedData**: `bitsPerValue` must satisfy `0 <= bitsPerValue < 32`
6. **IsSimilar**: The `epsilon` parameter is unused; comparison always uses machine epsilon
7. **Memory usage**: `Bounds2Int.ToArray()` / `Add` / `Sub` may allocate large arrays
