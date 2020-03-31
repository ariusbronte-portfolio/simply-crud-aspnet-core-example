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
    public class PersonDeleteTest : HttpClientHelper, IClassFixture<WebApplicationFactoryFixture>
    {
        public PersonDeleteTest(WebApplicationFactoryFixture factory) : base(factoryFixture: factory)
        {
        }

        [Fact]
        public async Task PersonDelete_CorrectParams_NoContentResponse()
        {
            // Arrange
            var dto = new PersonDto
            {
                FirstName = "firstName1",
                LastName = "lastName1",
                Age = 18
            };

            var post = await PostAsAsync<PersonDto>(requestUri: "person", instance: dto);

            // Act
            var response = await DeleteAsync(requestUri: "person", arg1: post.Id);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NoContent, actual: response.StatusCode);
        }

        [Fact]
        public async Task PersonDelete_NotFoundItem_NotFoundResponse()
        {
            // Act
            var response = await DeleteAsync(requestUri: "person", arg1: 0);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
            Assert.IsRFC404NotFound(details: await ReadAsAsync<ProblemDetails>(httpResponseMessage: response));
        }

        [Fact]
        public async Task PersonDelete_IncorrectRequest_BadRequestResponse()
        {
            // Arrange
            var errors = new Dictionary<string, string[]>
            {
                {"id", new[] {"The value 'abc' is not valid."}},
            };

            // Act
            var response = await DeleteAsync(requestUri: "person", arg1: "abc");

            // Assert
            Assert.Equal(expected: HttpStatusCode.BadRequest, actual: response.StatusCode);

            var details = await ReadAsAsync<ValidationProblemDetails>(httpResponseMessage: response);
            Assert.IsRFC400BadRequest(details: details, errors: errors);
        }
    }
}