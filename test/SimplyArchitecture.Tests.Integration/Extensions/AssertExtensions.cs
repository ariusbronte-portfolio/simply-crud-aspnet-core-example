using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable CheckNamespace

namespace Xunit
{
    /// <inheritdoc cref="Xunit.Assert" />
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class Assert
    {
        /// <summary>
        ///     Verifies that a ProblemDetails equals RFC 404 Not Found.
        /// </summary>
        /// <remarks>
        ///     https://tools.ietf.org/html/rfc7231#section-6.5.4
        /// </remarks>
        /// <param name="details">A machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807.</param>
        /// <exception cref="System.ArgumentNullException">
        ///    The details must not be null.
        /// </exception>
        public static void IsRFC404NotFound(ProblemDetails details)
        {
            if (details == null)
            {
                throw new ArgumentNullException(nameof(details));
            }

            Equal("https://tools.ietf.org/html/rfc7231#section-6.5.4", details.Type);
            Equal("Not Found", details.Title);
            Equal((int) HttpStatusCode.NotFound, details.Status);
        }

        /// <summary>
        ///     Verifies that a ValidationProblemDetails equals RFC 400 Bad Request.
        /// </summary>
        /// <remarks>
        ///     https://tools.ietf.org/html/rfc7231#section-6.5.1
        /// </remarks>
        /// <param name="details">A machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807.</param>
        /// <param name="errors">Validation errors associated with this instance of <see cref="Microsoft.AspNetCore.Mvc.ValidationProblemDetails"/>.</param>
        /// <exception cref="System.ArgumentNullException">
        ///    The details must not be null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///    The errors can be an empty collection.
        /// </exception>
        public static void IsRFC400BadRequest(ValidationProblemDetails details, IDictionary<string, string[]> errors)
        {
            if (details == null)
            {
                throw new ArgumentNullException(nameof(details));
            }

            if (errors?.Count == 0)
            {
                throw new ArgumentException("Value cannot be an empty collection.", nameof(errors));
            }

            Equal("https://tools.ietf.org/html/rfc7231#section-6.5.1", details.Type);
            Equal("One or more validation errors occurred.", details.Title);
            Equal((int) HttpStatusCode.BadRequest, details.Status);
            Equal(errors, details.Errors);
        }
    }
}