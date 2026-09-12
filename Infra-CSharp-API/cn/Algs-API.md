# Algs API 文档

## 命名空间: JLGames.Infra.AStar

本模块提供二维 / 三维网格 A* 寻路，以及方向、坐标、优先级队列等配套类型。

### 接口 (Interfaces)

#### IAStarAlg
A* 核心算法接口

```csharp
/// <summary>
/// A*核心算法接口
/// </summary>
public interface IAStarAlg
{
    /// <summary>
    /// 初始化地图尺寸
    /// </summary>
    /// <param name="width">地图宽度</param>
    /// <param name="height">地图高度</param>
    /// <param name="depth">地图深度</param>
    void InitMapSize(int width, int height, int depth);

    /// <summary>
    /// 初始化地图尺寸（二维，深度默认为 1）
    /// </summary>
    /// <param name="width">地图宽度</param>
    /// <param name="height">地图高度</param>
    void InitMapSize(int width, int height);

    /// <summary>
    /// 取允许检索的方向
    /// </summary>
    /// <returns>允许的方向索引数组</returns>
    int[] AllowdDirections { get; }

    /// <summary>
    /// 设置允许检索的方向(可用于特殊地图)
    /// </summary>
    /// <param name="direction">方向索引数组</param>
    void SetAllowedDirections(int[] direction);

    /// <summary>
    /// 设置自定义启发函数（节点到终点的距离估值）
    /// </summary>
    /// <param name="hn">启发函数委托</param>
    void SetCustomFuncHn(AStarDelegates.FuncHn hn);

    /// <summary>
    /// 设置自定义步进代价函数（沿某方向的移动代价）
    /// </summary>
    /// <param name="dn">步进代价委托</param>
    void SetCustomFuncDn(AStarDelegates.FuncDn dn);

    /// <summary>
    /// 以一维数组设置地图数据
    /// </summary>
    /// <param name="data">长度 = width × height × depth</param>
    /// <returns>复制后的地图 [depth][height][width]，无效时返回 null</returns>
    int[][][] SetData(int[] data);

    /// <summary>
    /// 设置地图数据
    /// 注意: depth=1
    /// </summary>
    /// <param name="data">长度 = width × height（depth = 1）</param>
    /// <returns>复制后的地图 [depth][height][width]，无效时返回 null</returns>
    int[][][] SetData(int[][] data);

    /// <summary>
    /// 以三维数组设置地图数据
    /// </summary>
    /// <param name="data">布局 [depth][height][width]，尺寸须与初始化一致</param>
    /// <returns>复制后的地图，无效时返回 null</returns>
    int[][][] SetData(int[][][] data);

    /// <summary>
    /// 二维寻路
    /// </summary>
    /// <param name="sx">起点 X</param>
    /// <param name="sy">起点 Y</param>
    /// <param name="ex">终点 X</param>
    /// <param name="ey">终点 Y</param>
    /// <returns>路径，无解时返回 null</returns>
    Position[] Search(int sx, int sy, int ex, int ey);

    /// <summary>
    /// 三维寻路
    /// </summary>
    /// <param name="sx">起点 X</param>
    /// <param name="sy">起点 Y</param>
    /// <param name="sz">起点 Z</param>
    /// <param name="ex">终点 X</param>
    /// <param name="ey">终点 Y</param>
    /// <param name="ez">终点 Z</param>
    /// <returns>路径，无解时返回 null</returns>
    Position[] Search(int sx, int sy, int sz, int ex, int ey, int ez);

    /// <summary>
    /// 在两点之间寻路
    /// </summary>
    /// <param name="startPos">起点</param>
    /// <param name="endPos">终点</param>
    /// <returns>路径，无解时返回 null</returns>
    Position[] SearchPosition(Position startPos, Position endPos);
}
```

#### IAStarGridMap
基于格子的 A* 地图门面：管理地图数据、格子尺寸及高层寻路查询

