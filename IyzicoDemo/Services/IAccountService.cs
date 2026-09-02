using IyzicoDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace IyzicoDemo.Services
{
    public interface IAccountService
    {
            Task<IActionResult> Register(RegisterRequest model);
            Task<IActionResult> Login(LoginRequest model);
            Task<IActionResult> Logout();
            Task<List<AdminModel>> GetCountsforAdmin();
    }
}
