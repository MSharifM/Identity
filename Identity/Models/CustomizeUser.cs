using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class CustomizeUser : IdentityUser
    {
        public string City { get; set; }
    }
}
