# Algs API Documentation

## Namespace: JLGames.Infra.AStar

This module provides 2D/3D grid A* pathfinding, plus supporting types for directions, coordinates, and priority queues.

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
    /// Initialize map size (2D, depth defaults to 1).
    /// </summary>
    /// <param name="width">Map width</param>
    /// <param name="height">Map height</param>
    void InitMapSize(int width, int height);

    /// <summary>
    /// Directions that allows retrieval
    /// </summary>
    /// <returns>Allowed direction indices</returns>
    int[] AllowdDirections { get; }

    /// <summary>
    /// Set the allowed directions to retrieve, this can be used for special maps.
    /// </summary>
    /// <param name="direction">Direction indices</param>
    void SetAllowedDirections(int[] direction);

    /// <summary>
    /// Set custom heuristic function (distance estimate from node to goal).
    /// </summary>
    /// <param name="hn">Heuristic delegate</param>
    void SetCustomFuncHn(AStarDelegates.FuncHn hn);

    /// <summary>
    /// Set custom step-cost function (cost along a direction).
    /// </summary>
    /// <param name="dn">Step-cost delegate</param>
    void SetCustomFuncDn(AStarDelegates.FuncDn dn);

    /// <summary>
    /// Set map data from a flat array.
    /// </summary>
    /// <param name="data">Length = width × height × depth</param>
    /// <returns>Copied map [depth][height][width], or null if invalid</returns>
    int[][][] SetData(int[] data);

    /// <summary>
    /// Set map data.
    /// Note: depth=1
    /// </summary>
    /// <param name="data">Length = width × height (depth = 1)</param>
    /// <returns>Copied map [depth][height][width], or null if invalid</returns>
    int[][][] SetData(int[][] data);

    /// <summary>
    /// Set map data from a 3D array.
    /// </summary>
    /// <param name="data">Layout [depth][height][width], dimensions must match init</param>
    /// <returns>Copied map, or null if invalid</returns>
    int[][][] SetData(int[][][] data);

    /// <summary>
    /// 2D pathfinding.
    /// </summary>
    /// <param name="sx">Start point X</param>
    /// <param name="sy">Start point Y</param>
    /// <param name="ex">End point X</param>
    /// <param name="ey">End point Y</param>
    /// <returns>Path from start to end, or null if no path</returns>
    Position[] Search(int sx, int sy, int ex, int ey);

    /// <summary>
    /// 3D pathfinding.
    /// </summary>
    /// <param name="sx">Start point X</param>
    /// <param name="sy">Start point Y</param>
    /// <param name="sz">Start point Z</param>
    /// <param name="ex">End point X</param>
    /// <param name="ey">End point Y</param>
    /// <param name="ez">End point Z</param>
    /// <returns>Path from start to end, or null if no path</returns>
    Position[] Search(int sx, int sy, int sz, int ex, int ey, int ez);

    /// <summary>
    /// Pathfinding between two positions.
    /// </summary>
    /// <param name="startPos">Start point</param>
    /// <param name="endPos">End point</param>
    /// <returns>Path from start to end, or null if no path</returns>
    Position[] SearchPosition(Position startPos, Position endPos);
}
```

#### IAStarGridMap
Grid-based A* map facade: manages map data, cell size, and high-level path queries

```csharp
/// <summary>
/// Grid-based A* map facade: manages map data, cell size, and high-level path queries.
/// </summary>
public interface IAStarGridMap
{
    /// <summary>
    /// Initialize map with logical data size; grid size defaults to 1×1×1.
    /// </summary>
    /// <param name="dataSize">Cell count in each dimension</param>
    void InitGridMap(Size dataSize);

    /// <summary>
    /// Initialize map with logical data size and per-cell world size.
    /// </summary>
    /// <param name="dataSize">Cell count in each dimension</param>
    /// <param name="gridSize">World size of one cell</param>
    void InitGridMap(Size dataSize, Size gridSize);

    /// <summary>
    /// Set allowed expansion directions for pathfinding.
    /// </summary>
    /// <param name="direction">Direction indices</param>
    void SetAllowedDirections(int[] direction);

    /// <summary>
    /// Set map data from a flat array.
    /// </summary>
    /// <param name="data">Length must equal width × height × depth</param>
    /// <returns>Null on success, or an exception describing the error</returns>
    Exception SetMapData(int[] data);

    /// <summary>
    /// Set map data from a 2D array (depth = 1).
    /// </summary>
    /// <param name="data">Length must equal width × height</param>
    /// <returns>Null on success, or an exception describing the error</returns>
    Exception SetMapData(int[][] data);

