using System;
using System.Threading.Tasks;
using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Humanizer;

namespace Amadeus.Bot.Events
{
    public class OnGuildMemberRemoved
    {
        public static async Task AmadeusOnGuildMemberRemoved(DiscordClient sender, GuildMemberRemoveEventArgs e)
        {
            var channel = await ConfigHelper.GetChannel("Mod Channel", e.Guild);
            if (channel == null) return;

            var embed = new DiscordEmbedBuilder();
            embed.WithTitle("User left");
            embed.WithColor(DiscordColor.IndianRed);
            embed.WithDescription(
                $"`{e.Member.Id}`{Environment.NewLine}{e.Member.Username}#{e.Member.Discriminator}");
            embed.WithFooter($"Joined {e.Member.JoinedAt.Humanize()}");
            embed.WithThumbnail(e.Member.AvatarUrl);
            await channel.SendMessageAsync(embed.Build());
        }
    }
}