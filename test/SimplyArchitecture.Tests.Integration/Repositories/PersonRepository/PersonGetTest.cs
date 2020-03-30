using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SimplyArchitecture.Tests.Integration.Fixtures;
using SimplyArchitecture.Tests.Integration.Helpers;
using SimplyArchitecture.WebApi.Dto;
using Xunit;

namespace SimplyArchitecture.Tests.Integration.Repositories.PersonRepository
{
    public class PersonGetTest : HttpClientHelper, IClassFixture<WebApplicationFactoryFixture>
    {
        public PersonGetTest(WebApplicationFactoryFixture factory) : base(factoryFixture: factory)
        {
        }

        [Fact]
        public async Task GetAll_WithoutAnyItems_EmptyResponse()
        {
            // Act
            var response = await GetAsync(requestUri: "person");
            
            // Assert
            Assert.Equal(expected: HttpStatusCode.OK,actual: response.StatusCode);
            Assert.Empty(collection: await ReadAsAsync<IEnumerable<PersonDto>>(httpResponseMessage: response));
        }
    }
}