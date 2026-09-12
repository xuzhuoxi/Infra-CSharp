# JLGames.Infra.Imagex API 文档

## 概述

Imagex 模块提供二维像素缓冲区与卷积滤波相关类型，包括图像接口、RGBA 图像、透明度通道，以及稀疏卷积核与滤波矩阵。

## 命名空间

`JLGames.Infra.Imagex`

---

## 接口

### IImage

二维像素缓冲区的读写访问接口。

```csharp
public interface IImage
```

#### 属性

##### Bounds

```csharp
Bounds2Int Bounds { get; }
```

**描述：** 数据范围。

**类型：** `Bounds2Int`

#### 方法

##### At(int x, int y)

```csharp
uint At(int x, int y);
```

**描述：** 取像素值。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标

**返回值：**
- `uint`: 打包为 uint 的像素颜色

##### Set(int x, int y, uint color)

```csharp
void Set(int x, int y, uint color);
```

**描述：** 设置像素值。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标
- `color` (uint): 打包颜色值

---

### IAlpha

逐像素透明度通道的读写访问接口。

```csharp
public interface IAlpha
```

#### 方法

##### AlphaAt(int x, int y)

```csharp
byte AlphaAt(int x, int y);
```

**描述：** 取透明度。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标

**返回值：**
- `byte`: 透明度，取值 0–255

##### SetAlpha(int x, int y, byte alpha)

```csharp
void SetAlpha(int x, int y, byte alpha);
```

**描述：** 设置透明度。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标
- `alpha` (byte): 透明度，取值 0–255

---

## 类

### RGBA

以字节数组存储的 RGBA 图像（每像素 R、G、B、A 各一字节）。实现 `ICloneable<RGBA>`、`IImage`、`IAlpha`。

```csharp
public class RGBA : ICloneable<RGBA>, IImage, IAlpha
```

#### 构造函数

##### RGBA(int width, int height)

```csharp
public RGBA(int width, int height)
```

**描述：** 在原点 (0, 0) 处创建指定宽高的图像。

**参数：**
- `width` (int): 图像宽度（像素）
- `height` (int): 图像高度（像素）

##### RGBA(Bounds2Int rect)

```csharp
public RGBA(Bounds2Int rect)
```

**描述：** 按给定边界矩形创建图像。

**参数：**
- `rect` (Bounds2Int): 像素边界

#### 属性

##### Bounds

```csharp
public Bounds2Int Bounds { get; }
```

**描述：** 数据范围。

#### 方法

##### Clone()

```csharp
public RGBA Clone()
```

**描述：** 深拷贝像素数据及布局元数据。

**返回值：**
- `RGBA`: 新的独立副本

##### At(int x, int y)

```csharp
public uint At(int x, int y)
```

**描述：** 取像素值（等价于 `RgbaAt`）。越界时返回 0。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标

**返回值：**
- `uint`: 打包为 uint 的像素颜色

##### Set(int x, int y, uint color)

```csharp
public void Set(int x, int y, uint color)
```

**描述：** 设置像素值。越界时忽略。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标
- `color` (uint): 打包颜色值

##### AlphaAt(int x, int y)

```csharp
public byte AlphaAt(int x, int y)
```

**描述：** 取透明度。越界时返回 0。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标

**返回值：**
- `byte`: 透明度值

##### SetAlpha(int x, int y, byte alpha)

```csharp
public void SetAlpha(int x, int y, byte alpha)
```

**描述：** 设置透明度。越界时忽略。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标
- `alpha` (byte): 透明度值

##### RgbaAt(int x, int y)

```csharp
public uint RgbaAt(int x, int y)
```

**描述：** 读取打包的 RGBA uint（R 在高字节，A 在低字节）。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标

**返回值：**
- `uint`: 打包颜色，越界返回 0

##### PixOffset(int x, int y)

```csharp
public int PixOffset(int x, int y)
```

**描述：** 像素 (x, y) 在底层数组中 R 通道的字节偏移。不进行边界检查。

**参数：**
- `x` (int): X 坐标
- `y` (int): Y 坐标

**返回值：**
- `int`: 底层字节数组中的下标

