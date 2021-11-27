using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.RolesModule;

public class RolesCommand
{
    private readonly InteractionContext _ctx;

    public RolesCommand(InteractionContext ctx)
    {
        _ctx = ctx;
    }

    public async Task RunSlash()
    {
        await _ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        var dropdown = await GetDropdown();
        if (dropdown == null)
        {
            await _ctx.EditResponseAsync(
                new DiscordWebhookBuilder().WithContent("Failed to get list of self-assignable roles."));
        }

        var embed = new DiscordEmbedBuilder();
        embed.WithTitle("Self-assignable Roles");
        embed.WithDescription("Assign to or remove roles from yourself here.");
        await _ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed.Build()).AddComponents(dropdown));
    }

    private async Task<DiscordSelectComponent?> GetDropdown()
    {
        var roles = await RolesHelper.GetSelfAssignableRoles(_ctx.Guild);
        if (roles == null) return null;

        var options = roles.Select(x => new DiscordSelectComponentOption(x.Name, x.Id.ToString(),
            isDefault: _ctx.Member.Roles.Any(y => y.Id == x.Id))).ToList();
        return new DiscordSelectComponent("selfAssignRoles", "Select role(s)",
            options, minOptions: 0, maxOptions: options.Count);
    }
}