    /// <summary>
    /// Set map data from a 3D array [depth][height][width].
    /// </summary>
    /// <param name="data">Dimensions must match initialization</param>
    /// <returns>Null on success, or an exception describing the error</returns>
    Exception SetMapData(int[][][] data);

    /// <summary>
    /// Set custom step-cost and heuristic functions (either may be null to keep default).
    /// </summary>
    /// <param name="dn">Step-cost delegate</param>
    /// <param name="hn">Heuristic delegate</param>
    void SetCustomFunc(AStarDelegates.FuncDn dn, AStarDelegates.FuncHn hn);

    /// <summary>
    /// Get world size of one grid cell.
    /// </summary>
    /// <returns>Grid size</returns>
    Size GetGridSize();

    /// <summary>
    /// Get logical map size in cells.
    /// </summary>
    /// <returns>Data size</returns>
    Size GetDataSize();

    /// <summary>
    /// Get total map size in world units (grid size × data size).
    /// </summary>
    /// <returns>Pixel/world size</returns>
    Size GetPixelSize();

    /// <summary>
    /// Get the underlying A* algorithm instance.
    /// </summary>
    /// <returns>A* algorithm</returns>
    IAStarAlg GetAStartAlg();

    /// <summary>
    /// Get the cell value at the given position.
    /// </summary>
    /// <param name="pos">Grid position</param>
    /// <returns>Cell value, or GridOut if out of bounds</returns>
    int GetDataValue(Position pos);

    /// <summary>
    /// Check whether every point on the path is walkable.
    /// </summary>
    /// <param name="path">Path to validate</param>
    /// <returns>True if all cells are walkable</returns>
    bool CheckPath(Position[] path);

    /// <summary>
    /// Check whether start and end can be reached in a straight line without obstacles.
    /// </summary>
    /// <param name="startPos">Start position</param>
    /// <param name="endPos">End position</param>
    /// <returns>True if a clear straight path exists</returns>
    bool CanLineTo(Position startPos, Position endPos);

    /// <summary>
    /// Find a path between two positions; optionally removes collinear intermediate points.
    /// </summary>
    /// <param name="startPos">Start position</param>
    /// <param name="endPos">End position</param>
    /// <param name="keepTurningPoint">If true, keep all turning points; if false, simplify collinear segments</param>
    /// <returns>Path array, or null if unreachable</returns>
    Position[] SearchPath(Position startPos, Position endPos, bool keepTurningPoint = false);
}
```

### Structs

#### Position
Grid position (X, Y, Z)

```csharp
/// <summary>
/// Grid position (X, Y, Z).
/// </summary>
public struct Position : IEquatable<Position>
{
    /// <summary>Grid X coordinate</summary>
    public int X, Y, Z;

    /// <summary>
    /// Add a direction offset to this position.
    /// </summary>
    /// <param name="vector">Direction offset</param>
    /// <returns>New position</returns>
    public Position AddVector(DirectionVector vector);

    public override string ToString();
    public override bool Equals(object obj);
    public override int GetHashCode();
    public bool Equals(Position other);

    /// <summary>Equality operator</summary>
    public static bool operator !=(Position b, Position c);

    /// <summary>Equality operator</summary>
    public static bool operator ==(Position b, Position c);

    /// <summary>Default empty position (0, 0, 0)</summary>
    public static readonly Position Empty;
}
```

#### PriorityPosition
Grid position with A* priority (f-score)

```csharp
/// <summary>
/// Grid position with A* priority (f-score).
/// </summary>
public struct PriorityPosition
{
    /// <summary>Grid X coordinate</summary>
    public int X, Y, Z;

    /// <summary>Open-list priority (typically f = g + h)</summary>
    public int Priority;

    /// <summary>
    /// Compare coordinates and priority with another entry.
    /// </summary>
    /// <param name="pos">Other entry</param>
    /// <returns>True if equal</returns>
    public bool EqualTo(PriorityPosition pos);

    /// <summary>
    /// Add a direction offset and return a plain position.
    /// </summary>
    /// <param name="vector">Direction offset</param>
    /// <returns>New position</returns>
    public Position AddVector(DirectionVector vector);

    public override string ToString();

    /// <summary>Default empty entry</summary>
    public static readonly PriorityPosition Empty;
}
```

#### Size
Width, height, and depth of a grid or map region

```csharp
/// <summary>
/// Width, height, and depth of a grid or map region.
/// </summary>
public struct Size
{
    /// <summary>Width (X extent)</summary>
    public int Width, Height, Depth;

    /// <summary>Total cell count: Width × Height × Depth</summary>
    public int Area { get; }

