namespace IyzicoDemo.Models
{
    public class CreateCartRequest
    {
        public string ProductName { get; set; } = null!;
        //public int CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
