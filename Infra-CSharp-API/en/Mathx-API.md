# Mathx API Documentation

## Namespace: JLGames.Infra.Mathx

### Structs

#### Point2Int
2D integer point struct

```csharp
/// <summary>
/// 2D point (Integer)
/// </summary>
public struct Point2Int : IEquatable<Point2Int>
{
    public int X;
    public int Y;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    public Point2Int(int x, int y);

    /// <summary>
    /// Set coordinate values
    /// </summary>
    /// <param name="newX">New X coordinate</param>
    /// <param name="newY">New Y coordinate</param>
    public void Set(int newX, int newY);

    /// <summary>
    /// Indexer, access X or Y coordinate through index
    /// </summary>
    /// <param name="index">Index: 0=X, 1=Y</param>
    /// <returns>Coordinate value</returns>
    public int this[int index] { get; set; }

    /// <summary>
    /// Zero vector
    /// </summary>
    public static readonly Point2Int Zero;

    /// <summary>
    /// Addition operator
    /// </summary>
    public static Point2Int operator +(Point2Int b, Point2Int c);

    /// <summary>
    /// Subtraction operator
    /// </summary>
    public static Point2Int operator -(Point2Int b, Point2Int c);

    /// <summary>
    /// Equality comparison operator
    /// </summary>
    public static bool operator ==(Point2Int b, Point2Int c);

    /// <summary>
    /// Inequality comparison operator
    /// </summary>
    public static bool operator !=(Point2Int b, Point2Int c);

    /// <summary>
    /// Implicit conversion to Point3Int
    /// </summary>
    public static implicit operator Point3Int(Point2Int v);
}
```

#### Point3Int
3D integer point struct

```csharp
/// <summary>
/// 3D point (Integer)
/// </summary>
public struct Point3Int : IEquatable<Point3Int>
{
    public int X;
    public int Y;
    public int Z;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    public Point3Int(int x, int y, int z);

    /// <summary>
    /// Set coordinate values
    /// </summary>
    /// <param name="newX">New X coordinate</param>
    /// <param name="newY">New Y coordinate</param>
    /// <param name="newZ">New Z coordinate</param>
    public void Set(int newX, int newY, int newZ);

    /// <summary>
    /// Zero vector
    /// </summary>
    public static readonly Point3Int Zero;
}
```

#### Bounds2Int
2D integer bounds struct

