using System;
using System.Security.Claims;
using TTHandiCrafts.Infrastructure.Identity.Interfaces.Enums;

namespace TTHandiCrafts.Infrastructure.Identity.Interfaces.Dtos
{
    public class RoleClaims
    {
        public RoleClaims()
        {

        }

        public RoleClaims(Claim claim)
        {
            Type = Enum.Parse<UserClaimTypes>(claim.Type);
            Value = (Permission)int.Parse(claim.Value);
        }
        public UserClaimTypes Type { get; set; }
        public Permission Value { get; set; }
    }
}
