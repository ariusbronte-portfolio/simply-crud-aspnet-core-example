using System.ComponentModel.DataAnnotations;
using SimplyArchitecture.WebApi.Validators;

namespace SimplyArchitecture.WebApi.Dto
{
    /// <summary>
    ///     Presents the essence of the persons.
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        ///     Gets the primary key for this person.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets person firstname.
        /// </summary>
        [Required]
        [StringLength(maximumLength: 128)]
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets person lastname.
        /// </summary>
        [Required]
        [StringLength(maximumLength: 128)]
        public string LastName { get; set; }

        /// <summary>
        ///     Gets person age.
        /// </summary>
        [GreaterThan(value: 0)]
        public int Age { get; set; }
    }
}