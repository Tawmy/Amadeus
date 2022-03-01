using Amadeus.Db;
using Amadeus.Db.Models;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.EntityFrameworkCore;

namespace Amadeus.Bot.Handler;

public static class SelfAssignRolesHandler
{
    public static async Task ShowRoleSelection(DiscordClient sender, ComponentInteractionCreateEventArgs e,
        int selfAssignMenuId)
    {
        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

        if (e.User is not DiscordMember member) return;

        var menu = await GetSelfAssignMenuById(selfAssignMenuId);
        if (menu.GuildId != member.Guild.Id) return; // Give out warning?

        // if menu has a required role, check and give out warning if user is missing it
        if (menu.RequiredRoleId != null && !member.Roles.Select(x => x.Id).Contains(menu.RequiredRoleId.Value))
        {
            var reqRole = e.Guild.Roles.First(x => x.Key == menu.RequiredRoleId).Value;
            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder()
                .WithContent($"You need the {reqRole.Name} role to use this function.")
                .AsEphemeral(true));
        }

        var menuComponent = GetMenuComponent(e.Guild, member, menu);
        
        var embed = new DiscordEmbedBuilder();
        embed.WithTitle(menu.Title);
        embed.WithDescription(menu.Description);
        await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder()
            .AddEmbed(embed.Build())
            .AddComponents(menuComponent)
            .AsEphemeral(true));
    }

    public static async Task AssignRoles(DiscordClient sender, ComponentInteractionCreateEventArgs e,
        int selfAssignMenuId)
    {
        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

        var menu = await GetSelfAssignMenuById(selfAssignMenuId);
        if (menu.GuildId != e.Guild.Id) return; // Give out warning?

        var member = await e.Guild.GetMemberAsync(e.User.Id);
        if (member == null) return; // TODO handle this error?

        var memberRoles = member.Roles.ToList();
        var menuRoles = e.Guild.Roles.Where(x =>
                menu.SelfAssignMenuDiscordEntityAssignments.Select(y => y.DiscordEntityId).Contains(x.Value.Id))
            .Select(x => x.Value).ToList();

        // Add newly selected roles
        foreach (var newRole in menuRoles.Where(x =>
                     e.Values.Contains(x.Id.ToString()) &&
                     !memberRoles.Select(y => y.Id).Contains(x.Id)))
            await member.GrantRoleAsync(newRole);

        // Remove unselected roles
        foreach (var revRole in menuRoles.Where(x =>
                     !e.Values.Contains(x.Id.ToString()) &&
                     memberRoles.Select(y => y.Id).Contains(x.Id)))
            await member.RevokeRoleAsync(revRole);
    }

    private static DiscordSelectComponent GetMenuComponent(DiscordGuild guild, DiscordMember member,
        SelfAssignMenu menu)
    {
        var roles = guild.Roles.Values.Where(x =>
            menu.SelfAssignMenuDiscordEntityAssignments.Select(y => y.DiscordEntityId).Contains(x.Id)).ToList();
        var options = roles.Select(x => new DiscordSelectComponentOption(x.Name, x.Id.ToString(),
            isDefault: member.Roles.Any(y => y.Id == x.Id))).ToList();
        return new DiscordSelectComponent($"selfAssignDropdown_{menu.Id}", "Select role(s)",
            options, minOptions: 0, maxOptions: options.Count);
    }

    private static async Task<SelfAssignMenu> GetSelfAssignMenuById(int selfAssignMenuId)
    {
        var context = new AmadeusContext();
        return await context.SelfAssignMenus.Where(x =>
                x.Id == selfAssignMenuId)
            .Include(x => x.RequiredRole)
            .Include(x => x.SelfAssignMenuDiscordEntityAssignments)
            .FirstAsync();
    }
}