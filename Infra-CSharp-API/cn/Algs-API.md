# Algs API 文档

## 命名空间: JLGames.Infra.Algs.AStar

### 接口 (Interfaces)

#### IAStarAlg
A*核心算法接口

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
    /// 初始化地图尺寸
    /// </summary>
    /// <param name="width">地图宽度</param>
    /// <param name="height">地图高度</param>
    void InitMapSize(int width, int height);

    /// <summary>
    /// 取允许检索的方向
    /// </summary>
    /// <returns>允许的方向数组</returns>
    int[] AllowdDirections { get; }

    /// <summary>
    /// 设置允许检索的方向(可用于特殊地图) 
    /// </summary>
    /// <param name="direction">方向数组</param>
    void SetAllowedDirections(int[] direction);

    /// <summary>
    /// 设置自定义的距离估值函数
    /// </summary>
    /// <param name="hn">距离估值函数</param>
    void SetCustomFuncHn(AStarDelegates.FuncHn hn);

    /// <summary>
    /// 设置自定义的方向估值函数
    /// </summary>
    /// <param name="dn">方向估值函数</param>
    void SetCustomFuncDn(AStarDelegates.FuncDn dn);

    /// <summary>
    /// 设置地图数据
    /// </summary>
    /// <param name="data">长度=地图的width*height*depth</param>
    /// <returns>处理后的地图数据</returns>
    int[][][] SetData(int[] data);

    /// <summary>
    /// 设置地图数据
    /// 注意: depth=1
    /// </summary>
    /// <param name="data">长度=地图的width*height</param>
    /// <returns>处理后的地图数据</returns>
    int[][][] SetData(int[][] data);

    /// <summary>
    /// 设置地图数据
    /// </summary>
    /// <param name="data">int[depth][height][width]对应长度与初始化时一致</param>
    /// <returns>处理后的地图数据</returns>
    int[][][] SetData(int[][][] data);

    /// <summary>
    /// 二维寻路
    /// </summary>
    /// <param name="sx">Start Point X</param>
    /// <param name="sy">Start Point Y</param>
    /// <param name="ex">End Point X</param>
    /// <param name="ey">End Point Y</param>
    /// <returns>寻路路径</returns>
    Position[] Search(int sx, int sy, int ex, int ey);

    /// <summary>
    /// 三维寻路
    /// sx:StartX; sy:StartY; sz:StartZ
    /// ex:EndX; ey:EndY; ez:EndZ
    /// </summary>
    /// <param name="sx">Start Point X</param>
    /// <param name="sy">Start Point Y</param>
    /// <param name="sz">Start Point Z</param>
    /// <param name="ex">End Point X</param>
    /// <param name="ey">End Point Y</param>
    /// <param name="ez">End Point Z</param>
    /// <returns>寻路路径</returns>
    Position[] Search(int sx, int sy, int sz, int ex, int ey, int ez);

    /// <summary>
    /// 寻路
    /// </summary>
    /// <param name="startPos">Start Point; 开始点</param>
    /// <param name="endPos">End Point: 结束点</param>
    /// <returns>寻路路径</returns>
    Position[] SearchPosition(Position startPos, Position endPos);
}
```

#### IAStarGridMap
A*网格地图接口

```csharp
/// <summary>
/// A*网格地图接口
/// 提供网格地图的寻路功能
/// </summary>
public interface IAStarGridMap
{
    /// <summary>
    /// 初始化地图尺寸 
    /// </summary>
    /// <param name="dataSize">数据大小</param>
    void InitGridMap(Size dataSize);

    /// <summary>
    /// 初始化地图尺寸
    /// </summary>
    /// <param name="dataSize">数据大小</param>
    /// <param name="gridSize">网格大小</param>
    void InitGridMap(Size dataSize, Size gridSize);

    /// <summary>
    /// 设置允许寻路的方向
    /// </summary>
    /// <param name="direction">方向数组</param>
    void SetAllowedDirections(int[] direction);

    /// <summary>
    /// 设置地图数据
    /// </summary>
    /// <param name="data">一维数据</param>
    /// <returns>异常信息</returns>
    Exception SetMapData(int[] data);

    /// <summary>
    /// 设置地图数据
    /// </summary>
    /// <param name="data">二维数据</param>
    /// <returns>异常信息</returns>
    Exception SetMapData(int[][] data);

    /// <summary>
    /// 设置地图数据
    /// </summary>
    /// <param name="data">三维数据</param>
    /// <returns>异常信息</returns>
    Exception SetMapData(int[][][] data);

