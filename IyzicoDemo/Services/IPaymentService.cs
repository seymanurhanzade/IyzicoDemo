using IyzicoDemo.Models;

namespace IyzicoDemo.Services
{
    public interface IPaymentService
    {
        Task<CreateCheckoutResponse> CreateCheckoutSessionAsync(
            CreateCheckoutRequest request,
            string baseUrl
        );

        Task HandleCheckoutCompletedAsync(string sessionId);
    }
}