# Mathx API 文档

## 命名空间: JLGames.Infra.Mathx

### 结构体 (Structs)

#### Point2Int
2D整型点结构体

```csharp
/// <summary>
/// 二维点（整型）
/// </summary>
public struct Point2Int : IEquatable<Point2Int>
{
    public int X;
    public int Y;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    public Point2Int(int x, int y);

    /// <summary>
    /// 设置坐标值
    /// </summary>
    /// <param name="newX">新的X坐标</param>
    /// <param name="newY">新的Y坐标</param>
    public void Set(int newX, int newY);

    /// <summary>
    /// 索引器，通过索引访问X或Y坐标
    /// </summary>
    /// <param name="index">索引：0=X，1=Y</param>
    /// <returns>坐标值</returns>
    public int this[int index] { get; set; }

    /// <summary>
    /// 零向量
    /// </summary>
    public static readonly Point2Int Zero;

    /// <summary>
    /// 加法运算符
    /// </summary>
    public static Point2Int operator +(Point2Int b, Point2Int c);

    /// <summary>
    /// 减法运算符
    /// </summary>
    public static Point2Int operator -(Point2Int b, Point2Int c);

    /// <summary>
    /// 相等比较运算符
    /// </summary>
    public static bool operator ==(Point2Int b, Point2Int c);

    /// <summary>
    /// 不等比较运算符
    /// </summary>
    public static bool operator !=(Point2Int b, Point2Int c);

    /// <summary>
    /// 隐式转换为Point3Int
    /// </summary>
    public static implicit operator Point3Int(Point2Int v);
}
```

#### Point3Int
3D整型点结构体

```csharp
/// <summary>
/// 三维点（整型）
/// </summary>
public struct Point3Int : IEquatable<Point3Int>
{
    public int X;
    public int Y;
    public int Z;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <param name="z">Z坐标</param>
    public Point3Int(int x, int y, int z);

    /// <summary>
    /// 设置坐标值
    /// </summary>
    /// <param name="newX">新的X坐标</param>
    /// <param name="newY">新的Y坐标</param>
    /// <param name="newZ">新的Z坐标</param>
    public void Set(int newX, int newY, int newZ);

    /// <summary>
    /// 零向量
    /// </summary>
    public static readonly Point3Int Zero;
}
```

#### Bounds2Int
2D整型边界结构体

