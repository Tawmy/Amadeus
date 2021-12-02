using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping;

public class CommandConfigMapping : IEntityTypeConfiguration<CommandConfig>
{
    public void Configure(EntityTypeBuilder<CommandConfig> b)
    {
        b.HasKey(x => x.Id);
    }
}