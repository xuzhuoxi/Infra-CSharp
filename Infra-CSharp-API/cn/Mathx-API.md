# Mathx API 文档

## 命名空间: JLGames.Infra.Mathx

几何点、线段、区间、二维边界、位掩码与常用数学工具。

### 结构体 (Structs)

#### Point1
一维点（`double`）

```csharp
/// <summary>
/// 一维点
/// </summary>
public struct Point1
{
    /// <summary>
    /// 区间点
    /// </summary>
    public struct AreaPoint1
    {
        /// <summary>
        /// 区间索引
        /// </summary>
        public long AreaIndex;

        /// <summary>
        /// 区间余量
        /// </summary>
        public double Remainder;
    }

    /// <summary>
    /// 一维坐标值。
    /// </summary>
    public double Value;

    /// <summary>
    /// 使用指定值创建一维点。
    /// </summary>
    /// <param name="value">坐标值。</param>
    public Point1(double value);

    /// <summary>
    /// 从 double 到 Point1 的隐式转换。
    /// </summary>
    public static implicit operator Point1(double point);

    /// <summary>
    /// 从 Point1 到 double 的隐式转换。
    /// </summary>
    public static implicit operator double(Point1 point);

    /// <summary>
    /// 转换为区间点
    /// </summary>
    public static AreaPoint1 ToAreaPoint1(double point1, ulong size);

    /// <summary>
    /// 转换为一维点
    /// </summary>
    public static Point1 FromAreaPoint1(AreaPoint1 point1, ulong size);
}
```

#### Point1Int
一维点（整型）

```csharp
/// <summary>
/// 一维点(整型)
/// </summary>
public struct Point1Int
{
    /// <summary>
    /// 区间点(整型)
    /// </summary>
    public struct AreaPoint1Int
    {
        /// <summary>
        /// 区间索引
        /// </summary>
        public int AreaIndex;

        /// <summary>
        /// 区间余量
        /// </summary>
        public int Remainder;
    }

    /// <summary>
    /// 一维坐标值（整型）。
    /// </summary>
    public int Value;

    /// <summary>
    /// 使用指定值创建整型一维点。
    /// </summary>
    /// <param name="value">坐标值。</param>
    public Point1Int(int value);

    /// <summary>
    /// 从 int 到 Point1Int 的隐式转换。
    /// </summary>
    public static implicit operator Point1Int(int point);

    /// <summary>
    /// 从 Point1Int 到 int 的隐式转换。
    /// </summary>
    public static implicit operator int(Point1Int point);

    /// <summary>
    /// 转换为区间点
    /// </summary>
    public static AreaPoint1Int ToAreaPoint1(int point1, uint size);

    /// <summary>
    /// 转换为一维点
    /// </summary>
    public static Point1Int FromAreaPoint1(AreaPoint1Int point1, uint size);
}
```

#### Point2Int
二维整型点

```csharp
/// <summary>
/// 二维点（整型）
/// </summary>
public struct Point2Int : IEquatable<Point2Int>
{
    /// <summary>
    /// X 坐标。
    /// </summary>
    public int X;

    /// <summary>
    /// Y 坐标。
    /// </summary>
    public int Y;

    /// <summary>
    /// 按索引访问分量：0 为 X，1 为 Y。
    /// </summary>
    /// <param name="index">分量索引（0 或 1）。</param>
    /// <exception cref="IndexOutOfRangeException">索引不是 0 或 1。</exception>
    public int this[int index] { get; set; }

    /// <summary>
    /// 创建二维整型点。
    /// </summary>
    /// <param name="x">X 坐标。</param>
    /// <param name="y">Y 坐标。</param>
    public Point2Int(int x, int y);

    /// <summary>
    /// 设置 X、Y 坐标。
    /// </summary>
    public void Set(int newX, int newY);

    /// <summary>
    /// 原点 (0, 0)。
    /// </summary>
    public static readonly Point2Int Zero;

    /// <summary>
    /// 分量相加。
    /// </summary>
    public static Point2Int operator +(Point2Int b, Point2Int c);

    /// <summary>
    /// 分量相减。
    /// </summary>
    public static Point2Int operator -(Point2Int b, Point2Int c);

    /// <summary>
    /// 不等比较。
    /// </summary>
    public static bool operator !=(Point2Int b, Point2Int c);

    /// <summary>
    /// 相等比较。
    /// </summary>
    public static bool operator ==(Point2Int b, Point2Int c);

    /// <summary>
    /// 提升为 Point3Int（Z 为 0）。
    /// </summary>
    public static implicit operator Point3Int(Point2Int v);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Point2Int other);
}
```

#### Point3Int
三维整型点

