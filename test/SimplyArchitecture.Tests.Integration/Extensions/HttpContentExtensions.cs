using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimplyArchitecture.Tests.Integration.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="System.Net.Http.HttpContent" />.
    /// </summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        ///     Cast the HTTP entity body to the specified {T}.
        /// </summary>
        /// <param name="content">A base class that represents the content headers and body of the HTTP entity.</param>
        /// <typeparam name="T">Object to cast the HTTP request body.</typeparam>
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }
            
            return JsonConvert.DeserializeObject<T>(value: await content.ReadAsStringAsync());
        }
    }
}