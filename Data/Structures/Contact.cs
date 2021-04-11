using System;

namespace Data.Structures
{
    /// <summary>
    /// Contact that represent information about a contact
    /// </summary>
    public class Contact : Base
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Company { get; private set; }
        public Relationships Link { get; private set; }

        /// <summary>
        /// Create a contact with default createdAt modifiedAt
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="company"></param>
        /// <param name="link"></param>
        public Contact(string firstName, string lastName, string email, string company, Relationships link) : base()
        {
            init(firstName, lastName, email, company, link);
        }

        /// <summary>
        /// Create a contact with a given createdAt, modifiedAt
        /// </summary>
        /// <param name="createdAt"></param>
        /// <param name="modifiedAt"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="company"></param>
        /// <param name="link"></param>
        public Contact(DateTime createdAt, DateTime modifiedAt, string firstName, string lastName, string email, string company, Relationships link) : base(createdAt, modifiedAt)
        {
            init(firstName, lastName, email, company, link);
        }

        /// <summary>
        /// Initialise the contact with the correct arguments
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="company"></param>
        /// <param name="link"></param>        
        private void init(string firstName, string lastName, string email, string company, Relationships link)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Company = company;
            Link = link;
        }
    }
}