```csharp
/// <summary>
/// 基于格子的 A* 地图门面：管理地图数据、格子尺寸及高层寻路查询
/// </summary>
public interface IAStarGridMap
{
    /// <summary>
    /// 以逻辑数据尺寸初始化地图，格子尺寸默认为 1×1×1
    /// </summary>
    /// <param name="dataSize">各维格子数量</param>
    void InitGridMap(Size dataSize);

    /// <summary>
    /// 以逻辑数据尺寸与单格世界尺寸初始化地图
    /// </summary>
    /// <param name="dataSize">各维格子数量</param>
    /// <param name="gridSize">单格世界尺寸</param>
    void InitGridMap(Size dataSize, Size gridSize);

    /// <summary>
    /// 设置寻路时允许扩展的方向
    /// </summary>
    /// <param name="direction">方向索引数组</param>
    void SetAllowedDirections(int[] direction);

    /// <summary>
    /// 以一维数组设置地图数据
    /// </summary>
    /// <param name="data">长度须等于 width × height × depth</param>
    /// <returns>成功返回 null，失败返回异常</returns>
    Exception SetMapData(int[] data);

    /// <summary>
    /// 以二维数组设置地图数据（depth = 1）
    /// </summary>
    /// <param name="data">长度须等于 width × height</param>
    /// <returns>成功返回 null，失败返回异常</returns>
    Exception SetMapData(int[][] data);

    /// <summary>
    /// 以三维数组 [depth][height][width] 设置地图数据
    /// </summary>
    /// <param name="data">尺寸须与初始化一致</param>
    /// <returns>成功返回 null，失败返回异常</returns>
    Exception SetMapData(int[][][] data);

    /// <summary>
    /// 设置自定义步进代价与启发函数（可传 null 保留默认）
    /// </summary>
    /// <param name="dn">步进代价委托</param>
    /// <param name="hn">启发函数委托</param>
    void SetCustomFunc(AStarDelegates.FuncDn dn, AStarDelegates.FuncHn hn);

    /// <summary>
    /// 获取单格世界尺寸
    /// </summary>
    /// <returns>格子尺寸</returns>
    Size GetGridSize();

    /// <summary>
    /// 获取逻辑地图尺寸（格子数）
    /// </summary>
    /// <returns>数据尺寸</returns>
    Size GetDataSize();

    /// <summary>
    /// 获取地图世界总尺寸（格子尺寸 × 数据尺寸）
    /// </summary>
    /// <returns>像素/世界尺寸</returns>
    Size GetPixelSize();

    /// <summary>
    /// 获取底层 A* 算法实例
    /// </summary>
    /// <returns>A* 算法</returns>
    IAStarAlg GetAStartAlg();

    /// <summary>
    /// 获取指定格子的数据值
    /// </summary>
    /// <param name="pos">格子坐标</param>
    /// <returns>格子值，越界时返回 GridOut</returns>
    int GetDataValue(Position pos);

    /// <summary>
    /// 检查路径上每一点是否均可通行
    /// </summary>
    /// <param name="path">待校验路径</param>
    /// <returns>全部可通行时返回 true</returns>
    bool CheckPath(Position[] path);

    /// <summary>
    /// 判断两点之间是否可直线通行（无遮挡）
    /// </summary>
    /// <param name="startPos">起点</param>
    /// <param name="endPos">终点</param>
    /// <returns>存在无障碍直线时返回 true</returns>
    bool CanLineTo(Position startPos, Position endPos);

    /// <summary>
    /// 在两点间寻路；可选是否保留共线中间点（拐点）
    /// </summary>
    /// <param name="startPos">起点</param>
    /// <param name="endPos">终点</param>
    /// <param name="keepTurningPoint">为 true 保留拐点，为 false 则简化共线段</param>
    /// <returns>路径数组，不可达时返回 null</returns>
    Position[] SearchPath(Position startPos, Position endPos, bool keepTurningPoint = false);
}
```

### 结构体 (Structs)

#### Position
格子坐标（X, Y, Z）

