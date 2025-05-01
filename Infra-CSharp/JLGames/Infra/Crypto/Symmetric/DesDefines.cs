namespace JLGames.Infra.Crypto.Symmetric
{
    public static class DesDefines
    {
        /// <summary>
        /// DES的块大小，字节数
        /// </summary>
        public const int BlockSize = 8;

        /// <summary>
        /// DES的密钥长度，字节数
        /// </summary>
        public const int KeySize = 8;

        /// <summary>
        /// 3DES的密钥长度，字节数
        /// 112位密钥会自动追加到168位
        /// </summary>
        public const int TripleKeySize = 24;
    }
}