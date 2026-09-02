using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Options;
using IyzicoDemo.Entity;
using IyzicoDemo.Models;
using IyzicoDemo.Settings;
using Microsoft.EntityFrameworkCore;

namespace IyzicoDemo.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IyzicoOption _options;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartService _cartService;

        public IyzicoPaymentService(IOptions<IyzicoOption> options, AppDbContext context, IHttpContextAccessor httpContextAccessor, ICartService cartService)
        {
            _options = options.Value;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _cartService = cartService;
        }

        public string GetGuestId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                if (httpContext.Request.Cookies.TryGetValue("guest_id", out var guestId) && !string.IsNullOrWhiteSpace(guestId))
                {
                    return guestId;
                }
            }
            return null;
        }
        public async Task<int> CreateOrderNo()
        {
            var orderNo = new Random().Next(100000, 999999);
            return orderNo;
        }
        public async Task<CreateCheckoutResponse> CreateCheckoutSessionAsync(
            CreateCheckoutRequest request,
            string baseUrl)
        {
            var guestId = GetGuestId();
            var existingCart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(x => x.guestId == guestId);

            var options = new Iyzipay.Options
            {
                ApiKey = _options.ApiKey,
                SecretKey = _options.SecretKey,
                BaseUrl = _options.BaseUrl
            };

            var paymentRequest = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = existingCart.CartItems.Sum(x => x.Price * x.Quantity).ToString(),
                PaidPrice = existingCart.CartItems.Sum(x => x.Price * x.Quantity).ToString(),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = "B" + DateTime.Now.Ticks,
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString()
            };

            paymentRequest.PaymentCard = new PaymentCard
            {
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                ExpireMonth = request.ExpireMonth,
                ExpireYear = request.ExpireYear,
                Cvc = request.Cvc,
                RegisterCard = 0
            };

            paymentRequest.Buyer = new Buyer
            {
                Id = "BY789",
                Name = request.CustomerName,
                Surname = "-",
                Email = request.CustomerEmail,
                IdentityNumber = "11111111111",
                RegistrationAddress = "Address",
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732",
                Ip = "85.34.78.112"
            };

            paymentRequest.BillingAddress = new Address
            {
                ContactName = request.CustomerName,
                City = "Istanbul",
                Country = "Turkey",
                Description = "Test Address",
                ZipCode = "34732"
            };
            paymentRequest.ShippingAddress = new Address
            {
                ContactName = request.CustomerName,
                City = "Istanbul",
                Country = "Turkey",
                Description = "Test Address",
                ZipCode = "34732"
            };

            paymentRequest.BasketItems = new List<BasketItem>
            {
                new BasketItem
                {
                    Id = "BI101",
                    Name = "Siparis",
                    Category1 = "Service",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = existingCart.CartItems.Sum(x => x.Price * x.Quantity).ToString()
                }
            };

            Payment payment = await Payment.Create(paymentRequest, options);
            await _context.SaveChangesAsync();


            if (payment.Status == "success")
            {
                var orderNo = await CreateOrderNo();
                try
                {
                    var entity = new Order
                    {
                        OrderNo = orderNo,
                        GuestCartId = guestId,
                        CustomerName = request.CustomerName,
                        CustomerEmail = request.CustomerEmail,
                        Total = existingCart.CartItems.Sum(x => x.Price * x.Quantity),
                        Currency = "TRY",
                        Status = OrderStatus.Paid,
                        CreatedAtUtc = DateTime.UtcNow
                    };
                    if (payment.Status == "success")
                    {
                        entity.PaidAtUtc = DateTime.UtcNow;
                    }
                    await _context.Orders.AddAsync(entity);
                    await _context.SaveChangesAsync();

                    if (payment.Status == "success")
                    {
                        foreach (var item in existingCart.CartItems)
                        {
                            var orderItemEntity = new OrderItems
                            {
                                OrderId = entity.Id,
                                ProductId = item.ProductId,
                                Price = item.Price,
                                Quantity = item.Quantity,
                                Total = item.Price * item.Quantity
                            };
                            await _context.OrderItems.AddAsync(orderItemEntity);
                            await _context.Products.Where(x => x.Id == item.ProductId).ForEachAsync(x => x.Quantity -= item.Quantity);
                        }
                    }
                    await _context.SaveChangesAsync();

                    var paymententity = new PaymentTable
                    {
                        OrderId = entity.Id,
                        Status = payment.Status,
                        TotalAmount = existingCart.CartItems.Sum(x => x.Price * x.Quantity),
                        Currency = "try",
                        CreatedAtUtc = DateTime.UtcNow
                    };
                    if (payment.Status == "success" && entity != null)
                    {
                        paymententity.StripeSessionId = payment.ConversationId;
                        paymententity.StripePaymentIntentId = payment.PaymentId;
                        paymententity.Status = "Paid";
                        paymententity.PaidAtUtc = DateTime.UtcNow;
                    }
                    _context.Payments.Add(paymententity);
                    await _context.SaveChangesAsync();

                    _cartService.DeleteCartsWithOrder();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Ödeme başarısız oldu. Hata mesajı: " + payment.ErrorMessage);
            }
            return new CreateCheckoutResponse
            {
                Status = payment.Status,
                PaymentId = payment.PaymentId,
                ConversationId = payment.ConversationId
            };
        }

        public Task HandleCheckoutCompletedAsync(string sessionId)
        {
            return Task.CompletedTask;
        }
    }
}