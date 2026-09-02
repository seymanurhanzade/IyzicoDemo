using System.ComponentModel.DataAnnotations;

namespace IyzicoDemo.Entity
{
    public class PaymentTable
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public string Provider { get; set; } = "Stripe";
        public string Status { get; set; } = "Pending"; // Pending, Paid, Cancelled, Failed
        public long TotalAmount { get; set; }
        public string Currency { get; set; } = "try";
        public string StripeSessionId { get; set; } = string.Empty;
        public string? StripePaymentIntentId { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAtUtc { get; set; }
    }
}
