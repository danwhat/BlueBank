using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    internal class TransactionLogConfiguration : IEntityTypeConfiguration<TransactionLog>
    {
        public void Configure(EntityTypeBuilder<TransactionLog> builder)
        {
            builder
                .Property(log => log.Value)
                .HasColumnType("money");
            builder
                .Property(log => log.BalanceAfter)
                .HasColumnType("money");
            builder
               .Property(transactionLog => transactionLog.CreatedAt)
               .HasDefaultValueSql("getdate()");
            builder
               .HasOne<Account>(transactionLog => transactionLog.Account)
               .WithMany(account => account.TransactionLogs)
               .HasForeignKey(transactionLog => transactionLog.AccountId);
        }
    }
}