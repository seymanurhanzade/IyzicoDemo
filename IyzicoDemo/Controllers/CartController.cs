using IyzicoDemo.Models;
using IyzicoDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IyzicoDemo.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: CartController/Create
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateCartRequest model)
        {
            await _cartService.CreateCart(model);
            return Ok();
        }

        [HttpDelete("delete-cart-item/{id}")]
        public void DeleteCartItem(Guid id)
        {
            _cartService.DeleteCartItem(id);
        }
    }
}
