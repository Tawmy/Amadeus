using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping
{
    public class ConfigOptionMapping : IEntityTypeConfiguration<ConfigOption>
    {
        public void Configure(EntityTypeBuilder<ConfigOption> b)
        {
            b.HasKey(x => x.Id);

            b.HasOne(x => x.ConfigOptionCategory)
                .WithMany(y => y.ConfigOptions)
                .HasForeignKey(x => x.ConfigOptionCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Description).IsRequired();
        }
    }
}