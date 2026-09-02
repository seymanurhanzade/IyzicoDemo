using IyzicoDemo.Entity;
using IyzicoDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IyzicoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        // POST: OrderController/Edit/5
        [HttpGet]
        public async Task<List<Order>> GetOrders()
        {
            return await _orderService.GetOrderItems();
        }
    }
}
