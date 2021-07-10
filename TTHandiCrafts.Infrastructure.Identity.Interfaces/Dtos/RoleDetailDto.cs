using System.Collections.Generic;

namespace TTHandiCrafts.Infrastructure.Identity.Interfaces.Dtos
{
    public class RoleDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<RoleClaims> Claims { get; set; }
    }
}
