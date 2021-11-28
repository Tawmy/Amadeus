using System.Text.RegularExpressions;
using Anilist4Net.Enums;

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
}