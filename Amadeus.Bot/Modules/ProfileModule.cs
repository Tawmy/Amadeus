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

            foreach (var catFields in categories.Select(x => userEntries.Where(y =>
                fields.First(z => z.Id == y.ProfileFieldId).ProfileFieldCategoryId == x.Id)))
            foreach (var entry in catFields)
            {
                entry.ProfileField = fields.First(x => x.Id == entry.ProfileFieldId);

                var hasUrl = entry.ProfileField.HasUrl && !string.IsNullOrWhiteSpace(entry.Url);
                embed.AddField(entry.ProfileField.Name, hasUrl ? $"[{entry.Value}]({entry.Url})" : entry.Value, true);
            }

            await ctx.RespondAsync(embed.Build());
        }
    }
}