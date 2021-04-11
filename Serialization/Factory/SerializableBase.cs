using System;

namespace Serialization.Factory
{
    [Serializable]
    public class SerializableBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public SerializableBase() { }
        public SerializableBase(DateTime createdAt, DateTime modifiedAt)
        {
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }
    }
}
