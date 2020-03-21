using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimplyArchitecture.WebApi.Validators
{
    /// <summary>
    ///     Ensures that the value of the specified property is greater than a particular value (or greater than the value of another property).
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class GreaterThan : ValidationAttribute
    {
        /// <summary>
        ///     Particular value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimplyArchitecture.WebApi.Validators.GreaterThan"/> class.
        /// </summary>
        /// <param name="value">Particular value.</param>
        public GreaterThan(int value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            return value != null && int.TryParse(value.ToString(), out var res) && res > Value;
        }
    }
}