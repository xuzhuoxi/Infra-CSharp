# Algs API Documentation

## Namespace: JLGames.Infra.Algs.AStar

### Interfaces

#### IAStarAlg
A* core algorithm interface

```csharp
/// <summary>
/// A* core algorithm interface
/// </summary>
public interface IAStarAlg
{
    /// <summary>
    /// Initialize map size.
    /// </summary>
    /// <param name="width">Map width</param>
    /// <param name="height">Map height</param>
    /// <param name="depth">Map depth</param>
    void InitMapSize(int width, int height, int depth);

    /// <summary>
    /// Initialize map size.
    /// </summary>
    /// <param name="width">Map width</param>
    /// <param name="height">Map height</param>
    void InitMapSize(int width, int height);

    /// <summary>
    /// Directions that allows retrieval
    /// </summary>
    /// <returns>Allowed directions array</returns>
    int[] AllowdDirections { get; }

    /// <summary>
    /// Set the allowed directions to retrieve, this can be used for special maps.
    /// </summary>
    /// <param name="direction">Direction array</param>
    void SetAllowedDirections(int[] direction);

    /// <summary>
    /// Set custom distance estimation function
    /// </summary>
    /// <param name="hn">Distance estimation function</param>
    void SetCustomFuncHn(AStarDelegates.FuncHn hn);

    /// <summary>
    /// Set custom direction evaluation function
    /// </summary>
    /// <param name="dn">Direction evaluation function</param>
    void SetCustomFuncDn(AStarDelegates.FuncDn dn);

    /// <summary>
    /// Set map data.
    /// </summary>
    /// <param name="data">Length = map width * height * depth</param>
    /// <returns>Processed map data</returns>
    int[][][] SetData(int[] data);

    /// <summary>
    /// Set map data.
    /// Note: depth=1
    /// </summary>
    /// <param name="data">Length = map width * height</param>
    /// <returns>Processed map data</returns>
    int[][][] SetData(int[][] data);

    /// <summary>
    /// Set map data.
    /// </summary>
    /// <param name="data">int[depth][height][width] corresponding length consistent with initialization</param>
    /// <returns>Processed map data</returns>
    int[][][] SetData(int[][][] data);

    /// <summary>
    /// 2D pathfinding.
    /// </summary>
    /// <param name="sx">Start Point X</param>
    /// <param name="sy">Start Point Y</param>
    /// <param name="ex">End Point X</param>
    /// <param name="ey">End Point Y</param>
    /// <returns>Pathfinding path</returns>
    Position[] Search(int sx, int sy, int ex, int ey);

    /// <summary>
    /// 3D pathfinding.
    /// sx:StartX; sy:StartY; sz:StartZ
    /// ex:EndX; ey:EndY; ez:EndZ
    /// </summary>
    /// <param name="sx">Start Point X</param>
    /// <param name="sy">Start Point Y</param>
    /// <param name="sz">Start Point Z</param>
    /// <param name="ex">End Point X</param>
    /// <param name="ey">End Point Y</param>
    /// <param name="ez">End Point Z</param>
    /// <returns>Pathfinding path</returns>
    Position[] Search(int sx, int sy, int sz, int ex, int ey, int ez);

    /// <summary>
    /// Pathfinding
    /// </summary>
    /// <param name="startPos">Start Point</param>
    /// <param name="endPos">End Point</param>
    /// <returns>Pathfinding path</returns>
    Position[] SearchPosition(Position startPos, Position endPos);
}
```

#### IAStarGridMap
A* grid map interface

