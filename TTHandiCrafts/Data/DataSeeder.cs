using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;

namespace TTHandiCrafts.Data
{
    public class DataSeeder
    {
        private readonly IApplicationDbContext dbContext;

        public DataSeeder(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task MigrationData()
        {
        }
    }
}