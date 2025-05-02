namespace JLGames.Infra.Net
{
    public static class SockDelegate
    {
        public delegate void FuncMessageHandler(byte[] msg, string remoteAddress, object other);
    }
}