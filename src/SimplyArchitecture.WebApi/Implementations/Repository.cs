using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimplyArchitecture.WebApi.Abstractions;
using SimplyArchitecture.WebApi.DataAccess;

namespace SimplyArchitecture.WebApi.Implementations
{
    /// <inheritdoc />
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <inheritdoc cref="SimplyArchitecture.WebApi.DataAccess.SimplyArchitectureDbContext"/>
        private readonly SimplyArchitectureDbContext _dbContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SimplyArchitecture.WebApi.Implementations.Repository{TEntity}"/> class.
        /// </summary>
        public Repository(SimplyArchitectureDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = _dbContext.Set<TEntity>();
        }

        /// <inheritdoc />
        public DbSet<TEntity> DbSet { get; }
        
        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await DbSet.ToArrayAsync(cancellationToken);
        }
        
        /// <inheritdoc />
        public async Task<TEntity> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await DbSet.FindAsync(id);
        }
        
        /// <inheritdoc />
        public async Task<TEntity> CreateAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await DbSet.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity.Entity;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            DbSet.Update(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(TEntity item, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            DbSet.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private bool _disposed;

        /// <inheritdoc cref="IDisposable.Dispose" />
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                }
            }
            
            _disposed = true;
        }

        /// <inheritdoc cref="IDisposable.Dispose" />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}