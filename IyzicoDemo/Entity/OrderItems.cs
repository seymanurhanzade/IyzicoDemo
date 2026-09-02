using System.ComponentModel.DataAnnotations;

namespace IyzicoDemo.Entity
{
    public class OrderItems
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public Guid ProductId { get; set; } 
        public Product Product { get; set; } = null!;
        public long Price { get; set; }
        public int Quantity { get; set; }
        public long Total { get; set; }
    }
}