    /// <summary>True if any dimension is zero</summary>
    public bool Empty { get; }
}
```

There is no public constructor. Assign fields with an object initializer, e.g. `new Size { Width = 100, Height = 100, Depth = 1 }`. For 2D maps, `Depth` must still be 1; otherwise `Empty` is true.

#### DirectionValue
Direction offset value

```csharp
/// <summary>
/// Direction offset value
/// </summary>
public struct DirectionValue : IEquatable<DirectionValue>
{
    /// <summary>Offset on X</summary>
    public int OffsetX, OffsetY, OffsetZ;

    /// <summary>Normalized unit direction (gcd-reduced when possible)</summary>
    public DirectionValue UnitValue { get; }

    public override string ToString();
    public override bool Equals(object obj);
    public override int GetHashCode();
    public bool Equals(DirectionValue other);

    /// <summary>
    /// Compare direction equality, including equivalent unit vectors.
    /// </summary>
    /// <param name="other">Other offset</param>
    /// <returns>True if same direction</returns>
    public bool DirectionEquals(DirectionValue other);

    /// <summary>Inequality operator</summary>
    public static bool operator !=(DirectionValue b, DirectionValue c);

    /// <summary>Equality operator</summary>
    public static bool operator ==(DirectionValue b, DirectionValue c);

    /// <summary>Add offsets</summary>
    public static DirectionValue operator +(DirectionValue b, DirectionValue c);

    /// <summary>Subtract offsets</summary>
    public static DirectionValue operator -(DirectionValue b, DirectionValue c);

    /// <summary>Scale offset</summary>
    public static DirectionValue operator *(DirectionValue b, int scale);
}
```

#### DirectionVector
3D orientation with weights

```csharp
/// <summary>
/// 3D Orientation with Weights
/// </summary>
public struct DirectionVector : IEquatable<DirectionVector>
{
    /// <summary>Base direction enum</summary>
    public Direction3D Direction;

    /// <summary>Step length multiplier</summary>
    public int Len;

    /// <summary>Movement cost for one step in this direction</summary>
    public int Vector;

    /// <summary>Unit offset for this direction</summary>
    public DirectionValue Value { get; }

    /// <summary>Offset scaled by Len</summary>
    public DirectionValue RealValue { get; }

    /// <summary>X offset per step</summary>
    public int OffsetX { get; }

    /// <summary>Y offset per step</summary>
    public int OffsetY { get; }

    /// <summary>Z offset per step</summary>
    public int OffsetZ { get; }

    /// <summary>Step cost (alias of Vector)</summary>
    public int OffsetV { get; }

    public override string ToString();
    public override bool Equals(object obj);
    public override int GetHashCode();
    public bool Equals(DirectionVector other);

    public static bool operator !=(DirectionVector b, DirectionVector c);
    public static bool operator ==(DirectionVector b, DirectionVector c);
}
```

#### QueueResult
Result of dequeuing from a position queue

```csharp
/// <summary>
/// Result of dequeuing from a position queue.
/// </summary>
public struct QueueResult
{
    /// <summary>Dequeued position</summary>
    public Position Position;

    /// <summary>Whether the operation succeeded</summary>
    public bool Ok;

    public override string ToString();

    /// <summary>Error sentinel when the queue is empty</summary>
    public static readonly QueueResult Error;
}
```

#### PriorityPositionQueueResult
Result of dequeuing from a priority position queue

```csharp
/// <summary>
/// Result of dequeuing from a priority position queue.
/// </summary>
public struct PriorityPositionQueueResult
{
    /// <summary>Dequeued entry</summary>
    public PriorityPosition Position;

    /// <summary>Whether the operation succeeded</summary>
    public bool Ok;

    public override string ToString();

    /// <summary>Error sentinel when the queue is empty</summary>
    public static readonly PriorityPositionQueueResult Error;
}
```

### Classes

#### AStarAlg
A* pathfinding algorithm implementation

```csharp
/// <summary>
/// A* pathfinding algorithm implementation.
/// </summary>
public class AStarAlg : IAStarAlg
{
    public void InitMapSize(int width, int height, int depth);
    public void InitMapSize(int width, int height);

    public int[][][] SetData(int[] data);
    public int[][][] SetData(int[][] data);
    public int[][][] SetData(int[][][] data);

    /// <summary>Directions allowed for path expansion</summary>
    public int[] AllowdDirections { get; }

    public void SetAllowedDirections(int[] direction);
    public void SetCustomFuncHn(AStarDelegates.FuncHn hn);
    public void SetCustomFuncDn(AStarDelegates.FuncDn dn);

    public Position[] Search(int sx, int sy, int ex, int ey);
    public Position[] Search(int sx, int sy, int sz, int ex, int ey, int ez);

