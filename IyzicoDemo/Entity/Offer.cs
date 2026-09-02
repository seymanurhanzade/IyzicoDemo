using System.ComponentModel.DataAnnotations;

namespace IyzicoDemo.Entity
{
    public class Offer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Products { get; set; } = null!;
        public long DiscountedPrice { get; set; }
        public long DiscountedAmount{get; set;}
    }
}