```csharp
/// <summary>
/// 格子坐标（X, Y, Z）
/// </summary>
public struct Position : IEquatable<Position>
{
    /// <summary>格子 X 坐标</summary>
    public int X, Y, Z;

    /// <summary>
    /// 将方向偏移加到当前坐标
    /// </summary>
    /// <param name="vector">方向偏移向量</param>
    /// <returns>新坐标</returns>
    public Position AddVector(DirectionVector vector);

    public override string ToString();
    public override bool Equals(object obj);
    public override int GetHashCode();
    public bool Equals(Position other);

    /// <summary>相等比较</summary>
    public static bool operator !=(Position b, Position c);

    /// <summary>相等比较</summary>
    public static bool operator ==(Position b, Position c);

    /// <summary>默认空坐标</summary>
    public static readonly Position Empty;
}
```

#### PriorityPosition
带 A* 优先级（f 值）的格子坐标

```csharp
/// <summary>
/// 带 A* 优先级（f 值）的格子坐标
/// </summary>
public struct PriorityPosition
{
    /// <summary>格子 X 坐标</summary>
    public int X, Y, Z;

    /// <summary>Open 表优先级（通常为 f = g + h）</summary>
    public int Priority;

    /// <summary>
    /// 与另一项比较坐标与优先级是否完全相同
    /// </summary>
    /// <param name="pos">另一项</param>
    /// <returns>相等时返回 true</returns>
    public bool EqualTo(PriorityPosition pos);

    /// <summary>
    /// 加上方向偏移并返回普通坐标
    /// </summary>
    /// <param name="vector">方向偏移向量</param>
    /// <returns>新坐标</returns>
    public Position AddVector(DirectionVector vector);

    public override string ToString();

    /// <summary>默认空项</summary>
    public static readonly PriorityPosition Empty;
}
```

#### Size
网格或地图区域的宽、高、深

```csharp
/// <summary>
/// 网格或地图区域的宽、高、深
/// </summary>
public struct Size
{
    /// <summary>宽度（X 方向）</summary>
    public int Width, Height, Depth;

    /// <summary>总格子数：Width × Height × Depth</summary>
    public int Area { get; }

    /// <summary>任一维为 0 时返回 true</summary>
    public bool Empty { get; }
}
```

无公共构造函数，使用对象初始化器赋值，例如 `new Size { Width = 100, Height = 100, Depth = 1 }`。二维地图也须将 `Depth` 设为 1，否则 `Empty` 为 true。

#### DirectionValue
方向偏移值

```csharp
/// <summary>
/// 方向偏移值
/// </summary>
public struct DirectionValue : IEquatable<DirectionValue>
{
    /// <summary>X 方向偏移</summary>
    public int OffsetX, OffsetY, OffsetZ;

    /// <summary>归一化单位方向（尽可能按最大公约数约简）</summary>
    public DirectionValue UnitValue { get; }

    public override string ToString();
    public override bool Equals(object obj);
    public override int GetHashCode();
    public bool Equals(DirectionValue other);

    /// <summary>
    /// 比较方向是否相同（含单位向量等价）
    /// </summary>
    /// <param name="other">另一偏移</param>
    /// <returns>同向时返回 true</returns>
    public bool DirectionEquals(DirectionValue other);

    /// <summary>不等比较</summary>
    public static bool operator !=(DirectionValue b, DirectionValue c);

    /// <summary>相等比较</summary>
    public static bool operator ==(DirectionValue b, DirectionValue c);

    /// <summary>偏移相加</summary>
    public static DirectionValue operator +(DirectionValue b, DirectionValue c);

    /// <summary>偏移相减</summary>
    public static DirectionValue operator -(DirectionValue b, DirectionValue c);

    /// <summary>缩放偏移</summary>
    public static DirectionValue operator *(DirectionValue b, int scale);
}
```

#### DirectionVector
带权值的三维方向

