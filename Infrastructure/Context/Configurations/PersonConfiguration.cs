using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .Property(person => person.Name)
                .IsRequired();
            builder
                .HasIndex(person => new { person.Doc })
                .IsUnique();
            builder
                .Property(person => person.IsActive)
                .HasDefaultValue(true);
            builder
                .Property(person => person.CreatedAt)
                .HasDefaultValueSql("getdate()");
            builder
                .Property(person => person.UpdatedAt)
                .HasDefaultValue(null);

        }
    }
}
