using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Serialization
{
    /// <summary>
    /// Abstract class for all encrypter. Need to be extended to a implement a particular type of encryption algorithm
    /// </summary>
    public abstract class Encrypter
    {
        /// <summary>
        /// The string key to use when encrypting
        /// </summary>
        protected string CryptoKey { get; set; }

        /// <summary>
        /// Use the current user windows SID as default crypto key if no key is provided
        /// </summary>
        public Encrypter() : this(null) { }

        /// <summary>
        /// Use the provided key as crypto key
        /// </summary>
        public Encrypter(string key)
        {
            SetKey(key);
        }

        /// <summary>
        /// Set the crypto key
        /// </summary>
        /// <param name="key"></param>
        internal virtual void SetKey(string key)
        {
            CryptoKey = key ?? WindowsIdentity.GetCurrent().User.Value;
        }

        /// <summary>
        /// Create an encryption stream using the subclass implemented algorithm
        /// </summary>
        /// <param name="outputStream"></param>
        /// <returns></returns>
        public abstract CryptoStream CreateEncryptionStream(Stream outputStream);

        /// <summary>
        /// Create a decryption stream using the subclass implemented algorithm
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public abstract CryptoStream CreateDecryptionStream(Stream inputStream);
    }
}
