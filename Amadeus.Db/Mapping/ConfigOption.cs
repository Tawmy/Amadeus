using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping
{
    public class ConfigOption : IEntityTypeConfiguration<Models.ConfigOption>
    {
        public void Configure(EntityTypeBuilder<Models.ConfigOption> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Description).IsRequired();

            b.HasOne(x => x.ConfigOptionCategory)
                .WithMany(y => y.ConfigOptions)
                .HasForeignKey(x => x.ConfigOptionCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}