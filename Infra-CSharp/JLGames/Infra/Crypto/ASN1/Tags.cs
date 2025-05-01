namespace JLGames.Infra.Crypto.ASN1
{
    /// <summary>
    /// 实际编码（DER/BER）与，ASN.1 标准定义中的Tag值略有不同
    /// 这里定义的是实际编码（DER/BER）的Tag值
    /// </summary>
    public static class DerTags
    {
        public const byte BOOLEAN = 0x01;
        public const byte INTEGER = 0x02;
        public const byte BIT_STRING = 0x03;
        public const byte OCTET_STRING = 0x04;
        public const byte NULL = 0x05;
        public const byte OBJECT_IDENTIFIER = 0x06;
        public const byte ObjectDescriptor = 0x07;
        public const byte EXTERNAL = 0x08;
        public const byte REAL = 0x09;
        public const byte ENUMERATED = 0x0A;
        public const byte EMBEDDED_PDV = 0x0B;
        public const byte UTF8String = 0x0C;
        public const byte RELATIVE_OID = 0x0D;
        public const byte TIME = 0x0E;
        public const byte SEQUENCE = 0x30;
        public const byte SEQUENCE_OF = 0x30;
        public const byte SET = 0x31;
        public const byte SET_OF = 0x31;
        public const byte NumericString = 0x12;
        public const byte PrintableString = 0x13;
        public const byte TeletexString = 0x14;
        public const byte VideotexString = 0x15;
        public const byte IA5String = 0x16;
        public const byte UTCTime = 0x17;
        public const byte GeneralizedTime = 0x18;
        public const byte GraphicString = 0x19;
        public const byte VisibleString = 0x1A;
        public const byte GeneralString = 0x1B;
        public const byte UniversalString = 0x1C;
        public const byte CHARACTER_STRING = 0x1D;
        public const byte BMPString = 0x1E;
    }

    /// <summary>
    /// ASN.1 标准定义中的Tag值
    /// </summary>
    public static class Asn1Tags
    {
        public const byte BOOLEAN = 0x01;
        public const byte INTEGER = 0x02;
        public const byte BIT_STRING = 0x03;
        public const byte OCTET_STRING = 0x04;
        public const byte NULL = 0x05;
        public const byte OBJECT_IDENTIFIER = 0x06;
        public const byte ObjectDescriptor = 0x07;
        public const byte EXTERNAL = 0x08;
        public const byte REAL = 0x09;
        public const byte ENUMERATED = 0x0A;
        public const byte EMBEDDED_PDV = 0x0B;
        public const byte UTF8String = 0x0C;
        public const byte RELATIVE_OID = 0x0D;
        public const byte TIME = 0x0E;
        public const byte SEQUENCE = 0x10;
        public const byte SEQUENCE_OF = 0x10;
        public const byte SET = 0x11;
        public const byte SET_OF = 0x11;
        public const byte NumericString = 0x12;
        public const byte PrintableString = 0x13;
        public const byte TeletexString = 0x14;
        public const byte T61String = 0x14;
        public const byte VideotexString = 0x15;
        public const byte IA5String = 0x16;
        public const byte UTCTime = 0x17;
        public const byte GeneralizedTime = 0x18;
        public const byte GraphicString = 0x19;
        public const byte VisibleString = 0x1A;
        public const byte GeneralString = 0x1B;
        public const byte UniversalString = 0x1C;
        public const byte CHARACTER_STRING = 0x1D;
        public const byte BMPString = 0x1E;
        public const byte DATE = 0x1F;
        public const byte TIME_OF_DAY = 0x20;
        public const byte DATE_TIME = 0x21;
        public const byte DURATION = 0x22;
    }
}