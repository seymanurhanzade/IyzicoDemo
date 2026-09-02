using System.ComponentModel.DataAnnotations;

namespace IyzicoDemo.Entity
{
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
     
}

