using System.IO;
using System.Linq;

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
        /// 验证标记后读取一个布尔数据块
        /// </summary>
        /// <returns></returns>
        public byte[] ReadBoolean()
        {
            return ReadBlock(DerTags.BOOLEAN).Value;
        }

        /// <summary>
        /// 验证标记后读取一个NULL数据块
        /// </summary>
        /// <returns></returns>
        public TLVBlock ReadNull()
        {
            return ReadBlock(DerTags.NULL);
        }

        /// <summary>
        /// 验证标记后读取一个整数数据块
        /// 如果有前导零，则移除
        /// </summary>
        /// <returns></returns>
        public byte[] ReadInteger()
        {
            var integerValue = ReadBlock(DerTags.INTEGER).Value;
            
            // 没有前导零，直接返回
            if (!(integerValue.Length > 1 && integerValue[0] == 0x00))
            {
                return integerValue;
            }

            // 移除前导零
            return integerValue.Skip(1).ToArray();
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
        /// 读取算法标识符AlgorithmIdentifier，ASN.1结构：
        /// AlgorithmIdentifier ::= SEQUENCE {
        ///     algorithm       OBJECT IDENTIFIER,              -- 公钥算法OID，如RSA的OID为 1.2.840.113549.1.1.1
        ///     parameters      ANY DEFINED BY algorithm OPTIONAL
        /// }
        /// </summary>
        /// <returns></returns>
        public TLVBlock ReadRsaOid()
        {
            ReadSequence(false);
            var oid = ReadObjectIdentifier(true);
            ReadNull();
            return oid;
        }

        /// <summary>
        /// 读取 OCTET STRING 数据块
        /// 不会包含前导比特数
        /// </summary>
        /// <param name="includeValue"></param>
        /// <returns></returns>
        public TLVBlock ReadOctetString(bool includeValue)
        {
            if (includeValue)
                return ReadBlock(DerTags.OCTET_STRING);
            return ReadBlockNoValue(DerTags.OCTET_STRING);
        }

        /// <summary>
        /// 读取 BIT STRING 数据块
        /// Value[0]: 必定是前导比特数
        /// </summary>
        /// <returns></returns>
        public TLVBlock ReadBitString()
        {
            var tag = ReadTag(DerTags.BIT_STRING);
            var length = ReadLength(out var lengthValue);
            var value = ReadValue(lengthValue);

            var leadingBits = value[0]; // 获取前导比特数
            value = value.Skip(1).ToArray();

            if (leadingBits > 0) // 前导比特数 >0 
            {
                var last = value[value.Length - 1];
                last &= (byte)(0xFF << leadingBits); // 移除无效的位数
                value[value.Length - 1] = last;
            }

            return new TLVBlock
            {
                Tag = tag,
                Length = length,
                LengthValue = lengthValue,
                Value = value
            };
        }

        public TLVBlock ReadObjectIdentifier(bool includeValue)
        {
            if (includeValue) return ReadBlock(DerTags.OBJECT_IDENTIFIER);
            return ReadBlockNoValue(DerTags.OBJECT_IDENTIFIER);
        }
    }
}