```csharp
/// <summary>
/// 2D bounds (Integer)
/// </summary>
public struct Bounds2Int : IEquatable<Bounds2Int>
{
    private Point2Int m_Min;
    private Point2Int m_Max;

    /// <summary>
    /// Minimum X coordinate
    /// </summary>
    public int XMin { get; set; }

    /// <summary>
    /// Minimum Y coordinate
    /// </summary>
    public int YMin { get; set; }

    /// <summary>
    /// Maximum X coordinate
    /// </summary>
    public int XMax { get; set; }

    /// <summary>
    /// Maximum Y coordinate
    /// </summary>
    public int YMax { get; set; }

    /// <summary>
    /// X direction size
    /// </summary>
    public int XSize { get; set; }

    /// <summary>
    /// Y direction size
    /// </summary>
    public int YSize { get; set; }

    /// <summary>
    /// Minimum point
    /// </summary>
    public Point2Int Min { get; set; }

    /// <summary>
    /// Maximum point
    /// </summary>
    public Point2Int Max { get; set; }

    /// <summary>
    /// X direction center point
    /// </summary>
    public int XCenter { get; }

    /// <summary>
    /// Y direction center point
    /// </summary>
    public int YCenter { get; }

    /// <summary>
    /// Center point
    /// </summary>
    public Point2Int Center { get; }

    /// <summary>
    /// Size
    /// </summary>
    public Point2Int Size { get; }

    /// <summary>
    /// Area
    /// </summary>
    public int Area { get; }

    /// <summary>
    /// Whether empty
    /// </summary>
    public bool IsNone { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="min">Minimum point</param>
    /// <param name="max">Maximum point</param>
    public Bounds2Int(Point2Int min, Point2Int max);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="xMin">Minimum X coordinate</param>
    /// <param name="yMin">Minimum Y coordinate</param>
    /// <param name="xMax">Maximum X coordinate</param>
    /// <param name="yMax">Maximum Y coordinate</param>
    public Bounds2Int(int xMin, int yMin, int xMax, int yMax);

    /// <summary>
    /// Set bounds
    /// </summary>
    /// <param name="min">Minimum point</param>
    /// <param name="max">Maximum point</param>
    public void Set(Point2Int min, Point2Int max);

    /// <summary>
    /// Check if contains specified point
    /// </summary>
    /// <param name="point">Point to check</param>
    /// <returns>Whether contains</returns>
    public bool Contains(Point2Int point);

    /// <summary>
    /// Check if contains specified coordinates
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>Whether contains</returns>
    public bool Contains(int x, int y);

    /// <summary>
    /// Check if contains specified X coordinate
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <returns>Whether contains</returns>
    public bool ContainsX(int x);

    /// <summary>
    /// Check if contains specified Y coordinate
    /// </summary>
    /// <param name="y">Y coordinate</param>
    /// <returns>Whether contains</returns>
    public bool ContainsY(int y);

    /// <summary>
    /// Check if two bounds intersect
    /// </summary>
    /// <param name="bounds2Int">Bounds to check</param>
    /// <returns>Whether intersect</returns>
    public bool Intersect(Bounds2Int bounds2Int);

    /// <summary>
    /// Convert to point array
    /// </summary>
    /// <returns>Array containing all points</returns>
    public Point2Int[] ToArray();

    /// <summary>
    /// Move bounds
    /// </summary>
    /// <param name="offset">Offset</param>
    /// <returns>Moved bounds</returns>
    public Bounds2Int Move(Point2Int offset);

    /// <summary>
    /// Crop bounds
    /// </summary>
    /// <param name="subCrop">Crop bounds</param>
    /// <returns>Cropped bounds</returns>
    public Bounds2Int Crop(Bounds2Int subCrop);

    /// <summary>
    /// Crop bounds
    /// </summary>
    /// <param name="min">Minimum point</param>
    /// <param name="max">Maximum point</param>
    /// <returns>Cropped bounds</returns>
    public Bounds2Int Crop2(Point2Int min, Point2Int max);

    /// <summary>
    /// Crop bounds
    /// </summary>
    /// <param name="minX">Minimum X coordinate</param>
    /// <param name="minY">Minimum Y coordinate</param>
    /// <param name="maxX">Maximum X coordinate</param>
    /// <param name="maxY">Maximum Y coordinate</param>
    /// <returns>Cropped bounds</returns>
    public Bounds2Int Crop2(int minX, int minY, int maxX, int maxY);

    /// <summary>
    /// X direction crop
    /// </summary>
    /// <param name="minX">Minimum X coordinate</param>
    /// <param name="maxX">Maximum X coordinate</param>
    /// <returns>Cropped bounds</returns>
    public Bounds2Int CropX(int minX, int maxX);

    /// <summary>
    /// Y direction crop
    /// </summary>
    /// <param name="minY">Minimum Y coordinate</param>
    /// <param name="maxY">Maximum Y coordinate</param>
    /// <returns>Cropped bounds</returns>
    public Bounds2Int CropY(int minY, int maxY);

    /// <summary>
    /// Split with x
    /// </summary>
    /// <param name="x">Split position</param>
    /// <returns>Split bounds array</returns>
    public Bounds2Int[] SplitX(int x);

    /// <summary>
    /// Split with y
    /// </summary>
    /// <param name="y">Split position</param>
    /// <returns>Split bounds array</returns>
    public Bounds2Int[] SplitY(int y);

    /// <summary>
    /// Create bounds with center point and size
    /// </summary>
    /// <param name="center">Center point</param>
    /// <param name="size">Size</param>
    /// <returns>New bounds</returns>
    public static Bounds2Int NewCenterBound(Point2Int center, Point2Int size);

    /// <summary>
    /// Create bounds with center point and size
    /// </summary>
    /// <param name="centerX">Center X coordinate</param>
    /// <param name="centerY">Center Y coordinate</param>
    /// <param name="sizeX">X direction size</param>
    /// <param name="sizeY">Y direction size</param>
    /// <returns>New bounds</returns>
    public static Bounds2Int NewCenterBound(int centerX, int centerY, int sizeX, int sizeY);

    /// <summary>
    /// Empty bounds
    /// </summary>
    public static readonly Bounds2Int Empty;
}
```

#### Array2D
Two-dimensional array struct

```csharp
/// <summary>
/// Two-dimensional array
/// Provides basic operations for two-dimensional arrays
/// </summary>
public struct Array2D<T>
{
    /// <summary>
    /// Width
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Height
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Array data
    /// </summary>
    public T[] Data { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="width">Width</param>
    /// <param name="height">Height</param>
    public Array2D(int width, int height);

    /// <summary>
    /// Indexer
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>Element value</returns>
    public T this[int x, int y] { get; set; }

    /// <summary>
    /// Check if coordinates are valid
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>Whether valid</returns>
    public bool IsValid(int x, int y);

    /// <summary>
    /// Get linear index
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>Linear index</returns>
    public int GetIndex(int x, int y);
}
```

