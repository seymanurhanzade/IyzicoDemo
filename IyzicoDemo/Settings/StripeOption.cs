namespace IyzicoDemo.Settings
{
    public class IyzicoOption
    {
        public const string SectionName = "Stripe";

        public string ApiKey { get; set; } = null!;

        public string SecretKey { get; set; } = null!;

        public string BaseUrl { get; set; } = null!;
    }
}
