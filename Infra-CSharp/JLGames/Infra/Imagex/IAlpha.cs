namespace JLGames.Infra.Imagex
{
    public interface IAlpha
    {
        /// <summary>
        /// Get alpha value
        /// 取透明度
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        byte AlphaAt(int x, int y);
        
        /// <summary>
        /// Set alpha value
        /// 设置透明度
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alpha"></param>
        void SetAlpha(int x, int y, byte alpha);
    }
}