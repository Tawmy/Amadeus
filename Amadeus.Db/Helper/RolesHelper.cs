using Amadeus.Db.Enums;
using Amadeus.Db.Models;
using DSharpPlus.Entities;
using logm.EntityRepository.Core;

namespace Amadeus.Db.Helper;

public static class RolesHelper
{
    public static async Task<List<DiscordRole>> GetSelfAssignableRoles(DiscordGuild guild)
    {
        // TODO new assignable menus logic
        var assignableRoles =
            await EntityRepository<AmadeusContext, DiscordEntity>.GetAllAsync(x =>
                x.DiscordEntityType == DiscordEntityType.Role);

        return assignableRoles
            .Select(assignableRole => guild.Roles
                .FirstOrDefault(x => x.Value.Id == assignableRole.Id).Value)
            .ToList();
    }
}