# JLGames.Infra.Imagex API Documentation

## Overview

The Imagex module provides 2D pixel-buffer and convolution-filter types, including image interfaces, an RGBA image class, per-pixel alpha access, and sparse convolution kernels and filter matrices.

## Namespace

`JLGames.Infra.Imagex`

---

## Interfaces

### IImage

Read/write access to a 2D pixel buffer.

```csharp
public interface IImage
```

#### Properties

##### Bounds

```csharp
Bounds2Int Bounds { get; }
```

**Description:** Data range.

**Type:** `Bounds2Int`

#### Methods

##### At(int x, int y)

```csharp
uint At(int x, int y);
```

**Description:** Get pixel value.

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `uint`: Pixel color as packed uint

##### Set(int x, int y, uint color)

```csharp
void Set(int x, int y, uint color);
```

**Description:** Set pixel value.

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate
- `color` (uint): Packed color value

---

### IAlpha

Read/write access to per-pixel alpha channel.

```csharp
public interface IAlpha
```

#### Methods

##### AlphaAt(int x, int y)

```csharp
byte AlphaAt(int x, int y);
```

**Description:** Get alpha value.

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `byte`: Alpha in range 0–255

##### SetAlpha(int x, int y, byte alpha)

```csharp
void SetAlpha(int x, int y, byte alpha);
```

**Description:** Set alpha value.

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate
- `alpha` (byte): Alpha in range 0–255

---

## Classes

### RGBA

RGBA image backed by a byte array (R, G, B, A per pixel). Implements `ICloneable<RGBA>`, `IImage`, and `IAlpha`.

```csharp
public class RGBA : ICloneable<RGBA>, IImage, IAlpha
```

#### Constructors

##### RGBA(int width, int height)

```csharp
public RGBA(int width, int height)
```

**Description:** Create an image with origin at (0, 0) and the given size.

**Parameters:**
- `width` (int): Image width in pixels
- `height` (int): Image height in pixels

##### RGBA(Bounds2Int rect)

```csharp
public RGBA(Bounds2Int rect)
```

**Description:** Create an image covering the given bounds rectangle.

**Parameters:**
- `rect` (Bounds2Int): Pixel bounds

#### Properties

##### Bounds

```csharp
public Bounds2Int Bounds { get; }
```

**Description:** Data range.

#### Methods

##### Clone()

```csharp
public RGBA Clone()
```

**Description:** Deep-copy pixel data and layout metadata.

**Return Value:**
- `RGBA`: A new independent copy

##### At(int x, int y)

```csharp
public uint At(int x, int y)
```

**Description:** Get pixel value (equivalent to `RgbaAt`). Returns 0 if out of bounds.

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `uint`: Pixel color as packed uint

##### Set(int x, int y, uint color)

```csharp
public void Set(int x, int y, uint color)
```

**Description:** Set pixel value. Ignored if out of bounds.

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate
- `color` (uint): Packed color value

##### AlphaAt(int x, int y)

```csharp
public byte AlphaAt(int x, int y)
```

**Description:** Get alpha value. Returns 0 if out of bounds.

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `byte`: Alpha value

##### SetAlpha(int x, int y, byte alpha)

```csharp
public void SetAlpha(int x, int y, byte alpha)
```

**Description:** Set alpha value. Ignored if out of bounds.

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate
- `alpha` (byte): Alpha value

##### RgbaAt(int x, int y)

```csharp
public uint RgbaAt(int x, int y)
```

**Description:** Get packed RGBA as uint (R in high byte, A in low byte).

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `uint`: Packed color, or 0 if out of bounds

##### PixOffset(int x, int y)

```csharp
public int PixOffset(int x, int y)
```

**Description:** Byte offset of the R channel for the pixel at (x, y) in the backing array. Does not perform bounds checking.

**Parameters:**
- `x` (int): X coordinate
- `y` (int): Y coordinate

**Return Value:**
- `int`: Index into the backing byte array

---

## Filters

### FilterKernel

Sparse convolution kernel as offset/weight vectors. Implements `ICloneable<FilterKernel>`.

```csharp
public class FilterKernel : ICloneable<FilterKernel>
```

#### Properties

##### Len

```csharp
public int Len { get; }
```

**Description:** Number of kernel vectors, or 0 if unset.

#### Methods

##### Less(int i, int j)

```csharp
public bool Less(int i, int j)
```

**Description:** Whether vector at index `i` sorts before index `j`.

**Parameters:**
- `i` (int): First index
- `j` (int): Second index

**Return Value:**
- `bool`: `true` if the first vector precedes the second

##### Less(KernelVector i, KernelVector j)

```csharp
public bool Less(KernelVector i, KernelVector j)
```

**Description:** Whether `i` sorts before `j` (row-major: Y then X).

**Parameters:**
- `i` (KernelVector): First vector
- `j` (KernelVector): Second vector

**Return Value:**
- `bool`: `true` if `i` precedes `j`

##### Swap(int i, int j)

```csharp
public void Swap(int i, int j)
```

**Description:** Swap two vectors by index.

