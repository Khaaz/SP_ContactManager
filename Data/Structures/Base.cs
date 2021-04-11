using System;

namespace Data.Structures
{
    /// <summary>
    /// Base class for structures
    /// </summary>
    public abstract class Base
    {
        public DateTime CreatedAt { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }

        /// <summary>
        /// Create a the base of the class with default CreatedAt and ModifiedAt
        /// </summary>
        public Base() : this(DateTime.Now, DateTime.Now) { }

        /// <summary>
        /// Create the base of the class with given createdAt and ModifiedAt
        /// </summary>
        /// <param name="createdAt"></param>
        /// <param name="modifiedTime"></param>
        public Base(DateTime createdAt, DateTime modifiedAt)
        {
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }
    }
}