```csharp
/// <summary>
/// 三维点（整型）
/// </summary>
public struct Point3Int : IEquatable<Point3Int>
{
    /// <summary>
    /// X 坐标。
    /// </summary>
    public int X;

    /// <summary>
    /// Y 坐标。
    /// </summary>
    public int Y;

    /// <summary>
    /// Z 坐标。
    /// </summary>
    public int Z;

    /// <summary>
    /// 按索引访问分量：0 为 X，1 为 Y，2 为 Z。
    /// </summary>
    /// <param name="index">分量索引（0、1 或 2）。</param>
    /// <exception cref="IndexOutOfRangeException">索引不是 0、1 或 2。</exception>
    public int this[int index] { get; set; }

    /// <summary>
    /// 创建三维整型点。
    /// </summary>
    public Point3Int(int x, int y, int z);

    /// <summary>
    /// 设置 X、Y、Z 坐标。
    /// </summary>
    public void Set(int newX, int newY, int newZ);

    /// <summary>
    /// 分量相加。
    /// </summary>
    public static Point3Int operator +(Point3Int b, Point3Int c);

    /// <summary>
    /// 分量相减。
    /// </summary>
    public static Point3Int operator -(Point3Int b, Point3Int c);

    /// <summary>
    /// 不等比较。
    /// </summary>
    public static bool operator !=(Point3Int b, Point3Int c);

    /// <summary>
    /// 相等比较。
    /// </summary>
    public static bool operator ==(Point3Int b, Point3Int c);

    /// <summary>
    /// 丢弃 Z，将 XY 分量转为 Point2Int。
    /// </summary>
    public static implicit operator Point2Int(Point3Int v);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Point3Int other);
}
```

#### Bounds2Int
轴对齐二维整型边界 `[XMin, XMax) × [YMin, YMax)`（半开区间）

```csharp
/// <summary>
/// 轴对齐二维整型边界 [XMin, XMax) × [YMin, YMax)（半开区间）。
/// </summary>
public struct Bounds2Int : IEquatable<Bounds2Int>
{
    /// <summary>
    /// 最小 X（含）。
    /// </summary>
    public int XMin { get; set; }

    /// <summary>
    /// 最小 Y（含）。
    /// </summary>
    public int YMin { get; set; }

    /// <summary>
    /// 最大 X（不含）。
    /// </summary>
    public int XMax { get; set; }

    /// <summary>
    /// 最大 Y（不含）。
    /// </summary>
    public int YMax { get; set; }

    /// <summary>
    /// 宽度（XMax − XMin）。
    /// </summary>
    public int XSize { get; set; }

    /// <summary>
    /// 高度（YMax − YMin）。
    /// </summary>
    public int YSize { get; set; }

    /// <summary>
    /// 最小角点（含）。
    /// </summary>
    public Point2Int Min { get; set; }

    /// <summary>
    /// 最大角点（不含）。
    /// </summary>
    public Point2Int Max { get; set; }

    /// <summary>
    /// 中心 X 坐标。
    /// </summary>
    public int XCenter { get; }

    /// <summary>
    /// 中心 Y 坐标。
    /// </summary>
    public int YCenter { get; }

    /// <summary>
    /// 中心点。
    /// </summary>
    public Point2Int Center { get; }

    /// <summary>
    /// 尺寸 (XSize, YSize)。
    /// </summary>
    public Point2Int Size { get; }

    /// <summary>
    /// 单元格数量（XSize × YSize）。
    /// </summary>
    public int Area { get; }

    /// <summary>
    /// 面积是否为 0。
    /// </summary>
    public bool IsNone { get; }

    /// <summary>
    /// 由最小、最大角点创建边界。
    /// </summary>
    public Bounds2Int(Point2Int min, Point2Int max);

    /// <summary>
    /// 由各轴上下界创建边界。
    /// </summary>
    public Bounds2Int(int xMin, int yMin, int xMax, int yMax);

    /// <summary>
    /// 重新设置最小、最大角点。
    /// </summary>
    public void Set(Point2Int min, Point2Int max);

    /// <summary>
    /// 点是否在本边界内（半开）。
    /// </summary>
    public bool Contains(Point2Int point);

    /// <summary>
    /// 坐标 (x, y) 是否在本边界内（半开）。
    /// </summary>
    public bool Contains(int x, int y);

    /// <summary>
    /// x 是否在 [XMin, XMax) 内。
    /// </summary>
    public bool ContainsX(int x);

    /// <summary>
    /// y 是否在 [YMin, YMax) 内。
    /// </summary>
    public bool ContainsY(int y);

    /// <summary>
    /// 判断两个范围是否相交
    /// </summary>
    /// <param name="bounds2Int">另一边界。</param>
    /// <returns>若对方任一角点在本边界内则为 true。</returns>
    public bool Intersect(Bounds2Int bounds2Int);

    /// <summary>
    /// 转为点数组
    /// </summary>
    /// <returns>边界内全部整型点；空边界返回 null。</returns>
    public Point2Int[] ToArray();

    /// <summary>
    /// 返回平移 offset 后的副本。
    /// </summary>
    public Bounds2Int Move(Point2Int offset);

    /// <summary>
    /// 与另一边界的交集（角点形式）。
    /// </summary>
    public Bounds2Int Crop(Bounds2Int subCrop);

    /// <summary>
    /// 与轴对齐矩形 [min, max) 的交集。
    /// </summary>
    public Bounds2Int Crop2(Point2Int min, Point2Int max);

    /// <summary>
    /// 与轴对齐矩形 [minX, maxX) × [minY, maxY) 的交集。
    /// </summary>
    public Bounds2Int Crop2(int minX, int minY, int maxX, int maxY);

    /// <summary>
    /// 仅沿 X 轴裁剪的交集。
    /// </summary>
    public Bounds2Int CropX(int minX, int maxX);

    /// <summary>
    /// 仅沿 Y 轴裁剪的交集。
    /// </summary>
    public Bounds2Int CropY(int minY, int maxY);

    /// <summary>
    /// X分割
    /// </summary>
    /// <param name="x">分割线（须落在 X 范围内才会切分）。</param>
    /// <returns>一个或两个子边界。</returns>
    public Bounds2Int[] SplitX(int x);

    /// <summary>
    /// Y分割
    /// </summary>
    /// <param name="y">分割线（须落在 Y 范围内才会切分）。</param>
    /// <returns>一个或两个子边界。</returns>
    public Bounds2Int[] SplitY(int y);

    /// <summary>
    /// 合并本边界与其它边界的格点（去重）。
    /// </summary>
    public Point2Int[] Add(Bounds2Int add, params Bounds2Int[] other);

    /// <summary>
    /// 本边界格点减去 sub 及其它边界中的格点。
    /// </summary>
    public Point2Int[] Sub(Bounds2Int sub, params Bounds2Int[] other);

    /// <summary>
    /// 不等比较。
    /// </summary>
    public static bool operator !=(Bounds2Int b, Bounds2Int c);

    /// <summary>
    /// 相等比较。
    /// </summary>
    public static bool operator ==(Bounds2Int b, Bounds2Int c);

    /// <summary>
    /// 以中心为基准缩放边界（各轴向上取整）。
    /// </summary>
    public static Bounds2Int operator /(Bounds2Int b, float c);

    /// <summary>
    /// 以中心为基准缩放边界（各轴向上取整）。
    /// </summary>
    public static Bounds2Int operator *(Bounds2Int b, float c);

    /// <summary>
    /// 以 center 为中心、给定尺寸构造半开边界。
    /// </summary>
    public static Bounds2Int NewCenterBound(Point2Int center, Point2Int size);

    /// <summary>
    /// 以 (centerX, centerY) 为中心、给定尺寸构造半开边界。
    /// </summary>
    public static Bounds2Int NewCenterBound(int centerX, int centerY, int sizeX, int sizeY);

    /// <summary>
    /// 空边界（面积为 0）。
    /// </summary>
    public static readonly Bounds2Int Empty;

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Bounds2Int other);
}
```

