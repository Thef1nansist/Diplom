using Microsoft.AspNetCore.Identity;

namespace Infastructure.Models
{
    public class AppUser :IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
