using Amadeus.Db.Enums;
using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace Amadeus.Db;

public class AmadeusContext : DbContext
{
    // constructor for API
    public AmadeusContext(DbContextOptions<AmadeusContext> options) : base(options)
    {
        MapEnums();
    }

    // constructor for EntityRepository
    public AmadeusContext()
    {
        MapEnums();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(Configuration.ConnectionString).UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresEnum<DiscordEntityType>();
    }

    private static void MapEnums()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<DiscordEntityType>();
    }

    #region DbSets

    public DbSet<CommandConfig> CommandConfigs => Set<CommandConfig>();

    public DbSet<CommandConfigDiscordEntityAssignment> CommandConfigDiscordEntityAssignments =>
        Set<CommandConfigDiscordEntityAssignment>();

    public DbSet<Config> Configs => Set<Config>();
    public DbSet<DiscordEntity> DiscordEntities => Set<DiscordEntity>();
    public DbSet<Guild> Guilds => Set<Guild>();
    public DbSet<SelfAssignMenu> SelfAssignMenus => Set<SelfAssignMenu>();

    public DbSet<SelfAssignMenuDiscordEntityAssignment> SelfAssignMenuDiscordEntityAssignments =>
        Set<SelfAssignMenuDiscordEntityAssignment>();

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