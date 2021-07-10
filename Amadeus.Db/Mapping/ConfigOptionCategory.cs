using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping
{
    public class ConfigOptionCategory : IEntityTypeConfiguration<Models.ConfigOptionCategory>
    {
        public void Configure(EntityTypeBuilder<Models.ConfigOptionCategory> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Description).IsRequired();
        }
    }
}