```csharp
/// <summary>
/// 二维边界（整型）
/// </summary>
public struct Bounds2Int : IEquatable<Bounds2Int>
{
    private Point2Int m_Min;
    private Point2Int m_Max;

    /// <summary>
    /// 最小X坐标
    /// </summary>
    public int XMin { get; set; }

    /// <summary>
    /// 最小Y坐标
    /// </summary>
    public int YMin { get; set; }

    /// <summary>
    /// 最大X坐标
    /// </summary>
    public int XMax { get; set; }

    /// <summary>
    /// 最大Y坐标
    /// </summary>
    public int YMax { get; set; }

    /// <summary>
    /// X方向大小
    /// </summary>
    public int XSize { get; set; }

    /// <summary>
    /// Y方向大小
    /// </summary>
    public int YSize { get; set; }

    /// <summary>
    /// 最小点
    /// </summary>
    public Point2Int Min { get; set; }

    /// <summary>
    /// 最大点
    /// </summary>
    public Point2Int Max { get; set; }

    /// <summary>
    /// X方向中心点
    /// </summary>
    public int XCenter { get; }

    /// <summary>
    /// Y方向中心点
    /// </summary>
    public int YCenter { get; }

    /// <summary>
    /// 中心点
    /// </summary>
    public Point2Int Center { get; }

    /// <summary>
    /// 大小
    /// </summary>
    public Point2Int Size { get; }

    /// <summary>
    /// 面积
    /// </summary>
    public int Area { get; }

    /// <summary>
    /// 是否为空
    /// </summary>
    public bool IsNone { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="min">最小点</param>
    /// <param name="max">最大点</param>
    public Bounds2Int(Point2Int min, Point2Int max);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="xMin">最小X坐标</param>
    /// <param name="yMin">最小Y坐标</param>
    /// <param name="xMax">最大X坐标</param>
    /// <param name="yMax">最大Y坐标</param>
    public Bounds2Int(int xMin, int yMin, int xMax, int yMax);

    /// <summary>
    /// 设置边界
    /// </summary>
    /// <param name="min">最小点</param>
    /// <param name="max">最大点</param>
    public void Set(Point2Int min, Point2Int max);

    /// <summary>
    /// 检查是否包含指定点
    /// </summary>
    /// <param name="point">要检查的点</param>
    /// <returns>是否包含</returns>
    public bool Contains(Point2Int point);

    /// <summary>
    /// 检查是否包含指定坐标
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <returns>是否包含</returns>
    public bool Contains(int x, int y);

    /// <summary>
    /// 检查是否包含指定X坐标
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <returns>是否包含</returns>
    public bool ContainsX(int x);

    /// <summary>
    /// 检查是否包含指定Y坐标
    /// </summary>
    /// <param name="y">Y坐标</param>
    /// <returns>是否包含</returns>
    public bool ContainsY(int y);

    /// <summary>
    /// Check if two bounds intersect
    /// 判断两个范围是否相交
    /// </summary>
    /// <param name="bounds2Int">要检查的边界</param>
    /// <returns>是否相交</returns>
    public bool Intersect(Bounds2Int bounds2Int);

    /// <summary>
    /// Convert to point array
    /// 转为点数组
    /// </summary>
    /// <returns>包含所有点的数组</returns>
    public Point2Int[] ToArray();

    /// <summary>
    /// 移动边界
    /// </summary>
    /// <param name="offset">偏移量</param>
    /// <returns>移动后的边界</returns>
    public Bounds2Int Move(Point2Int offset);

    /// <summary>
    /// 裁剪边界
    /// </summary>
    /// <param name="subCrop">裁剪边界</param>
    /// <returns>裁剪后的边界</returns>
    public Bounds2Int Crop(Bounds2Int subCrop);

    /// <summary>
    /// 裁剪边界
    /// </summary>
    /// <param name="min">最小点</param>
    /// <param name="max">最大点</param>
    /// <returns>裁剪后的边界</returns>
    public Bounds2Int Crop2(Point2Int min, Point2Int max);

    /// <summary>
    /// 裁剪边界
    /// </summary>
    /// <param name="minX">最小X坐标</param>
    /// <param name="minY">最小Y坐标</param>
    /// <param name="maxX">最大X坐标</param>
    /// <param name="maxY">最大Y坐标</param>
    /// <returns>裁剪后的边界</returns>
    public Bounds2Int Crop2(int minX, int minY, int maxX, int maxY);

    /// <summary>
    /// X方向裁剪
    /// </summary>
    /// <param name="minX">最小X坐标</param>
    /// <param name="maxX">最大X坐标</param>
    /// <returns>裁剪后的边界</returns>
    public Bounds2Int CropX(int minX, int maxX);

    /// <summary>
    /// Y方向裁剪
    /// </summary>
    /// <param name="minY">最小Y坐标</param>
    /// <param name="maxY">最大Y坐标</param>
    /// <returns>裁剪后的边界</returns>
    public Bounds2Int CropY(int minY, int maxY);

    /// <summary>
    /// Split with x
    /// X分割
    /// </summary>
    /// <param name="x">分割位置</param>
    /// <returns>分割后的边界数组</returns>
    public Bounds2Int[] SplitX(int x);

    /// <summary>
    /// Split with y
    /// Y分割
    /// </summary>
    /// <param name="y">分割位置</param>
    /// <returns>分割后的边界数组</returns>
    public Bounds2Int[] SplitY(int y);

    /// <summary>
    /// 创建以中心点和大小的边界
    /// </summary>
    /// <param name="center">中心点</param>
    /// <param name="size">大小</param>
    /// <returns>新的边界</returns>
    public static Bounds2Int NewCenterBound(Point2Int center, Point2Int size);

    /// <summary>
    /// 创建以中心点和大小的边界
    /// </summary>
    /// <param name="centerX">中心X坐标</param>
    /// <param name="centerY">中心Y坐标</param>
    /// <param name="sizeX">X方向大小</param>
    /// <param name="sizeY">Y方向大小</param>
    /// <returns>新的边界</returns>
    public static Bounds2Int NewCenterBound(int centerX, int centerY, int sizeX, int sizeY);

    /// <summary>
    /// 空边界
    /// </summary>
    public static readonly Bounds2Int Empty;
}
```

#### Array2D
二维数组结构体

