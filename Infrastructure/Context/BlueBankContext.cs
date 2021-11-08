using Infrastructure.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BlueBankContext : DbContext
    {
        public BlueBankContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        internal DbSet<Person> People { get; set; }
        internal DbSet<Contact> Contacts { get; set; }
        internal DbSet<Account> Accounts { get; set; }
        internal DbSet<TransactionLog> TransactionLog { get; set; }
        internal DbSet<Transaction> Transactions { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BlueBankDB;Trusted_Connection=true;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PersonConfiguration().Configure(modelBuilder.Entity<Person>());

            new AccountConfiguration().Configure(modelBuilder.Entity<Account>());

            new ContactConfiguration().Configure(modelBuilder.Entity<Contact>());

            new TransactionConfiguration().Configure(modelBuilder.Entity<Transaction>());

            new TransactionLogConfiguration().Configure(modelBuilder.Entity<TransactionLog>());
        }
    }
}