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
            builder.HasKey(keyExpression: x => x.Id);

            builder.Property(propertyExpression: x => x.FirstName).IsRequired().HasMaxLength(maxLength: 128);
            builder.Property(propertyExpression: x => x.LastName).IsRequired().HasMaxLength(maxLength: 128);
            builder.Property(propertyExpression: x => x.Age).IsRequired();
        }
    }
}