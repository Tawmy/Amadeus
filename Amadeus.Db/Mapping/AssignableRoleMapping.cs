using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping;

public class AssignableRoleMapping : IEntityTypeConfiguration<AssignableRole>
{
    public void Configure(EntityTypeBuilder<AssignableRole> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).ValueGeneratedNever();
        
        b.HasOne(x => x.Guild)
            .WithMany(y => y.AssignableRoles)
            .HasForeignKey(x => x.GuildId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}