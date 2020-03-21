using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplyArchitecture.WebApi.Domain;

namespace SimplyArchitecture.WebApi.Configurations
{
    /// <inheritdoc />
    public class PersonEntityConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Age).IsRequired();
        }
    }
}