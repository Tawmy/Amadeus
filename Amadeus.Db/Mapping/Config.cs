using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping
{
    public class Config : IEntityTypeConfiguration<Models.Config>
    {
        public void Configure(EntityTypeBuilder<Models.Config> b)
        {
            b.HasKey(x => x.Id);

            b.HasOne(x => x.ConfigOption)
                .WithMany(y => y.Configs)
                .HasForeignKey(x => x.ConfigOptionId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Guild)
                .WithMany(y => y.Configs)
                .HasForeignKey(x => x.GuildId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Property(x => x.Value).IsRequired();
        }
    }
}