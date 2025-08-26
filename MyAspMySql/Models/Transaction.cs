using System.ComponentModel.DataAnnotations;

namespace MyAspMySql.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; } // TopUp, Purchase, Refund

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Account Account { get; set; }
        public Payment Payment { get; set; }
    }
}