---

## 过滤器

### FilterKernel

以偏移/权重向量表示的稀疏卷积核。实现 `ICloneable<FilterKernel>`。

```csharp
public class FilterKernel : ICloneable<FilterKernel>
```

#### 属性

##### Len

```csharp
public int Len { get; }
```

**描述：** 卷积核向量数量；未初始化时为 0。

#### 方法

##### Less(int i, int j)

```csharp
public bool Less(int i, int j)
```

**描述：** 下标 `i` 的向量是否排在下标 `j` 之前。

**参数：**
- `i` (int): 第一个下标
- `j` (int): 第二个下标

**返回值：**
- `bool`: 前者排序靠前则为 `true`

##### Less(KernelVector i, KernelVector j)

```csharp
public bool Less(KernelVector i, KernelVector j)
```

**描述：** `i` 是否排在 `j` 之前（先行后列）。

**参数：**
- `i` (KernelVector): 第一个向量
- `j` (KernelVector): 第二个向量

**返回值：**
- `bool`: 前者排序靠前则为 `true`

##### Swap(int i, int j)

```csharp
public void Swap(int i, int j)
```

**描述：** 按下标交换两个向量。

**参数：**
- `i` (int): 第一个下标
- `j` (int): 第二个下标

##### IndexOfValue(int value)

```csharp
public int IndexOfValue(int value)
```

**描述：** 查找权重等于 `value` 的第一个下标。

**参数：**
- `value` (int): 要查找的权重

**返回值：**
- `int`: 下标，未找到返回 -1

##### Clone()

```csharp
public FilterKernel Clone()
```

**描述：** 浅拷贝向量数组。

**返回值：**
- `FilterKernel`: 含拷贝向量的新卷积核

##### FlipUpDownSelf()

```csharp
public void FlipUpDownSelf()
```

**描述：** 上下翻转自身。

##### FlipUuDown()

```csharp
public FilterKernel FlipUuDown()
```

**描述：** 上下翻转并返回新卷积核。

**返回值：**
- `FilterKernel`: 翻转后的副本

##### FlipLeftRightSelf()

```csharp
public void FlipLeftRightSelf()
```

**描述：** 左右翻转自身。

##### FlipLeftRight()

```csharp
public FilterKernel FlipLeftRight()
```

**描述：** 左右翻转并返回新卷积核。

**返回值：**
- `FilterKernel`: 翻转后的副本

##### Rotate90Self(bool clockwise)

```csharp
public void Rotate90Self(bool clockwise)
```

**描述：** 原地旋转 90 度。

**参数：**
- `clockwise` (bool): `true` 顺时针，`false` 逆时针

##### Rotate90(bool clockwise)

```csharp
public FilterKernel Rotate90(bool clockwise)
```

**描述：** 旋转 90 度并返回新卷积核。

**参数：**
- `clockwise` (bool): `true` 顺时针，`false` 逆时针

**返回值：**
- `FilterKernel`: 旋转后的副本

##### RotateSelf(bool clockwise, int count90)

```csharp
public void RotateSelf(bool clockwise, int count90)
```

**描述：** 原地按 90 度倍数旋转。

**参数：**
- `clockwise` (bool): 每步 `true` 为顺时针
- `count90` (int): 90° 步数（负值会归一化）

##### Rotate(bool clockwise, int count90)

```csharp
public FilterKernel Rotate(bool clockwise, int count90)
```

**描述：** 按 90 度倍数旋转并返回新卷积核。

**参数：**
- `clockwise` (bool): 每步 `true` 为顺时针
- `count90` (int): 90° 步数（负值会归一化）

**返回值：**
- `FilterKernel`: 旋转后的副本

##### Sort()

```csharp
public void Sort()
```

**描述：** 按先行后列（Y 再 X）对向量排序。

---

### FilterMatrix

图像卷积滤波矩阵（稀疏卷积核及倍率/偏移元数据）。实现 `ICloneable<FilterMatrix>`。

```csharp
public class FilterMatrix : ICloneable<FilterMatrix>
```

#### 属性

##### Kernel

```csharp
public FilterKernel Kernel { get; }
```

