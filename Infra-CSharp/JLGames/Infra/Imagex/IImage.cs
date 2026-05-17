using JLGames.Infra.Mathx;

namespace JLGames.Infra.Imagex
{
    /// <summary>
    /// Read/write access to a 2D pixel buffer.
    /// 二维像素缓冲区的读写访问接口。
    /// </summary>
    public interface IImage
    {
        /// <summary>
        /// Data range.
        /// 数据范围。
        /// </summary>
        Bounds2Int Bounds { get; }

        /// <summary>
        /// Get pixel value.
        /// 取像素值。
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <returns>Pixel color as packed uint; 打包为 uint 的像素颜色</returns>
        uint At(int x, int y);

        /// <summary>
        /// Set pixel value.
        /// 设置像素值。
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <param name="color">Packed color value; 打包颜色值</param>
        void Set(int x, int y, uint color);
    }
}
