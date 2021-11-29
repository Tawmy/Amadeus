using System.Text.RegularExpressions;
using Anilist4Net;
using Anilist4Net.Enums;
using DSharpPlus.Entities;

namespace Amadeus.Bot.Helper;

public static class AniListHelper
{
    public static string GetStatusString(MediaStatuses status)
    {
        return status switch
        {
            MediaStatuses.NOT_YET_RELEASED => "Not yet released",
            MediaStatuses.RELEASING => "Releasing",
            MediaStatuses.FINISHED => "Finished",
            MediaStatuses.HIATUS => "On hiatus",
            MediaStatuses.CANCELLED => "Cancelled",
            _ => string.Empty
        };
    }
    
    public static string ToDiscordMarkup(this string input)
    {
        input = Regex.Replace(input, "__", "**");
        input = Regex.Replace(input, "~!|!~", "||");
        return input;
    }

    public static string TruncateAndCloseSpoiler(this string input, int maxLength)
    {
        return input.Length <= maxLength ?
            input : Regex.Matches(input[..maxLength], "\\|\\|").Count % 2 != 0
                ? $"{input[..maxLength]}||…"
                : $"{input[..maxLength]}…";
    }

    public static void AddCommonMediaFieldsTop(this DiscordEmbedBuilder embed, Media media)
    {
        embed.WithTitle(media!.EnglishTitle ?? media.RomajiTitle ?? media.NativeTitle);
        embed.WithDescription($"{media.RomajiTitle}{Environment.NewLine}{media.NativeTitle}");
        embed.AddField("Released", $"{media.AiringStartDate:MMM yyyy}", true);

    }

    public static void AddCommonMediaFieldsBottom(this DiscordEmbedBuilder embed, Media media)
    {
        embed.AddField("Status", GetStatusString(media.Status), true);
        embed.AddField("Average Score", media.AverageScore != null ? $"{media.AverageScore}%" : "-", true);
        embed.AddField("Mean Score", media.MeanScore != null ? $"{media.MeanScore}%" : "-", true);
        embed.AddField("Genres", string.Join(", ", media.Genres));
        if (media.DescriptionMd != null)
        {
            embed.AddField("Description", media.DescriptionMd.StripHtml().ToDiscordMarkup().TruncateAndCloseSpoiler(140));
        }
    }

    public static void AddCommonMediaEmbedProperties(this DiscordEmbedBuilder embed, Media media)
    {
        embed.WithThumbnail(media!.CoverImageExtraLarge);
        embed.WithImageUrl(media.BannerImage);
        embed.WithFooter("AniList", "https://i.imgur.com/zqa6OEk.png");
        embed.WithColor(2010108);
    }

    public static DiscordLinkButtonComponent GetSiteButton(this Media media)
    {
        return new DiscordLinkButtonComponent(media.SiteUrl, "View all details on AniList");
    }
}