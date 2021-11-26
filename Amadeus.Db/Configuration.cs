using Amadeus.Db.Models;

namespace Amadeus.Db;

public static class Configuration
{
    public static string ConnectionString
    {
        get
        {
#if DEBUG
            return @"Host=localhost;Database=amadeus;Username=tawmy";
#else
            return @"Host=localhost;Database=amadeus;Username=tawmy";
#endif
        }
    }

    public static Dictionary<ulong, List<Config>> GuildConfigs { get; set; }
}