```csharp
/// <summary>
/// 带权值的三维方向
/// </summary>
public struct DirectionVector : IEquatable<DirectionVector>
{
    /// <summary>基础方向枚举</summary>
    public Direction3D Direction;

    /// <summary>步长倍数</summary>
    public int Len;

    /// <summary>该方向单步移动代价</summary>
    public int Vector;

    /// <summary>该方向的单位偏移</summary>
    public DirectionValue Value { get; }

    /// <summary>按 Len 缩放后的偏移</summary>
    public DirectionValue RealValue { get; }

    /// <summary>单步 X 偏移</summary>
    public int OffsetX { get; }

    /// <summary>单步 Y 偏移</summary>
    public int OffsetY { get; }

    /// <summary>单步 Z 偏移</summary>
    public int OffsetZ { get; }

    /// <summary>步进代价（同 Vector）</summary>
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
从坐标队列出队的结果

```csharp
/// <summary>
/// 从坐标队列出队的结果
/// </summary>
public struct QueueResult
{
    /// <summary>出队坐标</summary>
    public Position Position;

    /// <summary>操作是否成功</summary>
    public bool Ok;

    public override string ToString();

    /// <summary>队列为空时的错误哨兵</summary>
    public static readonly QueueResult Error;
}
```

#### PriorityPositionQueueResult
从优先级坐标队列出队的结果

```csharp
/// <summary>
/// 从优先级坐标队列出队的结果
/// </summary>
public struct PriorityPositionQueueResult
{
    /// <summary>出队项</summary>
    public PriorityPosition Position;

    /// <summary>操作是否成功</summary>
    public bool Ok;

    public override string ToString();

    /// <summary>队列为空时的错误哨兵</summary>
    public static readonly PriorityPositionQueueResult Error;
}
```

### 类 (Classes)

#### AStarAlg
A* 寻路算法实现

```csharp
/// <summary>
/// A* 寻路算法实现
/// </summary>
public class AStarAlg : IAStarAlg
{
    public void InitMapSize(int width, int height, int depth);
    public void InitMapSize(int width, int height);

    public int[][][] SetData(int[] data);
    public int[][][] SetData(int[][] data);
    public int[][][] SetData(int[][][] data);

    /// <summary>允许检索/扩展的方向集合</summary>
    public int[] AllowdDirections { get; }

    public void SetAllowedDirections(int[] direction);
    public void SetCustomFuncHn(AStarDelegates.FuncHn hn);
    public void SetCustomFuncDn(AStarDelegates.FuncDn dn);

    public Position[] Search(int sx, int sy, int ex, int ey);
    public Position[] Search(int sx, int sy, int sz, int ex, int ey, int ez);

    /// <summary>
    /// 在两点之间寻路；若起终点均落在上次成功路径上，则直接返回对应子路径
    /// </summary>
    public Position[] SearchPosition(Position startPos, Position endPos);
}
```

默认启发函数为曼哈顿距离。寻路前必须调用 `SetAllowedDirections`，否则内部方向数组为 null。格子值约定见 `AstarConst`：不可走为 `GridObstacle` 或 `GridOut`。

#### AStarGridMap
基于格子的 A* 地图实现

```csharp
/// <summary>
/// 基于格子的 A* 地图实现
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

`InitGridMap` 在 `dataSize` 或 `gridSize` 为 Empty 时抛出 `"GridMap Empty! "`。`SetMapData` 失败时返回 `new Exception("Data Error!")`。`SearchPath` 在起终点相同、可直线通行、或需要去掉共线点时会走快捷路径 / `AStarUtil.ClearRedundancies`。

#### AstarConst
A* 检索常量与默认格子取值

```csharp
/// <summary>
/// A* 检索常量与默认格子取值
/// </summary>
public class AstarConst
{
    /// <summary>每次检索递增的 Mask 步长（避免整图重置）</summary>
    public static readonly int DEFAULT_MASK_ADD = 1048576;

    /// <summary>Mask 累积上限，超出后触发地图重置</summary>
    public static readonly int MAX_MASK = 2000000000;

    /// <summary>地图外</summary>
    public static readonly int GridOut = -1;

    /// <summary>无</summary>
    public static readonly int GridNone = 0;

    /// <summary>可通行通路</summary>
    public static readonly int GridPath = 1;

    /// <summary>障碍</summary>
    public static readonly int GridObstacle = 2;
}
```

