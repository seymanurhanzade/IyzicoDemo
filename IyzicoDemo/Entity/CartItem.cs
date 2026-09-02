using System.ComponentModel.DataAnnotations;

namespace IyzicoDemo.Entity
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = null!;
        public Guid ProductId { get; set; } 
        public Product Product { get; set; } = null!;
        public long Price { get; set; }
        public int Quantity { get; set; }
        public long Total { get; set; }
    }
}