    /// <summary>
    /// Pathfinding between two positions. Reuses a cached sub-path when the request lies on the last successful path.
    /// </summary>
    public Position[] SearchPosition(Position startPos, Position endPos);
}
```

The default heuristic is Manhattan distance. Call `SetAllowedDirections` before searching; otherwise the internal direction array is null. Cell values follow `AstarConst`: `GridObstacle` and `GridOut` are blocked.

#### AStarGridMap
Grid-based A* map implementation

```csharp
/// <summary>
/// Grid-based A* map implementation.
/// </summary>
public class AStarGridMap : IAStarGridMap
{
    public void InitGridMap(Size dataSize);
    public void InitGridMap(Size dataSize, Size gridSize);

    public Exception SetMapData(int[] data);
    public Exception SetMapData(int[][] data);
    public Exception SetMapData(int[][][] data);

    public void SetAllowedDirections(int[] direction);
    public void SetCustomFunc(AStarDelegates.FuncDn dn, AStarDelegates.FuncHn hn);

    public Size GetGridSize();
    public Size GetDataSize();
    public Size GetPixelSize();
    public IAStarAlg GetAStartAlg();
    public int GetDataValue(Position pos);
    public bool CheckPath(Position[] path);
    public bool CanLineTo(Position startPos, Position endPos);
    public Position[] SearchPath(Position startPos, Position endPos, bool keepTurningPoint = false);
}
```

`InitGridMap` throws `"GridMap Empty! "` when `dataSize` or `gridSize` is Empty. `SetMapData` returns `new Exception("Data Error!")` on failure. `SearchPath` short-circuits when start equals end or a straight line is clear, and may call `AStarUtil.ClearRedundancies` when turning points are not kept.

#### AstarConst
A* search constants and default cell values

```csharp
/// <summary>
/// A* search constants and default cell values.
/// </summary>
public class AstarConst
{
    /// <summary>Mask increment per search pass (avoids full map reset)</summary>
    public static readonly int DEFAULT_MASK_ADD = 1048576;

    /// <summary>Upper bound for accumulated mask; triggers map reset when exceeded</summary>
    public static readonly int MAX_MASK = 2000000000;

    /// <summary>Out of map</summary>
    public static readonly int GridOut = -1;

    /// <summary>None</summary>
    public static readonly int GridNone = 0;

    /// <summary>Walkable path cell</summary>
    public static readonly int GridPath = 1;

    /// <summary>Obstacle</summary>
    public static readonly int GridObstacle = 2;
}
```

Grid maps treat any cell that is neither `GridOut` nor `GridObstacle` as walkable (including `GridNone` and `GridPath`).

#### DirectionGroup
Direction group

```csharp
/// <summary>
/// Direction group
/// </summary>
public class DirectionGroup
{
    /// <summary>Allowed direction indices</summary>
    public int[] Directions { get; }

    /// <summary>
    /// Set allowed directions from 2D enums.
    /// </summary>
    /// <param name="directions">2D directions</param>
    public void SetDirrections(Direction2D[] directions);

    /// <summary>
    /// Set allowed directions from 3D enums.
    /// </summary>
    /// <param name="directions">3D directions</param>
    public void SetDirrections(Direction3D[] directions);
}
```

#### PositionQueue
FIFO queue of grid positions

```csharp
/// <summary>
/// FIFO queue of grid positions.
/// </summary>
public class PositionQueue : Queue<Position>
{
    /// <summary>
    /// Enqueue a position.
    /// </summary>
    /// <param name="pos">Position</param>
    public void Push(Position pos);

    /// <summary>
    /// Dequeue the front element.
    /// </summary>
    /// <param name="pos">Unused; kept for API compatibility</param>
    /// <returns>Dequeue result</returns>
    public QueueResult Shift(Position pos);
}
```

Also inherits public members of `Queue<Position>`.

#### PriorityPositionQueue
Open-list queue sorted by ascending priority (lower f-score first)

```csharp
/// <summary>
/// Open-list queue sorted by ascending priority (lower f-score first).
/// </summary>
public class PriorityPositionQueue : List<PriorityPosition>
{
    /// <summary>
    /// Copy all entries to a new array.
    /// </summary>
    /// <returns>Snapshot of the queue</returns>
    public PriorityPosition[] GetAll();

    /// <summary>
    /// Insert by ascending priority (selection-style insert).
    /// </summary>
    /// <param name="ppos">Entry to insert</param>
    public void PushPriorityPosition(PriorityPosition ppos);

    /// <summary>
    /// Enqueue a 3D position with priority.
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    /// <param name="priority">Priority (f-score)</param>
    public void Push(int x, int y, int z, int priority);

