using Amadeus.Bot.Checks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace Amadeus.Bot.Handler.Errors;

public static class ContextMenuExecutionChecksFailedExceptionHandler
{
    public static async Task HandleException(ContextMenuErrorEventArgs e,
        ContextMenuExecutionChecksFailedException ex)
    {
        var check = ex.FailedChecks[0];

        switch (check)
        {
            case ModeratorMenuAttribute attr:
                var embed = GetModeratorMenuAttributeEmbed(attr);
                await e.Context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder().AddEmbed(embed));
                break;
        }
    }
    
    private static DiscordEmbed GetModeratorMenuAttributeEmbed(ModeratorMenuAttribute attr)
    {
        var roleName = attr.ModeratorRole?.Name ?? "moderator";
        var embed = new DiscordEmbedBuilder
        {
            Title = "Missing permissions",
            Description = $"You need the {roleName} role to run this command."
        };
        embed.WithColor(DiscordColor.IndianRed);
        if (attr.Ctx != null)
        {
            embed.WithAuthor(attr.Ctx.Member.Nickname ?? attr.Ctx.Member.Username ?? attr.Ctx.User.Username,
                iconUrl: attr.Ctx.Member.AvatarUrl ?? attr.Ctx.User.AvatarUrl);
        }
        return embed.Build();
    }
}