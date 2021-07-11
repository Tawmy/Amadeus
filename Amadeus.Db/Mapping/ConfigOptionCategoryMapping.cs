using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping
{
    public class ConfigOptionCategoryMapping : IEntityTypeConfiguration<ConfigOptionCategory>
    {
        public void Configure(EntityTypeBuilder<ConfigOptionCategory> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Description).IsRequired();
        }
    }
}