```csharp
/// <summary>
/// A* grid map interface
/// Provides pathfinding functionality for grid maps
/// </summary>
public interface IAStarGridMap
{
    /// <summary>
    /// Initialize map size
    /// </summary>
    /// <param name="dataSize">Data size</param>
    void InitGridMap(Size dataSize);

    /// <summary>
    /// Initialize map size
    /// </summary>
    /// <param name="dataSize">Data size</param>
    /// <param name="gridSize">Grid size</param>
    void InitGridMap(Size dataSize, Size gridSize);

    /// <summary>
    /// Set allowed pathfinding directions
    /// </summary>
    /// <param name="direction">Direction array</param>
    void SetAllowedDirections(int[] direction);

    /// <summary>
    /// Set map data
    /// </summary>
    /// <param name="data">One-dimensional data</param>
    /// <returns>Exception information</returns>
    Exception SetMapData(int[] data);

    /// <summary>
    /// Set map data
    /// </summary>
    /// <param name="data">Two-dimensional data</param>
    /// <returns>Exception information</returns>
    Exception SetMapData(int[][] data);

    /// <summary>
    /// Set map data
    /// </summary>
    /// <param name="data">Three-dimensional data</param>
    /// <returns>Exception information</returns>
    Exception SetMapData(int[][][] data);

    /// <summary>
    /// Set custom evaluation functions
    /// </summary>
    /// <param name="dn">Direction evaluation function</param>
    /// <param name="hn">Distance evaluation function</param>
    void SetCustomFunc(AStarDelegates.FuncDn dn, AStarDelegates.FuncHn hn);

    /// <summary>
    /// Get grid size
    /// </summary>
    /// <returns>Grid size</returns>
    Size GetGridSize();

    /// <summary>
    /// Get map data size
    /// </summary>
    /// <returns>Data size</returns>
    Size GetDataSize();

    /// <summary>
    /// Get map pixel size
    /// </summary>
    /// <returns>Pixel size</returns>
    Size GetPixelSize();

    /// <summary>
    /// 获取A*算法实例
    /// </summary>
    /// <returns>A*算法</returns>
    IAStarAlg GetAStartAlg();

    /// <summary>
    /// 获取指定位置的数据值
    /// </summary>
    /// <param name="pos">位置</param>
    /// <returns>数据值</returns>
    int GetDataValue(Position pos);

    /// <summary>
    /// Check if path is passable
    /// </summary>
    /// <param name="path">Path</param>
    /// <returns>Whether passable</returns>
    bool CheckPath(Position[] path);

    /// <summary>
    /// Check if two points can connect directly
    /// </summary>
    /// <param name="startPos">Start point</param>
    /// <param name="endPos">End point</param>
    /// <returns>Whether can connect directly</returns>
    bool CanLineTo(Position startPos, Position endPos);

    /// <summary>
    /// Search path
    /// Default clears turning points
    /// </summary>
    /// <param name="startPos">Start point</param>
    /// <param name="endPos">End point</param>
    /// <param name="keepTurningPoint">Whether to keep turning points</param>
    /// <returns>Pathfinding path</returns>
    Position[] SearchPath(Position startPos, Position endPos, bool keepTurningPoint = false);
}
```

### Structs

#### Position
Point location information struct

```csharp
/// <summary>
/// Point location information
/// </summary>
public struct Position : IEquatable<Position>
{
    public int X, Y, Z;

    /// <summary>
    /// Add direction vector
    /// </summary>
    /// <param name="vector">Direction vector</param>
    /// <returns>New position</returns>
    public Position AddVector(DirectionVector vector);

    /// <summary>
    /// String representation
    /// </summary>
    /// <returns>Position string</returns>
    public override string ToString();

    /// <summary>
    /// Equality comparison
    /// </summary>
    /// <param name="obj">Comparison object</param>
    /// <returns>Whether equal</returns>
    public override bool Equals(object obj);

    /// <summary>
    /// Hash code
    /// </summary>
    /// <returns>Hash value</returns>
    public override int GetHashCode();

    /// <summary>
    /// Equality comparison
    /// </summary>
    /// <param name="other">Comparison position</param>
    /// <returns>Whether equal</returns>
    public bool Equals(Position other);

    /// <summary>
    /// Inequality comparison operator
    /// </summary>
    public static bool operator !=(Position b, Position c);

    /// <summary>
    /// Equality comparison operator
    /// </summary>
    public static bool operator ==(Position b, Position c);

    /// <summary>
    /// Empty position
    /// </summary>
    public static readonly Position Empty;
}
```

#### PriorityPosition
Point location struct with weights

```csharp
/// <summary>
/// Point locations with weights
/// </summary>
public struct PriorityPosition
{
    public int X, Y, Z;
    public int Priority;

    /// <summary>
    /// Equality comparison
    /// </summary>
    /// <param name="pos">Comparison position</param>
    /// <returns>Whether equal</returns>
    public bool EqualTo(PriorityPosition pos);

    /// <summary>
    /// Add direction vector
    /// </summary>
    /// <param name="vector">Direction vector</param>
    /// <returns>New position</returns>
    public Position AddVector(DirectionVector vector);

    /// <summary>
    /// String representation
    /// </summary>
    /// <returns>Position string</returns>
    public override string ToString();

    /// <summary>
    /// Empty position
    /// </summary>
    public static readonly PriorityPosition Empty;
}
```

#### Size
Size struct

```csharp
/// <summary>
/// Size struct
/// Represents width, height, depth
/// </summary>
public struct Size
{
    public int Width;
    public int Height;
    public int Depth;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="width">Width</param>
    /// <param name="height">Height</param>
    public Size(int width, int height);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="width">Width</param>
    /// <param name="height">Height</param>
    /// <param name="depth">Depth</param>
    public Size(int width, int height, int depth);
}
```

#### DirectionVector
Direction vector struct

```csharp
/// <summary>
/// Direction vector
/// Represents movement direction offset
/// </summary>
public struct DirectionVector
{
    public int OffsetX;
    public int OffsetY;
    public int OffsetZ;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="offsetX">X offset</param>
    /// <param name="offsetY">Y offset</param>
    /// <param name="offsetZ">Z offset</param>
    public DirectionVector(int offsetX, int offsetY, int offsetZ);
}
```