网格地图将既不是 `GridOut` 也不是 `GridObstacle` 的格子视为可走（含 `GridNone` 与 `GridPath`）。

#### DirectionGroup
方向集合

```csharp
/// <summary>
/// 方向集合
/// </summary>
public class DirectionGroup
{
    /// <summary>允许的方向索引</summary>
    public int[] Directions { get; }

    /// <summary>
    /// 以二维方向枚举设置允许方向
    /// </summary>
    /// <param name="directions">二维方向数组</param>
    public void SetDirrections(Direction2D[] directions);

    /// <summary>
    /// 以三维方向枚举设置允许方向
    /// </summary>
    /// <param name="directions">三维方向数组</param>
    public void SetDirrections(Direction3D[] directions);
}
```

#### PositionQueue
格子坐标先进先出队列

```csharp
/// <summary>
/// 格子坐标先进先出队列
/// </summary>
public class PositionQueue : Queue<Position>
{
    /// <summary>
    /// 入队一个坐标
    /// </summary>
    /// <param name="pos">坐标</param>
    public void Push(Position pos);

    /// <summary>
    /// 出队队首元素
    /// </summary>
    /// <param name="pos">未使用，保留以兼容 API</param>
    /// <returns>出队结果</returns>
    public QueueResult Shift(Position pos);
}
```

另继承 `Queue<Position>` 的公共成员。

#### PriorityPositionQueue
按优先级升序排列的 Open 表（f 值越小越靠前）

```csharp
/// <summary>
/// 按优先级升序排列的 Open 表（f 值越小越靠前）
/// </summary>
public class PriorityPositionQueue : List<PriorityPosition>
{
    /// <summary>
    /// 将所有项复制为新数组
    /// </summary>
    /// <returns>队列快照</returns>
    public PriorityPosition[] GetAll();

    /// <summary>
    /// 按优先级升序插入（选择式插入）
    /// </summary>
    /// <param name="ppos">待插入项</param>
    public void PushPriorityPosition(PriorityPosition ppos);

    /// <summary>
    /// 入队三维坐标及优先级
    /// </summary>
    /// <param name="x">X 坐标</param>
    /// <param name="y">Y 坐标</param>
    /// <param name="z">Z 坐标</param>
    /// <param name="priority">优先级（f 值）</param>
    public void Push(int x, int y, int z, int priority);

    /// <summary>
    /// 入队二维坐标及优先级（Z = 0）
    /// </summary>
    /// <param name="x">X 坐标</param>
    /// <param name="y">Y 坐标</param>
    /// <param name="priority">优先级（f 值）</param>
    public void Push(int x, int y, int priority);

    /// <summary>
    /// 取出并返回尾部元素（本排序下优先级最低）
    /// </summary>
    /// <returns>出队结果</returns>
    public PriorityPositionQueueResult Pop();

    /// <summary>
    /// 取出并返回头部元素（优先级最高 / f 值最小）
    /// </summary>
    /// <returns>出队结果</returns>
    public PriorityPositionQueueResult Shift();

    /// <summary>当前队列长度</summary>
    public int Len { get; }
}
```

另继承 `List<PriorityPosition>` 的公共成员。A* 内部 Open 表使用 `Shift()` 取出 f 值最小的节点。

### 静态类 (Static Classes)

#### Positions
坐标构造与格式化辅助

```csharp
/// <summary>
/// 坐标构造与格式化辅助
/// </summary>
public static class Positions
{
    /// <summary>
    /// 创建二维坐标（Z = 0）
    /// </summary>
    public static Position NewPosition(int x, int y);

    /// <summary>
    /// 创建三维坐标
    /// </summary>
    public static Position NewPosition(int x, int y, int z);

    /// <summary>
    /// 创建二维优先级坐标（Z = 0）
    /// </summary>
    public static PriorityPosition NewPriorityPosition(int x, int y, int p);

    /// <summary>
    /// 创建三维优先级坐标
    /// </summary>
    public static PriorityPosition NewPriorityPosition(int x, int y, int z, int p);

    /// <summary>
    /// 将坐标数组格式化为路径字符串
    /// </summary>
    /// <typeparam name="T">具备 ToString 的坐标类型</typeparam>
    /// <param name="positions">路径点数组</param>
    /// <returns>格式化字符串</returns>
    public static string ToString<T>(T[] positions);
}
```

