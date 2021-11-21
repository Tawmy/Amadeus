using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping;

public class GuildMapping : IEntityTypeConfiguration<Guild>
{
    public void Configure(EntityTypeBuilder<Guild> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).ValueGeneratedNever();
    }
}