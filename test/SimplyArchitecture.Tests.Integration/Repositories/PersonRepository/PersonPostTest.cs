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
    public class PersonPostTest : HttpClientHelper, IClassFixture<WebApplicationFactoryFixture>
    {
        public PersonPostTest(WebApplicationFactoryFixture factory) : base(factoryFixture: factory)
        {
        }

        [Fact]
        public async Task PersonPost_CorrectParams_CreateItem()
        {
            // Arrange
            var dto = new PersonDto
            {
                FirstName = "firstName",
                LastName = "lastName",
                Age = 18
            };

            // Act
            var response = await PostAsync<PersonDto>(requestUri: "person", instance: dto);

            // Assert
            Assert.Equal(expected: HttpStatusCode.Created, actual: response.StatusCode);

            var person = await ReadAsAsync<PersonDto>(httpResponseMessage: response);
            Assert.True(condition: person.Id > 0);
            Assert.Equal(expected: dto.FirstName, actual: person.FirstName);
            Assert.Equal(expected: dto.LastName, actual: person.LastName);
            Assert.Equal(expected: dto.Age, actual: person.Age);
        }

        [Fact]
        public async Task PersonPost_IncorrectParameters_BadRequestValidationFailed()
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
            var response = await PostAsync<PersonDto>(requestUri: "person", instance: dto);

            // Assert
            Assert.Equal(expected: HttpStatusCode.BadRequest, actual: response.StatusCode);

            var details = await ReadAsAsync<ValidationProblemDetails>(httpResponseMessage: response);
            Assert.IsRFC400BadRequest(details: details, errors: errors);
        }
    }
}