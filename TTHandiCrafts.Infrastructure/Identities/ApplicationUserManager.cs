using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TTHandiCrafts.Infrastructure.Identities
{
    public class ApplicationUserManager<TUser> : UserManager<TUser>, IDisposable where TUser : class
    {
        private readonly TTHandiCraftsIdentityDbContext dbContext;

        public ApplicationUserManager(IUserStore<TUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<TUser> passwordHasher,
            IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<TUser>> logger,
            TTHandiCraftsIdentityDbContext dbContext) : base(store, optionsAccessor, passwordHasher, userValidators,
            passwordValidators,
            keyNormalizer, errors, services, logger)
        {
            this.dbContext = dbContext;
        }

        public override async Task<IdentityResult> ChangePasswordAsync(TUser user, string currentPassword,
            string newPassword)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            var result = await base.ChangePasswordAsync(user, currentPassword, newPassword);
            await transaction.CommitAsync();

            return result;
        }

        public override async Task<IdentityResult> ResetPasswordAsync(TUser user, string token, string newPassword)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            var result = await base.ResetPasswordAsync(user, token, newPassword);
            await transaction.CommitAsync();
            return result;
        }

        public override async Task<IdentityResult> AddPasswordAsync(TUser user, string password)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            var result = await base.AddPasswordAsync(user, password);

            await transaction.CommitAsync();
            return result;
        }

        public override async Task<IdentityResult> CreateAsync(TUser user, string password)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            var result = await base.CreateAsync(user, password);

            await transaction.CommitAsync();
            return result;
        }
    }
}