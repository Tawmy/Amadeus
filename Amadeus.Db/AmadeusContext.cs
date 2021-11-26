using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Amadeus.Db;

public class AmadeusContext : DbContext
{
    // constructor for API
    public AmadeusContext(DbContextOptions<AmadeusContext> options) : base(options)
    {
    }

    // constructor for EntityRepository
    public AmadeusContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(Configuration.ConnectionString).UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }

    #region DbSets

    public DbSet<AssignableRole> AssignableRoles { get; set; }
    public DbSet<Config> Configs { get; set; }
    public DbSet<Guild> Guilds { get; set; }

    #endregion
}

// Necessary for using EF migrations in Db project
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AmadeusContext>
{
    public AmadeusContext CreateDbContext(string[] args)
    {
        var b = new DbContextOptionsBuilder<AmadeusContext>();
        b.UseNpgsql(Configuration.ConnectionString).UseSnakeCaseNamingConvention();
        return new AmadeusContext(b.Options);
    }
}