**Parameters:**
- `i` (int): First index
- `j` (int): Second index

##### IndexOfValue(int value)

```csharp
public int IndexOfValue(int value)
```

**Description:** Find the first index whose weight equals `value`.

**Parameters:**
- `value` (int): Weight to search for

**Return Value:**
- `int`: Index, or -1 if not found

##### Clone()

```csharp
public FilterKernel Clone()
```

**Description:** Shallow-copy the vector array.

**Return Value:**
- `FilterKernel`: A new kernel with copied vectors

##### FlipUpDownSelf()

```csharp
public void FlipUpDownSelf()
```

**Description:** Flip itself upside down.

##### FlipUuDown()

```csharp
public FilterKernel FlipUuDown()
```

**Description:** Flip upside down and return a new kernel.

**Return Value:**
- `FilterKernel`: Flipped copy

##### FlipLeftRightSelf()

```csharp
public void FlipLeftRightSelf()
```

**Description:** Flip itself left and right.

##### FlipLeftRight()

```csharp
public FilterKernel FlipLeftRight()
```

**Description:** Flip left and right and return a new kernel.

**Return Value:**
- `FilterKernel`: Flipped copy

##### Rotate90Self(bool clockwise)

```csharp
public void Rotate90Self(bool clockwise)
```

**Description:** Rotate 90 degrees in place.

**Parameters:**
- `clockwise` (bool): `true` for clockwise, `false` for counter-clockwise

##### Rotate90(bool clockwise)

```csharp
public FilterKernel Rotate90(bool clockwise)
```

**Description:** Rotate 90 degrees and return a new kernel.

**Parameters:**
- `clockwise` (bool): `true` for clockwise, `false` for counter-clockwise

**Return Value:**
- `FilterKernel`: Rotated copy

##### RotateSelf(bool clockwise, int count90)

```csharp
public void RotateSelf(bool clockwise, int count90)
```

**Description:** Rotate in place by multiples of 90 degrees.

**Parameters:**
- `clockwise` (bool): `true` for clockwise per step
- `count90` (int): Number of 90° steps (negative values wrap)

##### Rotate(bool clockwise, int count90)

```csharp
public FilterKernel Rotate(bool clockwise, int count90)
```

**Description:** Rotate by multiples of 90 degrees and return a new kernel.

**Parameters:**
- `clockwise` (bool): `true` for clockwise per step
- `count90` (int): Number of 90° steps (negative values wrap)

**Return Value:**
- `FilterKernel`: Rotated copy

##### Sort()

```csharp
public void Sort()
```

**Description:** Sort vectors in row-major order (Y then X).

---

### FilterMatrix

Image convolution filter matrix (sparse kernel + scale/offset metadata). Implements `ICloneable<FilterMatrix>`.

```csharp
public class FilterMatrix : ICloneable<FilterMatrix>
```

#### Properties

##### Kernel

```csharp
public FilterKernel Kernel { get; }
```

**Description:** Sparse convolution kernel.

##### KernelRadius

```csharp
public int KernelRadius { get; }
```

**Description:** Kernel radius (half side length in pixels).

##### KernelSize

```csharp
public int KernelSize { get; }
```

**Description:** Kernel side length (`2 * KernelRadius + 1`).

##### KernelScale

```csharp
public int KernelScale { get; }
```

**Description:** Divisor applied after convolution (sum of weights should equal this).

##### ResultOffset

```csharp
public int ResultOffset { get; }
```

**Description:** Constant offset added to the filtered result.

##### IsScaleMatrix

```csharp
public bool IsScaleMatrix { get; }
```

**Description:** Whether it is a magnification filter.

##### IsPixelUnsafe

```csharp
public bool IsPixelUnsafe { get; }
```

**Description:** Whether the operation result may exceed the pixel range (unsafe pixel value). `true` when `ResultOffset != 0` or any kernel weight is negative.

#### Methods

##### Clone()

```csharp
public FilterMatrix Clone()
```

**Description:** Deep-copy kernel and metadata.

**Return Value:**
- `FilterMatrix`: A new independent copy

##### FlipUpDown()

```csharp
public FilterMatrix FlipUpDown()
```

**Description:** Flip upside down.

**Return Value:**
- `FilterMatrix`: Flipped copy with sorted kernel

##### FlipLeftRight()

```csharp
public FilterMatrix FlipLeftRight()
```

**Description:** Flip left and right.

**Return Value:**
- `FilterMatrix`: Flipped copy with sorted kernel

##### Rotate(bool clockwise, int count90)

```csharp
public FilterMatrix Rotate(bool clockwise, int count90)
```

**Description:** Rotate by multiples of 90 degrees.

**Parameters:**
- `clockwise` (bool): `true` for clockwise per step
- `count90` (int): Number of 90° steps

**Return Value:**
- `FilterMatrix`: Rotated copy with sorted kernel

##### CheckValidity()

```csharp
public bool CheckValidity()
```

**Description:** Check filter template validity (radius, scale, and weight sum).

