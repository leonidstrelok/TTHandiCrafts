using System.Collections.Generic;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Identity.Interfaces.Dtos;

namespace TTHandiCrafts.Infrastructure.Identity.Interfaces
{
    public interface IRoleManager
    {
        Task CreateRoleAsync(string role, IEnumerable<RoleClaims> claims);
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<RoleDetailDto> GetRoleById(string id);
        Task<string> GetRoleByName(string roleName);
        Task UpdateClaimsAsync(string id, IEnumerable<RoleClaims> claims);
        Task UpdateRoleNameAsync(string id, string roleName);
        Task DeleteRoleAsync(string id);
    }
}
