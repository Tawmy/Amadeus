using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.RolesModule;

public static class RolesMsgCommand
{
    public static async Task RunSlash(InteractionContext ctx, string title, string? description,
        DiscordChannel? channel)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        
        var embed = new DiscordEmbedBuilder();
        embed.WithTitle(title);
        if (description != null)
        {
            embed.WithDescription(description);
        }

        var btn = new DiscordButtonComponent(ButtonStyle.Primary, "selfAssignButton", "Show roles");

        // Use provided channel if not null, else use channel command was run in
        channel = channel != null ? channel : ctx.Channel;

        await channel.SendMessageAsync(new DiscordMessageBuilder().WithEmbed(embed.Build()).AddComponents(btn));
        await ctx.DeleteResponseAsync();
    }
}