#### AStarUtil
路径方向判断与拐点简化工具

```csharp
public static class AStarUtil
{
    /// <summary>
    /// 判断方向,前提是两点为线向
    /// </summary>
    /// <param name="startPos">起点</param>
    /// <param name="endPos">终点</param>
    /// <returns>三维方向枚举</returns>
    public static Direction3D GetDirection3D(Position startPos, Position endPos);

    /// <summary>
    /// 判断方向,前提是两点为线向
    /// 采用笛卡尔坐标系
    /// </summary>
    /// <param name="sourcePos">源点</param>
    /// <param name="targetPos">目标点</param>
    /// <returns>二维方向枚举</returns>
    public static Direction2D GetDirection2D(Position sourcePos, Position targetPos);

    /// <summary>
    /// 清除冗余点，保留拐点
    /// </summary>
    /// <param name="path">原始路径</param>
    /// <param name="allowDirection">允许的行走方向，仅沿这些方向的共线点会被移除</param>
    /// <returns>简化后的路径</returns>
    public static Position[] ClearRedundancies(Position[] path, int[] allowDirection);

    /// <summary>
    /// 清除冗余点，保留拐点
    /// </summary>
    /// <param name="path">原始路径</param>
    /// <returns>简化后的路径</returns>
    public static Position[] ClearRedundancies(Position[] path);

    /// <summary>
    /// 判断三点是否一线
    /// </summary>
    /// <param name="first">第一点</param>
    /// <param name="second">中间点</param>
    /// <param name="third">第三点</param>
    /// <returns>共线时返回 true</returns>
    public static bool IsInLine(Position first, Position second, Position third);

    /// <summary>
    /// 是否标准线向
    /// </summary>
    /// <param name="pos1">坐标一</param>
    /// <param name="pos2">坐标二</param>
    /// <param name="includeOblique">是否包含斜向</param>
    /// <returns>在轴对齐或（允许时）斜线上返回 true</returns>
    public static bool IsInStandardLine(Position pos1, Position pos2, bool includeOblique);
}
```

#### DirectionsStatic
一些默认常用方向组合

```csharp
/// <summary>
/// 一些默认常用方向组合
/// </summary>
public static class DirectionsStatic
{
    /// <summary>二维8方向</summary>
    public static readonly int[] DefaultDirections2D;

    /// <summary>三维26方向</summary>
    public static readonly int[] DefaultDirections3D;

    /// <summary>二维斜向4方向</summary>
    public static readonly int[] ObliqueDirections2D;

    /// <summary>三维斜向14方向</summary>
    public static readonly int[] ObliqueDirections3D;

    /// <summary>基本方向偏移值</summary>
    public static readonly DirectionValue[] BasicDirectionValue;

    /// <summary>基本带权方向偏移值</summary>
    public static readonly DirectionVector[] BasicDirectionVector;

    /// <summary>中心 / 无移动</summary>
    public static DirectionVector VectorCenter { get; }
    /// <summary>北（+Y）</summary>
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
    /// 根据方向偏移值查找方向
    /// </summary>
    /// <param name="dValue">方向偏移</param>
    /// <returns>匹配方向，无匹配时返回 None</returns>
    public static Direction3D GetDirectionByValue(DirectionValue dValue);

    /// <summary>
    /// 按索引获取带权方向向量
    /// </summary>
    public static DirectionVector GetVector(int direction);

    /// <summary>
    /// 获取二维方向的带权向量
    /// </summary>
    public static DirectionVector GetVector(Direction2D direction);

    /// <summary>
    /// 获取三维方向的带权向量
    /// </summary>
    public static DirectionVector GetVector(Direction3D direction);
}
```

`BasicDirectionVector` 中各方向默认步进代价 `Vector` 均为 5。`VectorUp` 等二维别名分别对应 `VectorNorth` 等。

