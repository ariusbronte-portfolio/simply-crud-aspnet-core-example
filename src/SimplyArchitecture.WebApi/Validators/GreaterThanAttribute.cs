using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace SimplyArchitecture.WebApi.Validators
{
    /// <summary>
    ///     Validation attribute to assert a integer property, field or parameter does not greater than particular value.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBePrivate.Global")]
    [AttributeUsage(validOn: AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class GreaterThanAttribute : ValidationAttribute
    {
        /// <summary>
        ///     Particular value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        ///     Constructor that accepts the particular value of the integer.
        /// </summary>
        /// <param name="value">The particular value.  It may not be negative.</param>
        public GreaterThanAttribute(int value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            return int.TryParse(s: value.ToString(), result: out var res) && res > Value;
        }
        
        /// <inheritdoc />
        public override string FormatErrorMessage(string name)
        {
            const string errorMessage = "The field {0} must be greater than {1}.";
            return string.Format(provider: CultureInfo.CurrentCulture, format: errorMessage, arg0: name, arg1: Value);
        }
    }
}