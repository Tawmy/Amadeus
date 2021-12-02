using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping;

public class
    CommandConfigDiscordEntityAssignmentMapping : IEntityTypeConfiguration<CommandConfigDiscordEntityAssignment>
{
    public void Configure(EntityTypeBuilder<CommandConfigDiscordEntityAssignment> b)
    {
        b.HasKey(x => x.Id);

        b.HasOne(x => x.CommandConfig)
            .WithMany(y => y.CommandConfigDiscordEntityAssignments)
            .HasForeignKey(x => x.CommandConfigId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.DiscordEntity)
            .WithMany(y => y.CommandConfigDiscordEntityAssignments)
            .HasForeignKey(x => x.DiscordEntityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}