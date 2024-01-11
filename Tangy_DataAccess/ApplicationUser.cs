using Microsoft.AspNetCore.Identity;

namespace Tangy_DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
