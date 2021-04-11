using Data.Structures;

namespace Serialization.Factory
{
    /// <summary>
    /// Contact extention methods for serialization
    /// </summary>
    public static class ContactFactory
    {
        /// <summary>
        /// Extention method to create a SerializableContact from a Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static SerializableContact CreateSerializable(this Contact contact)
        {
            return new SerializableContact(contact.CreatedAt, contact.ModifiedAt, contact.FirstName, contact.LastName, contact.Email, contact.Company, contact.Link);
        }

        /// <summary>
        /// Extention method to create a Contact from SerializableContact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static Contact CreateInstance(this SerializableContact contact)
        {
            return new Contact(contact.CreatedAt, contact.ModifiedAt, contact.FirstName, contact.LastName, contact.Email, contact.Company, contact.Link);
        }
    }
}
