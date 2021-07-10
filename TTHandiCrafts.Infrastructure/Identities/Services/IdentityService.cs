using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.UseCases.Commons.Constants;
using TTHandiCrafts.UseCases.Commons.Exceptions;

namespace TTHandiCrafts.Infrastructure.Identities.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationUserManager<IdentityUser> userManager;
        public IdentityService(ApplicationUserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> CreateUserAsync(string userName, string password,
            bool needChangePassword = true,
            CancellationToken cancellationToken = default)
        {
            var user = new IdentityUser()
            {
                UserName = userName,
            };
            var result = await userManager.CreateAsync(user, password);
            ThrowExceptionIfNotSuccess(result);

            return user.Id;
        }

        public async Task ChangeUserPassword(string userId, string newPassword,
            CancellationToken cancellationToken = default)
        {
            var user = await userManager.FindByIdAsync(userId);
            ThrowExceptionIfNull(userId, user);
            var result = await userManager.RemovePasswordAsync(user);
            ThrowExceptionIfNotSuccess(result);
            var setPasswordResult = await userManager.AddPasswordAsync(user, newPassword);
            ThrowExceptionIfNotSuccess(setPasswordResult);
        }

        public async Task UpdateUsernameAsync(string userId, string userName)
        {
            var user = await userManager.FindByIdAsync(userId);
            ThrowExceptionIfNull(userId, user);
            var result = await userManager.SetUserNameAsync(user, userName);
            ThrowExceptionIfNotSuccess(result);
        }

        public async Task AddToClaimAsync(string userId, string claimType, string claimValue,
            CancellationToken cancellationToken = default)
        {
            var user = await userManager.FindByIdAsync(userId);
            ThrowExceptionIfNull(userId, user);
            var claims = await userManager.GetClaimsAsync(user);
            if (!claims.Any(p => p.Type == claimType))
            {
                var result = await userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
                ThrowExceptionIfNotSuccess(result);
                return;
            }

            if (claimType.Contains(EmployeeClaimTypes.RoleId))
            {
                var claim = claims.First(p => p.Type == claimType);
                await userManager.RemoveClaimAsync(user, claim);
                var result = await userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
            }
        }

        public async Task AddToRoleUser(string userId, string role, CancellationToken cancellationToken = default)
        {
            var user = await userManager.FindByIdAsync(userId);
            ThrowExceptionIfNull(userId, user);
            var userRoles = await userManager.GetRolesAsync(user);

            if (userRoles.Any(p => p.Contains(role)))
            {
                return;
            }

            var roles = await userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                var removeRole = await userManager.RemoveFromRolesAsync(user, roles);
                ThrowExceptionIfNotSuccess(removeRole);
            }


            var result = await userManager.AddToRoleAsync(user, role);
            ThrowExceptionIfNotSuccess(result);
        }

        public async Task<string> GetUserName(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            ThrowExceptionIfNull(userId, user);
            return user.UserName;
        }

        public async Task RemoveUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            ThrowExceptionIfNull(userId, user);
            var result = await userManager.DeleteAsync(user);
            ThrowExceptionIfNotSuccess(result);
        }

        private static void ThrowExceptionIfNull(string userId, IdentityUser user)
        {
            if (user == null)
            {
                throw new NotFoundException(nameof(IdentityUser), userId);
            }
        }

        private static void ThrowExceptionIfNotSuccess(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new ValidationException(
                    identityResult.Errors.Select(prop => new ValidationFailure(prop.Code, prop.Description)));
            }
        }
    }
}