**Return Value:**
- `bool`: `true` if radius ≥ 1, scale ≥ 0, and weights sum to `KernelScale`

---

### KernelVector

Filter vector unit. Compared in row-major order (Y then X).

```csharp
public struct KernelVector : IComparable<KernelVector>
```

#### Fields

##### X

```csharp
public int X;
```

**Description:** Offset along X from kernel center.

##### Y

```csharp
public int Y;
```

**Description:** Offset along Y from kernel center.

##### Value

```csharp
public int Value;
```

**Description:** Convolution weight at this offset.

#### Methods

##### CompareTo(KernelVector j)

```csharp
public int CompareTo(KernelVector j)
```

**Description:** Compare by sort order (Y then X); never returns 0 for equal keys.

**Parameters:**
- `j` (KernelVector): Other vector

**Return Value:**
- `int`: -1 if this precedes `j`, otherwise 1

##### Less(KernelVector j)

```csharp
public bool Less(KernelVector j)
```

**Description:** Whether this vector sorts before `j` (row-major: Y then X).

**Parameters:**
- `j` (KernelVector): Other vector

**Return Value:**
- `bool`: `true` if this is less in sort order

---

## Usage Examples

### Basic Image Operations

```csharp
// Create RGBA image
var image = new RGBA(256, 256);

// Set pixel colors (packed format 0xRRGGBBAA: R in high byte, A in low byte)
uint redColor = 0xFF0000FF;    // Opaque red
uint greenColor = 0x00FF00FF;  // Opaque green
uint blueColor = 0x0000FFFF;   // Opaque blue

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
// Create image with bounds (Bounds2Int is xMin, yMin, xMax, yMax; max is exclusive)
var bounds = new Bounds2Int(10, 10, 110, 110);
var image = new RGBA(bounds);

// Check bounds
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
cloned.Set(50, 50, 0x000000FF); // Opaque black

// Original image is unaffected
uint originalPixel = original.At(50, 50); // Still white
uint clonedPixel = cloned.At(50, 50);     // Now black
```

### Kernel Vector Comparison

```csharp
var v1 = new KernelVector { X = -1, Y = -1, Value = 1 };
var v2 = new KernelVector { X = 0, Y = -1, Value = 2 };

bool less = v1.Less(v2);       // true (smaller X comes first on the same row)
int cmp = v1.CompareTo(v2);    // -1
```

### Filter Operations

```csharp
var kernel = new FilterKernel();
Console.WriteLine(kernel.Len); // 0 if unset

var clonedKernel = kernel.Clone();

// Geometric transforms on a populated kernel (return new instances; original unchanged)
FilterKernel source = clonedKernel;
var flippedUpDown = source.FlipUuDown();
var flippedLeftRight = source.FlipLeftRight();
var rotated90 = source.Rotate90(true);   // Clockwise 90 degrees
var rotated180 = source.Rotate(true, 2); // Clockwise 180 degrees

source.Sort();
```

### Filter Matrix Transforms

```csharp
FilterMatrix matrix = /* an initialized filter matrix */;

if (matrix.CheckValidity())
{
    Console.WriteLine($"Radius: {matrix.KernelRadius}, Size: {matrix.KernelSize}");
    Console.WriteLine($"Scale: {matrix.KernelScale}, Offset: {matrix.ResultOffset}");
    Console.WriteLine($"Scale filter: {matrix.IsScaleMatrix}, Unsafe pixels: {matrix.IsPixelUnsafe}");

    var flipped = matrix.FlipUpDown();
    var mirrored = matrix.FlipLeftRight();
    var rotated = matrix.Rotate(true, 1);

    FilterKernel kernel = matrix.Kernel;
    int n = kernel.Len;
}
```

---

## Notes

1. **Coordinate System:** Pixel coordinates match `Bounds`. `RGBA(int, int)` uses origin (0, 0). The max corner of `Bounds2Int` is exclusive.
2. **Color Format:** Packed uint is 0xRRGGBBAA (R in the high byte, A in the low byte). Backing storage is 4 bytes per pixel (R, G, B, A).
3. **Boundary Checking:** `At` / `RgbaAt` / `AlphaAt` return 0 when out of bounds; `Set` / `SetAlpha` ignore out-of-bounds writes. `PixOffset` does not check bounds.
4. **Memory Layout:** Row stride is `4 * width` bytes; `PixOffset` is relative to the minimum corner of `Bounds`.
5. **Transparency:** Alpha is 0–255; 0 is fully transparent, 255 is fully opaque.
6. **Kernel Data:** The `FilterKernel` vector array is an internal field with no public setter; `Len` is 0 when unset.
7. **KernelVector Ordering:** `CompareTo` never returns 0 for equal keys (returns 1 when equal).
8. **Filter Matrix:** `FilterMatrix` exposes kernel metadata and geometric transforms; it has no Apply API on `IImage`.

---

## Dependencies

- `JLGames.Infra.Mathx`: Uses mathematical types such as `Bounds2Int`
- `JLGames.Infra`: Uses the `ICloneable<T>` interface
- `System`: `IComparable<T>` and array sorting