### 委托 (Delegates)

#### AStarDelegates
代理定义

```csharp
/// <summary>
/// 代理定义
/// </summary>
public static class AStarDelegates
{
    /// <summary>
    /// 两点间的移动代价计算
    /// </summary>
    /// <param name="cx">当前点 X</param>
    /// <param name="cy">当前点 Y</param>
    /// <param name="cz">当前点 Z</param>
    /// <param name="ex">终点 X</param>
    /// <param name="ey">终点 Y</param>
    /// <param name="ez">终点 Z</param>
    /// <returns>到终点的估计代价（h）</returns>
    public delegate int FuncHn(int cx, int cy, int cz, int ex, int ey, int ez);

    /// <summary>
    /// 方向移动代价计算
    /// </summary>
    /// <param name="dirX">X 方向偏移（-1、0 或 1）</param>
    /// <param name="dirY">Y 方向偏移</param>
    /// <param name="dirZ">Z 方向偏移</param>
    /// <returns>该方向的步进代价</returns>
    public delegate int FuncDn(int dirX, int dirY, int dirZ);
}
```

### 枚举 (Enums)

#### Direction3D
三维方向

```csharp
/// <summary>
/// 三维方向
/// </summary>
public enum Direction3D
{
    X0_Y0_Z0 = 0, // 水平方向：中心
    X0_Y1_Z0,     // 水平方向：↑
    X1_Y1_Z0,     // 水平方向：↗
    X1_Y0_Z0,     // 水平方向：→
    X1_Y__Z0,     // 水平方向：↘
    X0_Y__Z0,     // 水平方向：↓
    X__Y__Z0,     // 水平方向：↙
    X__Y0_Z0,     // 水平方向：←
    X__Y1_Z0,     // 水平方向：↖
    X0_Y0_Z1,     // Z增加方向：中心
    X0_Y1_Z1,     // Z增加方向：↑
    X1_Y1_Z1,     // Z增加方向：↗
    X1_Y0_Z1,     // Z增加方向：→
    X1_Y__Z1,     // Z增加方向：↘
    X0_Y__Z1,     // Z增加方向：↓
    X__Y__Z1,     // Z增加方向：↙
    X__Y0_Z1,     // Z增加方向：←
    X__Y1_Z1,     // Z增加方向：↖
    X0_Y0_Z_,     // Z减小方向：中心
    X0_Y1_Z_,     // Z减小方向：↑
    X1_Y1_Z_,     // Z减小方向：↗
    X1_Y0_Z_,     // Z减小方向：→
    X1_Y__Z_,     // Z减小方向：↘
    X0_Y__Z_,     // Z减小方向：↓
    X__Y__Z_,     // Z减小方向：↙
    X__Y0_Z_,     // Z减小方向：←
    X__Y1_Z_,     // Z减小方向：↖
    None          // None
}
```

命名中 `__` 表示该轴偏移为 -1。枚举值可作为 `SetAllowedDirections` 的方向索引。

#### Direction2D
二维方向。注意：二维数据的视角坐标，向下 Y+1，对应为 Up 方向。