    /// <summary>
    /// Enqueue a 2D position with priority (Z = 0).
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="priority">Priority (f-score)</param>
    public void Push(int x, int y, int priority);

    /// <summary>
    /// Remove and return the tail element (lowest priority in this ordering).
    /// </summary>
    /// <returns>Dequeue result</returns>
    public PriorityPositionQueueResult Pop();

    /// <summary>
    /// Remove and return the head element (highest priority / lowest f-score).
    /// </summary>
    /// <returns>Dequeue result</returns>
    public PriorityPositionQueueResult Shift();

    /// <summary>Current queue length</summary>
    public int Len { get; }
}
```

Also inherits public members of `List<PriorityPosition>`. The A* open list uses `Shift()` to pop the lowest f-score node.

### Static Classes

#### Positions
Factory and formatting helpers for positions

```csharp
/// <summary>
/// Factory and formatting helpers for positions.
/// </summary>
public static class Positions
{
    /// <summary>
    /// Create a 2D position (Z = 0).
    /// </summary>
    public static Position NewPosition(int x, int y);

    /// <summary>
    /// Create a 3D position.
    /// </summary>
    public static Position NewPosition(int x, int y, int z);

    /// <summary>
    /// Create a 2D priority position (Z = 0).
    /// </summary>
    public static PriorityPosition NewPriorityPosition(int x, int y, int p);

    /// <summary>
    /// Create a 3D priority position.
    /// </summary>
    public static PriorityPosition NewPriorityPosition(int x, int y, int z, int p);

    /// <summary>
    /// Format an array of positions as a path string.
    /// </summary>
    /// <typeparam name="T">Position-like type with ToString</typeparam>
    /// <param name="positions">Path points</param>
    /// <returns>Formatted string</returns>
    public static string ToString<T>(T[] positions);
}
```

#### AStarUtil
Helpers for direction tests and path simplification

```csharp
public static class AStarUtil
{
    /// <summary>
    /// Judging the direction, the premise is that the two points are the line direction
    /// </summary>
    /// <param name="startPos">Start position</param>
    /// <param name="endPos">End position</param>
    /// <returns>3D direction enum</returns>
    public static Direction3D GetDirection3D(Position startPos, Position endPos);

    /// <summary>
    /// Judging the direction, the premise is that the two points are the line direction
    /// Use the Cartesian coordinate system
    /// </summary>
    /// <param name="sourcePos">Source position</param>
    /// <param name="targetPos">Target position</param>
    /// <returns>2D direction enum</returns>
    public static Direction2D GetDirection2D(Position sourcePos, Position targetPos);

    /// <summary>
    /// Clear redundant points, keep inflection points
    /// </summary>
    /// <param name="path">Original path</param>
    /// <param name="allowDirection">Allowed walk directions; only collinear points along these directions are removed</param>
    /// <returns>Simplified path</returns>
    public static Position[] ClearRedundancies(Position[] path, int[] allowDirection);

    /// <summary>
    /// Clear redundant points, keep inflection points
    /// </summary>
    /// <param name="path">Original path</param>
    /// <returns>Simplified path</returns>
    public static Position[] ClearRedundancies(Position[] path);

    /// <summary>
    /// Determine whether three points are in a line
    /// </summary>
    /// <param name="first">First point</param>
    /// <param name="second">Middle point</param>
    /// <param name="third">Third point</param>
    /// <returns>True if collinear</returns>
    public static bool IsInLine(Position first, Position second, Position third);

    /// <summary>
    /// Whether the standard line direction
    /// </summary>
    /// <param name="pos1">First position</param>
    /// <param name="pos2">Second position</param>
    /// <param name="includeOblique">Whether diagonal/3D oblique lines count</param>
    /// <returns>True if on axis-aligned or (when allowed) oblique line</returns>
    public static bool IsInStandardLine(Position pos1, Position pos2, bool includeOblique);
}
```

#### DirectionsStatic
Some default common direction combinations

```csharp
/// <summary>
/// Some default common direction combinations
/// </summary>
public static class DirectionsStatic
{
    /// <summary>2D 8 directions</summary>
    public static readonly int[] DefaultDirections2D;

    /// <summary>3D 26 directions</summary>
    public static readonly int[] DefaultDirections3D;

    /// <summary>2D oblique 4 directions</summary>
    public static readonly int[] ObliqueDirections2D;

    /// <summary>3D oblique 14 directions</summary>
    public static readonly int[] ObliqueDirections3D;

    /// <summary>Base direction offset value</summary>
    public static readonly DirectionValue[] BasicDirectionValue;

    /// <summary>Base weighted direction offset value</summary>
    public static readonly DirectionVector[] BasicDirectionVector;

