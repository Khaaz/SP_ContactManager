using System;
using System.IO;
using System.Security.Cryptography;

namespace Serialization
{
    /// <summary>
    /// Encrypter implemented using the Rijndael encryption algorithm (algorithm used in AES)
    /// </summary>
    public class RijndaelEncrpyter : Encrypter
    {
        private const int KEY_SIZE = 128;
        private const int IV_SIZE = 16; // block size is 128-bit
        private const int ITERATION = 300;

        private readonly byte[] SALT = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };
        private byte[] Key { get; set; }

        public RijndaelEncrpyter() : base()
        {
            CreateKey();
        }
        public RijndaelEncrpyter(string key) : base(key)
        {
            CreateKey();
        }

        /// <summary>
        /// Generate a correctly sized key from a variable length input string (crypto key)
        /// https://stackoverflow.com/a/17196217/10537087
        /// </summary>
        /// <param name="keyBytes"></param>
        private void CreateKey(int keyBytes = KEY_SIZE)
        {
            using (var keyGenerator = new Rfc2898DeriveBytes(CryptoKey, SALT, ITERATION))
            {
                Key = keyGenerator.GetBytes(keyBytes / 8);
            }
        }

        internal override void SetKey(string key)
        {
            base.SetKey(key);
            CreateKey();
        }

        public override CryptoStream CreateEncryptionStream(Stream outputStream)
        {
            byte[] iv = new byte[IV_SIZE];

            using (var rng = new RNGCryptoServiceProvider())
            {
                // Using a cryptographic random number generator
                rng.GetNonZeroBytes(iv);
            }

            // Write IV to the start of the stream
            outputStream.Write(iv, 0, iv.Length);

            Rijndael rijndael = new RijndaelManaged
            {
                KeySize = KEY_SIZE
            };

            CryptoStream encryptor = new CryptoStream(
                outputStream,
                rijndael.CreateEncryptor(Key, iv),
                CryptoStreamMode.Write);
            return encryptor;
        }

        public override CryptoStream CreateDecryptionStream(Stream inputStream)
        {
            byte[] iv = new byte[IV_SIZE];

            if (inputStream.Read(iv, 0, iv.Length) != iv.Length)
            {
                throw new ApplicationException("Failed to read IV from stream.");
            }

            Rijndael rijndael = new RijndaelManaged
            {
                KeySize = KEY_SIZE
            };

            CryptoStream decryptor = new CryptoStream(
                inputStream,
                rijndael.CreateDecryptor(Key, iv),
                CryptoStreamMode.Read);
            return decryptor;
        }
    }
}
