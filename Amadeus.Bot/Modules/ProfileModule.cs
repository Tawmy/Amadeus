using System.Linq;
using System.Threading.Tasks;
using Amadeus.Db;
using Amadeus.Db.Models;
using Amadeus.Db.Statics;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using logm.EntityRepository.Core;

namespace Amadeus.Bot.Modules
{
    public class ProfileModule : BaseCommandModule
    {
        [Command("profile")]
        public async Task ProfileCommand(CommandContext ctx)
        {
            var userEntries =
                await EntityRepository<AmadeusContext, ProfileEntry>.GetAllAsync(x => x.UserId == ctx.User.Id);

            var fields = ProfileFields.Get(userEntries.Select(x => x.ProfileFieldId));
            var categories = ProfileFieldCategories.Get(fields.Select(x => x.ProfileFieldCategoryId));

            var embed = new DiscordEmbedBuilder
            {
                Title = ctx.Member != null ? ctx.Member.Nickname ?? ctx.Member.Username : ctx.User.Username
            };

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