    /// <summary>
    /// 设置自定义估值函数
    /// </summary>
    /// <param name="dn">方向估值函数</param>
    /// <param name="hn">距离估值函数</param>
    void SetCustomFunc(AStarDelegates.FuncDn dn, AStarDelegates.FuncHn hn);

    /// <summary>
    /// 获取网格大小
    /// </summary>
    /// <returns>网格大小</returns>
    Size GetGridSize();

    /// <summary>
    /// 获取地图数据大小
    /// </summary>
    /// <returns>数据大小</returns>
    Size GetDataSize();

    /// <summary>
    /// 获取地图像素大小
    /// </summary>
    /// <returns>像素大小</returns>
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
    /// 判断路径是否通路
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>是否通路</returns>
    bool CheckPath(Position[] path);

    /// <summary>
    /// 判断是否两点直通
    /// </summary>
    /// <param name="startPos">起始点</param>
    /// <param name="endPos">结束点</param>
    /// <returns>是否直通</returns>
    bool CanLineTo(Position startPos, Position endPos);

    /// <summary>
    /// 检索路径
    /// 默认清除拐点
    /// </summary>
    /// <param name="startPos">起始点</param>
    /// <param name="endPos">结束点</param>
    /// <param name="keepTurningPoint">是否保留拐点</param>
    /// <returns>寻路路径</returns>
    Position[] SearchPath(Position startPos, Position endPos, bool keepTurningPoint = false);
}
```

### 结构体 (Structs)

#### Position
点位置信息结构体

```csharp
/// <summary>
/// point location information
/// 点位置信息
/// </summary>
public struct Position : IEquatable<Position>
{
    public int X, Y, Z;

    /// <summary>
    /// 添加方向向量
    /// </summary>
    /// <param name="vector">方向向量</param>
    /// <returns>新位置</returns>
    public Position AddVector(DirectionVector vector);

    /// <summary>
    /// 字符串表示
    /// </summary>
    /// <returns>位置字符串</returns>
    public override string ToString();

    /// <summary>
    /// 相等比较
    /// </summary>
    /// <param name="obj">比较对象</param>
    /// <returns>是否相等</returns>
    public override bool Equals(object obj);

    /// <summary>
    /// 哈希码
    /// </summary>
    /// <returns>哈希值</returns>
    public override int GetHashCode();

    /// <summary>
    /// 相等比较
    /// </summary>
    /// <param name="other">比较位置</param>
    /// <returns>是否相等</returns>
    public bool Equals(Position other);

    /// <summary>
    /// 不等比较运算符
    /// </summary>
    public static bool operator !=(Position b, Position c);

    /// <summary>
    /// 相等比较运算符
    /// </summary>
    public static bool operator ==(Position b, Position c);

    /// <summary>
    /// 空位置
    /// </summary>
    public static readonly Position Empty;
}
```

#### PriorityPosition
带权值的点位置结构体

```csharp
/// <summary>
/// Point locations with weights
/// 带权值的点位置
/// </summary>
public struct PriorityPosition
{
    public int X, Y, Z;
    public int Priority;

    /// <summary>
    /// 相等比较
    /// </summary>
    /// <param name="pos">比较位置</param>
    /// <returns>是否相等</returns>
    public bool EqualTo(PriorityPosition pos);

    /// <summary>
    /// 添加方向向量
    /// </summary>
    /// <param name="vector">方向向量</param>
    /// <returns>新位置</returns>
    public Position AddVector(DirectionVector vector);

    /// <summary>
    /// 字符串表示
    /// </summary>
    /// <returns>位置字符串</returns>
    public override string ToString();

    /// <summary>
    /// 空位置
    /// </summary>
    public static readonly PriorityPosition Empty;
}
```

#### Size
大小结构体

```csharp
/// <summary>
/// 大小结构体
/// 表示宽度、高度、深度
/// </summary>
public struct Size
{
    public int Width;
    public int Height;
    public int Depth;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    public Size(int width, int height);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    /// <param name="depth">深度</param>
    public Size(int width, int height, int depth);
}
```

#### DirectionVector
方向向量结构体

```csharp
/// <summary>
/// 方向向量
/// 表示移动方向的偏移量
/// </summary>
public struct DirectionVector
{
    public int OffsetX;
    public int OffsetY;
    public int OffsetZ;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="offsetX">X偏移</param>
    /// <param name="offsetY">Y偏移</param>
    /// <param name="offsetZ">Z偏移</param>
    public DirectionVector(int offsetX, int offsetY, int offsetZ);
}
```

### 类 (Classes)

#### AStarAlg
A*算法实现类

```csharp
/// <summary>
/// A*算法实现
/// 提供完整的A*寻路算法功能
/// </summary>
public class AStarAlg : IAStarAlg
{
    // 具体实现需要进一步分析文件内容
}
```

#### AStarGridMap
A*网格地图实现类

```csharp
/// <summary>
/// A*网格地图实现
/// 提供网格地图的寻路功能
/// </summary>
public class AStarGridMap : IAStarGridMap
{
    // 具体实现需要进一步分析文件内容
}
```

### 静态类 (Static Classes)

#### Positions
位置工具类

```csharp
/// <summary>
/// 位置工具类
/// 提供位置创建和操作的静态方法
/// </summary>
public static class Positions
{
    /// <summary>
    /// 创建二维位置
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <returns>位置</returns>
    public static Position NewPosition(int x, int y);

