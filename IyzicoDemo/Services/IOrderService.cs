using IyzicoDemo.Entity;

namespace IyzicoDemo.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrderItems();
    }
}
