using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping
{
    public class ProfileEntryMapping : IEntityTypeConfiguration<ProfileEntry>
    {
        public void Configure(EntityTypeBuilder<ProfileEntry> b)
        {
            b.HasKey(x => x.Id);

            b.HasOne(x => x.User)
                .WithMany(y => y.ProfileEntries)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Property(x => x.Value).IsRequired();
        }
    }
}