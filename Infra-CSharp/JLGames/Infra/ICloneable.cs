namespace JLGames.Infra
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}