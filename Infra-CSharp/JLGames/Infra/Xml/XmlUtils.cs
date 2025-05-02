using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace JLGames.Infra.Xml
{
    public class XmlUtils
    {
        /// <summary>
        /// Serialize to xml string
        /// 序列化为xml字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// Deserialize from xml string to object
        /// 从xml字符串反序列化为对象
        /// </summary>
        /// <param name="xml"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FromXml<T>(string xml)
        {
            return (T) FromXml(xml, typeof(T));
        }

        /// <summary>
        /// Deserialize from xml string to object
        /// 从xml字符串反序列化为对象
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
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