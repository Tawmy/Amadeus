using Amadeus.Db.Models;

namespace Amadeus.Db;

public static class Configuration
{
    public static string ConnectionString { get; set; }

    public static Dictionary<ulong, List<Config>> GuildConfigs { get; set; }
}