#### Line1
一维线段（浮点端点）

```csharp
/// <summary>
/// 一维线段（浮点端点）。
/// </summary>
public struct Line1 : IEquatable<Line1>
{
    /// <summary>
    /// 起点。
    /// </summary>
    public double Start;

    /// <summary>
    /// 终点。
    /// </summary>
    public double End;

    /// <summary>
    /// Start 与 End 中的较小值。
    /// </summary>
    public double Min { get; }

    /// <summary>
    /// Start 与 End 中的较大值。
    /// </summary>
    public double Max { get; }

    /// <summary>
    /// 有符号长度（End − Start）。
    /// </summary>
    public double Size { get; }

    /// <summary>
    /// 长度绝对值。
    /// </summary>
    public double AbsSize { get; }

    /// <summary>
    /// 起终点是否重合（浮点容差内）。
    /// </summary>
    public bool IsPoint { get; }

    /// <summary>
    /// 创建一维线段。
    /// </summary>
    public Line1(double start, double end);

    /// <summary>
    /// 按长度 size 分割本线段，对齐基准点为 0。
    /// </summary>
    /// <param name="size">分段长度（不可为 0）。</param>
    /// <returns>子线段数组；size 为 0 时返回 null。</returns>
    public Line1[] SliceAtZero(double size);

    /// <summary>
    /// 以 Start 为对齐基准点分割。
    /// </summary>
    public Line1[] SliceAtStart(double size);

    /// <summary>
    /// 以 End 为对齐基准点分割。
    /// </summary>
    public Line1[] SliceAtEnd(double size);

    /// <summary>
    /// 以 Min 为对齐基准点分割。
    /// </summary>
    public Line1[] SliceAtMin(double size);

    /// <summary>
    /// 以 Max 为对齐基准点分割。
    /// </summary>
    public Line1[] SliceAtMax(double size);

    /// <summary>
    /// 基于指定点分割为多条直线
    /// </summary>
    /// <param name="basisPoint">轴上的对齐基准坐标。</param>
    /// <param name="size">分段长度（不可为 0）。</param>
    /// <returns>子线段数组；size 为 0 时返回 null。</returns>
    public Line1[] SliceAt(double basisPoint, double size);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Line1 other);
}
```

#### Line1Int
一维线段（整型端点）

```csharp
/// <summary>
/// 一维线段（整型端点）。
/// </summary>
public struct Line1Int : IEquatable<Line1Int>
{
    /// <summary>
    /// 起点。
    /// </summary>
    public int Start;

    /// <summary>
    /// 终点。
    /// </summary>
    public int End;

    /// <summary>
    /// Start 与 End 中的较小值。
    /// </summary>
    public int Min { get; }

    /// <summary>
    /// Start 与 End 中的较大值。
    /// </summary>
    public int Max { get; }

    /// <summary>
    /// 有符号长度（End − Start）。
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// 长度绝对值。
    /// </summary>
    public int AbsSize { get; }

    /// <summary>
    /// 起终点是否重合。
    /// </summary>
    public bool IsPoint { get; }

    /// <summary>
    /// 创建整型一维线段。
    /// </summary>
    public Line1Int(int start, int end);

    /// <summary>
    /// 按长度 size 分割本线段，对齐基准点为 0。
    /// </summary>
    public Line1Int[] SliceAtZero(int size);

    /// <summary>
    /// 以 Start 为对齐基准点分割。
    /// </summary>
    public Line1Int[] SliceAtStart(int size);

    /// <summary>
    /// 以 End 为对齐基准点分割。
    /// </summary>
    public Line1Int[] SliceAtEnd(int size);

    /// <summary>
    /// 以 Min 为对齐基准点分割。
    /// </summary>
    public Line1Int[] SliceAtMin(int size);

    /// <summary>
    /// 以 Max 为对齐基准点分割。
    /// </summary>
    public Line1Int[] SliceAtMax(int size);

    /// <summary>
    /// 基于指定点分割为多条整型线段
    /// </summary>
    /// <param name="basisPoint">轴上的对齐基准坐标。</param>
    /// <param name="size">分段长度（不可为 0）。</param>
    /// <returns>子线段数组；size 为 0 时返回 null。</returns>
    public Line1Int[] SliceAt(int basisPoint, int size);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Line1Int other);
}
```