    /// <summary>
    /// 创建三维位置
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <param name="z">Z坐标</param>
    /// <returns>位置</returns>
    public static Position NewPosition(int x, int y, int z);

    /// <summary>
    /// 创建带权值的二维位置
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <param name="p">权值</param>
    /// <returns>带权值位置</returns>
    public static PriorityPosition NewPriorityPosition(int x, int y, int p);

    /// <summary>
    /// 创建带权值的三维位置
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <param name="z">Z坐标</param>
    /// <param name="p">权值</param>
    /// <returns>带权值位置</returns>
    public static PriorityPosition NewPriorityPosition(int x, int y, int z, int p);

    /// <summary>
    /// 将位置数组转换为字符串
    /// </summary>
    /// <typeparam name="T">位置类型</typeparam>
    /// <param name="positions">位置数组</param>
    /// <returns>字符串表示</returns>
    public static string ToString<T>(T[] positions);
}
```

### 委托 (Delegates)

#### AStarDelegates
A*算法委托定义

```csharp
/// <summary>
/// A*算法委托定义
/// 定义A*算法中使用的各种回调函数
/// </summary>
public static class AStarDelegates
{
    /// <summary>
    /// 距离估值函数委托
    /// 计算从当前位置到目标位置的估计距离
    /// </summary>
    /// <param name="current">当前位置</param>
    /// <param name="target">目标位置</param>
    /// <returns>估计距离</returns>
    public delegate int FuncHn(Position current, Position target);

    /// <summary>
    /// 方向估值函数委托
    /// 计算从当前位置到目标位置的方向估值
    /// </summary>
    /// <param name="current">当前位置</param>
    /// <param name="target">目标位置</param>
    /// <returns>方向估值</returns>
    public delegate int FuncDn(Position current, Position target);
}
```

### 枚举 (Enums)

#### Direction
方向枚举

```csharp
/// <summary>
/// 方向枚举
/// 定义寻路算法中使用的方向
/// </summary>
public enum Direction
{
    /// <summary>
    /// 上
    /// </summary>
    Up = 0,

    /// <summary>
    /// 下
    /// </summary>
    Down = 1,

    /// <summary>
    /// 左
    /// </summary>
    Left = 2,

    /// <summary>
    /// 右
    /// </summary>
    Right = 3,

    /// <summary>
    /// 前
    /// </summary>
    Forward = 4,

    /// <summary>
    /// 后
    /// </summary>
    Backward = 5
}
```

### 功能说明

#### A*算法特性

**核心功能**
- **启发式搜索**：使用启发式函数优化搜索效率
- **路径优化**：自动寻找最短路径
- **多维支持**：支持2D和3D寻路
- **自定义估值**：支持自定义距离和方向估值函数
- **方向控制**：可设置允许的移动方向

**算法优势**
1. **效率高**：启发式搜索大幅提升效率
2. **准确性**：保证找到最优路径
3. **灵活性**：支持多种地图类型和移动规则
4. **可扩展**：易于扩展新的寻路需求

#### 网格地图特性

**地图管理**
- **多尺寸支持**：支持不同尺寸的地图
- **数据管理**：支持一维、二维、三维数据
- **网格化**：将连续空间离散化为网格
- **路径验证**：提供路径有效性检查

**功能特性**
1. **直通检测**：检测两点间是否可直接到达
2. **路径优化**：自动清除不必要的拐点
3. **数据查询**：快速查询任意位置的数据
4. **尺寸管理**：灵活管理地图和网格尺寸

### 使用示例

#### 基本A*寻路
```csharp
// 创建A*算法实例
var aStarAlg = new AStarAlg();

// 初始化地图尺寸
aStarAlg.InitMapSize(100, 100);

// 设置地图数据（0表示可通行，1表示障碍）
int[] mapData = new int[100 * 100];
// 设置障碍物
for (int i = 0; i < 100; i++)
{
    mapData[i * 100 + 50] = 1; // 中间一列障碍
}
aStarAlg.SetData(mapData);

