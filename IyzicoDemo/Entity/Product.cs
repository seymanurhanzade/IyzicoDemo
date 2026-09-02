using System.ComponentModel.DataAnnotations;

namespace IyzicoDemo.Entity
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public long Price { get; set; }
        public string Explanation { get; set; } = null!;
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<ProductImage> ProductImages { get; set; } = null!;
        public OfferActive OfferActive { get; set; }
        public long OfferPrice { get; set; }
    }

    public enum OfferActive
    {
        Active,
        Inactive
    }
}