#### Line2Int
二维线段（整型端点）

```csharp
/// <summary>
/// 二维线段（整型端点）。
/// </summary>
public struct Line2Int : IEquatable<Line2Int>
{
    /// <summary>
    /// 起点。
    /// </summary>
    public Point2Int Start;

    /// <summary>
    /// 终点。
    /// </summary>
    public Point2Int End;

    /// <summary>
    /// 长度的平方（免开方）。
    /// </summary>
    public int SqrMagnitude { get; }

    /// <summary>
    /// 欧几里得长度。
    /// </summary>
    public double Magnitude { get; }

    /// <summary>
    /// 起终点是否重合。
    /// </summary>
    public bool IsPoint { get; }

    /// <summary>
    /// 创建二维整型线段。
    /// </summary>
    public Line2Int(Point2Int start, Point2Int end);

    public override string ToString();
    public override int GetHashCode();
    public override bool Equals(object obj);
    public bool Equals(Line2Int other);
}
```

#### Interval
闭区间浮点范围 `[Min, Max]`

```csharp
/// <summary>
/// 闭区间浮点范围 [Min, Max]。
/// </summary>
[Serializable]
public struct Interval
{
    /// <summary>
    /// 区间最小值。
    /// </summary>
    public float Min;

    /// <summary>
    /// 区间最大值。
    /// </summary>
    public float Max;

    /// <summary>
    /// 区间长度（含端点跨度：Max − Min）。
    /// </summary>
    public float Length { get; set; }

    /// <summary>
    /// 使用给定的 min、max 构造区间。
    /// </summary>
    public Interval(float min, float max);
}
```

#### IntervalInt
整型区间 `[Min, Max]`，可选择是否包含最大值

```csharp
/// <summary>
/// 整型区间 [Min, Max]，可选择是否包含最大值。
/// </summary>
[Serializable]
public struct IntervalInt
{
    /// <summary>
    /// 区间最小值。0 为第一位，1 为第二位，以此类推。
    /// </summary>
    public int Min;

    /// <summary>
    /// 区间最大值。
    /// </summary>
    public int Max;

    /// <summary>
    /// 计算 Length 时是否包含 Max。
    /// </summary>
    public bool MaxIncluded;

    /// <summary>
    /// 区间长度。
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 使用给定的 min、max、maxIncluded 构造区间。
    /// </summary>
    public IntervalInt(int min, int max, bool maxIncluded);
}
```

#### Range
半开浮点区间 `[Start, Start + Length)`

```csharp
/// <summary>
/// 半开浮点区间 [Start, Start + Length)。
/// </summary>
[Serializable]
public struct Range
{
    /// <summary>
    /// 区间起始索引。0 为第一位，1 为第二位，以此类推。
    /// </summary>
    public float Start;

    /// <summary>
    /// 区间长度。
    /// </summary>
    public float Length;

    /// <summary>
    /// 区间结束索引（不含）。
    /// </summary>
    public float End { get; set; }

    /// <summary>
    /// 使用给定的 start、length 构造区间。
    /// </summary>
    /// <param name="start">区间起始索引。</param>
    /// <param name="length">区间长度。</param>
    public Range(float start, float length);
}
```

#### RangeInt
半开整型区间 `[Start, Start + Length)`

```csharp
/// <summary>
/// 半开整型区间 [Start, Start + Length)。
/// </summary>
[Serializable]
public struct RangeInt
{
    /// <summary>
    /// 区间起始索引。0 为第一位，1 为第二位，以此类推。
    /// </summary>
    public int Start;

    /// <summary>
    /// 区间长度。
    /// </summary>
    public int Length;

    /// <summary>
    /// 区间结束索引（不含）。
    /// </summary>
    public int End { get; set; }

    /// <summary>
    /// 使用给定的 start、length 构造区间。
    /// </summary>
    /// <param name="start">区间起始索引。</param>
    /// <param name="length">区间长度。</param>
    public RangeInt(int start, int length);
}
```

### 类 (Classes)

#### Array2D&lt;T&gt;
二维数组包装类（锯齿数组 `T[][]`）

