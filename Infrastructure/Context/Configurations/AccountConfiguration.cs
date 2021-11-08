using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .Property(account => account.IsActive)
                .HasDefaultValue(true);
            builder
                .Property(account => account.CreatedAt)
                .HasDefaultValueSql("getdate()");
            builder
                .Property(person => person.UpdatedAt)
                .HasDefaultValue(null);
        }
    }
}
