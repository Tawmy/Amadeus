using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Amadeus.Bot.Handler;

public static class AssignRolesHandler
{
    public static async Task AssignRoles(DiscordClient sender, ComponentInteractionCreateEventArgs e)
    {
        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
        
        var assignableRoles = await RolesHelper.GetSelfAssignableRoles(e.Guild);
        var member = await e.Guild.GetMemberAsync(e.User.Id);
        if (assignableRoles == null || member == null)
        {
            return; // TODO handle this error?
        }

        var memberRoles = member.Roles.ToList();

        // Add newly selected roles
        foreach (var newRole in assignableRoles.Where(x =>
                     e.Values.Contains(x.Id.ToString()) &&
                     !memberRoles.Select(y => y.Id).Contains(x.Id)))
        {
            await member.GrantRoleAsync(newRole);
        }

        // Remove unselected roles
        foreach (var revRole in assignableRoles.Where(x =>
                     !e.Values.Contains(x.Id.ToString()) &&
                     memberRoles.Select(y => y.Id).Contains(x.Id)))
        {
            await member.RevokeRoleAsync(revRole);
        }
    }
}