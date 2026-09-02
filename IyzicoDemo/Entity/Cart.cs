using System;
using System.ComponentModel.DataAnnotations;

namespace IyzicoDemo.Entity
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public string guestId { get; set; } = null!;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
