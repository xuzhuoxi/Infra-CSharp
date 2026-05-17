namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// Simple repeating XOR obfuscation (not a standard cipher).
    /// 按字节循环异或的轻量混淆接口；实现快，不适合高安全场景。
    /// </summary>
    public interface IXorCipher : ICipher
    {
    }
}