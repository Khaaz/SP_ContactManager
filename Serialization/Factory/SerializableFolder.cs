using System;
using System.Collections.Generic;

namespace Serialization.Factory
{
    /// <summary>
    /// Serializable Folder class
    /// </summary>
    [Serializable]
    public class SerializableFolder : SerializableBase
    {
        public string Name { get; set; }
        public List<SerializableFolder> Folders { get; set; }
        public List<SerializableContact> Contacts { get; set; }

        public SerializableFolder() { }
        public SerializableFolder(DateTime createdAt, DateTime modifiedAt, string name) : base(createdAt, modifiedAt)
        {
            Name = name;
            Folders = new List<SerializableFolder>();
            Contacts = new List<SerializableContact>();
        }
    }
}
