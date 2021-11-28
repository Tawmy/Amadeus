using Amadeus.Bot.Helper;
using Anilist4Net;
using Anilist4Net.Enums;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Humanizer;

namespace Amadeus.Bot.Commands.AniListModule;

public static class AnimeCommand
{
    public static async Task RunSlash(InteractionContext ctx, string title)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        var anime = await new Anilist4Net.Client().GetMediaBySearch(title, MediaTypes.ANIME);

        if (anime == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Anime \"{title}\" not found"));
        }

        var embed = new DiscordEmbedBuilder();
        embed.WithTitle(anime!.EnglishTitle ?? anime.RomajiTitle ?? anime.NativeTitle);
        embed.WithDescription($"{anime.RomajiTitle}{Environment.NewLine}{anime.NativeTitle}");
        embed.AddFields(anime);
        embed.WithThumbnail(anime.CoverImageExtraLarge);
        embed.WithImageUrl(anime.BannerImage);
        embed.WithFooter("AniList", "https://i.imgur.com/zqa6OEk.png");
        embed.WithColor(2010108);

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed.Build()));
    }

    private static void AddFields(this DiscordEmbedBuilder embed, Media anime)
    {
        // Row 1
        embed.AddField("Released", $"{anime.AiringStartDate:MMM yyyy}", true);
        if (anime.Episodes > 1)
        {
            embed.AddField("Episodes", anime.Episodes.ToString(), true);
        }
        embed.AddField("Status", AniListHelper.GetStatusString(anime.Status), true);
        
        // Row 2
        embed.AddField("Average Score", anime.AverageScore != null ? $"{anime.AverageScore}%" : "-", true);
        embed.AddField("Mean Score", anime.MeanScore != null ? $"{anime.MeanScore}%" : "-", true);

        // Row 3
        embed.AddField("Genres", string.Join(", ", anime.Genres));
        
        // Row 4
        embed.AddField("Description", anime.DescriptionMd.StripHtml().Truncate(140));
    }
}