    /// <summary>Center / no movement</summary>
    public static DirectionVector VectorCenter { get; }
    /// <summary>North (+Y)</summary>
    public static DirectionVector VectorNorth { get; }
    public static DirectionVector VectorEastNorth { get; }
    public static DirectionVector VectorEast { get; }
    public static DirectionVector VectorEastSouth { get; }
    public static DirectionVector VectorSouth { get; }
    public static DirectionVector VectorWestSouth { get; }
    public static DirectionVector VectorWest { get; }
    public static DirectionVector VectorWestNorth { get; }

    public static DirectionVector VectorUp { get; }
    public static DirectionVector VectorRightUp { get; }
    public static DirectionVector VectorRight { get; }
    public static DirectionVector VectorRightDown { get; }
    public static DirectionVector VectorDown { get; }
    public static DirectionVector VectorLeftDown { get; }
    public static DirectionVector VectorLeft { get; }
    public static DirectionVector VectorLeftUp { get; }

    /// <summary>
    /// Find Direction Based on Direction Offset Value
    /// </summary>
    /// <param name="dValue">Direction offset</param>
    /// <returns>Matching direction, or Direction3D.None</returns>
    public static Direction3D GetDirectionByValue(DirectionValue dValue);

    /// <summary>
    /// Get weighted direction vector by index.
    /// </summary>
    public static DirectionVector GetVector(int direction);

    /// <summary>
    /// Get weighted direction vector for a 2D direction.
    /// </summary>
    public static DirectionVector GetVector(Direction2D direction);

    /// <summary>
    /// Get weighted direction vector for a 3D direction.
    /// </summary>
    public static DirectionVector GetVector(Direction3D direction);
}
```

Each entry in `BasicDirectionVector` uses a default step cost `Vector` of 5. The 2D aliases (`VectorUp`, etc.) map onto the north/east/south/west vectors.

### Delegates

#### AStarDelegates
Delegate definition

```csharp
/// <summary>
/// Delegate definition
/// </summary>
public static class AStarDelegates
{
    /// <summary>
    /// Calculation of movement cost between two points
    /// </summary>
    /// <param name="cx">Current X</param>
    /// <param name="cy">Current Y</param>
    /// <param name="cz">Current Z</param>
    /// <param name="ex">Goal X</param>
    /// <param name="ey">Goal Y</param>
    /// <param name="ez">Goal Z</param>
    /// <returns>Estimated cost to goal (h)</returns>
    public delegate int FuncHn(int cx, int cy, int cz, int ex, int ey, int ez);

    /// <summary>
    /// Calculation of movement cost with direction
    /// </summary>
    /// <param name="dirX">Direction offset on X (-1, 0, or 1)</param>
    /// <param name="dirY">Direction offset on Y</param>
    /// <param name="dirZ">Direction offset on Z</param>
    /// <returns>Step cost for this direction</returns>
    public delegate int FuncDn(int dirX, int dirY, int dirZ);
}
```

### Enums

#### Direction3D
3D Direction

```csharp
/// <summary>
/// 3D Direction
/// </summary>
public enum Direction3D
{
    X0_Y0_Z0 = 0, // Horizontal: center
    X0_Y1_Z0,     // Horizontal: north / +Y
    X1_Y1_Z0,     // Horizontal: northeast
    X1_Y0_Z0,     // Horizontal: east / +X
    X1_Y__Z0,     // Horizontal: southeast
    X0_Y__Z0,     // Horizontal: south / -Y
    X__Y__Z0,     // Horizontal: southwest
    X__Y0_Z0,     // Horizontal: west / -X
    X__Y1_Z0,     // Horizontal: northwest
    X0_Y0_Z1,     // +Z: center
    X0_Y1_Z1,     // +Z: north
    X1_Y1_Z1,     // +Z: northeast
    X1_Y0_Z1,     // +Z: east
    X1_Y__Z1,     // +Z: southeast
    X0_Y__Z1,     // +Z: south
    X__Y__Z1,     // +Z: southwest
    X__Y0_Z1,     // +Z: west
    X__Y1_Z1,     // +Z: northwest
    X0_Y0_Z_,     // -Z: center
    X0_Y1_Z_,     // -Z: north
    X1_Y1_Z_,     // -Z: northeast
    X1_Y0_Z_,     // -Z: east
    X1_Y__Z_,     // -Z: southeast
    X0_Y__Z_,     // -Z: south
    X__Y__Z_,     // -Z: southwest
    X__Y0_Z_,     // -Z: west
    X__Y1_Z_,     // -Z: northwest
    None
}
```

`__` in a name means that axis offset is -1. Enum values are used as direction indices for `SetAllowedDirections`.

#### Direction2D
2D Direction. Note: in 2D view coordinates, Y+1 is downward and corresponds to Up.

```csharp
/// <summary>
/// 2D Direction
/// Note: in 2D view coordinates, Y+1 is downward, corresponding to Up
/// </summary>
public enum Direction2D
{
    Center = Direction3D.X0_Y0_Z0,

