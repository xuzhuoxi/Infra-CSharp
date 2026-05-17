namespace JLGames.Infra
{
    /// <summary>
    /// Supports creating a copy of type <typeparamref name="T"/>.
    /// 支持创建 <typeparamref name="T"/> 类型的副本。
    /// </summary>
    /// <typeparam name="T">Type of the clone result (typically the implementing type).<br/>克隆结果的类型（通常为实现类型本身）。</typeparam>
    public interface ICloneable<out T>
    {
        /// <summary>
        /// Creates a copy of the current instance.
        /// 创建当前实例的副本。
        /// </summary>
        /// <returns>A new instance; semantics (deep vs shallow) are defined by the implementer.<br/>新实例；深拷贝或浅拷贝由实现方定义。</returns>
        T Clone();
    }
}
