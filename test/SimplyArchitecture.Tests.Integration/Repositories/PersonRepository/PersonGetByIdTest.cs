using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimplyArchitecture.Tests.Integration.Fixtures;
using SimplyArchitecture.Tests.Integration.Helpers;
using SimplyArchitecture.WebApi.Dto;
using Xunit;

namespace SimplyArchitecture.Tests.Integration.Repositories.PersonRepository
{
    [SuppressMessage(category: "ReSharper", checkId: "RedundantTypeArgumentsOfMethod")]
    public class PersonGetByIdTest : HttpClientHelper, IClassFixture<WebApplicationFactoryFixture>
    {
        public PersonGetByIdTest(WebApplicationFactoryFixture factory) : base(factoryFixture: factory)
        {
        }

        [Fact]
        public async Task GetById_WithAnyItems_NotEmptyResponse()
        {
            // Arrange
            var dto = new PersonDto
            {
                FirstName = "firstName",
                LastName = "lastName",
                Age = 18
            };

            var post = await PostAsAsync<PersonDto>(requestUri: "person", instance: dto);

            // Act
            var response = await GetAsync(requestUri: "person", arg1: post.Id);

            // Assert
            Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);

            var person = await ReadAsAsync<PersonDto>(httpResponseMessage: response);
            Assert.NotNull(@object: person);
            Assert.Equal(expected: post.Id, actual: person.Id);
            Assert.Equal(expected: post.FirstName, actual: person.FirstName);
            Assert.Equal(expected: post.LastName, actual: person.LastName);
            Assert.Equal(expected: post.Age, actual: person.Age);
        }

        [Fact]
        public async Task GetById_NotFoundItem_NotFoundResponse()
        {
            // Act
            var response = await GetAsync(requestUri: "person", arg1: 0);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
            Assert.IsRFC404NotFound(details: await ReadAsAsync<ProblemDetails>(httpResponseMessage: response));
        }

        [Fact]
        public async Task GetById_IncorrectRequest_BadRequestResponse()
        {
            // Arrange
            var errors = new Dictionary<string, string[]>
            {
                {"id", new[] {"The value 'abc' is not valid."}},
            };

            // Act
            var response = await GetAsync(requestUri: "person", arg1: "abc");

            // Assert
            Assert.Equal(expected: HttpStatusCode.BadRequest, actual: response.StatusCode);
            
            var details = await ReadAsAsync<ValidationProblemDetails>(httpResponseMessage: response);
            Assert.IsRFC400BadRequest(details: details, errors: errors);
        }
    }
}