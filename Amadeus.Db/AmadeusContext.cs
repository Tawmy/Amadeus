using System.IO;
using System.Reflection;
using Amadeus.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Amadeus.Db
{
    public class AmadeusContext : DbContext
    {
        public AmadeusContext(DbContextOptions<AmadeusContext> options) : base(options)
        {
        }

        #region DbSets

        public DbSet<Guild> Guilds { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
    
    // Necessary for using EF migrations in Db project
    // Reference: https://medium.com/oppr/net-core-using-entity-framework-core-in-a-separate-project-e8636f9dc9e5
    // Thank you Mr. Santos
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AmadeusContext> 
    { 
        public AmadeusContext CreateDbContext(string[] args) 
        { 
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Amadeus.Web/appsettings.json").Build(); 
            var builder = new DbContextOptionsBuilder<AmadeusContext>(); 
            var connectionString = configuration.GetConnectionString("AmadeusDev"); 
            builder.UseNpgsql(connectionString).UseCamelCaseNamingConvention(); 
            return new AmadeusContext(builder.Options); 
        } 
    }
}