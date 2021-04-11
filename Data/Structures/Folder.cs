using System;
using System.Collections.Generic;

namespace Data.Structures
{
    /// <summary>
    /// Folder that contains contacts and subfolders
    /// </summary>
    public class Folder : Base
    {
        public string Name { get; private set; }
        public List<Folder> Folders { get; private set; }
        public List<Contact> Contacts { get; private set; }

        /// <summary>
        /// Creates a Folder with default createdAt and modifedAt
        /// </summary>
        /// <param name="name">Name of the folder</param>
        public Folder(string name) : base()
        {
            init(name);
        }

        /// <summary>
        /// Creates a Folder wiuth a given createdAt and modifiedAt
        /// </summary>
        /// <param name="createdAt">Date de creation</param>
        /// <param name="modifiedAt">Date de derniere modification</param>
        /// <param name="name">Name of the folder</param>
        public Folder(DateTime createdAt, DateTime modifiedAt, string name) : base(createdAt, modifiedAt)
        {
            init(name);
        }

        /// <summary>
        /// Initialise the folder with a name
        /// </summary>
        /// <param name="name">Name of the folder</param>
        private void init(string name)
        {
            if (name.Contains("/"))
            {
                throw new ArgumentException("Name can't contain /");
            }
            Name = name;
            Folders = new List<Folder>();
            Contacts = new List<Contact>();
        }

        bool EmptyFolder()
        {
            return Folders.Count == 0;
        }

        bool EmptyContacts()
        {
            return Contacts.Count == 0;
        }

        bool Empty()
        {
            return EmptyFolder() && EmptyContacts();
        }

        /// <summary>
        /// Add a folder to the subfolder list
        /// </summary>
        /// <param name="f"></param>
        public void AddFolder(Folder f)
        {
            Folders.Add(f);
        }

        /// <summary>
        /// Add a contact to the contact list
        /// </summary>
        /// <param name="c"></param>
        public void AddContact(Contact c)
        {
            Contacts.Add(c);
        }
    }
}
