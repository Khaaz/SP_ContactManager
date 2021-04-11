using Data.Structures;
using Data.Validation;
using System;
using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// Manage the folder tree
    /// </summary>
    public class Manager
    {
        public Folder Root { get; private set; }
        public Folder LastFolder { get; private set; }

        /// <summary>
        /// Create the manager with a default root folder
        /// </summary>
        public Manager()
        {
            Root = new Folder("root");
            LastFolder = Root;
        }

        /// <summary>
        /// Load the folder tree from a folder
        /// </summary>
        /// <param name="f">The folder to use as Root</param>
        public void Load(Folder f)
        {
            Root = f;
            LastFolder = Root;
        }

        /// <summary>
        /// Add a folder at a specific path
        /// </summary>
        /// <param name="path">The path [nullable]</param>
        /// <param name="name">The folder name</param>
        /// <returns></returns>
        public Tuple<bool, string, string> AddFolder(string path, List<string> arguments)
        {
            Folder parent = GetFolder(path);

            if (!Validator.Validate(arguments, new List<IVerifier> { new RegexVerifier(@"^[^/]{1,20}$") }))
            {
                return new Tuple<bool, string, string>(false, "", "");
            }

            Folder f = new Folder(arguments[0]);
            parent.AddFolder(f);
            LastFolder = f;

            return new Tuple<bool, string, string>(true, parent.Name, f.Name);
        }

        /// <summary>
        /// Add a contact to a specified path
        /// </summary>
        /// <param name="path">The path [nullable]</param>
        /// <param name="firstName">The contact first name</param>
        /// <param name="lastName">The contact last name</param>
        /// <param name="email">The contact email</param>
        /// <param name="company">The contact company</param>
        /// <returns></returns>
        public Tuple<bool, string, string> AddContact(string path, List<string> arguments)
        {
            Folder parent = GetFolder(path);

            if (!Validator.Validate(arguments, new List<IVerifier> {
                new StringVerifier(1, 20),
                new StringVerifier(1, 20),
                new EmailVerifier(),
                new StringVerifier(1, 20),
                new EnumVerifier(),
            })
                )
            {
                return new Tuple<bool, string, string>(false, "", "");
            }

            Contact c = new Contact(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4].GetEnum<Relationships>());
            parent.AddContact(c);
            return new Tuple<bool, string, string>(true, parent.Name, c.FirstName);
        }

        /// <summary>
        /// Get a folder for a path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The found folder</returns>
        public Folder GetFolder(string path)
        {
            return GetFolder(ParsePath(path));
        }

        /// <summary>
        /// Get a folder from a parsed path. Return LastFolder if no path is specified. Return Root if invalid path.
        /// </summary>
        /// <param name="args">The parsed list path</param>
        /// <returns>The found folder</returns>
        Folder GetFolder(string[] args)
        {
            if (args == null)
            {
                return LastFolder;
            }

            Folder f = Root;
            foreach (var arg in args)
            {
                Folder tmp = f.Folders.Find(e => e.Name == arg);
                if (tmp != null) // could find
                {
                    f = tmp;
                }
                else // could not find => default to Root
                {
                    f = Root;
                    break;
                }
            }
            return f;
        }

        /// <summary>
        /// Parse path to an arg list (list of folders / subfolders)
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The parsed list</returns>
        string[] ParsePath(string path)
        {
            if (path == null)
            {
                return null;
            }
            else
            {
                if (path.StartsWith("/"))
                {
                    path = path.Remove(0, 1);
                }
                if (path.EndsWith("/"))
                {
                    path = path.Remove(path.Length - 1, 1);
                }
                return path.Split('/');
            }
        }
    }
}
