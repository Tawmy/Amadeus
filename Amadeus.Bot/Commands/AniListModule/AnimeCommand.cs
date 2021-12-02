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

        var anime = int.TryParse(title, out var result)
            ? await new Client().GetMediaById(result)
            : await new Client().GetMediaBySearch(title, MediaTypes.ANIME);

        if (anime == null ||
            !new[]
            {
                MediaFormats.TV, MediaFormats.ONA, MediaFormats.OVA, MediaFormats.MOVIE, MediaFormats.MOVIE,
                MediaFormats.MUSIC, MediaFormats.SPECIAL, MediaFormats.TV_SHORT
            }.Contains(anime.Format))
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Anime \"{title}\" not found"));
            return;
        }

        var embed = new DiscordEmbedBuilder();
        AniListHelper.AddCommonMediaFieldsTop(embed, anime);
        embed.AddFields(anime);
        AniListHelper.AddCommonMediaFieldsBottom(embed, anime);
        AniListHelper.AddCommonMediaEmbedProperties(embed, anime);
        var btn = AniListHelper.GetSiteButton(anime);
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed.Build()).AddComponents(btn));
    }

    private static void AddFields(this DiscordEmbedBuilder embed, Media anime)
    {
        if (anime.Episodes > 1) embed.AddField("Episodes", anime.Episodes.ToString(), true);

        if (anime.Format != MediaFormats.MOVIE || anime.Duration == null) return;

        var duration = TimeSpan.FromMinutes((int) anime.Duration);
        var durationString = duration.TotalMinutes < 60
            // If less than an hour, show minutes
            ? $"{duration.TotalMinutes} {(duration.TotalMinutes > 1 ? "mins" : "min")}"
            : duration.TotalMinutes % 60 == 0
                // If at least an hour and exactly N hours + 0 minutes
                ? $"{duration.Hours} {(duration.Hours == 1 ? "hour" : "hours")}"
                // If at least an hour and N hours + M minutes
                : $"{duration.Hours} {(duration.Hours == 1 ? "hour" : "hours")},{Environment.NewLine}" +
                  $"{duration.Minutes} {(duration.Minutes == 1 ? "min" : "mins")}";
        embed.AddField("Duration", durationString, true);
    }
}