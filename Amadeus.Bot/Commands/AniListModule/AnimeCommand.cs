using Amadeus.Bot.Helper;
using Anilist4Net;
using Anilist4Net.Enums;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.AniListModule;

public static class AnimeCommand
{
    public static async Task RunSlash(InteractionContext ctx, string title)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        var anime = await new Client().GetMediaBySearch(title, MediaTypes.ANIME);

        if (anime == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Anime \"{title}\" not found"));
        }

        var embed = new DiscordEmbedBuilder();
        embed.AddCommonMediaFieldsTop(anime!);
        embed.AddFields(anime!);
        embed.AddCommonMediaFieldsBottom(anime!);
        embed.AddCommonMediaEmbedProperties(anime!);
        var btn = anime!.GetSiteButton();
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed.Build()).AddComponents(btn));
    }

    private static void AddFields(this DiscordEmbedBuilder embed, Media anime)
    {
        if (anime.Episodes > 1)
        {
            embed.AddField("Episodes", anime.Episodes.ToString(), true);
        }
    }
}