using IyzicoDemo.Models;
using IyzicoDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IyzicoDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest model)
        {
            return await _accountService.Register(model);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest model)
        {
            return await _accountService.Login(model);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return await _accountService.Logout();
        }

        [HttpGet("management-counts")]
        public async Task<List<AdminModel>> GetCountsforAdmin()
        {
            return await _accountService.GetCountsforAdmin();
        }
    }
}
