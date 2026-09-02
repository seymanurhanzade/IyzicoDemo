using IyzicoDemo.Entity;
using IyzicoDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IyzicoDemo.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountService(UserManager<AppUser> userManager, AppDbContext context, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public string JwtSecurityTokenHandler(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim(ClaimTypes.Role, userRole)

                }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Api:Key"] ?? "null-key")), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                Console.WriteLine("JWT Token oluşturuldu: " + tokenHandler.WriteToken(token));
                return tokenHandler.WriteToken(token);
            }
            else
            {
                return new BadRequestObjectResult(new { message = "Kullanıcı bulunamadı." }).ToString() ?? "Boş dönüş";
            }

        }
        //public static string UserNameEdit(string model)
        //{
        //    string newUserName = model.Trim();
        //    newUserName = newUserName.Replace(" ", "");
        //    newUserName = newUserName
        //        .Replace("Ç", "c").Replace("ç", "c")
        //        .Replace("Ş", "s").Replace("ş", "s")
        //        .Replace("İ", "i").Replace("ı", "i")
        //        .Replace("Ü", "u").Replace("ü", "u")
        //        .Replace("Ö", "o").Replace("ö", "o")
        //        .Replace("Ğ", "g").Replace("ğ", "g");
        //    newUserName = newUserName.ToLowerInvariant();
        //    var random = new Random().Next(000000000, 999999999).ToString();
        //    newUserName = $"{newUserName}-{random}";
        //    return newUserName;
        //}
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            //char[] fullNameChars = model.FullName.ToCharArray();
            //string userName = UserNameEdit(model.FullName);
            //if (userName == null)
            //{
            //    return new BadRequestObjectResult(new { message = "Kullanıcı adı boş olamaz." });
            //}
            //else
            //{
            try
            {
                var user = new AppUser { FullName = model.FullName.Trim(), Email = model.Email.Trim(), UserName = Guid.NewGuid().ToString() };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return new OkResult();
                }
                await _context.SaveChangesAsync();
                return new OkObjectResult(new { message = "BAŞARILI.", detail = result });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { message = "Hata oluştu", details = ex.Message });
            }
            //}
        }
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (model.Email == null || model.Password == null)
            {
                return new BadRequestObjectResult(new { message = "Email ve şifre alanları boş olamaz." });
            }
            else
            {
                var userModel = await _userManager.FindByEmailAsync(model.Email);
                if (userModel == null)
                {
                    return new BadRequestObjectResult(new { message = "Kullanıcı kaydı bulunamadı." });
                }
                else
                {
                    try
                    {
                        var loginManager = await _signInManager.PasswordSignInAsync(userModel, model.Password, isPersistent: true, lockoutOnFailure: false);
                        var role = await _userManager.GetRolesAsync(userModel);
                        if (loginManager.Succeeded)
                        {
                            var token = JwtSecurityTokenHandler(userModel.Id);
                            return new OkObjectResult(new { token = token , role = role , email = userModel.Email });
                        }
                        else
                        {
                            return new BadRequestObjectResult(new { message = "Giriş başarısız. Şifre yanlış olabilir." });
                        }
                    }
                    catch (Exception ex)
                    {
                        return new BadRequestObjectResult(new { message = "Giriş başarısız.", details = ex.Message });

                    }
                }

            }

        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return await Task.FromResult<IActionResult>(new OkObjectResult(new { message = "Çıkış başarılı." }));
            }
            catch (Exception ex)
            {
                return await Task.FromResult<IActionResult>(new BadRequestObjectResult(new { message = "Çıkış başarısız.", details = ex.Message }));
            }
        }

        public async Task<List<AdminModel>> GetCountsforAdmin()
        {
            var productCount = await _context.Products.CountAsync();
            var categoryCount = await _context.Categories.CountAsync();
            var userCount = await _context.Users.CountAsync();
            var checkoutCount = await _context.Orders.CountAsync();

            return new List<AdminModel>
            {
                new AdminModel
                {
                    ProductCount = productCount,
                    CategoryCount = categoryCount,
                    UserCount = userCount,
                    OrderCount = checkoutCount
                }
            }.ToList();
        }
    }
}
