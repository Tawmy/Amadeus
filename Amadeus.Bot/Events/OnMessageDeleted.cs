using System;
using System.Threading.Tasks;
using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Humanizer;

namespace Amadeus.Bot.Events
{
    public static class OnMessageDeleted
    {
        public static async Task AmadeusOnMessageDeleted(DiscordClient sender, MessageDeleteEventArgs e)
        {
            if (e.Guild == null) return;

            var channel = await ConfigHelper.GetChannel("Log Channel", e.Guild);
            if (channel == null) return;

            var embed = new DiscordEmbedBuilder();

            var titleSuffix = e.Message.Author == null ? " (not cached)" : string.Empty;
            embed.WithTitle($"Message deleted{titleSuffix}");

            embed.AddField("Channel", e.Message.Channel.Mention, true);

            if (e.Message.Author != null)
                embed.AddField("User", e.Message.Author.Mention, true);

            if (!string.IsNullOrWhiteSpace(e.Message.Content))
                embed.AddField("Content", e.Message.Content.Trim());

            embed.WithFooter(e.Message.CreationTimestamp < DateTimeOffset.Now.AddDays(-1)
                    ? $"{e.Message.CreationTimestamp:dd. MMMM yyyy}"
                    : e.Message.CreationTimestamp.Humanize(),
                "https://i.imgur.com/FkOFUCC.png");
            await channel.SendMessageAsync(embed.Build());
        }
    }
}