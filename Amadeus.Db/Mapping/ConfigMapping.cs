using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping;

public class ConfigMapping : IEntityTypeConfiguration<Config>
{
    public void Configure(EntityTypeBuilder<Config> b)
    {
        b.HasKey(x => x.Id);

        b.HasOne(x => x.Guild)
            .WithMany(y => y.Configs)
            .HasForeignKey(x => x.GuildId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Property(x => x.Value).IsRequired();
    }
}