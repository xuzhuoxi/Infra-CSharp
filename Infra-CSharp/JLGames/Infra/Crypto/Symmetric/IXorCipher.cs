namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// 异常混淆器：Data Encrytion Standard（数据加密标准），对应算法是DEA
    /// 特点：
    ///   1. 快
    ///   2. 不安全
    /// </summary>
    public interface IXorCipher : ICipher
    {
    }
}