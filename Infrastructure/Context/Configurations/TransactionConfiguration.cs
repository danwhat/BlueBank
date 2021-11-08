using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .Property(transaction => transaction.Value)
                .HasColumnType("money");
            builder
               .Property(transaction => transaction.CreatedAt)
               .HasDefaultValueSql("getdate()");
            builder
               .HasOne<Account>(transaction => transaction.AccountFrom)
               .WithMany(account => account.TransactionsFrom);
            builder
               .HasOne<Account>(transaction => transaction.AccountTo)
               .WithMany(account => account.TransactionsTo);
        }
    }
}
