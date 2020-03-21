using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimplyArchitecture.WebApi.Abstractions;
using SimplyArchitecture.WebApi.Domain;
using SimplyArchitecture.WebApi.Dto;

namespace SimplyArchitecture.WebApi.Controllers
{
    /// <summary>
    ///     Controller for working with persons.
    /// </summary>
    [ApiController]
    [Route(template: "[controller]")]
    [Produces(contentType: "application/json")]
    public class PersonController : AbstractController<PersonEntity, PersonDto>
    {
        /// <inheritdoc />
        public PersonController(IRepository<PersonEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}