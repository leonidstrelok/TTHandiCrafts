using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTHandiCrafts.Models.Models.UserModels;

namespace TTHandiCrafts.Infrastructure.Persistences.Configurations.UserConfigurations
{
    public class UserWorkConfiguration : IEntityTypeConfiguration<UserWork>
    {
        public void Configure(EntityTypeBuilder<UserWork> builder)
        {
            
        }
    }
}