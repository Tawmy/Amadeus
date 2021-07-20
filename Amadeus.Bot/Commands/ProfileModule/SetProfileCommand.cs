using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amadeus.Db.Statics;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace Amadeus.Bot.Commands.ProfileModule
{
    public class SetProfileCommand
    {
        public static async Task Run(CommandContext ctx)
        {
            var pages = GeneratePages();
            await ctx.Channel.SendPaginatedMessageAsync(ctx.Member, pages);
        }

        private static IEnumerable<Page> GeneratePages()
        {
            var pages = new List<Page>();
            var profileFields = new ProfileFields().Get();

            foreach (var cat in new ProfileFieldCategories().Get())
            {
                var page = new Page();
                var embed = new DiscordEmbedBuilder
                {
                    Title = cat.Name,
                    Description = cat.Description
                };

                var cProfileFields = profileFields.Where(x =>
                        x.ProfileFieldCategoryId == cat.Id)
                    .ToList();
                foreach (var cProfileField in cProfileFields) embed.AddField(cProfileField.Name, "\u200b", true);

                page.Embed = embed.Build();
                pages.Add(page);
            }

            return pages;
        }
    }
}