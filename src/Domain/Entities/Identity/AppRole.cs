using Microsoft.AspNetCore.Identity;

namespace TechnestHackhaton.Domain.Entity.Identity
{
    public class AppRole : IdentityRole<string>
    {
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
