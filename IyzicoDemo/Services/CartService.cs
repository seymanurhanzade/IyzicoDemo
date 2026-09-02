using Azure;
using IyzicoDemo.Entity;
using IyzicoDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IyzicoDemo.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetOrCreateGuestId()
        {
            const string cookieName = "guest_id";
            var httpcontext = _httpContextAccessor.HttpContext;

            if (httpcontext.Request.Cookies.TryGetValue(cookieName, out var existingGuestId) &&
                !string.IsNullOrWhiteSpace(existingGuestId))
            {
                return existingGuestId;
            }

            var newGuestId = Guid.NewGuid().ToString();

            httpcontext.Response.Cookies.Append(cookieName, newGuestId, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return newGuestId;
        }
        public async Task<IActionResult> CreateCart(CreateCartRequest model)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == model.ProductId);

            if (model != null)
            {
                Guid cartId;
                var guestId = GetOrCreateGuestId().ToString();
                Console.WriteLine($"GuestId: {guestId}");

                if (guestId == null)
                {
                    var cartEntity = new Cart
                    {
                        guestId = guestId,
                        CreatedAtUtc = DateTime.UtcNow,
                        //CartItems = new List<CartItem>()
                    };

                    await _context.Carts.AddAsync(cartEntity);
                    await _context.SaveChangesAsync();

                    cartId = cartEntity.Id;
                }
                else
                {
                    var checkGuestId = _context.Carts.FirstOrDefault(x => x.guestId == guestId);
                    if (checkGuestId == null)
                    {
                        var cartEntity = new Cart
                        {
                            guestId = guestId,
                            CreatedAtUtc = DateTime.UtcNow,
                            //CartItems = new List<CartItem>()
                        };

                        await _context.Carts.AddAsync(cartEntity);
                        await _context.SaveChangesAsync();

                        cartId = cartEntity.Id;
                    }

                    else
                    {
                        cartId = checkGuestId.Id;
                    }
                }
                var CartItemEntity = _context.CartItems
                        .FirstOrDefault(x => x.CartId == cartId && x.ProductId == model.ProductId);

                if (CartItemEntity == null && product != null)
                {
                    await _context.CartItems.AddAsync(new CartItem
                    {
                        CartId = cartId,
                        ProductId = model.ProductId,
                        Price = product.Price,
                        Quantity = model.Quantity,
                        Total = product.Price * model.Quantity
                    });
                    await _context.SaveChangesAsync();

                }
                else
                {
                    if (CartItemEntity != null)
                        CartItemEntity.Quantity += model.Quantity;
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult<IActionResult>(new BadRequestObjectResult("Model boş olamaz."));
            }
            return await Task.FromResult<IActionResult>(new OkObjectResult("Cart başarıyla oluşturuldu."));
        }
        public void DeleteCartsWithOrder()
        {
            var guestId = GetOrCreateGuestId().ToString();
            var cartItem = _context.Carts.Include(x => x.CartItems).FirstOrDefault(x => x.guestId == guestId);
            //var cartItem =  _context.CartItems.FirstOrDefault(x => x.Id == id);
            var cartItemId = cartItem?.CartItems.Where(x => cartItem.Id == x.CartId).FirstOrDefault();
            if (cartItemId != null)
            {

                try
                {
                    _context.CartItems.Remove(cartItemId);
                    var save = _context.SaveChanges();
                    if (save > 0)
                    {
                        var cart = _context.Carts.FirstOrDefault(x => x.guestId == guestId);
                        if (cart != null)
                        {
                            _context.Carts.Remove(cart);
                            _context.SaveChanges();
                        }

                    }
                    else
                    {
                        Console.WriteLine($"No changes were made when trying to delete CartItem with Id {cartItem.Id}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting CartItem with Id {cartItem.Id}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"CartItem with Id {cartItemId} not found.");
            }
        }
   
        public void DeleteCartItem(Guid id)
        {
            var cartItem = _context.CartItems.Find(id);
            if(cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }
    }
}
