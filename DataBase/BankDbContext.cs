
using Bank.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bank.DataBase
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(o => o.Username).IsUnique();

        }
    }
}
