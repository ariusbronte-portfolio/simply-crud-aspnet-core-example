using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimplyArchitecture.WebApi.Abstractions
{
    /// <summary>
    ///     The base implementation of repository pattern.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "UnusedMemberInSuper.Global")]
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        ///     Access to context.
        /// </summary>
        DbSet<TEntity> DbSet { get; }
        
        /// <summary>
        ///     Returns objects from database.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Finds an object by a unique value in the database.
        /// </summary>
        Task<TEntity> FindByIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Creates an object in database.
        /// </summary>
        Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default);
        
        /// <summary>
        ///     Updates an object in the database.
        /// </summary>
        Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Removes an object from the database.
        /// </summary>
        Task RemoveAsync(TEntity item, CancellationToken cancellationToken = default);
    }
}