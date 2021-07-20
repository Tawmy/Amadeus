using System.Linq;
using System.Threading.Tasks;
using Amadeus.Db.Helper;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace Amadeus.Bot.Commands.ModerationModule
{
    public static class VerifyCommand
    {
        public static async Task Run(CommandContext ctx, DiscordMember member)
        {
            if (member == null)
            {
                await ctx.RespondAsync("Member not found");
                return;
            }

            var role = await ConfigHelper.GetRole("Verification Role", ctx.Guild);
            if (!member.Roles.Contains(role))
            {
                await member.GrantRoleAsync(role, $"Verification by {ctx.Member.Username}#{ctx.Member.Discriminator}");
                var embed = new DiscordEmbedBuilder
                {
                    Title = member.Nickname ?? member.Username,
                    Description = member.Mention
                };
                embed.WithAuthor($"{ctx.Member.Nickname ?? ctx.Member.Username} verified:",
                    iconUrl: ctx.Member.AvatarUrl);
                embed.WithThumbnail(member.AvatarUrl);
                embed.WithColor(DiscordColor.Blurple);
                await ctx.RespondAsync(embed.Build());
            }
            else
            {
                await ctx.RespondAsync($"{member.Nickname ?? member.Username} is already verified!");
            }
        }
    }
}