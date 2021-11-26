using Amadeus.Bot.Checks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace Amadeus.Bot.Handler.Errors;

public static class SlashExecutionChecksFailedExceptionHandler
{
    public static async Task HandleException(SlashCommandErrorEventArgs e,
        SlashExecutionChecksFailedException ex)
    {
        var check = ex.FailedChecks[0];

        switch (check)
        {
            case ModeratorSlashAttribute attr:
                var embed = GetModeratorSlashAttributeEmbed(attr);
                await e.Context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder().AddEmbed(embed));
                break;
        }
    }

    private static DiscordEmbed GetModeratorSlashAttributeEmbed(ModeratorSlashAttribute attr)
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