using System;
using System.IO;

namespace JLGames.Infra.Crypto.ASN1
{
    /// <summary>
    /// ASN.1 DER/BER 的 TLV（Tag-Length-Value）流式读取器。
    /// </summary>
    public class TLVReader : IDisposable
    {
        private readonly BinaryReader m_Reader;

        /// <summary>
        /// 基于已有 <see cref="BinaryReader"/> 创建读取器。
        /// </summary>
        /// <param name="reader">底层二进制读取器</param>
        public TLVReader(BinaryReader reader)
        {
            m_Reader = reader;
        }

        /// <summary>
        /// 基于字节数组创建读取器。
        /// </summary>
        /// <param name="data">DER/BER 编码数据</param>
        public TLVReader(byte[] data)
        {
            m_Reader = new BinaryReader(new MemoryStream(data));
        }

        /// <summary>
        /// 释放底层 <see cref="BinaryReader"/>。
        /// </summary>
        public void Dispose()
        {
            m_Reader?.Dispose();
        }

        /// <summary>
        /// 跳过指定字节数（相对当前流位置向前移动）。
        /// </summary>
        /// <param name="length">要跳过的字节数</param>
        public void SkipLength(int length)
        {
            m_Reader.BaseStream.Seek(length, SeekOrigin.Current);
        }

        /// <summary>
        /// 读取 ASN.1 数据块
        /// </summary>
        /// <returns>包含 Tag、Length 与 Value 的数据块</returns>
        public TLVBlock ReadBlock()
        {
            var tag = ReadTag();
            var length = ReadLength(out var lengthValue);
            var value = ReadValue(lengthValue);
            return new TLVBlock
            {
                Tag = tag, Length = length, Value = value, LengthValue = lengthValue
            };
        }

        /// <summary>
        /// 验证标记后读取 ASN.1 数据块
        /// </summary>
        /// <param name="expectedTag">期望的 Tag 值</param>
        /// <returns>验证通过后的数据块；长度为 0 时 Value 为 <c>null</c></returns>
        /// <exception cref="ArgumentException">Tag 与期望值不符</exception>
        public TLVBlock ReadBlock(byte expectedTag)
        {
            var tag = ReadTag(expectedTag);
            var length = ReadLength(out var lengthValue);
            if (lengthValue == 0)
            {
                return new TLVBlock
                {
                    Tag = tag, Length = length, LengthValue = lengthValue
                };
            }

            return new TLVBlock
            {
                Tag = tag, Length = length, Value = ReadValue(lengthValue), LengthValue = lengthValue
            };
        }

        /// <summary>
        /// 读取 ASN.1 数据块
        /// 不包含Value部分
        /// </summary>
        /// <returns>仅含 Tag 与 Length 的数据块</returns>
        public TLVBlock ReadBlockNoValue()
        {
            var tag = ReadTag();
            var length = ReadLength(out var lengthValue);
            return new TLVBlock
            {
                Tag = tag, Length = length, LengthValue = lengthValue
            };
        }

        /// <summary>
        /// 验证标记后读取 ASN.1 数据块
        /// 不包含Value部分
        /// </summary>
        /// <param name="expectedTag">期望的 Tag 值</param>
        /// <returns>验证通过后的数据块（不含 Value）</returns>
        public TLVBlock ReadBlockNoValue(byte expectedTag)
        {
            var tag = ReadTag(expectedTag);
            var length = ReadLength(out var lengthValue);
            return new TLVBlock
            {
                Tag = tag, Length = length, LengthValue = lengthValue
            };
        }


        /// <summary>
        /// 读取 ASN.1 标记（Tag）
        /// </summary>
        /// <returns>标记值</returns>
        public byte ReadTag()
        {
            if (!HasData()) throw new EndOfStreamException("Unexpected end of ASN.1 data.");
            return m_Reader.ReadByte();
        }

        /// <summary>
        /// 读取 ASN.1 标记（Tag）并验证
        /// </summary>
        /// <param name="expectedTag">期望的 Tag 值</param>
        /// <returns>读取到的 Tag</returns>
        /// <exception cref="ArgumentException">Tag 与期望值不符</exception>
        public byte ReadTag(byte expectedTag)
        {
            if (!HasData()) throw new EndOfStreamException("Unexpected end of ASN.1 data.");
            var tag = m_Reader.ReadByte();
            if (tag != expectedTag) throw new ArgumentException($"ASN.1 format error: Expected tag {expectedTag:X2}, but got {tag:X2}");
            return tag;
        }

        /// <summary>
        /// 读取 ASN.1 数据长度（Length）
        /// 短格式: 第一个1字节记录长度(小于128时，即小于0x80)
        /// 长格式: 第一个1字节记录长度数据字节数量n, 余下n个字节记录数量，强制大端
        /// </summary>
        /// <param name="lengthValue">解析出的 Value 字节长度</param>
        /// <returns>Length 字段的原始编码字节</returns>
        public byte[] ReadLength(out int lengthValue)
        {
            var len = m_Reader.ReadByte();

            // 短格式
            if ((len & 0x80) == 0)
            {
                lengthValue = len;
                return new[] { len };
            }

            // 长格式
            var lengthBytes = len & 0x7F;
            lengthValue = 0;
            var length = new byte[lengthBytes + 1];
            length[0] = len;
            for (var i = 0; i < lengthBytes; i++)
            {
                length[i + 1] = m_Reader.ReadByte();
                lengthValue = (lengthValue << 8) | length[i + 1];
            }

            return length;
        }

        /// <summary>
        /// 读取 ASN.1 数据（Value）
        /// </summary>
        /// <param name="length">Value 字节长度</param>
        /// <returns>Value 原始字节</returns>
        public byte[] ReadValue(int length)
        {
            return m_Reader.ReadBytes(length);
        }

        /// <summary>
        /// 读取一个字节
        /// </summary>
        /// <returns>读取的字节</returns>
        public byte ReadByte()
        {
            return m_Reader.ReadByte();
        }

        /// <summary>
        /// 是否还有数据未读取
        /// </summary>
        /// <returns>尚有未读字节时返回 <c>true</c></returns>
        public bool HasData()
        {
            return m_Reader.BaseStream.Position < m_Reader.BaseStream.Length;
        }
    }
}