```csharp
/// <summary>
/// 二维数组包装类
/// </summary>
public sealed class Array2D<T>
{
    /// <summary>
    /// 创建空包装（使用前需调用 SetData）。
    /// </summary>
    public Array2D();

    /// <summary>
    /// 包装锯齿二维数组；根据首行宽度与行数推断尺寸。
    /// </summary>
    /// <param name="data">行优先锯齿数组。</param>
    public Array2D(T[][] data);

    /// <summary>
    /// 由扁平缓冲与行宽构建锯齿二维数组。
    /// </summary>
    /// <param name="data">扁平行优先缓冲。</param>
    /// <param name="width">每行单元数。</param>
    public Array2D(T[] data, int width);

    /// <summary>
    /// 设置数据
    /// </summary>
    public void SetData(T[][] data);

    /// <summary>
    /// 设置数据
    /// </summary>
    public void SetData(T[] data, int width);

    /// <summary>
    /// 尺寸
    /// </summary>
    public Point2Int Size { get; }

    /// <summary>
    /// 边界
    /// </summary>
    public Bounds2Int Bound { get; }

    /// <summary>
    /// 取值
    /// </summary>
    public T GetValue(int x, int y);

    /// <summary>
    /// 取附近值为 value 的点坐标
    /// </summary>
    /// <param name="max">最大搜索范围</param>
    public Point2Int? GetNearValue(int x, int y, T value, int max);

    /// <summary>
    /// 值检查
    /// </summary>
    public bool CheckValue(int x, int y, T value);

    /// <summary>
    /// 取全部数据
    /// </summary>
    public T[][] GetData();

    /// <summary>
    /// 取区域数据
    /// </summary>
    public T[][] GetDataAtBound(Bounds2Int bound);

    /// <summary>
    /// 取区域数据
    /// </summary>
    public T[][] GetDataAtBound(int xMin, int yMin, int xMax, int yMax);

    /// <summary>
    /// 将所有单元格格式化为逗号分隔的行（可选反转 Y 顺序）。
    /// </summary>
    /// <param name="reverse">为 true 时按行索引从大到小输出。</param>
    public string ToPrintString(bool reverse);

    public override string ToString();
}
```

#### BitFixedData
定长位存储器。三个概念：原始数据、位数据、业务数据。

- 原始数据 (`uint`)：存储数据的真实数据类型
- 位数据 (`bit:bool`)：原始数据的按位数据值 (0/1)
- 业务数据 (`value:uint`)：由多个位数据拼接起来组成的数据值

```csharp
/// <summary>
/// 位存储器（定长）
/// </summary>
public class BitFixedData
{
    /// <summary>
    /// 每个数据的占用位数量
    /// </summary>
    public int BitsPerValue { get; }

    /// <summary>
    /// 数据长度
    /// </summary>
    public int ValueLen { get; }

    /// <summary>
    /// 全部数据的有效位数量。BitLen = BitsPerValue * ValueLen
    /// </summary>
    public int BitLen { get; }

    /// <summary>
    /// 数据载体结构的存储长度
    /// </summary>
    public int RawDataLen { get; }

    /// <summary>
    /// 原始数据
    /// </summary>
    public uint[] RawData { get; }

    /// <summary>
    /// 构造一个位存储器（每位宽 1）。
    /// </summary>
    public BitFixedData(int valueLen);

    /// <summary>
    /// 构造一个位存储器
    /// </summary>
    /// <param name="bitsPerValue">小于 32</param>
    public BitFixedData(int bitsPerValue, int valueLen);

    /// <summary>
    /// 设置原始数据。不足补 0；超量截断。
    /// </summary>
    public void SetRawData(uint[] data);

    /// <summary>
    /// 取全部业务数据
    /// </summary>
    public uint[] GetValues();

    /// <summary>
    /// 取一个范围内的数据
    /// </summary>
    public uint[] GetValues(int valueIndex, int valueCount);

    /// <summary>
    /// 取单个业务数据
    /// </summary>
    public uint GetValue(int valueIndex);

    /// <summary>
    /// 设置单个业务数据
    /// </summary>
    public void SetValue(int valueIndex, uint value);

    /// <summary>
    /// 取全部位数的二进制字符串表示
    /// </summary>
    public string GetStringBits();

    /// <summary>
    /// 取全部位数据
    /// </summary>
    public bool[] GetBits();

    /// <summary>
    /// 取一个范围内的位数据
    /// </summary>
    public bool[] GetBits(int bitIndex, int bitCount);

    /// <summary>
    /// 取单个位数据
    /// </summary>
    public bool GetBit(int bitIndex);

    /// <summary>
    /// 设置单个位数据
    /// </summary>
    public void SetBit(int bitIndex, bool isTrue);

    /// <summary>
    /// 取指定比特范围的二进制字符串表示（可在业务值组之间插入分隔符）。
    /// </summary>
    /// <param name="bitIndex">起始位索引。</param>
    /// <param name="bitLen">位数。</param>
    /// <param name="valueSpace">组间分隔字符串。</param>
    /// <returns>二进制字符串。</returns>
    public string ToBitString(int bitIndex, int bitLen, string valueSpace = "");

    public override string ToString();
}
```

### 静态工具类

#### Point1Utils
`Point1` 区间索引相关的扩展方法

```csharp
/// <summary>
/// Point1 区间索引相关的扩展方法。
/// </summary>
public static class Point1Utils
{
    /// <summary>
    /// 转换为区间点
    /// </summary>
    public static Point1.AreaPoint1 ToAreaPoint1(this Point1 point1, ulong size);

    /// <summary>
    /// 转换为一维点
    /// </summary>
    public static Point1 FromAreaPoint1(this Point1.AreaPoint1 point1, ulong size);
}
```

#### Point1IntUtils
`Point1Int` 区间索引相关的扩展方法

