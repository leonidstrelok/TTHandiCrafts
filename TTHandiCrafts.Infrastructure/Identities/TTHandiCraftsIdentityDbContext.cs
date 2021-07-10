using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TTHandiCrafts.Infrastructure.Identities
{
    public class TTHandiCraftsIdentityDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public TTHandiCraftsIdentityDbContext(DbContextOptions<TTHandiCraftsIdentityDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TTHandiCraftsIdentityDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}