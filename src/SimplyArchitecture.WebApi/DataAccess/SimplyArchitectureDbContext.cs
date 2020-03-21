using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SimplyArchitecture.WebApi.Configurations;
using SimplyArchitecture.WebApi.Domain;

namespace SimplyArchitecture.WebApi.DataAccess
{
    /// <inheritdoc />
    [SuppressMessage(category: "ReSharper", checkId: "SuggestBaseTypeForParameter")]
    [SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Global")]
    [SuppressMessage(category: "ReSharper", checkId: "UnassignedGetOnlyAutoProperty")]
    [SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Global")]
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
            builder.ApplyConfiguration(configuration: new PersonEntityConfiguration());
        }
        
        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(optionsBuilder: builder);
        }
    }
}