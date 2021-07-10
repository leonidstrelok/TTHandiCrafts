using Microsoft.AspNetCore.Http;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.UseCases.Commons.Constants;
using TTHandiCrafts.UseCases.Commons.Extensions;

namespace TTHandiCrafts.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int UserId => GetUserId();
        public bool IsAdminRole => GetRole();


        private bool GetRole()
        {
            return httpContextAccessor.HttpContext.User.IsInRole(Roles.ADMIN);
        }

        private int GetUserId()
        {
            if (!httpContextAccessor.HttpContext.User.IsInRole(Roles.ADMIN))
            {
                return httpContextAccessor.HttpContext?.User?.UserId() ?? 0;
            }

            return 0;
        }
    }
}