using System.IO;

namespace JLGames.Infra.Crypto.ASN1
{
    public class TLVAdvancedReader : TLVReader
    {
        public TLVAdvancedReader(BinaryReader reader) : base(reader)
        {
        }

        public TLVAdvancedReader(byte[] data) : base(data)
        {
        }

        /// <summary>
        /// 验证标记后读取一个整数数据块
        /// </summary>
        /// <returns></returns>
        public byte[] ReadInteger()
        {
            return ReadBlock(DerTags.INTEGER).Value;
        }

        /// <summary>
        /// 验证标记后读取一个布尔数据块
        /// </summary>
        /// <returns></returns>
        public byte[] ReadBoolean()
        {
            return ReadBlock(DerTags.BOOLEAN).Value;
        }

        /// <summary>
        /// 验证标记后读取一个SEQUENCE数据块
        /// </summary>
        /// <returns></returns>
        public TLVBlock ReadSequence(bool includeValue)
        {
            if (includeValue)
                return ReadBlock(DerTags.SEQUENCE);
            return ReadBlockNoValue(DerTags.SEQUENCE);
        }

        /// <summary>
        /// 读取算法标识符，实际上是SEQUENCE
        /// </summary>
        /// <returns></returns>
        public TLVBlock ReadAlgorithmIdentifier(bool includeValue)
        {
            return ReadSequence(includeValue);
        }

        /// <summary>
        /// 读取 OCTET STRING 数据块
        /// </summary>
        /// <param name="includeValue"></param>
        /// <returns></returns>
        public TLVBlock ReadOctetString(bool includeValue)
        {
            if (includeValue)
                return ReadBlock(DerTags.OCTET_STRING);
            return ReadBlockNoValue(DerTags.OCTET_STRING);
        }

        public TLVBlock ReadBitString(bool includeValue)
        {
            if (includeValue) return ReadBlock(DerTags.BIT_STRING);
            return ReadBlockNoValue(DerTags.BIT_STRING);
        }

        public TLVBlock ReadObjectIdentifier(bool includeValue)
        {
            if (includeValue) return ReadBlock(DerTags.OBJECT_IDENTIFIER);
            return ReadBlockNoValue(DerTags.OBJECT_IDENTIFIER);
        }
    }
}