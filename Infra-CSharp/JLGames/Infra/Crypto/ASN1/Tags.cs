namespace JLGames.Infra.Crypto.ASN1
{
    /// <summary>
    /// DER/BER 实际编码中的 Universal Tag 常量（含构造/上下文位，如 SEQUENCE 为 0x30）。
    /// 与 ASN.1 标准中的逻辑 Tag 编号可能不同，见 <see cref="Asn1Tags"/>。
    /// </summary>
    public static class DerTags
    {
        /// <summary>BOOLEAN (0x01)</summary>
        public const byte BOOLEAN = 0x01;
        /// <summary>INTEGER (0x02)</summary>
        public const byte INTEGER = 0x02;
        /// <summary>BIT STRING (0x03)</summary>
        public const byte BIT_STRING = 0x03;
        /// <summary>OCTET STRING (0x04)</summary>
        public const byte OCTET_STRING = 0x04;
        /// <summary>NULL (0x05)</summary>
        public const byte NULL = 0x05;
        /// <summary>OBJECT IDENTIFIER (0x06)</summary>
        public const byte OBJECT_IDENTIFIER = 0x06;
        /// <summary>ObjectDescriptor (0x07)</summary>
        public const byte ObjectDescriptor = 0x07;
        /// <summary>EXTERNAL (0x08)</summary>
        public const byte EXTERNAL = 0x08;
        /// <summary>REAL (0x09)</summary>
        public const byte REAL = 0x09;
        /// <summary>ENUMERATED (0x0A)</summary>
        public const byte ENUMERATED = 0x0A;
        /// <summary>EMBEDDED PDV (0x0B)</summary>
        public const byte EMBEDDED_PDV = 0x0B;
        /// <summary>UTF8String (0x0C)</summary>
        public const byte UTF8String = 0x0C;
        /// <summary>RELATIVE OID (0x0D)</summary>
        public const byte RELATIVE_OID = 0x0D;
        /// <summary>TIME (0x0E)</summary>
        public const byte TIME = 0x0E;
        /// <summary>SEQUENCE / SEQUENCE OF（构造类型，DER 编码为 0x30）</summary>
        public const byte SEQUENCE = 0x30;
        /// <summary>SEQUENCE OF（与 SEQUENCE 同 Tag）</summary>
        public const byte SEQUENCE_OF = 0x30;
        /// <summary>SET / SET OF（构造类型，DER 编码为 0x31）</summary>
        public const byte SET = 0x31;
        /// <summary>SET OF（与 SET 同 Tag）</summary>
        public const byte SET_OF = 0x31;
        /// <summary>NumericString (0x12)</summary>
        public const byte NumericString = 0x12;
        /// <summary>PrintableString (0x13)</summary>
        public const byte PrintableString = 0x13;
        /// <summary>TeletexString (0x14)</summary>
        public const byte TeletexString = 0x14;
        /// <summary>VideotexString (0x15)</summary>
        public const byte VideotexString = 0x15;
        /// <summary>IA5String (0x16)</summary>
        public const byte IA5String = 0x16;
        /// <summary>UTCTime (0x17)</summary>
        public const byte UTCTime = 0x17;
        /// <summary>GeneralizedTime (0x18)</summary>
        public const byte GeneralizedTime = 0x18;
        /// <summary>GraphicString (0x19)</summary>
        public const byte GraphicString = 0x19;
        /// <summary>VisibleString (0x1A)</summary>
        public const byte VisibleString = 0x1A;
        /// <summary>GeneralString (0x1B)</summary>
        public const byte GeneralString = 0x1B;
        /// <summary>UniversalString (0x1C)</summary>
        public const byte UniversalString = 0x1C;
        /// <summary>CHARACTER STRING (0x1D)</summary>
        public const byte CHARACTER_STRING = 0x1D;
        /// <summary>BMPString (0x1E)</summary>
        public const byte BMPString = 0x1E;
    }

    /// <summary>
    /// ASN.1 标准定义中的 Universal Tag 编号（未含 DER 构造位）。
    /// </summary>
    public static class Asn1Tags
    {
        /// <summary>BOOLEAN (0x01)</summary>
        public const byte BOOLEAN = 0x01;
        /// <summary>INTEGER (0x02)</summary>
        public const byte INTEGER = 0x02;
        /// <summary>BIT STRING (0x03)</summary>
        public const byte BIT_STRING = 0x03;
        /// <summary>OCTET STRING (0x04)</summary>
        public const byte OCTET_STRING = 0x04;
        /// <summary>NULL (0x05)</summary>
        public const byte NULL = 0x05;
        /// <summary>OBJECT IDENTIFIER (0x06)</summary>
        public const byte OBJECT_IDENTIFIER = 0x06;
        /// <summary>ObjectDescriptor (0x07)</summary>
        public const byte ObjectDescriptor = 0x07;
        /// <summary>EXTERNAL (0x08)</summary>
        public const byte EXTERNAL = 0x08;
        /// <summary>REAL (0x09)</summary>
        public const byte REAL = 0x09;
        /// <summary>ENUMERATED (0x0A)</summary>
        public const byte ENUMERATED = 0x0A;
        /// <summary>EMBEDDED PDV (0x0B)</summary>
        public const byte EMBEDDED_PDV = 0x0B;
        /// <summary>UTF8String (0x0C)</summary>
        public const byte UTF8String = 0x0C;
        /// <summary>RELATIVE OID (0x0D)</summary>
        public const byte RELATIVE_OID = 0x0D;
        /// <summary>TIME (0x0E)</summary>
        public const byte TIME = 0x0E;
        /// <summary>SEQUENCE / SEQUENCE OF (0x10)</summary>
        public const byte SEQUENCE = 0x10;
        /// <summary>SEQUENCE OF（与 SEQUENCE 同 Tag）</summary>
        public const byte SEQUENCE_OF = 0x10;
        /// <summary>SET / SET OF (0x11)</summary>
        public const byte SET = 0x11;
        /// <summary>SET OF（与 SET 同 Tag）</summary>
        public const byte SET_OF = 0x11;
        /// <summary>NumericString (0x12)</summary>
        public const byte NumericString = 0x12;
        /// <summary>PrintableString (0x13)</summary>
        public const byte PrintableString = 0x13;
        /// <summary>TeletexString (0x14)</summary>
        public const byte TeletexString = 0x14;
        /// <summary>T61String（与 TeletexString 同 Tag，0x14）</summary>
        public const byte T61String = 0x14;
        /// <summary>VideotexString (0x15)</summary>
        public const byte VideotexString = 0x15;
        /// <summary>IA5String (0x16)</summary>
        public const byte IA5String = 0x16;
        /// <summary>UTCTime (0x17)</summary>
        public const byte UTCTime = 0x17;
        /// <summary>GeneralizedTime (0x18)</summary>
        public const byte GeneralizedTime = 0x18;
        /// <summary>GraphicString (0x19)</summary>
        public const byte GraphicString = 0x19;
        /// <summary>VisibleString (0x1A)</summary>
        public const byte VisibleString = 0x1A;
        /// <summary>GeneralString (0x1B)</summary>
        public const byte GeneralString = 0x1B;
        /// <summary>UniversalString (0x1C)</summary>
        public const byte UniversalString = 0x1C;
        /// <summary>CHARACTER STRING (0x1D)</summary>
        public const byte CHARACTER_STRING = 0x1D;
        /// <summary>BMPString (0x1E)</summary>
        public const byte BMPString = 0x1E;
        /// <summary>DATE (0x1F)</summary>
        public const byte DATE = 0x1F;
        /// <summary>TIME OF DAY (0x20)</summary>
        public const byte TIME_OF_DAY = 0x20;
        /// <summary>DATE TIME (0x21)</summary>
        public const byte DATE_TIME = 0x21;
        /// <summary>DURATION (0x22)</summary>
        public const byte DURATION = 0x22;
    }
}
