using System;
using System.Diagnostics.CodeAnalysis;

namespace SimplyArchitecture.WebApi.Domain
{
    /// <summary>
    ///     Presents the essence of the persons.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class PersonEntity
    {
        /// <summary>
        ///     Default values for creating a record in the database.
        /// </summary>
        public PersonEntity()
        {
            CreationHistory = DateTimeOffset.UtcNow;
        }

        /// <summary>
        ///     Gets the primary key for this person.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets person firstname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets person lastname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets person age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        ///     Gets system creation history time.
        /// </summary>
        /// <remarks>Created in Universal Coordinated Universal Time (UTC).</remarks>
        public DateTimeOffset CreationHistory { get; private set; }
    }
}