```csharp
/// <summary>
/// 二维方向
/// 注意：二维数据的视角坐标，向下Y+1, 对应为Up方向
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

### 功能说明

#### A* 算法特性

**核心功能**
- **启发式搜索**：默认曼哈顿距离；可通过 `SetCustomFuncHn` 替换
- **步进代价**：默认使用方向向量的 `OffsetV`；可通过 `SetCustomFuncDn` 替换
- **多维支持**：`InitMapSize` / `Search` 提供 2D 与 3D 重载
- **方向控制**：用方向索引数组限制扩展方向（见 `DirectionsStatic`）
- **路径缓存**：`SearchPosition` 在起终点均落在上次成功路径上时直接截取子路径
- **检索 Mask**：通过递增 Mask 避免每次整图重置；超过 `AstarConst.MAX_MASK` 后重置检索图

**格子取值**
- `AstarConst.GridOut`（-1）：地图外
- `AstarConst.GridNone`（0）：无
- `AstarConst.GridPath`（1）：可通行
- `AstarConst.GridObstacle`（2）：障碍
- 寻路时跳过 `GridObstacle` 与 `GridOut`

#### 网格地图特性

**地图管理**
- 逻辑尺寸（格子数）与单格世界尺寸分开配置
- 支持一维、二维、三维地图数据
- `SetMapData` 以返回 `Exception`（或 null）表示结果，不抛出数据错误
- 可通过 `GetAStartAlg()` 访问底层 `IAStarAlg`

**功能特性**
1. **直通检测**：`CanLineTo` 判断标准线向上是否无遮挡
2. **路径优化**：`SearchPath(..., keepTurningPoint: false)` 会去掉共线中间点
3. **数据查询**：`GetDataValue` 越界返回 `GridOut`
4. **路径校验**：`CheckPath` 检查路径上每格是否可走

### 使用示例

#### 基本 A* 寻路
```csharp
var aStarAlg = new AStarAlg();

aStarAlg.InitMapSize(100, 100);

int[] mapData = new int[100 * 100];
for (int i = 0; i < 100; i++)
{
    mapData[i * 100 + 50] = AstarConst.GridObstacle; // 中间一列障碍
}
aStarAlg.SetData(mapData);

aStarAlg.SetAllowedDirections(DirectionsStatic.DefaultDirections2D);

Position[] path = aStarAlg.Search(0, 0, 99, 99);
if (path == null)
{
    Console.WriteLine("无路径");
    return;
}

Console.WriteLine($"路径长度: {path.Length}");
Console.WriteLine(Positions.ToString(path));
```

#### 网格地图寻路
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
Console.WriteLine($"路径有效: {isValid}");

bool canLineTo = gridMap.CanLineTo(startPos, endPos);
Console.WriteLine($"是否直通: {canLineTo}");
```

#### 自定义估值函数
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

#### 3D 寻路
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
    Console.WriteLine($"3D 路径长度: {path.Length}");
    foreach (var pos in path)
    {
        Console.WriteLine($"3D 路径点: {pos}");
    }
}
```

#### 路径优化
```csharp
var gridMap = new AStarGridMap();
gridMap.InitGridMap(new Size { Width = 100, Height = 100, Depth = 1 });
gridMap.SetAllowedDirections(DirectionsStatic.DefaultDirections2D);
// ... 设置地图数据 ...

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

Console.WriteLine($"保留拐点长度: {pathWithTurns?.Length}");
Console.WriteLine($"简化后长度: {optimizedPath?.Length}");

// 也可直接对已有路径去冗余
Position[] simplified = AStarUtil.ClearRedundancies(pathWithTurns);
```

#### 方向组合
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

### 设计特点

1. **算法优化**：启发式搜索；Mask 递增减少整图清零
2. **多维支持**：同一套 API 覆盖 2D / 3D
3. **灵活配置**：自定义 h / d 函数与允许方向
4. **路径优化**：网格地图可去掉共线中间点
5. **方向体系**：`Direction3D` / `Direction2D` 与带权 `DirectionVector`
6. **结构体坐标**：`Position` 等为值类型，适合高频寻路

### 注意事项

1. **地图初始化**：使用前必须 `InitMapSize` / `InitGridMap`；`Size.Depth` 二维时须为 1
2. **允许方向**：寻路前必须 `SetAllowedDirections`，否则方向数组为 null
3. **数据一致性**：`SetData` / `SetMapData` 长度须等于 width × height × depth；无效时前者返回 null，后者返回异常
4. **格子取值**：障碍请使用 `AstarConst.GridObstacle`（2）；`0`/`1` 均视为可走
5. **无路径**：`Search` / `SearchPosition` / `SearchPath` 无解时返回 null，使用前请判空
6. **委托签名**：`FuncHn` / `FuncDn` 使用整型坐标与方向偏移，不是 `Position`
7. **性能与内存**：大地图与 3D 地图占用更多内存；超长路径可能触发 Mask 上限异常 `"Mask Error: gn >= maxMask"`
