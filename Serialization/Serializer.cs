using Data.Structures;
using Serialization.Factory;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Serialization
{
    /// <summary>
    /// Abstract Serialiser that offers a common interface to handle serialisation.
    /// Use encrypter to encrypt serialised data
    /// </summary>
    public abstract class Serializer
    {
        private Encrypter encrypter { get; set; }

        /// <summary>
        /// The path where to store the file. It uses the "Documents" folder and the file will be named after the user name.
        /// </summary>
        private string Path { get; set; }

        /// <summary>
        /// Instantiate the Serializer using a particular Encrypter.
        /// </summary>
        public Serializer()
        {
            encrypter = new RijndaelEncrpyter();
            // Environment.UserName | WindowsIdentity.GetCurrent().Name.Replace('\\', '_')
            Path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\{WindowsIdentity.GetCurrent().Name.Replace('\\', '_')}";
        }

        /// <summary>
        /// Create a stream to write object with a specialised type of encryption
        /// </summary>
        /// <param name="outputStream"></param>
        /// <param name="obj"></param>
        public abstract void WriteObjectToStream(Stream outputStream, Object obj);

        /// <summary>
        /// Create a stream to read object with a specialised type of decryption
        /// </summary>
        /// <param name="outputStream"></param>
        public abstract Object ReadObjectFromStream(Stream inputStream);

        // https://stackoverflow.com/questions/28791185/encrypt-net-binary-serialization-stream
        /// <summary>
        /// Serialize a folder tree structure into a file. Can optionally use an encryption key
        /// </summary>
        /// <param name="f">The folder tree to serialise</param>
        /// <param name="key">The key to use as encryption key</param>
        public void Serialize(Folder f, string key = null)
        {
            encrypter.SetKey(key);
            using (FileStream file = new FileStream(Path, FileMode.Create))
            {
                using (CryptoStream cryptoStream = encrypter.CreateEncryptionStream(file))
                {
                    WriteObjectToStream(cryptoStream, f.CreateSerializable());
                }
            }
        }

        /// <summary>
        /// Deserialize a file into a folder tree structure. Can optionally use an encryption key
        /// </summary>
        /// <param name="key">The key to use as encryption key</param>
        /// <returns>The deserialised folder tree</returns>
        public Folder Deserialize(string key = null)
        {
            SerializableFolder folder;

            encrypter.SetKey(key);
            using (FileStream file = new FileStream(Path, FileMode.Open))
            {
                using (CryptoStream cryptoStream = encrypter.CreateDecryptionStream(file))
                {
                    folder = (SerializableFolder)ReadObjectFromStream(cryptoStream);
                }
            }

            return folder.CreateInstance();
        }
    }
}
