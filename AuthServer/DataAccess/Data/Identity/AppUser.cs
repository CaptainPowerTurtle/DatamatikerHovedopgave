using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}