```csharp
/// <summary>
/// 二维数组
/// 提供二维数组的基本操作
/// </summary>
public struct Array2D<T>
{
    /// <summary>
    /// 宽度
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// 高度
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// 数组数据
    /// </summary>
    public T[] Data { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    public Array2D(int width, int height);

    /// <summary>
    /// 索引器
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <returns>元素值</returns>
    public T this[int x, int y] { get; set; }

    /// <summary>
    /// 检查坐标是否有效
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <returns>是否有效</returns>
    public bool IsValid(int x, int y);

    /// <summary>
    /// 获取线性索引
    /// </summary>
    /// <param name="x">X坐标</param>
    /// <param name="y">Y坐标</param>
    /// <returns>线性索引</returns>
    public int GetIndex(int x, int y);
}
```

#### MathUtil
数学工具类

```csharp
/// <summary>
/// 数学工具类
/// 提供常用的数学计算功能
/// </summary>
public static class MathUtil
{
    /// <summary>
    /// 计算两点间距离
    /// </summary>
    /// <param name="p1">点1</param>
    /// <param name="p2">点2</param>
    /// <returns>距离</returns>
    public static float Distance(Point2Int p1, Point2Int p2);

    /// <summary>
    /// 计算两点间距离的平方
    /// </summary>
    /// <param name="p1">点1</param>
    /// <param name="p2">点2</param>
    /// <returns>距离的平方</returns>
    public static float DistanceSquared(Point2Int p1, Point2Int p2);

    /// <summary>
    /// 计算曼哈顿距离
    /// </summary>
    /// <param name="p1">点1</param>
    /// <param name="p2">点2</param>
    /// <returns>曼哈顿距离</returns>
    public static int ManhattanDistance(Point2Int p1, Point2Int p2);

    /// <summary>
    /// 计算切比雪夫距离
    /// </summary>
    /// <param name="p1">点1</param>
    /// <param name="p2">点2</param>
    /// <returns>切比雪夫距离</returns>
    public static int ChebyshevDistance(Point2Int p1, Point2Int p2);

    /// <summary>
    /// 线性插值
    /// </summary>
    /// <param name="a">起始值</param>
    /// <param name="b">结束值</param>
    /// <param name="t">插值因子</param>
    /// <returns>插值结果</returns>
    public static float Lerp(float a, float b, float t);

    /// <summary>
    /// 限制值在指定范围内
    /// </summary>
    /// <param name="value">要限制的值</param>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <returns>限制后的值</returns>
    public static int Clamp(int value, int min, int max);

    /// <summary>
    /// 限制值在指定范围内
    /// </summary>
    /// <param name="value">要限制的值</param>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <returns>限制后的值</returns>
    public static float Clamp(float value, float min, float max);
}
```

### 功能说明

#### 点结构体特性

**Point2Int**
- **坐标访问**：支持X、Y坐标的直接访问
- **索引器**：通过索引访问坐标（0=X，1=Y）
- **运算符重载**：支持加法、减法、相等比较
- **类型转换**：支持隐式转换为Point3Int
- **性能优化**：使用MethodImpl特性优化性能

**Point3Int**
- **三维坐标**：支持X、Y、Z三个坐标
- **扩展性**：从Point2Int扩展而来
- **一致性**：与Point2Int保持相同的接口设计

#### 边界结构体特性

**Bounds2Int**
- **边界表示**：通过最小点和最大点表示矩形边界
- **属性计算**：自动计算中心点、大小、面积等属性
- **包含检测**：支持点包含检测和边界相交检测
- **边界操作**：支持移动、裁剪、分割等操作
- **点数组转换**：可将边界转换为包含所有点的数组

#### 二维数组特性

**Array2D<T>**
- **泛型支持**：支持任意类型的二维数组
- **线性存储**：使用一维数组存储二维数据
- **索引访问**：支持[x,y]形式的索引访问
- **边界检查**：提供坐标有效性检查
- **内存效率**：比嵌套数组更高效

#### 数学工具特性

**MathUtil**
- **距离计算**：支持欧几里得、曼哈顿、切比雪夫距离
- **插值计算**：提供线性插值功能
- **值限制**：提供值范围限制功能
- **性能优化**：使用高效的数学算法

### 使用示例

#### 点操作
```csharp
// 创建点
var point1 = new Point2Int(10, 20);
var point2 = new Point2Int(5, 15);

// 基本操作
Console.WriteLine($"点1: {point1}"); // {X=10,Y=20}
Console.WriteLine($"点2: {point2}"); // {X=5,Y=15}

// 算术运算
var sum = point1 + point2; // {X=15,Y=35}
var diff = point1 - point2; // {X=5,Y=5}

// 比较操作
bool isEqual = point1 == point2; // false
bool isNotEqual = point1 != point2; // true

// 索引访问
int x = point1[0]; // 10
int y = point1[1]; // 20

// 设置值
point1.Set(30, 40);
Console.WriteLine($"修改后: {point1}"); // {X=30,Y=40}
```

