using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimplyArchitecture.WebApi.Abstractions
{
    /// <summary>
    ///     The base controller implementation.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBePrivate.Global")]
    public abstract class AbstractController<TEntity, TDto> : ControllerBase where TEntity : class
    {
        /// <inheritdoc cref="SimplyArchitecture.WebApi.Abstractions.IRepository{TEntity}"/>
        protected readonly IRepository<TEntity> Repository;

        /// <inheritdoc cref="AutoMapper.IMapper"/>
        protected readonly IMapper Mapper;

        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="SimplyArchitecture.WebApi.Abstractions.AbstractController{TEntity, TDto}"/> class.
        ///     </para>
        /// <example>
        ///     <code>
        ///        public class ConcreteController : AbstractController{TEntity, TDto}
        ///        {
        ///             public ConcreteController(IRepository{TEntity} repository, IMapper mapper) : base(repository, mapper)
        ///             {
        ///             }
        ///         }
        ///     </code>
        /// </example>
        /// </summary>
        protected AbstractController(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository ?? throw new ArgumentNullException(paramName: nameof(repository));
            Mapper = mapper ?? throw new ArgumentNullException(paramName: nameof(mapper));
        }

        /// <summary>
        ///     Get items.
        /// </summary>
        /// <param name="cancellationToken">Client closed connection.</param>
        /// <returns>The list of items</returns>
        /// <response code="200">Returns the list of items.</response>
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TDto>>> Get(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await Repository.GetAsync(cancellationToken: cancellationToken);
            var response = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(source: entities);
            return Ok(value: response);
        }

        /// <summary>
        ///     Get item by id.
        /// </summary>
        /// <param name="id">Unique key in a relational database.</param>
        /// <param name="cancellationToken">Client closed connection.</param>
        /// <returns>The concrete item.</returns>
        /// <response code="200">Return the concrete item.</response>
        /// <response code="400">If the item will not pass validation.</response>
        /// <response code="404">If the item not found.</response>
        [HttpGet(template: "{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TDto>> GetById(long id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await Repository.FindByIdAsync(id: id, cancellationToken: cancellationToken);
            if (entity == null) return NotFound();
            var response = Mapper.Map<TEntity, TDto>(source: entity);
            return Ok(value: response);
        }

        /// <summary>
        ///     Creates a new item.
        /// </summary>
        /// <param name="item">Request body.</param>
        /// <param name="cancellationToken">Client closed connection.</param>
        /// <returns>A newly created item.</returns>
        /// <response code="201">Returns the newly created item.</response>
        /// <response code="400">If the item will not pass validation.</response>
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TDto>> Create([FromBody] TDto item, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = Mapper.Map<TDto, TEntity>(source: item);
            var response = await Repository.CreateAsync(item: entity, cancellationToken: cancellationToken);
            var dto = Mapper.Map<TEntity, TDto>(source: response);
            return Created(uri: nameof(GetById), value: dto);
        }

        /// <summary>
        ///     Update item.
        /// </summary>
        /// <param name="item">Request body.</param>
        /// <param name="cancellationToken">Client closed connection.</param>
        /// <returns>Nothing.</returns>
        /// <response code="204">Nothing.</response>
        /// <response code="400">If the item will not pass validation.</response>
        /// <response code="404">If the item not found.</response>
        [HttpPut]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTodoItem([FromBody] TDto item, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = Mapper.Map<TDto, TEntity>(source: item);
            await Repository.UpdateAsync(item: entity, cancellationToken: cancellationToken);
            return NoContent();
        }

        /// <summary>
        ///     Delete item.
        /// </summary>
        /// <param name="id">Request body.</param>
        /// <param name="cancellationToken">Client closed connection.</param>
        /// <returns>Nothing.</returns>
        /// <response code="204">Nothing.</response>
        /// <response code="400">If the item will not pass validation.</response>
        /// <response code="404">If the item not found.</response>
        [HttpDelete(template: "{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await Repository.FindByIdAsync(id, cancellationToken);
            if (entity == null) return NotFound();
            await Repository.RemoveAsync(entity, cancellationToken: cancellationToken);
            return NoContent();
        }
    }
}