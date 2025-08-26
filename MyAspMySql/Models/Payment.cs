namespace MyAspMySql.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int TransactionId { get; set; }
        public int MerchantId { get; set; }

        // Navigation
        public Transaction Transaction { get; set; }
        public Merchant Merchant { get; set; }
    }
}