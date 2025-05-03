using System;

namespace JLGames.Infra.Crypto.ASN1
{
    public struct TLVBlock
    {
        public byte Tag;
        public byte[] Length;
        public int LengthValue;
        public byte[] Value;

        public override string ToString()
        {
            var tagStr = $"[0x{Tag:X2}]";
            var lengthStr = $"[{string.Join(" ", Array.ConvertAll(Length, b => $"0x{b:X2}"))}]({LengthValue})";

            var valueStr = "Null";
            if (null != Value)
            {
                valueStr = $"[{string.Join(" ", Array.ConvertAll(Value, b => $"0x{b:X2}"))}]";
            }

            return $"{{Tag={tagStr}, Length={lengthStr} Value={valueStr}}}";
        }
    }
}