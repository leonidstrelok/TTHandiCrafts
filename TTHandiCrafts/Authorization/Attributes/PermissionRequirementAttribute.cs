using Microsoft.AspNetCore.Mvc;
using TTHandiCrafts.Authorization.Filters;
using TTHandiCrafts.Infrastructure.Identity.Interfaces.Enums;

namespace TTHandiCrafts.Authorization.Attributes
{
    public class PermissionRequirementAttribute : TypeFilterAttribute
    {
        public PermissionRequirementAttribute(UserClaimTypes claim, Permission permission) : base(typeof(PermissionRequirementFilter))
        {
            Arguments = new object[] { claim, permission };
        }
    }
}
