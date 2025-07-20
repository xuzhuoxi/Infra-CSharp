# JLGames.Infra.Imagex API 文档

## 概述

Imagex模块提供了图像处理功能，包括图像接口、RGBA图像类、透明度处理以及图像过滤器等。

## 命名空间

`JLGames.Infra.Imagex`

---

## 接口

### IImage

图像接口，定义了图像的基本操作。

```csharp
public interface IImage
```

#### 属性

##### Bounds

```csharp
Bounds2Int Bounds { get; }
```

**描述：** Data range / 数据范围定义

**类型：** `Bounds2Int`

#### 方法

##### At(int x, int y)

```csharp
uint At(int x, int y);
```

**描述：** Get pixel value / 取像素值

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标

**返回值：**
- `uint`: 像素值

##### Set(int x, int y, uint color)

```csharp
void Set(int x, int y, uint color);
```

**描述：** Set pixel value / 设置像素值

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标
- `color` (uint): 颜色值

---

### IAlpha

透明度接口，定义了透明度处理的基本操作。

```csharp
public interface IAlpha
```

#### 方法

##### AlphaAt(int x, int y)

```csharp
byte AlphaAt(int x, int y);
```

**描述：** Get alpha value / 取透明度

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标

**返回值：**
- `byte`: 透明度值

##### SetAlpha(int x, int y, byte alpha)

```csharp
void SetAlpha(int x, int y, byte alpha);
```

**描述：** Set alpha value / 设置透明度

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标
- `alpha` (byte): 透明度值

---

## 类

### RGBA

RGBA图像类，实现了IImage和IAlpha接口，提供32位RGBA图像处理功能。

```csharp
public class RGBA : ICloneable<RGBA>, IImage, IAlpha
```

#### 构造函数

##### RGBA(int width, int height)

```csharp
public RGBA(int width, int height)
```

**描述：** 创建指定宽度和高度的RGBA图像

**参数：**
- `width` (int): 图像宽度
- `height` (int): 图像高度

##### RGBA(Bounds2Int rect)

```csharp
public RGBA(Bounds2Int rect)
```

**描述：** 根据边界创建RGBA图像

**参数：**
- `rect` (Bounds2Int): 图像边界

#### 属性

##### Bounds

```csharp
public Bounds2Int Bounds => m_Rect;
```

**描述：** 图像边界

#### 方法

##### Clone()

```csharp
public RGBA Clone()
```

**描述：** 克隆图像

**返回值：**
- `RGBA`: 克隆的图像对象

##### At(int x, int y)

```csharp
public uint At(int x, int y)
```

**描述：** 获取像素值

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标

**返回值：**
- `uint`: 像素值

##### Set(int x, int y, uint color)

```csharp
public void Set(int x, int y, uint color)
```

**描述：** 设置像素值

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标
- `color` (uint): 颜色值

##### AlphaAt(int x, int y)

```csharp
public byte AlphaAt(int x, int y)
```

**描述：** 获取透明度值

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标

**返回值：**
- `byte`: 透明度值

##### SetAlpha(int x, int y, byte alpha)

```csharp
public void SetAlpha(int x, int y, byte alpha)
```

**描述：** 设置透明度值

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标
- `alpha` (byte): 透明度值

##### RgbaAt(int x, int y)

```csharp
public uint RgbaAt(int x, int y)
```

**描述：** 获取RGBA像素值

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标

**返回值：**
- `uint`: RGBA像素值

##### PixOffset(int x, int y)

```csharp
public int PixOffset(int x, int y)
```

**描述：** 计算像素在数组中的偏移量

**参数：**
- `x` (int): X坐标
- `y` (int): Y坐标

**返回值：**
- `int`: 像素偏移量

---

## 过滤器

### FilterKernel

向量核心过滤器类，提供图像过滤器的核心功能。

```csharp
public class FilterKernel : ICloneable<FilterKernel>
```

#### 属性

##### Vectors

```csharp
internal KernelVector[] Vectors;
```

**描述：** 核心向量数组

##### Len

```csharp
public int Len => Vectors?.Length ?? 0;
```

**描述：** 向量数量

#### 方法

##### Less(int i, int j)

```csharp
public bool Less(int i, int j)
```

**描述：** 比较两个向量的顺序

**参数：**
- `i` (int): 第一个向量索引
- `j` (int): 第二个向量索引

**返回值：**
- `bool`: 比较结果

##### Less(KernelVector i, KernelVector j)

```csharp
public bool Less(KernelVector i, KernelVector j)
```

**描述：** 比较两个向量

**参数：**
- `i` (KernelVector): 第一个向量
- `j` (KernelVector): 第二个向量

**返回值：**
- `bool`: 比较结果

##### Swap(int i, int j)

```csharp
public void Swap(int i, int j)
```

**描述：** Swap data / 交换数据

**参数：**
- `i` (int): 第一个索引
- `j` (int): 第二个索引

##### IndexOfValue(int value)

```csharp
public int IndexOfValue(int value)
```

**描述：** 查找指定值的索引

**参数：**
- `value` (int): 要查找的值

**返回值：**
- `int`: 值的索引，未找到返回-1

##### Clone()

```csharp
public FilterKernel Clone()
```

**描述：** 克隆过滤器核心

**返回值：**
- `FilterKernel`: 克隆的对象

##### FlipUpDownSelf()

```csharp
public void FlipUpDownSelf()
```

**描述：** Flip itself upside down / 上下翻转自身

##### FlipUuDown()

```csharp
public FilterKernel FlipUuDown()
```

**描述：** Flip Upside down / 上下翻转

**返回值：**
- `FilterKernel`: 翻转后的过滤器核心

##### FlipLeftRightSelf()

