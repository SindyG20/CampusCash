using System;
using System.Linq;
using MyAspMySql.Models;

namespace MyAspMySql.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Make sure the DB is created
            context.Database.EnsureCreated();

            // If we already have users, no need to seed again
            if (context.Users.Any())
            {
                return; 
            }

            // Create a test user
            var user = new User
            {
                Username = "john_doe",
                Email = "john@example.com",
                PasswordHash = "12345", // ⚠ For demo only (later hash it)
                Role = "Student",
                CreatedAt = DateTime.Now
            };

             context.Users.Add(user);
            context.SaveChanges();

            // Create an account for this user
            var account = new Account
            {
                UserId = user.Id,
                AccountNumber = new Random().Next(10000000, 99999999).ToString(),
                Balance = 250.00m,
                CreatedAt = DateTime.Now
            };

            context.Accounts.Add(account);
            context.SaveChanges();
        }
    }
}