#### MathUtil
Mathematics utility class

```csharp
/// <summary>
/// Mathematics utility class
/// Provides common mathematical calculation functions
/// </summary>
public static class MathUtil
{
    /// <summary>
    /// Calculate distance between two points
    /// </summary>
    /// <param name="p1">Point 1</param>
    /// <param name="p2">Point 2</param>
    /// <returns>Distance</returns>
    public static float Distance(Point2Int p1, Point2Int p2);

    /// <summary>
    /// Calculate squared distance between two points
    /// </summary>
    /// <param name="p1">Point 1</param>
    /// <param name="p2">Point 2</param>
    /// <returns>Squared distance</returns>
    public static float DistanceSquared(Point2Int p1, Point2Int p2);

    /// <summary>
    /// Calculate Manhattan distance
    /// </summary>
    /// <param name="p1">Point 1</param>
    /// <param name="p2">Point 2</param>
    /// <returns>Manhattan distance</returns>
    public static int ManhattanDistance(Point2Int p1, Point2Int p2);

    /// <summary>
    /// Calculate Chebyshev distance
    /// </summary>
    /// <param name="p1">Point 1</param>
    /// <param name="p2">Point 2</param>
    /// <returns>Chebyshev distance</returns>
    public static int ChebyshevDistance(Point2Int p1, Point2Int p2);

    /// <summary>
    /// Linear interpolation
    /// </summary>
    /// <param name="a">Start value</param>
    /// <param name="b">End value</param>
    /// <param name="t">Interpolation factor</param>
    /// <returns>Interpolation result</returns>
    public static float Lerp(float a, float b, float t);

    /// <summary>
    /// Clamp value within specified range
    /// </summary>
    /// <param name="value">Value to clamp</param>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <returns>Clamped value</returns>
    public static int Clamp(int value, int min, int max);

    /// <summary>
    /// Clamp value within specified range
    /// </summary>
    /// <param name="value">Value to clamp</param>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <returns>Clamped value</returns>
    public static float Clamp(float value, float min, float max);
}
```

### Function Description

#### Point Struct Features

**Point2Int**
- **Coordinate access**: Supports direct access to X, Y coordinates
- **Indexer**: Access coordinates through index (0=X, 1=Y)
- **Operator overloading**: Supports addition, subtraction, equality comparison
- **Type conversion**: Supports implicit conversion to Point3Int
- **Performance optimization**: Uses MethodImpl attributes for performance optimization

**Point3Int**
- **3D coordinates**: Supports X, Y, Z three coordinates
- **Extensibility**: Extended from Point2Int
- **Consistency**: Maintains same interface design as Point2Int

#### Bounds Struct Features

**Bounds2Int**
- **Bounds representation**: Represents rectangular bounds through minimum and maximum points
- **Property calculation**: Automatically calculates center point, size, area and other properties
- **Containment detection**: Supports point containment detection and bounds intersection detection
- **Bounds operations**: Supports move, crop, split and other operations
- **Point array conversion**: Can convert bounds to array containing all points

#### Two-dimensional Array Features

**Array2D<T>**
- **Generic support**: Supports two-dimensional arrays of any type
- **Linear storage**: Uses one-dimensional array to store two-dimensional data
- **Index access**: Supports [x,y] form index access
- **Bounds checking**: Provides coordinate validity checking
- **Memory efficiency**: More efficient than nested arrays

#### Mathematics Utility Features

**MathUtil**
- **Distance calculation**: Supports Euclidean, Manhattan, Chebyshev distances
- **Interpolation calculation**: Provides linear interpolation functionality
- **Value clamping**: Provides value range clamping functionality
- **Performance optimization**: Uses efficient mathematical algorithms

### Usage Examples

#### Point Operations
```csharp
// Create points
var point1 = new Point2Int(10, 20);
var point2 = new Point2Int(5, 15);

// Basic operations
Console.WriteLine($"Point 1: {point1}"); // {X=10,Y=20}
Console.WriteLine($"Point 2: {point2}"); // {X=5,Y=15}

// Arithmetic operations
var sum = point1 + point2; // {X=15,Y=35}
var diff = point1 - point2; // {X=5,Y=5}

// Comparison operations
bool isEqual = point1 == point2; // false
bool isNotEqual = point1 != point2; // true

// Index access
int x = point1[0]; // 10
int y = point1[1]; // 20

// Set values
point1.Set(30, 40);
Console.WriteLine($"After modification: {point1}"); // {X=30,Y=40}
```

