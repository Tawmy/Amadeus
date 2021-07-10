using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amadeus.Db.Mapping
{
    public class Guild : IEntityTypeConfiguration<Models.Guild>
    {
        public void Configure(EntityTypeBuilder<Models.Guild> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}