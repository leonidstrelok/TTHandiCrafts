using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using TTHandiCrafts.Infrastructure.Identity.Interfaces.Enums;

namespace TTHandiCrafts.Authorization.Filters
{
    public class PermissionRequirementFilter : IAuthorizationFilter
    {
        private readonly UserClaimTypes claimType;
        private readonly Permission permission;

        public PermissionRequirementFilter(UserClaimTypes claimType, Permission permission)
        {
            this.claimType = claimType;
            this.permission = permission;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.FindAll(p => p.Type == claimType.ToString());

            if (!claims.Any())
            {
                context.Result = new ForbidResult();
                return;
            }

            var maxClaimValue = (Permission)claims.Max(p => int.Parse(p.Value));

            if (maxClaimValue < permission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
