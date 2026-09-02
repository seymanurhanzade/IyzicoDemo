using Iyzipay.Model;
using System.ComponentModel.DataAnnotations;

namespace IyzicoDemo.Entity
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public int OrderNo { get; set; } 
        public string GuestCartId { get; set; } = null!;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public long Total { get; set; }  
        public string Currency { get; set; } = "try";
        public OrderStatus Status { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAtUtc { get; set; }
        public List<OrderItems> OrderItems { get; set; } = null!;
        public int OrderItemsCount { get; set; } = 0;
    }

    public enum OrderStatus
    {
        Pending = 0,        // Sipariş alındı
        PaymentFailed = 1,  // Ödeme başarısız
        Paid = 2,           // Ödeme alındı
        Preparing = 3,      // Hazırlanıyor
        Shipped = 4,        // Kargoya verildi
        Delivered = 5,      // Teslim edildi
        Cancelled = 6,      // İptal edildi
        Refunded = 7        // İade edildi
    }
}
