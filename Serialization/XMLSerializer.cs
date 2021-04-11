using Serialization.Factory;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Serialization
{
    /// <summary>
    /// Serializer using XML Serialization
    /// </summary>
    public class XMLSerializer : Serializer
    {
        public override void WriteObjectToStream(Stream outputStream, Object obj)
        {
            if (Object.ReferenceEquals(null, obj))
            {
                return;
            }
            XmlSerializer xmls = new XmlSerializer(typeof(SerializableFolder));
            xmls.Serialize(outputStream, obj);
        }

        public override Object ReadObjectFromStream(Stream inputStream)
        {
            XmlSerializer xmls = new XmlSerializer(typeof(SerializableFolder));
            Object obj = xmls.Deserialize(inputStream);
            return obj;
        }
    }
}
