using System;
using System.Threading.Tasks;
using Amadeus.Db.Enums;
using Amadeus.Db.Models;
using logm.EntityRepository.Core;

namespace Amadeus.Db.Helper
{
    public static class ConfigHelper
    {
        public static async Task<string> GetString(string option, ulong? guildId = null)
        {
            if (guildId != null)
            {
                var cfgGuild = await EntityRepository<AmadeusContext, Config>.GetSingleAsync(x =>
                    x.ConfigOption.Name.Equals(option) &&
                    x.GuildId == guildId);

                if (cfgGuild != null) return cfgGuild.Value;

                var cfgDefault =
                    await EntityRepository<AmadeusContext, ConfigOption>.GetSingleAsync(x =>
                        x.Name.Equals(option));

                if (cfgDefault == null) return null;

                // if no config set, get default value, set for guild, and return
                // this avoids user confusion if default bot behaviour is ever changed
                await Set(option, guildId.Value, cfgDefault.DefaultValue);
                return cfgDefault.DefaultValue;
            }
            else
            {
                // when no guildId provided, return default value
                var cfgDefault =
                    await EntityRepository<AmadeusContext, ConfigOption>.GetSingleAsync(x =>
                        x.Name.Equals(option));
                return cfgDefault.DefaultValue;
            }
        }

        public static async Task<char> GetChar(string option, ulong? guildId = null)
        {
            return (await GetString(option, guildId))[0];
        }

        public static async Task<int> GetInt(string option, ulong? guildId = null)
        {
            return Convert.ToInt32(await GetString(option, guildId));
        }

        public static async Task<bool> GetBool(string option, ulong? guildId = null)
        {
            return (await GetString(option, guildId)).Equals("1");
        }

        public static async Task<ulong> GetUlong(string option, ulong? guildId = null)
        {
            return Convert.ToUInt64(await GetString(option, guildId));
        }

        public static async Task<bool> Set(string option, ulong guildId, object value)
        {
            var opt = await EntityRepository<AmadeusContext, ConfigOption>.GetSingleAsync(x =>
                x.Name.Equals(option));
            if (opt == null) return false;

            string valueStr = default;

            switch (opt.CsType)
            {
                case CsType.Boolean when value is bool b:
                    valueStr = b ? "1" : "0";
                    break;
                case CsType.Int when value is int:
                case CsType.Char when value is char:
                    valueStr = value.ToString();
                    break;
                case CsType.String when value is string s:
                    valueStr = s;
                    break;
                default:
                    return false;
            }

            var curr = await EntityRepository<AmadeusContext, Config>.GetSingleAsync(x =>
                x.ConfigOption.Name.Equals(option) &&
                x.GuildId == guildId);

            // does not exist, create
            if (curr == null)
                return await EntityRepository<AmadeusContext, Config>.CreateAsync(new Config
                    {GuildId = guildId, ConfigOptionId = opt.Id, Value = valueStr}) != null;

            // exists, modify
            curr.Value = valueStr;
            return await EntityRepository<AmadeusContext, Config>.ModifyAsync(x => x.Id == curr.Id, curr) != null;
        }
    }
}