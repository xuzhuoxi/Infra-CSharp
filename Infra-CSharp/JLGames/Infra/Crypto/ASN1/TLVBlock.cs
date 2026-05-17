using System;

namespace JLGames.Infra.Crypto.ASN1
{
    /// <summary>
    /// ASN.1 TLV 数据块（Tag-Length-Value）。
    /// </summary>
    public struct TLVBlock
    {
        /// <summary>类型标签（Tag）</summary>
        public byte Tag;
        /// <summary>长度字段的原始 DER 编码</summary>
        public byte[] Length;
        /// <summary>解析后的 Value 字节长度</summary>
        public int LengthValue;
        /// <summary>值字段；未读取 Value 时为 <c>null</c></summary>
        public byte[] Value;

        /// <summary>
        /// 返回便于调试的十六进制字符串表示。
        /// </summary>
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