using Data;
using Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace ContactManager
{
    /// <summary>
    /// Handles the console logic
    /// </summary>
    class Engine
    {
        private Manager Manager { get; set; }
        private Display Display { get; set; }
        private Menu Menu { get; set; }
        private Serializer Serializer { get; set; }


        /// <summary>
        /// Construct an Engine
        /// </summary>
        /// <param name="manager">Data Manager</param>
        public Engine(Manager manager, Serializer serializer)
        {
            this.Manager = manager;
            this.Serializer = serializer;
            this.Menu = new Menu(this);
            this.Display = new Display();
        }

        /// <summary>
        /// Run the program
        /// </summary>
        public void Run()
        {
            Menu.Run();
        }

        /// <summary>
        /// Logic for displaying the folder tree
        /// </summary>
        /// <param name="path">Path to display from [nullable]</param>
        internal void DisplayFunc(string path)
        {
            if (path == null)
            {
                Display.All(Manager);
            }
            else
            {
                Display.From(Manager.GetFolder(path));
            }
        }

        /// <summary>
        /// Save a folder tree in a file
        /// </summary>
        /// <param name="key">The optional encryption key</param>
        /// <returns>The status of the serialisation</returns>
        internal SerialisationStatus SaveFunc(string key)
        {
            try
            {
                Serializer.Serialize(Manager.Root, key);
            }
            catch
            {
                return SerialisationStatus.FAILURE;
            }
            return SerialisationStatus.SUCCESS;
        }

        /// <summary>
        /// Load a folder tree from a file
        /// </summary>
        /// <param name="key">The optional encryption key</param>
        /// <returns>The status of the deserialisation</returns>
        internal SerialisationStatus LoadFunc(string key)
        {
            try
            {
                var f = Serializer.Deserialize(key);
                Manager.Load(f);
            }
            catch (FileNotFoundException)
            {
                return SerialisationStatus.FILE_NOT_EXIST;
            }
            catch (CryptographicException)
            {
                return SerialisationStatus.INVALID_KEY;
            }
            catch
            {
                return SerialisationStatus.FAILURE;
            }

            return SerialisationStatus.SUCCESS;
        }

        /// <summary>
        /// Logic to add a folder
        /// </summary>
        /// <param name="path">path [nullable]</param>
        /// <param name="arguments">All arguments for adding a folder</param>
        /// <returns>Whether it worked or not</returns>
        internal Tuple<bool, string, string> AddFolderFunc(string path, List<string> arguments)
        {
            if (!EnsureArgumentCount(arguments, 1))
            {
                return new Tuple<bool, string, string>(false, "", "");
            }
            return Manager.AddFolder(path, arguments);
        }

        /// <summary>
        /// Logic to add a contact
        /// </summary>
        /// <param name="path">Path [nullable]</param>
        /// <param name="arguments">All arguments for adding a contact</param>
        /// <returns>Whether it worked or not</returns>
        internal Tuple<bool, string, string> AddContactFunc(string path, List<string> arguments)
        {
            if (!EnsureArgumentCount(arguments, 5))
            {
                return new Tuple<bool, string, string>(false, "", "");
            }
            return Manager.AddContact(path, arguments);
        }

        /// <summary>
        /// Ensure arguments are correct (enough arguments)
        /// </summary>
        /// <param name="arguments">List of arguments</param>
        /// <param name="required">Number of arguments required</param>
        /// <returns>Whether the arguments are correct or not</returns>
        bool EnsureArgumentCount(List<string> arguments, int required)
        {
            if (arguments.Count < required)
            {
                return false;
            }
            return true;
        }
    }
}
