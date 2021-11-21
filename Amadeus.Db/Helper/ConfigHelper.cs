using Amadeus.Db.Models;
using logm.EntityRepository.Core;

namespace Amadeus.Db.Helper;

public class ConfigHelper
{
    public static async Task LoadConfigs()
    {
        // load options for default values and getting names

        var configs = await EntityRepository<AmadeusContext, Config>.GetAllAsync();
        var guilds = configs.Select(x => x.GuildId).Distinct().ToList();

        var dicts = new Dictionary<ulong, List<Config>>();
        guilds.ForEach(x => dicts.Add(x, configs.Where(y => y.GuildId == x).ToList()));
        Configuration.GuildConfigs = dicts;
    }
}