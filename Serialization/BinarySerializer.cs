using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialization
{
    /// <summary>
    /// Serializer using Binary Serialization
    /// </summary>
    public class BinarySerializer : Serializer
    {
        public override void WriteObjectToStream(Stream outputStream, Object obj)
        {
            if (Object.ReferenceEquals(null, obj))
            {
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(outputStream, obj);
        }

        public override Object ReadObjectFromStream(Stream inputStream)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            Object obj = binForm.Deserialize(inputStream);
            return obj;
        }
    }
}
