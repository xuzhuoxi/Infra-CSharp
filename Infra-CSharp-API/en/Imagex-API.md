# JLGames.Infra.Imagex API Documentation

## Overview

The Imagex module provides image processing functionality, including image interfaces, RGBA image classes, transparency processing, and image filters.

## Namespace

`JLGames.Infra.Imagex`

---

## Interfaces

### IImage

Image interface that defines basic image operations.

```csharp
public interface IImage
```

#### Properties

##### Bounds

```csharp
Bounds2Int Bounds { get; }
```

**Description:** Data range definition

**Type:** `Bounds2Int`

#### Methods

##### At(int x, int y)

```csharp
uint At(int x, int y);
```

**Description:** Get pixel value

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `uint`: Pixel value

##### Set(int x, int y, uint color)

```csharp
void Set(int x, int y, uint color);
```

**Description:** Set pixel value

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate
- `color` (uint): Color value

---

### IAlpha

Alpha interface that defines basic transparency processing operations.

```csharp
public interface IAlpha
```

#### Methods

##### AlphaAt(int x, int y)

```csharp
byte AlphaAt(int x, int y);
```

**Description:** Get alpha value

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `byte`: Alpha value

##### SetAlpha(int x, int y, byte alpha)

```csharp
void SetAlpha(int x, int y, byte alpha);
```

**Description:** Set alpha value

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate
- `alpha` (byte): Alpha value

---

## Classes

### RGBA

RGBA image class that implements IImage and IAlpha interfaces, providing 32-bit RGBA image processing functionality.

```csharp
public class RGBA : ICloneable<RGBA>, IImage, IAlpha
```

#### Constructors

##### RGBA(int width, int height)

```csharp
public RGBA(int width, int height)
```

**Description:** Create RGBA image with specified width and height

**Parameters:**
- `width` (int): Image width
- `height` (int): Image height

##### RGBA(Bounds2Int rect)

```csharp
public RGBA(Bounds2Int rect)
```

**Description:** Create RGBA image based on bounds

**Parameters:**
- `rect` (Bounds2Int): Image bounds

#### Properties

##### Bounds

```csharp
public Bounds2Int Bounds => m_Rect;
```

**Description:** Image bounds

#### Methods

##### Clone()

```csharp
public RGBA Clone()
```

**Description:** Clone image

**Return Value:**
- `RGBA`: Cloned image object

##### At(int x, int y)

```csharp
public uint At(int x, int y)
```

**Description:** Get pixel value

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `uint`: Pixel value

##### Set(int x, int y, uint color)

```csharp
public void Set(int x, int y, uint color)
```

**Description:** Set pixel value

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate
- `color` (uint): Color value

##### AlphaAt(int x, int y)

```csharp
public byte AlphaAt(int x, int y)
```

**Description:** Get alpha value

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `byte`: Alpha value

##### SetAlpha(int x, int y, byte alpha)

```csharp
public void SetAlpha(int x, int y, byte alpha)
```

**Description:** Set alpha value

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate
- `alpha` (byte): Alpha value

##### RgbaAt(int x, int y)

```csharp
public uint RgbaAt(int x, int y)
```

**Description:** Get RGBA pixel value

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `uint`: RGBA pixel value

##### PixOffset(int x, int y)

```csharp
public int PixOffset(int x, int y)
```

**Description:** Calculate pixel offset in array

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `int`: Pixel offset

---

## Filters

### FilterKernel

Vector kernel filter class that provides core functionality for image filters.

```csharp
public class FilterKernel : ICloneable<FilterKernel>
```

#### Properties

##### Vectors

```csharp
internal KernelVector[] Vectors;
```

**Description:** Kernel vector array

##### Len

```csharp
public int Len => Vectors?.Length ?? 0;
```

**Description:** Vector count

#### Methods

##### Less(int i, int j)

```csharp
public bool Less(int i, int j)
```

**Description:** Compare order of two vectors

**Parameters:**
- `i` (int): First vector index
- `j` (int): Second vector index

**Return Value:**
- `bool`: Comparison result

##### Less(KernelVector i, KernelVector j)

```csharp
public bool Less(KernelVector i, KernelVector j)
```

**Description:** Compare two vectors

**Parameters:**
- `i` (KernelVector): First vector
- `j` (KernelVector): Second vector

**Return Value:**
- `bool`: Comparison result

##### Swap(int i, int j)

```csharp
public void Swap(int i, int j)
```

**Description:** Swap data

**Parameters:**
- `i` (int): First index
- `j` (int): Second index

##### IndexOfValue(int value)

```csharp
public int IndexOfValue(int value)
```

**Description:** Find index of specified value

**Parameters:**
- `value` (int): Value to find

**Return Value:**
- `int`: Value index, returns -1 if not found

##### Clone()

```csharp
public FilterKernel Clone()
```

**Description:** Clone filter kernel

**Return Value:**
- `FilterKernel`: Cloned object

##### FlipUpDownSelf()

```csharp
public void FlipUpDownSelf()
```

**Description:** Flip itself upside down

##### FlipUuDown()

```csharp
public FilterKernel FlipUuDown()
```

**Description:** Flip upside down

**Return Value:**
- `FilterKernel`: Flipped filter kernel

##### FlipLeftRightSelf()

```csharp
public void FlipLeftRightSelf()
```

**Description:** Flips itself left and right

##### FlipLeftRight()

```csharp
public FilterKernel FlipLeftRight()
```

**Description:** Flip left and right