    North = Direction3D.X0_Y1_Z0,
    EastNorth = Direction3D.X1_Y1_Z0,
    East = Direction3D.X1_Y0_Z0,
    EastSouth = Direction3D.X1_Y__Z0,
    South = Direction3D.X0_Y__Z0,
    WestSouth = Direction3D.X__Y__Z0,
    West = Direction3D.X__Y0_Z0,
    WestNorth = Direction3D.X__Y1_Z0,

    Up = Direction3D.X0_Y1_Z0,
    RightUp = Direction3D.X1_Y1_Z0,
    Right = Direction3D.X1_Y0_Z0,
    RightDown = Direction3D.X1_Y__Z0,
    Down = Direction3D.X0_Y__Z0,
    LeftDown = Direction3D.X__Y__Z0,
    Left = Direction3D.X__Y0_Z0,
    LeftUp = Direction3D.X__Y1_Z0
}
```

### Function Description

#### A* Algorithm Features

**Core Features**
- **Heuristic search**: Manhattan distance by default; replace with `SetCustomFuncHn`
- **Step cost**: Uses the direction vector `OffsetV` by default; replace with `SetCustomFuncDn`
- **Multi-dimensional support**: 2D and 3D overloads of `InitMapSize` / `Search`
- **Direction control**: Restrict expansion with a direction-index array (see `DirectionsStatic`)
- **Path cache**: `SearchPosition` returns a sub-path when both points lie on the last successful path
- **Search mask**: Increments a mask to avoid a full map reset; resets the search map when the mask exceeds `AstarConst.MAX_MASK`

**Cell values**
- `AstarConst.GridOut` (-1): out of map
- `AstarConst.GridNone` (0): none
- `AstarConst.GridPath` (1): walkable
- `AstarConst.GridObstacle` (2): obstacle
- Search skips `GridObstacle` and `GridOut`

#### Grid Map Features

**Map Management**
- Logical size (cell count) is separate from per-cell world size
- Supports 1D, 2D, and 3D map data
- `SetMapData` reports data errors by returning an `Exception` (or null), rather than throwing
- Access the underlying `IAStarAlg` via `GetAStartAlg()`

**Functional Features**
1. **Direct connection detection**: `CanLineTo` checks a clear standard-line path
2. **Path optimization**: `SearchPath(..., keepTurningPoint: false)` drops collinear intermediate points
3. **Data query**: `GetDataValue` returns `GridOut` when out of bounds
4. **Path validation**: `CheckPath` verifies every cell is walkable

### Usage Examples

#### Basic A* Pathfinding
```csharp
var aStarAlg = new AStarAlg();

aStarAlg.InitMapSize(100, 100);

int[] mapData = new int[100 * 100];
for (int i = 0; i < 100; i++)
{
    mapData[i * 100 + 50] = AstarConst.GridObstacle; // Middle column obstacle
}
aStarAlg.SetData(mapData);

aStarAlg.SetAllowedDirections(DirectionsStatic.DefaultDirections2D);

Position[] path = aStarAlg.Search(0, 0, 99, 99);
if (path == null)
{
    Console.WriteLine("No path");
    return;
}

Console.WriteLine($"Path length: {path.Length}");
Console.WriteLine(Positions.ToString(path));
```

#### Grid Map Pathfinding
```csharp
var gridMap = new AStarGridMap();

gridMap.InitGridMap(
    new Size { Width = 100, Height = 100, Depth = 1 },
    new Size { Width = 10, Height = 10, Depth = 1 });

int[][] mapData = new int[100][];
for (int i = 0; i < 100; i++)
{
    mapData[i] = new int[100];
    for (int j = 0; j < 100; j++)
    {
        mapData[i][j] = (i + j) % 2 == 0 ? AstarConst.GridPath : AstarConst.GridObstacle;
    }
}
Exception err = gridMap.SetMapData(mapData);
if (err != null)
{
    Console.WriteLine(err.Message); // "Data Error!"
    return;
}

gridMap.SetAllowedDirections(DirectionsStatic.DefaultDirections2D);

var startPos = Positions.NewPosition(0, 0);
var endPos = Positions.NewPosition(99, 99);

Position[] path = gridMap.SearchPath(startPos, endPos);
bool isValid = gridMap.CheckPath(path);
Console.WriteLine($"Path valid: {isValid}");

