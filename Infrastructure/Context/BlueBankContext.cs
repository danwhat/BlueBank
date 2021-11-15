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

            var person1 = new Person { Name = "Daniel", Doc = "11111111111", Address = "Rua 1", Id = 1, Type = 1 };
            var person2 = new Person { Name = "Ana Karine", Doc = "22222222222", Address = "Rua 2", Id = 2, Type = 1 };

            modelBuilder.Entity<Person>().HasData(person1, person2);

            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, PersonId = person1.Id, PhoneNumber = "(11) 0011-1406" },
                new Contact { Id = 2, PersonId = person1.Id, PhoneNumber = "(22) 2201-2226" },
                new Contact { Id = 3, PersonId = person2.Id, PhoneNumber = "(00) 0001-0101" });

            var account1 = new Account { PersonId = 1, Id = 1 };
            var account2 = new Account { PersonId = 2, Id = 2 };

            modelBuilder.Entity<Account>().HasData(account1, account2);

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { AccountToId = 1, Value = 1000, Id = 1 },
                new Transaction { AccountToId = 2, Value = 1000, Id = 2 },
                new Transaction { AccountFromId = 1, AccountToId = 2, Value = 99, Id = 3 }
                );

            modelBuilder.Entity<TransactionLog>().HasData(
                new TransactionLog { Id = 1, AccountId = 1, TransactionId = 1, BalanceAfter = 1000, Value = 1000 },
                new TransactionLog { Id = 2, AccountId = 2, TransactionId = 2, BalanceAfter = 1000, Value = 1000 },
                new TransactionLog { Id = 3, AccountId = 1, TransactionId = 3, BalanceAfter = 901, Value = 99 },
                new TransactionLog { Id = 4, AccountId = 2, TransactionId = 3, BalanceAfter = 1099, Value = 99 }
            );
        }
    }
}