```csharp
/// <summary>
/// Point1Int 区间索引相关的扩展方法。
/// </summary>
public static class Point1IntUtils
{
    /// <summary>
    /// 转换为区间点
    /// </summary>
    public static Point1Int.AreaPoint1Int ToAreaPoint1(this Point1Int point1, uint size);

    /// <summary>
    /// 转换为一维点
    /// </summary>
    public static Point1Int FromAreaPoint1(this Point1Int.AreaPoint1Int point1, uint size);
}
```

#### BitMark
为基本整型预计算的单比特与多比特掩码

```csharp
/// <summary>
/// 为基本整型预计算的单比特与多比特掩码。
/// </summary>
public static class BitMark
{
    /// <summary>
    /// 一个 byte 数据对应的位数
    /// </summary>
    public const int BitsPerByte = 8;

    /// <summary>
    /// 一个 ushort 数据对应的位数
    /// </summary>
    public const int BitsPerUshort = 16;

    /// <summary>
    /// 一个 int 数据对应的位数
    /// </summary>
    public const int BitsPerInt = 32;

    /// <summary>
    /// 一个 uint 数据对应的位数
    /// </summary>
    public const int BitsPerUint = 32;

    /// <summary>
    /// 一个 ulong 数据对应的位数
    /// </summary>
    public const int BitsPerUlong = 64;

    /// <summary>
    /// 返回从 markIndex 起连续 markLen 位的 byte 掩码。
    /// </summary>
    public static byte GetByteMark(int markIndex, int markLen);

    /// <summary>
    /// 返回 markIndex 处的单比特 byte 掩码。
    /// </summary>
    public static byte GetByteMark(int markIndex);

    /// <summary>
    /// 返回从 markIndex 起连续 markLen 位的 ushort 掩码。
    /// </summary>
    public static ushort GetUshortMark(int markIndex, int markLen);

    /// <summary>
    /// 返回 markIndex 处的单比特 ushort 掩码。
    /// </summary>
    public static ushort GetUshortMark(int markIndex);

    /// <summary>
    /// 返回从 markIndex 起连续 markLen 位的 int 掩码。
    /// </summary>
    public static int GetIntMark(int markIndex, int markLen);

    /// <summary>
    /// 返回 markIndex 处的单比特 int 掩码。
    /// </summary>
    public static int GetIntMark(int markIndex);

    /// <summary>
    /// 返回从 markIndex 起连续 markLen 位的 uint 掩码。
    /// </summary>
    public static uint GetUintMark(int markIndex, int markLen);

    /// <summary>
    /// 返回 markIndex 处的单比特 uint 掩码。
    /// </summary>
    public static uint GetUintMark(int markIndex);

    /// <summary>
    /// 返回从 markIndex 起连续 markLen 位的 ulong 掩码。
    /// </summary>
    public static ulong GetUlongMark(int markIndex, int markLen);

    /// <summary>
    /// 返回 markIndex 处的单比特 ulong 掩码。
    /// </summary>
    public static ulong GetUlongMark(int markIndex);
}
```

#### MathUtil
常用数学工具：钳制、奇偶、余数/取模、距离与浮点比较等

```csharp
/// <summary>
/// 常用数学工具：钳制、奇偶、余数/取模、距离与浮点比较等。
/// </summary>
public static class MathUtil
{
    /// <summary>
    /// 将给定值限制在 min 与 max 之间。若已在范围内则原样返回。
    /// </summary>
    public static float Clamp(float value, float min, float max);

    /// <summary>
    /// 将给定值限制在 min 与 max 之间。若已在范围内则原样返回。
    /// </summary>
    public static int Clamp(int value, int min, int max);

    /// <summary>
    /// 判断 val 是否严格位于 a 与 b 之间（不含端点；a、b 大小无关）。
    /// </summary>
    public static bool Between(double val, double a, double b);
    public static bool Between(float val, float a, float b);
    public static bool Between(int val, int a, int b);
    public static bool Between(long val, long a, long b);

    /// <summary>
    /// 将值限制在 0 与 1 之间并返回。
    /// </summary>
    public static float Clamp01(float value);

    /// <summary>
    /// 是否为奇数
    /// </summary>
    public static bool IsOdd(int num);

    /// <summary>
    /// 向下偶数
    /// </summary>
    public static int FloorToEven(float number);

    /// <summary>
    /// 向下奇数
    /// </summary>
    public static int FloorToOdd(float number);

    /// <summary>
    /// 向上偶数
    /// </summary>
    public static int CeilToEven(float number);

    /// <summary>
    /// 向上奇数
    /// </summary>
    public static int CeilToOdd(float number);

    /// <summary>
    /// 判断两个浮点数是否近似相等（差值小于机器精度）。
    /// </summary>
    /// <param name="epsilon">未使用，保留以兼容 API。</param>
    public static bool IsSimilar(float a, float b, float epsilon = float.Epsilon);
    public static bool IsSimilar(double a, double b, double epsilon = double.Epsilon);

    /// <summary>
    /// 向下取整为 int，通过 epsilon 施加微小正偏置以降低边界误差。
    /// </summary>
    public static int FloorToInt(this float a, float epsilon = float.Epsilon);
    public static int FloorToInt(this double a, double epsilon = double.Epsilon);

    /// <summary>
    /// 向上取整为 int，通过 epsilon 施加微小负偏置以降低边界误差。
    /// </summary>
    public static int CeilToInt(this float a, float epsilon = float.Epsilon);
    public static int CeilToInt(this double a, double epsilon = double.Epsilon);

    /// <summary>
    /// 求余，结果符号与 a 一致
    /// </summary>
    public static int Rem(this int a, int b);
    public static double Rem(this double a, double b);

    /// <summary>
    /// 求模，结果符号与 b 一致
    /// </summary>
    public static int Mod(this int a, int b);
    public static double Mod(this double a, double b);

    /// <summary>
    /// 二维点之间的欧几里得距离。
    /// </summary>
    public static float Distance(float ax, float ay, float bx, float by);

    /// <summary>
    /// 二维点之间欧几里得距离的平方（免开方）。
    /// </summary>
    public static float DistanceSquare(float ax, float ay, float bx, float by);
}
```

