using System.Linq;
using System.Threading.Tasks;
using Amadeus.Db;
using Amadeus.Db.Models;
using Amadeus.Db.Statics;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using logm.EntityRepository.Core;

namespace Amadeus.Bot.Commands.ProfileModule
{
    public static class ProfileCommand
    {
        public static async Task Run(CommandContext ctx)
        {
            await Run(ctx, null);
        }

        public static async Task Run(CommandContext ctx, DiscordMember member)
        {
            var userEntries =
                await EntityRepository<AmadeusContext, ProfileEntry>.GetAllAsync(x =>
                    x.UserId == (member != null ? member.Id : ctx.User.Id));
            

            var embed = new DiscordEmbedBuilder
            {
                Title = member != null ? member.Nickname ?? member.Username :
                    ctx.Member != null ? ctx.Member.Nickname ?? ctx.Member.Username : ctx.User.Username
            };
            
            if (userEntries.Count == 0)
            {
                const string command = "TODO";
                embed.Description = member != null && member.Id != ctx.User.Id
                    ? $"{embed.Title} does not have a profile. Tell them to create one using {command}!"
                    : $"You do not have a profile. Create one using {command}!";
            }
            
            var fields = ProfileFields.Get(userEntries.Select(x => x.ProfileFieldId));
            var categories = ProfileFieldCategories.Get(fields.Select(x => x.ProfileFieldCategoryId));

            foreach (var category in categories)
            {
                embed.AddField("\u200b", $"***__{category.Name}__***");

                foreach (var entry in userEntries.Where(x =>
                    fields.First(y => y.Id == x.ProfileFieldId).ProfileFieldCategoryId == category.Id))
                {
                    entry.ProfileField = fields.First(x => x.Id == entry.ProfileFieldId);

                    var hasUrl = entry.ProfileField.HasUrl && !string.IsNullOrWhiteSpace(entry.Url);
                    embed.AddField(entry.ProfileField.Name,
                        hasUrl ? $"[{entry.Value}]({entry.Url})" : entry.Value,
                        true);
                }
            }

            await ctx.RespondAsync(embed.Build());
        }
    }
}