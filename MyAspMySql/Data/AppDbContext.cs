using Microsoft.EntityFrameworkCore;
using MyAspMySql.Models;

namespace MyAspMySql.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User → Account (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserId);
                // Account → Transactions (1:many)
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId);

            // Transaction → Payment (1:1)
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Payment)
                .WithOne(p => p.Transaction)
                .HasForeignKey<Payment>(p => p.TransactionId);

            // Merchant → Payments (1:many)
            modelBuilder.Entity<Merchant>()
                .HasMany(m => m.Payments)
                .WithOne(p => p.Merchant)
                .HasForeignKey(p => p.MerchantId);
        }
    }
}
