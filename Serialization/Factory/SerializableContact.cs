using Data.Structures;
using System;

namespace Serialization.Factory
{
    /// <summary>
    /// Serializable Contact class
    /// </summary>
    [Serializable]
    public class SerializableContact : SerializableBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public Relationships Link { get; set; }

        public SerializableContact() { }

        public SerializableContact(DateTime createdAt, DateTime modifiedAt, string firstName, string lastName, string email, string company, Relationships link) : base(createdAt, modifiedAt)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Company = company;
            Link = link;
        }
    }
}
