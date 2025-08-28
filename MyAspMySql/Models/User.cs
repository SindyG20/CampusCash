using System;
using System.Collections.Generic;

namespace MyAspMySql.Models
{
    public class User
    {
        public int Id { get; set; }

        public string StudentId { get; set; } = "";
        public string StudentNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = "";
        public string ContactNumber { get; set; } = "";
        public string AccountType { get; set; } = "Regular";

        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string Role { get; set; } = "Student";
        public DateTime CreatedAt { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}