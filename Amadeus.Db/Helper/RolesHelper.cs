using Amadeus.Db.Models;
using DSharpPlus.Entities;
using logm.EntityRepository.Core;

namespace Amadeus.Db.Helper;

public static class RolesHelper
{
    public static async Task<List<DiscordRole>> GetSelfAssignableRoles(DiscordGuild guild)
    {
        var assignableRoles =
            await EntityRepository<AmadeusContext, AssignableRole>.GetAllAsync();

        return assignableRoles
            .Select(assignableRole => guild.Roles
                .FirstOrDefault(x => x.Value.Id == assignableRole.Id).Value)
            .ToList();
    }
}