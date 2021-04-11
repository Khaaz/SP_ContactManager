using Data.Structures;

namespace Serialization.Factory
{
    /// <summary>
    /// Folder extention methods for serialization
    /// </summary>
    public static class FolderFactory
    {
        /// <summary>
        /// Extention method to create a SerializableFolder from a Folder
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static SerializableFolder CreateSerializable(this Folder folder)
        {
            SerializableFolder sFolder = new SerializableFolder(folder.CreatedAt, folder.ModifiedAt, folder.Name);

            foreach (var c in folder.Contacts)
            {
                sFolder.Contacts.Add(c.CreateSerializable());
            }

            foreach (var f in folder.Folders)
            {
                sFolder.Folders.Add(f.CreateSerializable());
            }

            return sFolder;
        }

        /// <summary>
        /// Extention method to create a Fodler fron a SerializableFolder
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static Folder CreateInstance(this SerializableFolder folder)
        {
            Folder iFolder = new Folder(folder.CreatedAt, folder.ModifiedAt, folder.Name);

            foreach (var c in folder.Contacts)
            {
                iFolder.AddContact(c.CreateInstance());
            }

            foreach (var f in folder.Folders)
            {
                iFolder.AddFolder(f.CreateInstance());
            }

            return iFolder;
        }
    }
}
