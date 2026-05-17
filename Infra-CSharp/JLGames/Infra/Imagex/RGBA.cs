using JLGames.Infra.Mathx;

namespace JLGames.Infra.Imagex
{
    /// <summary>
    /// RGBA image backed by a byte array (R, G, B, A per pixel).
    /// 以字节数组存储的 RGBA 图像（每像素 R、G、B、A 各一字节）。
    /// </summary>
    public class RGBA : ICloneable<RGBA>, IImage, IAlpha
    {
        private byte[] m_Pix;
        private int m_Stride;
        private Bounds2Int m_Rect;

        /// <inheritdoc/>
        public Bounds2Int Bounds => m_Rect;

        /// <summary>
        /// Create an image with origin at (0, 0) and the given size.
        /// 在原点 (0, 0) 处创建指定宽高的图像。
        /// </summary>
        /// <param name="width">Image width in pixels; 图像宽度（像素）</param>
        /// <param name="height">Image height in pixels; 图像高度（像素）</param>
        public RGBA(int width, int height)
        {
            m_Stride = 4 * width;
            m_Rect = new Bounds2Int(0, 0, width, height);
            m_Pix = new byte[m_Stride * height];
        }

        /// <summary>
        /// Create an image covering the given bounds rectangle.
        /// 按给定边界矩形创建图像。
        /// </summary>
        /// <param name="rect">Pixel bounds; 像素边界</param>
        public RGBA(Bounds2Int rect)
        {
            var size = rect.Size;
            m_Stride = 4 * size.X;
            m_Rect = rect;
            m_Pix = new byte[m_Stride * size.Y];
        }

        /// <summary>
        /// Deep-copy pixel data and layout metadata.
        /// 深拷贝像素数据及布局元数据。
        /// </summary>
        /// <returns>A new independent copy; 新的独立副本</returns>
        public RGBA Clone()
        {
            var clone = new RGBA(m_Rect)
            {
                m_Pix = m_Pix.Clone() as byte[],
                m_Stride = m_Stride
            };
            return clone;
        }

        /// <inheritdoc/>
        public uint At(int x, int y)
        {
            return RgbaAt(x, y);
        }

        /// <inheritdoc/>
        public void Set(int x, int y, uint color)
        {
            if (!m_Rect.Contains(x, y)) return;
            var i = PixOffset(x, y);
            var r = color & 0xFF000000 >> 24;
            var g = color & 0x00FF0000 >> 16;
            var b = color & 0x0000FF00 >> 8;
            var a = color & 0x000000FF;
            m_Pix[i] = (byte) r;
            m_Pix[i + 1] = (byte) g;
            m_Pix[i + 2] = (byte) b;
            m_Pix[i + 3] = (byte) a;
        }

        /// <inheritdoc/>
        public byte AlphaAt(int x, int y)
        {
            if (!m_Rect.Contains(x, y)) return 0;
            var i = PixOffset(x, y);
            return m_Pix[i + 3];
        }

        /// <inheritdoc/>
        public void SetAlpha(int x, int y, byte alpha)
        {
            if (!m_Rect.Contains(x, y)) return;
            var i = PixOffset(x, y);
            m_Pix[i + 3] = alpha;
        }

        /// <summary>
        /// Get packed RGBA as uint (R in high byte, A in low byte).
        /// 读取打包的 RGBA uint（R 在高字节，A 在低字节）。
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <returns>Packed color, or 0 if out of bounds; 打包颜色，越界返回 0</returns>
        public uint RgbaAt(int x, int y)
        {
            if (!m_Rect.Contains(x, y)) return 0;
            var i = PixOffset(x, y);
            return (((uint) m_Pix[i]) << 24) + (((uint) m_Pix[i + 1]) << 16) + ((uint) m_Pix[i + 2] << 8) + m_Pix[i + 3];
        }

        /// <summary>
        /// Byte offset of the R channel for the pixel at (x, y) in the backing array.
        /// 像素 (x, y) 在底层数组中 R 通道的字节偏移。
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <returns>Index into <c>m_Pix</c>; <c>m_Pix</c> 中的下标</returns>
        public int PixOffset(int x, int y)
        {
            return (y - m_Rect.YMin) * m_Stride + (x - m_Rect.XMin) * 4;
        }
    }
}