#### Bounds Operations
```csharp
// Create bounds
var bounds = new Bounds2Int(0, 0, 100, 100);
Console.WriteLine($"Bounds: {bounds}"); // {Min={X=0,Y=0},Max={X=100,Y=100},Center={X=50,Y=50},Size={X=100,Y=100},Area=10000}

// Property access
Console.WriteLine($"Center point: {bounds.Center}"); // {X=50,Y=50}
Console.WriteLine($"Size: {bounds.Size}"); // {X=100,Y=100}
Console.WriteLine($"Area: {bounds.Area}"); // 10000

// Containment detection
var point = new Point2Int(25, 25);
bool contains = bounds.Contains(point); // true
bool containsX = bounds.ContainsX(25); // true
bool containsY = bounds.ContainsY(25); // true

// Bounds operations
var movedBounds = bounds.Move(new Point2Int(10, 10));
Console.WriteLine($"After move: {movedBounds}"); // {Min={X=10,Y=10},Max={X=110,Y=110},...}

var croppedBounds = bounds.Crop(new Bounds2Int(25, 25, 75, 75));
Console.WriteLine($"After crop: {croppedBounds}"); // {Min={X=25,Y=25},Max={X=75,Y=75},...}

// Split operations
var splitBounds = bounds.SplitX(50);
Console.WriteLine($"X split: {splitBounds.Length} bounds"); // 2

// Create center bounds
var centerBounds = Bounds2Int.NewCenterBound(new Point2Int(50, 50), new Point2Int(20, 20));
Console.WriteLine($"Center bounds: {centerBounds}"); // {Min={X=40,Y=40},Max={X=60,Y=60},...}
```

#### Two-dimensional Array Operations
```csharp
// Create two-dimensional array
var array = new Array2D<int>(5, 5);

// Set values
array[2, 3] = 42;
array[1, 1] = 10;

// Get values
int value = array[2, 3]; // 42

// Check coordinate validity
bool isValid = array.IsValid(2, 3); // true
bool isInvalid = array.IsValid(10, 10); // false

// Iterate through array
for (int y = 0; y < array.Height; y++)
{
    for (int x = 0; x < array.Width; x++)
    {
        if (array.IsValid(x, y))
        {
            Console.Write($"{array[x, y]} ");
        }
    }
    Console.WriteLine();
}
```

#### Mathematical Calculations
```csharp
// Distance calculations
var p1 = new Point2Int(0, 0);
var p2 = new Point2Int(3, 4);

float distance = MathUtil.Distance(p1, p2); // 5.0
float distanceSquared = MathUtil.DistanceSquared(p1, p2); // 25.0
int manhattanDistance = MathUtil.ManhattanDistance(p1, p2); // 7
int chebyshevDistance = MathUtil.ChebyshevDistance(p1, p2); // 4

// Interpolation calculations
float interpolated = MathUtil.Lerp(0.0f, 100.0f, 0.5f); // 50.0

// Value clamping
int clamped = MathUtil.Clamp(150, 0, 100); // 100
float clampedFloat = MathUtil.Clamp(75.5f, 0.0f, 100.0f); // 75.5
```

#### Complex Operation Combinations
```csharp
// Create game map bounds
var mapBounds = new Bounds2Int(0, 0, 1000, 1000);

// Create player position
var playerPos = new Point2Int(500, 500);

// Create view range
var viewBounds = Bounds2Int.NewCenterBound(playerPos, new Point2Int(100, 100));

// Check if view is within map
var visibleArea = mapBounds.Crop(viewBounds);

// Get all points in view
var visiblePoints = visibleArea.ToArray();

// Calculate distance to map center
var distanceToEdge = MathUtil.Distance(playerPos, mapBounds.Center);

Console.WriteLine($"Points in view: {visiblePoints.Length}");
Console.WriteLine($"Distance to map center: {distanceToEdge}");
```

### Design Features

1. **Performance optimization**: Uses structs and MethodImpl attributes for performance optimization
2. **Type safety**: Strong typing design, avoids type errors
3. **Operator overloading**: Supports intuitive mathematical operations
4. **Memory efficiency**: Structs avoid heap allocation, improving performance
5. **Complete functionality**: Provides common mathematical and geometric operations
6. **Easy to use**: Clean API design

### Notes

1. **Value types**: All structs are value types and will be copied when passed
2. **Bounds checking**: Check coordinate validity before use
3. **Performance considerations**: Avoid unnecessary struct copying during heavy calculations
4. **Precision issues**: Integer calculations avoid floating-point precision problems
5. **Memory usage**: ToArray() method may cause large memory allocations 