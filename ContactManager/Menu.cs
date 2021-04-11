using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactManager
{
    /// <summary>
    /// Handles showing and interacting with the menu
    /// </summary>
    class Menu
    {
        private Engine Engine { get; set; }
        private string LastAction { get; set; }

        /// <summary>
        /// Create a Menu
        /// </summary>
        /// <param name="engine">The engine to be linked with the menu</param>
        public Menu(Engine engine)
        {
            this.Engine = engine;
            this.LastAction = "";
        }

        /// <summary>
        /// Run the Menu
        /// </summary>
        internal void Run()
        {
            Instructions instruction = Instructions.None;
            string input = "";

            Clean();
            do
            {
                input = Console.ReadLine();
                List<string> arguments = input.Split(' ').ToList();
                try
                {
                    instruction = arguments[0].GetEnum<Instructions>();
                    arguments.RemoveAt(0);
                }
                catch
                {
                    Console.WriteLine("Instruction inconnue.");
                    continue;
                }

                switch (instruction)
                {
                    case Instructions.Display:
                        {
                            string path = GetTag(arguments, "-p");
                            Engine.DisplayFunc(path);
                            break;
                        }
                    case Instructions.Load:
                        {
                            string key = GetTag(arguments, "-k");

                            SerialisationStatus status = Engine.LoadFunc(key);
                            if (status == SerialisationStatus.FILE_NOT_EXIST)
                            {
                                Console.WriteLine("Could not load the folder tree: File doesn't exist");
                            }
                            else if (status == SerialisationStatus.INVALID_KEY)
                            {
                                Console.WriteLine("Could not load the folder tree: Invalid key");
                            }
                            else if (status == SerialisationStatus.FAILURE)
                            {
                                Console.WriteLine("Could not load the folder tree: Serialisation Error");
                            }
                            else
                            {
                                LastAction = "Folder tree loaded from file.";
                                Clean();
                            }
                            break;
                        }
                    case Instructions.Save:
                        {
                            string key = GetTag(arguments, "-k");

                            SerialisationStatus status = Engine.SaveFunc(key);
                            if (status == SerialisationStatus.FAILURE)
                            {
                                Console.WriteLine("Could not save the folder tree: Serialisation Error");
                            }
                            else
                            {
                                LastAction = "Folder tree saved into file.";
                                Clean();
                            }

                            break;
                        }
                    case Instructions.AddFolder:
                        {
                            string path = GetTag(arguments, "-p");

                            var res = Engine.AddFolderFunc(path, arguments);
                            if (!res.Item1)
                            {
                                Console.WriteLine("Format is: {0} [name]", Instructions.AddFolder.GetString<Instructions>());
                            }
                            else
                            {
                                LastAction = $"Folder {res.Item3} added under {res.Item2}";
                                Clean();
                            }
                            break;
                        }
                    case Instructions.AddContact:
                        {
                            string path = GetTag(arguments, "-p");

                            var res = Engine.AddContactFunc(path, arguments);
                            if (!res.Item1)
                            {
                                Console.WriteLine("Format is: {0} [firstname] [lastname] [email@email.com] [company] [link]", Instructions.AddContact.GetString<Instructions>());
                            }
                            else
                            {
                                LastAction = $"Contact {res.Item3} added under {res.Item2}";
                                Clean();
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Instruction inconnue.");
                            break;
                        }
                }
            } while (instruction != Instructions.Exit);
        }

        /// <summary>
        /// Clear the console and display nicely formatted data
        /// </summary>
        void Clean()
        {
            Console.Clear();
            Show();

            if (LastAction.Length > 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Last action: {0}", LastAction);
            }
            Console.WriteLine("\n=== DISPLAY ===\n");
            Engine.DisplayFunc(null);
            Console.WriteLine("\n=== COMMAND ===\n");
        }

        /// <summary>
        /// Parse arguments to get the value for a particular tag
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        string GetTag(List<string> arguments, string param = "-p")
        {
            string path = null;
            int delete = -1;
            for (int i = 0; i < arguments.Count - 1; ++i)
            {
                if (arguments[i] == param)
                {
                    path = arguments[i + 1];
                    delete = i;
                    break;
                }
            }

            if (delete != -1)
            {
                arguments.RemoveAt(delete);
                arguments.RemoveAt(delete);
            }

            return path;
        }

        /// <summary>
        /// Display the menu
        /// </summary>
        void Show()
        {
            Console.WriteLine("=== MENU ===");
            Console.WriteLine("1 <{0}> Display file tree [-p to specify path]", Instructions.Display.GetString<Instructions>());
            Console.WriteLine("2 <{0}> Load data [-k to specify key]", Instructions.Load.GetString<Instructions>());
            Console.WriteLine("3 <{0}> Save data [-k to specify key]", Instructions.Save.GetString<Instructions>());
            Console.WriteLine("4 <{0}> Add a folder [-p to specify path]", Instructions.AddFolder.GetString<Instructions>());
            Console.WriteLine("\t{0} [name]", Instructions.AddFolder.GetString<Instructions>());
            Console.WriteLine("5 <{0}> Add a contact [-p to specify path]", Instructions.AddContact.GetString<Instructions>());
            Console.WriteLine("\t{0} [firstname] [lastname] [email@email.com] [company] [link(friend, colleague, relation, network)]", Instructions.AddContact.GetString<Instructions>());
            Console.WriteLine("6 <{0}> Exit app", Instructions.Exit.GetString<Instructions>());
            Console.WriteLine("");
            Console.WriteLine("- [help] path: '-p /' '-p /folder'", Instructions.AddFolder.GetString<Instructions>());
            Console.WriteLine("- [help] key: '-k key'", Instructions.AddFolder.GetString<Instructions>());
        }
    }
}
