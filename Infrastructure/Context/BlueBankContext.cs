using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    internal class BlueBankContext : DbContext
    {
        internal DbSet<Person> People { get; set; }
        internal DbSet<Contact> Contacts { get; set; }
        internal DbSet<Account> Accounts { get; set; }
        internal DbSet<Balance> Balances { get; set; }
        internal DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BlueBanckDB;Trusted_Connection=true;");
            //modelBuilder
            //    .Entity<PromocaoProduto>()
            //    .HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });
            //base.OnModelCreating(modelBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                        .HasOne(m => m.AccountFrom)
                        .WithMany(t => t.OutgoingTransactions)
                        .HasForeignKey(m => m.AccountFromId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                        .HasOne(m => m.AccountTo)
                        .WithMany(t => t.IncomingTransactions)
                        .HasForeignKey(m => m.AccountToId)
                        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}