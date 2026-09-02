
using Microsoft.AspNetCore.Identity;

namespace IyzicoDemo.Entity
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; } = null!;
    }
}