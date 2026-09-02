namespace IyzicoDemo.Models
{
    public class CreateCheckoutResponse
    {
        public string Status { get; set; } = null!;

        public string PaymentId { get; set; } = null!;

        public string ConversationId { get; set; } = null!;
    }
}
