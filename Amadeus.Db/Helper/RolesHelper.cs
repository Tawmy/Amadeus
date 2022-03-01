using Amadeus.Db.Models;
using DSharpPlus.Entities;
using logm.EntityRepository.Core;

namespace Amadeus.Db.Helper;

public static class RolesHelper
{
    public static async Task<List<SelfAssignMenu>> GetSelfAssignMenus(DiscordGuild guild)
    {
        try
        {
            return await EntityRepository<AmadeusContext, SelfAssignMenu>.GetAllAsync(x =>
                x.GuildId == guild.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}