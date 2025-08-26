using System.ComponentModel.DataAnnotations;

namespace MyAspMySql.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } = "Student"; // Student, Staff, Admin

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Account Account { get; set; }
    }
}