using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SimplyArchitecture.WebApi.Domain;

namespace SimplyArchitecture.WebApi.DataAccess
{
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class SimplyArchitectureDbContext : DbContext
    {
        /// <inheritdoc />
        public SimplyArchitectureDbContext()
        {
        }

        /// <inheritdoc />
        public SimplyArchitectureDbContext(DbContextOptions<SimplyArchitectureDbContext> options) : base(options: options)
        {
        }
        
        /// <inheritdoc cref="SimplyArchitecture.WebApi.Domain.PersonEntity"/>
        public DbSet<PersonEntity> Persons { get; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(modelBuilder: builder);
        }
        
        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(optionsBuilder: builder);
        }
    }
}