### Classes

#### AStarAlg
A* algorithm implementation class

```csharp
/// <summary>
/// A* algorithm implementation
/// Provides complete A* pathfinding algorithm functionality
/// </summary>
public class AStarAlg : IAStarAlg
{
    // Specific implementation requires further analysis of file content
}
```

#### AStarGridMap
A* grid map implementation class

```csharp
/// <summary>
/// A* grid map implementation
/// Provides pathfinding functionality for grid maps
/// </summary>
public class AStarGridMap : IAStarGridMap
{
    // Specific implementation requires further analysis of file content
}
```

### Static Classes

#### Positions
Position utility class

```csharp
/// <summary>
/// Position utility class
/// Provides static methods for position creation and operations
/// </summary>
public static class Positions
{
    /// <summary>
    /// Create 2D position
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <returns>Position</returns>
    public static Position NewPosition(int x, int y);

    /// <summary>
    /// Create 3D position
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    /// <returns>Position</returns>
    public static Position NewPosition(int x, int y, int z);

    /// <summary>
    /// Create 2D position with weight
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="p">Weight</param>
    /// <returns>Position with weight</returns>
    public static PriorityPosition NewPriorityPosition(int x, int y, int p);

    /// <summary>
    /// Create 3D position with weight
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    /// <param name="p">Weight</param>
    /// <returns>Position with weight</returns>
    public static PriorityPosition NewPriorityPosition(int x, int y, int z, int p);

    /// <summary>
    /// Convert position array to string
    /// </summary>
    /// <typeparam name="T">Position type</typeparam>
    /// <param name="positions">Position array</param>
    /// <returns>String representation</returns>
    public static string ToString<T>(T[] positions);
}
```

### Delegates

#### AStarDelegates
A* algorithm delegate definitions

```csharp
/// <summary>
/// A* algorithm delegate definitions
/// Defines various callback functions used in A* algorithm
/// </summary>
public static class AStarDelegates
{
    /// <summary>
    /// Distance estimation function delegate
    /// Calculates estimated distance from current position to target position
    /// </summary>
    /// <param name="current">Current position</param>
    /// <param name="target">Target position</param>
    /// <returns>Estimated distance</returns>
    public delegate int FuncHn(Position current, Position target);

    /// <summary>
    /// Direction evaluation function delegate
    /// Calculates direction evaluation from current position to target position
    /// </summary>
    /// <param name="current">Current position</param>
    /// <param name="target">Target position</param>
    /// <returns>Direction evaluation</returns>
    public delegate int FuncDn(Position current, Position target);
}
```

### Enums

#### Direction
Direction enumeration

```csharp
/// <summary>
/// Direction enumeration
/// Defines directions used in pathfinding algorithms
/// </summary>
public enum Direction
{
    /// <summary>
    /// Up
    /// </summary>
    Up = 0,

    /// <summary>
    /// Down
    /// </summary>
    Down = 1,

    /// <summary>
    /// Left
    /// </summary>
    Left = 2,

    /// <summary>
    /// Right
    /// </summary>
    Right = 3,

    /// <summary>
    /// Forward
    /// </summary>
    Forward = 4,

    /// <summary>
    /// Backward
    /// </summary>
    Backward = 5
}
```

### Function Description

#### A* Algorithm Features

**Core Features**
- **Heuristic search**: Uses heuristic functions to optimize search efficiency
- **Path optimization**: Automatically finds shortest path
- **Multi-dimensional support**: Supports 2D and 3D pathfinding
- **Custom evaluation**: Supports custom distance and direction evaluation functions
- **Direction control**: Can set allowed movement directions

**Algorithm Advantages**
1. **High efficiency**: Heuristic search greatly improves efficiency
2. **Accuracy**: Guarantees finding optimal path
3. **Flexibility**: Supports various map types and movement rules
4. **Extensibility**: Easy to extend for new pathfinding requirements

#### Grid Map Features

**Map Management**
- **Multi-size support**: Supports maps of different sizes
- **Data management**: Supports one-dimensional, two-dimensional, and three-dimensional data
- **Gridification**: Discretizes continuous space into grids
- **Path validation**: Provides path validity checking

**Functional Features**
1. **Direct connection detection**: Detects whether two points can connect directly
2. **Path optimization**: Automatically removes unnecessary turning points
3. **Data query**: Fast query of data at any position
4. **Size management**: Flexible management of map and grid sizes

### Usage Examples