### 功能说明

#### 点结构体特性

**Point1 / Point1Int**
- **隐式转换**：可与 `double` / `int` 互相转换
- **区间点**：按给定块大小拆成 `AreaIndex` 与 `Remainder`，用于网格/分块索引
- **扩展方法**：`Point1Utils` / `Point1IntUtils` 提供实例扩展

**Point2Int**
- **坐标访问**：支持 X、Y 直接访问与索引器（0=X，1=Y）
- **运算符重载**：加法、减法、相等比较
- **类型转换**：隐式提升为 `Point3Int`（Z = 0）
- **性能优化**：运算符使用 `MethodImpl` 内联

**Point3Int**
- **三维坐标**：X、Y、Z 与索引器（0/1/2）
- **运算符重载**：加法、减法、相等比较
- **类型转换**：隐式降维为 `Point2Int`（丢弃 Z）

#### 边界与线段

**Bounds2Int**
- **半开矩形**：`[XMin, XMax) × [YMin, YMax)`
- **属性计算**：中心、尺寸、面积、空边界判断
- **包含与相交**：点包含、轴向包含、角点相交检测
- **几何操作**：平移、裁剪、分割、格点并/差、绕中心缩放

**Line1 / Line1Int**
- **有向线段**：保留 Start/End 方向，同时提供 Min/Max、有符号长度与绝对长度
- **对齐切分**：可按 0、起点、终点、最小/最大端或任意基准点切成等长子段

**Line2Int**
- **二维整型线段**：提供平方长度、欧几里得长度与退化点判断

#### 区间类型

**Interval / IntervalInt**
- **闭区间**：由 Min、Max 表示；`IntervalInt` 可通过 `MaxIncluded` 决定 Length 是否含 Max

**Range / RangeInt**
- **半开区间**：由 Start 与 Length 表示，End 不含

#### 二维数组特性

**Array2D&lt;T&gt;**
- **包装已有数据**：包装 `T[][]` 或由扁平缓冲按行宽切分
- **边界访问**：越界 `GetValue` 返回 `default(T)`
- **邻域搜索**：`GetNearValue` 在给定半径内查找目标值
- **区域切片**：`GetDataAtBound` 按 `Bounds2Int` 裁剪子阵列

#### 位运算特性

**BitMark**
- **预计算单比特掩码**：`1 << index` 形式，覆盖 byte/ushort/int/uint/ulong
- **连续位掩码重载**：`(index, len)` 形式生成多比特掩码

**BitFixedData**
- **定长打包**：按 `BitsPerValue` 把业务值打包进 `uint[]`
- **三层访问**：原始数据、位、业务值均可读写
- **二进制文本**：`ToBitString` / `GetStringBits` 输出 0/1 字符串

#### 数学工具特性

**MathUtil**
- **钳制**：`Clamp`、`Clamp01`、开区间 `Between`
- **奇偶取整**：向下/向上到偶数或奇数
- **求余与求模**：`Rem` 符号随被除数，`Mod` 符号随除数
- **距离**：二维欧几里得距离及其平方
- **浮点辅助**：近似相等、带偏置的 Floor/Ceil 转 int

### 使用示例

#### 点操作
```csharp
var point1 = new Point2Int(10, 20);
var point2 = new Point2Int(5, 15);

Console.WriteLine($"点1: {point1}"); // {X=10,Y=20}
Console.WriteLine($"点2: {point2}"); // {X=5,Y=15}

var sum = point1 + point2;   // {X=15,Y=35}
var diff = point1 - point2;  // {X=5,Y=5}

bool isEqual = point1 == point2;     // false
bool isNotEqual = point1 != point2;  // true

int x = point1[0]; // 10
int y = point1[1]; // 20

point1.Set(30, 40);
Console.WriteLine($"修改后: {point1}"); // {X=30,Y=40}

Point3Int p3 = point1;               // Z = 0
Point2Int back = new Point3Int(1, 2, 3); // {X=1,Y=2}
```

#### 一维点与区间索引
```csharp
Point1 p = 5.5;
var area = p.ToAreaPoint1(2);                 // AreaIndex=2, Remainder=1.5
Point1 restored = area.FromAreaPoint1(2);     // 5.5

Point1Int pi = -3;
var areaInt = pi.ToAreaPoint1(4);             // 负坐标也能落到正确块
```