#### 边界操作
```csharp
// 创建边界
var bounds = new Bounds2Int(0, 0, 100, 100);
Console.WriteLine($"边界: {bounds}"); // {Min={X=0,Y=0},Max={X=100,Y=100},Center={X=50,Y=50},Size={X=100,Y=100},Area=10000}

// 属性访问
Console.WriteLine($"中心点: {bounds.Center}"); // {X=50,Y=50}
Console.WriteLine($"大小: {bounds.Size}"); // {X=100,Y=100}
Console.WriteLine($"面积: {bounds.Area}"); // 10000

// 包含检测
var point = new Point2Int(25, 25);
bool contains = bounds.Contains(point); // true
bool containsX = bounds.ContainsX(25); // true
bool containsY = bounds.ContainsY(25); // true

// 边界操作
var movedBounds = bounds.Move(new Point2Int(10, 10));
Console.WriteLine($"移动后: {movedBounds}"); // {Min={X=10,Y=10},Max={X=110,Y=110},...}

var croppedBounds = bounds.Crop(new Bounds2Int(25, 25, 75, 75));
Console.WriteLine($"裁剪后: {croppedBounds}"); // {Min={X=25,Y=25},Max={X=75,Y=75},...}

// 分割操作
var splitBounds = bounds.SplitX(50);
Console.WriteLine($"X分割: {splitBounds.Length} 个边界"); // 2

// 创建中心边界
var centerBounds = Bounds2Int.NewCenterBound(new Point2Int(50, 50), new Point2Int(20, 20));
Console.WriteLine($"中心边界: {centerBounds}"); // {Min={X=40,Y=40},Max={X=60,Y=60},...}
```

#### 二维数组操作
```csharp
// 创建二维数组
var array = new Array2D<int>(5, 5);

// 设置值
array[2, 3] = 42;
array[1, 1] = 10;

// 获取值
int value = array[2, 3]; // 42

// 检查坐标有效性
bool isValid = array.IsValid(2, 3); // true
bool isInvalid = array.IsValid(10, 10); // false

// 遍历数组
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

#### 数学计算
```csharp
// 距离计算
var p1 = new Point2Int(0, 0);
var p2 = new Point2Int(3, 4);

float distance = MathUtil.Distance(p1, p2); // 5.0
float distanceSquared = MathUtil.DistanceSquared(p1, p2); // 25.0
int manhattanDistance = MathUtil.ManhattanDistance(p1, p2); // 7
int chebyshevDistance = MathUtil.ChebyshevDistance(p1, p2); // 4

// 插值计算
float interpolated = MathUtil.Lerp(0.0f, 100.0f, 0.5f); // 50.0

// 值限制
int clamped = MathUtil.Clamp(150, 0, 100); // 100
float clampedFloat = MathUtil.Clamp(75.5f, 0.0f, 100.0f); // 75.5
```

#### 复杂操作组合
```csharp
// 创建游戏地图边界
var mapBounds = new Bounds2Int(0, 0, 1000, 1000);

// 创建玩家位置
var playerPos = new Point2Int(500, 500);

// 创建视野范围
var viewBounds = Bounds2Int.NewCenterBound(playerPos, new Point2Int(100, 100));

// 检查视野是否在地图内
var visibleArea = mapBounds.Crop(viewBounds);

// 获取视野内的所有点
var visiblePoints = visibleArea.ToArray();

// 计算到地图边界的距离
var distanceToEdge = MathUtil.Distance(playerPos, mapBounds.Center);

Console.WriteLine($"视野内点数: {visiblePoints.Length}");
Console.WriteLine($"到地图中心距离: {distanceToEdge}");
```

### 设计特点

1. **性能优化**：使用结构体和MethodImpl特性优化性能
2. **类型安全**：强类型设计，避免类型错误
3. **操作符重载**：支持直观的数学运算
4. **内存效率**：结构体避免堆分配，提高性能
5. **功能完整**：提供常用的数学和几何操作
6. **易于使用**：简洁的API设计

### 注意事项

1. **值类型**：所有结构体都是值类型，传递时会被复制
2. **边界检查**：使用前应检查坐标的有效性
3. **性能考虑**：大量计算时注意避免不必要的结构体复制
4. **精度问题**：整型计算避免浮点数精度问题
5. **内存使用**：ToArray()方法可能产生大量内存分配 