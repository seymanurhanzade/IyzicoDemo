using IyzicoDemo.Entity;
using Microsoft.EntityFrameworkCore;

namespace IyzicoDemo.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrderItems()
        {
            var entity = await _context.Orders.Include(x => x.OrderItems).Select(o => new Order
            {
                Id = o.Id,
                OrderNo = o.OrderNo,
                GuestCartId = o.GuestCartId,
                CustomerName = o.CustomerName,
                CustomerEmail = o.CustomerEmail,
                CustomerPhone = o.CustomerPhone,
                Total = o.Total,
                Currency = o.Currency,
                Status = o.Status,
                CreatedAtUtc = o.CreatedAtUtc,
                PaidAtUtc = o.PaidAtUtc,
                OrderItemsCount = o.OrderItems.Count,
                OrderItems = o.OrderItems.Select(oi => new OrderItems
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    ProductId = oi.ProductId,
                    Price = oi.Price,
                    Quantity = oi.Quantity,
                    Total = oi.Total
                }).ToList()
            }).ToListAsync();

            return entity;
        }

        
    }
}