// 设置允许的移动方向（8方向）
aStarAlg.SetAllowedDirections(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 });

// 执行寻路
Position[] path = aStarAlg.Search(0, 0, 99, 99);

// 输出路径
Console.WriteLine($"路径长度: {path.Length}");
foreach (var pos in path)
{
    Console.WriteLine($"路径点: {pos}");
}
```

#### 网格地图寻路
```csharp
// 创建网格地图
var gridMap = new AStarGridMap();

// 初始化地图
gridMap.InitGridMap(new Size(100, 100), new Size(10, 10));

// 设置地图数据
int[][] mapData = new int[100][];
for (int i = 0; i < 100; i++)
{
    mapData[i] = new int[100];
    for (int j = 0; j < 100; j++)
    {
        mapData[i][j] = (i + j) % 2; // 棋盘式障碍
    }
}
gridMap.SetMapData(mapData);

// 创建起始和结束位置
var startPos = Positions.NewPosition(0, 0);
var endPos = Positions.NewPosition(99, 99);

// 执行寻路
Position[] path = gridMap.SearchPath(startPos, endPos);

// 检查路径有效性
bool isValid = gridMap.CheckPath(path);
Console.WriteLine($"路径有效: {isValid}");

// 检查是否直通
bool canLineTo = gridMap.CanLineTo(startPos, endPos);
Console.WriteLine($"是否直通: {canLineTo}");
```

#### 自定义估值函数
```csharp
// 创建A*算法实例
var aStarAlg = new AStarAlg();

// 设置自定义距离估值函数（曼哈顿距离）
aStarAlg.SetCustomFuncHn((current, target) => {
    return Math.Abs(current.X - target.X) + Math.Abs(current.Y - target.Y) + Math.Abs(current.Z - target.Z);
});

// 设置自定义方向估值函数
aStarAlg.SetCustomFuncDn((current, target) => {
    // 根据地形类型调整方向权重
    int terrainCost = GetTerrainCost(current);
    return terrainCost;
});

// 执行寻路
Position[] path = aStarAlg.SearchPosition(
    Positions.NewPosition(0, 0, 0),
    Positions.NewPosition(10, 10, 0)
);
```

#### 3D寻路
```csharp
// 创建3D A*算法
var aStarAlg = new AStarAlg();

// 初始化3D地图
aStarAlg.InitMapSize(50, 50, 10);

// 设置3D地图数据
int[][][] mapData = new int[10][][];
for (int z = 0; z < 10; z++)
{
    mapData[z] = new int[50][];
    for (int y = 0; y < 50; y++)
    {
        mapData[z][y] = new int[50];
        for (int x = 0; x < 50; x++)
        {
            mapData[z][y][x] = (x + y + z) % 3 == 0 ? 1 : 0; // 3D障碍物
        }
    }
}
aStarAlg.SetData(mapData);

// 执行3D寻路
Position[] path = aStarAlg.Search(0, 0, 0, 49, 49, 9);

Console.WriteLine($"3D路径长度: {path.Length}");
foreach (var pos in path)
{
    Console.WriteLine($"3D路径点: {pos}");
}
```

#### 路径优化
```csharp
// 创建网格地图
var gridMap = new AStarGridMap();

// 初始化并设置地图数据
gridMap.InitGridMap(new Size(100, 100));
// ... 设置地图数据 ...

// 寻路（保留拐点）
Position[] pathWithTurns = gridMap.SearchPath(
    Positions.NewPosition(0, 0),
    Positions.NewPosition(99, 99),
    true
);

// 寻路（清除拐点）
Position[] optimizedPath = gridMap.SearchPath(
    Positions.NewPosition(0, 0),
    Positions.NewPosition(99, 99),
    false
);

Console.WriteLine($"原始路径长度: {pathWithTurns.Length}");
Console.WriteLine($"优化路径长度: {optimizedPath.Length}");
```

### 设计特点

1. **算法优化**：使用启发式搜索提高效率
2. **多维支持**：支持2D和3D寻路
3. **灵活配置**：支持自定义估值函数和移动方向
4. **路径优化**：自动优化路径，减少拐点
5. **性能优化**：使用结构体和MethodImpl特性
6. **易于使用**：简洁的API设计

### 注意事项

1. **地图初始化**：使用前必须正确初始化地图尺寸
2. **数据一致性**：地图数据长度必须与初始化尺寸一致
3. **性能考虑**：大地图寻路可能耗时较长
4. **内存使用**：3D地图会占用较多内存
5. **估值函数**：自定义估值函数影响寻路效率和准确性 