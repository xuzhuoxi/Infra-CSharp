using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace JLGames.Infra.Xml
{
    /// <summary>
    /// XML serialization helpers based on <see cref="XmlSerializer"/>.
    /// 基于 <see cref="XmlSerializer"/> 的 XML 序列化与反序列化工具。
    /// </summary>
    public static class XmlUtils
    {
        /// <summary>
        /// Serialize an object to an XML string.
        /// 将对象序列化为 XML 字符串。
        /// </summary>
        /// <param name="obj">Object to serialize; returns an empty string when null.<br/>待序列化的对象；为 null 时返回空字符串。</param>
        /// <returns>XML text, or an empty string if <paramref name="obj"/> is null.<br/>XML 文本；<paramref name="obj"/> 为 null 时为空字符串。</returns>
        public static string ToXml(object obj)
        {
            if (null == obj) return "";
            try
            {
                var sb = new StringBuilder();
                using (var writer = new StringWriter(sb))
                {
                    var xml = new XmlSerializer(obj.GetType());
                    xml.Serialize(writer, obj);
                    writer.Close();
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Deserialize an XML string to an instance of <typeparamref name="T"/>.
        /// 将 XML 字符串反序列化为 <typeparamref name="T"/> 类型实例。
        /// </summary>
        /// <typeparam name="T">Target type (must be concrete and XML-serializable).<br/>目标类型（须为可实例化的可序列化类型）。</typeparam>
        /// <param name="xml">XML text; returns default when null or empty.<br/>XML 文本；为 null 或空时返回默认值。</param>
        /// <returns>Deserialized instance, or default(<typeparamref name="T"/>) when <paramref name="xml"/> is null or empty.<br/>反序列化结果；<paramref name="xml"/> 为 null 或空时为 default(<typeparamref name="T"/>)。</returns>
        public static T FromXml<T>(string xml)
        {
            return (T)FromXml(xml, typeof(T));
        }

        /// <summary>
        /// Deserialize an XML string to an instance of the specified type.
        /// 将 XML 字符串反序列化为指定类型的实例。
        /// </summary>
        /// <param name="xml">XML text; returns null when null or empty.<br/>XML 文本；为 null 或空时返回 null。</param>
        /// <param name="type">Target type (must be concrete and XML-serializable).<br/>目标类型（须为可实例化的可序列化类型）。</param>
        /// <returns>Deserialized instance, or null when <paramref name="xml"/> is null or empty.<br/>反序列化结果；<paramref name="xml"/> 为 null 或空时为 null。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.<br/><paramref name="type"/> 为 null。</exception>
        /// <exception cref="ArgumentException"><paramref name="type"/> is abstract and cannot be instantiated.<br/><paramref name="type"/> 为抽象类型，无法实例化。</exception>
        public static object FromXml(string xml, System.Type type)
        {
            if (string.IsNullOrEmpty(xml))
                return null;
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (type.IsAbstract)
                throw new ArgumentException("Cannot deserialize XML to new instances of type '" + type.Name + ".'");
            try
            {
                using (var reader = new StringReader(xml))
                {
                    return new XmlSerializer(type).Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}