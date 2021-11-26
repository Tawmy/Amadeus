using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Humanizer;

namespace Amadeus.Bot.Events;

public static class GuildMemberAddedEvent
{
    public static async Task ClientOnGuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs e)
    {
        var channel = await ConfigHelper.GetChannel("Moderator Channel", e.Guild);
        if (channel == null) return;

        var embed = new DiscordEmbedBuilder();
        embed.WithTitle("User joined");
        embed.WithColor(DiscordColor.CornflowerBlue);
        embed.WithDescription(
            $"{e.Member.Mention}{Environment.NewLine}{e.Member.Username}#{e.Member.Discriminator}");
        embed.WithFooter($"Created {e.Member.CreationTimestamp.Humanize()}");
        embed.WithThumbnail(e.Member.AvatarUrl);
        await channel.SendMessageAsync(embed.Build());
    }
}