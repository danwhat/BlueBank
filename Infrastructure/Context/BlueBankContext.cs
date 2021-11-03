using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BlueBankContext : DbContext
    {
        internal DbSet<Person> People { get; set; }
        internal DbSet<Contact> Contacts { get; set; }
        internal DbSet<Account> Accounts { get; set; }
        internal DbSet<Balance> Balances { get; set; }
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

            modelBuilder.Entity<Account>()
                .Property(account => account.IsActive)
                .HasDefaultValue(true);
        }
    }
}