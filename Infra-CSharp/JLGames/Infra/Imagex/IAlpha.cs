namespace JLGames.Infra.Imagex
{
    /// <summary>
    /// Read/write access to per-pixel alpha channel.
    /// 逐像素透明度通道的读写访问接口。
    /// </summary>
    public interface IAlpha
    {
        /// <summary>
        /// Get alpha value.
        /// 取透明度。
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <returns>Alpha in range 0–255; 透明度，取值 0–255</returns>
        byte AlphaAt(int x, int y);

        /// <summary>
        /// Set alpha value.
        /// 设置透明度。
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <param name="alpha">Alpha in range 0–255; 透明度，取值 0–255</param>
        void SetAlpha(int x, int y, byte alpha);
    }
}