bool canLineTo = gridMap.CanLineTo(startPos, endPos);
Console.WriteLine($"Can connect directly: {canLineTo}");
```

#### Custom Evaluation Functions
```csharp
var aStarAlg = new AStarAlg();
aStarAlg.InitMapSize(20, 20);
aStarAlg.SetAllowedDirections(DirectionsStatic.DefaultDirections2D);

aStarAlg.SetCustomFuncHn((cx, cy, cz, ex, ey, ez) =>
{
    return Math.Abs(cx - ex) + Math.Abs(cy - ey) + Math.Abs(cz - ez);
});

aStarAlg.SetCustomFuncDn((dirX, dirY, dirZ) =>
{
    int steps = Math.Abs(dirX) + Math.Abs(dirY) + Math.Abs(dirZ);
    return steps == 0 ? 0 : (steps == 1 ? 10 : 14);
});

Position[] path = aStarAlg.SearchPosition(
    Positions.NewPosition(0, 0, 0),
    Positions.NewPosition(10, 10, 0)
);
```

#### 3D Pathfinding
```csharp
var aStarAlg = new AStarAlg();
aStarAlg.InitMapSize(50, 50, 10);

int[][][] mapData = new int[10][][];
for (int z = 0; z < 10; z++)
{
    mapData[z] = new int[50][];
    for (int y = 0; y < 50; y++)
    {
        mapData[z][y] = new int[50];
        for (int x = 0; x < 50; x++)
        {
            mapData[z][y][x] = (x + y + z) % 3 == 0
                ? AstarConst.GridObstacle
                : AstarConst.GridPath;
        }
    }
}
aStarAlg.SetData(mapData);
aStarAlg.SetAllowedDirections(DirectionsStatic.DefaultDirections3D);

Position[] path = aStarAlg.Search(0, 0, 0, 49, 49, 9);
if (path != null)
{
    Console.WriteLine($"3D path length: {path.Length}");
    foreach (var pos in path)
    {
        Console.WriteLine($"3D path point: {pos}");
    }
}
```

#### Path Optimization
```csharp
var gridMap = new AStarGridMap();
gridMap.InitGridMap(new Size { Width = 100, Height = 100, Depth = 1 });
gridMap.SetAllowedDirections(DirectionsStatic.DefaultDirections2D);
// ... Set map data ...

Position[] pathWithTurns = gridMap.SearchPath(
    Positions.NewPosition(0, 0),
    Positions.NewPosition(99, 99),
    true
);

Position[] optimizedPath = gridMap.SearchPath(
    Positions.NewPosition(0, 0),
    Positions.NewPosition(99, 99),
    false
);

Console.WriteLine($"Path with turns: {pathWithTurns?.Length}");
Console.WriteLine($"Optimized path: {optimizedPath?.Length}");

// Or simplify an existing path directly
Position[] simplified = AStarUtil.ClearRedundancies(pathWithTurns);
```

#### Direction Combinations
```csharp
var group = new DirectionGroup();
group.SetDirrections(new[]
{
    Direction2D.North, Direction2D.East, Direction2D.South, Direction2D.West
});

var aStarAlg = new AStarAlg();
aStarAlg.InitMapSize(32, 32);
aStarAlg.SetAllowedDirections(group.Directions);

DirectionVector north = DirectionsStatic.VectorNorth;
Position next = Positions.NewPosition(5, 5).AddVector(north); // [5,6,0]
```

### Design Features

1. **Algorithm optimization**: Heuristic search; mask increment avoids a full map clear
2. **Multi-dimensional support**: One API covers 2D and 3D
3. **Flexible configuration**: Custom h/d functions and allowed directions
4. **Path optimization**: Grid maps can drop collinear intermediate points
5. **Direction system**: `Direction3D` / `Direction2D` plus weighted `DirectionVector`
6. **Value-type coordinates**: `Position` and related structs suit high-frequency search

### Notes

1. **Map initialization**: Call `InitMapSize` / `InitGridMap` first; set `Size.Depth` to 1 for 2D maps
2. **Allowed directions**: Call `SetAllowedDirections` before searching, or the direction array is null
3. **Data consistency**: `SetData` / `SetMapData` length must equal width × height × depth; invalid data returns null or an exception respectively
4. **Cell values**: Use `AstarConst.GridObstacle` (2) for blocked cells; `0` and `1` are both walkable
5. **No path**: `Search` / `SearchPosition` / `SearchPath` return null when unreachable
6. **Delegate signatures**: `FuncHn` / `FuncDn` take integer coordinates and direction offsets, not `Position`
7. **Performance and memory**: Large and 3D maps use more memory; very long paths may throw `"Mask Error: gn >= maxMask"`