**Return Value:**
- `FilterKernel`: Flipped filter kernel

##### Rotate90Self(bool clockwise)

```csharp
public void Rotate90Self(bool clockwise)
```

**Description:** Rotate 90 degrees

**Parameters:**
- `clockwise` (bool): Whether clockwise

##### Rotate90(bool clockwise)

```csharp
public FilterKernel Rotate90(bool clockwise)
```

**Description:** Rotate 90 degrees

**Parameters:**
- `clockwise` (bool): Whether clockwise

**Return Value:**
- `FilterKernel`: Rotated filter kernel

##### RotateSelf(bool clockwise, int count90)

```csharp
public void RotateSelf(bool clockwise, int count90)
```

**Description:** Rotate

**Parameters:**
- `clockwise` (bool): Whether clockwise
- `count90` (int): Number of 90-degree rotations

##### Rotate(bool clockwise, int count90)

```csharp
public FilterKernel Rotate(bool clockwise, int count90)
```

**Description:** Rotate

**Parameters:**
- `clockwise` (bool): Whether clockwise
- `count90` (int): Number of 90-degree rotations

**Return Value:**
- `FilterKernel`: Rotated filter kernel

##### Sort()

```csharp
public void Sort()
```

**Description:** Sort

---

### FilterMatrix

Filter matrix class that provides image filtering matrix functionality.

```csharp
public class FilterMatrix
```

#### Main Methods

##### Apply(IImage source, IImage target)

```csharp
public void Apply(IImage source, IImage target)
```

**Description:** Apply filter to image

**Parameters:**
- `source` (IImage): Source image
- `target` (IImage): Target image

---

### KernelVector

Kernel vector class that represents vectors in the filter kernel.

```csharp
public class KernelVector
```

#### Properties

##### X

```csharp
public int X { get; set; }
```

**Description:** X coordinate

##### Y

```csharp
public int Y { get; set; }
```

**Description:** Y coordinate

##### Value

```csharp
public int Value { get; set; }
```

**Description:** Vector value

#### Methods

##### Less(KernelVector other)

```csharp
public bool Less(KernelVector other)
```

**Description:** Compare two vectors

**Parameters:**
- `other` (KernelVector): Another vector

**Return Value:**
- `bool`: Comparison result

---

## Usage Examples

### Basic Image Operations

```csharp
// Create RGBA image
var image = new RGBA(256, 256);

// Set pixel colors
uint redColor = 0xFF0000FF;    // Red
uint greenColor = 0xFF00FF00;  // Green
uint blueColor = 0xFFFF0000;   // Blue

image.Set(100, 100, redColor);
image.Set(150, 150, greenColor);
image.Set(200, 200, blueColor);

// Get pixel value
uint pixel = image.At(100, 100);

// Set transparency
image.SetAlpha(100, 100, 128); // Semi-transparent
byte alpha = image.AlphaAt(100, 100);
```

### Image Boundary Operations

```csharp
// Create image with boundaries
var bounds = new Bounds2Int(10, 10, 100, 100);
var image = new RGBA(bounds);

// Check boundaries
Console.WriteLine($"Image bounds: {image.Bounds}");
Console.WriteLine($"Image width: {image.Bounds.Size.X}");
Console.WriteLine($"Image height: {image.Bounds.Size.Y}");
```

### Image Cloning

```csharp
// Create original image
var original = new RGBA(100, 100);
original.Set(50, 50, 0xFFFFFFFF); // White pixel

// Clone image
var cloned = original.Clone();

// Modify cloned image
cloned.Set(50, 50, 0xFF000000); // Black pixel

// Original image is unaffected
uint originalPixel = original.At(50, 50); // Still white
uint clonedPixel = cloned.At(50, 50);     // Now black
```

### Filter Operations

```csharp
// Create filter kernel
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

// Flip operations
var flippedUpDown = kernel.FlipUuDown();
var flippedLeftRight = kernel.FlipLeftRight();

// Rotation operations
var rotated90 = kernel.Rotate90(true);  // Clockwise rotation 90 degrees
var rotated180 = kernel.Rotate(true, 2); // Clockwise rotation 180 degrees

// Sort
kernel.Sort();
```

### Image Processing Pipeline

```csharp
// Create source and target images
var sourceImage = new RGBA(512, 512);
var targetImage = new RGBA(512, 512);

// Fill source image
for (int x = 0; x < 512; x++)
{
    for (int y = 0; y < 512; y++)
    {
        uint color = (uint)((x + y) % 256) | 0xFF000000;
        sourceImage.Set(x, y, color);
    }
}

// Create filter matrix
var filterMatrix = new FilterMatrix();

// Apply filter
filterMatrix.Apply(sourceImage, targetImage);
```

---

## Notes

1. **Coordinate System:** Images use a coordinate system with the top-left corner as the origin
2. **Color Format:** RGBA uses 32-bit integer format, formatted as 0xAARRGGBB
3. **Boundary Checking:** All pixel operations perform boundary checking, operations outside boundaries are ignored
4. **Memory Management:** Image data is stored in byte arrays, pay attention to memory usage
5. **Performance Considerations:** Pay attention to performance for large numbers of pixel operations, avoid frequent boundary checking
6. **Transparency:** Alpha channel value range is 0-255, 0 means completely transparent, 255 means completely opaque

---

## Dependencies

- `JLGames.Infra.Mathx`: Uses mathematical types like Bounds2Int
- `JLGames.Infra`: Uses ICloneable interface
- `System`: Basic types and array operations 