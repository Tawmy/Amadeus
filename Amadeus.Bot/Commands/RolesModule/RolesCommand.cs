using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
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

        var roles = await RolesHelper.GetSelfAssignableRoles(_ctx.Guild);
        if (roles == null)
        {
            await _ctx.EditResponseAsync(
                new DiscordWebhookBuilder().WithContent("Failed to get list of self-assignable roles."));
            return;
        }

        var embed = new DiscordEmbedBuilder();
        embed.WithTitle("Self-assignable Roles");

        var botMsg = await SendRoleMessage(embed, roles, "Assign to or remove roles from yourself here.");

        // wait for button press
        var result = await botMsg.WaitForButtonAsync();

        if (result.TimedOut)
        {
            // break on timeout or when user clicked done button
            await SendRoleMessage(embed, null, "No roles were changed.");
        }

        await SendConfirmationMessage(roles, embed, result);
    }

    private async Task<DiscordMessage> SendRoleMessage(DiscordEmbedBuilder embed,
        ICollection<DiscordRole>? roles, string description)
    {
        embed.WithDescription(description);
        var webhookBuilder = new DiscordWebhookBuilder().AddEmbed(embed.Build());

        if (roles != null)
        {
            AddRoleButtons(roles, webhookBuilder);
        }

        return await _ctx.EditResponseAsync(webhookBuilder);
    }

    private void AddRoleButtons(ICollection<DiscordRole> roles, DiscordWebhookBuilder builder)
    {
        var userRoles = roles.Where(x => !_ctx.Member.Roles.Select(y => y.Id).Contains(x.Id));
        var btnsAdd = userRoles
            .Select(role => new DiscordButtonComponent(ButtonStyle.Primary, role.Id.ToString(), role.Name)).ToList();
        if (btnsAdd.Count > 0)
        {
            builder.AddComponents(btnsAdd);
        }

        var btnsRem = roles.Where(x => !userRoles.Contains(x))
            .Select(role => new DiscordButtonComponent(ButtonStyle.Danger, role.Id.ToString(), role.Name)).ToList();
        if (btnsRem.Count > 0)
        {
            builder.AddComponents(btnsRem);
        }
    }

    private async Task SendConfirmationMessage(List<DiscordRole> roles, DiscordEmbedBuilder embed,
        InteractivityResult<ComponentInteractionCreateEventArgs> result)
    {
        var role = roles.First(x => x.Id.ToString().Equals(result.Result.Id));

        string roleDescription;
        if (_ctx.Member.Roles.Any(x => x.Id == role.Id))
        {
            await _ctx.Member.RevokeRoleAsync(role);
            roleDescription = $"Removed {role.Name}.";
        }
        else
        {
            await _ctx.Member.GrantRoleAsync(role);
            roleDescription = $"Added {role.Name}.";
        }

        await SendRoleMessage(embed, null, roleDescription);
    }
}