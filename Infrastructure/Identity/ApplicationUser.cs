using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Pixel.Kidsparties.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string NameIdentifier { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
    }
}
