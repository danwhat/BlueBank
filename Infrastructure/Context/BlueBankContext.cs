using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BlueBankContext : DbContext
    {
        internal DbSet<Person> People { get; set; }
        internal DbSet<Contact> Contacts { get; set; }
        internal DbSet<Account> Accounts { get; set; }
        internal DbSet<TransactionLog> TransactionLog { get; set; }
        internal DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BlueBankDB;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(person => person.Name)
                .IsRequired();
            modelBuilder.Entity<Person>()
                .HasIndex(person => new { person.Doc })
                .IsUnique();
            modelBuilder.Entity<Person>()
                .Property(person => person.IsActive)
                .HasDefaultValue(true);
            modelBuilder.Entity<Person>()
                .Property(person => person.CreatedAt)
                .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Person>()
                .Property(person => person.UpdatedAt)
                .HasDefaultValue(null);

            modelBuilder.Entity<Account>()
                .Property(account => account.IsActive)
                .HasDefaultValue(true);
            modelBuilder.Entity<Account>()
                .Property(account => account.CreatedAt)
                .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Account>()
                .Property(person => person.UpdatedAt)
                .HasDefaultValue(null);

            modelBuilder.Entity<Contact>()
                .Property(contact => contact.CreatedAt)
                .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Contact>()
                .HasOne<Person>(contact => contact.Person)
                .WithMany(person => person.Contacts)
                .HasForeignKey(contact => contact.PersonId);

            modelBuilder.Entity<Transaction>()
               .Property(transaction => transaction.CreatedAt)
               .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Transaction>()
               .HasOne<Account>(transaction => transaction.AccountFrom)
               .WithMany(account => account.TransactionsFrom);
            modelBuilder.Entity<Transaction>()
               .HasOne<Account>(transaction => transaction.AccountTo)
               .WithMany(account => account.TransactionsTo);

            modelBuilder.Entity<TransactionLog>()
               .Property(transactionLog => transactionLog.CreatedAt)
               .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<TransactionLog>()
               .HasOne<Account>(transactionLog => transactionLog.Account)
               .WithMany(account => account.TransactionLogs)
               .HasForeignKey(transactionLog => transactionLog.AccountId);

        }
    }
}