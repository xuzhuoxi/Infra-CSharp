namespace JLGames.Infra.Crypto.ASN1
{
    public struct TLVBlock
    {
        public byte Tag;
        public int Length;
        public byte[] Value;

        public override string ToString()
        {
            if (null == Value)
                return $"{{Tag={Tag}, Length={Length}, Value=Null}}";
            return $"{{Tag={Tag}, Length={Length}, Value=[{string.Join(" ", Value)}]}}";
        }
    }
}