using TechnestHackhaton.Domain.Entity.Common;

namespace TechnestHackhaton.Domain.Entity
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
