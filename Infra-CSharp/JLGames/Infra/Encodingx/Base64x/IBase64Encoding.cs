namespace JLGames.Infra.Encodingx.Base64x
{
    public interface IBase64Encoding
    {
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string EncodeToString(byte[] input);

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string EncodeToString(string input);

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        byte[] EncodeToBytes(byte[] input);

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        byte[] EncodeToBytes(string input);


        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        byte[] DecodeBytesFrom(byte[] input);

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        byte[] DecodeBytesFrom(string input);

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string DecodeStringFrom(byte[] input);

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string DecodeStringFrom(string input);
    }
}