**描述：** 稀疏卷积核。

##### KernelRadius

```csharp
public int KernelRadius { get; }
```

**描述：** 卷积核半径（像素半边长）。

##### KernelSize

```csharp
public int KernelSize { get; }
```

**描述：** 卷积核边长（`2 * KernelRadius + 1`）。

##### KernelScale

```csharp
public int KernelScale { get; }
```

**描述：** 卷积后的除数（权重之和应等于此值）。

##### ResultOffset

```csharp
public int ResultOffset { get; }
```

**描述：** 滤波结果上加的常数偏移。

##### IsScaleMatrix

```csharp
public bool IsScaleMatrix { get; }
```

**描述：** 是否为倍率滤波器。

##### IsPixelUnsafe

```csharp
public bool IsPixelUnsafe { get; }
```

**描述：** 运算结果是否可能超出像素范围（非安全像素值）。当 `ResultOffset != 0` 或任一卷积权重小于 0 时为 `true`。

#### 方法

##### Clone()

```csharp
public FilterMatrix Clone()
```

**描述：** 深拷贝卷积核及元数据。

**返回值：**
- `FilterMatrix`: 新的独立副本

##### FlipUpDown()

```csharp
public FilterMatrix FlipUpDown()
```

**描述：** 上下翻转。

**返回值：**
- `FilterMatrix`: 翻转并已排序卷积核的副本

##### FlipLeftRight()

```csharp
public FilterMatrix FlipLeftRight()
```

**描述：** 左右翻转。

**返回值：**
- `FilterMatrix`: 翻转并已排序卷积核的副本

##### Rotate(bool clockwise, int count90)

```csharp
public FilterMatrix Rotate(bool clockwise, int count90)
```

**描述：** 按 90 度倍数旋转。

**参数：**
- `clockwise` (bool): 每步 `true` 为顺时针
- `count90` (int): 90° 步数

**返回值：**
- `FilterMatrix`: 旋转并已排序卷积核的副本

##### CheckValidity()

```csharp
public bool CheckValidity()
```

**描述：** 检查滤波模板有效性（半径、倍率及权重和）。

**返回值：**
- `bool`: 半径 ≥ 1、倍率 ≥ 0 且权重和等于 `KernelScale` 时为 `true`

---

### KernelVector

滤波器向量单元。按先行后列（先 Y 后 X）比较。

```csharp
public struct KernelVector : IComparable<KernelVector>
```

#### 字段

##### X

```csharp
public int X;
```

**描述：** 相对卷积核中心的 X 偏移。

##### Y

```csharp
public int Y;
```

**描述：** 相对卷积核中心的 Y 偏移。

##### Value

```csharp
public int Value;
```

**描述：** 该偏移处的卷积权重。

#### 方法

##### CompareTo(KernelVector j)

```csharp
public int CompareTo(KernelVector j)
```

**描述：** 按排序规则比较（先 Y 后 X）；相等时也不返回 0。

**参数：**
- `j` (KernelVector): 另一向量

**返回值：**
- `int`: 若本项排在前面返回 -1，否则 1

##### Less(KernelVector j)

```csharp
public bool Less(KernelVector j)
```

**描述：** 本向量是否排在 `j` 之前（先行后列：先 Y 后 X）。

**参数：**
- `j` (KernelVector): 另一向量

**返回值：**
- `bool`: 排序意义下小于对方则为 `true`

---

## 使用示例

### 基本图像操作

```csharp
// 创建 RGBA 图像
var image = new RGBA(256, 256);

// 设置像素颜色（打包格式 0xRRGGBBAA：R 在高字节，A 在低字节）
uint redColor = 0xFF0000FF;    // 不透明红
uint greenColor = 0x00FF00FF;  // 不透明绿
uint blueColor = 0x0000FFFF;   // 不透明蓝

image.Set(100, 100, redColor);
image.Set(150, 150, greenColor);
image.Set(200, 200, blueColor);

// 获取像素值
uint pixel = image.At(100, 100);

// 设置透明度
image.SetAlpha(100, 100, 128); // 半透明
byte alpha = image.AlphaAt(100, 100);
```

