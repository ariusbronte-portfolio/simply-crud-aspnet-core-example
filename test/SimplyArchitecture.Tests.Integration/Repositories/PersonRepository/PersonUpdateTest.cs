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
    public class PersonUpdateTest : HttpClientHelper, IClassFixture<WebApplicationFactoryFixture>
    {
        public PersonUpdateTest(WebApplicationFactoryFixture factory) : base(factoryFixture: factory)
        {
        }

        [Fact]
        public async Task PersonUpdate_CorrectParams_NoContentResponse()
        {
            // Arrange
            var dto1 = new PersonDto
            {
                FirstName = "firstName1",
                LastName = "lastName1",
                Age = 18
            };

            var post = await PostAsAsync<PersonDto>(requestUri: "person", instance: dto1);

            // Act
            var dto2 = new PersonDto
            {
                Id = post.Id,
                FirstName = "firstName2",
                LastName = "lastName2",
                Age = 36
            };

            var response = await PutAsync(requestUri: "person", instance: dto2);
            var get = await GetAsAsync<PersonDto>(requestUri: "person", arg1: post.Id);

            // Assert
            Assert.Equal(expected: HttpStatusCode.NoContent, actual: response.StatusCode);
            Assert.Equal(expected: dto2.Id, actual: get.Id);
            Assert.Equal(expected: dto2.FirstName, actual: get.FirstName);
            Assert.Equal(expected: dto2.LastName, actual: get.LastName);
            Assert.Equal(expected: dto2.Age, actual: get.Age);
        }

        [Fact]
        public async Task PersonUpdate_IncorrectParams_BadRequestValidationFailed()
        {
            // Arrange
            var dto = new PersonDto();
            var errors = new Dictionary<string, string[]>
            {
                {"FirstName", new[] {"The FirstName field is required."}},
                {"LastName", new[] {"The LastName field is required."}},
                {"Age", new[] {"The field Age must be greater than 0."}}
            };

            // Act
            var response = await PutAsync(requestUri: "person", instance: dto);

            // Assert
            Assert.Equal(expected: HttpStatusCode.BadRequest, actual: response.StatusCode);

            var details = await ReadAsAsync<ValidationProblemDetails>(httpResponseMessage: response);
            Assert.IsRFC400BadRequest(details: details, errors: errors);
        }
    }
}