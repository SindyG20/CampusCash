using System.ComponentModel.DataAnnotations;

namespace MyAspMySql.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, StringLength(20)]
        public string AccountNumber { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; } = 0.00M;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}