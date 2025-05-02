using JLGames.Infra.Mathx;

namespace JLGames.Infra.Imagex
{
    public interface IImage
    {
        /// <summary>
        /// Data range
        /// 数据范围定义
        /// </summary>
        Bounds2Int Bounds { get; }

        /// <summary>
        /// Get pixel value
        /// 取像素值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        uint At(int x, int y);

        /// <summary>
        /// Set pixel value
        /// 设置像素值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        void Set(int x, int y, uint color);
    }
}