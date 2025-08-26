using System.ComponentModel.DataAnnotations;

namespace MyAspMySql.Models
{
    public class Merchant
    {
        public int MerchantId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<Payment> Payments { get; set; }
    }
}