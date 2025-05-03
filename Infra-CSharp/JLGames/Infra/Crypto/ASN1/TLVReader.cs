using System;
using System.IO;

namespace JLGames.Infra.Crypto.ASN1
{
    public class TLVReader : IDisposable
    {
        private readonly BinaryReader m_Reader;

        public TLVReader(BinaryReader reader)
        {
            m_Reader = reader;
        }

        public TLVReader(byte[] data)
        {
            m_Reader = new BinaryReader(new MemoryStream(data));
        }

        public void Dispose()
        {
            m_Reader?.Dispose();
        }

        /// <summary>
        /// 路过指定长度
        /// </summary>
        /// <param name="length"></param>
        public void SkipLength(int length)
        {
            m_Reader.BaseStream.Seek(length, SeekOrigin.Current);
        }

        /// <summary>
        /// 读取 ASN.1 数据块
        /// </summary>
        /// <returns></returns>
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
        /// <param name="expectedTag"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <param name="expectedTag"></param>
        /// <returns>标记值</returns>
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
        /// origBytes: 原始字节数组
        /// </summary>
        /// <returns>数据块字节数量</returns>
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
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] ReadValue(int length)
        {
            return m_Reader.ReadBytes(length);
        }

        /// <summary>
        /// 读取一个字节
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return m_Reader.ReadByte();
        }

        /// <summary>
        /// 是否还有数据未读取
        /// </summary>
        /// <returns></returns>
        public bool HasData()
        {
            return m_Reader.BaseStream.Position < m_Reader.BaseStream.Length;
        }
    }
}