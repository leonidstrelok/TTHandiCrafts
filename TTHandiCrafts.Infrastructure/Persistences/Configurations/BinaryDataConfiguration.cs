using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TTHandiCrafts.Models.Models;

namespace TTHandiCrafts.Infrastructure.Persistences.Configurations
{
    public class BinaryDataConfiguration : IEntityTypeConfiguration<BinaryData>
    {
        public void Configure(EntityTypeBuilder<BinaryData> builder)
        {
            
        }
    }
}