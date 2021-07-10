using Microsoft.AspNetCore.Authorization;
using TTHandiCrafts.UseCases.Commons.Constants;

namespace TTHandiCrafts.Infrastructure.Security.Extensions
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddSecurityPolicies(this AuthorizationOptions options)
        {
            options.AddAdminPolicy();
  
        }

        private static void AddAdminPolicy(this AuthorizationOptions options)
        {
            options.AddPolicy(Policys.Policys.Admin,
                builder => builder.RequireRole(Roles.ADMIN));
        }
    }
}