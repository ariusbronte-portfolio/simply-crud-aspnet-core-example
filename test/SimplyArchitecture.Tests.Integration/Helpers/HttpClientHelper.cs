using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using SimplyArchitecture.Tests.Integration.Extensions;
using SimplyArchitecture.Tests.Integration.Fixtures;
using SimplyArchitecture.WebApi;

namespace SimplyArchitecture.Tests.Integration.Helpers
{
    /// <summary>
    ///     Helper class for sending HTTP requests and getting HTTP responses in unit tests 
    /// </summary>
    /// <example>
    ///     <code>
    ///         public class AbstractTest : HttpClientHelper, IClassFixture{WebApplicationFactoryFixture}
    ///         {
    ///              public AbstractTest(WebApplicationFactoryFixture factory) : base(factory)
    ///              {
    ///              }
    ///         }
    ///     </code>
    /// </example>
    [SuppressMessage("ReSharper", "RedundantTypeArgumentsOfMethod")]
    public class HttpClientHelper : IDisposable
    {
        /// <summary>
        ///     Provides a base class for sending HTTP requests and getting HTTP responses from a resource with the specified URI.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimplyArchitecture.Tests.Integration.Helpers.HttpClientHelper"/> class.
        /// </summary>
        protected HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimplyArchitecture.Tests.Integration.Helpers.HttpClientHelper"/> class.
        /// </summary>
        protected HttpClientHelper(WebApplicationFactoryFixture<Startup> factoryFixture)
        {
            _httpClient = factoryFixture?.CreateClient() ?? throw new ArgumentNullException(nameof(factoryFixture));
        }

        ///  <inheritdoc cref="System.Net.Http.HttpClient.GetAsync(string)" />
        protected async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requestUri));
            }

            return await _httpClient.GetAsync(requestUri: requestUri);
        }

        ///  <inheritdoc cref="System.Net.Http.HttpClient.GetAsync(string)" />
        protected async Task<HttpResponseMessage> GetAsync(string requestUri, object arg1)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requestUri));
            }

            if (arg1 == null)
            {
                throw new ArgumentNullException(nameof(arg1));
            }

            return await _httpClient.GetAsync(requestUri: GetUrlWithArgs(requestUri, arg1));
        }


        ///  <inheritdoc cref="SimplyArchitecture.Tests.Integration.Extensions.HttpClientExtensions.GetAsAsync{T}" />
        protected async Task<T> GetAsAsync<T>(string requestUri, object arg1)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requestUri));
            }

            if (arg1 == null)
            {
                throw new ArgumentNullException(nameof(arg1));
            }

            return await _httpClient.GetAsAsync<T>(requestUri: GetUrlWithArgs(requestUri, arg1));
        }

        ///  <inheritdoc cref="SimplyArchitecture.Tests.Integration.Extensions.HttpClientExtensions.PostAsync{T}" />
        protected async Task<HttpResponseMessage> PostAsync<T>(string requestUri, T instance)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requestUri));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return await _httpClient.PostAsync<T>(requestUri, instance);
        }

        ///  <inheritdoc cref="SimplyArchitecture.Tests.Integration.Extensions.HttpClientExtensions.PostAsAsync{T}" />
        protected async Task<T> PostAsAsync<T>(string requestUri, T instance)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requestUri));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return await _httpClient.PostAsAsync(requestUri: requestUri, instance: instance);
        }

        ///  <inheritdoc cref="SimplyArchitecture.Tests.Integration.Extensions.HttpClientExtensions.PutAsync{T}" />
        protected async Task<HttpResponseMessage> PutAsync<T>(string requestUri, T instance)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requestUri));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return await _httpClient.PutAsync<T>(requestUri, instance);
        }

        ///  <inheritdoc cref="System.Net.Http.HttpClient.DeleteAsync(string)" />
        protected async Task<HttpResponseMessage> DeleteAsync(string requestUri, object arg1)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requestUri));
            }

            if (arg1 == null)
            {
                throw new ArgumentNullException(nameof(arg1));
            }

            return await _httpClient.DeleteAsync(GetUrlWithArgs(requestUri, arg1));
        }

        ///  <inheritdoc cref="SimplyArchitecture.Tests.Integration.Extensions.HttpContentExtensions.ReadAsAsync{T}" />
        protected async Task<T> ReadAsAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage == null)
            {
                throw new ArgumentNullException(nameof(httpResponseMessage));
            }

            return await httpResponseMessage.Content.ReadAsAsync<T>();
        }

        /// <summary>
        ///     Returns request uri with one argument.
        /// </summary>
        /// <param name="requestUri">Specified URI.</param>
        /// <param name="arg1">Argument in specified URI.</param>
        /// <exception cref="System.ArgumentException">
        ///    The requestUri must not be null or whitespace.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///    The arg1 must not be null.
        /// </exception>
        private static string GetUrlWithArgs(string requestUri, object arg1)
        {
            if (string.IsNullOrWhiteSpace(requestUri))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requestUri));
            }

            if (arg1 == null)
            {
                throw new ArgumentNullException(nameof(arg1));
            }

            return $"{requestUri}/{arg1}";
        }

        /// <inheritdoc cref="System.IDisposable.Dispose"/>
        public void Dispose()
        {
            _httpClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}