### 图像边界操作

```csharp
// 创建带边界的图像（Bounds2Int 为 xMin, yMin, xMax, yMax，最大角不含）
var bounds = new Bounds2Int(10, 10, 110, 110);
var image = new RGBA(bounds);

// 检查边界
Console.WriteLine($"图像边界: {image.Bounds}");
Console.WriteLine($"图像宽度: {image.Bounds.Size.X}");
Console.WriteLine($"图像高度: {image.Bounds.Size.Y}");
```

### 图像克隆

```csharp
// 创建原始图像
var original = new RGBA(100, 100);
original.Set(50, 50, 0xFFFFFFFF); // 白色像素

// 克隆图像
var cloned = original.Clone();

// 修改克隆图像
cloned.Set(50, 50, 0x000000FF); // 不透明黑

// 原始图像不受影响
uint originalPixel = original.At(50, 50); // 仍然是白色
uint clonedPixel = cloned.At(50, 50);     // 现在是黑色
```

### 卷积核向量比较

```csharp
var v1 = new KernelVector { X = -1, Y = -1, Value = 1 };
var v2 = new KernelVector { X = 0, Y = -1, Value = 2 };

bool less = v1.Less(v2);       // true（同一行时 X 更小者靠前）
int cmp = v1.CompareTo(v2);    // -1
```

### 过滤器操作

```csharp
var kernel = new FilterKernel();
Console.WriteLine(kernel.Len); // 未初始化时为 0

var clonedKernel = kernel.Clone();

// 已填充卷积核上的几何变换（返回新实例，不修改原核）
FilterKernel source = clonedKernel;
var flippedUpDown = source.FlipUuDown();
var flippedLeftRight = source.FlipLeftRight();
var rotated90 = source.Rotate90(true);   // 顺时针旋转 90 度
var rotated180 = source.Rotate(true, 2); // 顺时针旋转 180 度

source.Sort();
```

### 滤波矩阵变换

```csharp
FilterMatrix matrix = /* 已初始化的滤波矩阵 */;

if (matrix.CheckValidity())
{
    Console.WriteLine($"半径: {matrix.KernelRadius}, 边长: {matrix.KernelSize}");
    Console.WriteLine($"倍率: {matrix.KernelScale}, 偏移: {matrix.ResultOffset}");
    Console.WriteLine($"倍率滤波: {matrix.IsScaleMatrix}, 非安全像素: {matrix.IsPixelUnsafe}");

    var flipped = matrix.FlipUpDown();
    var mirrored = matrix.FlipLeftRight();
    var rotated = matrix.Rotate(true, 1);

    FilterKernel kernel = matrix.Kernel;
    int n = kernel.Len;
}
```

---

## 注意事项

1. **坐标系统：** 像素坐标与 `Bounds` 一致；`RGBA(int, int)` 原点为 (0, 0)。`Bounds2Int` 的最大角为不含上界。
2. **颜色格式：** 打包 uint 为 0xRRGGBBAA（R 在高字节，A 在低字节）。底层按每像素 4 字节（R、G、B、A）存储。
3. **边界检查：** `At` / `RgbaAt` / `AlphaAt` 越界返回 0；`Set` / `SetAlpha` 越界忽略。`PixOffset` 不检查边界。
4. **内存布局：** 行跨度为 `4 * 宽度` 字节；`PixOffset` 相对 `Bounds` 的最小角计算。
5. **透明度：** Alpha 取值 0–255，0 为完全透明，255 为完全不透明。
6. **卷积核数据：** `FilterKernel` 的向量数组为内部字段，无公开赋值入口；`Len` 在未初始化时为 0。
7. **KernelVector 排序：** `CompareTo` 在键相等时也不返回 0（相等时返回 1）。
8. **滤波矩阵：** `FilterMatrix` 提供卷积核元数据与几何变换，不包含对 `IImage` 的 Apply 接口。

---

## 依赖关系

- `JLGames.Infra.Mathx`: 使用 `Bounds2Int` 等数学类型
- `JLGames.Infra`: 使用 `ICloneable<T>` 接口
- `System`: `IComparable<T>`、数组排序
