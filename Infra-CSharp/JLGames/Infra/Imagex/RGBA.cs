using JLGames.Infra.Mathx;

namespace JLGames.Infra.Imagex
{
    public class RGBA : ICloneable<RGBA>, IImage, IAlpha
    {
        private byte[] m_Pix;
        private int m_Stride;
        private Bounds2Int m_Rect;

        public Bounds2Int Bounds => m_Rect;

        public RGBA(int width, int height)
        {
            m_Stride = 4 * width;
            m_Rect = new Bounds2Int(0, 0, width, height);
            m_Pix = new byte[m_Stride * height];
        }

        public RGBA(Bounds2Int rect)
        {
            var size = rect.Size;
            m_Stride = 4 * size.X;
            m_Rect = rect;
            m_Pix = new byte[m_Stride * size.Y];
        }

        public RGBA Clone()
        {
            var clone = new RGBA(m_Rect)
            {
                m_Pix = m_Pix.Clone() as byte[],
                m_Stride = m_Stride
            };
            return clone;
        }

        public uint At(int x, int y)
        {
            return RgbaAt(x, y);
        }

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

        public byte AlphaAt(int x, int y)
        {
            if (!m_Rect.Contains(x, y)) return 0;
            var i = PixOffset(x, y);
            return m_Pix[i + 3];
        }

        public void SetAlpha(int x, int y, byte alpha)
        {
            if (!m_Rect.Contains(x, y)) return;
            var i = PixOffset(x, y);
            m_Pix[i + 3] = alpha;
        }

        public uint RgbaAt(int x, int y)
        {
            if (!m_Rect.Contains(x, y)) return 0;
            var i = PixOffset(x, y);
            return (((uint) m_Pix[i]) << 24) + (((uint) m_Pix[i + 1]) << 16) + ((uint) m_Pix[i + 2] << 8) + m_Pix[i + 3];
        }

        public int PixOffset(int x, int y)
        {
            return (y - m_Rect.YMin) * m_Stride + (x - m_Rect.XMin) * 4;
        }
    }
}