#### 边界操作
```csharp
var bounds = new Bounds2Int(0, 0, 100, 100);
Console.WriteLine($"边界: {bounds}");
// {Min={X=0,Y=0},Max={X=100,Y=100},Center={X=50,Y=50},Size={X=100,Y=100},Area=10000}

Console.WriteLine($"中心点: {bounds.Center}"); // {X=50,Y=50}
Console.WriteLine($"大小: {bounds.Size}");     // {X=100,Y=100}
Console.WriteLine($"面积: {bounds.Area}");     // 10000

var point = new Point2Int(25, 25);
bool contains = bounds.Contains(point); // true
bool containsX = bounds.ContainsX(25);  // true
bool containsY = bounds.ContainsY(25);  // true
bool onMax = bounds.Contains(100, 50);  // false（半开，XMax 不含）

var movedBounds = bounds.Move(new Point2Int(10, 10));
var croppedBounds = bounds.Crop(new Bounds2Int(25, 25, 75, 75));

var splitBounds = bounds.SplitX(50);
Console.WriteLine($"X分割: {splitBounds.Length} 个边界"); // 2

var centerBounds = Bounds2Int.NewCenterBound(new Point2Int(50, 50), new Point2Int(20, 20));
// Min={X=40,Y=40}, Max={X=60,Y=60}

var scaled = bounds * 0.5f; // 绕中心缩放，各轴向上取整
```

#### 线段切分
```csharp
var line = new Line1(1.5, 10.0);
Line1[] parts = line.SliceAtZero(4);
// 按长度 4、对齐坐标 0 切分

var lineInt = new Line1Int(0, 10);
Line1Int[] chunks = lineInt.SliceAtStart(3);

var seg = new Line2Int(new Point2Int(0, 0), new Point2Int(3, 4));
Console.WriteLine(seg.SqrMagnitude); // 25
Console.WriteLine(seg.Magnitude);    // 5
```

#### 区间与范围
```csharp
var interval = new Interval(0f, 10f);
Console.WriteLine(interval.Length); // 10

var intervalInt = new IntervalInt(0, 10, maxIncluded: true);
Console.WriteLine(intervalInt.Length); // 11

var range = new RangeInt(2, 5);
Console.WriteLine(range.End); // 7（不含）
```

#### 二维数组操作
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
int missing = array.GetValue(9, 9);   // 0（越界返回 default）

Point2Int? near = array.GetNearValue(0, 0, 8, 4); // {X=2,Y=2}

int[][] slice = array.GetDataAtBound(new Bounds2Int(1, 1, 3, 3));
Console.WriteLine(array.ToPrintString(reverse: false));
```

#### 位掩码与位存储器
```csharp
byte bit3 = BitMark.GetByteMark(3); // 0b00001000

var pack = new BitFixedData(bitsPerValue: 4, valueLen: 8);
pack.SetValue(0, 13);
uint v = pack.GetValue(0);          // 13
pack.SetBit(0, true);
bool b0 = pack.GetBit(0);
string bits = pack.GetStringBits();
```

#### 数学计算
```csharp
float distance = MathUtil.Distance(0f, 0f, 3f, 4f);       // 5
float distanceSquare = MathUtil.DistanceSquare(0f, 0f, 3f, 4f); // 25

int clamped = MathUtil.Clamp(150, 0, 100);     // 100
float clamped01 = MathUtil.Clamp01(1.5f);      // 1
bool inside = MathUtil.Between(5, 0, 10);      // true

bool odd = MathUtil.IsOdd(7);                  // true
int even = MathUtil.FloorToEven(5.9f);         // 4

int rem = (-5).Rem(3);                         // -2（符号随被除数）
int mod = (-5).Mod(3);                         // 1（符号随除数）

int floored = 3.9f.FloorToInt();
```

#### 复杂操作组合
```csharp
var mapBounds = new Bounds2Int(0, 0, 1000, 1000);
var playerPos = new Point2Int(500, 500);
var viewBounds = Bounds2Int.NewCenterBound(playerPos, new Point2Int(100, 100));
var visibleArea = mapBounds.Crop(viewBounds);
var visiblePoints = visibleArea.ToArray();

float distanceToCenter = MathUtil.Distance(
    playerPos.X, playerPos.Y,
    mapBounds.Center.X, mapBounds.Center.Y);

Console.WriteLine($"视野内点数: {visiblePoints.Length}");
Console.WriteLine($"到地图中心距离: {distanceToCenter}");
```

### 设计特点

1. **性能优化**：点与边界的运算符使用结构体与 `MethodImpl` 内联
2. **类型安全**：整型/浮点、闭区间/半开区间分离，避免混用
3. **操作符重载**：点加减、边界缩放等直观运算
4. **内存效率**：几何类型为值类型；`Array2D` 包装已有锯齿数组
5. **功能完整**：覆盖点、线、边界、区间、位打包与常用数学
6. **易于使用**：隐式转换、扩展方法与简洁构造函数

### 注意事项

1. **值类型**：结构体按值复制；修改副本不会影响原值
2. **半开边界**：`Bounds2Int` 与 `Range`/`RangeInt` 的上界不含；`Contains(XMax, y)` 为 false
3. **空结果**：`ToArray()`、`SliceAt`（size 为 0）、`GetDataAtBound` 在空输入时可能返回 `null`
4. **Array2D 越界**：`GetValue` 越界返回 `default(T)`，不会抛异常
5. **BitFixedData**：`bitsPerValue` 必须满足 `0 <= bitsPerValue < 32`
6. **IsSimilar**：`epsilon` 参数未使用，比较始终基于机器精度
7. **内存使用**：`Bounds2Int.ToArray()` / `Add` / `Sub` 可能产生大量分配
