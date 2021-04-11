using Data;
using Data.Structures;
using System;

namespace ContactManager
{
    /// <summary>
    /// Handles displaying the folder tree
    /// </summary>
    class Display
    {
        /// <summary>
        /// Display the folder tree from root
        /// </summary>
        /// <param name="manager"></param>
        internal void All(Manager manager)
        {
            Folder(manager.Root, 0);
            AllContacts(manager.Root, 1);
            AllSubFolders(manager.Root, 1);
        }

        /// <summary>
        /// Display the folder tree from a particular folder
        /// </summary>
        /// <param name="folder">The folder to display from</param>
        internal void From(Folder folder)
        {
            AllSubFolders(folder, 0);
        }

        /// <summary>
        /// Display the folder, the contacts and all subfolders from the folder
        /// </summary>
        /// <param name="folder">The folder to display</param>
        /// <param name="depth">The depth level</param>
        void AllSubFolders(Folder folder, int depth)
        {
            foreach (Folder subf in folder.Folders)
            {
                Folder(subf, depth);
                AllContacts(subf, depth + 1);
                AllSubFolders(subf, depth + 1);
            }
        }

        /// <summary>
        /// Display all contacts for this folder
        /// </summary>
        /// <param name="folder">The folder to display all contacts from</param>
        /// <param name="depth">The depth</param>
        void AllContacts(Folder folder, int depth)
        {
            foreach (Contact c in folder.Contacts)
            {
                Contact(c, depth);
            }
        }

        /// <summary>
        /// Display the folder
        /// </summary>
        /// <param name="folder">The folder to display</param>
        /// <param name="depth">THe depth</param>
        void Folder(Folder folder, int depth)
        {
            Console.WriteLine("{0}[D] {1} - {2}", RenderDepth(depth), folder.Name, folder.CreatedAt.ToString());
        }

        /// <summary>
        /// Display the contatc
        /// </summary>
        /// <param name="contact">The contact to display</param>
        /// <param name="depth">THe depth</param>
        void Contact(Contact contact, int depth)
        {
            Console.WriteLine("{0}[C] {1} {2} ({3}), Email:{4}, Link:{5}", RenderDepth(depth), contact.FirstName, contact.LastName, contact.Company, contact.Email, contact.Link);

        }

        /// <summary>
        /// Render depth by adding \t
        /// </summary>
        /// <param name="depth">The depth</param>
        /// <returns></returns>
        string RenderDepth(int depth)
        {
            string renderedDepth = "";
            for (int i = 0; i < depth; ++i)
            {
                renderedDepth += "\t";
            }
            return renderedDepth;
        }
    }
}
