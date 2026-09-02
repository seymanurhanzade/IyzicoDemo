namespace IyzicoDemo.Models
{
    public class CreateCheckoutRequest
    {
        public string CustomerName { get; set; } = null!;

        public string CustomerEmail { get; set; } = null!;

        public string CardHolderName { get; set; } = null!;

        public string CardNumber { get; set; } = null!;

        public string ExpireMonth { get; set; } = null!;

        public string ExpireYear { get; set; } = null!;

        public string Cvc { get; set; } = null!;

    }
}
