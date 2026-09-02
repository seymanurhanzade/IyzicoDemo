using IyzicoDemo.Entity;
using IyzicoDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace IyzicoDemo.Services
{
    public interface ICartService
    {
        Task<IActionResult> CreateCart(CreateCartRequest model);
        void DeleteCartsWithOrder();
        void DeleteCartItem(Guid id);
    }
}