```csharp
public void FlipLeftRightSelf()
```

**描述：** Flips itself left and right / 左右翻转自身

##### FlipLeftRight()

```csharp
public FilterKernel FlipLeftRight()
```

**描述：** Flip left and right / 左右翻转

**返回值：**
- `FilterKernel`: 翻转后的过滤器核心

##### Rotate90Self(bool clockwise)

```csharp
public void Rotate90Self(bool clockwise)
```

**描述：** Rotate 90 degrees / 旋转90度

**参数：**
- `clockwise` (bool): 是否为顺时针

##### Rotate90(bool clockwise)

```csharp
public FilterKernel Rotate90(bool clockwise)
```

**描述：** Rotate 90 degrees / 旋转90度

**参数：**
- `clockwise` (bool): 是否为顺时针

**返回值：**
- `FilterKernel`: 旋转后的过滤器核心

##### RotateSelf(bool clockwise, int count90)

```csharp
public void RotateSelf(bool clockwise, int count90)
```

**描述：** Rotate / 旋转

**参数：**
- `clockwise` (bool): 是否为顺时针
- `count90` (int): 90度旋转次数

##### Rotate(bool clockwise, int count90)

```csharp
public FilterKernel Rotate(bool clockwise, int count90)
```

**描述：** Rotate / 旋转

**参数：**
- `clockwise` (bool): 是否为顺时针
- `count90` (int): 90度旋转次数

**返回值：**
- `FilterKernel`: 旋转后的过滤器核心

##### Sort()

```csharp
public void Sort()
```

**描述：** Sort / 排序

---

### FilterMatrix

过滤器矩阵类，提供图像过滤矩阵功能。

```csharp
public class FilterMatrix
```

#### 主要方法

##### Apply(IImage source, IImage target)

```csharp
public void Apply(IImage source, IImage target)
```

**描述：** 应用过滤器到图像

**参数：**
- `source` (IImage): 源图像
- `target` (IImage): 目标图像

---

### KernelVector

核心向量类，表示过滤器核心中的向量。

```csharp
public class KernelVector
```

#### 属性

##### X

```csharp
public int X { get; set; }
```

**描述：** X坐标

##### Y

```csharp
public int Y { get; set; }
```

**描述：** Y坐标

##### Value

```csharp
public int Value { get; set; }
```

**描述：** 向量值

#### 方法

##### Less(KernelVector other)

```csharp
public bool Less(KernelVector other)
```

**描述：** 比较两个向量

**参数：**
- `other` (KernelVector): 另一个向量

**返回值：**
- `bool`: 比较结果

---

## 使用示例

### 基本图像操作

```csharp
// 创建RGBA图像
var image = new RGBA(256, 256);

// 设置像素颜色
uint redColor = 0xFF0000FF;    // 红色
uint greenColor = 0xFF00FF00;  // 绿色
uint blueColor = 0xFFFF0000;   // 蓝色

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
// 创建带边界的图像
var bounds = new Bounds2Int(10, 10, 100, 100);
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
cloned.Set(50, 50, 0xFF000000); // 黑色像素

// 原始图像不受影响
uint originalPixel = original.At(50, 50); // 仍然是白色
uint clonedPixel = cloned.At(50, 50);     // 现在是黑色
```

### 过滤器操作

```csharp
// 创建过滤器核心
var kernel = new FilterKernel();
kernel.Vectors = new KernelVector[]
{
    new KernelVector { X = -1, Y = -1, Value = 1 },
    new KernelVector { X = 0, Y = -1, Value = 2 },
    new KernelVector { X = 1, Y = -1, Value = 1 },
    new KernelVector { X = -1, Y = 0, Value = 2 },
    new KernelVector { X = 0, Y = 0, Value = 4 },
    new KernelVector { X = 1, Y = 0, Value = 2 },
    new KernelVector { X = -1, Y = 1, Value = 1 },
    new KernelVector { X = 0, Y = 1, Value = 2 },
    new KernelVector { X = 1, Y = 1, Value = 1 }
};

// 翻转操作
var flippedUpDown = kernel.FlipUuDown();
var flippedLeftRight = kernel.FlipLeftRight();

// 旋转操作
var rotated90 = kernel.Rotate90(true);  // 顺时针旋转90度
var rotated180 = kernel.Rotate(true, 2); // 顺时针旋转180度

// 排序
kernel.Sort();
```

### 图像处理管道

```csharp
// 创建源图像和目标图像
var sourceImage = new RGBA(512, 512);
var targetImage = new RGBA(512, 512);

// 填充源图像
for (int x = 0; x < 512; x++)
{
    for (int y = 0; y < 512; y++)
    {
        uint color = (uint)((x + y) % 256) | 0xFF000000;
        sourceImage.Set(x, y, color);
    }
}

// 创建过滤器矩阵
var filterMatrix = new FilterMatrix();

// 应用过滤器
filterMatrix.Apply(sourceImage, targetImage);
```

---

## 注意事项

1. **坐标系统：** 图像使用左上角为原点的坐标系统
2. **颜色格式：** RGBA使用32位整数格式，格式为0xAARRGGBB
3. **边界检查：** 所有像素操作都会进行边界检查，超出边界的操作会被忽略
4. **内存管理：** 图像数据存储在字节数组中，注意内存使用
5. **性能考虑：** 大量像素操作时注意性能，避免频繁的边界检查
6. **透明度：** Alpha通道值范围是0-255，0表示完全透明，255表示完全不透明

---

## 依赖关系

- `JLGames.Infra.Mathx`: 使用Bounds2Int等数学类型
- `JLGames.Infra`: 使用ICloneable接口
- `System`: 基础类型和数组操作 