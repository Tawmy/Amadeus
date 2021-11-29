using Amadeus.Db.Enums;
using Amadeus.Db.Models;
using Amadeus.Db.Statics;
using DSharpPlus.Entities;
using logm.EntityRepository.Core;

namespace Amadeus.Db.Helper;

public static class ConfigHelper
{
    public static async Task LoadGuildConfigs()
    {
        // load options for default values and getting names

        var configs = await EntityRepository<AmadeusContext, Config>.GetAllAsync();
        var guilds = configs.Select(x => x.GuildId).Distinct().ToList();

        var dicts = new Dictionary<ulong, List<Config>>();
        guilds.ForEach(x => dicts.Add(x, configs.Where(y => y.GuildId == x).ToList()));
        Configuration.GuildConfigs = dicts;
    }

    public static async Task<string> GetString(string option, ulong? guildId = null)
    {
        var defaultOption = new ConfigOptions().Get(option);

        // when no guildId provided, return default value
        if (guildId == null) return defaultOption?.DefaultValue;

        // check if guild in dictionary
        // if so, get config for that specific option
        var cfgGuild = Configuration.GuildConfigs.TryGetValue(guildId.Value, out var configs)
            ? configs.FirstOrDefault(x => x.ConfigOptionId == defaultOption.Id)
            : null;

        if (cfgGuild != null) return cfgGuild.Value;

        // if no config set, get default value, set for guild, and return
        // this avoids user confusion if default bot behaviour is ever changed
        await Set(option, guildId.Value, defaultOption.DefaultValue);
        return defaultOption.DefaultValue;
    }

    public static async Task<char> GetChar(string option, ulong guildId)
    {
        return (await GetString(option, guildId))[0];
    }

    public static async Task<char> GetChar(string option)
    {
        return (await GetString(option))[0];
    }

    public static async Task<int> GetInt(string option, ulong guildId)
    {
        return Convert.ToInt32(await GetString(option, guildId));
    }

    public static async Task<int> GetInt(string option)
    {
        return Convert.ToInt32(await GetString(option));
    }

    public static async Task<bool> GetBool(string option, ulong guildId)
    {
        return (await GetString(option, guildId)).Equals("1");
    }

    public static async Task<bool> GetBool(string option)
    {
        return (await GetString(option)).Equals("1");
    }

    public static async Task<ulong> GetUlong(string option, ulong guildId)
    {
        return Convert.ToUInt64(await GetString(option, guildId));
    }

    public static async Task<ulong> GetUlong(string option)
    {
        return Convert.ToUInt64(await GetString(option));
    }

    public static async Task<DiscordRole> GetRole(string option, DiscordGuild guild)
    {
        var roleId = Convert.ToUInt64(await GetString(option, guild.Id));
        return guild.Roles.TryGetValue(roleId, out var result) ? result : null;
    }

    public static async Task<DiscordChannel> GetChannel(string option, DiscordGuild guild)
    {
        var channelId = Convert.ToUInt64(await GetString(option, guild.Id));
        return guild.Channels.TryGetValue(channelId, out var result) ? result : null;
    }

    public static async Task<bool> Set(int optionId, ulong guildId, object value)
    {
        var opt = new ConfigOptions().Get(optionId);
        return await SetInternal(opt, guildId, value);
    }

    public static async Task<bool> Set(string option, ulong guildId, object value)
    {
        var opt = new ConfigOptions().Get(option);
        return await SetInternal(opt, guildId, value);
    }

    private static async Task<bool> SetInternal(ConfigOption opt, ulong guildId, object value)
    {
        string valueStr;
        switch (opt.Type)
        {
            case ConfigType.Boolean when value is bool b:
                valueStr = b ? "1" : "0";
                break;
            case ConfigType.Int when value is int:
            case ConfigType.Char when value is char:
                valueStr = value.ToString();
                break;
            case ConfigType.String when value is string s:
                valueStr = s;
                break;
            case ConfigType.Role when value is ulong:
            case ConfigType.Channel when value is ulong:
                valueStr = value.ToString();
                break;
            case ConfigType.Role when value is DiscordRole role:
                valueStr = role.Id.ToString();
                break;
            case ConfigType.Channel when value is DiscordChannel channel:
                valueStr = channel.Id.ToString();
                break;
            default:
                return false;
        }

        if (Configuration.GuildConfigs.TryGetValue(guildId, out var guildConfigs))
        {
            // guild exists in cached configs

            var curr = guildConfigs.FirstOrDefault(x => x.ConfigOptionId == opt.Id);
            if (curr != null)
            {
                // config exists in cached guild config
                // -> edit in cached config and database
                curr.Value = valueStr;
                return await EntityRepository<AmadeusContext, Config>.ModifyAsync(x =>
                    x.Id == curr.Id, curr) != null;
            }

            // config does not exist in cached guild config
            // -> create in cached configs and add to database
            var cCfg = new Config {ConfigOptionId = opt.Id, GuildId = guildId, Value = valueStr};
            guildConfigs.Add(cCfg);
            return await EntityRepository<AmadeusContext, Config>.CreateAsync(cCfg) != null;
        }

        {
            // guild does not exist in cached configs
            // -> create cached config for guild, add option to that and database
            var cCfg = new Config {ConfigOptionId = opt.Id, GuildId = guildId, Value = valueStr};
            var cConfigs = new List<Config> {cCfg};
            Configuration.GuildConfigs.Add(guildId, cConfigs);
            return await EntityRepository<AmadeusContext, Config>.CreateAsync(cCfg) != null;
        }
    }
}