#### Basic A* Pathfinding
```csharp
// Create A* algorithm instance
var aStarAlg = new AStarAlg();

// Initialize map size
aStarAlg.InitMapSize(100, 100);

// Set map data (0 means passable, 1 means obstacle)
int[] mapData = new int[100 * 100];
// Set obstacles
for (int i = 0; i < 100; i++)
{
    mapData[i * 100 + 50] = 1; // Middle column obstacle
}
aStarAlg.SetData(mapData);

// Set allowed movement directions (8 directions)
aStarAlg.SetAllowedDirections(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 });

// Execute pathfinding
Position[] path = aStarAlg.Search(0, 0, 99, 99);

// Output path
Console.WriteLine($"Path length: {path.Length}");
foreach (var pos in path)
{
    Console.WriteLine($"Path point: {pos}");
}
```

#### Grid Map Pathfinding
```csharp
// Create grid map
var gridMap = new AStarGridMap();

// Initialize map
gridMap.InitGridMap(new Size(100, 100), new Size(10, 10));

// Set map data
int[][] mapData = new int[100][];
for (int i = 0; i < 100; i++)
{
    mapData[i] = new int[100];
    for (int j = 0; j < 100; j++)
    {
        mapData[i][j] = (i + j) % 2; // Checkerboard obstacles
    }
}
gridMap.SetMapData(mapData);

// Create start and end positions
var startPos = Positions.NewPosition(0, 0);
var endPos = Positions.NewPosition(99, 99);

// Execute pathfinding
Position[] path = gridMap.SearchPath(startPos, endPos);

// Check path validity
bool isValid = gridMap.CheckPath(path);
Console.WriteLine($"Path valid: {isValid}");

// Check if can connect directly
bool canLineTo = gridMap.CanLineTo(startPos, endPos);
Console.WriteLine($"Can connect directly: {canLineTo}");
```

#### Custom Evaluation Functions
```csharp
// Create A* algorithm instance
var aStarAlg = new AStarAlg();

// Set custom distance evaluation function (Manhattan distance)
aStarAlg.SetCustomFuncHn((current, target) => {
    return Math.Abs(current.X - target.X) + Math.Abs(current.Y - target.Y) + Math.Abs(current.Z - target.Z);
});

// Set custom direction evaluation function
aStarAlg.SetCustomFuncDn((current, target) => {
    // Adjust direction weight based on terrain type
    int terrainCost = GetTerrainCost(current);
    return terrainCost;
});

// Execute pathfinding
Position[] path = aStarAlg.SearchPosition(
    Positions.NewPosition(0, 0, 0),
    Positions.NewPosition(10, 10, 0)
);
```

#### 3D Pathfinding
```csharp
// Create 3D A* algorithm
var aStarAlg = new AStarAlg();

// Initialize 3D map
aStarAlg.InitMapSize(50, 50, 10);

// Set 3D map data
int[][][] mapData = new int[10][][];
for (int z = 0; z < 10; z++)
{
    mapData[z] = new int[50][];
    for (int y = 0; y < 50; y++)
    {
        mapData[z][y] = new int[50];
        for (int x = 0; x < 50; x++)
        {
            mapData[z][y][x] = (x + y + z) % 3 == 0 ? 1 : 0; // 3D obstacles
        }
    }
}
aStarAlg.SetData(mapData);

// Execute 3D pathfinding
Position[] path = aStarAlg.Search(0, 0, 0, 49, 49, 9);

Console.WriteLine($"3D path length: {path.Length}");
foreach (var pos in path)
{
    Console.WriteLine($"3D path point: {pos}");
}
```

#### Path Optimization
```csharp
// Create grid map
var gridMap = new AStarGridMap();

// Initialize and set map data
gridMap.InitGridMap(new Size(100, 100));
// ... Set map data ...

// Pathfinding (keep turning points)
Position[] pathWithTurns = gridMap.SearchPath(
    Positions.NewPosition(0, 0),
    Positions.NewPosition(99, 99),
    true
);

// Pathfinding (clear turning points)
Position[] optimizedPath = gridMap.SearchPath(
    Positions.NewPosition(0, 0),
    Positions.NewPosition(99, 99),
    false
);

Console.WriteLine($"Original path length: {pathWithTurns.Length}");
Console.WriteLine($"Optimized path length: {optimizedPath.Length}");
```

### Design Features

1. **Algorithm optimization**: Uses heuristic search to improve efficiency
2. **Multi-dimensional support**: Supports 2D and 3D pathfinding
3. **Flexible configuration**: Supports custom evaluation functions and movement directions
4. **Path optimization**: Automatically optimizes paths, reduces turning points
5. **Performance optimization**: Uses structs and MethodImpl attributes
6. **Easy to use**: Clean API design

### Notes

1. **Map initialization**: Must correctly initialize map size before use
2. **Data consistency**: Map data length must be consistent with initialization size
3. **Performance considerations**: Large map pathfinding may take longer
4. **Memory usage**: 3D maps will use more memory
5. **Evaluation functions**: Custom evaluation functions affect pathfinding efficiency and accuracy 