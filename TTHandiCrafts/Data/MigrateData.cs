using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TTHandiCrafts.Infrastructure.Identities;
using TTHandiCrafts.Infrastructure.Persistences;

namespace TTHandiCrafts.Data
{
    public class MigrateData
    {
        private readonly AppDbContext dbContext;
        private readonly TTHandiCraftsIdentityDbContext _identityDbContext;

        public MigrateData(AppDbContext dbContext,
            TTHandiCraftsIdentityDbContext identityDbContext)
        {
            this.dbContext = dbContext;
            this._identityDbContext = identityDbContext;
        }

        public async Task MigrationData()
        {
            await dbContext.Database.MigrateAsync();
            await _identityDbContext.Database.MigrateAsync();
        }
    }
}