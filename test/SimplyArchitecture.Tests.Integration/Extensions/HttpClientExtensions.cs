using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimplyArchitecture.Tests.Integration.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="System.Net.Http.HttpClient" />.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        ///     Sending a GET request and casting the response to the {T} object.
        /// </summary>
        /// <param name="httpClient">Provides a base class for sending HTTP requests and getting HTTP responses from a resource with the specified URI.</param>
        /// <param name="requestUri">Specified URI.</param>
        /// <typeparam name="T">Object to cast the HTTP response body.</typeparam>
        /// <exception cref="ArgumentException">
        ///    The httpClient must not be null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///    The requestUri must not be null or whitespace.
        /// </exception>
        public static async Task<T> GetAsAsync<T>(this HttpClient httpClient, string requestUri)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(paramName: nameof(httpClient));
            }

            if (string.IsNullOrWhiteSpace(value: requestUri))
            {
                throw new ArgumentException(message: "Value cannot be null or whitespace.", paramName: nameof(requestUri));
            }

            var response = await httpClient.GetAsync(requestUri: requestUri);
            return await ReadAsAsync<T>(httpResponseMessage: response);
        }
        
        /// <summary>
        ///     Sending a POST request and casting the request to the {T} object.
        /// </summary>
        /// <param name="httpClient">Provides a base class for sending HTTP requests and getting HTTP responses from a resource with the specified URI.</param>
        /// <param name="requestUri">Specified URI.</param>
        /// <param name="instance">Request body.</param>
        /// <typeparam name="T">Object to cast the HTTP request body.</typeparam>
        /// <returns>Represents the HTTP response message, including the code and status data.</returns>
        /// <exception cref="ArgumentException">
        ///    The httpClient must not be null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///    The instance must not be null or whitespace.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///    The requestUri must not be null or whitespace.
        /// </exception>
        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient httpClient, string requestUri, T instance)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(paramName: nameof(httpClient));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(paramName: nameof(instance));
            }

            if (string.IsNullOrWhiteSpace(value: requestUri))
            {
                throw new ArgumentException(message: "Value cannot be null or whitespace.", paramName: nameof(requestUri));
            }

            var json = SerializeObject(value: instance);
            var content = new StringContent(content: json, encoding: Encoding.UTF8, mediaType: MediaTypeNames.Application.Json);
            
            return await httpClient.PostAsync(requestUri: requestUri, content: content);
        }

        /// <summary>
        ///     Sending a POST request and casting the response to the {T} object.
        /// </summary>
        /// <param name="httpClient">Provides a base class for sending HTTP requests and getting HTTP responses from a resource with the specified URI.</param>
        /// <param name="requestUri">Specified URI.</param>
        /// <param name="instance">Request body.</param>
        /// <typeparam name="T">Object to cast the HTTP response body.</typeparam>
        /// <exception cref="ArgumentException">
        ///    The httpClient must not be null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///    The instance must not be null or whitespace.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///    The requestUri must not be null or whitespace.
        /// </exception>
        public static async Task<T> PostAsAsync<T>(this HttpClient httpClient, string requestUri, T instance)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(paramName: nameof(httpClient));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(paramName: nameof(instance));
            }

            if (string.IsNullOrWhiteSpace(value: requestUri))
            {
                throw new ArgumentException(message: "Value cannot be null or whitespace.", paramName: nameof(requestUri));
            }
            
            var response = await httpClient.PostAsync(requestUri: requestUri, instance: instance);
            return await ReadAsAsync<T>(httpResponseMessage: response);
        }
        
        /// <summary>
        ///     Sending a PUT request and casting the request to the {T} object.
        /// </summary>
        /// <param name="httpClient">Provides a base class for sending HTTP requests and getting HTTP responses from a resource with the specified URI.</param>
        /// <param name="requestUri">Specified URI.</param>
        /// <param name="instance">Request body.</param>
        /// <typeparam name="T">Object to cast the HTTP request body.</typeparam>
        /// <returns>Represents the HTTP response message, including the code and status data.</returns>
        /// <exception cref="ArgumentException">
        ///    The httpClient must not be null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///    The instance must not be null or whitespace.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///    The requestUri must not be null or whitespace.
        /// </exception>
        public static async Task<HttpResponseMessage> PutAsync<T>(this HttpClient httpClient, string requestUri, T instance)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(paramName: nameof(httpClient));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(paramName: nameof(instance));
            }

            if (string.IsNullOrWhiteSpace(value: requestUri))
            {
                throw new ArgumentException(message: "Value cannot be null or whitespace.", paramName: nameof(requestUri));
            }

            var json = SerializeObject(value: instance);
            var content = new StringContent(content: json, encoding: Encoding.UTF8, mediaType: MediaTypeNames.Application.Json);
            
            return await httpClient.PutAsync(requestUri: requestUri, content: content);
        }
        
        /// <inheritdoc cref="Newtonsoft.Json.JsonConvert.SerializeObject(object)"/>
        /// <exception cref="ArgumentNullException">The value must not be null.</exception>
        private static string SerializeObject(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName: nameof(value));
            }
            
            return JsonConvert.SerializeObject(value: value);
        }
        
        /// <inheritdoc cref="SimplyArchitecture.Tests.Integration.Extensions.HttpContentExtensions.ReadAsAsync{T}"/>
        /// <exception cref="httpResponseMessage">The httpResponseMessage must not be null.</exception>
        private static async Task<T> ReadAsAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage == null)
            {
                throw new ArgumentNullException(paramName: nameof(httpResponseMessage));
            }
            
            return await httpResponseMessage.Content.ReadAsAsync<T>();
        }
    }
}