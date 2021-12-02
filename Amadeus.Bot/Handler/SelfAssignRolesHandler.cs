using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Amadeus.Bot.Handler;

public static class SelfAssignRolesHandler
{
    public static async Task ShowRoleSelection(DiscordClient sender, ComponentInteractionCreateEventArgs e)
    {
        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

        if (e.User is not DiscordMember member) return; // todo handle this better

        // TODO replace with proper check later
        var verRole = await ConfigHelper.GetRole("Verification Role", e.Guild);
        if (verRole == null || !member.Roles.Contains(verRole))
        {
            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder()
                .WithContent("You need to be verified to use this function.")
                .AsEphemeral(true));
            return;
        }

        var dropdown = await GetDropdown(e.Guild, member);
        if (dropdown == null)
        {
            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder()
                .WithContent("Failed to get list of self-assignable roles.")
                .AsEphemeral(true));
            return;
        }

        var embed = new DiscordEmbedBuilder();
        embed.WithTitle("Self-assignable Roles");
        embed.WithDescription("Select roles to add to yourself here. Unselecting will remove them again.");
        await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder()
            .AddEmbed(embed.Build())
            .AddComponents(dropdown)
            .AsEphemeral(true));
    }

    public static async Task AssignRoles(DiscordClient sender, ComponentInteractionCreateEventArgs e)
    {
        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

        var assignableRoles = await RolesHelper.GetSelfAssignableRoles(e.Guild);
        var member = await e.Guild.GetMemberAsync(e.User.Id);
        if (assignableRoles is null || member == null) return; // TODO handle this error?

        var memberRoles = member.Roles.ToList();

        // Add newly selected roles
        foreach (var newRole in assignableRoles.Where(x =>
                     e.Values.Contains(x.Id.ToString()) &&
                     !memberRoles.Select(y => y.Id).Contains(x.Id)))
            await member.GrantRoleAsync(newRole);

        // Remove unselected roles
        foreach (var revRole in assignableRoles.Where(x =>
                     !e.Values.Contains(x.Id.ToString()) &&
                     memberRoles.Select(y => y.Id).Contains(x.Id)))
            await member.RevokeRoleAsync(revRole);
    }

    private static async Task<DiscordSelectComponent?> GetDropdown(DiscordGuild guild, DiscordMember member)
    {
        var roles = await RolesHelper.GetSelfAssignableRoles(guild);
        var options = roles.Select(x => new DiscordSelectComponentOption(x.Name, x.Id.ToString(),
            isDefault: member.Roles.Any(y => y.Id == x.Id))).ToList();
        return new DiscordSelectComponent("selfAssignDropdown", "Select role(s)",
            options, minOptions: 0, maxOptions: options.Count);
    }
}